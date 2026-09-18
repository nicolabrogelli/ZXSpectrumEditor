using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using ZXSpectrumSpriteGenerator.Models;

namespace ZXSpectrumSpriteGenerator.Services
{
    /// <summary>
    /// Servizio per importare sprite da sorgenti Assembly Z80, testo esadecimale/decimale, binari o immagini.
    /// </summary>
    public static class SpriteImporter
    {
        /// <summary>
        /// Esegue il parsing di testo contenente direttive db/defb/DEFB o liste di numeri in vari formati.
        /// </summary>
        public static List<byte> ParseBytesFromText(string text)
        {
            List<byte> result = new List<byte>();
            if (string.IsNullOrWhiteSpace(text)) return result;

            // Rimuovi commenti
            var lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            List<string> cleanLines = new List<string>();

            foreach (var line in lines)
            {
                string cleaned = line;
                int commentIdx = cleaned.IndexOf(';');
                if (commentIdx >= 0) cleaned = cleaned.Substring(0, commentIdx);

                commentIdx = cleaned.IndexOf("//", StringComparison.Ordinal);
                if (commentIdx >= 0) cleaned = cleaned.Substring(0, commentIdx);

                int remIdx = cleaned.IndexOf("REM", StringComparison.OrdinalIgnoreCase);
                if (remIdx >= 0) cleaned = cleaned.Substring(0, remIdx);

                // Rimuovi etichette (es. "sprite_label:")
                cleaned = Regex.Replace(cleaned, @"^[A-Za-z0-9_]+:\s*", "");

                // Rimuovi direttive assembly
                cleaned = Regex.Replace(cleaned, @"\b(db|defb|DEFB|\.byte|BYTE|DATA)\b", " ", RegexOptions.IgnoreCase);

                // Rimuovi numeri di riga basic all'inizio
                cleaned = Regex.Replace(cleaned, @"^\s*\d+\s+", "");

                cleanLines.Add(cleaned);
            }

            string allText = string.Join(" ", cleanLines);

            // Trova tutti i token numerici separati da virgole o spazi
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

        private static bool TryParseByte(string token, out byte value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(token)) return false;

            token = token.Trim();

            try
            {
                // Formato Binario: %01010101 oppure 0b01010101 oppure bin(...), oppure solo 8 bit di 0 e 1 con prefisso
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

                // Formato Esadecimale: $FF oppure 0xFF oppure FFh
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
                if (token.EndsWith("h", StringComparison.OrdinalIgnoreCase))
                {
                    string hex = token.Substring(0, token.Length - 1).Trim();
                    if (byte.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value))
                        return true;
                }

                // Formato Decimale: 0-255
                if (byte.TryParse(token, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
                {
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
        /// Importa sprite da testo Assembly / lista byte suddividendoli secondo larghezza e altezza specificate.
        /// </summary>
        public static List<Sprite> ImportFromText(string text, int width, int height, string baseName = "Sprite")
        {
            List<Sprite> sprites = new List<Sprite>();
            List<byte> bytes = ParseBytesFromText(text);

            int bytesPerSprite = (width / 8) * height;
            if (bytesPerSprite <= 0) return sprites;

            int spriteCount = bytes.Count / bytesPerSprite;
            if (bytes.Count % bytesPerSprite != 0)
            {
                // Include l'ultimo sprite anche se parziale
                spriteCount++;
            }

            for (int s = 0; s < spriteCount; s++)
            {
                int offset = s * bytesPerSprite;
                int count = Math.Min(bytesPerSprite, bytes.Count - offset);
                byte[] spriteData = new byte[bytesPerSprite];
                bytes.CopyTo(offset, spriteData, 0, count);

                var sprite = new Sprite(width, height, $"{baseName}_{s}");
                sprite.SetFromBytesRowByRow(spriteData);
                sprites.Add(sprite);
            }

            return sprites;
        }

        /// <summary>
        /// Importa sprite da un file binario grezzo.
        /// </summary>
        public static List<Sprite> ImportFromBinaryFile(string filePath, int width, int height, string baseName = "Import")
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            List<Sprite> sprites = new List<Sprite>();

            int bytesPerSprite = (width / 8) * height;
            if (bytesPerSprite <= 0) return sprites;

            int spriteCount = bytes.Length / bytesPerSprite;
            for (int s = 0; s < spriteCount; s++)
            {
                int offset = s * bytesPerSprite;
                byte[] spriteData = new byte[bytesPerSprite];
                Array.Copy(bytes, offset, spriteData, 0, bytesPerSprite);

                var sprite = new Sprite(width, height, $"{baseName}_{s}");
                sprite.SetFromBytesRowByRow(spriteData);
                sprites.Add(sprite);
            }

            return sprites;
        }

        /// <summary>
        /// Importa un'immagine bitmap (PNG, BMP, etc.) convertendola in monocromatico 1-bit.
        /// </summary>
        public static Sprite ImportFromImage(string filePath, int targetWidth, int targetHeight, int luminanceThreshold = 128)
        {
            using (Bitmap source = new Bitmap(filePath))
            {
                var sprite = new Sprite(targetWidth, targetHeight, Path.GetFileNameWithoutExtension(filePath));

                // Ridimensiona se necessario
                using (Bitmap resized = new Bitmap(targetWidth, targetHeight))
                {
                    using (Graphics g = Graphics.FromImage(resized))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                        g.DrawImage(source, 0, 0, targetWidth, targetHeight);
                    }

                    for (int y = 0; y < targetHeight; y++)
                    {
                        for (int x = 0; x < targetWidth; x++)
                        {
                            Color c = resized.GetPixel(x, y);
                            // Luminanza percepita
                            int lum = (int)(c.R * 0.299 + c.G * 0.587 + c.B * 0.114);
                            bool isInk = lum < luminanceThreshold; // Sfondo chiaro -> pixel nero è INK
                            sprite.SetPixel(x, y, isInk);
                        }
                    }
                }

                return sprite;
            }
        }

        /// <summary>
        /// Tenta di estrarre indicazioni su colore Ink, Paper e Bright da commenti presenti nel testo.
        /// Riconosce formati come:
        /// ; Ink: 2, Paper: 0
        /// ; Colore Ink: 5, Colore Paper: 1
        /// ; INK 4, PAPER 2
        /// </summary>
        public static bool TryExtractColorsFromText(string text, out int ink, out int paper, out bool? bright, out bool? flash)
        {
            ink = -1;
            paper = -1;
            bright = null;
            flash = null;

            if (string.IsNullOrWhiteSpace(text)) return false;

            var inkMatch = Regex.Match(text, @"\b(?:ink|colore\s*ink)[:=\s]+(\d+)", RegexOptions.IgnoreCase);
            if (inkMatch.Success && int.TryParse(inkMatch.Groups[1].Value, out int ci))
            {
                ink = Math.Max(0, Math.Min(7, ci));
            }

            var paperMatch = Regex.Match(text, @"\b(?:paper|colore\s*paper)[:=\s]+(\d+)", RegexOptions.IgnoreCase);
            if (paperMatch.Success && int.TryParse(paperMatch.Groups[1].Value, out int cp))
            {
                paper = Math.Max(0, Math.Min(7, cp));
            }

            var brightMatch = Regex.Match(text, @"\b(?:bright)[:=\s]+(true|false|1|0)", RegexOptions.IgnoreCase);
            if (brightMatch.Success)
            {
                string bVal = brightMatch.Groups[1].Value.ToLower();
                bright = (bVal == "true" || bVal == "1");
            }

            var flashMatch = Regex.Match(text, @"\b(?:flash)[:=\s]+(true|false|1|0)", RegexOptions.IgnoreCase);
            if (flashMatch.Success)
            {
                string fVal = flashMatch.Groups[1].Value.ToLower();
                flash = (fVal == "true" || fVal == "1");
            }

            return ink >= 0 || paper >= 0 || bright.HasValue || flash.HasValue;
        }

        public static bool TryExtractColorsFromText(string text, out int ink, out int paper, out bool? bright)
        {
            return TryExtractColorsFromText(text, out ink, out paper, out bright, out _);
        }

        /// <summary>
        /// Importa tile 8x8 pixel da sorgente Assembly Z80 o testo, rispettando i colori Ink, Paper, Bright
        /// specificati nei commenti di ciascun tile (es. "; Tile 1: Muro (Ink: 2, Paper: 0)"),
        /// negli attributi ZX a 9 byte o usando i colori predefiniti scelti dall'utente.
        /// </summary>
        public static List<Tile> ImportTilesFromText(
            string text,
            string defaultPrefix = "Tile",
            int defaultInk = 7,
            int defaultPaper = 0,
            bool defaultBright = true,
            bool defaultFlash = false)
        {
            List<Tile> tiles = new List<Tile>();
            if (string.IsNullOrWhiteSpace(text)) return tiles;

            var lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            // Verifica se l'intero testo è formato da blocchi puri di 9 byte (8 byte bitmap + 1 attributo)
            List<byte> allRawBytes = ParseBytesFromText(text);
            bool globalIs9Bytes = allRawBytes.Count > 0 && (allRawBytes.Count % 9 == 0) && (allRawBytes.Count % 8 != 0);

            if (globalIs9Bytes && !Regex.IsMatch(text, @"\b(?:ink|paper)\b", RegexOptions.IgnoreCase))
            {
                int count = allRawBytes.Count / 9;
                for (int i = 0; i < count; i++)
                {
                    byte[] chunk = new byte[8];
                    allRawBytes.CopyTo(i * 9 +1, chunk, 0, 8);
                    byte attr = allRawBytes[i * 9 + 8];
                    int ink = attr & 0x07;
                    int paper = (attr >> 3) & 0x07;
                    bool bright = (attr & 0x40) != 0;
                    bool flash = (attr & 0x80) != 0;

                    var t = new Tile($"{defaultPrefix}{i + 1}", chunk)
                    {
                        InkColor = ink,
                        PaperColor = paper,
                        Bright = bright,
                        Flash = flash
                    };
                    tiles.Add(t);
                }
                return tiles;
            }

            int baseInk = defaultInk;
            int basePaper = defaultPaper;
            bool baseBright = defaultBright;
            bool baseFlash = defaultFlash;

            // Se ci sono commenti globali in cima al testo (prima di qualsiasi riga di dati)
            int currentInk = baseInk;
            int currentPaper = basePaper;
            bool currentBright = baseBright;
            bool currentFlash = baseFlash;
            string currentTileName = null;
            bool hasPerTileColor = false;
            List<byte> currentTileBytes = new List<byte>();
            int tileCounter = 1;

            for (int l = 0; l < lines.Length; l++)
            {
                string line = lines[l];
                string trimmed = line.Trim();
                if (string.IsNullOrWhiteSpace(trimmed)) continue;

                // Estrai commenti prima di rimuoverli
                string comment = null;
                int commentIdx = line.IndexOf(';');
                if (commentIdx >= 0) comment = line.Substring(commentIdx);
                else
                {
                    commentIdx = line.IndexOf("//", StringComparison.Ordinal);
                    if (commentIdx >= 0) comment = line.Substring(commentIdx);
                    else
                    {
                        commentIdx = line.IndexOf("REM", StringComparison.OrdinalIgnoreCase);
                        if (commentIdx >= 0) comment = line.Substring(commentIdx);
                    }
                }

                if (!string.IsNullOrEmpty(comment))
                {
                    bool isPerTileComment = Regex.IsMatch(comment, @"\bTile\s*\d*[:\s]", RegexOptions.IgnoreCase);

                    var inkMatch = Regex.Match(comment, @"\b(?:ink|colore\s*ink)[:=\s]+(\d+)", RegexOptions.IgnoreCase);
                    if (inkMatch.Success && int.TryParse(inkMatch.Groups[1].Value, out int ci))
                    {
                        currentInk = Math.Max(0, Math.Min(7, ci));
                        if (isPerTileComment) hasPerTileColor = true;
                        else baseInk = currentInk;
                    }

                    var paperMatch = Regex.Match(comment, @"\b(?:paper|colore\s*paper)[:=\s]+(\d+)", RegexOptions.IgnoreCase);
                    if (paperMatch.Success && int.TryParse(paperMatch.Groups[1].Value, out int cp))
                    {
                        currentPaper = Math.Max(0, Math.Min(7, cp));
                        if (isPerTileComment) hasPerTileColor = true;
                        else basePaper = currentPaper;
                    }

                    var brightMatch = Regex.Match(comment, @"\b(?:bright)[:=\s]+(true|false|1|0)", RegexOptions.IgnoreCase);
                    if (brightMatch.Success)
                    {
                        string bVal = brightMatch.Groups[1].Value.ToLower();
                        currentBright = (bVal == "true" || bVal == "1");
                        if (!isPerTileComment) baseBright = currentBright;
                    }

                    var flashMatch = Regex.Match(comment, @"\b(?:flash)[:=\s]+(true|false|1|0)", RegexOptions.IgnoreCase);
                    if (flashMatch.Success)
                    {
                        string fVal = flashMatch.Groups[1].Value.ToLower();
                        currentFlash = (fVal == "true" || fVal == "1");
                        if (!isPerTileComment) baseFlash = currentFlash;
                    }

                    // Cerca eventuale nome nel commento: es. "; Tile 1: Muro (Ink: 2...)"
                    var nameMatch = Regex.Match(comment, @";\s*Tile\s*\d*[:\s]+([A-Za-z0-9_ ]+?)(?:\s*\(|$)", RegexOptions.IgnoreCase);
                    if (nameMatch.Success)
                    {
                        string n = nameMatch.Groups[1].Value.Trim();
                        if (!string.IsNullOrEmpty(n) && !n.StartsWith("="))
                        {
                            currentTileName = n;
                        }
                    }
                }

                // Estrai eventuale etichetta (es. "tile_1_muro:" o "muro:")
                var labelMatch = Regex.Match(line, @"^\s*([A-Za-z0-9_]+):");
                if (labelMatch.Success)
                {
                    string label = labelMatch.Groups[1].Value;
                    if (string.IsNullOrEmpty(currentTileName) && !label.Equals("tileset", StringComparison.OrdinalIgnoreCase))
                    {
                        currentTileName = label;
                    }
                }

                // Rimuovi commenti, etichette e direttive per ottenere solo i byte di questa riga
                string cleaned = line;
                if (commentIdx >= 0) cleaned = cleaned.Substring(0, commentIdx);
                cleaned = Regex.Replace(cleaned, @"^[A-Za-z0-9_]+:\s*", "");
                cleaned = Regex.Replace(cleaned, @"\b(db|defb|DEFB|\.byte|BYTE|DATA)\b", " ", RegexOptions.IgnoreCase);
                cleaned = Regex.Replace(cleaned, @"^\s*\d+\s+", "");

                string[] tokens = cleaned.Split(new[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var token in tokens)
                {
                    if (TryParseByte(token, out byte b))
                    {
                        currentTileBytes.Add(b);
                    }
                }

                // Se abbiamo accumulato almeno 8 byte per un tile
                while (currentTileBytes.Count >= 8)
                {
                    int ink = currentInk;
                    int paper = currentPaper;
                    bool bright = currentBright;
                    bool flash = currentFlash;

                    // Se ci sono esattamente 9 byte accumulati per questa riga/definizione (es. 8 byte bitmap + 1 attributo)
                    if (currentTileBytes.Count == 9 && !hasPerTileColor)
                    {
                        byte attr = currentTileBytes[8];
                        ink = attr & 0x07;
                        paper = (attr >> 3) & 0x07;
                        bright = (attr & 0x40) != 0;
                        flash = (attr & 0x80) != 0;
                        currentTileBytes.RemoveAt(8);
                    }

                    byte[] tileData = new byte[8];
                    for (int i = 0; i < 8; i++) tileData[i] = currentTileBytes[i];
                    currentTileBytes.RemoveRange(0, 8);

                    string name = !string.IsNullOrWhiteSpace(currentTileName)
                        ? currentTileName
                        : $"{defaultPrefix}{tileCounter}";

                    var tile = new Tile(name, tileData)
                    {
                        InkColor = ink,
                        PaperColor = paper,
                        Bright = bright,
                        Flash = flash
                    };
                    tiles.Add(tile);
                    tileCounter++;

                    // Se abbiamo consumato tutti i byte correnti, prepariamo lo stato per il prossimo tile
                    if (currentTileBytes.Count == 0)
                    {
                        currentTileName = null;
                        if (hasPerTileColor)
                        {
                            currentInk = baseInk;
                            currentPaper = basePaper;
                            currentBright = baseBright;
                            currentFlash = baseFlash;
                            hasPerTileColor = false;
                        }
                    }
                }
            }

            // Dati parziali residui alla fine (es. meno di 8 byte rimasti)
            if (currentTileBytes.Count > 0)
            {
                byte[] tileData = new byte[8];
                for (int i = 0; i < currentTileBytes.Count && i < 8; i++)
                    tileData[i] = currentTileBytes[i];

                string name = !string.IsNullOrWhiteSpace(currentTileName)
                    ? currentTileName
                    : $"{defaultPrefix}{tileCounter}";

                var tile = new Tile(name, tileData)
                {
                    InkColor = currentInk,
                    PaperColor = currentPaper,
                    Bright = currentBright,
                    Flash = currentFlash
                };
                tiles.Add(tile);
            }

            return tiles;
        }
    }
}
