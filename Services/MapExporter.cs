using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Globalization;
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
        public static List<Tile> GetTilesToExport(GameMap map, SpriteProject project, MapCompressionMode compressionMode, bool exportAllMaps = false)
        {
            var list = new List<Tile>();
            if (project == null || project.Tiles.Count == 0) return list;

            if (exportAllMaps)
            {
                var seenIds = new HashSet<int>();
                foreach (var m in project.Maps)
                {
                    if (m.AssignedTileIds != null && m.AssignedTileIds.Count > 0)
                    {
                        int limit = (project.TilesPerMap > 0) ? project.TilesPerMap : m.AssignedTileIds.Count;
                        if (compressionMode == MapCompressionMode.Packed2Bit) limit = Math.Min(limit, 3);
                        int count = Math.Min(m.AssignedTileIds.Count, limit);
                        for (int i = 0; i < count; i++)
                        {
                            int tid = m.AssignedTileIds[i];
                            if (!seenIds.Contains(tid))
                            {
                                seenIds.Add(tid);
                                if (tid > 0 && tid <= project.Tiles.Count)
                                    list.Add(project.Tiles[tid - 1]);
                            }
                        }
                    }
                }
                if (list.Count == 0)
                {
                    int maxAllowed = (project.TilesPerMap > 0) ? project.TilesPerMap : project.Tiles.Count;
                    if (compressionMode == MapCompressionMode.Packed2Bit) maxAllowed = Math.Min(maxAllowed, 3);
                    int count = Math.Min(project.Tiles.Count, maxAllowed);
                    for (int i = 0; i < count; i++)
                        list.Add(project.Tiles[i]);
                }
                return list;
            }

            if (map != null && map.AssignedTileIds != null && map.AssignedTileIds.Count > 0)
            {
                int maxAllowed = (project.TilesPerMap > 0) ? project.TilesPerMap : map.AssignedTileIds.Count;
                if (compressionMode == MapCompressionMode.Packed2Bit)
                {
                    maxAllowed = Math.Min(maxAllowed, 3);
                }
                int count = Math.Min(map.AssignedTileIds.Count, maxAllowed);
                for (int i = 0; i < count; i++)
                {
                    int tid = map.AssignedTileIds[i];
                    if (tid > 0 && tid <= project.Tiles.Count)
                    {
                        list.Add(project.Tiles[tid - 1]);
                    }
                }
            }
            else
            {
                int maxAllowed = (project.TilesPerMap > 0) ? project.TilesPerMap : project.Tiles.Count;
                if (compressionMode == MapCompressionMode.Packed2Bit)
                {
                    maxAllowed = Math.Min(maxAllowed, 3);
                }
                int count = Math.Min(project.Tiles.Count, maxAllowed);
                for (int i = 0; i < count; i++)
                    list.Add(project.Tiles[i]);
            }

            return list;
        }

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
                    if (m < project.Maps.Count - 1) sb.AppendLine();
                }
            }
            else if (map != null)
            {
                ExportSingleMapAsm(sb, map, dirStr, compressionMode, numberFormat);
            }

            // Esportazione dei dati grafici 8x8 dei Tile (Tileset)
            if (project != null && project.Tiles.Count > 0)
            {
                var tilesToExport = GetTilesToExport(map, project, compressionMode, exportAllMaps);

                sb.AppendLine();
                sb.AppendLine("; ===========================================================");
                sb.AppendLine("; TILESET (8x8 PIXEL) - DEFINIZIONI GRAFICHE DEI TILE");
                sb.AppendLine($"; Totale tile esportati: {tilesToExport.Count} (assegnati alla mappa: {(map?.AssignedTileIds?.Count ?? 0)}, limite di progetto: {project.TilesPerMap})");
                sb.AppendLine("; ===========================================================");
                for (int t = 0; t < tilesToExport.Count; t++)
                {
                    var tile = tilesToExport[t];
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
            bool hasAssigned = (map.AssignedTileIds != null && map.AssignedTileIds.Count > 0);

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
                    int val = hasAssigned ? map.GetExportSlot(map.GetTile(x, y)) : map.GetTile(x, y);
                    sb.Append(val);
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
            bool hasAssigned = (map.AssignedTileIds != null && map.AssignedTileIds.Count > 0);

            bool hasOverflow = false;
            for (int y = 0; y < map.Height && !hasOverflow; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    int slotVal = hasAssigned ? map.GetExportSlot(map.GetTile(x, y)) : map.GetTile(x, y);
                    if (slotVal > 3)
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
            sb.AppendLine($"; Indici tile (2-bit): 0..3 (00=Tile 0, 01=Tile 1, 10=Tile 2, 11=Tile 3)");
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
                    int t0 = (startX < map.Width) ? map.GetExportSlot2Bit(map.GetTile(startX, y)) : 0;
                    int t1 = (startX + 1 < map.Width) ? map.GetExportSlot2Bit(map.GetTile(startX + 1, y)) : 0;
                    int t2 = (startX + 2 < map.Width) ? map.GetExportSlot2Bit(map.GetTile(startX + 2, y)) : 0;
                    int t3 = (startX + 3 < map.Width) ? map.GetExportSlot2Bit(map.GetTile(startX + 3, y)) : 0;

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

            // Esportazione dei dati grafici 8x8 dei Tile (Tileset) in C
            if (project != null && project.Tiles.Count > 0)
            {
                var tilesToExport = GetTilesToExport(map, project, compressionMode, exportAllMaps);

                sb.AppendLine();
                sb.AppendLine("// ===========================================================");
                sb.AppendLine("// TILESET (8x8 PIXEL) - DEFINIZIONI GRAFICHE DEI TILE");
                sb.AppendLine($"// Totale tile esportati: {tilesToExport.Count} (assegnati alla mappa: {(map?.AssignedTileIds?.Count ?? 0)}, limite di progetto: {project.TilesPerMap})");
                sb.AppendLine("// ===========================================================");
                for (int t = 0; t < tilesToExport.Count; t++)
                {
                    var tile = tilesToExport[t];
                    string tName = SanitizeIdentifier(tile.Name);
                    byte attr = tile.GetAttributeByte();
                    var hexData = tile.GetBytesRowByRow();
                    sb.AppendLine($"// Tile {t + 1}: {tile.Name} (Attr: 0x{attr:X2})");
                    sb.Append($"const unsigned char tile_{t + 1}_{tName}[9] = {{ 0x{attr:X2}, ");
                    for (int b = 0; b < hexData.Length; b++)
                    {
                        sb.Append($"0x{hexData[b]:X2}");
                        if (b < hexData.Length - 1) sb.Append(", ");
                    }
                    sb.AppendLine(" };");
                }
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
            bool hasAssigned = (map.AssignedTileIds != null && map.AssignedTileIds.Count > 0);
            sb.AppendLine($"// --- Mappa {map.Name} (Standard 1:1 - {map.Width * map.Height} byte) ---");
            sb.AppendLine($"#define {cleanName.ToUpper()}_WIDTH  {map.Width}");
            sb.AppendLine($"#define {cleanName.ToUpper()}_HEIGHT {map.Height}");
            sb.AppendLine($"const unsigned char {cleanName}[{map.Height}][{map.Width}] = {{");

            for (int y = 0; y < map.Height; y++)
            {
                sb.Append("    { ");
                for (int x = 0; x < map.Width; x++)
                {
                    int val = hasAssigned ? map.GetExportSlot(map.GetTile(x, y)) : map.GetTile(x, y);
                    sb.Append(val);
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
            bool hasAssigned = (map.AssignedTileIds != null && map.AssignedTileIds.Count > 0);

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
                    int t0 = (startX < map.Width) ? map.GetExportSlot2Bit(map.GetTile(startX, y)) : 0;
                    int t1 = (startX + 1 < map.Width) ? map.GetExportSlot2Bit(map.GetTile(startX + 1, y)) : 0;
                    int t2 = (startX + 2 < map.Width) ? map.GetExportSlot2Bit(map.GetTile(startX + 2, y)) : 0;
                    int t3 = (startX + 3 < map.Width) ? map.GetExportSlot2Bit(map.GetTile(startX + 3, y)) : 0;

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

        public static string ExportToBasic(GameMap map, int startLine = 5000, MapCompressionMode compressionMode = MapCompressionMode.Uncompressed, SpriteProject project = null)
        {
            if (map == null) return string.Empty;

            StringBuilder sb = new StringBuilder();
            int line = startLine;
            bool hasAssigned = (map.AssignedTileIds != null && map.AssignedTileIds.Count > 0);

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
                        int t0 = (startX < map.Width) ? map.GetExportSlot2Bit(map.GetTile(startX, y)) : 0;
                        int t1 = (startX + 1 < map.Width) ? map.GetExportSlot2Bit(map.GetTile(startX + 1, y)) : 0;
                        int t2 = (startX + 2 < map.Width) ? map.GetExportSlot2Bit(map.GetTile(startX + 2, y)) : 0;
                        int t3 = (startX + 3 < map.Width) ? map.GetExportSlot2Bit(map.GetTile(startX + 3, y)) : 0;

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
                        int val = hasAssigned ? map.GetExportSlot(map.GetTile(x, y)) : map.GetTile(x, y);
                        sb.Append(val);
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
                        int t0 = (startX < map.Width) ? map.GetExportSlot2Bit(map.GetTile(startX, y)) : 0;
                        int t1 = (startX + 1 < map.Width) ? map.GetExportSlot2Bit(map.GetTile(startX + 1, y)) : 0;
                        int t2 = (startX + 2 < map.Width) ? map.GetExportSlot2Bit(map.GetTile(startX + 2, y)) : 0;
                        int t3 = (startX + 3 < map.Width) ? map.GetExportSlot2Bit(map.GetTile(startX + 3, y)) : 0;

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
                        int val = hasAssigned ? map.GetExportSlot(map.GetTile(x, y)) : map.GetTile(x, y);
                        sb.Append(val);
                        if (x < map.Width - 1) sb.Append(",");
                    }
                    sb.AppendLine();
                    line += 10;
                }
            }

            // Esportazione dei dati grafici 8x8 dei Tile (Tileset) in BASIC
            if (project != null && project.Tiles.Count > 0)
            {
                var tilesToExport = GetTilesToExport(map, project, compressionMode);

                sb.AppendLine();
                sb.AppendLine($"{line} REM *** TILESET (8x8 PIXEL) - {tilesToExport.Count} TILE ESPORTATI (Limite: {project.TilesPerMap}) ***"); line += 10;
                for (int t = 0; t < tilesToExport.Count; t++)
                {
                    var tile = tilesToExport[t];
                    byte attr = tile.GetAttributeByte();
                    var rawBytes = tile.GetBytesRowByRow();
                    sb.AppendLine($"{line} REM Tile {t}: {tile.Name} (Attr={attr})"); line += 10;
                    sb.Append($"{line} DATA {attr}");
                    for (int b = 0; b < rawBytes.Length; b++)
                    {
                        sb.Append($",{rawBytes[b]}");
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

    /// <summary>
    /// Servizio per importare mappe di gioco per Sinclair ZX Spectrum da sorgenti di testo o dati binari
    /// (Assembly Z80, C/Z88DK, Sinclair BASIC o elenchi di byte).
    /// Supporta il formato compresso 2-bit (4 tile per byte riga per riga).
    /// </summary>
    public static class MapImporter
    {
        /// <summary>
        /// Esegue il parsing di testo contenente direttive db/defb/DEFB, C array, BASIC DATA
        /// o liste di numeri in vari formati (binario % o 0b, esadecimale $, 0x o h, decimale).
        /// </summary>
        public static List<byte> ParseBytesFromText(string text)
        {
            var result = new List<byte>();
            if (string.IsNullOrWhiteSpace(text)) return result;

            // Rimuovi commenti a blocco C /* ... */
            string cleanText = Regex.Replace(text, @"/\*.*?\*/", " ", RegexOptions.Singleline);

            var lines = cleanText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var cleanLines = new List<string>();

            foreach (var line in lines)
            {
                // Se la riga indica l'inizio della sezione TILESET, fermati (importiamo solo la mappa, non le tiles)
                if (line.IndexOf("TILESET", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    break;
                }

                string cleaned = line;

                // Rimuovi commenti di riga Assembly (;)
                int commentIdx = cleaned.IndexOf(';');
                if (commentIdx >= 0) cleaned = cleaned.Substring(0, commentIdx);

                // Rimuovi commenti di riga C (//)
                commentIdx = cleaned.IndexOf("//", StringComparison.Ordinal);
                if (commentIdx >= 0) cleaned = cleaned.Substring(0, commentIdx);

                // Rimuovi commenti BASIC (REM)
                int remIdx = cleaned.IndexOf("REM", StringComparison.OrdinalIgnoreCase);
                if (remIdx >= 0) cleaned = cleaned.Substring(0, remIdx);

                // Rimuovi etichette (es. "map_label:")
                cleaned = Regex.Replace(cleaned, @"^[A-Za-z0-9_]+:\s*", "");

                // Rimuovi direttive assembly, C keywords e BASIC DATA
                cleaned = Regex.Replace(cleaned, @"\b(defb|db|DEFB|\.byte|BYTE|DATA|const|unsigned|char|static)\b", " ", RegexOptions.IgnoreCase);

                // Rimuovi numeri di riga basic all'inizio (es. "5040 DATA ...")
                cleaned = Regex.Replace(cleaned, @"^\s*\d+\s+", "");

                // Rimuovi caratteri di formattazione C ({, }, [, ], =, ;)
                cleaned = cleaned.Replace('{', ' ').Replace('}', ' ').Replace('[', ' ').Replace(']', ' ').Replace('=', ' ');

                cleanLines.Add(cleaned);
            }

            string allText = string.Join(" ", cleanLines);

            // Trova tutti i token separati da virgole, spazi o tabulazioni
            string[] tokens = allText.Split(new[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var token in tokens)
            {
                string t = token.Trim();
                if (string.IsNullOrEmpty(t)) continue;

                if (TryParseByte(t, out byte b))
                {
                    result.Add(b);
                }
            }

            return result;
        }

        /// <summary>
        /// Tenta di interpretare un singolo token come byte (binario %, 0b, hex $, 0x, h o decimale).
        /// </summary>
        public static bool TryParseByte(string token, out byte value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(token)) return false;

            token = token.Trim();

            try
            {
                // Formato Binario: %01010101 oppure 0b01010101
                if (token.StartsWith("%"))
                {
                    string bin = token.Substring(1).Trim();
                    value = Convert.ToByte(bin, 2);
                    return true;
                }
                if (token.StartsWith("0b", StringComparison.OrdinalIgnoreCase))
                {
                    string bin = token.Substring(2).Trim();
                    value = Convert.ToByte(bin, 2);
                    return true;
                }

                // Formato Esadecimale: $FF, 0xFF, FFh
                if (token.StartsWith("$"))
                {
                    string hex = token.Substring(1).Trim();
                    if (byte.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value))
                        return true;
                }
                if (token.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                {
                    string hex = token.Substring(2).Trim();
                    if (byte.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value))
                        return true;
                }
                if (token.EndsWith("h", StringComparison.OrdinalIgnoreCase) && token.Length > 1)
                {
                    string hex = token.Substring(0, token.Length - 1).Trim();
                    if (byte.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value))
                        return true;
                }

                // Decimale standard (0..255)
                if (byte.TryParse(token, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
                {
                    return true;
                }

                // Binario senza prefisso (stringa di esattamente 8 caratteri '0' e '1')
                if (token.Length == 8 && Regex.IsMatch(token, @"^[01]{8}$"))
                {
                    value = Convert.ToByte(token, 2);
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Analizza il testo per verificare se contiene intestazioni con dimensioni (Assembly, C, BASIC).
        /// </summary>
        public static bool TryDetectHeader(string text, out int width, out int height, out string mapName, out bool hasHeaderBytes)
        {
            width = 32;
            height = 24;
            mapName = "Mappa Importata";
            hasHeaderBytes = false;

            if (string.IsNullOrWhiteSpace(text)) return false;

            // 1. Riconoscimento intestazione Assembly esportata
            // Es: cleanName_packed_width: defb 32
            //     cleanName_packed_height: defb 24
            Match mW = Regex.Match(text, @"(\w+)_packed_width:\s*(?:defb|db|\.byte)\s*(\d+)", RegexOptions.IgnoreCase);
            Match mH = Regex.Match(text, @"(\w+)_packed_height:\s*(?:defb|db|\.byte)\s*(\d+)", RegexOptions.IgnoreCase);
            if (mW.Success && mH.Success)
            {
                if (int.TryParse(mW.Groups[2].Value, out int w) && int.TryParse(mH.Groups[2].Value, out int h))
                {
                    width = Math.Max(1, Math.Min(256, w));
                    height = Math.Max(1, Math.Min(256, h));
                    mapName = mW.Groups[1].Value.Replace("_", " ").Trim();
                    hasHeaderBytes = true;
                    return true;
                }
            }

            // 2. Riconoscimento intestazione C / Z88DK
            // Es: #define MAP1_PACKED_WIDTH 32
            //     #define MAP1_PACKED_HEIGHT 24
            Match cW = Regex.Match(text, @"#define\s+(\w+)_PACKED_WIDTH\s+(\d+)", RegexOptions.IgnoreCase);
            Match cH = Regex.Match(text, @"#define\s+(\w+)_PACKED_HEIGHT\s+(\d+)", RegexOptions.IgnoreCase);
            if (cW.Success && cH.Success)
            {
                if (int.TryParse(cW.Groups[2].Value, out int w) && int.TryParse(cH.Groups[2].Value, out int h))
                {
                    width = Math.Max(1, Math.Min(256, w));
                    height = Math.Max(1, Math.Min(256, h));
                    mapName = cW.Groups[1].Value.Replace("_", " ").Trim();
                    hasHeaderBytes = false;
                    return true;
                }
            }

            // 3. Riconoscimento intestazione Sinclair BASIC
            // Es: DATA 32,24,8
            Match bData = Regex.Match(text, @"DATA\s+(\d+)\s*,\s*(\d+)\s*,\s*(\d+)", RegexOptions.IgnoreCase);
            if (bData.Success)
            {
                if (int.TryParse(bData.Groups[1].Value, out int w) && int.TryParse(bData.Groups[2].Value, out int h))
                {
                    width = Math.Max(1, Math.Min(256, w));
                    height = Math.Max(1, Math.Min(256, h));
                    Match bName = Regex.Match(text, @"REM\s+\*\*\*\s+MAPPA(?:\s+ZX\s+SPECTRUM)?:\s+([^\*]+)", RegexOptions.IgnoreCase);
                    if (bName.Success)
                    {
                        mapName = bName.Groups[1].Value.Replace("(Compresso 2-bit, 4 tile/byte)", "").Trim();
                    }
                    hasHeaderBytes = true;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Decomprime i dati a 2-bit (4 tile/byte riga per riga) inserendoli nella GameMap indicata.
        /// Supporta la mappatura completa di tutti e 4 i valori a 2-bit (0..3) e preserva intatte
        /// tutte le tile già presenti nel Set Mappa.
        /// </summary>
        public static void ImportPacked2Bit(
            GameMap targetMap,
            IReadOnlyList<byte> bytes,
            int width,
            int height,
            int slot0TileId,
            int slot1TileId,
            int slot2TileId,
            int slot3TileId,
            bool skipHeader,
            bool preserveExistingAssignedTiles = true)
        {
            if (targetMap == null || bytes == null) return;

            width = Math.Max(1, width);
            height = Math.Max(1, height);

            int bytesPerRow = (width + 3) / 4;
            int startIndex = skipHeader ? 3 : 0;

            // Se la mappa ha dimensioni diverse, ridimensionala
            if (targetMap.Width != width || targetMap.Height != height)
            {
                targetMap.Resize(width, height);
            }

            // Pulisci le celle della mappa (imposta tutte le celle a 0)
            targetMap.Clear();

            // Decompressione riga per riga
            for (int y = 0; y < height; y++)
            {
                for (int b = 0; b < bytesPerRow; b++)
                {
                    int byteIdx = startIndex + (y * bytesPerRow) + b;
                    if (byteIdx >= bytes.Count) break;

                    byte packedByte = bytes[byteIdx];

                    // Estrai i 4 indici a 2-bit (MSB -> LSB)
                    int i0 = (packedByte >> 6) & 0x03;
                    int i1 = (packedByte >> 4) & 0x03;
                    int i2 = (packedByte >> 2) & 0x03;
                    int i3 = packedByte & 0x03;

                    int startX = b * 4;
                    if (startX + 0 < width) targetMap.SetTile(startX + 0, y, ResolveTileId(i0, slot0TileId, slot1TileId, slot2TileId, slot3TileId));
                    if (startX + 1 < width) targetMap.SetTile(startX + 1, y, ResolveTileId(i1, slot0TileId, slot1TileId, slot2TileId, slot3TileId));
                    if (startX + 2 < width) targetMap.SetTile(startX + 2, y, ResolveTileId(i2, slot0TileId, slot1TileId, slot2TileId, slot3TileId));
                    if (startX + 3 < width) targetMap.SetTile(startX + 3, y, ResolveTileId(i3, slot0TileId, slot1TileId, slot2TileId, slot3TileId));
                }
            }

            // Configura le tile assegnate alla mappa: NON cancellare le tile esistenti se preserveExistingAssignedTiles è vero!
            if (targetMap.AssignedTileIds == null)
            {
                targetMap.AssignedTileIds = new List<int>();
            }

            if (!preserveExistingAssignedTiles)
            {
                targetMap.AssignedTileIds.Clear();
            }

            // Aggiungi ciascuna tile utilizzata nella mappatura degli slot (se non già presente)
            int[] usedSlots = new int[] { slot0TileId, slot1TileId, slot2TileId, slot3TileId };
            foreach (int tid in usedSlots)
            {
                if (tid > 0 && !targetMap.AssignedTileIds.Contains(tid))
                {
                    targetMap.AssignedTileIds.Add(tid);
                }
            }
        }

        /// <summary>
        /// Overload per compatibilità con chiamate a 3 slot (slot 0 predefinito a vuoto/0).
        /// </summary>
        public static void ImportPacked2Bit(
            GameMap targetMap,
            IReadOnlyList<byte> bytes,
            int width,
            int height,
            int slot1TileId,
            int slot2TileId,
            int slot3TileId,
            bool skipHeader = false)
        {
            ImportPacked2Bit(targetMap, bytes, width, height, 0, slot1TileId, slot2TileId, slot3TileId, skipHeader, true);
        }

        private static int ResolveTileId(int index2Bit, int slot0TileId, int slot1TileId, int slot2TileId, int slot3TileId)
        {
            switch (index2Bit)
            {
                case 0: return slot0TileId;
                case 1: return slot1TileId;
                case 2: return slot2TileId;
                case 3: return slot3TileId;
                default: return 0;
            }
        }

        /// <summary>
        /// Rileva automaticamente il formato più probabile per i dati byte incollati.
        /// </summary>
        public static MapImportFormat DetectFormat(IReadOnlyList<byte> bytes, int expectedWidth = 32, int expectedHeight = 16)
        {
            if (bytes == null || bytes.Count == 0) return MapImportFormat.Packed2Bit;

            // Se sono 512 byte (o tra 500 e 520)
            if (bytes.Count == 512 || (bytes.Count >= 500 && bytes.Count <= 520))
            {
                // Controlla se ci sono valori tipici di attributi Spectrum (> 7, es. 22, 66, 68)
                bool hasHighAttributes = false;
                for (int i = 0; i < Math.Min(bytes.Count, 512); i++)
                {
                    if (bytes[i] > 7)
                    {
                        hasHighAttributes = true;
                        break;
                    }
                }
                return hasHighAttributes ? MapImportFormat.ManicMinerAttributes : MapImportFormat.Linear1BytePerCell;
            }

            // Se corrisponde alla dimensione 2-bit (es. 128 byte per 32x16, o 192 per 32x24)
            int packed2BitExpected = ((expectedWidth + 3) / 4) * expectedHeight;
            if (bytes.Count == packed2BitExpected || (bytes.Count >= packed2BitExpected && bytes.Count <= packed2BitExpected + 3))
            {
                return MapImportFormat.Packed2Bit;
            }

            // Se corrisponde esattamente a W * H
            if (bytes.Count == expectedWidth * expectedHeight)
            {
                return MapImportFormat.Linear1BytePerCell;
            }

            return MapImportFormat.Packed2Bit;
        }

        /// <summary>
        /// Importa il layout della caverna (512 byte di attributi ZX Spectrum conformi a SkoolKit 45056)
        /// mappando ciascun attributo sulla rispettiva tile della caverna o del progetto.
        /// </summary>
        public static void ImportManicMinerAttributes(
            GameMap targetMap,
            IReadOnlyList<byte> bytes,
            SpriteProject project,
            int width = 32,
            int height = 16,
            IDictionary<byte, int> customAttrToTileMap = null,
            bool updateCavernData = true)
        {
            if (targetMap == null || bytes == null || bytes.Count == 0) return;

            width = Math.Max(1, width);
            height = Math.Max(1, height);

            if (targetMap.Width != width || targetMap.Height != height)
            {
                targetMap.Resize(width, height);
            }

            // Costruisci dizionario attributo -> tileId (1-based)
            Dictionary<byte, int> attrMap = new Dictionary<byte, int>();

            if (customAttrToTileMap != null && customAttrToTileMap.Count > 0)
            {
                foreach (var kvp in customAttrToTileMap)
                    attrMap[kvp.Key] = kvp.Value;
            }
            else
            {
                // Mappatura automatica basata sui tile della caverna ManicMinerData se presente
                if (targetMap.ManicMinerData != null && targetMap.ManicMinerData.TileDefs != null)
                {
                    for (int s = 0; s < 8; s++)
                    {
                        var td = targetMap.ManicMinerData.TileDefs[s];
                        if (td != null)
                        {
                            int tid = (targetMap.AssignedTileIds != null && s < targetMap.AssignedTileIds.Count)
                                ? targetMap.AssignedTileIds[s]
                                : (s + 1);
                            if (!attrMap.ContainsKey(td.Attribute))
                            {
                                attrMap[td.Attribute] = tid;
                            }
                        }
                    }
                }

                // Altrimenti basata sulle tile assegnate alla mappa
                if (targetMap.AssignedTileIds != null && targetMap.AssignedTileIds.Count > 0 && project != null)
                {
                    foreach (int tid in targetMap.AssignedTileIds)
                    {
                        if (tid > 0 && tid <= project.Tiles.Count)
                        {
                            var t = project.Tiles[tid - 1];
                            byte attr = (byte)((t.Flash ? 128 : 0) | (t.Bright ? 64 : 0) | (t.PaperColor << 3) | t.InkColor);
                            if (!attrMap.ContainsKey(attr)) attrMap[attr] = tid;
                        }
                    }
                }

                // Altrimenti basata su tutte le tile del progetto
                if (attrMap.Count == 0 && project != null)
                {
                    for (int i = 0; i < project.Tiles.Count; i++)
                    {
                        var t = project.Tiles[i];
                        byte attr = (byte)((t.Flash ? 128 : 0) | (t.Bright ? 64 : 0) | (t.PaperColor << 3) | t.InkColor);
                        if (!attrMap.ContainsKey(attr)) attrMap[attr] = i + 1;
                    }
                }
            }

            targetMap.Clear();

            int maxCells = Math.Min(bytes.Count, width * height);
            for (int i = 0; i < maxCells; i++)
            {
                int x = i % width;
                int y = i / width;
                byte attr = bytes[i];

                int tileId = 0;
                if (attrMap.TryGetValue(attr, out int mappedId))
                {
                    tileId = mappedId;
                }
                else if (attr < 8 && targetMap.AssignedTileIds != null && attr < targetMap.AssignedTileIds.Count)
                {
                    // Fallback: se l'attributo è < 8 ed è un indice slot valido
                    tileId = targetMap.AssignedTileIds[attr];
                }

                targetMap.SetTile(x, y, tileId);
            }

            if (updateCavernData && targetMap.ManicMinerData != null)
            {
                targetMap.ManicMinerData.UpdateFromGameMap(targetMap, project);
            }
        }

        /// <summary>
        /// Importa i dati nel formato lineare 1 byte per cella (riga per riga),
        /// mappando ciascun valore (0..N) sulla corrispondente tile o slot del Set Mappa.
        /// </summary>
        public static void ImportLinear1BytePerCell(
            GameMap targetMap,
            IReadOnlyList<byte> bytes,
            int width,
            int height,
            IReadOnlyList<int> slotTileIds,
            bool isZeroBased = true,
            SpriteProject project = null)
        {
            if (targetMap == null || bytes == null || bytes.Count == 0) return;

            width = Math.Max(1, width);
            height = Math.Max(1, height);

            if (targetMap.Width != width || targetMap.Height != height)
            {
                targetMap.Resize(width, height);
            }

            targetMap.Clear();

            int maxCells = Math.Min(bytes.Count, width * height);
            for (int i = 0; i < maxCells; i++)
            {
                int x = i % width;
                int y = i / width;
                int val = bytes[i];

                int tileId = 0;
                if (slotTileIds != null && slotTileIds.Count > 0)
                {
                    int slotIdx = isZeroBased ? val : (val - 1);
                    if (slotIdx >= 0 && slotIdx < slotTileIds.Count)
                    {
                        tileId = slotTileIds[slotIdx];
                    }
                }
                else
                {
                    tileId = isZeroBased ? val : val;
                }

                targetMap.SetTile(x, y, tileId);
            }

            if (targetMap.ManicMinerData != null && project != null)
            {
                targetMap.ManicMinerData.UpdateFromGameMap(targetMap, project);
            }
        }
    }

    /// <summary>
    /// Formato dei dati di input per l'importazione di mappe.
    /// </summary>
    public enum MapImportFormat
    {
        Packed2Bit,           // Formato compresso 2-bit (4 tile per byte, Jet Set Willy / Standard)
        ManicMinerAttributes, // 512 byte di attributi ZX Spectrum (SkoolKit 45056, 32x16)
        Linear1BytePerCell    // Formato lineare 1 byte per cella (indici tile 0..N)
    }
}
