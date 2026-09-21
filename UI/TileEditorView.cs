using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using ZXSpectrumSpriteGenerator.Models;
using ZXSpectrumSpriteGenerator.Services;
using ZXSpectrumSpriteGenerator.UI.Controls;

namespace ZXSpectrumSpriteGenerator.UI
{
    /// <summary>
    /// Editor completo e dedicato per i Tile 8x8 del Sinclair ZX Spectrum.
    /// Offre gli stessi strumenti, comandi, palette e filmstrip dell'Editor Sprite,
    /// ma specializzato nella creazione e rifinitura dei singoli blocchi grafici per le mappe.
    /// </summary>
    public partial class TileEditorView : UserControl
    {
        private SpriteProject _project;
        private DrawingTool _currentTool = DrawingTool.Pencil;
        private readonly List<Button> _inkButtons = new List<Button>();
        private readonly List<Button> _paperButtons = new List<Button>();
        private bool _isUpdatingName = false;

        public event EventHandler ProjectModified;
        public event EventHandler RequestSwitchToMaps;
        public event EventHandler RequestSwitchToSprites;

        public TileEditorView()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;
            InitColorPickers();
            CenterCanvas();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SpriteProject Project
        {
            get => _project;
            set
            {
                _project = value;
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                    return;

                if (_project != null && _project.Tiles.Count == 0)
                {
                    // Inizializza con i preset di base se vuoto
                    _project.Tiles.Add(new Tile("Muro_Mattoni", TilePresets.BrickWall));
                    _project.Tiles.Add(new Tile("Piattaforma", TilePresets.Platform) { InkColor = 5 });
                    _project.Tiles.Add(new Tile("Scala", TilePresets.Ladder) { InkColor = 6 });
                    _project.Tiles.Add(new Tile("Spuntoni", TilePresets.Spikes) { InkColor = 2 });
                    _project.SelectedTileIndex = 0;
                }

                _filmstrip.IsTilesMode = true;
                _filmstrip.Project = _project;
                SelectTile(_project != null ? _project.SelectedTileIndex : 0);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Tile CurrentTile => _project?.CurrentTile;

        private void InitColorPickers()
        {
            _pnlInkColors.Controls.Clear();
            _pnlPaperColors.Controls.Clear();
            _inkButtons.Clear();
            _paperButtons.Clear();

            int btnW = 24;
            int btnH = 22;

            for (int i = 0; i < 8; i++)
            {
                int colorIndex = i;
                Color col = ZxSpectrumPalette.GetColor(colorIndex, true);

                Button btnInk = new Button
                {
                    Width = btnW,
                    Height = btnH,
                    BackColor = col,
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(2),
                    Tag = colorIndex
                };
                btnInk.FlatAppearance.BorderColor = Color.Black;
                btnInk.FlatAppearance.BorderSize = 1;
                btnInk.Click += (s, e) => SetInkColor(colorIndex);
                _inkButtons.Add(btnInk);
                _pnlInkColors.Controls.Add(btnInk);

                Button btnPaper = new Button
                {
                    Width = btnW,
                    Height = btnH,
                    BackColor = col,
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(2),
                    Tag = colorIndex
                };
                btnPaper.FlatAppearance.BorderColor = Color.Black;
                btnPaper.FlatAppearance.BorderSize = 1;
                btnPaper.Click += (s, e) => SetPaperColor(colorIndex);
                _paperButtons.Add(btnPaper);
                _pnlPaperColors.Controls.Add(btnPaper);
            }
        }

        public void SelectTile(int index)
        {
            if (_project == null || _project.Tiles.Count == 0)
            {
                _canvas.CurrentSprite = null;
                UpdateTileInfo();
                return;
            }

            _project.SelectedTileIndex = Math.Max(0, Math.Min(_project.Tiles.Count - 1, index));
            var tile = _project.CurrentTile;

            _canvas.CurrentSprite = tile;
            _filmstrip.EnsureVisible(_project.SelectedTileIndex);
            _filmstrip.Invalidate();

            UpdateColorSelectionUI();
            UpdateTileInfo();
            CenterCanvas();
        }

        public void RefreshView()
        {
            _filmstrip.Invalidate();
            _canvas.Invalidate();
            UpdateColorSelectionUI();
            UpdateTileInfo();
            CenterCanvas();
        }

        private void CenterCanvas()
        {
            if (_canvas == null || _centerPanel == null) return;
            int x = Math.Max(20, (_centerPanel.ClientSize.Width - _canvas.Width) / 2);
            int y = Math.Max(20, (_centerPanel.ClientSize.Height - _canvas.Height - 30) / 2);
            _canvas.Location = new Point(x, y);
        }

        private void CenterPanel_Resize(object sender, EventArgs e) => CenterCanvas();

        private void SetTool(DrawingTool tool, ToolStripButton activeBtn)
        {
            _currentTool = tool;
            _canvas.CurrentTool = tool;
            _btnToolPencil.Checked = (activeBtn == _btnToolPencil);
            _btnToolEraser.Checked = (activeBtn == _btnToolEraser);
            _btnToolLine.Checked = (activeBtn == _btnToolLine);
            _btnToolRect.Checked = (activeBtn == _btnToolRect);
            _btnToolRectFill.Checked = (activeBtn == _btnToolRectFill);
            _btnToolCircle.Checked = (activeBtn == _btnToolCircle);
            _btnToolCircleFill.Checked = (activeBtn == _btnToolCircleFill);
            _btnToolFill.Checked = (activeBtn == _btnToolFill);
            _btnToolPicker.Checked = (activeBtn == _btnToolPicker);
        }

        private void SetInkColor(int color)
        {
            if (CurrentTile == null) return;
            CurrentTile.InkColor = color;
            _canvas.Invalidate();
            UpdateColorSelectionUI();
            _pnlPreviewBox.Invalidate();
            _pnlPatternPreview.Invalidate();
            _filmstrip.Invalidate();
            UpdateTileInfo();
            ProjectModified?.Invoke(this, EventArgs.Empty);
        }

        private void SetPaperColor(int color)
        {
            if (CurrentTile == null) return;
            CurrentTile.PaperColor = color;
            _canvas.Invalidate();
            UpdateColorSelectionUI();
            _pnlPreviewBox.Invalidate();
            _pnlPatternPreview.Invalidate();
            _filmstrip.Invalidate();
            UpdateTileInfo();
            ProjectModified?.Invoke(this, EventArgs.Empty);
        }

        private void ChkBright_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentTile == null) return;
            CurrentTile.Bright = _chkBright.Checked;
            _canvas.Invalidate();
            _pnlPreviewBox.Invalidate();
            _pnlPatternPreview.Invalidate();
            _filmstrip.Invalidate();
            UpdateTileInfo();
            ProjectModified?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateColorSelectionUI()
        {
            if (CurrentTile == null) return;

            _chkBright.Checked = CurrentTile.Bright;
            _chkFlash.Checked = CurrentTile.Flash;

            for (int i = 0; i < _inkButtons.Count; i++)
            {
                bool isSelected = (i == CurrentTile.InkColor);
                _inkButtons[i].FlatAppearance.BorderColor = isSelected ? Color.White : Color.Black;
                _inkButtons[i].FlatAppearance.BorderSize = isSelected ? 3 : 1;
            }

            for (int i = 0; i < _paperButtons.Count; i++)
            {
                bool isSelected = (i == CurrentTile.PaperColor);
                _paperButtons[i].FlatAppearance.BorderColor = isSelected ? Color.White : Color.Black;
                _paperButtons[i].FlatAppearance.BorderSize = isSelected ? 3 : 1;
            }
        }

        private void UpdateTileInfo()
        {
            var tile = CurrentTile;
            if (tile == null)
            {
                _txtName.Text = string.Empty;
                _lblInfo.Text = "Nessun tile selezionato";
                _txtQuickAsm.Text = string.Empty;
                _pnlPreviewBox.Invalidate();
                _pnlPatternPreview.Invalidate();
                return;
            }

            _isUpdatingName = true;
            _txtName.Text = tile.Name;
            _isUpdatingName = false;

            int idx = _project != null ? _project.SelectedTileIndex + 1 : 1;
            int total = _project != null ? _project.Tiles.Count : 1;
            _lblInfo.Text = $"Tile #{idx} di {total}\r\nDimensione: 8x8 pixel\r\nMemoria: 9 byte (1 attr + 8 pixel)\r\nColore: Ink={tile.InkColor}, Paper={tile.PaperColor}\r\nAttributi: Bright={(tile.Bright ? "ON" : "OFF")}, Flash={(tile.Flash ? "ON" : "OFF")}";

            // Genera codice assembly rapido (9 byte: 1 attributo colore + 8 byte bitmap)
            byte attr = tile.GetAttributeByte();
            byte[] bytes = tile.GetBytesRowByRow();
            StringBuilder sb = new StringBuilder();
            sb.Append($"{SanitizeAsmName(tile.Name)}:\r\n    defb {attr}, ");
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i]);
                if (i < bytes.Length - 1) sb.Append(", ");
            }
            _txtQuickAsm.Text = sb.ToString();

            _pnlPreviewBox.Invalidate();
            _pnlPatternPreview.Invalidate();
        }

        private static string SanitizeAsmName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "tile";
            string s = System.Text.RegularExpressions.Regex.Replace(name, @"[^a-zA-Z0-9_]", "_").ToLower();
            if (char.IsDigit(s[0])) s = "tile_" + s;
            return s;
        }

        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            if (_isUpdatingName || CurrentTile == null) return;
            CurrentTile.Name = _txtName.Text;
            _filmstrip.Invalidate();
            UpdateTileInfo();
            ProjectModified?.Invoke(this, EventArgs.Empty);
        }

        private void Canvas_SpriteChanged(object sender, EventArgs e)
        {
            _filmstrip.Invalidate();
            _pnlPreviewBox.Invalidate();
            _pnlPatternPreview.Invalidate();
            UpdateTileInfo();
            ProjectModified?.Invoke(this, EventArgs.Empty);
        }

        private void Canvas_HoverCoordinatesChanged(object sender, HoverCoordinateEventArgs e)
        {
            _lblCoords.Text = $"Pixel: ({e.PixelX}, {e.PixelY}) | Cella: ({e.CharX}, {e.CharY})";
        }

        private void Filmstrip_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_project != null)
            {
                SelectTile(_project.SelectedTileIndex);
            }
        }

        // Tools
        private void BtnToolPencil_Click(object sender, EventArgs e) => SetTool(DrawingTool.Pencil, _btnToolPencil);
        private void BtnToolEraser_Click(object sender, EventArgs e) => SetTool(DrawingTool.Pencil, _btnToolEraser); // Gomma usa tasto destro
        private void BtnToolLine_Click(object sender, EventArgs e) => SetTool(DrawingTool.Line, _btnToolLine);
        private void BtnToolRect_Click(object sender, EventArgs e) => SetTool(DrawingTool.RectangleOutline, _btnToolRect);
        private void BtnToolRectFill_Click(object sender, EventArgs e) => SetTool(DrawingTool.RectangleFilled, _btnToolRectFill);
        private void BtnToolCircle_Click(object sender, EventArgs e) => SetTool(DrawingTool.CircleOutline, _btnToolCircle);
        private void BtnToolCircleFill_Click(object sender, EventArgs e) => SetTool(DrawingTool.CircleFilled, _btnToolCircleFill);
        private void BtnToolFill_Click(object sender, EventArgs e) => SetTool(DrawingTool.FloodFill, _btnToolFill);
        private void BtnToolPicker_Click(object sender, EventArgs e)
        {
            // Pipetta rapida
            if (CurrentTile != null)
            {
                MessageBox.Show($"Tile corrente: Ink={CurrentTile.InkColor} ({ZxSpectrumPalette.ColorNames[CurrentTile.InkColor]}), Paper={CurrentTile.PaperColor} ({ZxSpectrumPalette.ColorNames[CurrentTile.PaperColor]}), Bright={CurrentTile.Bright}, Flash={CurrentTile.Flash}", "Proprietà Colore Tile", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Transformations
        public void ActionUndo()
        {
            _canvas.Undo();
            _filmstrip.Invalidate();
            UpdateTileInfo();
            ProjectModified?.Invoke(this, EventArgs.Empty);
        }

        public void ActionRedo()
        {
            _canvas.Redo();
            _filmstrip.Invalidate();
            UpdateTileInfo();
            ProjectModified?.Invoke(this, EventArgs.Empty);
        }

        private void BtnUndo_Click(object sender, EventArgs e) => ActionUndo();
        private void BtnRedo_Click(object sender, EventArgs e) => ActionRedo();

        private void BtnFlipH_Click(object sender, EventArgs e)
        {
            if (CurrentTile == null) return;
            _canvas.PushUndoState();
            CurrentTile.FlipHorizontal();
            _canvas.Invalidate();
            _filmstrip.Invalidate();
            UpdateTileInfo();
            ProjectModified?.Invoke(this, EventArgs.Empty);
        }

        private void BtnFlipV_Click(object sender, EventArgs e)
        {
            if (CurrentTile == null) return;
            _canvas.PushUndoState();
            CurrentTile.FlipVertical();
            _canvas.Invalidate();
            _filmstrip.Invalidate();
            UpdateTileInfo();
            ProjectModified?.Invoke(this, EventArgs.Empty);
        }

        private void BtnRotate_Click(object sender, EventArgs e)
        {
            if (CurrentTile == null) return;
            _canvas.PushUndoState();
            CurrentTile.Rotate90(true);
            _canvas.Invalidate();
            _filmstrip.Invalidate();
            UpdateTileInfo();
            ProjectModified?.Invoke(this, EventArgs.Empty);
        }

        private void BtnInvert_Click(object sender, EventArgs e)
        {
            if (CurrentTile == null) return;
            _canvas.PushUndoState();
            CurrentTile.Invert();
            _canvas.Invalidate();
            _filmstrip.Invalidate();
            UpdateTileInfo();
            ProjectModified?.Invoke(this, EventArgs.Empty);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (CurrentTile == null) return;
            _canvas.PushUndoState();
            CurrentTile.Clear();
            _canvas.Invalidate();
            _filmstrip.Invalidate();
            UpdateTileInfo();
            ProjectModified?.Invoke(this, EventArgs.Empty);
        }

        private void BtnGrid_Click(object sender, EventArgs e)
        {
            _canvas.ShowPixelGrid = _btnGrid.Checked;
        }

        private void BtnZoomIn_Click(object sender, EventArgs e)
        {
            if (_canvas.CellSize < 48)
            {
                _canvas.CellSize += 4;
                _lblZoom.Text = $"{_canvas.CellSize}px";
                CenterCanvas();
            }
        }

        private void BtnZoomOut_Click(object sender, EventArgs e)
        {
            if (_canvas.CellSize > 16)
            {
                _canvas.CellSize -= 4;
                _lblZoom.Text = $"{_canvas.CellSize}px";
                CenterCanvas();
            }
        }

        // Filmstrip Actions
        public void ActionAddTile()
        {
            if (_project == null) return;
            var t = _project.AddTile();
            SelectTile(_project.Tiles.Count - 1);
            ProjectModified?.Invoke(this, EventArgs.Empty);
        }

        private void BtnAddTile_Click(object sender, EventArgs e) => ActionAddTile();

        public void ActionImportTiles()
        {
            if (_project == null) return;
            using (var dlg = new AddTilesDialog(_project))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK && dlg.NewlyAddedTiles.Count > 0)
                {
                    foreach (var t in dlg.NewlyAddedTiles)
                    {
                        _project.Tiles.Add(t);
                    }
                    _project.SelectedTileIndex = _project.Tiles.Count - 1;
                    SelectTile(_project.SelectedTileIndex);
                    ProjectModified?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void BtnImportTiles_Click(object sender, EventArgs e) => ActionImportTiles();

        public void ActionPasteFromClipboard()
        {
            if (_project == null) return;
            if (!Clipboard.ContainsText())
            {
                MessageBox.Show("Nessun testo presente negli appunti da importare come tile.", "Appunti Vuoti", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string text = Clipboard.GetText();
            int defInk = CurrentTile != null ? CurrentTile.InkColor : 7;
            int defPaper = CurrentTile != null ? CurrentTile.PaperColor : 0;
            bool defBright = CurrentTile != null ? CurrentTile.Bright : true;
            bool defFlash = CurrentTile != null ? CurrentTile.Flash : false;

            int tileIndex = _project.Tiles.Count + 1;
            var tiles = SpriteImporter.ImportTilesFromText(text, $"Tile_{tileIndex}_", defInk, defPaper, defBright, defFlash);
            if (tiles.Count == 0)
            {
                MessageBox.Show("Nessun dato byte valido trovato negli appunti.\nCopia dichiarazioni 'db', 'defb' o liste di byte separati da virgole.", "Formato Non Riconosciuto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var t in tiles)
            {
                _project.Tiles.Add(t);
            }

            SelectTile(_project.Tiles.Count - 1);
            ProjectModified?.Invoke(this, EventArgs.Empty);
            MessageBox.Show($"Importati con successo {tiles.Count} tile dagli appunti!", "Importazione Completata", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnPasteClipboard_Click(object sender, EventArgs e) => ActionPasteFromClipboard();

        private void BtnDuplicateTile_Click(object sender, EventArgs e)
        {
            if (_project == null) return;
            var dup = _project.DuplicateCurrentTile();
            if (dup != null)
            {
                SelectTile(_project.SelectedTileIndex);
                ProjectModified?.Invoke(this, EventArgs.Empty);
            }
        }

        private void BtnDeleteTile_Click(object sender, EventArgs e)
        {
            if (_project == null || _project.Tiles.Count <= 1)
            {
                MessageBox.Show("Il progetto deve contenere almeno un tile.", "Avviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"Sei sicuro di voler eliminare il tile '{CurrentTile?.Name}'?", "Conferma Eliminazione", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _project.RemoveCurrentTile();
                SelectTile(_project.SelectedTileIndex);
                ProjectModified?.Invoke(this, EventArgs.Empty);
            }
        }

        private void BtnMoveLeft_Click(object sender, EventArgs e)
        {
            if (_project != null && _project.MoveCurrentTileUp())
            {
                SelectTile(_project.SelectedTileIndex);
                ProjectModified?.Invoke(this, EventArgs.Empty);
            }
        }

        private void BtnMoveRight_Click(object sender, EventArgs e)
        {
            if (_project != null && _project.MoveCurrentTileDown())
            {
                SelectTile(_project.SelectedTileIndex);
                ProjectModified?.Invoke(this, EventArgs.Empty);
            }
        }

        private void BtnCopyAsm_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_txtQuickAsm.Text))
            {
                Clipboard.SetText(_txtQuickAsm.Text);
                MessageBox.Show("Dati Assembly del tile copiati negli appunti!", "Copia Riuscita", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnBackToMap_Click(object sender, EventArgs e) => RequestSwitchToMaps?.Invoke(this, EventArgs.Empty);
        private void BtnBackToSprites_Click(object sender, EventArgs e) => RequestSwitchToSprites?.Invoke(this, EventArgs.Empty);

        // Previews
        private void PnlPreviewBox_Paint(object sender, PaintEventArgs e)
        {
            if (CurrentTile == null) return;
            Graphics g = e.Graphics;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;

            // 1x (8x8)
            using (Bitmap b1 = CurrentTile.RenderToBitmap(1))
            {
                g.DrawImage(b1, 15, 25);
            }
            using (var brush = new SolidBrush(Color.Gray))
            {
                g.DrawString("1x (8x8)", Font, brush, 8, 8);
            }

            // 4x (32x32)
            using (Bitmap b4 = CurrentTile.RenderToBitmap(4))
            {
                g.DrawImage(b4, 75, 12);
            }
            using (var brush = new SolidBrush(Color.Gray))
            {
                g.DrawString("4x (32px)", Font, brush, 72, 48);
            }

            // 8x (64x64)
            using (Bitmap b8 = CurrentTile.RenderToBitmap(7))
            {
                g.DrawImage(b8, 155, 6);
            }
        }

        private void PnlPatternPreview_Paint(object sender, PaintEventArgs e)
        {
            if (CurrentTile == null) return;
            Graphics g = e.Graphics;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;

            int step = 8 * 3; // 24px per tile
            using (Bitmap bmp = CurrentTile.RenderToBitmap(3))
            {
                for (int ry = 0; ry < 2; ry++)
                {
                    for (int rx = 0; rx < 8; rx++)
                    {
                        g.DrawImage(bmp, 12 + rx * step, 12 + ry * step);
                    }
                }
            }

            using (Pen borderPen = new Pen(Color.FromArgb(60, 255, 255, 255), 1f))
            {
                for (int ry = 0; ry <= 2; ry++)
                    g.DrawLine(borderPen, 12, 12 + ry * step, 12 + 8 * step, 12 + ry * step);
                for (int rx = 0; rx <= 8; rx++)
                    g.DrawLine(borderPen, 12 + rx * step, 12, 12 + rx * step, 12 + 2 * step);
            }
        }

        private void _chkFlash_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentTile == null) return;
            
            CurrentTile.Flash = _chkFlash.Checked;
            _canvas.Invalidate();
            _pnlPreviewBox.Invalidate();
            _pnlPatternPreview.Invalidate();
            _filmstrip.Invalidate();
            UpdateTileInfo();
            ProjectModified?.Invoke(this, EventArgs.Empty);
        }
    }
}
