using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using ZXSpectrumSpriteGenerator.Models;

namespace ZXSpectrumSpriteGenerator.Services
{
    /// <summary>
    /// Modalità di compressione per l'esportazione della mappa in Assembly Z80.
    /// </summary>
    public enum MapCompressionMode
    {
        Uncompressed,   // 1:1 - 1 byte per tile
        Packed2Bit,     // Compresso - 4 tile per byte (2 bit per tile, riga per riga)
        Both            // Esporta sia il formato 1:1 sia il formato compresso
    }

    /// <summary>
    /// Servizio per esportare le mappe di gioco in vari formati:
    /// Assembly Z80 (defb/db), C/Z88DK, Sinclair BASIC (DATA), file binario (.bin) e immagine (.png).
    /// </summary>
    public static class MapExporter
    {
        public static string ExportToAssembly(GameMap map, AsmDirective directive = AsmDirective.Defb, bool exportAllMaps = false, SpriteProject project = null, MapCompressionMode compressionMode = MapCompressionMode.Both, NumberFormat numberFormat = NumberFormat.BinaryPercent)
        {
            if (map == null && project == null) return string.Empty;

            StringBuilder sb = new StringBuilder();
            string dirStr = (directive == AsmDirective.Db) ? "db" : "defb";

            sb.AppendLine("; ===========================================================");
            sb.AppendLine("; SINCLAIR ZX SPECTRUM - ESPORTAZIONE MAPPE DI GIOCO (TILEMAP)");
            sb.AppendLine("; Generato da ZX Spectrum Sprite Generator & Studio");
            if (compressionMode == MapCompressionMode.Packed2Bit)
            {
                sb.AppendLine("; Formato COMPRESSO: 4 tile per byte (2 bit per tile, indici 0..3)");
            }
            else if (compressionMode == MapCompressionMode.Both)
            {
                sb.AppendLine("; Formato STANDARD (1 byte/tile) + COMPRESSO (4 tile/byte, 2-bit/tile)");
            }
            else
            {
                sb.AppendLine("; Tile 0 = Sfondo/Vuoto, Tile 1..N = Indice Tile (8x8)");
            }
            sb.AppendLine("; ===========================================================");
            sb.AppendLine();

            if (exportAllMaps && project != null)
            {
                for (int m = 0; m < project.Maps.Count; m++)
                {
                    ExportSingleMapAsm(sb, project.Maps[m], dirStr, compressionMode, numberFormat);
                    sb.AppendLine();
                }
            }
            else if (map != null)
            {
                ExportSingleMapAsm(sb, map, dirStr, compressionMode, numberFormat);
            }

            // Esportazione dei dati grafici 8x8 dei Tile (Tileset)
            if (project != null && project.Tiles.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("; ===========================================================");
                sb.AppendLine("; TILESET (8x8 PIXEL) - DEFINIZIONI GRAFICHE DEI TILE");
                sb.AppendLine($"; Totale tile: {project.Tiles.Count}" + ((compressionMode == MapCompressionMode.Packed2Bit && project.Tiles.Count > 3) ? " (nota: indici 2-bit supportano tile 1..3 oltre a vuoto)" : ""));
                sb.AppendLine("; ===========================================================");
                for (int t = 0; t < project.Tiles.Count; t++)
                {
                    var tile = project.Tiles[t];
                    string tName = SanitizeIdentifier(tile.Name);
                    sb.AppendLine($"; Tile {t + 1}: {tile.Name} (Ink: {tile.InkColor}, Paper: {tile.PaperColor}, Bright: {(tile.Bright ? "1" : "0")}, Flash: {(tile.Flash ? "1" : "0")})");
                    sb.AppendLine($"tile_{t + 1}_{tName}:");
                    byte attr = tile.GetAttributeByte();
                    var hexData = tile.GetBytesRowByRow();
                    sb.Append($"    {dirStr} {attr}, ");
                    for (int b = 0; b < hexData.Length; b++)
                    {
                        sb.Append(hexData[b]);
                        if (b < hexData.Length - 1) sb.Append(", ");
                    }
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        public static string ExportToAssemblyPacked(GameMap map, AsmDirective directive = AsmDirective.Defb, bool exportAllMaps = false, SpriteProject project = null, NumberFormat numberFormat = NumberFormat.BinaryPercent)
        {
            return ExportToAssembly(map, directive, exportAllMaps, project, MapCompressionMode.Packed2Bit, numberFormat);
        }

        public static void ExportSingleMapAsm(StringBuilder sb, GameMap map, string dirStr, MapCompressionMode mode = MapCompressionMode.Both, NumberFormat numberFormat = NumberFormat.BinaryPercent)
        {
            if (map == null) return;

            if (mode == MapCompressionMode.Uncompressed || mode == MapCompressionMode.Both)
            {
                ExportSingleMapAsmStandard(sb, map, dirStr);
            }

            if (mode == MapCompressionMode.Both)
            {
                sb.AppendLine();
            }

            if (mode == MapCompressionMode.Packed2Bit || mode == MapCompressionMode.Both)
            {
                ExportSingleMapAsmPacked(sb, map, dirStr, numberFormat);
            }
        }

        public static void ExportSingleMapAsmStandard(StringBuilder sb, GameMap map, string dirStr)
        {
            if (map == null) return;

            string cleanName = SanitizeIdentifier(map.Name);
            int totalBytes = map.Width * map.Height;

            sb.AppendLine($"; --- Mappa: {map.Name} [Formato Standard 1:1, 1 byte/tile] ---");
            sb.AppendLine($"; Dimensioni: {map.Width}x{map.Height} celle ({map.Width * map.TileWidth}x{map.Height * map.TileHeight} px), {totalBytes} byte");
            sb.AppendLine($"{cleanName}_width:  {dirStr} {map.Width}");
            sb.AppendLine($"{cleanName}_height: {dirStr} {map.Height}");
            sb.AppendLine($"{cleanName}_data:");

            for (int y = 0; y < map.Height; y++)
            {
                sb.Append($"    {dirStr} ");
                for (int x = 0; x < map.Width; x++)
                {
                    sb.Append(map.GetTile(x, y));
                    if (x < map.Width - 1) sb.Append(",");
                }
                sb.AppendLine($" ; Riga {y}");
            }
        }

        public static void ExportSingleMapAsmPacked(StringBuilder sb, GameMap map, string dirStr, NumberFormat numberFormat = NumberFormat.BinaryPercent)
        {
            if (map == null) return;

            string cleanName = SanitizeIdentifier(map.Name);
            int bytesPerRow = (map.Width + 3) / 4;
            int totalPackedBytes = bytesPerRow * map.Height;

            bool hasOverflow = false;
            for (int y = 0; y < map.Height && !hasOverflow; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    if (map.GetTile(x, y) > 3)
                    {
                        hasOverflow = true;
                        break;
                    }
                }
            }

            sb.AppendLine($"; --- Mappa: {map.Name} [Formato Compresso 2-bit, 4 tile/byte] ---");
            sb.AppendLine($"; Dimensioni: {map.Width}x{map.Height} celle ({map.Width * map.TileWidth}x{map.Height * map.TileHeight} px)");
            sb.AppendLine($"; Memoria: {totalPackedBytes} byte ({bytesPerRow} byte/riga, 4 tile per byte riga per riga)");
            sb.AppendLine($"; Struttura byte (MSB -> LSB): [Bit 7-6: Tile x+0] [Bit 5-4: Tile x+1] [Bit 3-2: Tile x+2] [Bit 1-0: Tile x+3]");
            sb.AppendLine($"; Indici tile (2-bit): 0..3 (00=Vuoto/Sfondo, 01=Tile 1, 10=Tile 2, 11=Tile 3)");
            if (hasOverflow)
            {
                sb.AppendLine("; ATTENZIONE: La mappa contiene indici tile >= 4. I valori sono stati mascherati a 2 bit (& 3).");
            }
            sb.AppendLine($"{cleanName}_packed_width:         {dirStr} {map.Width}");
            sb.AppendLine($"{cleanName}_packed_height:        {dirStr} {map.Height}");
            sb.AppendLine($"{cleanName}_packed_bytes_per_row: {dirStr} {bytesPerRow}");
            sb.AppendLine($"{cleanName}_packed_data:");

            for (int y = 0; y < map.Height; y++)
            {
                sb.Append($"    {dirStr} ");
                for (int b = 0; b < bytesPerRow; b++)
                {
                    int startX = b * 4;
                    int t0 = (startX < map.Width) ? (map.GetTile(startX, y) & 0x03) : 0;
                    int t1 = (startX + 1 < map.Width) ? (map.GetTile(startX + 1, y) & 0x03) : 0;
                    int t2 = (startX + 2 < map.Width) ? (map.GetTile(startX + 2, y) & 0x03) : 0;
                    int t3 = (startX + 3 < map.Width) ? (map.GetTile(startX + 3, y) & 0x03) : 0;

                    byte packedByte = (byte)((t0 << 6) | (t1 << 4) | (t2 << 2) | t3);
                    sb.Append(FormatPackedByte(packedByte, numberFormat));

                    if (b < bytesPerRow - 1)
                    {
                        sb.Append(", ");
                    }
                }
                sb.AppendLine($" ; Riga {y}");
            }
        }

        private static string FormatPackedByte(byte value, NumberFormat format)
        {
            switch (format)
            {
                case NumberFormat.HexPrefixDollar:
                    return "$" + value.ToString("X2");
                case NumberFormat.HexPrefix0x:
                    return "0x" + value.ToString("X2");
                case NumberFormat.Decimal:
                    return value.ToString();
                case NumberFormat.BinaryPercent:
                default:
                    return "%" + Convert.ToString(value, 2).PadLeft(8, '0');
            }
        }

        public static string ExportToC(GameMap map, bool exportAllMaps = false, SpriteProject project = null, MapCompressionMode compressionMode = MapCompressionMode.Uncompressed)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("// ===========================================================");
            sb.AppendLine("// SINCLAIR ZX SPECTRUM / Z88DK - MAPPE DI GIOCO");
            if (compressionMode == MapCompressionMode.Packed2Bit)
                sb.AppendLine("// FORMATO COMPRESSO: 2 bit per tile (4 tile per byte riga per riga)");
            else if (compressionMode == MapCompressionMode.Both)
                sb.AppendLine("// FORMATO STANDARD (1 byte/tile) + COMPRESSO (4 tile/byte, 2-bit/tile)");
            else
                sb.AppendLine("// FORMATO STANDARD: 1 byte per tile");
            sb.AppendLine("// ===========================================================");
            sb.AppendLine();

            if (exportAllMaps && project != null)
            {
                foreach (var m in project.Maps)
                {
                    ExportSingleMapC(sb, m, compressionMode);
                    sb.AppendLine();
                }
            }
            else if (map != null)
            {
                ExportSingleMapC(sb, map, compressionMode);
            }

            return sb.ToString();
        }

        public static void ExportSingleMapC(StringBuilder sb, GameMap map, MapCompressionMode mode = MapCompressionMode.Uncompressed)
        {
            if (map == null) return;

            if (mode == MapCompressionMode.Uncompressed || mode == MapCompressionMode.Both)
            {
                ExportSingleMapCStandard(sb, map);
            }

            if (mode == MapCompressionMode.Both)
            {
                sb.AppendLine();
            }

            if (mode == MapCompressionMode.Packed2Bit || mode == MapCompressionMode.Both)
            {
                ExportSingleMapCPacked(sb, map);
            }
        }

        public static void ExportSingleMapCStandard(StringBuilder sb, GameMap map)
        {
            if (map == null) return;
            string cleanName = SanitizeIdentifier(map.Name);
            sb.AppendLine($"// --- Mappa {map.Name} (Standard 1:1 - {map.Width * map.Height} byte) ---");
            sb.AppendLine($"#define {cleanName.ToUpper()}_WIDTH  {map.Width}");
            sb.AppendLine($"#define {cleanName.ToUpper()}_HEIGHT {map.Height}");
            sb.AppendLine($"const unsigned char {cleanName}[{map.Height}][{map.Width}] = {{");

            for (int y = 0; y < map.Height; y++)
            {
                sb.Append("    { ");
                for (int x = 0; x < map.Width; x++)
                {
                    sb.Append(map.GetTile(x, y));
                    if (x < map.Width - 1) sb.Append(", ");
                }
                sb.Append(" }");
                if (y < map.Height - 1) sb.Append(",");
                sb.AppendLine($" // Riga {y}");
            }
            sb.AppendLine("};");
        }

        public static void ExportSingleMapCPacked(StringBuilder sb, GameMap map)
        {
            if (map == null) return;
            string cleanName = SanitizeIdentifier(map.Name);
            int bytesPerRow = (map.Width + 3) / 4;
            int totalBytes = bytesPerRow * map.Height;

            sb.AppendLine($"// --- Mappa {map.Name} (Compresso 2-bit, 4 tile/byte - {totalBytes} byte) ---");
            sb.AppendLine($"// Struttura byte (MSB->LSB): [Bit 7-6: Tile x+0] [Bit 5-4: Tile x+1] [Bit 3-2: Tile x+2] [Bit 1-0: Tile x+3]");
            sb.AppendLine($"#define {cleanName.ToUpper()}_PACKED_WIDTH         {map.Width}");
            sb.AppendLine($"#define {cleanName.ToUpper()}_PACKED_HEIGHT        {map.Height}");
            sb.AppendLine($"#define {cleanName.ToUpper()}_PACKED_BYTES_PER_ROW {bytesPerRow}");
            sb.AppendLine($"const unsigned char {cleanName}_packed[{map.Height}][{bytesPerRow}] = {{");

            for (int y = 0; y < map.Height; y++)
            {
                sb.Append("    { ");
                for (int b = 0; b < bytesPerRow; b++)
                {
                    int startX = b * 4;
                    int t0 = (startX < map.Width) ? (map.GetTile(startX, y) & 0x03) : 0;
                    int t1 = (startX + 1 < map.Width) ? (map.GetTile(startX + 1, y) & 0x03) : 0;
                    int t2 = (startX + 2 < map.Width) ? (map.GetTile(startX + 2, y) & 0x03) : 0;
                    int t3 = (startX + 3 < map.Width) ? (map.GetTile(startX + 3, y) & 0x03) : 0;

                    byte packed = (byte)((t0 << 6) | (t1 << 4) | (t2 << 2) | t3);
                    sb.Append("0x" + packed.ToString("X2"));

                    if (b < bytesPerRow - 1) sb.Append(", ");
                }
                sb.Append(" }");
                if (y < map.Height - 1) sb.Append(",");
                sb.AppendLine($" // Riga {y}");
            }
            sb.AppendLine("};");
        }

        public static string ExportToBasic(GameMap map, int startLine = 5000, MapCompressionMode compressionMode = MapCompressionMode.Uncompressed)
        {
            if (map == null) return string.Empty;

            StringBuilder sb = new StringBuilder();
            int line = startLine;

            if (compressionMode == MapCompressionMode.Packed2Bit)
            {
                int bytesPerRow = (map.Width + 3) / 4;
                int totalPackedBytes = bytesPerRow * map.Height;

                sb.AppendLine($"{line} REM *** MAPPA ZX SPECTRUM: {map.Name} (Compresso 2-bit, 4 tile/byte) ***"); line += 10;
                sb.AppendLine($"{line} REM Dim: {map.Width}x{map.Height} tiles, {bytesPerRow} byte/riga, {totalPackedBytes} byte"); line += 10;
                sb.AppendLine($"{line} REM Bit 7-6: T0, Bit 5-4: T1, Bit 3-2: T2, Bit 1-0: T3"); line += 10;
                sb.AppendLine($"{line} DATA {map.Width},{map.Height},{bytesPerRow}"); line += 10;

                for (int y = 0; y < map.Height; y++)
                {
                    sb.Append($"{line} DATA ");
                    for (int b = 0; b < bytesPerRow; b++)
                    {
                        int startX = b * 4;
                        int t0 = (startX < map.Width) ? (map.GetTile(startX, y) & 0x03) : 0;
                        int t1 = (startX + 1 < map.Width) ? (map.GetTile(startX + 1, y) & 0x03) : 0;
                        int t2 = (startX + 2 < map.Width) ? (map.GetTile(startX + 2, y) & 0x03) : 0;
                        int t3 = (startX + 3 < map.Width) ? (map.GetTile(startX + 3, y) & 0x03) : 0;

                        byte packed = (byte)((t0 << 6) | (t1 << 4) | (t2 << 2) | t3);
                        sb.Append(packed);

                        if (b < bytesPerRow - 1) sb.Append(",");
                    }
                    sb.AppendLine();
                    line += 10;
                }
            }
            else if (compressionMode == MapCompressionMode.Both)
            {
                sb.AppendLine($"{line} REM *** MAPPA ZX SPECTRUM: {map.Name} (Standard 1:1) ***"); line += 10;
                sb.AppendLine($"{line} REM Dim: {map.Width}x{map.Height} tiles, {map.Width * map.Height} byte"); line += 10;
                sb.AppendLine($"{line} DATA {map.Width},{map.Height}"); line += 10;

                for (int y = 0; y < map.Height; y++)
                {
                    sb.Append($"{line} DATA ");
                    for (int x = 0; x < map.Width; x++)
                    {
                        sb.Append(map.GetTile(x, y));
                        if (x < map.Width - 1) sb.Append(",");
                    }
                    sb.AppendLine();
                    line += 10;
                }

                int bytesPerRow = (map.Width + 3) / 4;
                int totalPackedBytes = bytesPerRow * map.Height;
                sb.AppendLine();
                sb.AppendLine($"{line} REM *** MAPPA ZX SPECTRUM: {map.Name} (Compresso 2-bit, 4 tile/byte) ***"); line += 10;
                sb.AppendLine($"{line} DATA {map.Width},{map.Height},{bytesPerRow}"); line += 10;

                for (int y = 0; y < map.Height; y++)
                {
                    sb.Append($"{line} DATA ");
                    for (int b = 0; b < bytesPerRow; b++)
                    {
                        int startX = b * 4;
                        int t0 = (startX < map.Width) ? (map.GetTile(startX, y) & 0x03) : 0;
                        int t1 = (startX + 1 < map.Width) ? (map.GetTile(startX + 1, y) & 0x03) : 0;
                        int t2 = (startX + 2 < map.Width) ? (map.GetTile(startX + 2, y) & 0x03) : 0;
                        int t3 = (startX + 3 < map.Width) ? (map.GetTile(startX + 3, y) & 0x03) : 0;

                        byte packed = (byte)((t0 << 6) | (t1 << 4) | (t2 << 2) | t3);
                        sb.Append(packed);

                        if (b < bytesPerRow - 1) sb.Append(",");
                    }
                    sb.AppendLine();
                    line += 10;
                }
            }
            else
            {
                sb.AppendLine($"{line} REM *** MAPPA ZX SPECTRUM: {map.Name} ***"); line += 10;
                sb.AppendLine($"{line} REM Dim: {map.Width}x{map.Height} tiles, {map.Width * map.Height} byte"); line += 10;
                sb.AppendLine($"{line} DATA {map.Width},{map.Height}"); line += 10;

                for (int y = 0; y < map.Height; y++)
                {
                    sb.Append($"{line} DATA ");
                    for (int x = 0; x < map.Width; x++)
                    {
                        sb.Append(map.GetTile(x, y));
                        if (x < map.Width - 1) sb.Append(",");
                    }
                    sb.AppendLine();
                    line += 10;
                }
            }

            return sb.ToString();
        }

        public static void ExportToBinary(string filePath, GameMap map, MapCompressionMode compressionMode = MapCompressionMode.Uncompressed)
        {
            if (map == null) return;
            byte[] data = (compressionMode == MapCompressionMode.Packed2Bit)
                ? map.ToPackedByteArray()
                : map.ToByteArray();
            File.WriteAllBytes(filePath, data);
        }

        public static void ExportToPackedBinary(string filePath, GameMap map)
        {
            ExportToBinary(filePath, map, MapCompressionMode.Packed2Bit);
        }

        public static Bitmap RenderMapToBitmap(GameMap map, SpriteProject project, int scale = 1, bool showGrid = false)
        {
            if (map == null) return new Bitmap(16, 16);

            int tilePixelSize = map.TileWidth;
            int bmpW = map.Width * tilePixelSize * scale;
            int bmpH = map.Height * tilePixelSize * scale;

            Bitmap bmp = new Bitmap(Math.Max(1, bmpW), Math.Max(1, bmpH));

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.SmoothingMode = SmoothingMode.None;

                Color bgColor = ZxSpectrumPalette.GetColor(map.DefaultPaper, map.Bright);
                using (SolidBrush bgBrush = new SolidBrush(bgColor))
                {
                    g.FillRectangle(bgBrush, 0, 0, bmpW, bmpH);
                }

                int step = tilePixelSize * scale;

                for (int y = 0; y < map.Height; y++)
                {
                    for (int x = 0; x < map.Width; x++)
                    {
                        int tileId = map.GetTile(x, y);
                        if (tileId > 0 && project != null && (tileId - 1) < project.Tiles.Count)
                        {
                            var tile = project.Tiles[tileId - 1];
                            using (Bitmap tileBmp = tile.RenderToBitmap(scale))
                            {
                                Rectangle destRect = new Rectangle(x * step, y * step, step, step);
                                g.DrawImage(tileBmp, destRect, 0, 0, tileBmp.Width, tileBmp.Height, GraphicsUnit.Pixel);
                            }
                        }

                        if (showGrid)
                        {
                            using (Pen gridPen = new Pen(Color.FromArgb(60, 255, 255, 255), 1f))
                            {
                                g.DrawRectangle(gridPen, x * step, y * step, step, step);
                            }
                        }
                    }
                }
            }

            return bmp;
        }

        public static void ExportToPng(string filePath, GameMap map, SpriteProject project, int scale = 2)
        {
            using (Bitmap bmp = RenderMapToBitmap(map, project, scale, showGrid: false))
            {
                bmp.Save(filePath, ImageFormat.Png);
            }
        }

        private static string SanitizeIdentifier(string name)
        {
            if (string.IsNullOrEmpty(name)) return "map";
            string clean = Regex.Replace(name, @"[^a-zA-Z0-9_]", "_");
            if (char.IsDigit(clean[0])) clean = "m_" + clean;
            return clean.ToLower();
        }
    }
}
