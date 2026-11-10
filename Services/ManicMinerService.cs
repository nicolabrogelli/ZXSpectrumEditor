using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZXSpectrumSpriteGenerator.Models;

namespace ZXSpectrumSpriteGenerator.Services
{
    /// <summary>
    /// Servizio dedicato all'importazione, esportazione e gestione dei livelli e delle caverne di Manic Miner.
    /// Supporta i formati ufficiali a 1024 byte per caverna (SkoolKit 45056 / $B000), binari grezzi (.bin),
    /// sorgenti Assembly Z80 e iniezione/estrazione da snapshot ZX Spectrum (.sna).
    /// </summary>
    public static class ManicMinerService
    {
        public const int CavernBlockSize = 1024;
        public const int TotalCaverns = 20;
        public const int TotalCavernsSize = TotalCaverns * CavernBlockSize; // 20480 byte (20 KB)
        public const int BaseMemoryAddress = 45056; // $B000

        // Offset dell'indirizzo $B000 all'interno di uno snapshot .SNA 48K (Header 27 byte + offset da 16384)
        public const int SnaHeaderSize = 27;
        public const int SnaRamBase = 16384;
        public const int Sna45056Offset = SnaHeaderSize + (BaseMemoryAddress - SnaRamBase); // 27 + 28672 = 28699

        public static readonly string[] OriginalCavernNames = new string[]
        {
            "Central Cavern",
            "The Cold Room",
            "The Menagerie",
            "Abandoned Uranium Workings",
            "Eugene's Lair",
            "Processing Plant",
            "The Vat",
            "Miner Willy meets the Kong Beast",
            "Wacky Amoebatrons",
            "The Endorian Forest",
            "The Attack of the Mutant Telephones",
            "Return of the Alien Kong Beast",
            "Ore Refinery",
            "Skylab Landing Bay",
            "The Bank",
            "The Sixteenth Cavern",
            "The Warehouse",
            "Amoebatrons' Revenge",
            "Solar Power Generator",
            "The Final Barrier"
        };

        /// <summary>
        /// Esporta una singola caverna in un array di 1024 byte.
        /// </summary>
        public static byte[] ExportCavernToBinary(ManicMinerCavern cavern)
        {
            if (cavern == null) return new byte[CavernBlockSize];
            return cavern.To1024ByteArray();
        }

        /// <summary>
        /// Esporta un set di caverne in un unico blocco binario sequenziale da 20.480 byte (20 caverne da 1024 byte).
        /// </summary>
        public static byte[] ExportAllCavernsToBinary(IList<ManicMinerCavern> caverns)
        {
            byte[] buffer = new byte[TotalCavernsSize];
            if (caverns == null) return buffer;

            for (int i = 0; i < Math.Min(TotalCaverns, caverns.Count); i++)
            {
                if (caverns[i] != null)
                {
                    byte[] cBytes = caverns[i].To1024ByteArray();
                    Array.Copy(cBytes, 0, buffer, i * CavernBlockSize, CavernBlockSize);
                }
            }
            return buffer;
        }

        /// <summary>
        /// Importa una o più caverne da dati binari (.bin, dump o snapshot .sna).
        /// Rileva automaticamente la tipologia di file in base alle dimensioni.
        /// </summary>
        public static List<ManicMinerCavern> ImportFromBinary(byte[] data)
        {
            var list = new List<ManicMinerCavern>();
            if (data == null || data.Length == 0) return list;

            int startOffset = 0;

            // Rilevamento snapshot ZX Spectrum .SNA 48K (49179 byte standard)
            if (data.Length == 49179 || (data.Length > Sna45056Offset + CavernBlockSize && data.Length < 49200))
            {
                startOffset = Sna45056Offset;
            }

            int availableBytes = data.Length - startOffset;
            int numCaverns = Math.Min(TotalCaverns, availableBytes / CavernBlockSize);
            if (numCaverns <= 0 && availableBytes >= CavernBlockSize) numCaverns = 1;

            for (int i = 0; i < numCaverns; i++)
            {
                int off = startOffset + (i * CavernBlockSize);
                if (off + CavernBlockSize <= data.Length)
                {
                    var c = ManicMinerCavern.From1024ByteArray(data, off, i);
                    if (string.IsNullOrWhiteSpace(c.Name) && i < OriginalCavernNames.Length)
                    {
                        c.Name = OriginalCavernNames[i];
                    }
                    list.Add(c);
                }
            }

            return list;
        }

        /// <summary>
        /// Esporta una singola caverna in codice Assembly Z80 conforme a SkoolKit.
        /// </summary>
        public static string ExportCavernToAssembly(ManicMinerCavern cavern, AsmDirective directive = AsmDirective.Defb)
        {
            if (cavern == null) return string.Empty;
            int addr = BaseMemoryAddress + (cavern.CavernIndex * CavernBlockSize);
            return cavern.ToSkoolKitAssembly(addr, directive);
        }

        /// <summary>
        /// Esporta tutte le caverne specificate in un unico file sorgente Assembly Z80.
        /// </summary>
        public static string ExportAllCavernsToAssembly(IList<ManicMinerCavern> caverns, AsmDirective directive = AsmDirective.Defb)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("; ===========================================================================");
            sb.AppendLine("; MANIC MINER - TUTTE LE CAVERNE (Disassembly SkoolKit / Sjasmplus / Pasmo)");
            sb.AppendLine($"; Base Address: {BaseMemoryAddress} (${BaseMemoryAddress:X4})");
            sb.AppendLine("; ===========================================================================");
            sb.AppendLine();

            if (caverns != null)
            {
                for (int i = 0; i < caverns.Count; i++)
                {
                    sb.AppendLine(ExportCavernToAssembly(caverns[i], directive));
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Inietta le caverne modificate direttamente in un file snapshot .SNA di Manic Miner esistente,
        /// consentendo di testarle immediatamente su emulatore.
        /// </summary>
        public static void InjectIntoSna(string sourceSnaPath, string targetSnaPath, IList<ManicMinerCavern> caverns)
        {
            if (!File.Exists(sourceSnaPath))
                throw new FileNotFoundException("File snapshot .SNA di origine non trovato.", sourceSnaPath);

            byte[] snaBytes = File.ReadAllBytes(sourceSnaPath);
            if (snaBytes.Length < Sna45056Offset + CavernBlockSize)
                throw new InvalidDataException("Il file specificato non sembra uno snapshot ZX Spectrum .SNA 48K valido.");

            if (caverns != null)
            {
                for (int i = 0; i < Math.Min(TotalCaverns, caverns.Count); i++)
                {
                    if (caverns[i] != null)
                    {
                        byte[] cBytes = caverns[i].To1024ByteArray();
                        int destOffset = Sna45056Offset + (i * CavernBlockSize);
                        if (destOffset + CavernBlockSize <= snaBytes.Length)
                        {
                            Array.Copy(cBytes, 0, snaBytes, destOffset, CavernBlockSize);
                        }
                    }
                }
            }

            File.WriteAllBytes(targetSnaPath, snaBytes);
        }

        /// <summary>
        /// Crea un progetto SpriteProject completo inizializzato per Manic Miner,
        /// con la caverna "Central Cavern" autentica già configurata con tile, sprite e parametri.
        /// </summary>
        public static SpriteProject CreateManicMinerProject()
        {
            var project = new SpriteProject
            {
                Title = "Manic Miner Studio (ZX Spectrum 48K)",
                TilesPerMap = 8,
                DefaultWidth = 16,
                DefaultHeight = 16
            };

            project.Sprites.Clear();
            project.Tiles.Clear();
            project.Maps.Clear();

            // Aggiunge la caverna Central Cavern originale
            var centralCavern = ManicMinerCavern.CreateCentralCavernDefault();

            // Aggiunge gli sprite dei guardiani di Central Cavern nel progetto
            for (int s = 0; s < 8; s++)
            {
                var sprite = new Sprite(16, 16, $"Guardian_Frame_{s + 1}")
                {
                    GroupName = "Central Cavern Guardians",
                    InkColor = 6, // Yellow
                    PaperColor = 0,
                    Bright = true
                };
                byte[] frameBytes = new byte[32];
                if (centralCavern.GuardianGraphics != null && centralCavern.GuardianGraphics.Length >= 256)
                {
                    Array.Copy(centralCavern.GuardianGraphics, s * 32, frameBytes, 0, 32);
                }
                sprite.SetFromBytesRowByRow(frameBytes);
                project.Sprites.Add(sprite);
            }

            // Converte la caverna in GameMap e la registra nel progetto
            var map = centralCavern.ToGameMap(project);
            project.Maps.Add(map);
            project.SelectedMapIndex = 0;
            project.SelectedTileIndex = 0;
            project.SelectedIndex = 0;

            return project;
        }
    }
}
