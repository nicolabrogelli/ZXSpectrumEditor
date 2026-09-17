using System;

namespace ZXSpectrumSpriteGenerator.Models
{
    /// <summary>
    /// Rappresenta un singolo tile grafico monocromatico 8x8 pixel per Sinclair ZX Spectrum.
    /// Ogni tile occupa esattamente 8 byte di memoria dati (1 byte per riga, 8 righe).
    /// </summary>
    public class Tile : Sprite
    {
        public const int TileSize = 8;

        public Tile(string name = "Tile") : base(TileSize, TileSize, name)
        {
            GroupName = "Tileset";
        }

        public Tile(string name, byte[] data) : base(TileSize, TileSize, name)
        {
            GroupName = "Tileset";
            if (data != null && data.Length > 0)
            {
                SetFromBytesRowByRow(data);
            }
        }

        public new Tile Clone()
        {
            var clone = new Tile(Name)
            {
                GroupName = GroupName,
                InkColor = InkColor,
                PaperColor = PaperColor,
                Bright = Bright
            };

            for (int y = 0; y < TileSize; y++)
            {
                for (int x = 0; x < TileSize; x++)
                {
                    clone.SetPixel(x, y, this.GetPixel(x, y));
                }
            }

            return clone;
        }
    }
}
