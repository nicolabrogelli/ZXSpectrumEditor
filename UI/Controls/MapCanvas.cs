using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ZXSpectrumSpriteGenerator.Models;

namespace ZXSpectrumSpriteGenerator.UI.Controls
{
    public enum MapDrawingTool
    {
        Pencil,
        Eraser,
        FloodFill,
        RectangleOutline,
        RectangleFilled,
        PickTile
    }

    public class MapHoverEventArgs : EventArgs
    {
        public int TileX { get; set; }
        public int TileY { get; set; }
        public int PixelX { get; set; }
        public int PixelY { get; set; }
        public int TileId { get; set; }
    }

    /// <summary>
    /// Canvas interattivo per il disegno e la modifica di mappe di gioco (tilemap) Sinclair ZX Spectrum.
    /// Supporta scala zoom (1x, 2x, 3x, 4x), griglia, strumenti Matita, Gomma, Secchiello, Rettangoli e Pipetta.
    /// </summary>
    public class MapCanvas : Control
    {
        private SpriteProject _project;
        private GameMap _map;
        private int _scale = 2;
        private bool _showGrid = true;
        private int _activeTileId = 1;
        private MapDrawingTool _currentTool = MapDrawingTool.Pencil;

        // Disegno e trascinamento forme
        private bool _isDrawing = false;
        private bool _isDraggingShape = false;
        private Point _shapeStartTile = Point.Empty;
        private Point _currentHoverTile = new Point(-1, -1);

        public event EventHandler MapChanged;
        public event EventHandler<MapHoverEventArgs> HoverCoordinatesChanged;
        public event EventHandler<int> TilePicked;

        public MapCanvas()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.Selectable, true);

            BackColor = Color.FromArgb(15, 15, 18);
            Cursor = Cursors.Cross;
            UpdateControlSize();
        }

        public SpriteProject Project
        {
            get => _project;
            set
            {
                _project = value;
                Invalidate();
            }
        }

        public GameMap Map
        {
            get => _map;
            set
            {
                _map = value;
                UpdateControlSize();
                Invalidate();
            }
        }

        public int ScaleFactor
        {
            get => _scale;
            set
            {
                int clamped = Math.Max(1, Math.Min(5, value));
                if (_scale != clamped)
                {
                    _scale = clamped;
                    UpdateControlSize();
                    Invalidate();
                }
            }
        }

        public bool ShowGrid
        {
            get => _showGrid;
            set
            {
                _showGrid = value;
                Invalidate();
            }
        }

        public int ActiveTileId
        {
            get => _activeTileId;
            set
            {
                _activeTileId = Math.Max(0, value);
                Invalidate();
            }
        }

        public MapDrawingTool CurrentTool
        {
            get => _currentTool;
            set
            {
                _currentTool = value;
                Cursor = (_currentTool == MapDrawingTool.PickTile) ? Cursors.Hand : Cursors.Cross;
            }
        }

        public void UpdateControlSize()
        {
            if (_map == null)
            {
                Size = new Size(256 * _scale, 192 * _scale);
                return;
            }

            int w = _map.Width * _map.TileWidth * _scale;
            int h = _map.Height * _map.TileHeight * _scale;
            Size = new Size(w, h);
        }

        private int TilePixelSize => (_map != null ? _map.TileWidth : 16) * _scale;

        private Point ScreenToTile(Point pt)
        {
            int step = TilePixelSize;
            if (step <= 0 || _map == null) return new Point(-1, -1);

            int tx = pt.X / step;
            int ty = pt.Y / step;

            if (tx >= 0 && tx < _map.Width && ty >= 0 && ty < _map.Height)
                return new Point(tx, ty);

            return new Point(-1, -1);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            if (_map == null)
            {
                using (var brush = new SolidBrush(Color.Gray))
                {
                    g.DrawString("Nessuna mappa selezionata.", Font, brush, 20, 20);
                }
                return;
            }

            int step = TilePixelSize;
            int totalW = _map.Width * step;
            int totalH = _map.Height * step;

            // Sfondo mappa (Colore Paper ZX Spectrum)
            Color paperColor = ZxSpectrumPalette.GetColor(_map.DefaultPaper, _map.Bright);
            using (SolidBrush bgBrush = new SolidBrush(paperColor))
            {
                g.FillRectangle(bgBrush, 0, 0, totalW, totalH);
            }

            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.SmoothingMode = SmoothingMode.None;

            // Render dei tile della mappa
            for (int y = 0; y < _map.Height; y++)
            {
                for (int x = 0; x < _map.Width; x++)
                {
                    int tileId = _map.GetTile(x, y);
                    if (tileId > 0 && _project != null && (tileId - 1) < _project.Sprites.Count)
                    {
                        var sprite = _project.Sprites[tileId - 1];
                        using (Bitmap spriteBmp = sprite.RenderToBitmap(_scale))
                        {
                            g.DrawImage(spriteBmp, x * step, y * step);
                        }
                    }
                }
            }

            // Griglia dei tile
            if (_showGrid)
            {
                using (Pen gridPen = new Pen(Color.FromArgb(45, 255, 255, 255), 1f))
                using (Pen screenPen = new Pen(Color.FromArgb(120, 0, 220, 255), 1.5f))
                {
                    for (int x = 0; x <= _map.Width; x++)
                    {
                        // Evidenzia bordo schermo standard ZX Spectrum (ogni 16 tile)
                        bool isScreenEdge = (x > 0 && x % 16 == 0);
                        g.DrawLine(isScreenEdge ? screenPen : gridPen, x * step, 0, x * step, totalH);
                    }

                    for (int y = 0; y <= _map.Height; y++)
                    {
                        // Evidenzia bordo schermo standard ZX Spectrum (ogni 12 tile)
                        bool isScreenEdge = (y > 0 && y % 12 == 0);
                        g.DrawLine(isScreenEdge ? screenPen : gridPen, 0, y * step, totalW, y * step);
                    }
                }
            }

            // Anteprima trascinamento forme (Rettangolo / Rettangolo Pieno)
            if (_isDraggingShape && _shapeStartTile.X >= 0 && _currentHoverTile.X >= 0)
            {
                int minX = Math.Min(_shapeStartTile.X, _currentHoverTile.X);
                int maxX = Math.Max(_shapeStartTile.X, _currentHoverTile.X);
                int minY = Math.Min(_shapeStartTile.Y, _currentHoverTile.Y);
                int maxY = Math.Max(_shapeStartTile.Y, _currentHoverTile.Y);

                Rectangle shapeRect = new Rectangle(minX * step, minY * step, (maxX - minX + 1) * step, (maxY - minY + 1) * step);

                if (_currentTool == MapDrawingTool.RectangleFilled)
                {
                    using (SolidBrush prevBrush = new SolidBrush(Color.FromArgb(90, 0, 220, 255)))
                    {
                        g.FillRectangle(prevBrush, shapeRect);
                    }
                }

                using (Pen prevPen = new Pen(Color.FromArgb(255, 255, 100), 2f) { DashStyle = DashStyle.Dash })
                {
                    g.DrawRectangle(prevPen, shapeRect);
                }
            }

            // Evidenziazione tile sotto il cursore
            if (_currentHoverTile.X >= 0 && _currentHoverTile.X < _map.Width &&
                _currentHoverTile.Y >= 0 && _currentHoverTile.Y < _map.Height)
            {
                Rectangle hoverRect = new Rectangle(_currentHoverTile.X * step, _currentHoverTile.Y * step, step, step);
                using (Pen cursorPen = new Pen(Color.FromArgb(200, 255, 255, 0), 1.5f))
                {
                    g.DrawRectangle(cursorPen, hoverRect);
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.Focus();
            if (_map == null) return;

            Point tile = ScreenToTile(e.Location);
            if (tile.X < 0 || tile.Y < 0) return;

            // Click Destro: Pipetta rapida (raccoglie il tile sotto il cursore)
            if (e.Button == MouseButtons.Right)
            {
                int picked = _map.GetTile(tile.X, tile.Y);
                _activeTileId = picked;
                TilePicked?.Invoke(this, picked);
                Invalidate();
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                switch (_currentTool)
                {
                    case MapDrawingTool.PickTile:
                        int picked = _map.GetTile(tile.X, tile.Y);
                        _activeTileId = picked;
                        TilePicked?.Invoke(this, picked);
                        break;

                    case MapDrawingTool.Pencil:
                        _map.PushUndoState();
                        _isDrawing = true;
                        _map.SetTile(tile.X, tile.Y, _activeTileId);
                        Invalidate();
                        MapChanged?.Invoke(this, EventArgs.Empty);
                        break;

                    case MapDrawingTool.Eraser:
                        _map.PushUndoState();
                        _isDrawing = true;
                        _map.SetTile(tile.X, tile.Y, 0);
                        Invalidate();
                        MapChanged?.Invoke(this, EventArgs.Empty);
                        break;

                    case MapDrawingTool.FloodFill:
                        _map.PushUndoState();
                        FloodFill(tile.X, tile.Y, _activeTileId);
                        Invalidate();
                        MapChanged?.Invoke(this, EventArgs.Empty);
                        break;

                    case MapDrawingTool.RectangleOutline:
                    case MapDrawingTool.RectangleFilled:
                        _map.PushUndoState();
                        _isDraggingShape = true;
                        _shapeStartTile = tile;
                        Invalidate();
                        break;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_map == null) return;

            Point tile = ScreenToTile(e.Location);
            if (tile != _currentHoverTile)
            {
                _currentHoverTile = tile;

                if (tile.X >= 0 && tile.Y >= 0)
                {
                    int tileId = _map.GetTile(tile.X, tile.Y);
                    HoverCoordinatesChanged?.Invoke(this, new MapHoverEventArgs
                    {
                        TileX = tile.X,
                        TileY = tile.Y,
                        PixelX = tile.X * _map.TileWidth,
                        PixelY = tile.Y * _map.TileHeight,
                        TileId = tileId
                    });
                }

                if (_isDrawing && tile.X >= 0 && tile.Y >= 0)
                {
                    int tileToPlace = (_currentTool == MapDrawingTool.Eraser) ? 0 : _activeTileId;
                    if (_map.GetTile(tile.X, tile.Y) != tileToPlace)
                    {
                        _map.SetTile(tile.X, tile.Y, tileToPlace);
                        MapChanged?.Invoke(this, EventArgs.Empty);
                    }
                }

                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (_map == null) return;

            if (_isDraggingShape && _shapeStartTile.X >= 0 && _currentHoverTile.X >= 0)
            {
                int minX = Math.Min(_shapeStartTile.X, _currentHoverTile.X);
                int maxX = Math.Max(_shapeStartTile.X, _currentHoverTile.X);
                int minY = Math.Min(_shapeStartTile.Y, _currentHoverTile.Y);
                int maxY = Math.Max(_shapeStartTile.Y, _currentHoverTile.Y);

                if (_currentTool == MapDrawingTool.RectangleFilled)
                {
                    for (int y = minY; y <= maxY; y++)
                    {
                        for (int x = minX; x <= maxX; x++)
                        {
                            _map.SetTile(x, y, _activeTileId);
                        }
                    }
                }
                else if (_currentTool == MapDrawingTool.RectangleOutline)
                {
                    for (int x = minX; x <= maxX; x++)
                    {
                        _map.SetTile(x, minY, _activeTileId);
                        _map.SetTile(x, maxY, _activeTileId);
                    }
                    for (int y = minY; y <= maxY; y++)
                    {
                        _map.SetTile(minX, y, _activeTileId);
                        _map.SetTile(maxX, y, _activeTileId);
                    }
                }

                _isDraggingShape = false;
                Invalidate();
                MapChanged?.Invoke(this, EventArgs.Empty);
            }

            _isDrawing = false;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _currentHoverTile = new Point(-1, -1);
            Invalidate();
        }

        private void FloodFill(int startX, int startY, int targetTileId)
        {
            if (_map == null) return;
            int originalTileId = _map.GetTile(startX, startY);
            if (originalTileId == targetTileId) return;

            Queue<Point> queue = new Queue<Point>();
            bool[,] visited = new bool[_map.Width, _map.Height];

            queue.Enqueue(new Point(startX, startY));
            visited[startX, startY] = true;

            while (queue.Count > 0)
            {
                Point pt = queue.Dequeue();
                _map.SetTile(pt.X, pt.Y, targetTileId);

                int[] dx = { 0, 0, -1, 1 };
                int[] dy = { -1, 1, 0, 0 };

                for (int i = 0; i < 4; i++)
                {
                    int nx = pt.X + dx[i];
                    int ny = pt.Y + dy[i];

                    if (nx >= 0 && nx < _map.Width && ny >= 0 && ny < _map.Height &&
                        !visited[nx, ny] && _map.GetTile(nx, ny) == originalTileId)
                    {
                        visited[nx, ny] = true;
                        queue.Enqueue(new Point(nx, ny));
                    }
                }
            }
        }
    }
}
