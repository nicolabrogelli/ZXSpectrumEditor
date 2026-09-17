using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ZXSpectrumSpriteGenerator.Models;

namespace ZXSpectrumSpriteGenerator.UI.Controls
{
    public enum DrawingTool
    {
        Pencil,
        Line,
        RectangleOutline,
        RectangleFilled,
        CircleOutline,
        CircleFilled,
        FloodFill
    }

    public class HoverCoordinateEventArgs : EventArgs
    {
        public int PixelX { get; set; }
        public int PixelY { get; set; }
        public int CharX { get; set; }
        public int CharY { get; set; }
    }

    /// <summary>
    /// Controllo per il disegno interattivo dello sprite con griglia, supporto strumenti e visualizzazione delle celle 8x8 ZX Spectrum.
    /// </summary>
    public class SpriteCanvas : Control
    {
        private Sprite _sprite;
        private DrawingTool _tool = DrawingTool.Pencil;
        private bool _showPixelGrid = true;
        private bool _showCharGrid = true;
        private int _cellSize = 24; // Dimensione in pixel di ciascun pixel dello sprite

        // Dragging & Tool preview
        private bool _isMouseDown = false;
        private MouseButtons _mouseButton = MouseButtons.None;
        private Point _dragStartPoint = Point.Empty;
        private Point _currentMousePixel = new Point(-1, -1);

        // Undo / Redo
        private readonly Stack<bool[,]> _undoStack = new Stack<bool[,]>();
        private readonly Stack<bool[,]> _redoStack = new Stack<bool[,]>();

        public event EventHandler<HoverCoordinateEventArgs> HoverCoordinatesChanged;
        public event EventHandler SpriteChanged;

        public SpriteCanvas()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            BackColor = Color.FromArgb(32, 32, 38);
            Cursor = Cursors.Cross;
        }

        public Sprite CurrentSprite
        {
            get => _sprite;
            set
            {
                if (_sprite != value)
                {
                    _sprite = value;
                    _undoStack.Clear();
                    _redoStack.Clear();
                    UpdateSize();
                    Invalidate();
                }
            }
        }

        public DrawingTool CurrentTool
        {
            get => _tool;
            set => _tool = value;
        }

        public bool ShowPixelGrid
        {
            get => _showPixelGrid;
            set { _showPixelGrid = value; Invalidate(); }
        }

        public bool ShowCharGrid
        {
            get => _showCharGrid;
            set { _showCharGrid = value; Invalidate(); }
        }

        public int CellSize
        {
            get => _cellSize;
            set
            {
                _cellSize = Math.Max(8, Math.Min(64, value));
                UpdateSize();
                Invalidate();
            }
        }

        public void UpdateSize()
        {
            if (_sprite == null)
            {
                Size = new Size(256, 256);
            }
            else
            {
                Size = new Size(_sprite.Width * _cellSize + 1, _sprite.Height * _cellSize + 1);
            }
        }

        public bool CanUndo => _undoStack.Count > 0;
        public bool CanRedo => _redoStack.Count > 0;

        public void PushUndoState()
        {
            if (_sprite == null) return;
            bool[,] state = new bool[_sprite.Width, _sprite.Height];
            for (int y = 0; y < _sprite.Height; y++)
            {
                for (int x = 0; x < _sprite.Width; x++)
                {
                    state[x, y] = _sprite.GetPixel(x, y);
                }
            }
            _undoStack.Push(state);
            _redoStack.Clear();
        }

        public void Undo()
        {
            if (_undoStack.Count == 0 || _sprite == null) return;

            // Salva stato attuale in redo
            bool[,] currentState = new bool[_sprite.Width, _sprite.Height];
            for (int y = 0; y < _sprite.Height; y++)
            {
                for (int x = 0; x < _sprite.Width; x++)
                {
                    currentState[x, y] = _sprite.GetPixel(x, y);
                }
            }
            _redoStack.Push(currentState);

            bool[,] prevState = _undoStack.Pop();
            for (int y = 0; y < _sprite.Height; y++)
            {
                for (int x = 0; x < _sprite.Width; x++)
                {
                    _sprite.SetPixel(x, y, prevState[x, y]);
                }
            }

            Invalidate();
            SpriteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Redo()
        {
            if (_redoStack.Count == 0 || _sprite == null) return;

            bool[,] currentState = new bool[_sprite.Width, _sprite.Height];
            for (int y = 0; y < _sprite.Height; y++)
            {
                for (int x = 0; x < _sprite.Width; x++)
                {
                    currentState[x, y] = _sprite.GetPixel(x, y);
                }
            }
            _undoStack.Push(currentState);

            bool[,] nextState = _redoStack.Pop();
            for (int y = 0; y < _sprite.Height; y++)
            {
                for (int x = 0; x < _sprite.Width; x++)
                {
                    _sprite.SetPixel(x, y, nextState[x, y]);
                }
            }

            Invalidate();
            SpriteChanged?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.None;
            g.PixelOffsetMode = PixelOffsetMode.Half;

            if (_sprite == null)
            {
                using (var brush = new SolidBrush(Color.Gray))
                {
                    g.DrawString("Nessuno sprite selezionato", Font, brush, 10, 10);
                }
                return;
            }

            Color inkColor = ZxSpectrumPalette.GetColor(_sprite.InkColor, _sprite.Bright);
            Color paperColor = ZxSpectrumPalette.GetColor(_sprite.PaperColor, _sprite.Bright);

            using (SolidBrush inkBrush = new SolidBrush(inkColor))
            using (SolidBrush paperBrush = new SolidBrush(paperColor))
            {
                // Disegna i pixel
                for (int y = 0; y < _sprite.Height; y++)
                {
                    for (int x = 0; x < _sprite.Width; x++)
                    {
                        Rectangle rect = new Rectangle(x * _cellSize, y * _cellSize, _cellSize, _cellSize);
                        bool pixel = _sprite.GetPixel(x, y);
                        g.FillRectangle(pixel ? inkBrush : paperBrush, rect);
                    }
                }

                // Disegna anteprima dello strumento se in drag
                if (_isMouseDown && _tool != DrawingTool.Pencil && _tool != DrawingTool.FloodFill)
                {
                    DrawToolPreview(g, inkColor, paperColor);
                }
            }

            // Griglia dei pixel (sottile grigia)
            if (_showPixelGrid && _cellSize >= 10)
            {
                using (Pen gridPen = new Pen(Color.FromArgb(50, 128, 128, 128)))
                {
                    for (int x = 0; x <= _sprite.Width; x++)
                    {
                        g.DrawLine(gridPen, x * _cellSize, 0, x * _cellSize, _sprite.Height * _cellSize);
                    }
                    for (int y = 0; y <= _sprite.Height; y++)
                    {
                        g.DrawLine(gridPen, 0, y * _cellSize, _sprite.Width * _cellSize, y * _cellSize);
                    }
                }
            }

            // Griglia delle celle di carattere ZX Spectrum 8x8 (linee spesse/colorate)
            if (_showCharGrid)
            {
                using (Pen charPen = new Pen(Color.FromArgb(180, 0, 200, 255), 2f))
                {
                    charPen.DashStyle = DashStyle.Dash;
                    for (int x = 0; x <= _sprite.Width; x += 8)
                    {
                        g.DrawLine(charPen, x * _cellSize, 0, x * _cellSize, _sprite.Height * _cellSize);
                    }
                    for (int y = 0; y <= _sprite.Height; y += 8)
                    {
                        g.DrawLine(charPen, 0, y * _cellSize, _sprite.Width * _cellSize, y * _cellSize);
                    }
                }
            }

            // Bordo esterno
            using (Pen borderPen = new Pen(Color.FromArgb(80, 80, 100), 2f))
            {
                g.DrawRectangle(borderPen, 0, 0, _sprite.Width * _cellSize, _sprite.Height * _cellSize);
            }
        }

        private void DrawToolPreview(Graphics g, Color ink, Color paper)
        {
            bool isInk = _mouseButton == MouseButtons.Left;
            Color previewColor = isInk ? ink : paper;

            using (Brush b = new SolidBrush(Color.FromArgb(180, previewColor)))
            {
                List<Point> previewPixels = GetShapePixels(_dragStartPoint, _currentMousePixel, _tool);
                foreach (var p in previewPixels)
                {
                    if (p.X >= 0 && p.X < _sprite.Width && p.Y >= 0 && p.Y < _sprite.Height)
                    {
                        g.FillRectangle(b, p.X * _cellSize, p.Y * _cellSize, _cellSize, _cellSize);
                    }
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (_sprite == null) return;

            Point p = PointToPixel(e.Location);
            if (!IsPixelValid(p)) return;

            _isMouseDown = true;
            _mouseButton = e.Button;
            _dragStartPoint = p;
            _currentMousePixel = p;

            PushUndoState();

            if (_tool == DrawingTool.Pencil)
            {
                bool value = _mouseButton == MouseButtons.Left;
                _sprite.SetPixel(p.X, p.Y, value);
                Invalidate();
                SpriteChanged?.Invoke(this, EventArgs.Empty);
            }
            else if (_tool == DrawingTool.FloodFill)
            {
                bool targetVal = _sprite.GetPixel(p.X, p.Y);
                bool fillVal = _mouseButton == MouseButtons.Left;
                if (targetVal != fillVal)
                {
                    FloodFill(_sprite, p.X, p.Y, targetVal, fillVal);
                    Invalidate();
                    SpriteChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Point p = PointToPixel(e.Location);

            if (p != _currentMousePixel)
            {
                _currentMousePixel = p;
                if (IsPixelValid(p))
                {
                    HoverCoordinatesChanged?.Invoke(this, new HoverCoordinateEventArgs
                    {
                        PixelX = p.X,
                        PixelY = p.Y,
                        CharX = p.X / 8,
                        CharY = p.Y / 8
                    });
                }
            }

            if (_isMouseDown && _sprite != null)
            {
                if (_tool == DrawingTool.Pencil)
                {
                    if (IsPixelValid(p))
                    {
                        bool value = _mouseButton == MouseButtons.Left;
                        if (_sprite.GetPixel(p.X, p.Y) != value)
                        {
                            _sprite.SetPixel(p.X, p.Y, value);
                            Invalidate();
                            SpriteChanged?.Invoke(this, EventArgs.Empty);
                        }
                    }
                }
                else
                {
                    // Aggiorna preview per Line, Rect, Circle
                    Invalidate();
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (!_isMouseDown || _sprite == null) return;

            Point p = PointToPixel(e.Location);
            bool value = _mouseButton == MouseButtons.Left;

            if (_tool != DrawingTool.Pencil && _tool != DrawingTool.FloodFill)
            {
                List<Point> pixels = GetShapePixels(_dragStartPoint, p, _tool);
                foreach (var pt in pixels)
                {
                    _sprite.SetPixel(pt.X, pt.Y, value);
                }
                SpriteChanged?.Invoke(this, EventArgs.Empty);
            }

            _isMouseDown = false;
            _mouseButton = MouseButtons.None;
            Invalidate();
        }

        private Point PointToPixel(Point pt)
        {
            return new Point(pt.X / _cellSize, pt.Y / _cellSize);
        }

        private bool IsPixelValid(Point p)
        {
            if (_sprite == null) return false;
            return p.X >= 0 && p.X < _sprite.Width && p.Y >= 0 && p.Y < _sprite.Height;
        }

        private List<Point> GetShapePixels(Point p0, Point p1, DrawingTool tool)
        {
            List<Point> result = new List<Point>();

            switch (tool)
            {
                case DrawingTool.Line:
                    return GetBresenhamLine(p0.X, p0.Y, p1.X, p1.Y);

                case DrawingTool.RectangleOutline:
                    int minX = Math.Min(p0.X, p1.X);
                    int maxX = Math.Max(p0.X, p1.X);
                    int minY = Math.Min(p0.Y, p1.Y);
                    int maxY = Math.Max(p0.Y, p1.Y);
                    for (int x = minX; x <= maxX; x++)
                    {
                        result.Add(new Point(x, minY));
                        result.Add(new Point(x, maxY));
                    }
                    for (int y = minY + 1; y < maxY; y++)
                    {
                        result.Add(new Point(minX, y));
                        result.Add(new Point(maxX, y));
                    }
                    break;

                case DrawingTool.RectangleFilled:
                    int fMinX = Math.Min(p0.X, p1.X);
                    int fMaxX = Math.Max(p0.X, p1.X);
                    int fMinY = Math.Min(p0.Y, p1.Y);
                    int fMaxY = Math.Max(p0.Y, p1.Y);
                    for (int y = fMinY; y <= fMaxY; y++)
                    {
                        for (int x = fMinX; x <= fMaxX; x++)
                        {
                            result.Add(new Point(x, y));
                        }
                    }
                    break;

                case DrawingTool.CircleOutline:
                    return GetCirclePixels(p0, p1, filled: false);

                case DrawingTool.CircleFilled:
                    return GetCirclePixels(p0, p1, filled: true);
            }

            return result;
        }

        private List<Point> GetBresenhamLine(int x0, int y0, int x1, int y1)
        {
            List<Point> line = new List<Point>();
            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                line.Add(new Point(x0, y0));
                if (x0 == x1 && y0 == y1) break;
                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }

            return line;
        }

        private List<Point> GetCirclePixels(Point p0, Point p1, bool filled)
        {
            List<Point> pts = new List<Point>();
            int r = (int)Math.Sqrt(Math.Pow(p1.X - p0.X, 2) + Math.Pow(p1.Y - p0.Y, 2));
            int cx = p0.X;
            int cy = p0.Y;

            if (filled)
            {
                for (int dy = -r; dy <= r; dy++)
                {
                    for (int dx = -r; dx <= r; dx++)
                    {
                        if (dx * dx + dy * dy <= r * r)
                        {
                            pts.Add(new Point(cx + dx, cy + dy));
                        }
                    }
                }
            }
            else
            {
                int x = 0;
                int y = r;
                int d = 3 - 2 * r;

                void AddSym(int px, int py)
                {
                    pts.Add(new Point(cx + px, cy + py));
                    pts.Add(new Point(cx - px, cy + py));
                    pts.Add(new Point(cx + px, cy - py));
                    pts.Add(new Point(cx - px, cy - py));
                    pts.Add(new Point(cx + py, cy + px));
                    pts.Add(new Point(cx - py, cy + px));
                    pts.Add(new Point(cx + py, cy - px));
                    pts.Add(new Point(cx - py, cy - px));
                }

                AddSym(x, y);
                while (y >= x)
                {
                    x++;
                    if (d > 0)
                    {
                        y--;
                        d = d + 4 * (x - y) + 10;
                    }
                    else
                    {
                        d = d + 4 * x + 6;
                    }
                    AddSym(x, y);
                }
            }

            return pts;
        }

        private void FloodFill(Sprite sprite, int startX, int startY, bool targetVal, bool fillVal)
        {
            Queue<Point> queue = new Queue<Point>();
            queue.Enqueue(new Point(startX, startY));
            bool[,] visited = new bool[sprite.Width, sprite.Height];

            while (queue.Count > 0)
            {
                Point pt = queue.Dequeue();
                int x = pt.X;
                int y = pt.Y;

                if (x < 0 || x >= sprite.Width || y < 0 || y >= sprite.Height) continue;
                if (visited[x, y]) continue;
                visited[x, y] = true;

                if (sprite.GetPixel(x, y) == targetVal)
                {
                    sprite.SetPixel(x, y, fillVal);

                    queue.Enqueue(new Point(x + 1, y));
                    queue.Enqueue(new Point(x - 1, y));
                    queue.Enqueue(new Point(x, y + 1));
                    queue.Enqueue(new Point(x, y - 1));
                }
            }
        }
    }
}
