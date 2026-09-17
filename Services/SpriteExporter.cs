using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using ZXSpectrumSpriteGenerator.Models;

namespace ZXSpectrumSpriteGenerator.Services
{
    public enum NumberFormat
    {
        Decimal,
        HexPrefixDollar,    // $FF
        HexPrefix0x,        // 0xFF
        BinaryPercent       // %11110000
    }

    public enum AsmDirective
    {
        Db,         // db
        Defb,       // defb
        DefbUpper,  // DEFB
        Byte        // .byte
    }

    public enum MaskMode
    {
        None,
        InterleavedSpriteMask,  // sprite, mask, sprite, mask...
        InterleavedMaskSprite,  // mask, sprite, mask, sprite...
        Sequential              // all sprite bytes, then all mask bytes
    }

    public class ExportOptions
    {
        public NumberFormat NumberFormat { get; set; } = NumberFormat.Decimal;
        public AsmDirective Directive { get; set; } = AsmDirective.Db;
        public MaskMode MaskMode { get; set; } = MaskMode.None;
        public bool MaskForAnd { get; set; } = true; // true = 0 for sprite, 1 for bg; false = 1 for sprite, 0 for bg
        public int BytesPerLine { get; set; } = 16;
        public bool IncludeLabels { get; set; } = true;
        public bool IncludeComments { get; set; } = true;
        public bool ExportAllSprites { get; set; } = true;
    }

    /// <summary>
    /// Servizio per esportazione di sprite in vari formati (Assembly Z80, C/Z88DK, BASIC, Binario, Immagini).
    /// </summary>
    public static class SpriteExporter
    {
        public static string FormatByte(byte value, NumberFormat format)
        {
            switch (format)
            {
                case NumberFormat.Decimal:
                    return value.ToString();
                case NumberFormat.HexPrefixDollar:
                    return "$" + value.ToString("X2");
                case NumberFormat.HexPrefix0x:
                    return "0x" + value.ToString("X2");
                case NumberFormat.BinaryPercent:
                    return "%" + Convert.ToString(value, 2).PadLeft(8, '0');
                default:
                    return value.ToString();
            }
        }

        public static string GetDirectiveString(AsmDirective dir)
        {
            switch (dir)
            {
                case AsmDirective.Db: return "db";
                case AsmDirective.Defb: return "defb";
                case AsmDirective.DefbUpper: return "DEFB";
                case AsmDirective.Byte: return ".byte";
                default: return "db";
            }
        }

        /// <summary>
        /// Genera codice Assembly Z80 compatibile con Pasmo, SJASMPlus, Z80ASM, ecc.
        /// </summary>
        public static string ExportToAssembly(SpriteProject project, ExportOptions options)
        {
            StringBuilder sb = new StringBuilder();
            string dir = GetDirectiveString(options.Directive);

            var spritesToExport = options.ExportAllSprites ? project.Sprites : new List<Sprite> { project.CurrentSprite };

            if (options.IncludeComments)
            {
                sb.AppendLine("; ==========================================================");
                sb.AppendLine($"; ZX Spectrum Sprite Data - {project.Title}");
                sb.AppendLine($"; Totale sprite: {spritesToExport.Count}, Dimensione: {project.DefaultWidth}x{project.DefaultHeight} pixel");
                sb.AppendLine($"; Generato con ZX Spectrum Sprite Generator");
                sb.AppendLine("; ==========================================================");
                sb.AppendLine();
            }

            for (int sIndex = 0; sIndex < spritesToExport.Count; sIndex++)
            {
                var sprite = spritesToExport[sIndex];
                if (sprite == null) continue;

                if (options.IncludeLabels)
                {
                    string label = string.IsNullOrWhiteSpace(sprite.Name) ? $"Sprite_{sIndex}" : sprite.Name.Replace(" ", "_");
                    sb.AppendLine($"{label}:");
                }

                if (options.IncludeComments)
                {
                    sb.AppendLine($"; Sprite {sIndex} ({sprite.Width}x{sprite.Height} px, {sprite.Width / 8 * sprite.Height} bytes)");
                }

                byte[] spriteBytes = sprite.GetBytesRowByRow();
                byte[] maskBytes = options.MaskMode != MaskMode.None ? sprite.GenerateMaskBytes(options.MaskForAnd) : null;

                List<byte> finalBytes = new List<byte>();

                if (options.MaskMode == MaskMode.None)
                {
                    finalBytes.AddRange(spriteBytes);
                }
                else if (options.MaskMode == MaskMode.InterleavedSpriteMask)
                {
                    for (int i = 0; i < spriteBytes.Length; i++)
                    {
                        finalBytes.Add(spriteBytes[i]);
                        finalBytes.Add(maskBytes[i]);
                    }
                }
                else if (options.MaskMode == MaskMode.InterleavedMaskSprite)
                {
                    for (int i = 0; i < spriteBytes.Length; i++)
                    {
                        finalBytes.Add(maskBytes[i]);
                        finalBytes.Add(spriteBytes[i]);
                    }
                }
                else if (options.MaskMode == MaskMode.Sequential)
                {
                    finalBytes.AddRange(spriteBytes);
                    finalBytes.AddRange(maskBytes);
                }

                int bpl = Math.Max(1, options.BytesPerLine);
                for (int i = 0; i < finalBytes.Count; i += bpl)
                {
                    sb.Append("    ").Append(dir).Append(" ");
                    int count = Math.Min(bpl, finalBytes.Count - i);
                    for (int j = 0; j < count; j++)
                    {
                        sb.Append(FormatByte(finalBytes[i + j], options.NumberFormat));
                        if (j < count - 1)
                        {
                            sb.Append(",");
                        }
                    }
                    sb.AppendLine();
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// Genera un array C/C++ compatibile con Z88DK / SDCC.
        /// </summary>
        public static string ExportToC(SpriteProject project, bool exportAll = true)
        {
            StringBuilder sb = new StringBuilder();
            var spritesToExport = exportAll ? project.Sprites : new List<Sprite> { project.CurrentSprite };

            sb.AppendLine("// ZX Spectrum Sprite Data - Z88DK / C format");
            sb.AppendLine("#include <stdint.h>");
            sb.AppendLine();

            for (int sIndex = 0; sIndex < spritesToExport.Count; sIndex++)
            {
                var sprite = spritesToExport[sIndex];
                if (sprite == null) continue;

                string label = string.IsNullOrWhiteSpace(sprite.Name) ? $"sprite_{sIndex}" : sprite.Name.ToLower().Replace(" ", "_");
                byte[] bytes = sprite.GetBytesRowByRow();

                sb.AppendLine($"// Frame {sIndex}: {sprite.Width}x{sprite.Height}");
                sb.AppendLine($"const uint8_t {label}[{bytes.Length}] = {{");

                int bytesPerRow = sprite.Width / 8;
                for (int y = 0; y < sprite.Height; y++)
                {
                    sb.Append("    ");
                    for (int b = 0; b < bytesPerRow; b++)
                    {
                        int idx = y * bytesPerRow + b;
                        sb.Append("0x").Append(bytes[idx].ToString("X2"));
                        if (idx < bytes.Length - 1)
                            sb.Append(", ");
                    }
                    sb.AppendLine();
                }

                sb.AppendLine("};");
                sb.AppendLine();
            }

            // Tabella puntatori a tutti i frame
            if (spritesToExport.Count > 1)
            {
                sb.AppendLine($"const uint8_t* const sprite_frames[{spritesToExport.Count}] = {{");
                for (int sIndex = 0; sIndex < spritesToExport.Count; sIndex++)
                {
                    var sprite = spritesToExport[sIndex];
                    string label = string.IsNullOrWhiteSpace(sprite.Name) ? $"sprite_{sIndex}" : sprite.Name.ToLower().Replace(" ", "_");
                    sb.Append($"    {label}");
                    if (sIndex < spritesToExport.Count - 1) sb.Append(",");
                    sb.AppendLine();
                }
                sb.AppendLine("};");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Genera linee DATA per Sinclair BASIC con un loader di esempio.
        /// </summary>
        public static string ExportToBasic(SpriteProject project, int startLine = 1000, int lineStep = 10)
        {
            StringBuilder sb = new StringBuilder();
            int currentLine = startLine;

            var spritesToExport = project.Sprites;
            int totalBytes = 0;

            foreach (var s in spritesToExport)
            {
                totalBytes += (s.Width / 8) * s.Height;
            }

            sb.AppendLine($"10 REM ZX Spectrum Sprite BASIC Loader");
            sb.AppendLine($"20 CLEAR 59999");
            sb.AppendLine($"30 LET ADDR = 60000");
            sb.AppendLine($"40 FOR I = 0 TO {totalBytes - 1}");
            sb.AppendLine($"50 READ B: POKE ADDR + I, B");
            sb.AppendLine($"60 NEXT I");
            sb.AppendLine($"70 PRINT \"Caricati {totalBytes} byte di sprite.\"");
            sb.AppendLine($"80 STOP");
            sb.AppendLine();

            foreach (var sprite in spritesToExport)
            {
                sb.AppendLine($"{currentLine} REM Sprite: {sprite.Name}");
                currentLine += lineStep;

                byte[] bytes = sprite.GetBytesRowByRow();
                for (int i = 0; i < bytes.Length; i += 8)
                {
                    sb.Append($"{currentLine} DATA ");
                    int count = Math.Min(8, bytes.Length - i);
                    for (int j = 0; j < count; j++)
                    {
                        sb.Append(bytes[i + j]);
                        if (j < count - 1) sb.Append(",");
                    }
                    sb.AppendLine();
                    currentLine += lineStep;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Esporta i dati binari grezzi in un file.
        /// </summary>
        public static void ExportToBinaryFile(string filePath, SpriteProject project, bool exportAll = true)
        {
            var spritesToExport = exportAll ? project.Sprites : new List<Sprite> { project.CurrentSprite };
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                foreach (var sprite in spritesToExport)
                {
                    byte[] bytes = sprite.GetBytesRowByRow();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
        }

        /// <summary>
        /// Esporta uno spritesheet orizzontale in formato PNG.
        /// </summary>
        public static void ExportSpritesheetPng(string filePath, SpriteProject project, int scale = 2)
        {
            if (project.Sprites.Count == 0) return;

            int singleW = project.DefaultWidth * scale;
            int singleH = project.DefaultHeight * scale;
            int totalW = singleW * project.Sprites.Count;

            using (Bitmap sheet = new Bitmap(totalW, singleH))
            {
                using (Graphics g = Graphics.FromImage(sheet))
                {
                    for (int i = 0; i < project.Sprites.Count; i++)
                    {
                        using (Bitmap spriteBmp = project.Sprites[i].RenderToBitmap(scale))
                        {
                            g.DrawImage(spriteBmp, i * singleW, 0);
                        }
                    }
                }
                sheet.Save(filePath, ImageFormat.Png);
            }
        }
    }
}
