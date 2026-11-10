using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
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
        private bool _isUpdatingTilesPerMap = false;

        // Controlli specifici per Manic Miner (SkoolKit 45056)
        private ToolStripDropDownButton _btnManicMiner;
        private ToolStripSeparator _sepMM;
        private ToolStripButton _btnToolWilly;
        private ToolStripButton _btnToolPortal;
        private ToolStripButton _btnToolItem;
        private ToolStripButton _btnToolGuardian;
        private ToolStripButton _btnToolMMProps;

        public event EventHandler ProjectModified;
        public event EventHandler RequestSwitchToSprites;
        public event EventHandler<int> RequestSwitchToTiles;

        public MapCompressionMode SelectedCompressionMode { get; set; } = MapCompressionMode.Packed2Bit;

        public MapEditorView()
        {
            InitializeComponent();

            _pnlAssignedTiles.AllowDrop = true;
            _pnlAssignedTiles.DragEnter += PnlAssignedTiles_DragEnter;
            _pnlAssignedTiles.DragOver += PnlAssignedTiles_DragOver;
            _pnlAssignedTiles.DragDrop += PnlAssignedTiles_DragDrop;

            InitializeManicMinerControls();

            _rightPanel.Resize += (s, e) => LayoutRightPanel();
            LayoutRightPanel();
        }

        private void LayoutRightPanel()
        {
            if (_rightPanel == null || _pnlAssignedTiles == null || _pnlLibraryTiles == null) return;

            int panelWidth = _rightPanel.ClientSize.Width - 20;
            if (panelWidth < 100) panelWidth = 260;

            if (_previewBox != null)
                _previewBox.Width = panelWidth;

            if (_numTilesPerMap != null && _lblTilesPerMap != null)
            {
                _numTilesPerMap.Left = 10 + panelWidth - _numTilesPerMap.Width;
                _lblTilesPerMap.Left = _numTilesPerMap.Left - _lblTilesPerMap.Width - 6;
            }

            int topAssigned = 126;
            int middleHeight = 56; // 8px + 14px + 4px + 24px + 6px
            int bottomMargin = 10;
            int avail = _rightPanel.ClientSize.Height - topAssigned - middleHeight - bottomMargin;
            int eachPanelH = Math.Max(160, avail / 2);

            _pnlAssignedTiles.SetBounds(10, topAssigned, panelWidth, eachPanelH);

            int topTitle = _pnlAssignedTiles.Bottom + 8;
            _lblLibraryTitle.Location = new Point(10, topTitle);

            int topButtons = _lblLibraryTitle.Bottom + 4;
            int btnW = (panelWidth - 6) / 2;
            _btnAddTile.SetBounds(10, topButtons, btnW, 24);
            _btnGoToTileEditor.SetBounds(10 + btnW + 6, topButtons, panelWidth - btnW - 6, 24);

            int topLibrary = _btnAddTile.Bottom + 6;
            int libraryH = Math.Max(160, _rightPanel.ClientSize.Height - topLibrary - bottomMargin);
            _pnlLibraryTiles.SetBounds(10, topLibrary, panelWidth, libraryH);
        }

        private void NumTilesPerMap_ValueChanged(object sender, EventArgs e)
        {
            if (_isUpdatingTilesPerMap || _project == null || _numTilesPerMap == null) return;
            int val = (int)_numTilesPerMap.Value;
            if (_project.TilesPerMap != val)
            {
                _project.TilesPerMap = val;
                RefreshTilesetPalette();
                UpdateQuickAsm();
                ProjectModified?.Invoke(this, EventArgs.Empty);
            }
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
                if (_numTilesPerMap != null)
                {
                    _isUpdatingTilesPerMap = true;
                    _numTilesPerMap.Enabled = (_project != null);
                    if (_project != null)
                    {
                        _numTilesPerMap.Value = Math.Max(1, Math.Min(256, _project.TilesPerMap));
                    }
                    _isUpdatingTilesPerMap = false;
                }
                _canvas.Project = _project;
                UpdateMapListCombo();
                SwitchToMapIndex(_project?.SelectedMapIndex ?? 0);
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
            if (_btnToolWilly != null) _btnToolWilly.Checked = (activeBtn == _btnToolWilly);
            if (_btnToolPortal != null) _btnToolPortal.Checked = (activeBtn == _btnToolPortal);
            if (_btnToolItem != null) _btnToolItem.Checked = (activeBtn == _btnToolItem);
            if (_btnToolGuardian != null) _btnToolGuardian.Checked = (activeBtn == _btnToolGuardian);
        }

        private void CenterPanel_Resize(object sender, EventArgs e) => CenterCanvas();

        private void BtnNewMap_Click(object sender, EventArgs e) => ActionNewMap();
        private void BtnCloneMap_Click(object sender, EventArgs e) => ActionDuplicateMap();
        private void BtnDeleteMap_Click(object sender, EventArgs e) => ActionDeleteMap();
        private void BtnRenameMap_Click(object sender, EventArgs e) => ActionRenameMap();
        private void BtnResizeMap_Click(object sender, EventArgs e) => ActionResizeMap();
        private void BtnExport_Click(object sender, EventArgs e) => ActionExportMap();
        private void BtnImport_Click(object sender, EventArgs e) => ActionImportMap();
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

        public void RefreshView()
        {
            UpdateMapListCombo();
            SwitchToMapIndex(_project?.SelectedMapIndex ?? 0);
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

        private void SwitchToMapIndex(int index)
        {
            if (_project == null || index < 0 || index >= _project.Maps.Count) return;

            _project.SelectedMapIndex = index;
            _canvas.Map = _project.CurrentMap;

            var map = _project.CurrentMap;
            if (map != null && map.AssignedTileIds != null && map.AssignedTileIds.Count > 0)
            {
                if (!map.AssignedTileIds.Contains(_canvas.ActiveTileId))
                {
                    _canvas.ActiveTileId = map.AssignedTileIds[0];
                }
            }

            RefreshTilesetPalette();
            UpdateMapInfo();
            UpdateQuickAsm();
            UpdateManicMinerUiState();
            CenterCanvas();
            _canvas.Invalidate();
        }

        private void CmbMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isUpdatingMapCombo || _project == null) return;

            int idx = _cmbMaps.SelectedIndex;
            if (idx >= 0 && idx < _project.Maps.Count)
            {
                SwitchToMapIndex(idx);
            }
        }

        public void RefreshTilesetPalette()
        {
            if (_project == null) return;
            if (_pnlAssignedTiles == null || _pnlLibraryTiles == null) return;

            // Sposta temporaneamente il focus fuori dai pannelli per evitare che WinForms auto-scrolli durante la ricreazione dei controlli
            if (_canvas != null && _canvas.CanFocus)
            {
                _canvas.Focus();
            }

            // Salva le posizioni correnti di scroll
            int assignedScrollY = -_pnlAssignedTiles.AutoScrollPosition.Y;
            int libraryScrollY = -_pnlLibraryTiles.AutoScrollPosition.Y;

            // In Windows Forms, se un pannello con AutoScroll è scrollato (DisplayRectangle.Y < 0),
            // l'aggiunta di controlli con Top=y viene sfalsata, accumulando spazi vuoti a ogni refresh.
            // Reimpostare AutoScrollPosition a (0, 0) prima di svuotare e ripopolare previene disallineamenti.
            _pnlAssignedTiles.AutoScrollPosition = new Point(0, 0);
            _pnlLibraryTiles.AutoScrollPosition = new Point(0, 0);

            _pnlAssignedTiles.SuspendLayout();
            _pnlLibraryTiles.SuspendLayout();

            while (_pnlAssignedTiles.Controls.Count > 0)
            {
                var c = _pnlAssignedTiles.Controls[0];
                _pnlAssignedTiles.Controls.RemoveAt(0);
                c.Dispose();
            }
            while (_pnlLibraryTiles.Controls.Count > 0)
            {
                var c = _pnlLibraryTiles.Controls[0];
                _pnlLibraryTiles.Controls.RemoveAt(0);
                c.Dispose();
            }

            _pnlAssignedTiles.HorizontalScroll.Maximum = 0;
            _pnlAssignedTiles.HorizontalScroll.Enabled = false;
            _pnlAssignedTiles.HorizontalScroll.Visible = false;

            _pnlLibraryTiles.HorizontalScroll.Maximum = 0;
            _pnlLibraryTiles.HorizontalScroll.Enabled = false;
            _pnlLibraryTiles.HorizontalScroll.Visible = false;

            var currentMap = _project.CurrentMap;
            if (currentMap == null)
            {
                _pnlAssignedTiles.ResumeLayout(true);
                _pnlLibraryTiles.ResumeLayout(true);
                return;
            }

            if (currentMap.AssignedTileIds == null)
                currentMap.AssignedTileIds = new List<int>();
            currentMap.AssignedTileIds.RemoveAll(id => id <= 0 || id > _project.Tiles.Count);

            // Calcolo utilizzi di ogni tile sulla mappa corrente
            var counts = new Dictionary<int, int>();
            for (int cy = 0; cy < currentMap.Height; cy++)
            {
                for (int cx = 0; cx < currentMap.Width; cx++)
                {
                    int tid = currentMap.GetTile(cx, cy);
                    if (tid > 0)
                        counts[tid] = counts.ContainsKey(tid) ? counts[tid] + 1 : 1;
                }
            }

            int assignedCount = currentMap.AssignedTileIds.Count;
            int maxPerMap = _project.TilesPerMap;
            _lblPalTitle.Text = $"📌 SET MAPPA ({assignedCount}/{maxPerMap})";
            if (_lblLibraryTitle != null)
                _lblLibraryTitle.Text = $"📚 LIBRERIA TILE ({_project.Tiles.Count} totali)";

            int cardW = Math.Max(210, _pnlAssignedTiles.ClientSize.Width - 8);
            int cardH = 46;

            int totalAssignedH = 4;
            // --- 1. POPOLAMENTO PANNELLO ASSEGNATE (SET MAPPA) ---
            if (currentMap.AssignedTileIds.Count == 0)
            {
                Label emptyLabel = new Label
                {
                    Text = "Nessuna tile assegnata a questa mappa.\n\nTrascina qui le tile dalla Libreria sottostante\noppure fai doppio clic su una tile per aggiungerla.",
                    Dock = DockStyle.Fill,
                    ForeColor = Color.FromArgb(140, 160, 180),
                    Font = new Font("Segoe UI", 7.8f, FontStyle.Italic),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                _pnlAssignedTiles.Controls.Add(emptyLabel);
            }
            else
            {
                int y = 4;
                for (int slotIdx = 0; slotIdx < currentMap.AssignedTileIds.Count; slotIdx++)
                {
                    int slotNum = slotIdx;
                    int tileId = currentMap.AssignedTileIds[slotIdx];
                    if (tileId <= 0 || tileId > _project.Tiles.Count) continue;

                    var tile = _project.Tiles[tileId - 1];
                    counts.TryGetValue(tileId, out int cnt);
                    Bitmap thumb = tile.RenderToBitmap(1);

                    Panel card = CreateAssignedTileCard(tileId, slotNum, tile.Name, thumb, y, cardW, cardH, cnt, slotIdx, currentMap.AssignedTileIds.Count);
                    _pnlAssignedTiles.Controls.Add(card);
                    y += cardH + 4;
                }
                totalAssignedH = y;
            }

            // --- 2. POPOLAMENTO PANNELLO LIBRERIA TILE ---
            int libY = 4;
            int libCardW = Math.Max(210, _pnlLibraryTiles.ClientSize.Width - 8);

            for (int i = 0; i < _project.Tiles.Count; i++)
            {
                int tileId = i + 1;
                var tile = _project.Tiles[i];
                counts.TryGetValue(tileId, out int cnt);
                Bitmap thumb = tile.RenderToBitmap(1);

                int assignedSlot = currentMap.AssignedTileIds.IndexOf(tileId);
                bool isAssigned = (assignedSlot >= 0);

                Panel card = CreateLibraryTileCard(tileId, tile.Name, thumb, libY, libCardW, cardH, isAssigned, assignedSlot, cnt);
                _pnlLibraryTiles.Controls.Add(card);
                libY += cardH + 4;
            }
            int totalLibH = libY;

            _pnlAssignedTiles.ResumeLayout(true);
            _pnlLibraryTiles.ResumeLayout(true);

            // Calcola e ripristina lo scroll massimo consentito per evitare spazi neri
            int maxAssignedScroll = Math.Max(0, totalAssignedH - _pnlAssignedTiles.ClientSize.Height);
            if (assignedScrollY > 0 && maxAssignedScroll > 0)
                _pnlAssignedTiles.AutoScrollPosition = new Point(0, Math.Min(assignedScrollY, maxAssignedScroll));
            else
                _pnlAssignedTiles.AutoScrollPosition = new Point(0, 0);

            int maxLibScroll = Math.Max(0, totalLibH - _pnlLibraryTiles.ClientSize.Height);
            if (libraryScrollY > 0 && maxLibScroll > 0)
                _pnlLibraryTiles.AutoScrollPosition = new Point(0, Math.Min(libraryScrollY, maxLibScroll));
            else
                _pnlLibraryTiles.AutoScrollPosition = new Point(0, 0);

            UpdateActiveTileDisplay();
        }

        private Panel CreateAssignedTileCard(int tileId, int slotNum, string name, Bitmap thumb, int top, int width, int height, int usageCount, int slotIndex, int totalSlots)
        {
            bool isSelected = (_canvas.ActiveTileId == tileId);
            bool isBeyondLimit = (_project.TilesPerMap > 0 && slotNum >= _project.TilesPerMap);

            Panel card = new Panel
            {
                Top = top,
                Left = 4,
                Width = width,
                Height = height,
                BackColor = isSelected ? Color.FromArgb(40, 75, 115) : (isBeyondLimit ? Color.FromArgb(45, 25, 25) : Color.FromArgb(28, 30, 38)),
                BorderStyle = isSelected ? BorderStyle.FixedSingle : BorderStyle.None,
                Cursor = Cursors.Hand,
                Tag = tileId
            };

            // Badge slot (#0, #1, ...)
            Label lblSlot = new Label
            {
                Top = 6,
                Left = 4,
                Width = 26,
                Height = 34,
                Text = $"#{slotNum}",
                Font = new Font("Segoe UI", 7.8f, FontStyle.Bold),
                ForeColor = isBeyondLimit ? Color.FromArgb(255, 120, 100) : Color.FromArgb(0, 230, 255),
                BackColor = Color.FromArgb(16, 20, 28),
                TextAlign = ContentAlignment.MiddleCenter
            };

            PictureBox pic = new PictureBox
            {
                Top = 7,
                Left = 33,
                Width = 32,
                Height = 32,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Black,
                Image = thumb
            };

            Label lblTitle = new Label
            {
                Top = 4,
                Left = 69,
                Width = width - 122,
                Height = 18,
                Text = $"Tile #{tileId - 1}: {name}",
                ForeColor = isSelected ? Color.Cyan : Color.White,
                Font = new Font("Segoe UI", 8f, isSelected ? FontStyle.Bold : FontStyle.Regular),
                AutoEllipsis = true,
                Tag = "title"
            };

            string statusText = usageCount > 0 ? $"Usata: {usageCount}x" : "Non usata";
            if (_project?.CurrentMap?.IsManicMinerMode == true && slotNum >= 0 && slotNum < 8)
            {
                var mmDefs = _project.CurrentMap.ManicMinerData.TileDefs;
                string role = (mmDefs != null && mmDefs[slotNum] != null) ? mmDefs[slotNum].RoleName : "";
                statusText = $"[{role}] • {statusText}";
            }
            else if (isBeyondLimit) statusText += " (Fuori limite)";
            Label lblStatus = new Label
            {
                Top = 24,
                Left = 69,
                Width = width - 122,
                Height = 16,
                Text = statusText,
                ForeColor = usageCount > 0 ? Color.FromArgb(120, 230, 160) : Color.FromArgb(160, 170, 185),
                Font = new Font("Segoe UI", 7.2f, FontStyle.Regular),
                AutoEllipsis = true,
                Tag = "status"
            };

            // Pulsante ▲ Sposta Su
            Button btnUp = new Button
            {
                Top = 4,
                Left = width - 50,
                Width = 22,
                Height = 18,
                Text = "▲",
                Font = new Font("Segoe UI", 6f, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(40, 44, 56),
                ForeColor = (slotIndex > 0) ? Color.White : Color.FromArgb(80, 80, 90),
                Enabled = (slotIndex > 0),
                Cursor = (slotIndex > 0) ? Cursors.Hand : Cursors.Default
            };
            btnUp.FlatAppearance.BorderSize = 0;
            btnUp.Click += (s, e) => MoveAssignedTileSlot(tileId, -1);

            // Pulsante ▼ Sposta Giù
            Button btnDown = new Button
            {
                Top = 24,
                Left = width - 50,
                Width = 22,
                Height = 18,
                Text = "▼",
                Font = new Font("Segoe UI", 6f, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(40, 44, 56),
                ForeColor = (slotIndex < totalSlots - 1) ? Color.White : Color.FromArgb(80, 80, 90),
                Enabled = (slotIndex < totalSlots - 1),
                Cursor = (slotIndex < totalSlots - 1) ? Cursors.Hand : Cursors.Default
            };
            btnDown.FlatAppearance.BorderSize = 0;
            btnDown.Click += (s, e) => MoveAssignedTileSlot(tileId, 1);

            // Pulsante ✖ Rimuovi dal Set Mappa
            Button btnRemove = new Button
            {
                Top = 4,
                Left = width - 26,
                Width = 22,
                Height = 38,
                Text = "✖",
                Font = new Font("Segoe UI", 8.2f, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(45, 25, 25),
                ForeColor = Color.FromArgb(255, 120, 100),
                Cursor = Cursors.Hand
            };
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.Click += (s, e) => UnassignTileFromCurrentMap(tileId);

            Action selectCard = () =>
            {
                _canvas.ActiveTileId = tileId;
                RefreshTilesetSelection();
                UpdateActiveTileDisplay();
            };

            card.Click += (s, e) => selectCard();
            pic.Click += (s, e) => selectCard();
            lblSlot.Click += (s, e) => selectCard();
            lblTitle.Click += (s, e) => selectCard();
            lblStatus.Click += (s, e) => selectCard();

            card.Controls.AddRange(new Control[] { lblSlot, pic, lblTitle, lblStatus, btnUp, btnDown, btnRemove });
            return card;
        }

        private Panel CreateLibraryTileCard(int tileId, string name, Bitmap thumb, int top, int width, int height, bool isAssigned, int assignedSlot, int usageCount)
        {
            bool isSelected = (_canvas.ActiveTileId == tileId);

            Panel card = new Panel
            {
                Top = top,
                Left = 4,
                Width = width,
                Height = height,
                BackColor = isSelected ? Color.FromArgb(40, 70, 105) : (isAssigned ? Color.FromArgb(24, 32, 28) : Color.FromArgb(28, 28, 34)),
                BorderStyle = isSelected ? BorderStyle.FixedSingle : BorderStyle.None,
                Cursor = Cursors.Hand,
                Tag = tileId
            };

            PictureBox pic = new PictureBox
            {
                Top = 7,
                Left = 6,
                Width = 32,
                Height = 32,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Black,
                Image = thumb
            };

            Label lblTitle = new Label
            {
                Top = 4,
                Left = 44,
                Width = width - 105,
                Height = 18,
                Text = $"Tile #{tileId - 1}: {name}",
                ForeColor = isSelected ? Color.Cyan : Color.White,
                Font = new Font("Segoe UI", 8f, isSelected ? FontStyle.Bold : FontStyle.Regular),
                AutoEllipsis = true,
                Tag = "title"
            };

            string statusText = isAssigned ? $"✔ Nel set (Slot #{assignedSlot})" : (usageCount > 0 ? $"⚠ Usata {usageCount}x (non nel set)" : "➕ Trascina per assegnare");
            Color statusColor = isAssigned ? Color.FromArgb(120, 230, 160) : (usageCount > 0 ? Color.FromArgb(255, 130, 100) : Color.FromArgb(140, 180, 220));
            Label lblStatus = new Label
            {
                Top = 24,
                Left = 44,
                Width = width - 105,
                Height = 16,
                Text = statusText,
                ForeColor = statusColor,
                Font = new Font("Segoe UI", 7.2f, FontStyle.Regular),
                AutoEllipsis = true,
                Tag = "status"
            };

            Button btnAction = new Button
            {
                Top = 8,
                Left = width - 58,
                Width = 52,
                Height = 30,
                Text = isAssigned ? "✖ Rimuovi" : "➕ Assegna",
                Font = new Font("Segoe UI", 6.8f, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = isAssigned ? Color.FromArgb(50, 24, 24) : Color.FromArgb(0, 100, 180),
                ForeColor = isAssigned ? Color.FromArgb(255, 140, 120) : Color.White,
                Cursor = Cursors.Hand,
                Enabled = true
            };
            btnAction.FlatAppearance.BorderSize = 0;
            btnAction.Click += (s, e) =>
            {
                if (isAssigned)
                {
                    UnassignTileFromCurrentMap(tileId);
                }
                else
                {
                    AssignTileToCurrentMap(tileId);
                }
            };

            Action selectCard = () =>
            {
                _canvas.ActiveTileId = tileId;
                RefreshTilesetSelection();
                UpdateActiveTileDisplay();
            };

            Action assignAction = () =>
            {
                if (!isAssigned)
                {
                    AssignTileToCurrentMap(tileId);
                }
                else
                {
                    UnassignTileFromCurrentMap(tileId);
                }
            };

            // Drag & Drop: avvia DoDragDrop al trascinamento del mouse
            Point dragStart = Point.Empty;
            Action<Control> wireDrag = (ctrl) =>
            {
                ctrl.MouseDown += (s, e) =>
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        dragStart = e.Location;
                    }
                };
                ctrl.MouseMove += (s, e) =>
                {
                    if (e.Button == MouseButtons.Left && dragStart != Point.Empty)
                    {
                        int dx = Math.Abs(e.X - dragStart.X);
                        int dy = Math.Abs(e.Y - dragStart.Y);
                        if (dx > SystemInformation.DragSize.Width || dy > SystemInformation.DragSize.Height)
                        {
                            dragStart = Point.Empty;
                            card.DoDragDrop(new TileDragData(tileId), DragDropEffects.Copy);
                        }
                    }
                };
            };

            wireDrag(card);
            wireDrag(pic);
            wireDrag(lblTitle);
            wireDrag(lblStatus);

            card.Click += (s, e) => selectCard();
            pic.Click += (s, e) => selectCard();
            lblTitle.Click += (s, e) => selectCard();
            lblStatus.Click += (s, e) => selectCard();

            card.DoubleClick += (s, e) => assignAction();
            pic.DoubleClick += (s, e) => assignAction();
            lblTitle.DoubleClick += (s, e) => assignAction();
            lblStatus.DoubleClick += (s, e) => assignAction();

            card.Controls.AddRange(new Control[] { pic, lblTitle, lblStatus, btnAction });
            return card;
        }

        private void RefreshTilesetSelection()
        {
            int currentTile = _canvas.ActiveTileId;

            if (_pnlAssignedTiles != null)
            {
                foreach (Control c in _pnlAssignedTiles.Controls)
                {
                    if (c is Panel card && card.Tag is int tileId)
                    {
                        bool isSelected = (tileId == currentTile);
                        card.BackColor = isSelected ? Color.FromArgb(40, 75, 115) : Color.FromArgb(28, 30, 38);
                        card.BorderStyle = isSelected ? BorderStyle.FixedSingle : BorderStyle.None;
                        foreach (Control child in card.Controls)
                        {
                            if (child is Label lbl && lbl.Tag as string == "title")
                            {
                                lbl.ForeColor = isSelected ? Color.Cyan : Color.White;
                                lbl.Font = new Font("Segoe UI", 8f, isSelected ? FontStyle.Bold : FontStyle.Regular);
                            }
                        }
                    }
                }
            }

            if (_pnlLibraryTiles != null)
            {
                foreach (Control c in _pnlLibraryTiles.Controls)
                {
                    if (c is Panel card && card.Tag is int tileId)
                    {
                        bool isSelected = (tileId == currentTile);
                        int assignedSlot = (_project?.CurrentMap?.AssignedTileIds != null) ? _project.CurrentMap.AssignedTileIds.IndexOf(tileId) : -1;
                        bool isAssigned = (assignedSlot >= 0);
                        card.BackColor = isSelected ? Color.FromArgb(40, 70, 105) : (isAssigned ? Color.FromArgb(24, 32, 28) : Color.FromArgb(28, 28, 34));
                        card.BorderStyle = isSelected ? BorderStyle.FixedSingle : BorderStyle.None;
                        foreach (Control child in card.Controls)
                        {
                            if (child is Label lbl && lbl.Tag as string == "title")
                            {
                                lbl.ForeColor = isSelected ? Color.Cyan : Color.White;
                                lbl.Font = new Font("Segoe UI", 8f, isSelected ? FontStyle.Bold : FontStyle.Regular);
                            }
                        }
                    }
                }
            }
        }

        private void UpdateTileUsageLabels()
        {
            if (_project == null) return;
            var map = _project.CurrentMap;
            if (map == null) return;

            var counts = new Dictionary<int, int>();
            for (int cy = 0; cy < map.Height; cy++)
            {
                for (int cx = 0; cx < map.Width; cx++)
                {
                    int tid = map.GetTile(cx, cy);
                    if (tid > 0)
                        counts[tid] = counts.ContainsKey(tid) ? counts[tid] + 1 : 1;
                }
            }

            if (_pnlAssignedTiles != null)
            {
                foreach (Control c in _pnlAssignedTiles.Controls)
                {
                    if (c is Panel card && card.Tag is int tileId)
                    {
                        counts.TryGetValue(tileId, out int cnt);
                        foreach (Control child in card.Controls)
                        {
                            if (child is Label lblStatus && lblStatus.Tag as string == "status")
                            {
                                int slotIdx = (map.AssignedTileIds != null) ? map.AssignedTileIds.IndexOf(tileId) : -1;
                                bool isBeyond = (slotIdx + 1 > _project.TilesPerMap);
                                string statusText = cnt > 0 ? $"Usata: {cnt}x" : "Non usata";
                                if (isBeyond) statusText += " (Fuori limite)";
                                lblStatus.Text = statusText;
                                lblStatus.ForeColor = cnt > 0 ? Color.FromArgb(120, 230, 160) : Color.FromArgb(160, 170, 185);
                            }
                        }
                    }
                }
            }

            if (_pnlLibraryTiles != null)
            {
                foreach (Control c in _pnlLibraryTiles.Controls)
                {
                    if (c is Panel card && card.Tag is int tileId)
                    {
                        counts.TryGetValue(tileId, out int cnt);
                        int assignedSlot = (map.AssignedTileIds != null) ? map.AssignedTileIds.IndexOf(tileId) : -1;
                        bool isAssigned = (assignedSlot >= 0);

                        foreach (Control child in card.Controls)
                        {
                            if (child is Label lblStatus && lblStatus.Tag as string == "status")
                            {
                                if (isAssigned)
                                {
                                    lblStatus.Text = cnt > 0 ? $"✔ Nel set (#{assignedSlot + 1}) • {cnt}x" : $"✔ Nel set (Slot #{assignedSlot + 1})";
                                    lblStatus.ForeColor = Color.FromArgb(120, 230, 160);
                                }
                                else
                                {
                                    lblStatus.Text = cnt > 0 ? $"⚠ Usata: {cnt}x (non nel set)" : "➕ Trascina per assegnare";
                                    lblStatus.ForeColor = cnt > 0 ? Color.FromArgb(255, 130, 100) : Color.FromArgb(140, 180, 220);
                                }
                            }
                        }
                    }
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
                _lblActiveTileName.ForeColor = Color.Yellow;
                _lblActiveTileName.Text = "[Cella Vuota / Gomma]\n(Nessuna tile - colore Paper)";
            }
            else if (_project != null && (tileId - 1) < _project.Tiles.Count)
            {
                var tile = _project.Tiles[tileId - 1];
                _picActiveTile.Image = tile.RenderToBitmap(2);

                int count = 0;
                var map = _project.CurrentMap;
                if (map != null)
                {
                    for (int cy = 0; cy < map.Height; cy++)
                    {
                        for (int cx = 0; cx < map.Width; cx++)
                        {
                            if (map.GetTile(cx, cy) == tileId) count++;
                        }
                    }
                }

                int assignedSlot = (map?.AssignedTileIds != null) ? map.AssignedTileIds.IndexOf(tileId) : -1;
                bool isAssigned = (assignedSlot >= 0);

                if (isAssigned)
                {
                    _lblActiveTileName.Text = $"Tile #{tileId - 1}: {tile.Name}\n✔ Set Mappa: Slot #{assignedSlot} • Usata: {count}x";
                    _lblActiveTileName.ForeColor = Color.Cyan;
                }
                else
                {
                    string warn = count > 0 ? $"⚠ Non nel set! Usata: {count}x (esportata come 0)" : "➕ Non nel set della mappa (non esportata)";
                    _lblActiveTileName.Text = $"Tile #{tileId - 1}: {tile.Name}\n{warn}";
                    _lblActiveTileName.ForeColor = Color.FromArgb(255, 140, 140);
                }
            }
            else
            {
                _picActiveTile.Image = null;
                _lblActiveTileName.ForeColor = Color.Yellow;
                _lblActiveTileName.Text = $"Tile #{tileId - 1}";
            }

            oldBmp?.Dispose();
        }

        private void PnlAssignedTiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TileDragData)) || e.Data.GetDataPresent(typeof(int)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void PnlAssignedTiles_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TileDragData)) || e.Data.GetDataPresent(typeof(int)))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void PnlAssignedTiles_DragDrop(object sender, DragEventArgs e)
        {
            int tileId = 0;
            if (e.Data.GetDataPresent(typeof(TileDragData)))
            {
                var data = (TileDragData)e.Data.GetData(typeof(TileDragData));
                tileId = data.TileId;
            }
            else if (e.Data.GetDataPresent(typeof(int)))
            {
                tileId = (int)e.Data.GetData(typeof(int));
            }

            if (tileId > 0)
            {
                AssignTileToCurrentMap(tileId);
            }
        }

        public void AssignTileToCurrentMap(int tileId)
        {
            if (_project?.CurrentMap == null || tileId <= 0) return;
            var map = _project.CurrentMap;
            if (map.AssignedTileIds == null)
                map.AssignedTileIds = new List<int>();

            if (map.AssignedTileIds.Contains(tileId))
            {
                int slot = map.AssignedTileIds.IndexOf(tileId);
                MessageBox.Show($"La tile #{tileId - 1} è già assegnata a questa mappa nello Slot #{slot}.", "Tile Già Assegnata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _canvas.ActiveTileId = tileId;
                RefreshTilesetSelection();
                UpdateActiveTileDisplay();
                return;
            }

            if (map.AssignedTileIds.Count >= _project.TilesPerMap)
            {
                var res = MessageBox.Show(
                    $"La mappa ha già raggiunto il limite di {_project.TilesPerMap} tile assegnate configurato per il progetto.\n\n" +
                    $"Vuoi incrementare il limite 'Tile/Mappa' del progetto per assegnare anche questa tile?",
                    "Limite Tile Raggiunto",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (res == DialogResult.Yes)
                {
                    _project.TilesPerMap++;
                    if (_numTilesPerMap != null)
                    {
                        _isUpdatingTilesPerMap = true;
                        _numTilesPerMap.Value = _project.TilesPerMap;
                        _isUpdatingTilesPerMap = false;
                    }
                }
                else
                {
                    return;
                }
            }

            map.AssignedTileIds.Add(tileId);
            _canvas.ActiveTileId = tileId;
            ProjectModified?.Invoke(this, EventArgs.Empty);
            RefreshTilesetPalette();
            UpdateQuickAsm();
        }

        public void UnassignTileFromCurrentMap(int tileId)
        {
            if (_project?.CurrentMap == null || tileId <= 0) return;
            var map = _project.CurrentMap;
            if (map.AssignedTileIds == null || !map.AssignedTileIds.Contains(tileId)) return;

            int usageCount = 0;
            for (int y = 0; y < map.Height; y++)
                for (int x = 0; x < map.Width; x++)
                    if (map.GetTile(x, y) == tileId) usageCount++;

            if (usageCount > 0)
            {
                var res = MessageBox.Show(
                    $"La tile #{tileId - 1} è attualmente utilizzata {usageCount} volte nella mappa.\n\n" +
                    $"Se la rimuovi dal set della mappa, le celle corrispondenti non verranno esportate (saranno esportate come vuoto/0).\n\n" +
                    $"Vuoi procedere con la rimozione dal set?",
                    "Conferma Rimozione Tile dal Set",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (res != DialogResult.Yes) return;
            }

            map.AssignedTileIds.Remove(tileId);

            if (_canvas.ActiveTileId == tileId)
            {
                _canvas.ActiveTileId = (map.AssignedTileIds.Count > 0) ? map.AssignedTileIds[0] : 0;
            }

            _canvas?.Focus();
            ProjectModified?.Invoke(this, EventArgs.Empty);
            RefreshTilesetPalette();
            UpdateQuickAsm();
            _canvas?.Invalidate();
        }

        public void MoveAssignedTileSlot(int tileId, int direction)
        {
            if (_project?.CurrentMap == null || tileId <= 0) return;
            var map = _project.CurrentMap;
            if (map.AssignedTileIds == null) return;

            int idx = map.AssignedTileIds.IndexOf(tileId);
            if (idx < 0) return;

            int newIdx = idx + direction;
            if (newIdx < 0 || newIdx >= map.AssignedTileIds.Count) return;

            map.AssignedTileIds.RemoveAt(idx);
            map.AssignedTileIds.Insert(newIdx, tileId);

            ProjectModified?.Invoke(this, EventArgs.Empty);
            RefreshTilesetPalette();
            UpdateQuickAsm();
        }

        private void UpdateMapInfo()
        {
        }

        private void UpdateQuickAsm()
        {
        }

        private void Canvas_HoverCoordinatesChanged(object sender, MapHoverEventArgs e)
        {
        }

        private void Canvas_MapChanged(object sender, EventArgs e)
        {
            ProjectModified?.Invoke(this, EventArgs.Empty);
            UpdateTileUsageLabels();
            UpdateActiveTileDisplay();
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
                    UpdateMapListCombo();
                    SwitchToMapIndex(_project.SelectedMapIndex);
                    ProjectModified?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void ActionDuplicateMap()
        {
            if (_project?.CurrentMap == null) return;
            var dup = _project.DuplicateCurrentMap();
            UpdateMapListCombo();
            SwitchToMapIndex(_project.SelectedMapIndex);
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
                UpdateMapListCombo();
                SwitchToMapIndex(_project.SelectedMapIndex);
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
                UpdateTileUsageLabels();
                UpdateActiveTileDisplay();
                UpdateQuickAsm();
                ProjectModified?.Invoke(this, EventArgs.Empty);
            }
        }

        public void ActionUndo()
        {
            if (_project?.CurrentMap != null && _project.CurrentMap.Undo())
            {
                _canvas.Invalidate();
                UpdateTileUsageLabels();
                UpdateActiveTileDisplay();
                UpdateQuickAsm();
                ProjectModified?.Invoke(this, EventArgs.Empty);
            }
        }

        public void ActionRedo()
        {
            if (_project?.CurrentMap != null && _project.CurrentMap.Redo())
            {
                _canvas.Invalidate();
                UpdateTileUsageLabels();
                UpdateActiveTileDisplay();
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

        public void ActionImportMap()
        {
            if (_project == null) return;

            var defaultFmt = (_project.CurrentMap != null && _project.CurrentMap.IsManicMinerMode)
                ? MapImportFormat.ManicMinerAttributes
                : MapImportFormat.Packed2Bit;

            using (var dlg = new ImportMapDialog(_project, defaultFmt))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    UpdateMapListCombo();
                    SwitchToMapIndex(_project.SelectedMapIndex);
                    ProjectModified?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void ActionExportMap()
        {
            if (_project?.CurrentMap == null) return;

            using (Form dlg = new Form())
            {
                dlg.Text = "Esporta Dati Mappa di Gioco ZX Spectrum";
                dlg.Size = new Size(740, 580);
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.BackColor = Color.FromArgb(30, 30, 36);
                dlg.ForeColor = Color.White;
                dlg.MinimumSize = new Size(620, 480);

                // Pannello superiore: componente di selezione della tipologia di mappa e configurazione tile
                Panel topPanel = new Panel { Dock = DockStyle.Top, Height = 72, BackColor = Color.FromArgb(24, 24, 28), Padding = new Padding(12, 10, 12, 6) };

                Label lblFormatTitle = new Label { Text = "Tipologia Mappa:", AutoSize = true, Left = 12, Top = 12, Font = new Font("Segoe UI", 9f, FontStyle.Bold), ForeColor = Color.FromArgb(0, 220, 255) };

                ComboBox cmbType = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(40, 40, 48),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9f),
                    Left = 135,
                    Top = 9,
                    Width = 230
                };
                cmbType.Items.AddRange(new object[] {
                    "Compresso 2-bit (4 tile/byte)",
                    "Standard 1:1 (1 byte/tile)",
                    "Entrambi (Standard + Compresso)",
                    "Manic Miner (1024 Byte - SkoolKit 45056)"
                });
                if (_project.CurrentMap.IsManicMinerMode)
                    cmbType.SelectedIndex = 3;
                else
                    cmbType.SelectedIndex = (SelectedCompressionMode == MapCompressionMode.Uncompressed) ? 1 : (SelectedCompressionMode == MapCompressionMode.Both ? 2 : 0);

                Label lblTilesTitle = new Label
                {
                    Text = "Tile/Mappa (prog):",
                    AutoSize = true,
                    Left = 385,
                    Top = 12,
                    Font = new Font("Segoe UI", 8.8f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(0, 220, 255)
                };

                NumericUpDown numExportTiles = new NumericUpDown
                {
                    Left = 515,
                    Top = 9,
                    Width = 60,
                    Minimum = 1,
                    Maximum = 256,
                    Value = Math.Max(1, Math.Min(256, _project.TilesPerMap)),
                    BackColor = Color.FromArgb(40, 40, 48),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 8.5f, FontStyle.Bold)
                };

                Label lblFormatInfo = new Label
                {
                    AutoSize = false,
                    Left = 12,
                    Top = 38,
                    Width = 700,
                    Height = 28,
                    Font = new Font("Segoe UI", 8.2f),
                    ForeColor = Color.FromArgb(200, 230, 200)
                };

                topPanel.Controls.AddRange(new Control[] { lblFormatTitle, cmbType, lblTilesTitle, numExportTiles, lblFormatInfo });

                TabControl tc = new TabControl { Dock = DockStyle.Fill };
                TabPage tabAsm = new TabPage("Assembly Z80");
                TabPage tabC = new TabPage("C / Z88DK");
                TabPage tabBasic = new TabPage("Sinclair BASIC");
                TabPage tabBin = new TabPage("File Binario (.bin)");

                TextBox txtAsm = new TextBox { Dock = DockStyle.Fill, Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Both, BackColor = Color.FromArgb(20, 20, 25), ForeColor = Color.FromArgb(180, 255, 180), Font = new Font("Consolas", 8.8f) };
                tabAsm.Controls.Add(txtAsm);

                TextBox txtC = new TextBox { Dock = DockStyle.Fill, Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Both, BackColor = Color.FromArgb(20, 20, 25), ForeColor = Color.FromArgb(180, 220, 255), Font = new Font("Consolas", 8.8f) };
                tabC.Controls.Add(txtC);

                TextBox txtBasic = new TextBox { Dock = DockStyle.Fill, Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Both, BackColor = Color.FromArgb(20, 20, 25), ForeColor = Color.FromArgb(255, 230, 150), Font = new Font("Consolas", 8.8f) };
                tabBasic.Controls.Add(txtBasic);

                // Pannello per il tab binario
                Panel pnlBin = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(20, 20, 25), Padding = new Padding(20) };
                Label lblBinInfo = new Label
                {
                    Dock = DockStyle.Top,
                    Height = 140,
                    Font = new Font("Consolas", 9.5f),
                    ForeColor = Color.FromArgb(200, 240, 255)
                };
                Button btnSaveBinDirect = new Button
                {
                    Text = "💾 Esporta File Binario (.bin)...",
                    Width = 250,
                    Height = 36,
                    Top = 150,
                    Left = 20,
                    BackColor = Color.FromArgb(40, 130, 80),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9f, FontStyle.Bold)
                };
                pnlBin.Controls.AddRange(new Control[] { lblBinInfo, btnSaveBinDirect });
                tabBin.Controls.Add(pnlBin);

                tc.TabPages.AddRange(new TabPage[] { tabAsm, tabC, tabBasic, tabBin });

                Action updateAllTabs = () =>
                {
                    if (cmbType.SelectedIndex == 3)
                    {
                        var mm = _project.CurrentMap.ManicMinerData ?? ManicMinerCavern.CreateCentralCavernDefault();
                        mm.UpdateFromGameMap(_project.CurrentMap, _project);
                        txtAsm.Text = mm.ToSkoolKitAssembly(45056, AsmDirective.Defb);
                        txtC.Text = $"// Manic Miner 1024 Byte Cavern: {mm.Name}\nconst unsigned char cavern_{mm.CavernIndex}[1024] = {{\n\t" +
                                    string.Join(", ", mm.To1024ByteArray()) + "\n};\n";
                        txtBasic.Text = $"10 REM Manic Miner Loader for {mm.Name}\n20 CLEAR 45055\n30 LOAD \"\" CODE 45056,1024\n40 RANDOMIZE USR 33792\n";
                        lblFormatInfo.Text = "Manic Miner 1024 Byte (SkoolKit 45056): Blocco esatto 1024 byte da $B000 (layout attributi 32x16, 8 tile, Willy spawn, portale, nastro, chiavi, guardiani e grafica).";
                        lblBinInfo.Text = $"FILE BINARIO CAVERNA MANIC MINER (1024 BYTE)\n" +
                                          $"--------------------------------------------------\n" +
                                          $"Caverna: {mm.Name}\n" +
                                          $"Dimensione file: 1024 byte (1 KB esatto)\n" +
                                          $"Indirizzo base Spectrum: 45056 ($B000)\n" +
                                          $"Conforme a: Richard Dymond / SkoolKit 45056 / Matthew Smith 1983\n" +
                                          $"Include: Layout 32x16 (512B) + Nome (32B) + 8 Tile (72B) + Willy (7B) + Nastro (4B) + Bordo (1B) + Items (25B) + Portale (37B) + Item Graphic (8B) + Aria (2B) + Guardiani (28B) + Grafica Guardiani (256B)";
                        return;
                    }

                    MapCompressionMode mode;
                    switch (cmbType.SelectedIndex)
                    {
                        case 0: mode = MapCompressionMode.Packed2Bit; break;
                        case 1: mode = MapCompressionMode.Uncompressed; break;
                        default: mode = MapCompressionMode.Both; break;
                    }
                    SelectedCompressionMode = mode;

                    int w = _project.CurrentMap.Width;
                    int h = _project.CurrentMap.Height;
                    int stdBytes = w * h;
                    int packedBytesPerRow = (w + 3) / 4;
                    int packedBytes = packedBytesPerRow * h;
                    var exportTilesList = MapExporter.GetTilesToExport(_project.CurrentMap, _project, mode);
                    int tilesToExport = exportTilesList.Count;
                    int assignedCount = (_project.CurrentMap.AssignedTileIds != null) ? _project.CurrentMap.AssignedTileIds.Count : 0;

                    if (mode == MapCompressionMode.Packed2Bit)
                    {
                        int savingPct = (stdBytes > 0) ? ((stdBytes - packedBytes) * 100 / stdBytes) : 0;
                        lblFormatInfo.Text = $"Compresso 2-bit: {packedBytes} byte ({packedBytesPerRow} B/riga, -{savingPct}% RAM) | Tileset esportato: {tilesToExport} tile (assegnati alla mappa: {assignedCount}, limite 2-bit: max 3 + sfondo, limite progetto: {_project.TilesPerMap})";
                        lblBinInfo.Text = $"FILE BINARIO COMPRESSO (.BIN)\n" +
                                          $"--------------------------------------------------\n" +
                                          $"Dimensioni mappa: {w}x{h} celle tile\n" +
                                          $"Formato byte: 4 tile per byte riga per riga\n" +
                                          $"Byte per riga: {packedBytesPerRow}\n" +
                                          $"Dimensione file: {packedBytes} byte (risparmio {savingPct}%)\n" +
                                          $"Layout: [b7-6: T0] [b5-4: T1] [b3-2: T2] [b1-0: T3]\n" +
                                          $"Tileset esportato: {tilesToExport} tile (assegnati alla mappa: {assignedCount}, limite progetto: {_project.TilesPerMap})";
                    }
                    else if (mode == MapCompressionMode.Uncompressed)
                    {
                        lblFormatInfo.Text = $"Standard 1:1: {stdBytes} byte (1 byte per tile) | Tileset esportato: {tilesToExport} tile (assegnati alla mappa: {assignedCount}, limite progetto: {_project.TilesPerMap})";
                        lblBinInfo.Text = $"FILE BINARIO STANDARD 1:1 (.BIN)\n" +
                                          $"--------------------------------------------------\n" +
                                          $"Dimensioni mappa: {w}x{h} celle tile\n" +
                                          $"Formato byte: 1 byte per tile (sequenziale)\n" +
                                          $"Dimensione file: {stdBytes} byte\n" +
                                          $"Supporta tutti gli indici di tile fino a 255\n" +
                                          $"Tileset esportato: {tilesToExport} tile (assegnati alla mappa: {assignedCount}, limite progetto: {_project.TilesPerMap})";
                    }
                    else
                    {
                        lblFormatInfo.Text = $"Entrambi i formati inclusi: 1:1 ({stdBytes} B) e Compresso ({packedBytes} B) | Tileset esportato: {tilesToExport} tile (assegnati: {assignedCount})";
                        lblBinInfo.Text = $"FILE BINARIO (.BIN)\n" +
                                          $"--------------------------------------------------\n" +
                                          $"Seleziona 'Compresso' o 'Standard' in alto per esportare\nil file binario nel formato desiderato.\n" +
                                          $"Tileset esportato: {tilesToExport} tile (assegnati alla mappa: {assignedCount}, limite progetto: {_project.TilesPerMap})";
                    }

                    txtAsm.Text = MapExporter.ExportToAssembly(_project.CurrentMap, AsmDirective.Defb, exportAllMaps: false, project: _project, compressionMode: mode);
                    txtC.Text = MapExporter.ExportToC(_project.CurrentMap, exportAllMaps: false, project: _project, compressionMode: mode);
                    txtBasic.Text = MapExporter.ExportToBasic(_project.CurrentMap, startLine: 5000, compressionMode: mode, project: _project);
                };

                cmbType.SelectedIndexChanged += (s, e) => updateAllTabs();

                numExportTiles.ValueChanged += (s, e) =>
                {
                    int val = (int)numExportTiles.Value;
                    if (_project.TilesPerMap != val)
                    {
                        _project.TilesPerMap = val;
                        if (_numTilesPerMap != null)
                        {
                            _isUpdatingTilesPerMap = true;
                            _numTilesPerMap.Value = val;
                            _isUpdatingTilesPerMap = false;
                        }
                        ProjectModified?.Invoke(this, EventArgs.Empty);
                        RefreshTilesetPalette();
                        updateAllTabs();
                    }
                };

                updateAllTabs();

                btnSaveBinDirect.Click += (s, e) =>
                {
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        if (cmbType.SelectedIndex == 3)
                        {
                            var mm = _project.CurrentMap.ManicMinerData ?? ManicMinerCavern.CreateCentralCavernDefault();
                            mm.UpdateFromGameMap(_project.CurrentMap, _project);
                            sfd.Filter = "File Binario Manic Miner (*.bin)|*.bin";
                            sfd.FileName = $"{mm.Name.Replace(" ", "_")}_1024b.bin";
                            if (sfd.ShowDialog(dlg) == DialogResult.OK)
                            {
                                File.WriteAllBytes(sfd.FileName, mm.To1024ByteArray());
                                MessageBox.Show($"File binario salvato con successo (1024 byte):\n{sfd.FileName}", "Salvataggio Completato", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            return;
                        }

                        bool isPacked = (cmbType.SelectedIndex == 0);
                        sfd.Filter = "File Binario (*.bin)|*.bin";
                        sfd.FileName = isPacked ? $"{_project.CurrentMap.Name}_packed.bin" : $"{_project.CurrentMap.Name}.bin";
                        if (sfd.ShowDialog(dlg) == DialogResult.OK)
                        {
                            MapCompressionMode bMode = isPacked ? MapCompressionMode.Packed2Bit : MapCompressionMode.Uncompressed;
                            MapExporter.ExportToBinary(sfd.FileName, _project.CurrentMap, bMode);
                            MessageBox.Show($"File binario salvato con successo:\n{sfd.FileName}", "Salvataggio Binario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                };

                Panel bottom = new Panel { Dock = DockStyle.Bottom, Height = 46, BackColor = Color.FromArgb(24, 24, 28) };
                Button btnCopy = new Button { Text = "📋 Copia Codice", Left = 15, Top = 9, Width = 140, Height = 28, BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                btnCopy.Click += (s, e) =>
                {
                    if (tc.SelectedTab == tabBin)
                    {
                        Clipboard.SetText(lblBinInfo.Text);
                        MessageBox.Show("Informazioni file binario copiate negli appunti!", "Copia Riuscita", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    TextBox activeBox = (tc.SelectedTab == tabAsm) ? txtAsm :
                                       (tc.SelectedTab == tabC) ? txtC : txtBasic;
                    Clipboard.SetText(activeBox.Text);
                    MessageBox.Show("Codice esportato copiato negli appunti!", "Copia Riuscita", MessageBoxButtons.OK, MessageBoxIcon.Information);
                };

                Button btnSaveFile = new Button { Text = "💾 Salva su File...", Left = 165, Top = 9, Width = 140, Height = 28, BackColor = Color.FromArgb(40, 130, 80), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                btnSaveFile.Click += (s, e) =>
                {
                    bool isMM = (cmbType.SelectedIndex == 3);
                    bool isPacked = (cmbType.SelectedIndex == 0);
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        if (tc.SelectedTab == tabAsm)
                        {
                            sfd.Filter = "Assembly Z80 (*.asm;*.z80)|*.asm;*.z80";
                            sfd.FileName = isMM ? $"{_project.CurrentMap.Name}_45056.asm" : (isPacked ? $"{_project.CurrentMap.Name}_packed.asm" : $"{_project.CurrentMap.Name}.asm");
                        }
                        else if (tc.SelectedTab == tabC)
                        {
                            sfd.Filter = "C Source (*.c;*.h)|*.c;*.h";
                            sfd.FileName = isPacked ? $"{_project.CurrentMap.Name}_packed.c" : $"{_project.CurrentMap.Name}.c";
                        }
                        else if (tc.SelectedTab == tabBasic)
                        {
                            sfd.Filter = "Sinclair BASIC (*.bas;*.txt)|*.bas;*.txt";
                            sfd.FileName = isPacked ? $"{_project.CurrentMap.Name}_packed.bas" : $"{_project.CurrentMap.Name}.bas";
                        }
                        else
                        {
                            sfd.Filter = "File Binario (*.bin)|*.bin";
                            sfd.FileName = isMM ? $"{_project.CurrentMap.Name}_1024b.bin" : (isPacked ? $"{_project.CurrentMap.Name}_packed.bin" : $"{_project.CurrentMap.Name}.bin");
                        }

                        if (sfd.ShowDialog(dlg) == DialogResult.OK)
                        {
                            if (tc.SelectedTab == tabBin)
                            {
                                if (isMM)
                                {
                                    var mm = _project.CurrentMap.ManicMinerData ?? ManicMinerCavern.CreateCentralCavernDefault();
                                    mm.UpdateFromGameMap(_project.CurrentMap, _project);
                                    File.WriteAllBytes(sfd.FileName, mm.To1024ByteArray());
                                }
                                else
                                {
                                    MapCompressionMode bMode = isPacked ? MapCompressionMode.Packed2Bit : MapCompressionMode.Uncompressed;
                                    MapExporter.ExportToBinary(sfd.FileName, _project.CurrentMap, bMode);
                                }
                            }
                            else
                            {
                                TextBox activeBox = (tc.SelectedTab == tabAsm) ? txtAsm :
                                                   (tc.SelectedTab == tabC) ? txtC : txtBasic;
                                File.WriteAllText(sfd.FileName, activeBox.Text, System.Text.Encoding.UTF8);
                            }
                            MessageBox.Show($"File salvato con successo:\n{sfd.FileName}", "Salvataggio Completato", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                };

                Button btnClose = new Button { Text = "Chiudi", Left = 620, Top = 9, Width = 90, Height = 28, BackColor = Color.FromArgb(60, 60, 70), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DialogResult = DialogResult.OK, Anchor = AnchorStyles.Right | AnchorStyles.Top };

                bottom.Controls.AddRange(new Control[] { btnCopy, btnSaveFile, btnClose });

                dlg.Controls.Add(tc);
                dlg.Controls.Add(topPanel);
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

        #region Supporto e Funzionalità Manic Miner (SkoolKit 45056)

        private void InitializeManicMinerControls()
        {
            _btnManicMiner = new ToolStripDropDownButton("🕹️ Manic Miner")
            {
                DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
                ForeColor = Color.FromArgb(0, 240, 255),
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };

            var miToggleMM = new ToolStripMenuItem("⚙️ Attiva / Configura Caverna Manic Miner per Mappa Corrente", null, (s, e) => ActionToggleManicMiner());
            var miNewMM = new ToolStripMenuItem("➕ Nuova Caverna (Central Cavern Default)", null, (s, e) => ActionNewManicMinerCavern());
            var miPaste512 = new ToolStripMenuItem("📋 Incolla 512 Byte Layout Caverna dagli Appunti...", null, (s, e) => ActionPasteCavern512Bytes());
            var miImportMM = new ToolStripMenuItem("📂 Importa Caverna (.BIN, .SNA o .ASM 512B)...", null, (s, e) => ActionImportManicMiner());
            var miExportMM = new ToolStripMenuItem("💾 Esporta Caverna Corrente (1024 Byte .BIN)...", null, (s, e) => ActionExportCurrentCavernBin());
            var miExportAllMM = new ToolStripMenuItem("💾 Esporta Tutte le Caverne (20 KB .BIN)...", null, (s, e) => ActionExportAllCavernsBin());
            var miInjectSNA = new ToolStripMenuItem("⚡ Inietta Caverne in Snapshot .SNA...", null, (s, e) => ActionInjectSna());
            var miAsmMM = new ToolStripMenuItem("📋 Visualizza / Esporta Assembly SkoolKit (45056)...", null, (s, e) => ActionShowSkoolKitAsm());
            var miPropsMM = new ToolStripMenuItem("📝 Parametri Caverna (Nome, Bordo, Aria, Nastro)...", null, (s, e) => ActionShowManicMinerProperties());

            _btnManicMiner.DropDownItems.AddRange(new ToolStripItem[] {
                miToggleMM,
                new ToolStripSeparator(),
                miNewMM,
                miPaste512,
                miImportMM,
                new ToolStripSeparator(),
                miExportMM,
                miExportAllMM,
                miInjectSNA,
                new ToolStripSeparator(),
                miAsmMM,
                miPropsMM
            });

            _tsMgmt.Items.Add(new ToolStripSeparator());
            _tsMgmt.Items.Add(_btnManicMiner);

            // Tools per posizionamento interattivo entità MM sulla mappa
            _sepMM = new ToolStripSeparator();
            _btnToolWilly = new ToolStripButton("🚩 Willy") { ToolTipText = "Posiziona Miner Willy (Spawn)" };
            _btnToolWilly.Click += (s, e) => SetTool(MapDrawingTool.PlaceWilly, _btnToolWilly);

            _btnToolPortal = new ToolStripButton("🚪 Portale") { ToolTipText = "Posiziona Portale Uscita (16x16 / 2x2 celle)" };
            _btnToolPortal.Click += (s, e) => SetTool(MapDrawingTool.PlacePortal, _btnToolPortal);

            _btnToolItem = new ToolStripButton("💎 Chiave") { ToolTipText = "Aggiungi/Rimuovi Chiave / Item (fino a 5)" };
            _btnToolItem.Click += (s, e) => SetTool(MapDrawingTool.PlaceItem, _btnToolItem);

            _btnToolGuardian = new ToolStripButton("👾 Guardiano") { ToolTipText = "Aggiungi/Rimuovi Guardiano Orizzontale (fino a 4)" };
            _btnToolGuardian.Click += (s, e) => SetTool(MapDrawingTool.PlaceGuardian, _btnToolGuardian);

            _btnToolMMProps = new ToolStripButton("⚙️ Caverna") { ToolTipText = "Modifica Parametri Caverna (Nome, Bordo, Aria, Nastro)" };
            _btnToolMMProps.Click += (s, e) => ActionShowManicMinerProperties();

            _tsTools.Items.AddRange(new ToolStripItem[] {
                _sepMM, _btnToolWilly, _btnToolPortal, _btnToolItem, _btnToolGuardian, _btnToolMMProps
            });

            UpdateManicMinerUiState();
        }

        private void UpdateManicMinerUiState()
        {
            bool isMM = (_project?.CurrentMap?.IsManicMinerMode == true);
            if (_btnToolWilly != null) _btnToolWilly.Visible = isMM;
            if (_btnToolPortal != null) _btnToolPortal.Visible = isMM;
            if (_btnToolItem != null) _btnToolItem.Visible = isMM;
            if (_btnToolGuardian != null) _btnToolGuardian.Visible = isMM;
            if (_btnToolMMProps != null) _btnToolMMProps.Visible = isMM;
            if (_sepMM != null) _sepMM.Visible = isMM;

            if (_btnManicMiner != null)
            {
                _btnManicMiner.Text = isMM ? "🕹️ Manic Miner [Attivo]" : "🕹️ Manic Miner";
                _btnManicMiner.BackColor = isMM ? Color.FromArgb(30, 60, 80) : Color.Transparent;
            }
        }

        private void ActionToggleManicMiner()
        {
            if (_project?.CurrentMap == null) return;
            var curMap = _project.CurrentMap;
            if (curMap.ManicMinerData == null)
            {
                var cavern = ManicMinerCavern.CreateCentralCavernDefault();
                cavern.Name = curMap.Name;
                cavern.CavernIndex = _project.SelectedMapIndex;
                curMap.ManicMinerData = cavern;
                curMap.Resize(32, 16);
                MessageBox.Show($"La mappa '{curMap.Name}' è ora configurata come Caverna Manic Miner (32x16, 1024 byte).\n" +
                                $"Puoi usare gli strumenti 🚩 Willy, 🚪 Portale, 💎 Chiave e 👾 Guardiano sulla barra strumenti.",
                                "Modalità Manic Miner Attivata", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("Disattivare la modalità Manic Miner per questa mappa?", "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    curMap.ManicMinerData = null;
                }
            }
            UpdateManicMinerUiState();
            _canvas.Invalidate();
            RefreshTilesetPalette();
            ProjectModified?.Invoke(this, EventArgs.Empty);
        }

        private void ActionNewManicMinerCavern()
        {
            if (_project == null) return;
            int idx = _project.Maps.Count;
            string cName = (idx < ManicMinerService.OriginalCavernNames.Length)
                ? ManicMinerService.OriginalCavernNames[idx]
                : $"Caverna {idx + 1}";

            var cavern = ManicMinerCavern.CreateCentralCavernDefault();
            cavern.CavernIndex = idx;
            cavern.Name = cName;

            var newMap = cavern.ToGameMap(_project);
            _project.Maps.Add(newMap);
            UpdateMapListCombo();
            SwitchToMapIndex(_project.Maps.Count - 1);
            ProjectModified?.Invoke(this, EventArgs.Empty);
        }

        public void ActionPasteCavern512Bytes()
        {
            if (_project == null) return;
            string clipText = string.Empty;
            if (Clipboard.ContainsText())
            {
                clipText = Clipboard.GetText();
            }

            var bytes = MapImporter.ParseBytesFromText(clipText);
            if (bytes == null || bytes.Count < 500)
            {
                // Se gli appunti non contengono 512 byte validi, apri la finestra di dialogo preimpostata su Manic Miner
                using (var dlg = new ImportMapDialog(_project, MapImportFormat.ManicMinerAttributes))
                {
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        UpdateMapListCombo();
                        SwitchToMapIndex(_project.SelectedMapIndex);
                        ProjectModified?.Invoke(this, EventArgs.Empty);
                    }
                }
                return;
            }

            var curMap = _project.CurrentMap;
            if (curMap == null)
            {
                curMap = _project.AddMap("Nuova Caverna", 32, 16);
            }

            var result = MessageBox.Show(
                $"Rilevati {bytes.Count} byte negli appunti (Layout 32x16 Manic Miner).\n\n" +
                $"Vuoi applicarli direttamente alla caverna corrente '{curMap.Name}'?",
                "Incolla Layout Caverna (512 Byte)",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                curMap.PushUndoState();
                if (curMap.ManicMinerData == null)
                {
                    curMap.ManicMinerData = ManicMinerCavern.CreateCentralCavernDefault();
                    curMap.ManicMinerData.Name = curMap.Name;
                }

                MapImporter.ImportManicMinerAttributes(curMap, bytes, _project, 32, 16, null, true);

                RefreshTilesetPalette();
                UpdateMapInfo();
                UpdateQuickAsm();
                _canvas.Invalidate();
                ProjectModified?.Invoke(this, EventArgs.Empty);

                MessageBox.Show(
                    $"Layout della caverna '{curMap.Name}' (512 byte) importato con successo!\n" +
                    $"Dimensioni: 32x16 celle.",
                    "Importazione Completata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void ActionImportManicMiner()
        {
            if (_project == null) return;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Tutti i formati Manic Miner (*.bin;*.sna;*.asm;*.txt)|*.bin;*.sna;*.asm;*.txt|" +
                             "Snapshot ZX Spectrum (*.sna)|*.sna|" +
                             "File Binari Caverna (*.bin)|*.bin|" +
                             "Sorgenti Assembly / Dati 512B (*.asm;*.txt)|*.asm;*.txt|" +
                             "Tutti i file (*.*)|*.*";
                ofd.Title = "Importa Caverna / Snapshot / Dati Manic Miner";
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        string ext = Path.GetExtension(ofd.FileName).ToLowerInvariant();
                        if (ext == ".asm" || ext == ".txt")
                        {
                            string text = File.ReadAllText(ofd.FileName);
                            var bytes = MapImporter.ParseBytesFromText(text);
                            if (bytes.Count == 512)
                            {
                                var curMap = _project.CurrentMap ?? _project.AddMap("Caverna Importata", 32, 16);
                                curMap.PushUndoState();
                                if (curMap.ManicMinerData == null)
                                {
                                    curMap.ManicMinerData = ManicMinerCavern.CreateCentralCavernDefault();
                                    curMap.ManicMinerData.Name = curMap.Name;
                                }
                                MapImporter.ImportManicMinerAttributes(curMap, bytes, _project, 32, 16, null, true);
                                RefreshTilesetPalette();
                                UpdateMapInfo();
                                UpdateQuickAsm();
                                _canvas.Invalidate();
                                ProjectModified?.Invoke(this, EventArgs.Empty);
                                MessageBox.Show($"Layout caverna da 512 byte importato con successo in '{curMap.Name}'!", "Importazione Riuscita", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            else if (bytes.Count >= 1024)
                            {
                                var cav = ManicMinerCavern.From1024ByteArray(bytes.ToArray(), 0, _project.Maps.Count);
                                var gMap = cav.ToGameMap(_project);
                                _project.Maps.Add(gMap);
                                UpdateMapListCombo();
                                SwitchToMapIndex(_project.Maps.Count - 1);
                                ProjectModified?.Invoke(this, EventArgs.Empty);
                                MessageBox.Show($"Caverna '{cav.Name}' (1024 byte) importata con successo!", "Importazione Riuscita", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }

                        byte[] fileBytes = File.ReadAllBytes(ofd.FileName);
                        var imported = ManicMinerService.ImportFromBinary(fileBytes);
                        if (imported == null || imported.Count == 0)
                        {
                            MessageBox.Show("Nessuna caverna valida trovata nel file selezionato.", "Importazione Fallita", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        int startIdx = _project.Maps.Count;
                        foreach (var cav in imported)
                        {
                            var gMap = cav.ToGameMap(_project);
                            _project.Maps.Add(gMap);
                        }

                        UpdateMapListCombo();
                        SwitchToMapIndex(startIdx);
                        ProjectModified?.Invoke(this, EventArgs.Empty);
                        MessageBox.Show($"Importate con successo {imported.Count} caverna/e da:\n{Path.GetFileName(ofd.FileName)}",
                                        "Importazione Completata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Errore durante l'importazione:\n{ex.Message}", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ActionExportCurrentCavernBin()
        {
            if (_project?.CurrentMap == null) return;
            var curMap = _project.CurrentMap;
            var cavern = curMap.ManicMinerData ?? ManicMinerCavern.CreateCentralCavernDefault();
            cavern.UpdateFromGameMap(curMap, _project);

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "File Binario Manic Miner (*.bin)|*.bin";
                sfd.FileName = $"{cavern.Name.Replace(" ", "_")}_1024b.bin";
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    byte[] bytes = cavern.To1024ByteArray();
                    File.WriteAllBytes(sfd.FileName, bytes);
                    MessageBox.Show($"Caverna esportata con successo (1024 byte esatti conforme a SkoolKit 45056):\n{sfd.FileName}",
                                    "Esportazione Completata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ActionExportAllCavernsBin()
        {
            if (_project == null) return;
            var list = new List<ManicMinerCavern>();
            foreach (var m in _project.Maps)
            {
                if (m.ManicMinerData != null)
                {
                    m.ManicMinerData.UpdateFromGameMap(m, _project);
                    list.Add(m.ManicMinerData);
                }
            }
            if (list.Count == 0)
            {
                MessageBox.Show("Nessuna caverna Manic Miner trovata nel progetto.", "Avviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "File Binario 20 Caverne (*.bin)|*.bin";
                sfd.FileName = "ManicMiner_20Caverns_20KB.bin";
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    byte[] bytes = ManicMinerService.ExportAllCavernsToBinary(list);
                    File.WriteAllBytes(sfd.FileName, bytes);
                    MessageBox.Show($"Esportate con successo {list.Count} caverne in blocco unico da {bytes.Length} byte:\n{sfd.FileName}",
                                    "Esportazione Completata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ActionInjectSna()
        {
            if (_project == null) return;
            var list = new List<ManicMinerCavern>();
            foreach (var m in _project.Maps)
            {
                if (m.ManicMinerData != null)
                {
                    m.ManicMinerData.UpdateFromGameMap(m, _project);
                    list.Add(m.ManicMinerData);
                }
            }
            if (list.Count == 0)
            {
                MessageBox.Show("Nessuna caverna Manic Miner configurata nel progetto da iniettare.", "Avviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Snapshot ZX Spectrum (*.sna)|*.sna";
                ofd.Title = "Seleziona Snapshot .SNA originale di Manic Miner";
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.Filter = "Snapshot ZX Spectrum (*.sna)|*.sna";
                        sfd.FileName = Path.GetFileNameWithoutExtension(ofd.FileName) + "_modded.sna";
                        if (sfd.ShowDialog(this) == DialogResult.OK)
                        {
                            try
                            {
                                ManicMinerService.InjectIntoSna(ofd.FileName, sfd.FileName, list);
                                MessageBox.Show($"Iniezione completata con successo!\nNuovo file: {sfd.FileName}\nPuoi aprirlo subito in qualsiasi emulatore ZX Spectrum!",
                                                "Snapshot Generato", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Errore durante l'iniezione: {ex.Message}", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void ActionShowSkoolKitAsm()
        {
            if (_project?.CurrentMap == null) return;
            var curMap = _project.CurrentMap;
            var cavern = curMap.ManicMinerData ?? ManicMinerCavern.CreateCentralCavernDefault();
            cavern.UpdateFromGameMap(curMap, _project);

            string asmCode = cavern.ToSkoolKitAssembly(45056, AsmDirective.Defb);

            using (Form dlg = new Form())
            {
                dlg.Text = $"Assembly SkoolKit 45056 - {cavern.Name}";
                dlg.Size = new Size(720, 540);
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.BackColor = Color.FromArgb(30, 30, 36);
                dlg.ForeColor = Color.White;

                TextBox txt = new TextBox
                {
                    Dock = DockStyle.Fill,
                    Multiline = true,
                    ReadOnly = true,
                    ScrollBars = ScrollBars.Both,
                    BackColor = Color.FromArgb(20, 20, 25),
                    ForeColor = Color.FromArgb(180, 255, 180),
                    Font = new Font("Consolas", 9f),
                    Text = asmCode
                };

                Panel pnl = new Panel { Dock = DockStyle.Bottom, Height = 45, BackColor = Color.FromArgb(24, 24, 28) };
                Button btnCopy = new Button { Text = "📋 Copia", Left = 15, Top = 8, Width = 100, Height = 28, BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                btnCopy.Click += (s, e) => { Clipboard.SetText(txt.Text); MessageBox.Show("Codice copiato negli appunti!", "Copia", MessageBoxButtons.OK, MessageBoxIcon.Information); };

                Button btnSave = new Button { Text = "💾 Salva File ASM...", Left = 125, Top = 8, Width = 140, Height = 28, BackColor = Color.FromArgb(40, 130, 80), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                btnSave.Click += (s, e) =>
                {
                    using (SaveFileDialog sfd = new SaveFileDialog())
                    {
                        sfd.Filter = "File Assembly (*.asm;*.skool)|*.asm;*.skool";
                        sfd.FileName = $"{cavern.Name.Replace(" ", "_")}_45056.asm";
                        if (sfd.ShowDialog(dlg) == DialogResult.OK)
                        {
                            File.WriteAllText(sfd.FileName, txt.Text, Encoding.UTF8);
                            MessageBox.Show($"File salvato con successo:\n{sfd.FileName}", "Salvataggio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                };

                Button btnClose = new Button { Text = "Chiudi", Left = 600, Top = 8, Width = 90, Height = 28, BackColor = Color.FromArgb(60, 60, 70), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DialogResult = DialogResult.OK, Anchor = AnchorStyles.Right | AnchorStyles.Top };

                pnl.Controls.AddRange(new Control[] { btnCopy, btnSave, btnClose });
                dlg.Controls.Add(txt);
                dlg.Controls.Add(pnl);
                dlg.ShowDialog(this);
            }
        }

        private void ActionShowManicMinerProperties()
        {
            if (_project?.CurrentMap == null) return;
            var curMap = _project.CurrentMap;
            if (curMap.ManicMinerData == null)
            {
                curMap.ManicMinerData = ManicMinerCavern.CreateCentralCavernDefault();
                curMap.ManicMinerData.Name = curMap.Name;
            }

            using (var dlg = new ManicMinerPropertiesDialog(curMap))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    UpdateMapListCombo();
                    _canvas.Invalidate();
                    RefreshTilesetPalette();
                    ProjectModified?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        #endregion
    }

    /// <summary>
    /// Oggetto di trasferimento per il Drag & Drop di una tile dalla libreria al set della mappa.
    /// </summary>
    public class TileDragData
    {
        public int TileId { get; }
        public TileDragData(int tileId)
        {
            TileId = tileId;
        }
    }

    /// <summary>
    /// Dialog modale per l'importazione di mappe di gioco.
    /// Supporta:
    /// 1. Formato Manic Miner (512 byte di attributi colore ZX Spectrum, specifica SkoolKit 45056, 32x16)
    /// 2. Formato Lineare 1 byte per cella (indici tile 0..N per qualsiasi dimensione W x H)
    /// 3. Formato Compresso 2-bit (4 tile per byte, standard Jet Set Willy / compresso)
    /// Riconosce automaticamente il formato dai byte incollati o caricati da file.
    /// </summary>
    public class ImportMapDialog : Form
    {
        private readonly SpriteProject _project;
        private MapImportFormat _selectedFormat = MapImportFormat.Packed2Bit;

        // Selettore Formato
        private ComboBox _cmbFormat;

        // Controlli destinazione e dimensioni
        private RadioButton _rbOverwriteCurrent;
        private RadioButton _rbNewMap;
        private TextBox _txtMapName;
        private NumericUpDown _numWidth;
        private NumericUpDown _numHeight;
        private Label _lblExpectedStats;
        private CheckBox _chkSkipHeader;

        // Controlli assegnazione tile (fino a 8 slot per Manic Miner e formato lineare)
        private GroupBox _grpTiles;
        private Label _lblMode;
        private ComboBox _cmbMappingMode;
        private CheckBox _chkKeepExistingTiles;
        private Button _btnAutoMapMM;

        private Label[] _lblSlots = new Label[8];
        private ComboBox[] _cmbSlots = new ComboBox[8];
        private PictureBox[] _picSlots = new PictureBox[8];

        // Editor testo e statistiche
        private TextBox _txtCode;
        private Label _lblParseStatus;
        private Button _btnOk;
        private Button _btnCancel;

        private List<byte> _parsedBytes = new List<byte>();

        private class TileItem
        {
            public int TileId { get; set; }
            public string DisplayName { get; set; }
            public override string ToString() => DisplayName;
        }

        public ImportMapDialog(SpriteProject project, MapImportFormat defaultFormat = MapImportFormat.Packed2Bit)
        {
            _project = project;
            _selectedFormat = defaultFormat;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "📥 Importa Mappa di Gioco / Caverna Manic Miner";
            this.Size = new Size(840, 770);
            this.MinimumSize = new Size(760, 640);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(28, 28, 33);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 9f);
            this.ShowIcon = false;

            // 1. TOP PANEL: Titolo, Formato, Destinazione, Dimensioni e Mappatura Tile
            Panel pnlTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 345,
                BackColor = Color.FromArgb(22, 22, 26),
                Padding = new Padding(14, 10, 14, 10)
            };

            Label lblTitle = new Label
            {
                Text = "📥 IMPORTAZIONE MAPPA DI GIOCO",
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 220, 255),
                AutoSize = true,
                Location = new Point(14, 8)
            };

            Label lblDesc = new Label
            {
                Text = "Incolla nel box sottostante i dati Assembly (defb/db), array C, BASIC (DATA) o byte esadecimali/binari.\n" +
                       "Supporta Caverne Manic Miner (512 byte attributi SkoolKit), Lineare 1 byte/cella e Compresso 2-bit (JSW).",
                Font = new Font("Segoe UI", 8.2f),
                ForeColor = Color.FromArgb(200, 210, 225),
                AutoSize = true,
                Location = new Point(14, 30)
            };

            // Raggruppamento Formato Dati
            Label lblFormat = new Label
            {
                Text = "Formato Dati:",
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 215, 0),
                AutoSize = true,
                Location = new Point(14, 68)
            };

            _cmbFormat = new ComboBox
            {
                Location = new Point(110, 65),
                Width = 680,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(36, 36, 44),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold)
            };
            _cmbFormat.Items.Add("🕹️ Manic Miner / SkoolKit 45056 (512 Byte - Attributi Colore ZX Spectrum, 32x16)");
            _cmbFormat.Items.Add("📄 Lineare 1 Byte per Cella (Indici Tile 0..N, larghezza x altezza)");
            _cmbFormat.Items.Add("📦 Compresso 2-Bit (4 tile per byte - Jet Set Willy / Standard)");
            _cmbFormat.SelectedIndex = (_selectedFormat == MapImportFormat.ManicMinerAttributes) ? 0 :
                                       (_selectedFormat == MapImportFormat.Linear1BytePerCell) ? 1 : 2;
            _cmbFormat.SelectedIndexChanged += (s, e) =>
            {
                _selectedFormat = (_cmbFormat.SelectedIndex == 0) ? MapImportFormat.ManicMinerAttributes :
                                  (_cmbFormat.SelectedIndex == 1) ? MapImportFormat.Linear1BytePerCell :
                                  MapImportFormat.Packed2Bit;
                ApplyFormatChange();
                UpdateStats();
            };

            // Raggruppamento Opzioni Mappa (Destinazione & Dimensioni)
            GroupBox grpTarget = new GroupBox
            {
                Text = "Destinazione e Dimensioni",
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 215, 0),
                Location = new Point(14, 98),
                Size = new Size(796, 85)
            };

            bool hasCurrentMap = _project?.CurrentMap != null;
            string curName = hasCurrentMap ? _project.CurrentMap.Name : "Nessuna";

            _rbOverwriteCurrent = new RadioButton
            {
                Text = $"Sovrascrivi mappa corrente ({curName})",
                AutoSize = true,
                Location = new Point(14, 22),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                Checked = hasCurrentMap,
                Enabled = hasCurrentMap
            };

            _rbNewMap = new RadioButton
            {
                Text = "Crea Nuova Mappa:",
                Checked = !hasCurrentMap,
                AutoSize = true,
                Location = new Point(360, 22),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8.5f)
            };

            _txtMapName = new TextBox
            {
                Text = $"Mappa {(_project?.Maps.Count ?? 0) + 1}",
                Location = new Point(500, 20),
                Width = 200,
                BackColor = Color.FromArgb(36, 36, 44),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8.5f),
                Enabled = !hasCurrentMap
            };

            _rbOverwriteCurrent.CheckedChanged += (s, e) =>
            {
                _txtMapName.Enabled = _rbNewMap.Checked;
                if (_rbOverwriteCurrent.Checked && _project?.CurrentMap != null)
                {
                    _numWidth.Value = _project.CurrentMap.Width;
                    _numHeight.Value = _project.CurrentMap.Height;
                }
            };
            _rbNewMap.CheckedChanged += (s, e) => _txtMapName.Enabled = _rbNewMap.Checked;

            Label lblW = new Label
            {
                Text = "Larghezza (celle):",
                AutoSize = true,
                Location = new Point(14, 54),
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 8.2f)
            };

            _numWidth = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 256,
                Value = (_selectedFormat == MapImportFormat.ManicMinerAttributes) ? 32 : (_project?.CurrentMap?.Width ?? 32),
                Location = new Point(125, 52),
                Width = 60,
                BackColor = Color.FromArgb(36, 36, 44),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold)
            };

            Label lblH = new Label
            {
                Text = "Altezza (celle):",
                AutoSize = true,
                Location = new Point(200, 54),
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 8.2f)
            };

            _numHeight = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 256,
                Value = (_selectedFormat == MapImportFormat.ManicMinerAttributes) ? 16 : (_project?.CurrentMap?.Height ?? 24),
                Location = new Point(290, 52),
                Width = 60,
                BackColor = Color.FromArgb(36, 36, 44),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold)
            };

            _lblExpectedStats = new Label
            {
                Text = "Byte attesi: 512 (1 B/cella: 32x16)",
                AutoSize = true,
                Location = new Point(365, 54),
                ForeColor = Color.FromArgb(120, 230, 160),
                Font = new Font("Segoe UI", 8.2f, FontStyle.Bold)
            };

            _chkSkipHeader = new CheckBox
            {
                Text = "Salta byte intestazione (width, height, bytes/row)",
                AutoSize = true,
                Location = new Point(560, 52),
                ForeColor = Color.FromArgb(180, 220, 255),
                Font = new Font("Segoe UI", 8.2f),
                Visible = (_selectedFormat == MapImportFormat.Packed2Bit)
            };

            grpTarget.Controls.AddRange(new Control[] {
                _rbOverwriteCurrent, _rbNewMap, _txtMapName,
                lblW, _numWidth, lblH, _numHeight, _lblExpectedStats, _chkSkipHeader
            });

            // Raggruppamento Mappatura Tile
            _grpTiles = new GroupBox
            {
                Text = "Mappatura Tile per Formato",
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 220, 255),
                Location = new Point(14, 188),
                Size = new Size(796, 148)
            };

            _lblMode = new Label
            {
                Text = "Schema 2-Bit:",
                Location = new Point(14, 20),
                AutoSize = true,
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 8.2f)
            };

            _cmbMappingMode = new ComboBox
            {
                Location = new Point(100, 17),
                Width = 320,
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(36, 36, 44),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8.2f)
            };
            _cmbMappingMode.Items.Add("4 Tile del Set Mappa: %00=Tile #0, %01=Tile #1, %10=Tile #2, %11=Tile #3");
            _cmbMappingMode.Items.Add("Con Sfondo: %00=Vuoto/Sfondo, %01=Tile #0, %10=Tile #1, %11=Tile #2");
            _cmbMappingMode.SelectedIndex = 0;
            _cmbMappingMode.SelectedIndexChanged += (s, e) => ApplyDefaultSlotMapping();

            _chkKeepExistingTiles = new CheckBox
            {
                Text = "Mantieni tile già presenti nel Set Mappa",
                Checked = true,
                AutoSize = true,
                Location = new Point(430, 18),
                ForeColor = Color.FromArgb(140, 230, 160),
                Font = new Font("Segoe UI", 8.2f, FontStyle.Bold)
            };

            _btnAutoMapMM = new Button
            {
                Text = "🔍 Auto-Mappa da Attributi Tile",
                Location = new Point(14, 18),
                Size = new Size(240, 24),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Visible = false
            };
            _btnAutoMapMM.FlatAppearance.BorderSize = 0;
            _btnAutoMapMM.Click += (s, e) => AutoMapManicMinerSlots();

            _grpTiles.Controls.AddRange(new Control[] { _lblMode, _cmbMappingMode, _chkKeepExistingTiles, _btnAutoMapMM });

            // Creazione controlli per gli 8 slot (2 righe x 4 colonne)
            int[] colX = new int[] { 14, 210, 406, 602 };
            for (int i = 0; i < 8; i++)
            {
                int r = i / 4;
                int c = i % 4;
                int x = colX[c];
                int y = (r == 0) ? 48 : 94;

                _lblSlots[i] = new Label
                {
                    Text = $"Slot {i}:",
                    Location = new Point(x, y + 3),
                    Width = 84,
                    Font = new Font("Segoe UI", 7.6f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(140, 220, 255)
                };

                _cmbSlots[i] = new ComboBox
                {
                    Location = new Point(x + 86, y),
                    Width = 76,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    BackColor = Color.FromArgb(36, 36, 44),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 7.6f)
                };

                _picSlots[i] = new PictureBox
                {
                    Location = new Point(x + 164, y),
                    Size = new Size(24, 24),
                    BackColor = Color.Black,
                    SizeMode = PictureBoxSizeMode.Zoom
                };

                int slotIdx = i;
                _cmbSlots[slotIdx].SelectedIndexChanged += (s, e) => UpdateSlotPreview(_cmbSlots[slotIdx], _picSlots[slotIdx]);

                _grpTiles.Controls.AddRange(new Control[] { _lblSlots[i], _cmbSlots[i], _picSlots[i] });
            }

            pnlTop.Controls.AddRange(new Control[] { lblTitle, lblDesc, lblFormat, _cmbFormat, grpTarget, _grpTiles });

            // 2. BOTTOM PANEL: Statistiche di Parsing & Pulsanti Azione
            Panel pnlBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 62,
                BackColor = Color.FromArgb(20, 20, 24),
                Padding = new Padding(14, 10, 14, 10)
            };

            _lblParseStatus = new Label
            {
                Text = "In attesa di dati...",
                Location = new Point(14, 14),
                AutoSize = true,
                Font = new Font("Segoe UI", 8.8f, FontStyle.Bold),
                ForeColor = Color.Yellow
            };

            _btnOk = new Button
            {
                Text = "📥 Importa Mappa",
                DialogResult = DialogResult.OK,
                Size = new Size(150, 36),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(520, 13),
                BackColor = Color.FromArgb(40, 130, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Enabled = false
            };
            _btnOk.FlatAppearance.BorderSize = 0;
            _btnOk.Click += BtnOk_Click;

            _btnCancel = new Button
            {
                Text = "Annulla",
                DialogResult = DialogResult.Cancel,
                Size = new Size(100, 36),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(680, 13),
                BackColor = Color.FromArgb(60, 60, 70),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f),
                Cursor = Cursors.Hand
            };
            _btnCancel.FlatAppearance.BorderSize = 0;

            pnlBottom.Controls.AddRange(new Control[] { _lblParseStatus, _btnOk, _btnCancel });

            // 3. CENTER PANEL: Barra Strumenti & TextBox Codice Mappa
            Panel pnlCenter = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(14, 6, 14, 6)
            };

            Panel pnlTextTools = new Panel
            {
                Dock = DockStyle.Top,
                Height = 32,
                BackColor = Color.FromArgb(28, 28, 33)
            };

            Label lblCodeTitle = new Label
            {
                Text = "Dati Mappa (Incolla qui il codice o carica da file):",
                Location = new Point(0, 7),
                AutoSize = true,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                ForeColor = Color.White
            };

            Button btnPaste = new Button
            {
                Text = "📋 Incolla dagli Appunti",
                Size = new Size(160, 26),
                Location = new Point(330, 3),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnPaste.FlatAppearance.BorderSize = 0;
            btnPaste.Click += (s, e) =>
            {
                if (Clipboard.ContainsText())
                {
                    _txtCode.Text = Clipboard.GetText();
                }
            };

            Button btnOpenFile = new Button
            {
                Text = "📂 Carica da File...",
                Size = new Size(140, 26),
                Location = new Point(500, 3),
                BackColor = Color.FromArgb(45, 75, 115),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnOpenFile.FlatAppearance.BorderSize = 0;
            btnOpenFile.Click += BtnOpenFile_Click;

            Button btnClear = new Button
            {
                Text = "🧹 Pulisci",
                Size = new Size(80, 26),
                Location = new Point(650, 3),
                BackColor = Color.FromArgb(50, 50, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8f),
                Cursor = Cursors.Hand
            };
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Click += (s, e) => _txtCode.Clear();

            pnlTextTools.Controls.AddRange(new Control[] { lblCodeTitle, btnPaste, btnOpenFile, btnClear });

            _txtCode = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ScrollBars = ScrollBars.Both,
                Font = new Font("Consolas", 9f),
                BackColor = Color.FromArgb(16, 16, 20),
                ForeColor = Color.FromArgb(180, 255, 180),
                WordWrap = false
            };
            _txtCode.TextChanged += TxtCode_TextChanged;

            pnlCenter.Controls.Add(_txtCode);
            pnlCenter.Controls.Add(pnlTextTools);

            this.Controls.Add(pnlCenter);
            this.Controls.Add(pnlBottom);
            this.Controls.Add(pnlTop);

            // Eventi di calcolo dinamico
            _numWidth.ValueChanged += (s, e) => UpdateStats();
            _numHeight.ValueChanged += (s, e) => UpdateStats();
            _chkSkipHeader.CheckedChanged += (s, e) => UpdateStats();

            this.AcceptButton = _btnOk;
            this.CancelButton = _btnCancel;

            ApplyFormatChange();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            PopulateTileCombos();
            if (_selectedFormat == MapImportFormat.ManicMinerAttributes)
            {
                AutoMapManicMinerSlots();
            }
            else
            {
                ApplyDefaultSlotMapping();
            }
            UpdateStats();
        }

        private void PopulateTileCombos()
        {
            for (int i = 0; i < 8; i++)
            {
                var cmb = _cmbSlots[i];
                cmb.Items.Clear();
                cmb.Items.Add(new TileItem { TileId = 0, DisplayName = "(Vuoto / 0)" });

                if (_project != null)
                {
                    for (int t = 0; t < _project.Tiles.Count; t++)
                    {
                        int tid = t + 1;
                        string name = $"#{tid}: {_project.Tiles[t].Name}";
                        cmb.Items.Add(new TileItem { TileId = tid, DisplayName = name });
                    }
                }
            }
        }

        private void ApplyFormatChange()
        {
            bool isMM = (_selectedFormat == MapImportFormat.ManicMinerAttributes);
            bool isLinear = (_selectedFormat == MapImportFormat.Linear1BytePerCell);
            bool is2Bit = (_selectedFormat == MapImportFormat.Packed2Bit);

            _chkSkipHeader.Visible = is2Bit;
            _lblMode.Visible = is2Bit;
            _cmbMappingMode.Visible = is2Bit;
            _chkKeepExistingTiles.Visible = is2Bit;
            _btnAutoMapMM.Visible = isMM;

            if (isMM)
            {
                _grpTiles.Text = "Mappatura Tile Manic Miner (8 Ruoli Canonici / Attributi SkoolKit 45056)";
                _numWidth.Value = 32;
                _numHeight.Value = 16;

                string[] mmNames = new string[] {
                    "Background", "Floor", "Crumbling", "Wall", "Conveyor", "Nasty 1", "Nasty 2", "Spare"
                };

                for (int i = 0; i < 8; i++)
                {
                    _lblSlots[i].Visible = true;
                    _cmbSlots[i].Visible = true;
                    _picSlots[i].Visible = true;
                    _lblSlots[i].Text = $"{i}: {mmNames[i]}";
                }
            }
            else if (isLinear)
            {
                _grpTiles.Text = "Mappatura Tile Formato Lineare (Indici 0..7)";
                for (int i = 0; i < 8; i++)
                {
                    _lblSlots[i].Visible = true;
                    _cmbSlots[i].Visible = true;
                    _picSlots[i].Visible = true;
                    _lblSlots[i].Text = $"Slot {i}:";
                }
            }
            else // Packed2Bit
            {
                _grpTiles.Text = "Mappatura Tile per Indici 2-bit (0..3)";
                string[] labels2Bit = new string[] { "Slot 0 (%00):", "Slot 1 (%01):", "Slot 2 (%10):", "Slot 3 (%11):" };
                for (int i = 0; i < 4; i++)
                {
                    _lblSlots[i].Visible = true;
                    _cmbSlots[i].Visible = true;
                    _picSlots[i].Visible = true;
                    _lblSlots[i].Text = labels2Bit[i];
                }
                for (int i = 4; i < 8; i++)
                {
                    _lblSlots[i].Visible = false;
                    _cmbSlots[i].Visible = false;
                    _picSlots[i].Visible = false;
                }
            }
        }

        private void AutoMapManicMinerSlots()
        {
            if (_project == null) return;
            var curMap = _project.CurrentMap;

            byte[] expectedAttrs = new byte[] { 0, 66, 2, 22, 4, 68, 5, 0 };
            string[] roleNames = new string[] {
                "Background", "Floor", "Crumbling", "Wall", "Conveyor", "Nasty 1", "Nasty 2", "Spare"
            };

            if (curMap?.ManicMinerData?.TileDefs != null)
            {
                for (int s = 0; s < 8; s++)
                {
                    if (curMap.ManicMinerData.TileDefs[s] != null)
                        expectedAttrs[s] = curMap.ManicMinerData.TileDefs[s].Attribute;
                }
            }

            // Conta frequenza degli attributi nei byte parsati se disponibili
            Dictionary<byte, int> attrFreq = new Dictionary<byte, int>();
            foreach (var b in _parsedBytes)
            {
                attrFreq[b] = attrFreq.ContainsKey(b) ? attrFreq[b] + 1 : 1;
            }

            for (int s = 0; s < 8; s++)
            {
                byte targetAttr = expectedAttrs[s];
                int matchedTileId = 0;

                // 1. Cerca prima nelle tile già assegnate alla mappa
                if (curMap?.AssignedTileIds != null && s < curMap.AssignedTileIds.Count)
                {
                    int candidateId = curMap.AssignedTileIds[s];
                    if (candidateId > 0 && candidateId <= _project.Tiles.Count)
                    {
                        var t = _project.Tiles[candidateId - 1];
                        byte candidateAttr = (byte)((t.Flash ? 128 : 0) | (t.Bright ? 64 : 0) | (t.PaperColor << 3) | t.InkColor);
                        if (candidateAttr == targetAttr || (targetAttr == 0 && candidateAttr == 0))
                        {
                            matchedTileId = candidateId;
                        }
                    }
                }

                // 2. Se non trovata, cerca in tutte le tile del progetto per attributo ZX Spectrum
                if (matchedTileId == 0)
                {
                    for (int i = 0; i < _project.Tiles.Count; i++)
                    {
                        var t = _project.Tiles[i];
                        byte candidateAttr = (byte)((t.Flash ? 128 : 0) | (t.Bright ? 64 : 0) | (t.PaperColor << 3) | t.InkColor);
                        if (candidateAttr == targetAttr)
                        {
                            if (targetAttr == 0 && s != 0 && s != 7) continue;
                            matchedTileId = i + 1;
                            break;
                        }
                    }
                }

                // 3. Fallback: assegna slot sequenziale se disponibile
                if (matchedTileId == 0 && curMap?.AssignedTileIds != null && s < curMap.AssignedTileIds.Count)
                {
                    matchedTileId = curMap.AssignedTileIds[s];
                }

                SelectTileInCombo(_cmbSlots[s], matchedTileId);

                int countInRoom = attrFreq.ContainsKey(targetAttr) ? attrFreq[targetAttr] : 0;
                string freqInfo = countInRoom > 0 ? $" ({targetAttr}) [{countInRoom}]" : $" ({targetAttr})";
                _lblSlots[s].Text = $"{s}: {roleNames[s]}{freqInfo}";
            }
        }

        private void ApplyDefaultSlotMapping()
        {
            var curMap = _project?.CurrentMap;
            bool isFull4Tiles = (_cmbMappingMode.SelectedIndex == 0);

            int d0 = 0, d1 = 0, d2 = 0, d3 = 0;

            if (curMap?.AssignedTileIds != null && curMap.AssignedTileIds.Count > 0)
            {
                if (isFull4Tiles)
                {
                    if (curMap.AssignedTileIds.Count >= 1) d0 = curMap.AssignedTileIds[0];
                    if (curMap.AssignedTileIds.Count >= 2) d1 = curMap.AssignedTileIds[1];
                    if (curMap.AssignedTileIds.Count >= 3) d2 = curMap.AssignedTileIds[2];
                    if (curMap.AssignedTileIds.Count >= 4) d3 = curMap.AssignedTileIds[3];
                }
                else
                {
                    d0 = 0; // Sfondo vuoto
                    if (curMap.AssignedTileIds.Count >= 1) d1 = curMap.AssignedTileIds[0];
                    if (curMap.AssignedTileIds.Count >= 2) d2 = curMap.AssignedTileIds[1];
                    if (curMap.AssignedTileIds.Count >= 3) d3 = curMap.AssignedTileIds[2];
                }
            }
            else if (_project != null && _project.Tiles.Count > 0)
            {
                if (isFull4Tiles)
                {
                    if (_project.Tiles.Count >= 1) d0 = 1;
                    if (_project.Tiles.Count >= 2) d1 = 2;
                    if (_project.Tiles.Count >= 3) d2 = 3;
                    if (_project.Tiles.Count >= 4) d3 = 4;
                }
                else
                {
                    d0 = 0;
                    if (_project.Tiles.Count >= 1) d1 = 1;
                    if (_project.Tiles.Count >= 2) d2 = 2;
                    if (_project.Tiles.Count >= 3) d3 = 3;
                }
            }

            SelectTileInCombo(_cmbSlots[0], d0);
            SelectTileInCombo(_cmbSlots[1], d1);
            SelectTileInCombo(_cmbSlots[2], d2);
            SelectTileInCombo(_cmbSlots[3], d3);
        }

        private void SelectTileInCombo(ComboBox cmb, int tileId)
        {
            for (int i = 0; i < cmb.Items.Count; i++)
            {
                if (cmb.Items[i] is TileItem item && item.TileId == tileId)
                {
                    cmb.SelectedIndex = i;
                    return;
                }
            }
            if (cmb.Items.Count > 0) cmb.SelectedIndex = 0;
        }

        private void UpdateSlotPreview(ComboBox cmb, PictureBox pic)
        {
            if (cmb.SelectedItem is TileItem item && item.TileId > 0 && _project != null && (item.TileId - 1) < _project.Tiles.Count)
            {
                var old = pic.Image;
                pic.Image = _project.Tiles[item.TileId - 1].RenderToBitmap(2);
                old?.Dispose();
            }
            else
            {
                pic.Image = null;
            }
        }

        private void TxtCode_TextChanged(object sender, EventArgs e)
        {
            string text = _txtCode.Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                _parsedBytes.Clear();
                UpdateStats();
                return;
            }

            // Tenta il riconoscimento automatico dell'intestazione (Assembly, C o BASIC)
            if (MapImporter.TryDetectHeader(text, out int dw, out int dh, out string dName, out bool hasHeaderBytes))
            {
                _numWidth.Value = dw;
                _numHeight.Value = dh;
                if (!string.IsNullOrWhiteSpace(dName) && _txtMapName.Text.StartsWith("Mappa"))
                {
                    _txtMapName.Text = dName;
                }
                _chkSkipHeader.Checked = hasHeaderBytes;
            }

            _parsedBytes = MapImporter.ParseBytesFromText(text);

            // Auto-rilevamento intelligente formato in base al numero di byte e ai valori
            if (_parsedBytes.Count == 512 || (_parsedBytes.Count >= 500 && _parsedBytes.Count <= 520))
            {
                _numWidth.Value = 32;
                _numHeight.Value = 16;
                bool hasHighAttr = false;
                foreach (var b in _parsedBytes) { if (b > 7) { hasHighAttr = true; break; } }
                _cmbFormat.SelectedIndex = hasHighAttr ? 0 : 1;
                if (_cmbFormat.SelectedIndex == 0)
                {
                    AutoMapManicMinerSlots();
                }
            }
            else if (_parsedBytes.Count == 128)
            {
                _numWidth.Value = 32;
                _numHeight.Value = 16;
                _cmbFormat.SelectedIndex = 2; // 2-bit
            }

            UpdateStats();
        }

        private void UpdateStats()
        {
            int w = (int)_numWidth.Value;
            int h = (int)_numHeight.Value;

            if (_selectedFormat == MapImportFormat.ManicMinerAttributes)
            {
                int expectedDataBytes = w * h;
                _lblExpectedStats.Text = $"Byte attesi: {expectedDataBytes} (1 B/cella: {w}x{h} - SkoolKit 45056)";

                int availableBytes = _parsedBytes.Count;
                if (availableBytes == 0)
                {
                    _lblParseStatus.Text = "In attesa di dati...";
                    _lblParseStatus.ForeColor = Color.Yellow;
                    _btnOk.Enabled = false;
                }
                else if (availableBytes == expectedDataBytes)
                {
                    _lblParseStatus.Text = $"✔ Dati completi e validi per Manic Miner: {availableBytes} byte (32x16 celle)";
                    _lblParseStatus.ForeColor = Color.FromArgb(120, 230, 160);
                    _btnOk.Enabled = true;
                }
                else
                {
                    _lblParseStatus.Text = $"⚠ Trovati {availableBytes} byte su {expectedDataBytes} attesi.";
                    _lblParseStatus.ForeColor = Color.FromArgb(255, 180, 80);
                    _btnOk.Enabled = true;
                }
            }
            else if (_selectedFormat == MapImportFormat.Linear1BytePerCell)
            {
                int expectedDataBytes = w * h;
                _lblExpectedStats.Text = $"Byte attesi: {expectedDataBytes} (1 B/cella: {w}x{h})";

                int availableBytes = _parsedBytes.Count;
                if (availableBytes == 0)
                {
                    _lblParseStatus.Text = "In attesa di dati...";
                    _lblParseStatus.ForeColor = Color.Yellow;
                    _btnOk.Enabled = false;
                }
                else if (availableBytes == expectedDataBytes)
                {
                    _lblParseStatus.Text = $"✔ Dati completi per formato lineare: {availableBytes} byte ({w}x{h} celle)";
                    _lblParseStatus.ForeColor = Color.FromArgb(120, 230, 160);
                    _btnOk.Enabled = true;
                }
                else
                {
                    _lblParseStatus.Text = $"⚠ Trovati {availableBytes} byte su {expectedDataBytes} attesi.";
                    _lblParseStatus.ForeColor = Color.FromArgb(255, 180, 80);
                    _btnOk.Enabled = true;
                }
            }
            else // Packed2Bit
            {
                int bytesPerRow = (w + 3) / 4;
                int expectedDataBytes = bytesPerRow * h;
                _lblExpectedStats.Text = $"Byte attesi: {expectedDataBytes} ({bytesPerRow} B/riga x {h} righe, 4 tile/byte)";

                int availableBytes = _parsedBytes.Count;
                if (_chkSkipHeader.Checked)
                {
                    availableBytes = Math.Max(0, availableBytes - 3);
                }

                int completeRows = bytesPerRow > 0 ? (availableBytes / bytesPerRow) : 0;

                if (_parsedBytes.Count == 0)
                {
                    _lblParseStatus.Text = "In attesa di dati...";
                    _lblParseStatus.ForeColor = Color.Yellow;
                    _btnOk.Enabled = false;
                }
                else if (availableBytes == expectedDataBytes)
                {
                    _lblParseStatus.Text = $"✔ Dati completi e validi: {availableBytes} byte ({completeRows}/{h} righe per mappa {w}x{h})";
                    _lblParseStatus.ForeColor = Color.FromArgb(120, 230, 160);
                    _btnOk.Enabled = true;
                }
                else if (availableBytes > expectedDataBytes)
                {
                    int excess = availableBytes - expectedDataBytes;
                    _lblParseStatus.Text = $"⚠ Trovati {availableBytes} byte ({completeRows} righe complete). Eccedenza di +{excess} byte; verranno usati i primi {expectedDataBytes}.";
                    _lblParseStatus.ForeColor = Color.FromArgb(255, 180, 80);
                    _btnOk.Enabled = true;
                }
                else
                {
                    int missing = expectedDataBytes - availableBytes;
                    _lblParseStatus.Text = $"⚠ Trovati {availableBytes} byte su {expectedDataBytes} attesi ({completeRows}/{h} righe complete). Mancano {missing} byte; le celle rimanenti saranno vuote.";
                    _lblParseStatus.ForeColor = Color.FromArgb(255, 140, 100);
                    _btnOk.Enabled = true;
                }
            }
        }

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Tutti i file supportati (*.asm;*.inc;*.txt;*.c;*.h;*.bas;*.bin)|*.asm;*.inc;*.txt;*.c;*.h;*.bas;*.bin|" +
                             "Sorgenti Assembly (*.asm;*.inc;*.txt)|*.asm;*.inc;*.txt|" +
                             "File Binari Raw (*.bin)|*.bin|" +
                             "Sorgenti C (*.c;*.h)|*.c;*.h|" +
                             "Sinclair BASIC (*.bas)|*.bas|" +
                             "Tutti i file (*.*)|*.*";
                ofd.Title = "Carica Dati Mappa da File";

                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        if (Path.GetExtension(ofd.FileName).Equals(".bin", StringComparison.OrdinalIgnoreCase))
                        {
                            byte[] binBytes = File.ReadAllBytes(ofd.FileName);
                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < binBytes.Length; i++)
                            {
                                if (i % 8 == 0)
                                {
                                    if (i > 0) sb.AppendLine();
                                    sb.Append("defb ");
                                }
                                else
                                {
                                    sb.Append(", ");
                                }
                                sb.Append("%" + Convert.ToString(binBytes[i], 2).PadLeft(8, '0'));
                            }
                            _txtCode.Text = sb.ToString();
                        }
                        else
                        {
                            _txtCode.Text = File.ReadAllText(ofd.FileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Impossibile leggere il file:\n{ex.Message}", "Errore File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (_parsedBytes == null || _parsedBytes.Count == 0)
            {
                MessageBox.Show("Nessun dato byte valido trovato nel testo incollato.", "Dati Non Validi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int w = (int)_numWidth.Value;
            int h = (int)_numHeight.Value;

            GameMap targetMap;
            if (_rbNewMap.Checked)
            {
                string name = string.IsNullOrWhiteSpace(_txtMapName.Text) ? $"Mappa {_project.Maps.Count + 1}" : _txtMapName.Text.Trim();
                targetMap = _project.AddMap(name, w, h);
            }
            else
            {
                targetMap = _project.CurrentMap;
                if (targetMap == null)
                {
                    targetMap = _project.AddMap("Mappa 1", w, h);
                }
                else
                {
                    targetMap.PushUndoState();
                }
            }

            if (_selectedFormat == MapImportFormat.ManicMinerAttributes)
            {
                // Mappa per Attributi Manic Miner (512 byte)
                Dictionary<byte, int> customAttrToTile = new Dictionary<byte, int>();

                byte[] expectedAttrs = new byte[] { 0, 66, 2, 22, 4, 68, 5, 0 };
                if (targetMap.ManicMinerData?.TileDefs != null)
                {
                    for (int s = 0; s < 8; s++)
                    {
                        if (targetMap.ManicMinerData.TileDefs[s] != null)
                            expectedAttrs[s] = targetMap.ManicMinerData.TileDefs[s].Attribute;
                    }
                }

                for (int s = 0; s < 8; s++)
                {
                    int tid = (_cmbSlots[s].SelectedItem is TileItem it) ? it.TileId : 0;
                    byte attr = expectedAttrs[s];
                    if (!customAttrToTile.ContainsKey(attr))
                    {
                        customAttrToTile[attr] = tid;
                    }
                }

                // Popola gli 8 AssignedTileIds di targetMap
                if (targetMap.AssignedTileIds == null) targetMap.AssignedTileIds = new List<int>();
                for (int s = 0; s < 8; s++)
                {
                    int tid = (_cmbSlots[s].SelectedItem is TileItem it) ? it.TileId : 0;
                    if (s < targetMap.AssignedTileIds.Count)
                    {
                        if (tid > 0) targetMap.AssignedTileIds[s] = tid;
                    }
                    else if (tid > 0)
                    {
                        targetMap.AssignedTileIds.Add(tid);
                    }
                }

                if (targetMap.ManicMinerData == null)
                {
                    targetMap.ManicMinerData = ManicMinerCavern.CreateCentralCavernDefault();
                    targetMap.ManicMinerData.Name = targetMap.Name;
                }

                MapImporter.ImportManicMinerAttributes(targetMap, _parsedBytes, _project, w, h, customAttrToTile, true);

                MessageBox.Show(
                    $"Caverna '{targetMap.Name}' importata con successo nel formato Manic Miner!\n\n" +
                    $"Dimensioni: {targetMap.Width}x{targetMap.Height} celle (512 byte)\n" +
                    $"Tile mappate per attributo: {customAttrToTile.Count}",
                    "Importazione Caverna Riuscita",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (_selectedFormat == MapImportFormat.Linear1BytePerCell)
            {
                List<int> slotTileIds = new List<int>();
                for (int s = 0; s < 8; s++)
                {
                    int tid = (_cmbSlots[s].SelectedItem is TileItem it) ? it.TileId : 0;
                    slotTileIds.Add(tid);
                }

                MapImporter.ImportLinear1BytePerCell(targetMap, _parsedBytes, w, h, slotTileIds, true, _project);

                MessageBox.Show(
                    $"Mappa '{targetMap.Name}' importata con successo nel formato lineare 1 byte/cella!\n\n" +
                    $"Dimensioni: {targetMap.Width}x{targetMap.Height} celle",
                    "Importazione Riuscita",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else // Packed2Bit
            {
                int slot0 = (_cmbSlots[0].SelectedItem is TileItem it0) ? it0.TileId : 0;
                int slot1 = (_cmbSlots[1].SelectedItem is TileItem it1) ? it1.TileId : 0;
                int slot2 = (_cmbSlots[2].SelectedItem is TileItem it2) ? it2.TileId : 0;
                int slot3 = (_cmbSlots[3].SelectedItem is TileItem it3) ? it3.TileId : 0;

                bool keepExisting = _chkKeepExistingTiles.Checked;
                MapImporter.ImportPacked2Bit(targetMap, _parsedBytes, w, h, slot0, slot1, slot2, slot3, _chkSkipHeader.Checked, keepExisting);

                MessageBox.Show(
                    $"Mappa '{targetMap.Name}' importata con successo nel formato compresso 2-bit!\n\n" +
                    $"Dimensioni: {targetMap.Width}x{targetMap.Height} celle\n" +
                    $"Tile nel Set Mappa: {targetMap.AssignedTileIds.Count} (mantenute intatte)",
                    "Importazione Riuscita",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
