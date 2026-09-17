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
    /// Servizio per esportare le mappe di gioco in vari formati:
    /// Assembly Z80 (defb/db), C/Z88DK, Sinclair BASIC (DATA), file binario (.bin) e immagine (.png).
    /// </summary>
    public static class MapExporter
    {
        public static string ExportToAssembly(GameMap map, AsmDirective directive = AsmDirective.Defb, bool exportAllMaps = false, SpriteProject project = null)
        {
            if (map == null && project == null) return string.Empty;

            StringBuilder sb = new StringBuilder();
            string dirStr = (directive == AsmDirective.Db) ? "db" : "defb";

            sb.AppendLine("; ===========================================================");
            sb.AppendLine("; SINCLAIR ZX SPECTRUM - ESPORTAZIONE MAPPE DI GIOCO (TILEMAP)");
            sb.AppendLine("; Generato da ZX Spectrum Sprite Generator & Studio");
            sb.AppendLine("; Tile 0 = Sfondo/Vuoto, Tile 1..N = Indice Tile (8x8)");
            sb.AppendLine("; ===========================================================");
            sb.AppendLine();

            if (exportAllMaps && project != null)
            {
                for (int m = 0; m < project.Maps.Count; m++)
                {
                    ExportSingleMapAsm(sb, project.Maps[m], dirStr);
                    sb.AppendLine();
                }
            }
            else if (map != null)
            {
                ExportSingleMapAsm(sb, map, dirStr);
            }

            // Esportazione dei dati grafici 8x8 dei Tile (Tileset)
            if (project != null && project.Tiles.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("; ===========================================================");
                sb.AppendLine("; TILESET (8x8 PIXEL) - DEFINIZIONI GRAFICHE DEI TILE");
                sb.AppendLine($"; Totale tile: {project.Tiles.Count}");
                sb.AppendLine("; ===========================================================");
                for (int t = 0; t < project.Tiles.Count; t++)
                {
                    var tile = project.Tiles[t];
                    string tName = SanitizeIdentifier(tile.Name);
                    sb.AppendLine($"; Tile {t + 1}: {tile.Name} (Ink: {tile.InkColor}, Paper: {tile.PaperColor})");
                    sb.AppendLine($"tile_{t + 1}_{tName}:");
                    var hexData = tile.GetBytesRowByRow();
                    sb.Append($"    {dirStr} ");
                    for (int b = 0; b < hexData.Length; b++)
                    {
                        sb.Append(hexData[b]);
                        if (b < hexData.Length - 1) sb.Append(",");
                    }
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        private static void ExportSingleMapAsm(StringBuilder sb, GameMap map, string dirStr)
        {
            string cleanName = SanitizeIdentifier(map.Name);
            int totalBytes = map.Width * map.Height;

            sb.AppendLine($"; --- Mappa: {map.Name} ({map.Width}x{map.Height} celle, {map.Width * map.TileWidth}x{map.Height * map.TileHeight} px, {totalBytes} byte) ---");
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

        public static string ExportToC(GameMap map, bool exportAllMaps = false, SpriteProject project = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("// ===========================================================");
            sb.AppendLine("// SINCLAIR ZX SPECTRUM / Z88DK - MAPPE DI GIOCO");
            sb.AppendLine("// ===========================================================");
            sb.AppendLine();

            if (exportAllMaps && project != null)
            {
                foreach (var m in project.Maps)
                {
                    ExportSingleMapC(sb, m);
                    sb.AppendLine();
                }
            }
            else if (map != null)
            {
                ExportSingleMapC(sb, map);
            }

            return sb.ToString();
        }

        private static void ExportSingleMapC(StringBuilder sb, GameMap map)
        {
            string cleanName = SanitizeIdentifier(map.Name);
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
                sb.AppendLine();
            }
            sb.AppendLine("};");
        }

        public static string ExportToBasic(GameMap map, int startLine = 5000)
        {
            if (map == null) return string.Empty;

            StringBuilder sb = new StringBuilder();
            int line = startLine;

            sb.AppendLine($"{line} REM *** MAPPA ZX SPECTRUM: {map.Name} ***"); line += 10;
            sb.AppendLine($"{line} REM Dim: {map.Width}x{map.Height} tiles"); line += 10;
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

            return sb.ToString();
        }

        public static void ExportToBinary(string filePath, GameMap map)
        {
            if (map == null) return;
            byte[] data = map.ToByteArray();
            File.WriteAllBytes(filePath, data);
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
