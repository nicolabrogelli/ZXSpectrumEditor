using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        PickTile,
        PlaceWilly,
        PlacePortal,
        PlaceItem,
        PlaceGuardian
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

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SpriteProject Project
        {
            get => _project;
            set
            {
                _project = value;
                Invalidate();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        private bool _showManicMinerOverlays = true;
        public bool ShowManicMinerOverlays
        {
            get => _showManicMinerOverlays;
            set
            {
                _showManicMinerOverlays = value;
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

        private int TilePixelSize => (_map != null ? _map.TileWidth : 8) * _scale;

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
                    if (tileId > 0 && _project != null && (tileId - 1) < _project.Tiles.Count)
                    {
                        var tile = _project.Tiles[tileId - 1];
                        using (Bitmap tileBmp = tile.RenderToBitmap(_scale))
                        {
                            Rectangle destRect = new Rectangle(x * step, y * step, step, step);
                            g.DrawImage(tileBmp, destRect, 0, 0, tileBmp.Width, tileBmp.Height, GraphicsUnit.Pixel);
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
                    int screenTileW = Math.Max(1, 256 / (_map.TileWidth > 0 ? _map.TileWidth : 8));
                    int screenTileH = Math.Max(1, 192 / (_map.TileHeight > 0 ? _map.TileHeight : 8));

                    for (int x = 0; x <= _map.Width; x++)
                    {
                        // Evidenzia bordo schermo standard ZX Spectrum (ogni 32 tile da 8px = 256px)
                        bool isScreenEdge = (x > 0 && x % screenTileW == 0);
                        g.DrawLine(isScreenEdge ? screenPen : gridPen, x * step, 0, x * step, totalH);
                    }

                    for (int y = 0; y <= _map.Height; y++)
                    {
                        // Evidenzia bordo schermo standard ZX Spectrum (ogni 24 tile da 8px = 192px)
                        bool isScreenEdge = (y > 0 && y % screenTileH == 0);
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

            // Overlay Entità Manic Miner (Willy, Portale, Oggetti, Nastro, Guardiani)
            if (_showManicMinerOverlays && _map?.ManicMinerData != null)
            {
                DrawManicMinerOverlays(g, step);
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

        private void DrawManicMinerOverlays(Graphics g, int step)
        {
            var mm = _map.ManicMinerData;
            if (mm == null) return;

            using (Font overlayFont = new Font("Segoe UI", Math.Max(7.5f, step * 0.45f), FontStyle.Bold))
            using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                // 1. Nastro Trasportatore (Conveyor)
                if (mm.ConveyorLength > 0 && mm.ConveyorY < _map.Height)
                {
                    int startX = mm.ConveyorX * step;
                    int y = mm.ConveyorY * step;
                    int w = mm.ConveyorLength * step;
                    Rectangle convRect = new Rectangle(startX, y, w, step);
                    using (SolidBrush cBrush = new SolidBrush(Color.FromArgb(60, 0, 255, 120)))
                    using (Pen cPen = new Pen(Color.FromArgb(0, 255, 120), 1.5f) { DashStyle = DashStyle.Dash })
                    {
                        g.FillRectangle(cBrush, convRect);
                        g.DrawRectangle(cPen, convRect);
                    }
                    string arrow = mm.ConveyorDirection == 0 ? "◄ ◄ ◄ NASTRO ◄ ◄ ◄" : "► ► ► NASTRO ► ► ►";
                    using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(220, 255, 255, 255)))
                    {
                        g.DrawString(arrow, overlayFont, textBrush, convRect, sf);
                    }
                }

                // 2. Portale di Uscita (16x16 pixel = 2x2 celle)
                Rectangle portalRect = new Rectangle(mm.PortalX * step, mm.PortalY * step, 2 * step, 2 * step);
                using (SolidBrush pBrush = new SolidBrush(Color.FromArgb(90, 255, 0, 200)))
                using (Pen pPen = new Pen(Color.FromArgb(255, 220, 0), 2f))
                {
                    g.FillRectangle(pBrush, portalRect);
                    g.DrawRectangle(pPen, portalRect);
                }
                using (SolidBrush pTextBrush = new SolidBrush(Color.Yellow))
                {
                    g.DrawString("🚪 EXIT", overlayFont, pTextBrush, portalRect, sf);
                }

                // 3. Oggetti Collezionabili (Items / Chiavi)
                if (mm.Items != null)
                {
                    for (int i = 0; i < mm.Items.Count; i++)
                    {
                        var it = mm.Items[i];
                        Rectangle itRect = new Rectangle(it.X * step, it.Y * step, step, step);
                        using (SolidBrush itemBg = new SolidBrush(Color.FromArgb(180, 0, 220, 255)))
                        using (Pen itPen = new Pen(Color.White, 1.5f))
                        {
                            g.FillEllipse(itemBg, itRect.X + 2, itRect.Y + 2, itRect.Width - 4, itRect.Height - 4);
                            g.DrawEllipse(itPen, itRect.X + 2, itRect.Y + 2, itRect.Width - 4, itRect.Height - 4);
                        }
                        using (SolidBrush itTextBrush = new SolidBrush(Color.Black))
                        {
                            g.DrawString($"💎{i + 1}", overlayFont, itTextBrush, itRect, sf);
                        }
                    }
                }

                // 4. Guardiani Orizzontali (Range patrol + marker)
                if (mm.HorizontalGuardians != null)
                {
                    for (int i = 0; i < mm.HorizontalGuardians.Count; i++)
                    {
                        var gObj = mm.HorizontalGuardians[i];
                        int minPx = gObj.MinX * step;
                        int maxPx = (gObj.MaxX + 1) * step;
                        int cy = (int)((gObj.Y + 0.5f) * step);

                        using (Pen patrolPen = new Pen(Color.FromArgb(220, 255, 60, 60), 2f) { DashStyle = DashStyle.DashDot })
                        {
                            g.DrawLine(patrolPen, minPx, cy, maxPx, cy);
                            g.DrawLine(patrolPen, minPx, cy - 4, minPx, cy + 4);
                            g.DrawLine(patrolPen, maxPx, cy - 4, maxPx, cy + 4);
                        }

                        Rectangle gRect = new Rectangle(gObj.InitialX * step, gObj.Y * step, step, step);
                        using (SolidBrush gBrush = new SolidBrush(Color.FromArgb(200, 220, 40, 40)))
                        using (Pen gPen = new Pen(Color.Yellow, 1.5f))
                        {
                            g.FillRectangle(gBrush, gRect);
                            g.DrawRectangle(gPen, gRect);
                        }
                        using (SolidBrush gTextBrush = new SolidBrush(Color.White))
                        {
                            g.DrawString($"👾{i + 1}", overlayFont, gTextBrush, gRect, sf);
                        }
                    }
                }

                // 5. Miner Willy Spawn
                Rectangle willyRect = new Rectangle(mm.WillyX * step, mm.WillyY * step, step, step);
                using (SolidBrush wBrush = new SolidBrush(Color.FromArgb(210, 0, 200, 80)))
                using (Pen wPen = new Pen(Color.White, 2f))
                {
                    g.FillRectangle(wBrush, willyRect);
                    g.DrawRectangle(wPen, willyRect);
                }
                using (SolidBrush wTextBrush = new SolidBrush(Color.Black))
                {
                    string dirSymbol = mm.WillyDirection == 0 ? "W►" : "◄W";
                    g.DrawString(dirSymbol, overlayFont, wTextBrush, willyRect, sf);
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

                    case MapDrawingTool.PlaceWilly:
                        if (_map.ManicMinerData != null)
                        {
                            _map.PushUndoState();
                            _map.ManicMinerData.WillyX = tile.X;
                            _map.ManicMinerData.WillyY = tile.Y;
                            _map.ManicMinerData.WillyPixelY = tile.Y * 8;
                            Invalidate();
                            MapChanged?.Invoke(this, EventArgs.Empty);
                        }
                        break;

                    case MapDrawingTool.PlacePortal:
                        if (_map.ManicMinerData != null)
                        {
                            _map.PushUndoState();
                            _map.ManicMinerData.PortalX = Math.Min(30, tile.X);
                            _map.ManicMinerData.PortalY = Math.Min(14, tile.Y);
                            Invalidate();
                            MapChanged?.Invoke(this, EventArgs.Empty);
                        }
                        break;

                    case MapDrawingTool.PlaceItem:
                        if (_map.ManicMinerData != null)
                        {
                            _map.PushUndoState();
                            var existing = _map.ManicMinerData.Items.Find(it => it.X == tile.X && it.Y == tile.Y);
                            if (existing != null)
                            {
                                _map.ManicMinerData.Items.Remove(existing);
                            }
                            else if (_map.ManicMinerData.Items.Count < 5)
                            {
                                byte nextAttr = (byte)(3 + _map.ManicMinerData.Items.Count);
                                _map.ManicMinerData.Items.Add(new ManicMinerItem(tile.X, tile.Y, nextAttr));
                            }
                            Invalidate();
                            MapChanged?.Invoke(this, EventArgs.Empty);
                        }
                        break;

                    case MapDrawingTool.PlaceGuardian:
                        if (_map.ManicMinerData != null)
                        {
                            _map.PushUndoState();
                            var existingG = _map.ManicMinerData.HorizontalGuardians.Find(g => g.InitialX == tile.X && g.Y == tile.Y);
                            if (existingG != null)
                            {
                                _map.ManicMinerData.HorizontalGuardians.Remove(existingG);
                            }
                            else if (_map.ManicMinerData.HorizontalGuardians.Count < 4)
                            {
                                int minX = Math.Max(0, tile.X - 4);
                                int maxX = Math.Min(31, tile.X + 4);
                                _map.ManicMinerData.HorizontalGuardians.Add(new ManicMinerGuardian
                                {
                                    Y = tile.Y,
                                    InitialX = tile.X,
                                    MinX = minX,
                                    MaxX = maxX,
                                    Direction = 0,
                                    Speed = 0,
                                    SpriteIndex = _map.ManicMinerData.HorizontalGuardians.Count * 2
                                });
                            }
                            Invalidate();
                            MapChanged?.Invoke(this, EventArgs.Empty);
                        }
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
