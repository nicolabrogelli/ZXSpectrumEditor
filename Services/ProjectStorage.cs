using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using ZXSpectrumSpriteGenerator.Models;

namespace ZXSpectrumSpriteGenerator.Services
{
    /// <summary>
    /// Servizio per salvare e caricare progetti sprite (.zxproj / .json).
    /// Il formato è JSON standard, leggibile, compatto e privo di dipendenze esterne.
    /// </summary>
    public static class ProjectStorage
    {
        public const string FileExtension = ".zxproj";
        public const string FileFilter = "Progetto Sprite ZX Spectrum (*.zxproj;*.json)|*.zxproj;*.json|Tutti i file (*.*)|*.*";

        /// <summary>
        /// Salva il progetto in un file JSON (.zxproj).
        /// </summary>
        public static void SaveToFile(string filePath, SpriteProject project)
        {
            string json = SerializeToJson(project);
            File.WriteAllText(filePath, json, Encoding.UTF8);
        }

        /// <summary>
        /// Carica un progetto da file (.zxproj o .json).
        /// </summary>
        public static SpriteProject LoadFromFile(string filePath)
        {
            string json = File.ReadAllText(filePath, Encoding.UTF8);
            return DeserializeFromJson(json);
        }

        /// <summary>
        /// Serializza il progetto in formato JSON strutturato e indentato.
        /// </summary>
        public static string SerializeToJson(SpriteProject project)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine("  \"formatVersion\": 1,");
            sb.AppendLine($"  \"title\": \"{EscapeJson(project.Title)}\",");
            sb.AppendLine($"  \"defaultWidth\": {project.DefaultWidth},");
            sb.AppendLine($"  \"defaultHeight\": {project.DefaultHeight},");
            sb.AppendLine($"  \"selectedIndex\": {project.SelectedIndex},");
            sb.AppendLine("  \"sprites\": [");

            for (int i = 0; i < project.Sprites.Count; i++)
            {
                var s = project.Sprites[i];
                sb.AppendLine("    {");
                sb.AppendLine($"      \"name\": \"{EscapeJson(s.Name)}\",");
                sb.AppendLine($"      \"groupName\": \"{EscapeJson(s.GroupName)}\",");
                sb.AppendLine($"      \"width\": {s.Width},");
                sb.AppendLine($"      \"height\": {s.Height},");
                sb.AppendLine($"      \"inkColor\": {s.InkColor},");
                sb.AppendLine($"      \"paperColor\": {s.PaperColor},");
                sb.AppendLine($"      \"bright\": {(s.Bright ? "true" : "false")},");
                sb.AppendLine($"      \"flash\": {(s.Flash ? "true" : "false")},");

                byte[] data = s.GetBytesRowByRow();
                string hex = ByteArrayToHex(data);
                sb.AppendLine($"      \"dataHex\": \"{hex}\"");

                sb.Append("    }");
                if (i < project.Sprites.Count - 1)
                {
                    sb.Append(",");
                }
                sb.AppendLine();
            }

            sb.AppendLine("  ],");
            sb.AppendLine($"  \"selectedTileIndex\": {project.SelectedTileIndex},");
            sb.AppendLine($"  \"tilesPerMap\": {project.TilesPerMap},");
            sb.AppendLine("  \"tiles\": [");

            for (int t = 0; t < project.Tiles.Count; t++)
            {
                var tile = project.Tiles[t];
                sb.AppendLine("    {");
                sb.AppendLine($"      \"name\": \"{EscapeJson(tile.Name)}\",");
                sb.AppendLine($"      \"inkColor\": {tile.InkColor},");
                sb.AppendLine($"      \"paperColor\": {tile.PaperColor},");
                sb.AppendLine($"      \"bright\": {(tile.Bright ? "true" : "false")},");
                sb.AppendLine($"      \"flash\": {(tile.Flash ? "true" : "false")},");

                byte[] data = tile.GetBytesRowByRow();
                string hex = ByteArrayToHex(data);
                sb.AppendLine($"      \"dataHex\": \"{hex}\"");

                sb.Append("    }");
                if (t < project.Tiles.Count - 1)
                {
                    sb.Append(",");
                }
                sb.AppendLine();
            }

            sb.AppendLine("  ],");
            sb.AppendLine($"  \"selectedMapIndex\": {project.SelectedMapIndex},");
            sb.AppendLine("  \"maps\": [");

            for (int m = 0; m < project.Maps.Count; m++)
            {
                var map = project.Maps[m];
                sb.AppendLine("    {");
                sb.AppendLine($"      \"name\": \"{EscapeJson(map.Name)}\",");
                sb.AppendLine($"      \"width\": {map.Width},");
                sb.AppendLine($"      \"height\": {map.Height},");
                sb.AppendLine($"      \"tileSize\": {map.TileWidth},");
                sb.AppendLine($"      \"defaultPaper\": {map.DefaultPaper},");
                sb.AppendLine($"      \"defaultInk\": {map.DefaultInk},");
                sb.AppendLine($"      \"bright\": {(map.Bright ? "true" : "false")},");

                byte[] mapData = map.ToRawByteArray();
                string mapHex = ByteArrayToHex(mapData);
                sb.AppendLine($"      \"tilesHex\": \"{mapHex}\",");
                string assignedStr = (map.AssignedTileIds != null && map.AssignedTileIds.Count > 0)
                    ? string.Join(", ", map.AssignedTileIds)
                    : string.Empty;
                sb.AppendLine($"      \"assignedTileIds\": [{assignedStr}]");

                sb.Append("    }");
                if (m < project.Maps.Count - 1)
                {
                    sb.Append(",");
                }
                sb.AppendLine();
            }

            sb.AppendLine("  ]");
            sb.AppendLine("}");
            return sb.ToString();
        }

        /// <summary>
        /// Deserializza un progetto da stringa JSON.
        /// </summary>
        public static SpriteProject DeserializeFromJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("Il contenuto del file è vuoto o non valido.");

            var project = new SpriteProject();

            // Estrai proprietà di primo livello
            string title = ExtractString(json, "title");
            if (!string.IsNullOrEmpty(title)) project.Title = title;

            int defW = ExtractInt(json, "defaultWidth", 16);
            int defH = ExtractInt(json, "defaultHeight", 16);
            int selIdx = ExtractInt(json, "selectedIndex", 0);
            int selMapIdx = ExtractInt(json, "selectedMapIndex", 0);

            project.DefaultWidth = defW;
            project.DefaultHeight = defH;

            // Estrai array di sprite
            int spritesStart = json.IndexOf("\"sprites\"", StringComparison.OrdinalIgnoreCase);
            if (spritesStart < 0)
                throw new InvalidDataException("Formato file non valido: sezione 'sprites' non trovata.");

            int arrayOpen = json.IndexOf('[', spritesStart);
            int tilesKeywordIdx = json.IndexOf("\"tiles\"", StringComparison.OrdinalIgnoreCase);
            int mapsKeywordIdx = json.IndexOf("\"maps\"", StringComparison.OrdinalIgnoreCase);

            int nextSectionIdx = -1;
            if (tilesKeywordIdx > 0 && mapsKeywordIdx > 0)
                nextSectionIdx = Math.Min(tilesKeywordIdx, mapsKeywordIdx);
            else if (tilesKeywordIdx > 0)
                nextSectionIdx = tilesKeywordIdx;
            else if (mapsKeywordIdx > 0)
                nextSectionIdx = mapsKeywordIdx;

            int arrayClose = nextSectionIdx > 0 ? json.LastIndexOf(']', nextSectionIdx) : json.LastIndexOf(']');
            if (arrayOpen < 0 || arrayClose < arrayOpen)
                throw new InvalidDataException("Formato file non valido: array 'sprites' malformato.");

            string spritesBlock = json.Substring(arrayOpen + 1, arrayClose - arrayOpen - 1);

            // Trova tutti gli oggetti racchiusi tra { e }
            var matches = Regex.Matches(spritesBlock, @"\{[^{}]*\}");

            project.Sprites.Clear();

            foreach (Match m in matches)
            {
                string objStr = m.Value;
                string sName = ExtractString(objStr, "name") ?? $"Sprite_{project.Sprites.Count}";
                string group = ExtractString(objStr, "groupName") ?? "Nemico 1";
                int sW = ExtractInt(objStr, "width", defW);
                int sH = ExtractInt(objStr, "height", defH);
                int ink = ExtractInt(objStr, "inkColor", 7);
                int paper = ExtractInt(objStr, "paperColor", 0);
                bool bright = ExtractBool(objStr, "bright", true);
                bool flash = ExtractBool(objStr, "flash", false);
                string hex = ExtractString(objStr, "dataHex");

                var sprite = new Sprite(sW, sH, sName)
                {
                    GroupName = group,
                    InkColor = ink,
                    PaperColor = paper,
                    Bright = bright,
                    Flash = flash
                };

                if (!string.IsNullOrEmpty(hex))
                {
                    byte[] bytes = HexToByteArray(hex);
                    sprite.SetFromBytesRowByRow(bytes);
                }

                project.Sprites.Add(sprite);
            }

            if (project.Sprites.Count == 0)
            {
                project.AddSprite("Sprite_0");
            }

            project.SelectedIndex = Math.Max(0, Math.Min(project.Sprites.Count - 1, selIdx));

            // Estrai array di tile (Tileset 8x8)
            int selTileIdx = ExtractInt(json, "selectedTileIndex", 0);
            project.TilesPerMap = ExtractInt(json, "tilesPerMap", 8);
            project.Tiles.Clear();
            if (tilesKeywordIdx > 0)
            {
                int tileArrayOpen = json.IndexOf('[', tilesKeywordIdx);
                int tileArrayClose = mapsKeywordIdx > 0 ? json.LastIndexOf(']', mapsKeywordIdx) : json.LastIndexOf(']');
                if (tileArrayOpen > 0 && tileArrayClose > tileArrayOpen)
                {
                    string tilesBlock = json.Substring(tileArrayOpen + 1, tileArrayClose - tileArrayOpen - 1);
                    var tileMatches = Regex.Matches(tilesBlock, @"\{[^{}]*\}");

                    foreach (Match m in tileMatches)
                    {
                        string objStr = m.Value;
                        string tName = ExtractString(objStr, "name") ?? $"Tile_{project.Tiles.Count + 1}";
                        int ink = ExtractInt(objStr, "inkColor", 7);
                        int paper = ExtractInt(objStr, "paperColor", 0);
                        bool bright = ExtractBool(objStr, "bright", true);
                        bool flash = ExtractBool(objStr, "flash", false);
                        string hex = ExtractString(objStr, "dataHex");

                        var tile = new Tile(tName)
                        {
                            InkColor = ink,
                            PaperColor = paper,
                            Bright = bright,
                            Flash = flash
                        };

                        if (!string.IsNullOrEmpty(hex))
                        {
                            tile.SetFromBytesRowByRow(HexToByteArray(hex));
                        }

                        project.Tiles.Add(tile);
                    }
                }
            }

            // Se non ci sono tile nel file (es. file salvato con vecchia versione), inizializza con i preset di default
            if (project.Tiles.Count == 0)
            {
                project.Tiles.Add(new Tile("Muro_Mattoni", TilePresets.BrickWall) { InkColor = 7, PaperColor = 0, Bright = true });
                project.Tiles.Add(new Tile("Piattaforma", TilePresets.Platform) { InkColor = 5, PaperColor = 0, Bright = true });
                project.Tiles.Add(new Tile("Scala", TilePresets.Ladder) { InkColor = 6, PaperColor = 0, Bright = true });
                project.Tiles.Add(new Tile("Blocco_Pietra", TilePresets.StoneBlock) { InkColor = 7, PaperColor = 0, Bright = false });
                project.Tiles.Add(new Tile("Spuntoni", TilePresets.Spikes) { InkColor = 2, PaperColor = 0, Bright = true });
                project.Tiles.Add(new Tile("Gemma", TilePresets.Gem) { InkColor = 6, PaperColor = 0, Bright = true });
            }

            project.SelectedTileIndex = Math.Max(0, Math.Min(project.Tiles.Count - 1, selTileIdx));

            // Estrai array di mappe se presente
            project.Maps.Clear();
            if (mapsKeywordIdx > 0)
            {
                int mapArrayOpen = json.IndexOf('[', mapsKeywordIdx);
                int mapArrayClose = json.LastIndexOf(']');
                if (mapArrayOpen > 0 && mapArrayClose > mapArrayOpen)
                {
                    string mapsBlock = json.Substring(mapArrayOpen + 1, mapArrayClose - mapArrayOpen - 1);
                    var mapMatches = Regex.Matches(mapsBlock, @"\{[^{}]*\}");

                    foreach (Match m in mapMatches)
                    {
                        string objStr = m.Value;
                        string mName = ExtractString(objStr, "name") ?? $"Mappa {project.Maps.Count + 1}";
                        int mW = ExtractInt(objStr, "width", 32);
                        int mH = ExtractInt(objStr, "height", 24);
                        int tSize = ExtractInt(objStr, "tileSize", 8);
                        int paper = ExtractInt(objStr, "defaultPaper", 0);
                        int ink = ExtractInt(objStr, "defaultInk", 7);
                        bool bright = ExtractBool(objStr, "bright", true);
                        string tilesHex = ExtractString(objStr, "tilesHex");

                        var map = new GameMap(mName, mW, mH)
                        {
                            TileWidth = tSize,
                            TileHeight = tSize,
                            DefaultPaper = paper,
                            DefaultInk = ink,
                            Bright = bright
                        };

                        bool isLegacy8Bit = false;
                        if (!string.IsNullOrEmpty(tilesHex))
                        {
                            byte[] mapBytes = HexToByteArray(tilesHex);
                            isLegacy8Bit = (mapBytes.Length < mW * mH * 2);
                            map.FromByteArray(mapBytes, mW, mH);
                        }

                        bool hasAssignedKey = objStr.IndexOf("\"assignedTileIds\"", StringComparison.OrdinalIgnoreCase) >= 0;
                        if (hasAssignedKey)
                        {
                            var assignedIds = ExtractIntList(objStr, "assignedTileIds");
                            map.AssignedTileIds = assignedIds ?? new List<int>();
                        }
                        else
                        {
                            // Fallback per file senza assignedTileIds (es. versioni precedenti):
                            // Popola con le tile utilizzate nella mappa, oppure con le prime N tile del progetto
                            var usedTiles = new List<int>();
                            for (int cy = 0; cy < map.Height; cy++)
                            {
                                for (int cx = 0; cx < map.Width; cx++)
                                {
                                    int t = map.GetTile(cx, cy);
                                    if (t > 0 && !usedTiles.Contains(t))
                                        usedTiles.Add(t);
                                }
                            }
                            int maxAllowed = (project.TilesPerMap > 0) ? project.TilesPerMap : 8;
                            for (int i = 1; i <= project.Tiles.Count && usedTiles.Count < maxAllowed; i++)
                            {
                                if (!usedTiles.Contains(i))
                                    usedTiles.Add(i);
                            }
                            map.AssignedTileIds = (usedTiles.Count > maxAllowed) ? usedTiles.GetRange(0, maxAllowed) : usedTiles;
                        }

                        // Ripara automaticamente tile troncati a 8-bit nei file di salvataggio legacy
                        if (isLegacy8Bit)
                        {
                            map.RepairLegacyTileIds();
                        }

                        project.Maps.Add(map);
                    }
                }
            }

            // Se non ci sono mappe nel file (es. file salvato con vecchia versione), crea mappa predefinita
            if (project.Maps.Count == 0)
            {
                var defMap = project.AddMap("Mappa 1", 32, 24);
                // Bordo di test
                for (int x = 0; x < 32; x++) { defMap.SetTile(x, 0, 1); defMap.SetTile(x, 23, 1); }
                for (int y = 0; y < 24; y++) { defMap.SetTile(0, y, 1); defMap.SetTile(31, y, 1); }
            }

            project.SelectedMapIndex = Math.Max(0, Math.Min(project.Maps.Count - 1, selMapIdx));
            return project;
        }

        private static string EscapeJson(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            return s.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r");
        }

        private static List<int> ExtractIntList(string text, string key)
        {
            var list = new List<int>();
            var match = Regex.Match(text, $"\"{key}\"\\s*:\\s*\\[([^\\]]*)\\]", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string raw = match.Groups[1].Value.Trim();
                if (!string.IsNullOrEmpty(raw))
                {
                    string[] parts = raw.Split(',');
                    foreach (var p in parts)
                    {
                        if (int.TryParse(p.Trim(), out int val))
                        {
                            list.Add(val);
                        }
                    }
                }
            }
            return list;
        }

        private static string ExtractString(string text, string key)
        {
            var match = Regex.Match(text, $"\"{key}\"\\s*:\\s*\"([^\"]*)\"", RegexOptions.IgnoreCase);
            return match.Success ? match.Groups[1].Value : null;
        }

        private static int ExtractInt(string text, string key, int defaultValue)
        {
            var match = Regex.Match(text, $"\"{key}\"\\s*:\\s*(-?\\d+)", RegexOptions.IgnoreCase);
            if (match.Success && int.TryParse(match.Groups[1].Value, out int val))
            {
                return val;
            }
            return defaultValue;
        }

        private static bool ExtractBool(string text, string key, bool defaultValue)
        {
            var match = Regex.Match(text, $"\"{key}\"\\s*:\\s*(true|false)", RegexOptions.IgnoreCase);
            if (match.Success && bool.TryParse(match.Groups[1].Value, out bool val))
            {
                return val;
            }
            return defaultValue;
        }

        private static string ByteArrayToHex(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return string.Empty;
            StringBuilder sb = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        private static byte[] HexToByteArray(string hex)
        {
            if (string.IsNullOrEmpty(hex)) return new byte[0];
            int len = hex.Length / 2;
            byte[] bytes = new byte[len];
            for (int i = 0; i < len; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return bytes;
        }
    }
}
