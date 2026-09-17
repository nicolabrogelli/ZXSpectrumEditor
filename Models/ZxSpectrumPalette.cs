using System;
using System.Drawing;

namespace ZXSpectrumSpriteGenerator.Models
{
    /// <summary>
    /// Palette ufficiale del Sinclair ZX Spectrum.
    /// Comprende 8 colori base a intensità normale (0-7) e 8 a intensità luminosa (Bright).
    /// </summary>
    public static class ZxSpectrumPalette
    {
        public static readonly string[] ColorNames = new string[]
        {
            "Black", "Blue", "Red", "Magenta", "Green", "Cyan", "Yellow", "White"
        };

        // Colori normali (circa 84% di intensità, 0xD7 = 215)
        public static readonly Color[] NormalColors = new Color[]
        {
            Color.FromArgb(0, 0, 0),       // 0: Black
            Color.FromArgb(0, 0, 205),     // 1: Blue
            Color.FromArgb(205, 0, 0),     // 2: Red
            Color.FromArgb(205, 0, 205),   // 3: Magenta
            Color.FromArgb(0, 205, 0),     // 4: Green
            Color.FromArgb(0, 205, 205),   // 5: Cyan
            Color.FromArgb(205, 205, 0),   // 6: Yellow
            Color.FromArgb(205, 205, 205)  // 7: White
        };

        // Colori bright (100% di intensità, 0xFF = 255)
        public static readonly Color[] BrightColors = new Color[]
        {
            Color.FromArgb(0, 0, 0),       // 0: Black
            Color.FromArgb(0, 0, 255),     // 1: Bright Blue
            Color.FromArgb(255, 0, 0),     // 2: Bright Red
            Color.FromArgb(255, 0, 255),   // 3: Bright Magenta
            Color.FromArgb(0, 255, 0),     // 4: Bright Green
            Color.FromArgb(0, 255, 255),   // 5: Bright Cyan
            Color.FromArgb(255, 255, 0),   // 6: Bright Yellow
            Color.FromArgb(255, 255, 255)  // 7: Bright White
        };

        public static Color GetColor(int colorIndex, bool bright)
        {
            int idx = Math.Max(0, Math.Min(7, colorIndex));
            return bright ? BrightColors[idx] : NormalColors[idx];
        }

        public static byte MakeAttribute(int ink, int paper, bool bright = true, bool flash = false)
        {
            int attr = (ink & 0x07) | ((paper & 0x07) << 3);
            if (bright) attr |= 0x40;
            if (flash) attr |= 0x80;
            return (byte)attr;
        }

        public static void DecodeAttribute(byte attr, out int ink, out int paper, out bool bright, out bool flash)
        {
            ink = attr & 0x07;
            paper = (attr >> 3) & 0x07;
            bright = (attr & 0x40) != 0;
            flash = (attr & 0x80) != 0;
        }
    }
}
