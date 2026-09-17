using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ZXSpectrumSpriteGenerator.Models;

namespace ZXSpectrumSpriteGenerator.UI.Controls
{
    /// <summary>
    /// Striscia orizzontale (filmstrip) che mostra le miniature di tutti gli sprite del progetto.
    /// Supporta scorrimento tramite scrollbar, rotella mouse, trascinamento (drag) e pulsanti,
    /// con evidenziazione del nemico selezionato e indicatori visivi sui bordi.
    /// </summary>
    public class SpriteFilmstrip : Control
    {
        private SpriteProject _project;
        private int _itemWidth = 84;
        private int _itemHeight = 92;
        private int _spacing = 8;
        private int _scrollOffsetX = 0;

        // Gestione drag del mouse per scorrimento orizzontale fluido
        private bool _isDragging = false;
        private int _dragStartX = 0;
        private int _dragStartScrollOffset = 0;
        private bool _hasDraggedEnough = false;

        public event EventHandler SelectedIndexChanged;
        public event EventHandler ScrollChanged;

        public int ItemWidth => _itemWidth;
        public int ItemHeight => _itemHeight;
        public int Spacing => _spacing;

        public SpriteFilmstrip()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.Selectable, true);

            BackColor = Color.FromArgb(24, 24, 28);
            Height = 105;
        }

        public SpriteProject Project
        {
            get => _project;
            set
            {
                _project = value;
                ClampScroll();
                Invalidate();
            }
        }

        public int TotalContentWidth
        {
            get
            {
                if (_project == null || _project.Sprites.Count == 0) return 0;
                return _spacing + _project.Sprites.Count * (_itemWidth + _spacing);
            }
        }

        public int MaxScrollOffsetX
        {
            get
            {
                int total = TotalContentWidth;
                return Math.Max(0, total - ClientSize.Width);
            }
        }

        public int ScrollOffsetX
        {
            get => _scrollOffsetX;
            set
            {
                int maxScroll = MaxScrollOffsetX;
                int clamped = Math.Max(0, Math.Min(maxScroll, value));
                if (_scrollOffsetX != clamped)
                {
                    _scrollOffsetX = clamped;
                    Invalidate();
                    ScrollChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void ClampScroll()
        {
            int max = MaxScrollOffsetX;
            if (_scrollOffsetX > max)
            {
                _scrollOffsetX = max;
                ScrollChanged?.Invoke(this, EventArgs.Empty);
            }
            if (_scrollOffsetX < 0)
            {
                _scrollOffsetX = 0;
                ScrollChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void ScrollLeft(int cardCount = 3)
        {
            ScrollOffsetX -= cardCount * (_itemWidth + _spacing);
        }

        public void ScrollRight(int cardCount = 3)
        {
            ScrollOffsetX += cardCount * (_itemWidth + _spacing);
        }

        public void ScrollToStart()
        {
            ScrollOffsetX = 0;
        }

        public void ScrollToEnd()
        {
            ScrollOffsetX = MaxScrollOffsetX;
        }

        public void EnsureVisible(int index)
        {
            if (_project == null || index < 0 || index >= _project.Sprites.Count) return;
            int cardLeft = _spacing + index * (_itemWidth + _spacing);
            int cardRight = cardLeft + _itemWidth;

            int viewLeft = _scrollOffsetX;
            int viewRight = _scrollOffsetX + ClientSize.Width;

            if (cardLeft < viewLeft)
            {
                ScrollOffsetX = Math.Max(0, cardLeft - _spacing);
            }
            else if (cardRight > viewRight)
            {
                ScrollOffsetX = Math.Max(0, cardRight + _spacing - ClientSize.Width);
            }
        }

        public void CenterSprite(int index)
        {
            if (_project == null || index < 0 || index >= _project.Sprites.Count) return;
            int cardCenter = _spacing + index * (_itemWidth + _spacing) + _itemWidth / 2;
            int targetOffset = cardCenter - ClientSize.Width / 2;
            ScrollOffsetX = targetOffset;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ClampScroll();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (_project == null || _project.Sprites.Count == 0)
            {
                using (var brush = new SolidBrush(Color.Gray))
                {
                    g.DrawString("Nessuno sprite presente nel progetto.", Font, brush, 10, 30);
                }
                return;
            }

            int x = _spacing - _scrollOffsetX;
            string currentGroup = _project.CurrentSprite?.GroupName;

            for (int i = 0; i < _project.Sprites.Count; i++)
            {
                var sprite = _project.Sprites[i];
                bool isSelected = (i == _project.SelectedIndex);
                bool isSameGroup = (!string.IsNullOrEmpty(currentGroup) && sprite.GroupName == currentGroup);
                Rectangle cardRect = new Rectangle(x, 5, _itemWidth, _itemHeight);

                // Disegna solo se visibile nell'area client (culling)
                if (cardRect.Right >= 0 && cardRect.Left <= ClientSize.Width)
                {
                    // Sfondo card
                    Color cardBg;
                    if (isSelected) cardBg = Color.FromArgb(45, 65, 95);
                    else if (isSameGroup) cardBg = Color.FromArgb(32, 42, 55);
                    else cardBg = Color.FromArgb(30, 30, 36);

                    using (SolidBrush brush = new SolidBrush(cardBg))
                    {
                        g.FillRectangle(brush, cardRect);
                    }

                    // Bordo selezione / gruppo
                    Color borderColor;
                    float borderWidth = 1f;
                    if (isSelected) { borderColor = Color.FromArgb(0, 220, 255); borderWidth = 2f; }
                    else if (isSameGroup) { borderColor = Color.FromArgb(0, 140, 180); borderWidth = 1.5f; }
                    else borderColor = Color.FromArgb(55, 55, 65);

                    using (Pen pen = new Pen(borderColor, borderWidth))
                    {
                        g.DrawRectangle(pen, cardRect);
                    }

                    // Badge del gruppo / nemico in alto
                    string groupTag = string.IsNullOrEmpty(sprite.GroupName) ? "Nemico" : sprite.GroupName;
                    if (groupTag.Length > 11) groupTag = groupTag.Substring(0, 10) + "…";
                    using (Font tagFont = new Font("Segoe UI", 6.8f, FontStyle.Bold))
                    using (SolidBrush tagBrush = new SolidBrush(isSameGroup ? Color.FromArgb(0, 220, 255) : Color.DarkGray))
                    {
                        StringFormat sfTag = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                        Rectangle tagRect = new Rectangle(cardRect.X + 2, cardRect.Y + 3, cardRect.Width - 4, 13);
                        g.DrawString(groupTag, tagFont, tagBrush, tagRect, sfTag);
                    }

                    // Render miniatura sprite
                    int thumbSize = 42;
                    int thumbX = cardRect.X + (cardRect.Width - thumbSize) / 2;
                    int thumbY = cardRect.Y + 17;
                    Rectangle thumbRect = new Rectangle(thumbX, thumbY, thumbSize, thumbSize);

                    using (Bitmap bmp = sprite.RenderToBitmap(1))
                    {
                        g.InterpolationMode = InterpolationMode.NearestNeighbor;
                        g.PixelOffsetMode = PixelOffsetMode.Half;
                        g.DrawImage(bmp, thumbRect);
                    }

                    // Bordo miniatura
                    using (Pen thumbPen = new Pen(Color.FromArgb(70, 70, 80)))
                    {
                        g.DrawRectangle(thumbPen, thumbRect);
                    }

                    // Testo indice e nome
                    string label = $"#{i} {sprite.Name}";
                    if (label.Length > 11) label = label.Substring(0, 10) + "…";

                    using (Font smallFont = new Font("Segoe UI", 7.2f))
                    using (SolidBrush textBrush = new SolidBrush(isSelected ? Color.White : Color.LightGray))
                    {
                        StringFormat sf = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };
                        Rectangle textRect = new Rectangle(cardRect.X + 2, thumbY + thumbSize + 2, cardRect.Width - 4, cardRect.Height - thumbSize - 19);
                        g.DrawString(label, smallFont, textBrush, textRect, sf);
                    }
                }

                x += _itemWidth + _spacing;
            }

            // Indicatori sfumati sui bordi se ci sono altri sprite da scorrere
            if (_scrollOffsetX > 0)
            {
                using (var brush = new LinearGradientBrush(new Rectangle(0, 0, 20, ClientSize.Height), Color.FromArgb(160, 15, 15, 20), Color.Transparent, LinearGradientMode.Horizontal))
                {
                    g.FillRectangle(brush, 0, 0, 20, ClientSize.Height);
                }
                using (var font = new Font("Segoe UI", 9f, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.FromArgb(0, 200, 255)))
                {
                    g.DrawString("◄", font, brush, 3, ClientSize.Height / 2 - 8);
                }
            }

            if (_scrollOffsetX < MaxScrollOffsetX)
            {
                using (var brush = new LinearGradientBrush(new Rectangle(ClientSize.Width - 20, 0, 20, ClientSize.Height), Color.Transparent, Color.FromArgb(160, 15, 15, 20), LinearGradientMode.Horizontal))
                {
                    g.FillRectangle(brush, ClientSize.Width - 20, 0, 20, ClientSize.Height);
                }
                using (var font = new Font("Segoe UI", 9f, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.FromArgb(0, 200, 255)))
                {
                    g.DrawString("►", font, brush, ClientSize.Width - 16, ClientSize.Height / 2 - 8);
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.Focus();
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
            {
                _isDragging = true;
                _dragStartX = e.X;
                _dragStartScrollOffset = _scrollOffsetX;
                _hasDraggedEnough = false;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_isDragging && (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle))
            {
                int dx = e.X - _dragStartX;
                if (Math.Abs(dx) > 4)
                {
                    _hasDraggedEnough = true;
                    Cursor = Cursors.SizeWE;
                    ScrollOffsetX = _dragStartScrollOffset - dx;
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Cursor = Cursors.Default;
            if (_isDragging)
            {
                _isDragging = false;
                if (!_hasDraggedEnough && e.Button == MouseButtons.Left)
                {
                    SelectSpriteAtPoint(e.Location);
                }
            }
        }

        private void SelectSpriteAtPoint(Point pt)
        {
            if (_project == null || _project.Sprites.Count == 0) return;

            int x = _spacing - _scrollOffsetX;
            for (int i = 0; i < _project.Sprites.Count; i++)
            {
                Rectangle cardRect = new Rectangle(x, 5, _itemWidth, _itemHeight);
                if (cardRect.Contains(pt))
                {
                    if (_project.SelectedIndex != i)
                    {
                        _project.SelectedIndex = i;
                        Invalidate();
                        SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
                    }
                    return;
                }
                x += _itemWidth + _spacing;
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (_project == null) return;

            ScrollOffsetX -= e.Delta / 2;
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Left || keyData == Keys.Right ||
                keyData == Keys.Home || keyData == Keys.End ||
                keyData == Keys.PageUp || keyData == Keys.PageDown)
            {
                return true;
            }
            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (_project == null || _project.Sprites.Count == 0) return;

            if (e.KeyCode == Keys.Left)
            {
                if (_project.SelectedIndex > 0)
                {
                    _project.SelectedIndex--;
                    EnsureVisible(_project.SelectedIndex);
                    Invalidate();
                    SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (_project.SelectedIndex < _project.Sprites.Count - 1)
                {
                    _project.SelectedIndex++;
                    EnsureVisible(_project.SelectedIndex);
                    Invalidate();
                    SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Home)
            {
                _project.SelectedIndex = 0;
                EnsureVisible(0);
                Invalidate();
                SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.End)
            {
                _project.SelectedIndex = _project.Sprites.Count - 1;
                EnsureVisible(_project.SelectedIndex);
                Invalidate();
                SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                ScrollLeft(4);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                ScrollRight(4);
                e.Handled = true;
            }
        }
    }
}
