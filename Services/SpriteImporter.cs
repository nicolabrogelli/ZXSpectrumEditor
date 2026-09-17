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
    }
}
