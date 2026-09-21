using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ZXSpectrumSpriteGenerator.Models;
using ZXSpectrumSpriteGenerator.Services;
using ZXSpectrumSpriteGenerator.UI.Controls;

namespace ZXSpectrumSpriteGenerator.UI
{
    /// <summary>
    /// Sezione completa dell'Editor Mappe di Gioco per Sinclair ZX Spectrum.
    /// Consente la creazione, gestione, disegno ed esportazione di più mappe di gioco (tilemap),
    /// utilizzando come tileset gli sprite disegnati nel progetto.
    /// </summary>
    public partial class MapEditorView : UserControl
    {
        private SpriteProject _project;
        private bool _isUpdatingMapCombo = false;

        public event EventHandler ProjectModified;
        public event EventHandler RequestSwitchToSprites;
        public event EventHandler<int> RequestSwitchToTiles;

        public MapEditorView()
        {
            InitializeComponent();
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

                if (_project != null && _project.Maps.Count == 0)
                {
                    _project.AddMap("Mappa 1", 32, 24);
                }
                _canvas.Project = _project;
                _canvas.Map = _project?.CurrentMap;
                UpdateMapListCombo();
                RefreshTilesetPalette();
                UpdateMapInfo();
                UpdateQuickAsm();
                CenterCanvas();
            }
        }

        private void SetTool(MapDrawingTool tool, ToolStripButton activeBtn)
        {
            _canvas.CurrentTool = tool;
            _btnToolPencil.Checked = (activeBtn == _btnToolPencil);
            _btnToolEraser.Checked = (activeBtn == _btnToolEraser);
            _btnToolFill.Checked = (activeBtn == _btnToolFill);
            _btnToolRect.Checked = (activeBtn == _btnToolRect);
            _btnToolRectFill.Checked = (activeBtn == _btnToolRectFill);
            _btnToolPicker.Checked = (activeBtn == _btnToolPicker);
        }

        private void CenterPanel_Resize(object sender, EventArgs e) => CenterCanvas();

        private void BtnNewMap_Click(object sender, EventArgs e) => ActionNewMap();
        private void BtnCloneMap_Click(object sender, EventArgs e) => ActionDuplicateMap();
        private void BtnDeleteMap_Click(object sender, EventArgs e) => ActionDeleteMap();
        private void BtnRenameMap_Click(object sender, EventArgs e) => ActionRenameMap();
        private void BtnResizeMap_Click(object sender, EventArgs e) => ActionResizeMap();
        private void BtnExport_Click(object sender, EventArgs e) => ActionExportMap();
        private void BtnPng_Click(object sender, EventArgs e) => ActionExportPng();
        private void BtnBackToSprites_Click(object sender, EventArgs e) => RequestSwitchToSprites?.Invoke(this, EventArgs.Empty);

        private void BtnGoToTiles_Click(object sender, EventArgs e)
        {
            int targetIdx = Math.Max(0, _canvas.ActiveTileId - 1);
            RequestSwitchToTiles?.Invoke(this, targetIdx);
        }

        private void BtnAddTile_Click(object sender, EventArgs e)
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
                    _canvas.ActiveTileId = _project.Tiles.Count;
                    RefreshTilesetPalette();
                    _canvas.Invalidate();
                    ProjectModified?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void BtnToolPencil_Click(object sender, EventArgs e) => SetTool(MapDrawingTool.Pencil, _btnToolPencil);
        private void BtnToolEraser_Click(object sender, EventArgs e) => SetTool(MapDrawingTool.Eraser, _btnToolEraser);
        private void BtnToolFill_Click(object sender, EventArgs e) => SetTool(MapDrawingTool.FloodFill, _btnToolFill);
        private void BtnToolRect_Click(object sender, EventArgs e) => SetTool(MapDrawingTool.RectangleOutline, _btnToolRect);
        private void BtnToolRectFill_Click(object sender, EventArgs e) => SetTool(MapDrawingTool.RectangleFilled, _btnToolRectFill);
        private void BtnToolPicker_Click(object sender, EventArgs e) => SetTool(MapDrawingTool.PickTile, _btnToolPicker);

        private void BtnClear_Click(object sender, EventArgs e) => ActionClearMap();
        private void BtnUndo_Click(object sender, EventArgs e) => ActionUndo();
        private void BtnRedo_Click(object sender, EventArgs e) => ActionRedo();
        private void BtnGrid_Click(object sender, EventArgs e)
        {
            _canvas.ShowGrid = _btnGrid.Checked;
        }
        private void BtnZoomOut_Click(object sender, EventArgs e) => ActionZoom(-1);
        private void BtnZoomIn_Click(object sender, EventArgs e) => ActionZoom(1);

        private void BtnCopyAsm_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_txtQuickMapAsm.Text))
            {
                Clipboard.SetText(_txtQuickMapAsm.Text);
                MessageBox.Show("Dati Assembly della mappa copiati negli appunti!", "Copia Riuscita", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void RefreshView()
        {
            UpdateMapListCombo();
            RefreshTilesetPalette();
            UpdateActiveTileDisplay();
            UpdateMapInfo();
            UpdateQuickAsm();
            _canvas.Invalidate();
            CenterCanvas();
        }

        private void CenterCanvas()
        {
            if (_canvas == null || _centerPanel == null) return;
            int x = Math.Max(15, (_centerPanel.ClientSize.Width - _canvas.Width) / 2);
            int y = Math.Max(15, (_centerPanel.ClientSize.Height - _canvas.Height) / 2);
            _canvas.Location = new Point(x, y);
        }

        private void UpdateMapListCombo()
        {
            if (_project == null || _cmbMaps == null) return;

            _isUpdatingMapCombo = true;
            _cmbMaps.Items.Clear();

            for (int i = 0; i < _project.Maps.Count; i++)
            {
                var m = _project.Maps[i];
                _cmbMaps.Items.Add($"#{i + 1}: {m.Name} ({m.Width}x{m.Height})");
            }

            if (_project.Maps.Count > 0)
            {
                _project.SelectedMapIndex = Math.Max(0, Math.Min(_project.Maps.Count - 1, _project.SelectedMapIndex));
                _cmbMaps.SelectedIndex = _project.SelectedMapIndex;
            }

            _isUpdatingMapCombo = false;
        }

        private void CmbMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isUpdatingMapCombo || _project == null) return;

            int idx = _cmbMaps.SelectedIndex;
            if (idx >= 0 && idx < _project.Maps.Count)
            {
                _project.SelectedMapIndex = idx;
                _canvas.Map = _project.CurrentMap;
                UpdateMapInfo();
                UpdateQuickAsm();
                CenterCanvas();
            }
        }

        public void RefreshTilesetPalette()
        {
            _pnlTileset.Controls.Clear();
            if (_project == null) return;

            int y = 4;
            int cardW = _pnlTileset.ClientSize.Width - 30;
            int cardH = 40;
            
            // Card per ogni tile 8x8 del progetto (NON sprite!)
            for (int i = 0; i < _project.Tiles.Count; i++)
            {
                int tileId = i + 1;
                var tile = _project.Tiles[i];
                string label = $"{tileId}: {tile.Name}";
                Bitmap thumb = tile.RenderToBitmap(1);

                Panel card = CreateTileCard(tileId, label, thumb, y, cardW, cardH);
                _pnlTileset.Controls.Add(card);
                y += cardH + 4;
            }

            UpdateActiveTileDisplay();
        }

        private Panel CreateTileCard(int tileId, string label, Bitmap thumb, int top, int width, int height)
        {
            bool isSelected = (_canvas.ActiveTileId == tileId);
            Panel card = new Panel
            {
                Top = top,
                Left = 4,
                Width = width,
                Height = height,
                BackColor = isSelected ? Color.FromArgb(40, 70, 110) : Color.FromArgb(28, 28, 34),
                BorderStyle = isSelected ? BorderStyle.FixedSingle : BorderStyle.None,
                Cursor = Cursors.Hand
            };

            PictureBox pic = new PictureBox
            {
                Top = 4,
                Left = 5,
                Width = 32,
                Height = 32,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Black,
                Image = thumb
            };

            Label lbl = new Label
            {
                Top = 11,
                Left = 44,
                Width = width - 48,
                Text = label,
                ForeColor = isSelected ? Color.Cyan : Color.White,
                Font = new Font("Segoe UI", 7.8f, isSelected ? FontStyle.Bold : FontStyle.Regular),
                AutoEllipsis = true
            };

            Action selectCard = () =>
            {
                _canvas.ActiveTileId = tileId;
                RefreshTilesetSelection();
                UpdateActiveTileDisplay();
            };

            Action openInEditor = () =>
            {
                if (tileId > 0)
                {
                    RequestSwitchToTiles?.Invoke(this, tileId - 1);
                }
            };

            card.Click += (s, e) => selectCard();
            pic.Click += (s, e) => selectCard();
            lbl.Click += (s, e) => selectCard();

            card.DoubleClick += (s, e) => openInEditor();
            pic.DoubleClick += (s, e) => openInEditor();
            lbl.DoubleClick += (s, e) => openInEditor();

            card.Controls.AddRange(new Control[] { pic, lbl });
            return card;
        }

        private void RefreshTilesetSelection()
        {
            int currentTile = _canvas.ActiveTileId;
            int idx = 0;
            foreach (Control c in _pnlTileset.Controls)
            {
                if (c is Panel card)
                {
                    bool isSelected = (idx == currentTile);
                    card.BackColor = isSelected ? Color.FromArgb(40, 70, 110) : Color.FromArgb(28, 28, 34);
                    card.BorderStyle = isSelected ? BorderStyle.FixedSingle : BorderStyle.None;

                    foreach (Control child in card.Controls)
                    {
                        if (child is Label lbl)
                        {
                            lbl.ForeColor = isSelected ? Color.Cyan : Color.White;
                            lbl.Font = new Font("Segoe UI", 7.8f, isSelected ? FontStyle.Bold : FontStyle.Regular);
                        }
                    }
                    idx++;
                }
            }
        }

        private void UpdateActiveTileDisplay()
        {
            int tileId = _canvas.ActiveTileId;
            var oldBmp = _picActiveTile.Image;

            if (tileId == 0)
            {
                _picActiveTile.Image = null;
                _picActiveTile.BackColor = Color.Black;
                _lblActiveTileName.Text = "Tile #0: [Vuoto / Sfondo]";
            }
            else if (_project != null && (tileId - 1) < _project.Tiles.Count)
            {
                var tile = _project.Tiles[tileId - 1];
                _picActiveTile.Image = tile.RenderToBitmap(2);
                _lblActiveTileName.Text = $"Tile #{tileId}: {tile.Name}";
            }
            else
            {
                _picActiveTile.Image = null;
                _lblActiveTileName.Text = $"Tile #{tileId}";
            }

            oldBmp?.Dispose();
        }

        private void UpdateMapInfo()
        {
            var map = _project?.CurrentMap;
            if (map == null)
            {
                _lblDimensions.Text = "Dimensioni: -";
                _lblPixels.Text = "Pixel: -";
                _lblMemory.Text = "Memoria: -";
                return;
            }

            int w = map.Width;
            int h = map.Height;
            int pxW = w * map.TileWidth;
            int pxH = h * map.TileHeight;
            int totalBytes = w * h;

            string screenTag = (w == 32 && h == 24) ? " (Schermo ZX)" : "";
            _lblDimensions.Text = $"Dimensioni: {w}x{h} celle (8x8 px)";
            _lblPixels.Text = $"Pixel: {pxW}x{pxH} px{screenTag}";
            _lblMemory.Text = $"Memoria Z80: {totalBytes} byte ({totalBytes} celle)";
        }

        private void UpdateQuickAsm()
        {
            if (_project?.CurrentMap == null)
            {
                _txtQuickMapAsm.Text = string.Empty;
                return;
            }

            _txtQuickMapAsm.Text = MapExporter.ExportToAssembly(_project.CurrentMap, AsmDirective.Defb, exportAllMaps: false, project: _project).Trim();
        }

        private void Canvas_HoverCoordinatesChanged(object sender, MapHoverEventArgs e)
        {
            string tInfo = e.TileId == 0 ? "Vuoto" : (_project != null && (e.TileId - 1) < _project.Tiles.Count ? $"Tile #{e.TileId} ({_project.Tiles[e.TileId - 1].Name})" : $"Tile #{e.TileId}");
            _lblHoverCoords.Text = $"Cella: ({e.TileX}, {e.TileY}) | Pixel: ({e.PixelX}, {e.PixelY}) | [{tInfo}]";
        }

        private void Canvas_MapChanged(object sender, EventArgs e)
        {
            ProjectModified?.Invoke(this, EventArgs.Empty);
            UpdateQuickAsm();
        }

        private void Canvas_TilePicked(object sender, int tileId)
        {
            RefreshTilesetSelection();
            UpdateActiveTileDisplay();
        }

        // Actions
        public void ActionNewMap()
        {
            if (_project == null) return;

            using (Form dlg = new Form())
            {
                dlg.Text = "Nuova Mappa di Gioco ZX Spectrum";
                dlg.Size = new Size(380, 260);
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.MaximizeBox = false;
                dlg.MinimizeBox = false;
                dlg.BackColor = Color.FromArgb(32, 32, 38);
                dlg.ForeColor = Color.White;

                Label lblN = new Label { Text = "Nome Mappa:", Left = 20, Top = 20, AutoSize = true };
                TextBox txtName = new TextBox { Left = 20, Top = 42, Width = 320, Text = $"Mappa {_project.Maps.Count + 1}", BackColor = Color.FromArgb(20, 20, 25), ForeColor = Color.White };

                Label lblW = new Label { Text = "Larghezza (celle tile 8x8):", Left = 20, Top = 80, AutoSize = true };
                NumericUpDown numW = new NumericUpDown { Left = 20, Top = 102, Width = 145, Minimum = 1, Maximum = 128, Value = 32, BackColor = Color.FromArgb(20, 20, 25), ForeColor = Color.White };

                Label lblH = new Label { Text = "Altezza (celle tile 8x8):", Left = 185, Top = 80, AutoSize = true };
                NumericUpDown numH = new NumericUpDown { Left = 185, Top = 102, Width = 155, Minimum = 1, Maximum = 64, Value = 24, BackColor = Color.FromArgb(20, 20, 25), ForeColor = Color.White };

                Label lblPreset = new Label { Text = "Preset: 32x24 = Schermo Singolo ZX (256x192 px con tile 8x8)", Left = 20, Top = 135, AutoSize = true, ForeColor = Color.FromArgb(0, 200, 255), Font = new Font("Segoe UI", 7.8f) };

                Button btnOk = new Button { Text = "Crea Mappa", Left = 140, Top = 170, Width = 100, Height = 32, BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DialogResult = DialogResult.OK };
                Button btnCancel = new Button { Text = "Annulla", Left = 250, Top = 170, Width = 90, Height = 32, BackColor = Color.FromArgb(60, 60, 70), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DialogResult = DialogResult.Cancel };

                dlg.Controls.AddRange(new Control[] { lblN, txtName, lblW, numW, lblH, numH, lblPreset, btnOk, btnCancel });
                dlg.AcceptButton = btnOk;
                dlg.CancelButton = btnCancel;

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    string mName = txtName.Text.Trim();
                    if (string.IsNullOrEmpty(mName)) mName = $"Mappa {_project.Maps.Count + 1}";

                    var newMap = _project.AddMap(mName, (int)numW.Value, (int)numH.Value);
                    _canvas.Map = newMap;
                    UpdateMapListCombo();
                    UpdateMapInfo();
                    UpdateQuickAsm();
                    CenterCanvas();
                    ProjectModified?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void ActionDuplicateMap()
        {
            if (_project?.CurrentMap == null) return;
            var dup = _project.DuplicateCurrentMap();
            _canvas.Map = dup;
            UpdateMapListCombo();
            UpdateMapInfo();
            UpdateQuickAsm();
            CenterCanvas();
            ProjectModified?.Invoke(this, EventArgs.Empty);
        }

        public void ActionDeleteMap()
        {
            if (_project == null || _project.Maps.Count <= 1)
            {
                MessageBox.Show("Il progetto deve contenere almeno una mappa di gioco.", "Avviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show($"Sei sicuro di voler eliminare la mappa '{_project.CurrentMap.Name}'?", "Conferma Eliminazione", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _project.RemoveCurrentMap();
                _canvas.Map = _project.CurrentMap;
                UpdateMapListCombo();
                UpdateMapInfo();
                UpdateQuickAsm();
                CenterCanvas();
                ProjectModified?.Invoke(this, EventArgs.Empty);
            }
        }

        public void ActionRenameMap()
        {
            if (_project?.CurrentMap == null) return;
            using (Form dlg = new Form())
            {
                dlg.Text = "Rinomina Mappa";
                dlg.Size = new Size(340, 160);
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.MaximizeBox = false;
                dlg.MinimizeBox = false;
                dlg.BackColor = Color.FromArgb(32, 32, 38);
                dlg.ForeColor = Color.White;

                Label lbl = new Label { Text = "Nuovo nome mappa:", Left = 20, Top = 15, AutoSize = true };
                TextBox txt = new TextBox { Left = 20, Top = 40, Width = 285, Text = _project.CurrentMap.Name, BackColor = Color.FromArgb(20, 20, 25), ForeColor = Color.White };
                Button btnOk = new Button { Text = "Salva", Left = 125, Top = 78, Width = 85, Height = 30, BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DialogResult = DialogResult.OK };
                Button btnCancel = new Button { Text = "Annulla", Left = 220, Top = 78, Width = 85, Height = 30, BackColor = Color.FromArgb(60, 60, 70), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DialogResult = DialogResult.Cancel };

                dlg.Controls.AddRange(new Control[] { lbl, txt, btnOk, btnCancel });
                dlg.AcceptButton = btnOk;
                dlg.CancelButton = btnCancel;

                if (dlg.ShowDialog(this) == DialogResult.OK && !string.IsNullOrWhiteSpace(txt.Text))
                {
                    _project.CurrentMap.Name = txt.Text.Trim();
                    UpdateMapListCombo();
                    UpdateQuickAsm();
                    ProjectModified?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void ActionResizeMap()
        {
            if (_project?.CurrentMap == null) return;
            var map = _project.CurrentMap;

            using (Form dlg = new Form())
            {
                dlg.Text = "Dimensioni Mappa";
                dlg.Size = new Size(340, 210);
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.MaximizeBox = false;
                dlg.MinimizeBox = false;
                dlg.BackColor = Color.FromArgb(32, 32, 38);
                dlg.ForeColor = Color.White;

                Label lblW = new Label { Text = "Larghezza (celle tile 8x8):", Left = 20, Top = 20, AutoSize = true };
                NumericUpDown numW = new NumericUpDown { Left = 20, Top = 44, Width = 130, Minimum = 1, Maximum = 128, Value = map.Width, BackColor = Color.FromArgb(20, 20, 25), ForeColor = Color.White };

                Label lblH = new Label { Text = "Altezza (celle tile 8x8):", Left = 170, Top = 20, AutoSize = true };
                NumericUpDown numH = new NumericUpDown { Left = 170, Top = 44, Width = 135, Minimum = 1, Maximum = 64, Value = map.Height, BackColor = Color.FromArgb(20, 20, 25), ForeColor = Color.White };

                Label lblHint = new Label { Text = "Tile: 8x8 px. I dati esistenti verranno preservati.", Left = 20, Top = 82, AutoSize = true, ForeColor = Color.LightGray, Font = new Font("Segoe UI", 7.8f) };

                Button btnOk = new Button { Text = "Applica", Left = 115, Top = 120, Width = 90, Height = 32, BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DialogResult = DialogResult.OK };
                Button btnCancel = new Button { Text = "Annulla", Left = 215, Top = 120, Width = 90, Height = 32, BackColor = Color.FromArgb(60, 60, 70), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DialogResult = DialogResult.Cancel };

                dlg.Controls.AddRange(new Control[] { lblW, numW, lblH, numH, lblHint, btnOk, btnCancel });
                dlg.AcceptButton = btnOk;
                dlg.CancelButton = btnCancel;

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    map.Resize((int)numW.Value, (int)numH.Value);
                    _canvas.UpdateControlSize();
                    _canvas.Invalidate();
                    UpdateMapInfo();
                    UpdateQuickAsm();
                    CenterCanvas();
                    ProjectModified?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void ActionClearMap()
        {
            if (_project?.CurrentMap == null) return;
            if (MessageBox.Show("Vuoi pulire l'intera mappa impostando tutte le celle a Vuoto?", "Pulisci Mappa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _project.CurrentMap.Clear(0);
                _canvas.Invalidate();
                UpdateQuickAsm();
                ProjectModified?.Invoke(this, EventArgs.Empty);
            }
        }

        public void ActionUndo()
        {
            if (_project?.CurrentMap != null && _project.CurrentMap.Undo())
            {
                _canvas.Invalidate();
                UpdateQuickAsm();
                ProjectModified?.Invoke(this, EventArgs.Empty);
            }
        }

        public void ActionRedo()
        {
            if (_project?.CurrentMap != null && _project.CurrentMap.Redo())
            {
                _canvas.Invalidate();
                UpdateQuickAsm();
                ProjectModified?.Invoke(this, EventArgs.Empty);
            }
        }

        public void ActionZoom(int delta)
        {
            _canvas.ScaleFactor += delta;
            _lblZoom.Text = $"{_canvas.ScaleFactor}x";
            CenterCanvas();
        }

        public void ActionExportMap()
        {
            if (_project?.CurrentMap == null) return;

            using (Form dlg = new Form())
            {
                dlg.Text = "Esporta Dati Mappa di Gioco ZX Spectrum";
                dlg.Size = new Size(680, 520);
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.BackColor = Color.FromArgb(30, 30, 36);
                dlg.ForeColor = Color.White;

                TabControl tc = new TabControl { Dock = DockStyle.Fill };
                TabPage tabAsm = new TabPage("Assembly Z80");
                TabPage tabC = new TabPage("C / Z88DK");
                TabPage tabBasic = new TabPage("Sinclair BASIC");

                TextBox txtAsm = new TextBox { Dock = DockStyle.Fill, Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Both, BackColor = Color.FromArgb(20, 20, 25), ForeColor = Color.FromArgb(180, 255, 180), Font = new Font("Consolas", 8.8f) };
                txtAsm.Text = MapExporter.ExportToAssembly(_project.CurrentMap, AsmDirective.Defb, exportAllMaps: false, project: _project);
                tabAsm.Controls.Add(txtAsm);

                TextBox txtC = new TextBox { Dock = DockStyle.Fill, Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Both, BackColor = Color.FromArgb(20, 20, 25), ForeColor = Color.FromArgb(180, 220, 255), Font = new Font("Consolas", 8.8f) };
                txtC.Text = MapExporter.ExportToC(_project.CurrentMap, exportAllMaps: false, project: _project);
                tabC.Controls.Add(txtC);

                TextBox txtBasic = new TextBox { Dock = DockStyle.Fill, Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Both, BackColor = Color.FromArgb(20, 20, 25), ForeColor = Color.FromArgb(255, 230, 150), Font = new Font("Consolas", 8.8f) };
                txtBasic.Text = MapExporter.ExportToBasic(_project.CurrentMap);
                tabBasic.Controls.Add(txtBasic);

                tc.TabPages.AddRange(new TabPage[] { tabAsm, tabC, tabBasic });

                Panel bottom = new Panel { Dock = DockStyle.Bottom, Height = 44, BackColor = Color.FromArgb(24, 24, 28) };
                Button btnCopy = new Button { Text = "📋 Copia Codice", Left = 15, Top = 8, Width = 140, Height = 28, BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                btnCopy.Click += (s, e) =>
                {
                    TextBox activeBox = (tc.SelectedTab == tabAsm) ? txtAsm : ((tc.SelectedTab == tabC) ? txtC : txtBasic);
                    Clipboard.SetText(activeBox.Text);
                    MessageBox.Show("Codice esportato copiato negli appunti!", "Copia Riuscita", MessageBoxButtons.OK, MessageBoxIcon.Information);
                };

                Button btnSaveFile = new Button { Text = "💾 Salva su File...", Left = 165, Top = 8, Width = 140, Height = 28, BackColor = Color.FromArgb(40, 130, 80), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                btnSaveFile.Click += (s, e) =>
                {
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        if (tc.SelectedTab == tabAsm) { sfd.Filter = "Assembly Z80 (*.asm;*.z80)|*.asm;*.z80"; sfd.FileName = "map.asm"; }
                        else if (tc.SelectedTab == tabC) { sfd.Filter = "C Source (*.c;*.h)|*.c;*.h"; sfd.FileName = "map.c"; }
                        else { sfd.Filter = "Sinclair BASIC (*.bas;*.txt)|*.bas;*.txt"; sfd.FileName = "map.bas"; }

                        if (sfd.ShowDialog(dlg) == DialogResult.OK)
                        {
                            TextBox activeBox = (tc.SelectedTab == tabAsm) ? txtAsm : ((tc.SelectedTab == tabC) ? txtC : txtBasic);
                            File.WriteAllText(sfd.FileName, activeBox.Text, System.Text.Encoding.UTF8);
                            MessageBox.Show($"File salvato con successo:\n{sfd.FileName}", "Salvataggio Completato", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                };

                Button btnClose = new Button { Text = "Chiudi", Left = 560, Top = 8, Width = 90, Height = 28, BackColor = Color.FromArgb(60, 60, 70), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DialogResult = DialogResult.OK };

                bottom.Controls.AddRange(new Control[] { btnCopy, btnSaveFile, btnClose });

                dlg.Controls.Add(tc);
                dlg.Controls.Add(bottom);
                dlg.ShowDialog(this);
            }
        }

        public void ActionExportPng()
        {
            if (_project?.CurrentMap == null) return;
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Immagine PNG (*.png)|*.png";
                sfd.FileName = $"{_project.CurrentMap.Name}.png";
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    MapExporter.ExportToPng(sfd.FileName, _project.CurrentMap, _project, scale: 2);
                    MessageBox.Show($"Mappa salvata come immagine PNG:\n{sfd.FileName}", "Esportazione Completata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
