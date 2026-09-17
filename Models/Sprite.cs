using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ZXSpectrumSpriteGenerator.Models
{
    /// <summary>
    /// Rappresenta un singolo sprite monocromatico 1-bit per ZX Spectrum.
    /// Supporta dimensioni multiple di 8 (8x8, 16x16, 24x24, etc.).
    /// </summary>
    public class Sprite
    {
        public string Name { get; set; }
        public string GroupName { get; set; } = "Nemico 1";
        public int Width { get; private set; }
        public int Height { get; private set; }
        private bool[,] _pixels;

        // Attributi colore ZX Spectrum (per preview/rendering)
        public int InkColor { get; set; } = 7;     // White
        public int PaperColor { get; set; } = 0;   // Black
        public bool Bright { get; set; } = true;

        public Sprite(int width = 16, int height = 16, string name = "Sprite")
        {
            if (width <= 0 || width % 8 != 0)
                throw new ArgumentException("La larghezza deve essere un multiplo di 8 pixel.", nameof(width));
            if (height <= 0)
                throw new ArgumentException("L'altezza deve essere maggiore di 0.", nameof(height));

            Width = width;
            Height = height;
            Name = name;
            _pixels = new bool[Width, Height];
        }

        public bool GetPixel(int x, int y)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
                return _pixels[x, y];
            return false;
        }

        public void SetPixel(int x, int y, bool value)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                _pixels[x, y] = value;
            }
        }

        public void Clear()
        {
            _pixels = new bool[Width, Height];
        }

        public void Invert()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    _pixels[x, y] = !_pixels[x, y];
                }
            }
        }

        public void FlipHorizontal()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width / 2; x++)
                {
                    int oppositeX = Width - 1 - x;
                    bool temp = _pixels[x, y];
                    _pixels[x, y] = _pixels[oppositeX, y];
                    _pixels[oppositeX, y] = temp;
                }
            }
        }

        public void FlipVertical()
        {
            for (int y = 0; y < Height / 2; y++)
            {
                int oppositeY = Height - 1 - y;
                for (int x = 0; x < Width; x++)
                {
                    bool temp = _pixels[x, y];
                    _pixels[x, y] = _pixels[x, oppositeY];
                    _pixels[x, oppositeY] = temp;
                }
            }
        }

        public void Rotate90(bool clockwise = true)
        {
            // Solo se è quadrato (es. 16x16, 24x24, 8x8)
            if (Width != Height) return;

            bool[,] newPixels = new bool[Width, Height];
            int n = Width;

            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    if (clockwise)
                    {
                        newPixels[n - 1 - y, x] = _pixels[x, y];
                    }
                    else
                    {
                        newPixels[y, n - 1 - x] = _pixels[x, y];
                    }
                }
            }
            _pixels = newPixels;
        }

        public void Shift(int dx, int dy, bool wrap = false)
        {
            bool[,] newPixels = new bool[Width, Height];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int srcX = x - dx;
                    int srcY = y - dy;

                    if (wrap)
                    {
                        srcX = (srcX % Width + Width) % Width;
                        srcY = (srcY % Height + Height) % Height;
                        newPixels[x, y] = _pixels[srcX, srcY];
                    }
                    else
                    {
                        if (srcX >= 0 && srcX < Width && srcY >= 0 && srcY < Height)
                        {
                            newPixels[x, y] = _pixels[srcX, srcY];
                        }
                    }
                }
            }

            _pixels = newPixels;
        }

        public void Resize(int newWidth, int newHeight)
        {
            if (newWidth <= 0 || newWidth % 8 != 0)
                throw new ArgumentException("La larghezza deve essere un multiplo di 8 pixel.", nameof(newWidth));
            if (newHeight <= 0)
                throw new ArgumentException("L'altezza deve essere maggiore di 0.", nameof(newHeight));

            bool[,] newPixels = new bool[newWidth, newHeight];
            int copyW = Math.Min(Width, newWidth);
            int copyH = Math.Min(Height, newHeight);

            for (int y = 0; y < copyH; y++)
            {
                for (int x = 0; x < copyW; x++)
                {
                    newPixels[x, y] = _pixels[x, y];
                }
            }

            Width = newWidth;
            Height = newHeight;
            _pixels = newPixels;
        }

        /// <summary>
        /// Esporta i byte riga per riga (formato standard per sprite software su ZX Spectrum).
        /// Per ogni riga, i byte vanno da sinistra a destra (8 pixel per byte, MSB a sinistra).
        /// </summary>
        public byte[] GetBytesRowByRow()
        {
            int bytesPerRow = Width / 8;
            byte[] data = new byte[bytesPerRow * Height];
            int index = 0;

            for (int y = 0; y < Height; y++)
            {
                for (int b = 0; b < bytesPerRow; b++)
                {
                    byte val = 0;
                    for (int bit = 0; bit < 8; bit++)
                    {
                        int x = b * 8 + bit;
                        if (_pixels[x, y])
                        {
                            val |= (byte)(1 << (7 - bit));
                        }
                    }
                    data[index++] = val;
                }
            }

            return data;
        }

        /// <summary>
        /// Popola i pixel dai byte organizzati riga per riga.
        /// </summary>
        public void SetFromBytesRowByRow(byte[] data)
        {
            int bytesPerRow = Width / 8;
            int totalExpected = bytesPerRow * Height;
            int count = Math.Min(data.Length, totalExpected);

            Clear();

            int index = 0;
            for (int y = 0; y < Height && index < count; y++)
            {
                for (int b = 0; b < bytesPerRow && index < count; b++)
                {
                    byte val = data[index++];
                    for (int bit = 0; bit < 8; bit++)
                    {
                        int x = b * 8 + bit;
                        bool isSet = (val & (1 << (7 - bit))) != 0;
                        _pixels[x, y] = isSet;
                    }
                }
            }
        }

        /// <summary>
        /// Genera una maschera per lo sprite: 0xFF per pixel vuoti, 0x00 per pixel pieni (per istruzioni AND del Z80)
        /// oppure invertita (1 per pieni, 0 per vuoti per istruzioni OR).
        /// </summary>
        public byte[] GenerateMaskBytes(bool forAndOperation = true)
        {
            int bytesPerRow = Width / 8;
            byte[] mask = new byte[bytesPerRow * Height];
            int index = 0;

            for (int y = 0; y < Height; y++)
            {
                for (int b = 0; b < bytesPerRow; b++)
                {
                    byte val = 0;
                    for (int bit = 0; bit < 8; bit++)
                    {
                        int x = b * 8 + bit;
                        bool pixel = _pixels[x, y];
                        // Per AND: sfondo = 1, sprite = 0 (quindi azzera i pixel dello sprite sullo schermo)
                        // Per OR: sfondo = 0, sprite = 1
                        bool maskBit = forAndOperation ? !pixel : pixel;
                        if (maskBit)
                        {
                            val |= (byte)(1 << (7 - bit));
                        }
                    }
                    mask[index++] = val;
                }
            }

            return mask;
        }

        public Sprite Clone()
        {
            Sprite clone = new Sprite(Width, Height, Name)
            {
                GroupName = GroupName,
                InkColor = InkColor,
                PaperColor = PaperColor,
                Bright = Bright
            };

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    clone._pixels[x, y] = _pixels[x, y];
                }
            }

            return clone;
        }

        /// <summary>
        /// Esegue il rendering dello sprite su una Bitmap con fattore di scala.
        /// </summary>
        public Bitmap RenderToBitmap(int scale = 1, Color? customInk = null, Color? customPaper = null)
        {
            if (scale < 1) scale = 1;
            Bitmap bmp = new Bitmap(Width * scale, Height * scale);

            Color ink = customInk ?? ZxSpectrumPalette.GetColor(InkColor, Bright);
            Color paper = customPaper ?? ZxSpectrumPalette.GetColor(PaperColor, Bright);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.InterpolationMode = InterpolationMode.NearestNeighbor;

                using (SolidBrush inkBrush = new SolidBrush(ink))
                using (SolidBrush paperBrush = new SolidBrush(paper))
                {
                    g.FillRectangle(paperBrush, 0, 0, bmp.Width, bmp.Height);

                    for (int y = 0; y < Height; y++)
                    {
                        for (int x = 0; x < Width; x++)
                        {
                            if (_pixels[x, y])
                            {
                                g.FillRectangle(inkBrush, x * scale, y * scale, scale, scale);
                            }
                        }
                    }
                }
            }

            return bmp;
        }
    }
}
