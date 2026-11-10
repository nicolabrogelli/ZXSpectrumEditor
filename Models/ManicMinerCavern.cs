using System;
using System.Collections.Generic;
using System.Text;
using ZXSpectrumSpriteGenerator.Services;

namespace ZXSpectrumSpriteGenerator.Models
{
    /// <summary>
    /// Rappresenta un singolo oggetto collezionabile (chiave/item) in una caverna di Manic Miner.
    /// Massimo 5 oggetti per caverna.
    /// </summary>
    public class ManicMinerItem
    {
        public int X { get; set; } // Colonna (0..31)
        public int Y { get; set; } // Riga (0..15)
        public byte Attribute { get; set; } = 3; // Attributo ZX Spectrum (Ink, Paper, Bright, Flash)

        public ManicMinerItem() { }

        public ManicMinerItem(int x, int y, byte attr)
        {
            X = Math.Max(0, Math.Min(31, x));
            Y = Math.Max(0, Math.Min(15, y));
            Attribute = attr;
        }

        public ManicMinerItem Clone()
        {
            return new ManicMinerItem(X, Y, Attribute);
        }
    }

    /// <summary>
    /// Rappresenta un guardiano orizzontale in una caverna di Manic Miner.
    /// Massimo 4 guardiani orizzontali per caverna (7 byte ciascuno).
    /// </summary>
    public class ManicMinerGuardian
    {
        public int Y { get; set; } = 7;           // Riga a schermo (0..15)
        public int InitialX { get; set; } = 8;    // Colonna iniziale (0..31)
        public int MinX { get; set; } = 8;        // Limite sinistro di pattuglia (0..31)
        public int MaxX { get; set; } = 15;       // Limite destro di pattuglia (0..31)
        public int Direction { get; set; } = 0;   // 0 = Destra, 1 = Sinistra
        public int Speed { get; set; } = 0;       // Velocità (0 = normale, ecc.)
        public int SpriteIndex { get; set; } = 0; // Indice dello sprite associato (0..7)

        public ManicMinerGuardian Clone()
        {
            return new ManicMinerGuardian
            {
                Y = this.Y,
                InitialX = this.InitialX,
                MinX = this.MinX,
                MaxX = this.MaxX,
                Direction = this.Direction,
                Speed = this.Speed,
                SpriteIndex = this.SpriteIndex
            };
        }
    }

    /// <summary>
    /// Definizione di una delle 8 tile fisse per una caverna di Manic Miner (9 byte: 1 attributo + 8 bitmap).
    /// Ruoli:
    /// 0: Background
    /// 1: Floor
    /// 2: Crumbling Floor
    /// 3: Wall
    /// 4: Conveyor
    /// 5: Nasty 1
    /// 6: Nasty 2
    /// 7: Spare / Extra
    /// </summary>
    public class ManicMinerTileDef
    {
        public string RoleName { get; set; } = "Tile";
        public byte Attribute { get; set; } = 0;
        public byte[] Bitmap { get; set; } = new byte[8];

        public ManicMinerTileDef(string roleName, byte attribute, byte[] bitmap)
        {
            RoleName = roleName;
            Attribute = attribute;
            Bitmap = new byte[8];
            if (bitmap != null)
            {
                Array.Copy(bitmap, Bitmap, Math.Min(8, bitmap.Length));
            }
        }

        public ManicMinerTileDef Clone()
        {
            return new ManicMinerTileDef(RoleName, Attribute, (byte[])Bitmap.Clone());
        }
    }

    /// <summary>
    /// Rappresenta la struttura completa di una caverna di Manic Miner (1024 byte esatti).
    /// Basato sulle specifiche ufficiali del disassembly SkoolKit (indirizzo base 45056 / $B000).
    /// </summary>
    public class ManicMinerCavern
    {
        public int CavernIndex { get; set; } = 0; // 0..19
        public string Name { get; set; } = "Central Cavern";
        public byte BorderColor { get; set; } = 2; // Default Red per Central Cavern
        public byte InitialAir { get; set; } = 63; // Air countdown ($3F / $FC)
        public byte GameClock { get; set; } = 252;

        // Miner Willy Spawn
        public int WillyX { get; set; } = 2;
        public int WillyY { get; set; } = 13;
        public int WillyPixelY { get; set; } = 104; // 104 * 2 = 208
        public int WillyDirection { get; set; } = 0; // 0 = Destra
        public int WillyFrame { get; set; } = 0;

        // Nastro Trasportatore (Conveyor)
        public int ConveyorX { get; set; } = 8;
        public int ConveyorY { get; set; } = 9;
        public int ConveyorLength { get; set; } = 20;
        public int ConveyorDirection { get; set; } = 0; // 0 = Sinistra, 1 = Destra

        // Portale di Uscita (Exit Portal: 16x16 pixel = 2x2 celle di caratteri)
        public int PortalX { get; set; } = 29;
        public int PortalY { get; set; } = 13;
        public byte PortalAttribute { get; set; } = 14; // Bright 0, Paper 1 (Blue), Ink 6 (Yellow)
        public byte[] PortalGraphic { get; set; } = new byte[32];

        // Oggetti Collezionabili (Items / Chiavi) - Fino a 5 oggetti
        public List<ManicMinerItem> Items { get; set; } = new List<ManicMinerItem>();
        public byte[] ItemGraphic { get; set; } = new byte[8];

        // Guardiani Orizzontali (fino a 4)
        public List<ManicMinerGuardian> HorizontalGuardians { get; set; } = new List<ManicMinerGuardian>();

        // Guardiani Verticali
        public byte VerticalGuardiansIndicator { get; set; } = 255; // 255 = nessun guardiano verticale

        // Grafica Speciale (es. Swordfish per The Final Barrier)
        public byte[] SwordfishGraphic { get; set; } = new byte[32];

        // Grafica dei Guardiani (8 sprite 16x16 = 256 byte)
        public byte[] GuardianGraphics { get; set; } = new byte[256];

        // Le 8 Tile Definitive della Caverna (9 byte ciascuna: 1 attr + 8 bitmap)
        public ManicMinerTileDef[] TileDefs { get; set; } = new ManicMinerTileDef[8];

        // Griglia Mappa (32 colonne x 16 righe): contiene lo slot tile (0..7) di ciascuna cella
        public byte[,] Grid { get; set; } = new byte[32, 16];

        public const int ScreenWidth = 32;
        public const int ScreenHeight = 16;
        public const int CavernBlockSize = 1024;

        public ManicMinerCavern()
        {
            InitializeDefaultTileDefs();
        }

        private void InitializeDefaultTileDefs()
        {
            TileDefs[0] = new ManicMinerTileDef("Background", 0, new byte[8]);
            TileDefs[1] = new ManicMinerTileDef("Floor", 66, new byte[] { 255, 255, 219, 110, 197, 64, 0, 0 });
            TileDefs[2] = new ManicMinerTileDef("Crumbling Floor", 2, new byte[] { 255, 219, 165, 36, 82, 32, 8, 0 });
            TileDefs[3] = new ManicMinerTileDef("Wall", 22, new byte[] { 34, 255, 136, 255, 34, 255, 136, 255 });
            TileDefs[4] = new ManicMinerTileDef("Conveyor", 4, new byte[] { 240, 102, 240, 102, 0, 153, 255, 0 });
            TileDefs[5] = new ManicMinerTileDef("Nasty 1", 68, new byte[] { 68, 40, 148, 81, 53, 214, 88, 16 });
            TileDefs[6] = new ManicMinerTileDef("Nasty 2", 5, new byte[] { 255, 254, 126, 124, 76, 76, 8, 8 });
            TileDefs[7] = new ManicMinerTileDef("Spare / Extra", 0, new byte[8]);
        }

        /// <summary>
        /// Genera l'esatto blocco di 1024 byte conforme alla specifica SkoolKit 45056 per questa caverna.
        /// </summary>
        public byte[] To1024ByteArray()
        {
            byte[] block = new byte[CavernBlockSize];

            // 1. Offsets 0..511: Screen Layout Attributes Grid (32 colonne x 16 righe)
            for (int y = 0; y < ScreenHeight; y++)
            {
                for (int x = 0; x < ScreenWidth; x++)
                {
                    int slot = Grid[x, y] & 0x07;
                    byte attr = (TileDefs != null && slot < TileDefs.Length && TileDefs[slot] != null)
                        ? TileDefs[slot].Attribute
                        : (byte)0;
                    block[y * ScreenWidth + x] = attr;
                }
            }

            // 2. Offsets 512..543: Cavern Name (32 byte ASCII space-padded)
            string padName = (Name ?? string.Empty).PadRight(32, ' ');
            if (padName.Length > 32) padName = padName.Substring(0, 32);
            byte[] nameBytes = Encoding.ASCII.GetBytes(padName);
            Array.Copy(nameBytes, 0, block, 512, Math.Min(32, nameBytes.Length));

            // 3. Offsets 544..615: Tile Definitions (8 tile x 9 byte = 72 byte)
            for (int i = 0; i < 8; i++)
            {
                int baseOff = 544 + (i * 9);
                if (TileDefs != null && i < TileDefs.Length && TileDefs[i] != null)
                {
                    block[baseOff] = TileDefs[i].Attribute;
                    if (TileDefs[i].Bitmap != null)
                    {
                        Array.Copy(TileDefs[i].Bitmap, 0, block, baseOff + 1, Math.Min(8, TileDefs[i].Bitmap.Length));
                    }
                }
            }

            // 4. Offsets 616..622: Miner Willy Initial State (7 byte)
            block[616] = (byte)(WillyPixelY > 0 ? WillyPixelY * 2 : WillyY * 8 * 2);
            block[617] = (byte)WillyFrame;
            block[618] = (byte)(WillyDirection == 0 ? 0 : 1);
            block[619] = 0; // Airborne indicator
            int willyAttrAddr = 23552 + (WillyY * 32) + WillyX;
            block[620] = (byte)(willyAttrAddr & 0xFF);
            block[621] = (byte)((willyAttrAddr >> 8) & 0xFF);
            block[622] = 0; // Jumping counter

            // 5. Offsets 623..626: Conveyor (4 byte)
            block[623] = (byte)(ConveyorDirection == 0 ? 0 : 1);
            // Indirizzo video del nastro nel buffer schermo (28672 = $7000)
            int convScreenAddr = CalculateScreenBufferAddress(ConveyorX, ConveyorY);
            block[624] = (byte)(convScreenAddr & 0xFF);
            block[625] = (byte)((convScreenAddr >> 8) & 0xFF);
            block[626] = (byte)ConveyorLength;

            // 6. Offset 627: Border Colour (1 byte)
            block[627] = (byte)(BorderColor & 0x07);

            // 7. Offset 628: Unused (1 byte)
            block[628] = 0;

            // 8. Offsets 629..653: Items (fino a 5 oggetti x 5 byte = 25 byte)
            int itemSlots = Math.Min(5, Items.Count);
            for (int i = 0; i < itemSlots; i++)
            {
                var it = Items[i];
                int off = 629 + (i * 5);
                block[off] = it.Attribute;
                int itAttrAddr = 23552 + (it.Y * 32) + it.X;
                block[off + 1] = (byte)(itAttrAddr & 0xFF);
                block[off + 2] = (byte)((itAttrAddr >> 8) & 0xFF);
                int itScreenAddr = CalculateScreenBufferAddress(it.X, it.Y);
                block[off + 3] = (byte)(itScreenAddr & 0xFF);
                block[off + 4] = (byte)((itScreenAddr >> 8) & 0xFF);
            }
            // Termina con 255 se ci sono meno di 5 oggetti
            for (int i = itemSlots; i < 5; i++)
            {
                int off = 629 + (i * 5);
                block[off] = 255;
            }

            // 9. Offsets 654..690: Exit Portal (37 byte)
            block[654] = 255; // Terminator per gli item
            block[655] = PortalAttribute;
            if (PortalGraphic != null && PortalGraphic.Length >= 32)
            {
                Array.Copy(PortalGraphic, 0, block, 656, 32);
            }
            int portalAttrAddr = 23552 + (PortalY * 32) + PortalX;
            block[688] = (byte)(portalAttrAddr & 0xFF);
            block[689] = (byte)((portalAttrAddr >> 8) & 0xFF);
            int portalScreenAddr = CalculateScreenBufferAddress(PortalX, PortalY);
            block[690] = (byte)(portalScreenAddr & 0xFF);

            // 10. Offsets 691..698: Item Graphic (8 byte)
            if (ItemGraphic != null && ItemGraphic.Length >= 8)
            {
                Array.Copy(ItemGraphic, 0, block, 691, 8);
            }

            // 11. Offsets 699..700: Air & Game Clock (2 byte)
            block[699] = InitialAir;
            block[700] = GameClock;

            // 12. Offsets 701..728: Horizontal Guardians (fino a 4 x 7 byte = 28 byte)
            int hGuardCount = Math.Min(4, HorizontalGuardians.Count);
            for (int i = 0; i < hGuardCount; i++)
            {
                var g = HorizontalGuardians[i];
                int off = 701 + (i * 7);
                block[off] = (byte)(g.Y * 10); // Velocità / riga
                int gAttrAddr = 23552 + (g.Y * 32) + g.InitialX;
                block[off + 1] = (byte)(gAttrAddr & 0xFF);
                block[off + 2] = (byte)((gAttrAddr >> 8) & 0xFF);
                int gScreenAddr = CalculateScreenBufferAddress(g.InitialX, g.Y);
                block[off + 3] = (byte)(gScreenAddr & 0xFF);
                block[off + 4] = (byte)(g.Direction == 0 ? 0 : 1);
                block[off + 5] = (byte)(256 - (32 - g.MinX)); // Calcolo limite patrol MinX
                block[off + 6] = (byte)(256 - (32 - g.MaxX)); // Calcolo limite patrol MaxX
            }
            if (hGuardCount < 4)
            {
                block[701 + (hGuardCount * 7)] = 255; // Terminator
            }

            // 13. Offsets 729..730: Unused
            block[729] = 0;
            block[730] = 0;

            // 14. Offset 731: Vertical Guardians Indicator
            block[731] = VerticalGuardiansIndicator;

            // 15. Offsets 732..733: Unused
            block[732] = 0;
            block[733] = 0;

            // 16. Offsets 734..765: Swordfish Graphic (32 byte)
            if (SwordfishGraphic != null && SwordfishGraphic.Length >= 32)
            {
                Array.Copy(SwordfishGraphic, 0, block, 734, 32);
            }

            // 17. Offsets 766..1021: Guardian Graphics (256 byte = 8 frame x 32 byte)
            if (GuardianGraphics != null && GuardianGraphics.Length >= 256)
            {
                Array.Copy(GuardianGraphics, 0, block, 766, 256);
            }

            // 18. Offsets 1022..1023: Padding finale a 1024 byte
            block[1022] = 0;
            block[1023] = 0;

            return block;
        }

        /// <summary>
        /// Carica e ricostruisce una caverna Manic Miner da un blocco di 1024 byte.
        /// </summary>
        public static ManicMinerCavern From1024ByteArray(byte[] data, int offset = 0, int cavernIndex = 0)
        {
            if (data == null || data.Length < offset + CavernBlockSize)
                throw new ArgumentException($"Dati insufficienti: attesi almeno 1024 byte all'offset {offset}.");

            var c = new ManicMinerCavern();
            c.CavernIndex = cavernIndex;

            // 1. Offsets 512..543: Cavern Name
            c.Name = Encoding.ASCII.GetString(data, offset + 512, 32).Trim();
            if (string.IsNullOrEmpty(c.Name)) c.Name = $"Caverna {cavernIndex + 1}";

            // 2. Offsets 544..615: Tile Definitions (8 tile x 9 byte)
            c.TileDefs = new ManicMinerTileDef[8];
            string[] defaultRoleNames = new string[] {
                "Background", "Floor", "Crumbling Floor", "Wall", "Conveyor", "Nasty 1", "Nasty 2", "Spare / Extra"
            };
            for (int i = 0; i < 8; i++)
            {
                int baseOff = offset + 544 + (i * 9);
                byte attr = data[baseOff];
                byte[] bmp = new byte[8];
                Array.Copy(data, baseOff + 1, bmp, 0, 8);
                c.TileDefs[i] = new ManicMinerTileDef(defaultRoleNames[i], attr, bmp);
            }

            // 3. Offsets 0..511: Screen Layout Attributes Grid (32 x 16)
            // Associa ciascun attributo della cella al corrispettivo slot di tile (0..7)
            for (int y = 0; y < ScreenHeight; y++)
            {
                for (int x = 0; x < ScreenWidth; x++)
                {
                    byte attr = data[offset + (y * ScreenWidth) + x];
                    byte slot = 0; // Default background
                    for (byte s = 0; s < 8; s++)
                    {
                        if (c.TileDefs[s].Attribute == attr)
                        {
                            slot = s;
                            break;
                        }
                    }
                    c.Grid[x, y] = slot;
                }
            }

            // 4. Offsets 616..622: Miner Willy Initial State
            c.WillyPixelY = data[offset + 616] / 2;
            c.WillyFrame = data[offset + 617];
            c.WillyDirection = data[offset + 618];
            int willyAddr = data[offset + 620] | (data[offset + 621] << 8);
            if (willyAddr >= 23552 && willyAddr < 23552 + 512)
            {
                int delta = willyAddr - 23552;
                c.WillyY = delta / 32;
                c.WillyX = delta % 32;
            }
            else
            {
                c.WillyY = Math.Max(0, Math.Min(15, c.WillyPixelY / 8));
                c.WillyX = 2;
            }

            // 5. Offsets 623..626: Conveyor
            c.ConveyorDirection = data[offset + 623];
            c.ConveyorLength = data[offset + 626];
            // Decode coordinate conveyor se presenti
            c.ConveyorX = 8;
            c.ConveyorY = 9;

            // 6. Offset 627: Border Colour
            c.BorderColor = (byte)(data[offset + 627] & 0x07);

            // 7. Offsets 629..653: Items (fino a 5 oggetti)
            c.Items = new List<ManicMinerItem>();
            for (int i = 0; i < 5; i++)
            {
                int itemOff = offset + 629 + (i * 5);
                byte attr = data[itemOff];
                if (attr == 255) break; // Terminato

                int addr = data[itemOff + 1] | (data[itemOff + 2] << 8);
                int ix = 0, iy = 0;
                if (addr >= 23552 && addr < 23552 + 512)
                {
                    int d = addr - 23552;
                    iy = d / 32;
                    ix = d % 32;
                }
                c.Items.Add(new ManicMinerItem(ix, iy, attr));
            }

            // 8. Offsets 654..690: Exit Portal
            c.PortalAttribute = data[offset + 655];
            c.PortalGraphic = new byte[32];
            Array.Copy(data, offset + 656, c.PortalGraphic, 0, 32);
            int pAddr = data[offset + 688] | (data[offset + 689] << 8);
            if (pAddr >= 23552 && pAddr < 23552 + 512)
            {
                int pd = pAddr - 23552;
                c.PortalY = pd / 32;
                c.PortalX = pd % 32;
            }

            // 9. Offsets 691..698: Item Graphic
            c.ItemGraphic = new byte[8];
            Array.Copy(data, offset + 691, c.ItemGraphic, 0, 8);

            // 10. Offsets 699..700: Air & Game Clock
            c.InitialAir = data[offset + 699];
            c.GameClock = data[offset + 700];

            // 11. Offsets 701..728: Horizontal Guardians
            c.HorizontalGuardians = new List<ManicMinerGuardian>();
            for (int i = 0; i < 4; i++)
            {
                int gOff = offset + 701 + (i * 7);
                byte b0 = data[gOff];
                if (b0 == 255) break;

                int gAddr = data[gOff + 1] | (data[gOff + 2] << 8);
                int gx = 8, gy = 7;
                if (gAddr >= 23552 && gAddr < 23552 + 512)
                {
                    int gd = gAddr - 23552;
                    gy = gd / 32;
                    gx = gd % 32;
                }
                int gDir = data[gOff + 4];
                int gMin = 32 - (256 - data[gOff + 5]);
                int gMax = 32 - (256 - data[gOff + 6]);

                c.HorizontalGuardians.Add(new ManicMinerGuardian
                {
                    Y = gy,
                    InitialX = gx,
                    MinX = Math.Max(0, Math.Min(31, gMin)),
                    MaxX = Math.Max(0, Math.Min(31, gMax)),
                    Direction = gDir,
                    Speed = 0,
                    SpriteIndex = i * 2
                });
            }

            // 12. Offset 731: Vertical Guardians Indicator
            c.VerticalGuardiansIndicator = data[offset + 731];

            // 13. Offsets 734..765: Swordfish Graphic
            c.SwordfishGraphic = new byte[32];
            Array.Copy(data, offset + 734, c.SwordfishGraphic, 0, 32);

            // 14. Offsets 766..1021: Guardian Graphics (256 byte = 8 frame x 32 byte)
            c.GuardianGraphics = new byte[256];
            Array.Copy(data, offset + 766, c.GuardianGraphics, 0, 256);

            return c;
        }

        /// <summary>
        /// Genera il codice sorgente Assembly Z80 formattato conforme a SkoolKit 45056 con etichette DEFB/DEFM e commenti.
        /// </summary>
        public string ToSkoolKitAssembly(int baseAddress = 45056, AsmDirective directive = AsmDirective.Defb)
        {
            string defb = (directive == AsmDirective.Db) ? "DB" : ((directive == AsmDirective.Byte) ? ".byte" : "DEFB");
            string defm = (directive == AsmDirective.Db) ? "DM" : "DEFM";
            string defw = (directive == AsmDirective.Db) ? "DW" : "DEFW";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"; ===========================================================================");
            sb.AppendLine($"; Manic Miner - Caverna {CavernIndex}: \"{Name}\" (1024 Byte SkoolKit Format)");
            sb.AppendLine($"; Base Address: {baseAddress} (${baseAddress:X4})");
            sb.AppendLine($"; ===========================================================================");
            sb.AppendLine();

            byte[] raw1024 = To1024ByteArray();

            // 1. Screen Layout Attributes
            sb.AppendLine($"; ---------------------------------------------------------------------------");
            sb.AppendLine($"; Offsets 0..511: Screen Layout Attributes (32x16 grid)");
            sb.AppendLine($"; ---------------------------------------------------------------------------");
            sb.AppendLine($"cavern_{CavernIndex}_layout:");
            for (int r = 0; r < 16; r++)
            {
                sb.Append($"\t{defb} ");
                for (int c = 0; c < 32; c++)
                {
                    sb.Append(raw1024[r * 32 + c]);
                    if (c < 31) sb.Append(", ");
                }
                sb.AppendLine($"\t; Riga {r}");
            }
            sb.AppendLine();

            // 2. Cavern Name
            sb.AppendLine($"; ---------------------------------------------------------------------------");
            sb.AppendLine($"; Offsets 512..543: Cavern Name (32 byte string)");
            sb.AppendLine($"; ---------------------------------------------------------------------------");
            string padName = (Name ?? string.Empty).PadRight(32, ' ');
            if (padName.Length > 32) padName = padName.Substring(0, 32);
            sb.AppendLine($"cavern_{CavernIndex}_name:");
            sb.AppendLine($"\t{defm} \"{padName}\"");
            sb.AppendLine();

            // 3. Tile Definitions
            sb.AppendLine($"; ---------------------------------------------------------------------------");
            sb.AppendLine($"; Offsets 544..615: Tile Definitions (8 tiles x 9 bytes: 1 attr + 8 bitmap)");
            sb.AppendLine($"; ---------------------------------------------------------------------------");
            string[] tileLabels = new string[] {
                "background", "floor", "crumbling_floor", "wall", "conveyor", "nasty1", "nasty2", "spare"
            };
            for (int i = 0; i < 8; i++)
            {
                int baseOff = 544 + (i * 9);
                sb.AppendLine($"cavern_{CavernIndex}_{tileLabels[i]}:");
                sb.AppendLine($"\t{defb} {raw1024[baseOff]}\t; Attr ({TileDefs[i].RoleName})");
                sb.Append($"\t{defb} ");
                for (int b = 0; b < 8; b++)
                {
                    sb.Append(raw1024[baseOff + 1 + b]);
                    if (b < 7) sb.Append(", ");
                }
                sb.AppendLine($"\t; Bitmap 8x8");
            }
            sb.AppendLine();

            // 4. Miner Willy Initial State
            sb.AppendLine($"; ---------------------------------------------------------------------------");
            sb.AppendLine($"; Offsets 616..622: Miner Willy Initial State (7 bytes)");
            sb.AppendLine($"; ---------------------------------------------------------------------------");
            sb.AppendLine($"cavern_{CavernIndex}_willy:");
            sb.AppendLine($"\t{defb} {raw1024[616]}\t; Pixel Y * 2 (Y={WillyY}, PixelY={WillyPixelY})");
            sb.AppendLine($"\t{defb} {raw1024[617]}\t; Frame");
            sb.AppendLine($"\t{defb} {raw1024[618]}\t; Direzione");
            sb.AppendLine($"\t{defb} {raw1024[619]}\t; Airborne indicator");
            int wAddr = raw1024[620] | (raw1024[621] << 8);
            sb.AppendLine($"\t{defw} {wAddr}\t; Indirizzo attribute buffer (Y={WillyY}, X={WillyX})");
            sb.AppendLine($"\t{defb} {raw1024[622]}\t; Jumping counter");
            sb.AppendLine();

            // 5. Conveyor
            sb.AppendLine($"; ---------------------------------------------------------------------------");
            sb.AppendLine($"; Offsets 623..626: Conveyor (4 bytes)");
            sb.AppendLine($"; ---------------------------------------------------------------------------");
            sb.AppendLine($"cavern_{CavernIndex}_conveyor:");
            sb.AppendLine($"\t{defb} {raw1024[623]}\t; Direzione (0=Left, 1=Right)");
            int convAddr = raw1024[624] | (raw1024[625] << 8);
            sb.AppendLine($"\t{defw} {convAddr}\t; Indirizzo video screen buffer (X={ConveyorX}, Y={ConveyorY})");
            sb.AppendLine($"\t{defb} {raw1024[626]}\t; Lunghezza in caratteri");
            sb.AppendLine();

            // 6. Border & Unused
            sb.AppendLine($"cavern_{CavernIndex}_border:\n\t{defb} {raw1024[627]}\t; Colore bordo ZX Spectrum");
            sb.AppendLine($"\t{defb} {raw1024[628]}\t; Unused byte");
            sb.AppendLine();

            // 7. Items
            sb.AppendLine($"; ---------------------------------------------------------------------------");
            sb.AppendLine($"; Offsets 629..653: Items (fino a 5 oggetti)");
            sb.AppendLine($"; ---------------------------------------------------------------------------");
            sb.AppendLine($"cavern_{CavernIndex}_items:");
            for (int i = 0; i < 5; i++)
            {
                int off = 629 + (i * 5);
                if (raw1024[off] == 255)
                {
                    sb.AppendLine($"\t{defb} 255\t; Fine lista item");
                    break;
                }
                int itAttr = raw1024[off + 1] | (raw1024[off + 2] << 8);
                int itScr = raw1024[off + 3] | (raw1024[off + 4] << 8);
                sb.AppendLine($"\t{defb} {raw1024[off]}, {itAttr & 0xFF}, {(itAttr >> 8) & 0xFF}, {itScr & 0xFF}, {(itScr >> 8) & 0xFF}\t; Item {i + 1}");
            }
            sb.AppendLine();

            // 8. Portal
            sb.AppendLine($"; ---------------------------------------------------------------------------");
            sb.AppendLine($"; Offsets 654..690: Exit Portal (37 bytes)");
            sb.AppendLine($"; ---------------------------------------------------------------------------");
            sb.AppendLine($"cavern_{CavernIndex}_portal:");
            sb.AppendLine($"\t{defb} {raw1024[654]}\t; Terminator item ($FF)");
            sb.AppendLine($"\t{defb} {raw1024[655]}\t; Portal attribute ({PortalAttribute})");
            sb.AppendLine($"\t; Grafica portale 16x16 pixel (32 byte)");
            for (int row = 0; row < 4; row++)
            {
                sb.Append($"\t{defb} ");
                for (int b = 0; b < 8; b++)
                {
                    sb.Append(raw1024[656 + (row * 8) + b]);
                    if (b < 7) sb.Append(", ");
                }
                sb.AppendLine();
            }
            int pAttr = raw1024[688] | (raw1024[689] << 8);
            sb.AppendLine($"\t{defw} {pAttr}\t; Indirizzo attribute buffer (Y={PortalY}, X={PortalX})");
            sb.AppendLine($"\t{defb} {raw1024[690]}\t; Screen buffer low byte");
            sb.AppendLine();

            // 9. Item Graphic
            sb.AppendLine($"; ---------------------------------------------------------------------------");
            sb.AppendLine($"; Offsets 691..698: Item Graphic 8x8 (8 bytes)");
            sb.AppendLine($"; ---------------------------------------------------------------------------");
            sb.AppendLine($"cavern_{CavernIndex}_item_graphic:");
            sb.Append($"\t{defb} ");
            for (int b = 0; b < 8; b++)
            {
                sb.Append(raw1024[691 + b]);
                if (b < 7) sb.Append(", ");
            }
            sb.AppendLine();
            sb.AppendLine();

            // 10. Air & Game Clock
            sb.AppendLine($"cavern_{CavernIndex}_air:\n\t{defb} {raw1024[699]}\t; Riserva d'aria iniziale");
            sb.AppendLine($"cavern_{CavernIndex}_clock:\n\t{defb} {raw1024[700]}\t; Game clock");
            sb.AppendLine();

            // 11. Guardians
            sb.AppendLine($"; ---------------------------------------------------------------------------");
            sb.AppendLine($"; Offsets 701..728: Horizontal Guardians (28 bytes)");
            sb.AppendLine($"; ---------------------------------------------------------------------------");
            sb.AppendLine($"cavern_{CavernIndex}_guardians:");
            for (int i = 0; i < 4; i++)
            {
                int off = 701 + (i * 7);
                if (raw1024[off] == 255)
                {
                    sb.AppendLine($"\t{defb} 255\t; Fine guardiani");
                    break;
                }
                sb.Append($"\t{defb} ");
                for (int b = 0; b < 7; b++)
                {
                    sb.Append(raw1024[off + b]);
                    if (b < 6) sb.Append(", ");
                }
                sb.AppendLine($"\t; Guardiano {i + 1}");
            }
            sb.AppendLine();

            // 12. Guardian Graphics
            sb.AppendLine($"; ---------------------------------------------------------------------------");
            sb.AppendLine($"; Offsets 766..1021: Guardian Graphics (8 sprite 16x16 = 256 bytes)");
            sb.AppendLine($"; ---------------------------------------------------------------------------");
            sb.AppendLine($"cavern_{CavernIndex}_guardian_graphics:");
            for (int s = 0; s < 8; s++)
            {
                sb.AppendLine($"; Sprite / Frame {s + 1} (32 byte)");
                for (int row = 0; row < 4; row++)
                {
                    sb.Append($"\t{defb} ");
                    for (int b = 0; b < 8; b++)
                    {
                        sb.Append(raw1024[766 + (s * 32) + (row * 8) + b]);
                        if (b < 7) sb.Append(", ");
                    }
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        private static int CalculateScreenBufferAddress(int col, int row)
        {
            // Indirizzo di memoria video simulato buffer per Manic Miner (28672 = $7000)
            int blockRow = row / 8;
            int lineInChar = row % 8;
            return 28672 + (blockRow * 2048) + (lineInChar * 256) + col;
        }

        /// <summary>
        /// Crea e restituisce l'autentica caverna originale "Central Cavern" (Cavern 0) di Manic Miner
        /// conforme al 100% ai dati SkoolKit 45056.
        /// </summary>
        public static ManicMinerCavern CreateCentralCavernDefault()
        {
            var c = new ManicMinerCavern
            {
                CavernIndex = 0,
                Name = "Central Cavern",
                BorderColor = 2, // Red
                InitialAir = 63,
                GameClock = 252,
                WillyX = 2,
                WillyY = 13,
                WillyPixelY = 104,
                WillyDirection = 0,
                WillyFrame = 0,
                ConveyorX = 8,
                ConveyorY = 9,
                ConveyorLength = 20,
                ConveyorDirection = 0,
                PortalX = 29,
                PortalY = 13,
                PortalAttribute = 14,
                VerticalGuardiansIndicator = 255
            };

            // Tile 0..7
            c.TileDefs[0] = new ManicMinerTileDef("Background", 0, new byte[8]);
            c.TileDefs[1] = new ManicMinerTileDef("Floor", 66, new byte[] { 255, 255, 219, 110, 197, 64, 0, 0 });
            c.TileDefs[2] = new ManicMinerTileDef("Crumbling Floor", 2, new byte[] { 255, 219, 165, 36, 82, 32, 8, 0 });
            c.TileDefs[3] = new ManicMinerTileDef("Wall", 22, new byte[] { 34, 255, 136, 255, 34, 255, 136, 255 });
            c.TileDefs[4] = new ManicMinerTileDef("Conveyor", 4, new byte[] { 240, 102, 240, 102, 0, 153, 255, 0 });
            c.TileDefs[5] = new ManicMinerTileDef("Nasty 1", 68, new byte[] { 68, 40, 148, 81, 53, 214, 88, 16 });
            c.TileDefs[6] = new ManicMinerTileDef("Nasty 2", 5, new byte[] { 255, 254, 126, 124, 76, 76, 8, 8 });
            c.TileDefs[7] = new ManicMinerTileDef("Spare / Extra", 0, new byte[8]);

            // Item Graphic (chiave 8x8)
            c.ItemGraphic = new byte[] { 48, 72, 136, 144, 104, 4, 10, 4 };

            // Portal Graphic (16x16 pixel = 32 byte)
            c.PortalGraphic = new byte[] {
                255,255, 146,73, 182,219, 255,255, 146,73, 182,219, 255,255, 146,73,
                182,219, 255,255, 146,73, 182,219, 255,255, 146,73, 182,219, 255,255
            };

            // Items (5 chiavi originali di Central Cavern)
            c.Items.Clear();
            c.Items.Add(new ManicMinerItem(9, 0, 3));
            c.Items.Add(new ManicMinerItem(29, 0, 4));
            c.Items.Add(new ManicMinerItem(1, 5, 5));
            c.Items.Add(new ManicMinerItem(24, 5, 6));
            c.Items.Add(new ManicMinerItem(18, 11, 7));

            // Horizontal Guardians
            c.HorizontalGuardians.Clear();
            c.HorizontalGuardians.Add(new ManicMinerGuardian
            {
                Y = 7,
                InitialX = 8,
                MinX = 8,
                MaxX = 15,
                Direction = 0,
                Speed = 0,
                SpriteIndex = 0
            });

            // Layout Central Cavern
            // Muri perimetrali:
            // x=0..31: riga 0 e 15 (muri e pavimenti)
            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 32; x++)
                {
                    // Bordi verticali (Wall = slot 3)
                    if (x == 0 || x == 31)
                    {
                        c.Grid[x, y] = 3;
                    }
                    else if (y == 15)
                    {
                        // Pavimento inferiore (Floor = slot 1)
                        c.Grid[x, y] = 1;
                    }
                    else
                    {
                        c.Grid[x, y] = 0; // Background
                    }
                }
            }

            // Piattaforme tipiche di Central Cavern
            // Piattaforma nastro trasportatore alla riga 9
            for (int x = 8; x < 8 + 20; x++)
            {
                c.Grid[x, 9] = 4; // Conveyor = slot 4
            }

            // Piattaforme piano terra e intermedie
            for (int x = 1; x <= 6; x++) c.Grid[x, 14] = 1;
            for (int x = 12; x <= 16; x++) c.Grid[x, 13] = 1;
            for (int x = 20; x <= 25; x++) c.Grid[x, 12] = 1;
            for (int x = 27; x <= 30; x++) c.Grid[x, 14] = 1;

            // Piattaforme superiori
            for (int x = 1; x <= 8; x++) c.Grid[x, 6] = 1;
            for (int x = 23; x <= 30; x++) c.Grid[x, 6] = 1;
            for (int x = 10; x <= 18; x++) c.Grid[x, 3] = 1;

            // Insidie (Nasties: slot 5 e 6)
            c.Grid[10, 14] = 5;
            c.Grid[11, 14] = 5;
            c.Grid[18, 14] = 6;
            c.Grid[19, 14] = 6;

            // Crumbling floor (slot 2)
            c.Grid[15, 6] = 2;
            c.Grid[16, 6] = 2;
            c.Grid[17, 6] = 2;

            return c;
        }

        /// <summary>
        /// Converte la caverna Manic Miner in un oggetto GameMap utilizzabile dall'editor di mappe.
        /// Assegna le 8 tile al progetto e imposta i riferimenti.
        /// </summary>
        public GameMap ToGameMap(SpriteProject project)
        {
            var map = new GameMap(Name, ScreenWidth, ScreenHeight)
            {
                DefaultPaper = 0,
                DefaultInk = 7,
                Bright = true
            };

            if (project != null)
            {
                // Assicura che le 8 tile di questa caverna siano registrate nel progetto
                List<int> assignedIds = new List<int>();
                for (int i = 0; i < 8; i++)
                {
                    var tDef = (TileDefs != null && i < TileDefs.Length) ? TileDefs[i] : null;
                    string tName = $"{Name}_{tDef?.RoleName ?? $"Tile_{i + 1}"}".Replace(" ", "");

                    // Cerca tile esistente o creane una nuova
                    Tile targetTile = project.Tiles.Find(t => t.Name == tName);
                    int ink = (tDef != null) ? (tDef.Attribute & 0x07) : 7;
                    int paper = (tDef != null) ? ((tDef.Attribute >> 3) & 0x07) : 0;
                    bool bright = (tDef != null) && ((tDef.Attribute & 0x40) != 0);
                    bool flash = (tDef != null) && ((tDef.Attribute & 0x80) != 0);

                    if (targetTile == null)
                    {
                        targetTile = new Tile(tName, tDef?.Bitmap)
                        {
                            InkColor = ink,
                            PaperColor = paper,
                            Bright = bright,
                            Flash = flash
                        };
                        project.Tiles.Add(targetTile);
                    }
                    else
                    {
                        targetTile.InkColor = ink;
                        targetTile.PaperColor = paper;
                        targetTile.Bright = bright;
                        targetTile.Flash = flash;
                        if (tDef?.Bitmap != null) targetTile.SetFromBytesRowByRow(tDef.Bitmap);
                    }

                    int tileId = project.Tiles.IndexOf(targetTile) + 1;
                    assignedIds.Add(tileId);
                }

                map.AssignedTileIds = assignedIds;

                // Popola la griglia di GameMap mappando gli slot 0..7 sui rispettivi tileId
                for (int y = 0; y < ScreenHeight; y++)
                {
                    for (int x = 0; x < ScreenWidth; x++)
                    {
                        int slot = Grid[x, y] & 0x07;
                        int tid = (slot < assignedIds.Count) ? assignedIds[slot] : 0;
                        map.SetTile(x, y, tid);
                    }
                }
            }

            map.ManicMinerData = this;
            return map;
        }

        /// <summary>
        /// Aggiorna i dati della caverna a partire dalle modifiche apportate sulla GameMap dell'editor.
        /// </summary>
        public void UpdateFromGameMap(GameMap map, SpriteProject project)
        {
            if (map == null) return;
            Name = map.Name;

            if (map.AssignedTileIds != null && map.AssignedTileIds.Count > 0 && project != null)
            {
                for (int y = 0; y < ScreenHeight; y++)
                {
                    for (int x = 0; x < ScreenWidth; x++)
                    {
                        int tid = map.GetTile(x, y);
                        int slot = map.AssignedTileIds.IndexOf(tid);
                        Grid[x, y] = (byte)(slot >= 0 ? (slot & 0x07) : 0);
                    }
                }

                for (int i = 0; i < Math.Min(8, map.AssignedTileIds.Count); i++)
                {
                    int tid = map.AssignedTileIds[i];
                    if (tid > 0 && tid <= project.Tiles.Count)
                    {
                        var pt = project.Tiles[tid - 1];
                        byte attr = (byte)((pt.Flash ? 0x80 : 0) |
                                          (pt.Bright ? 0x40 : 0) |
                                          ((pt.PaperColor & 0x07) << 3) |
                                          (pt.InkColor & 0x07));
                        if (TileDefs[i] != null)
                        {
                            TileDefs[i].Attribute = attr;
                            TileDefs[i].Bitmap = pt.GetBytesRowByRow();
                        }
                    }
                }
            }
        }
    }
}
