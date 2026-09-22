namespace ZXSpectrumSpriteGenerator.UI
{
    partial class MapEditorView
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        private void InitializeComponent()
        {
            this._topBar = new System.Windows.Forms.Panel();
            this._tsTools = new System.Windows.Forms.ToolStrip();
            this._lblTools = new System.Windows.Forms.ToolStripLabel();
            this._btnToolPencil = new System.Windows.Forms.ToolStripButton();
            this._btnToolEraser = new System.Windows.Forms.ToolStripButton();
            this._btnToolFill = new System.Windows.Forms.ToolStripButton();
            this._btnToolRect = new System.Windows.Forms.ToolStripButton();
            this._btnToolRectFill = new System.Windows.Forms.ToolStripButton();
            this._btnToolPicker = new System.Windows.Forms.ToolStripButton();
            this._btnClear = new System.Windows.Forms.ToolStripButton();
            this._sepTools1 = new System.Windows.Forms.ToolStripSeparator();
            this._btnUndo = new System.Windows.Forms.ToolStripButton();
            this._btnRedo = new System.Windows.Forms.ToolStripButton();
            this._sepTools2 = new System.Windows.Forms.ToolStripSeparator();
            this._btnGrid = new System.Windows.Forms.ToolStripButton();
            this._sepTools3 = new System.Windows.Forms.ToolStripSeparator();
            this._btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this._lblZoom = new System.Windows.Forms.ToolStripLabel();
            this._btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this._tsMgmt = new System.Windows.Forms.ToolStrip();
            this._btnGoToTiles = new System.Windows.Forms.ToolStripButton();
            this._btnBackToSprites = new System.Windows.Forms.ToolStripButton();
            this._sepMgmt1 = new System.Windows.Forms.ToolStripSeparator();
            this._lblM = new System.Windows.Forms.ToolStripLabel();
            this._cmbMaps = new System.Windows.Forms.ToolStripComboBox();
            this._btnNewMap = new System.Windows.Forms.ToolStripButton();
            this._btnCloneMap = new System.Windows.Forms.ToolStripButton();
            this._btnDeleteMap = new System.Windows.Forms.ToolStripButton();
            this._btnRenameMap = new System.Windows.Forms.ToolStripButton();
            this._btnResizeMap = new System.Windows.Forms.ToolStripButton();
            this._sepMgmt2 = new System.Windows.Forms.ToolStripSeparator();
            this._btnExport = new System.Windows.Forms.ToolStripButton();
            this._btnImport = new System.Windows.Forms.ToolStripButton();
            this._btnPng = new System.Windows.Forms.ToolStripButton();
            this._rightPanel = new System.Windows.Forms.Panel();
            this._lblDragHint = new System.Windows.Forms.Label();
            this._pnlAssignedTiles = new System.Windows.Forms.Panel();
            this._lblLibraryTitle = new System.Windows.Forms.Label();
            this._pnlLibraryTiles = new System.Windows.Forms.Panel();
            this._btnAddTile = new System.Windows.Forms.Button();
            this._btnGoToTileEditor = new System.Windows.Forms.Button();
            this._lblPalTitle = new System.Windows.Forms.Label();
            this._lblTilesPerMap = new System.Windows.Forms.Label();
            this._numTilesPerMap = new System.Windows.Forms.NumericUpDown();
            this._previewBox = new System.Windows.Forms.Panel();
            this._lblActiveTileName = new System.Windows.Forms.Label();
            this._picActiveTile = new System.Windows.Forms.PictureBox();
            this._lblTileTitle = new System.Windows.Forms.Label();
            this._centerPanel = new System.Windows.Forms.Panel();
            this._canvas = new ZXSpectrumSpriteGenerator.UI.Controls.MapCanvas();
            this._topBar.SuspendLayout();
            this._tsTools.SuspendLayout();
            this._tsMgmt.SuspendLayout();
            this._rightPanel.SuspendLayout();
            this._previewBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._picActiveTile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._numTilesPerMap)).BeginInit();
            this._centerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _topBar
            // 
            this._topBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this._topBar.Controls.Add(this._tsTools);
            this._topBar.Controls.Add(this._tsMgmt);
            this._topBar.Dock = System.Windows.Forms.DockStyle.Top;
            this._topBar.Location = new System.Drawing.Point(0, 0);
            this._topBar.Name = "_topBar";
            this._topBar.Size = new System.Drawing.Size(982, 68);
            this._topBar.TabIndex = 0;
            // 
            // _tsTools
            // 
            this._tsTools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(26)))));
            this._tsTools.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._tsTools.ForeColor = System.Drawing.Color.White;
            this._tsTools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._tsTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._lblTools,
            this._btnToolPencil,
            this._btnToolEraser,
            this._btnToolFill,
            this._btnToolRect,
            this._btnToolRectFill,
            this._btnToolPicker,
            this._btnClear,
            this._sepTools1,
            this._btnUndo,
            this._btnRedo,
            this._sepTools2,
            this._btnGrid,
            this._sepTools3,
            this._btnZoomOut,
            this._lblZoom,
            this._btnZoomIn});
            this._tsTools.Location = new System.Drawing.Point(0, 43);
            this._tsTools.Name = "_tsTools";
            this._tsTools.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this._tsTools.Size = new System.Drawing.Size(982, 25);
            this._tsTools.TabIndex = 1;
            // 
            // _lblTools
            // 
            this._lblTools.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._lblTools.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this._lblTools.Name = "_lblTools";
            this._lblTools.Size = new System.Drawing.Size(70, 22);
            this._lblTools.Text = "Strumenti: ";
            // 
            // _btnToolPencil
            // 
            this._btnToolPencil.Checked = true;
            this._btnToolPencil.CheckState = System.Windows.Forms.CheckState.Checked;
            this._btnToolPencil.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolPencil.Name = "_btnToolPencil";
            this._btnToolPencil.Size = new System.Drawing.Size(60, 22);
            this._btnToolPencil.Text = "✏️ Matita";
            this._btnToolPencil.Click += new System.EventHandler(this.BtnToolPencil_Click);
            // 
            // _btnToolEraser
            // 
            this._btnToolEraser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolEraser.Name = "_btnToolEraser";
            this._btnToolEraser.Size = new System.Drawing.Size(69, 22);
            this._btnToolEraser.Text = "🧹 Gomma";
            this._btnToolEraser.Click += new System.EventHandler(this.BtnToolEraser_Click);
            // 
            // _btnToolFill
            // 
            this._btnToolFill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolFill.Name = "_btnToolFill";
            this._btnToolFill.Size = new System.Drawing.Size(79, 22);
            this._btnToolFill.Text = "🪣 Secchiello";
            this._btnToolFill.Click += new System.EventHandler(this.BtnToolFill_Click);
            // 
            // _btnToolRect
            // 
            this._btnToolRect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolRect.Name = "_btnToolRect";
            this._btnToolRect.Size = new System.Drawing.Size(82, 22);
            this._btnToolRect.Text = "⬜ Rettangolo";
            this._btnToolRect.Click += new System.EventHandler(this.BtnToolRect_Click);
            // 
            // _btnToolRectFill
            // 
            this._btnToolRectFill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolRectFill.Name = "_btnToolRectFill";
            this._btnToolRectFill.Size = new System.Drawing.Size(83, 22);
            this._btnToolRectFill.Text = "⬛ Rett. Pieno";
            this._btnToolRectFill.Click += new System.EventHandler(this.BtnToolRectFill_Click);
            // 
            // _btnToolPicker
            // 
            this._btnToolPicker.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolPicker.Name = "_btnToolPicker";
            this._btnToolPicker.Size = new System.Drawing.Size(63, 22);
            this._btnToolPicker.Text = "💧 Pipetta";
            this._btnToolPicker.Click += new System.EventHandler(this.BtnToolPicker_Click);
            // 
            // _btnClear
            // 
            this._btnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnClear.Name = "_btnClear";
            this._btnClear.Size = new System.Drawing.Size(45, 22);
            this._btnClear.Text = "Pulisci";
            this._btnClear.ToolTipText = "Cancella l\'intera mappa con tile vuoto";
            this._btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // _sepTools1
            // 
            this._sepTools1.Name = "_sepTools1";
            this._sepTools1.Size = new System.Drawing.Size(6, 25);
            // 
            // _btnUndo
            // 
            this._btnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnUndo.Name = "_btnUndo";
            this._btnUndo.Size = new System.Drawing.Size(65, 22);
            this._btnUndo.Text = "↶ Annulla";
            this._btnUndo.ToolTipText = "Annulla ultima modifica mappa";
            this._btnUndo.Click += new System.EventHandler(this.BtnUndo_Click);
            // 
            // _btnRedo
            // 
            this._btnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnRedo.Name = "_btnRedo";
            this._btnRedo.Size = new System.Drawing.Size(54, 22);
            this._btnRedo.Text = "↷ Ripeti";
            this._btnRedo.ToolTipText = "Ripeti modifica mappa";
            this._btnRedo.Click += new System.EventHandler(this.BtnRedo_Click);
            // 
            // _sepTools2
            // 
            this._sepTools2.Name = "_sepTools2";
            this._sepTools2.Size = new System.Drawing.Size(6, 25);
            // 
            // _btnGrid
            // 
            this._btnGrid.Checked = true;
            this._btnGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this._btnGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnGrid.Name = "_btnGrid";
            this._btnGrid.Size = new System.Drawing.Size(66, 22);
            this._btnGrid.Text = "Griglia Tile";
            this._btnGrid.Click += new System.EventHandler(this.BtnGrid_Click);
            // 
            // _sepTools3
            // 
            this._sepTools3.Name = "_sepTools3";
            this._sepTools3.Size = new System.Drawing.Size(6, 25);
            // 
            // _btnZoomOut
            // 
            this._btnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnZoomOut.Name = "_btnZoomOut";
            this._btnZoomOut.Size = new System.Drawing.Size(51, 22);
            this._btnZoomOut.Text = "Zoom -";
            this._btnZoomOut.Click += new System.EventHandler(this.BtnZoomOut_Click);
            // 
            // _lblZoom
            // 
            this._lblZoom.ForeColor = System.Drawing.Color.LightGray;
            this._lblZoom.Name = "_lblZoom";
            this._lblZoom.Size = new System.Drawing.Size(19, 22);
            this._lblZoom.Text = "2x";
            // 
            // _btnZoomIn
            // 
            this._btnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnZoomIn.Name = "_btnZoomIn";
            this._btnZoomIn.Size = new System.Drawing.Size(54, 22);
            this._btnZoomIn.Text = "Zoom +";
            this._btnZoomIn.Click += new System.EventHandler(this.BtnZoomIn_Click);
            // 
            // _tsMgmt
            // 
            this._tsMgmt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this._tsMgmt.ForeColor = System.Drawing.Color.White;
            this._tsMgmt.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._tsMgmt.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnGoToTiles,
            this._btnBackToSprites,
            this._sepMgmt1,
            this._lblM,
            this._cmbMaps,
            this._btnNewMap,
            this._btnCloneMap,
            this._btnDeleteMap,
            this._btnRenameMap,
            this._btnResizeMap,
            this._sepMgmt2,
            this._btnExport,
            this._btnImport,
            this._btnPng});
            this._tsMgmt.Location = new System.Drawing.Point(0, 0);
            this._tsMgmt.Name = "_tsMgmt";
            this._tsMgmt.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this._tsMgmt.Size = new System.Drawing.Size(982, 25);
            this._tsMgmt.TabIndex = 0;
            // 
            // _btnGoToTiles
            // 
            this._btnGoToTiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this._btnGoToTiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnGoToTiles.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._btnGoToTiles.ForeColor = System.Drawing.Color.White;
            this._btnGoToTiles.Name = "_btnGoToTiles";
            this._btnGoToTiles.Size = new System.Drawing.Size(112, 22);
            this._btnGoToTiles.Text = "🧱 Editor Tiles (F3)";
            this._btnGoToTiles.ToolTipText = "Apri l\'Editor dedicato dei Tile 8x8 (Tasto rapido: F3)";
            this._btnGoToTiles.Click += new System.EventHandler(this.BtnGoToTiles_Click);
            // 
            // _btnBackToSprites
            // 
            this._btnBackToSprites.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(50)))), ((int)(((byte)(95)))));
            this._btnBackToSprites.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnBackToSprites.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._btnBackToSprites.ForeColor = System.Drawing.Color.White;
            this._btnBackToSprites.Name = "_btnBackToSprites";
            this._btnBackToSprites.Size = new System.Drawing.Size(85, 22);
            this._btnBackToSprites.Text = "👾 Sprite (F2)";
            this._btnBackToSprites.ToolTipText = "Torna all\'Editor di Disegno Sprite e Nemici (Tasto rapido: F2)";
            this._btnBackToSprites.Click += new System.EventHandler(this.BtnBackToSprites_Click);
            // 
            // _sepMgmt1
            // 
            this._sepMgmt1.Name = "_sepMgmt1";
            this._sepMgmt1.Size = new System.Drawing.Size(6, 25);
            // 
            // _lblM
            // 
            this._lblM.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._lblM.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this._lblM.Name = "_lblM";
            this._lblM.Size = new System.Drawing.Size(66, 22);
            this._lblM.Text = "🗺️ Mappa: ";
            // 
            // _cmbMaps
            // 
            this._cmbMaps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this._cmbMaps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbMaps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._cmbMaps.ForeColor = System.Drawing.Color.White;
            this._cmbMaps.Name = "_cmbMaps";
            this._cmbMaps.Size = new System.Drawing.Size(180, 25);
            this._cmbMaps.SelectedIndexChanged += new System.EventHandler(this.CmbMaps_SelectedIndexChanged);
            // 
            // _btnNewMap
            // 
            this._btnNewMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this._btnNewMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnNewMap.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._btnNewMap.ForeColor = System.Drawing.Color.White;
            this._btnNewMap.Name = "_btnNewMap";
            this._btnNewMap.Size = new System.Drawing.Size(112, 22);
            this._btnNewMap.Text = "➕ Nuova Mappa...";
            this._btnNewMap.Click += new System.EventHandler(this.BtnNewMap_Click);
            // 
            // _btnCloneMap
            // 
            this._btnCloneMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnCloneMap.Name = "_btnCloneMap";
            this._btnCloneMap.Size = new System.Drawing.Size(66, 22);
            this._btnCloneMap.Text = "📋 Duplica";
            this._btnCloneMap.ToolTipText = "Duplica la mappa corrente";
            this._btnCloneMap.Click += new System.EventHandler(this.BtnCloneMap_Click);
            // 
            // _btnDeleteMap
            // 
            this._btnDeleteMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnDeleteMap.Name = "_btnDeleteMap";
            this._btnDeleteMap.Size = new System.Drawing.Size(65, 22);
            this._btnDeleteMap.Text = "🗑️ Elimina";
            this._btnDeleteMap.ToolTipText = "Elimina la mappa corrente";
            this._btnDeleteMap.Click += new System.EventHandler(this.BtnDeleteMap_Click);
            // 
            // _btnRenameMap
            // 
            this._btnRenameMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnRenameMap.Name = "_btnRenameMap";
            this._btnRenameMap.Size = new System.Drawing.Size(77, 22);
            this._btnRenameMap.Text = "✏️ Rinomina";
            this._btnRenameMap.ToolTipText = "Rinomina la mappa corrente";
            this._btnRenameMap.Click += new System.EventHandler(this.BtnRenameMap_Click);
            // 
            // _btnResizeMap
            // 
            this._btnResizeMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnResizeMap.Name = "_btnResizeMap";
            this._btnResizeMap.Size = new System.Drawing.Size(95, 22);
            this._btnResizeMap.Text = "📐 Dimensioni...";
            this._btnResizeMap.ToolTipText = "Modifica dimensioni mappa in celle tile";
            this._btnResizeMap.Click += new System.EventHandler(this.BtnResizeMap_Click);
            // 
            // _sepMgmt2
            // 
            this._sepMgmt2.Name = "_sepMgmt2";
            this._sepMgmt2.Size = new System.Drawing.Size(6, 25);
            // 
            // _btnExport
            // 
            this._btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(130)))), ((int)(((byte)(80)))));
            this._btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnExport.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._btnExport.ForeColor = System.Drawing.Color.White;
            this._btnExport.Name = "_btnExport";
            this._btnExport.Size = new System.Drawing.Size(143, 19);
            this._btnExport.Text = "⚡ Esporta Dati Mappa...";
            this._btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // _btnImport
            // 
            this._btnImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this._btnImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnImport.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._btnImport.ForeColor = System.Drawing.Color.White;
            this._btnImport.Name = "_btnImport";
            this._btnImport.Size = new System.Drawing.Size(126, 22);
            this._btnImport.Text = "📥 Importa Mappa...";
            this._btnImport.Click += new System.EventHandler(this.BtnImport_Click);
            // 
            // _btnPng
            // 
            this._btnPng.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnPng.Name = "_btnPng";
            this._btnPng.Size = new System.Drawing.Size(89, 19);
            this._btnPng.Text = "📷 Salva PNG...";
            this._btnPng.Click += new System.EventHandler(this.BtnPng_Click);
            // 
            // _rightPanel
            // 
            this._rightPanel.AutoScroll = false;
            this._rightPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(31)))));
            this._rightPanel.Controls.Add(this._pnlLibraryTiles);
            this._rightPanel.Controls.Add(this._btnGoToTileEditor);
            this._rightPanel.Controls.Add(this._btnAddTile);
            this._rightPanel.Controls.Add(this._lblLibraryTitle);
            this._rightPanel.Controls.Add(this._pnlAssignedTiles);
            this._rightPanel.Controls.Add(this._lblDragHint);
            this._rightPanel.Controls.Add(this._numTilesPerMap);
            this._rightPanel.Controls.Add(this._lblTilesPerMap);
            this._rightPanel.Controls.Add(this._lblPalTitle);
            this._rightPanel.Controls.Add(this._previewBox);
            this._rightPanel.Controls.Add(this._lblTileTitle);
            this._rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this._rightPanel.Location = new System.Drawing.Point(692, 68);
            this._rightPanel.Name = "_rightPanel";
            this._rightPanel.Padding = new System.Windows.Forms.Padding(10);
            this._rightPanel.Size = new System.Drawing.Size(290, 686);
            this._rightPanel.TabIndex = 1;
            // 
            // _pnlAssignedTiles
            // 
            this._pnlAssignedTiles.AllowDrop = true;
            this._pnlAssignedTiles.AutoScroll = true;
            this._pnlAssignedTiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(22)))));
            this._pnlAssignedTiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pnlAssignedTiles.Location = new System.Drawing.Point(10, 126);
            this._pnlAssignedTiles.Name = "_pnlAssignedTiles";
            this._pnlAssignedTiles.Size = new System.Drawing.Size(260, 240);
            this._pnlAssignedTiles.TabIndex = 3;
            // 
            // _lblLibraryTitle
            // 
            this._lblLibraryTitle.AutoSize = true;
            this._lblLibraryTitle.Font = new System.Drawing.Font("Segoe UI", 8.2F, System.Drawing.FontStyle.Bold);
            this._lblLibraryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(0)))));
            this._lblLibraryTitle.Location = new System.Drawing.Point(10, 374);
            this._lblLibraryTitle.Name = "_lblLibraryTitle";
            this._lblLibraryTitle.Size = new System.Drawing.Size(160, 13);
            this._lblLibraryTitle.TabIndex = 17;
            this._lblLibraryTitle.Text = "📚 LIBRERIA TILE PROGETTO";
            // 
            // _btnAddTile
            // 
            this._btnAddTile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this._btnAddTile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnAddTile.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold);
            this._btnAddTile.ForeColor = System.Drawing.Color.White;
            this._btnAddTile.Location = new System.Drawing.Point(10, 392);
            this._btnAddTile.Name = "_btnAddTile";
            this._btnAddTile.Size = new System.Drawing.Size(126, 24);
            this._btnAddTile.TabIndex = 12;
            this._btnAddTile.Text = "➕ Nuova Tile...";
            this._btnAddTile.UseVisualStyleBackColor = false;
            this._btnAddTile.Click += new System.EventHandler(this.BtnAddTile_Click);
            // 
            // _btnGoToTileEditor
            // 
            this._btnGoToTileEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(75)))), ((int)(((byte)(115)))));
            this._btnGoToTileEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnGoToTileEditor.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold);
            this._btnGoToTileEditor.ForeColor = System.Drawing.Color.White;
            this._btnGoToTileEditor.Location = new System.Drawing.Point(140, 392);
            this._btnGoToTileEditor.Name = "_btnGoToTileEditor";
            this._btnGoToTileEditor.Size = new System.Drawing.Size(130, 24);
            this._btnGoToTileEditor.TabIndex = 13;
            this._btnGoToTileEditor.Text = "🧱 Editor (F3)";
            this._btnGoToTileEditor.UseVisualStyleBackColor = false;
            this._btnGoToTileEditor.Click += new System.EventHandler(this.BtnGoToTiles_Click);
            // 
            // _pnlLibraryTiles
            // 
            this._pnlLibraryTiles.AutoScroll = true;
            this._pnlLibraryTiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(22)))));
            this._pnlLibraryTiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pnlLibraryTiles.Location = new System.Drawing.Point(10, 420);
            this._pnlLibraryTiles.Name = "_pnlLibraryTiles";
            this._pnlLibraryTiles.Size = new System.Drawing.Size(260, 250);
            this._pnlLibraryTiles.TabIndex = 18;
            // 
            // _lblDragHint
            // 
            this._lblDragHint.AutoSize = true;
            this._lblDragHint.Font = new System.Drawing.Font("Segoe UI", 7.2F, System.Drawing.FontStyle.Italic);
            this._lblDragHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(180)))), ((int)(((byte)(200)))));
            this._lblDragHint.Location = new System.Drawing.Point(10, 109);
            this._lblDragHint.Name = "_lblDragHint";
            this._lblDragHint.Size = new System.Drawing.Size(200, 12);
            this._lblDragHint.TabIndex = 16;
            this._lblDragHint.Text = "Trascina qui le tile dalla Libreria sottostante:";
            // 
            // _numTilesPerMap
            // 
            this._numTilesPerMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(44)))));
            this._numTilesPerMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._numTilesPerMap.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._numTilesPerMap.ForeColor = System.Drawing.Color.White;
            this._numTilesPerMap.Location = new System.Drawing.Point(216, 84);
            this._numTilesPerMap.Maximum = new decimal(new int[] { 256, 0, 0, 0 });
            this._numTilesPerMap.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this._numTilesPerMap.Name = "_numTilesPerMap";
            this._numTilesPerMap.Size = new System.Drawing.Size(54, 22);
            this._numTilesPerMap.TabIndex = 15;
            this._numTilesPerMap.Value = new decimal(new int[] { 8, 0, 0, 0 });
            this._numTilesPerMap.ValueChanged += new System.EventHandler(this.NumTilesPerMap_ValueChanged);
            // 
            // _lblTilesPerMap
            // 
            this._lblTilesPerMap.AutoSize = true;
            this._lblTilesPerMap.Font = new System.Drawing.Font("Segoe UI", 7.6F);
            this._lblTilesPerMap.ForeColor = System.Drawing.Color.LightGray;
            this._lblTilesPerMap.Location = new System.Drawing.Point(145, 88);
            this._lblTilesPerMap.Name = "_lblTilesPerMap";
            this._lblTilesPerMap.Size = new System.Drawing.Size(65, 12);
            this._lblTilesPerMap.TabIndex = 14;
            this._lblTilesPerMap.Text = "Tile/Mappa:";
            // 
            // _lblPalTitle
            // 
            this._lblPalTitle.AutoSize = true;
            this._lblPalTitle.Font = new System.Drawing.Font("Segoe UI", 8.2F, System.Drawing.FontStyle.Bold);
            this._lblPalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this._lblPalTitle.Location = new System.Drawing.Point(10, 87);
            this._lblPalTitle.Name = "_lblPalTitle";
            this._lblPalTitle.Size = new System.Drawing.Size(120, 13);
            this._lblPalTitle.TabIndex = 2;
            this._lblPalTitle.Text = "📌 SET MAPPA (0/8)";
            // 
            // _previewBox
            // 
            this._previewBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this._previewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._previewBox.Controls.Add(this._lblActiveTileName);
            this._previewBox.Controls.Add(this._picActiveTile);
            this._previewBox.Location = new System.Drawing.Point(10, 24);
            this._previewBox.Name = "_previewBox";
            this._previewBox.Size = new System.Drawing.Size(260, 56);
            this._previewBox.TabIndex = 1;
            // 
            // _lblActiveTileName
            // 
            this._lblActiveTileName.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold);
            this._lblActiveTileName.ForeColor = System.Drawing.Color.Yellow;
            this._lblActiveTileName.Location = new System.Drawing.Point(54, 4);
            this._lblActiveTileName.Name = "_lblActiveTileName";
            this._lblActiveTileName.Size = new System.Drawing.Size(200, 46);
            this._lblActiveTileName.TabIndex = 1;
            this._lblActiveTileName.Text = "Tile #1: Nemico_Pos1_Nord";
            // 
            // _picActiveTile
            // 
            this._picActiveTile.BackColor = System.Drawing.Color.Black;
            this._picActiveTile.Location = new System.Drawing.Point(6, 6);
            this._picActiveTile.Name = "_picActiveTile";
            this._picActiveTile.Size = new System.Drawing.Size(42, 42);
            this._picActiveTile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._picActiveTile.TabIndex = 0;
            this._picActiveTile.TabStop = false;
            // 
            // _lblTileTitle
            // 
            this._lblTileTitle.AutoSize = true;
            this._lblTileTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._lblTileTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this._lblTileTitle.Location = new System.Drawing.Point(10, 6);
            this._lblTileTitle.Name = "_lblTileTitle";
            this._lblTileTitle.Size = new System.Drawing.Size(75, 15);
            this._lblTileTitle.TabIndex = 0;
            this._lblTileTitle.Text = "TILE ATTIVO";
            // 
            // _centerPanel
            // 
            this._centerPanel.AutoScroll = true;
            this._centerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(20)))));
            this._centerPanel.Controls.Add(this._canvas);
            this._centerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._centerPanel.Location = new System.Drawing.Point(0, 68);
            this._centerPanel.Name = "_centerPanel";
            this._centerPanel.Size = new System.Drawing.Size(692, 686);
            this._centerPanel.TabIndex = 2;
            this._centerPanel.Resize += new System.EventHandler(this.CenterPanel_Resize);
            // 
            // _canvas
            // 
            this._canvas.ActiveTileId = 1;
            this._canvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(18)))));
            this._canvas.CurrentTool = ZXSpectrumSpriteGenerator.UI.Controls.MapDrawingTool.Pencil;
            this._canvas.Cursor = System.Windows.Forms.Cursors.Cross;
            this._canvas.Location = new System.Drawing.Point(10, 10);
            this._canvas.Name = "_canvas";
            this._canvas.ScaleFactor = 2;
            this._canvas.ShowGrid = true;
            this._canvas.Size = new System.Drawing.Size(512, 384);
            this._canvas.TabIndex = 0;
            this._canvas.Text = "mapCanvas";
            this._canvas.MapChanged += new System.EventHandler(this.Canvas_MapChanged);
            this._canvas.HoverCoordinatesChanged += new System.EventHandler<ZXSpectrumSpriteGenerator.UI.Controls.MapHoverEventArgs>(this.Canvas_HoverCoordinatesChanged);
            this._canvas.TilePicked += new System.EventHandler<int>(this.Canvas_TilePicked);
            // 
            // MapEditorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(24)))));
            this.Controls.Add(this._centerPanel);
            this.Controls.Add(this._rightPanel);
            this.Controls.Add(this._topBar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "MapEditorView";
            this.Size = new System.Drawing.Size(982, 754);
            this._topBar.ResumeLayout(false);
            this._topBar.PerformLayout();
            this._tsTools.ResumeLayout(false);
            this._tsTools.PerformLayout();
            this._tsMgmt.ResumeLayout(false);
            this._tsMgmt.PerformLayout();
            this._rightPanel.ResumeLayout(false);
            this._rightPanel.PerformLayout();
            this._previewBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._picActiveTile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._numTilesPerMap)).EndInit();
            this._centerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _topBar;
        private System.Windows.Forms.ToolStrip _tsMgmt;
        private System.Windows.Forms.ToolStripButton _btnGoToTiles;
        private System.Windows.Forms.ToolStripButton _btnBackToSprites;
        private System.Windows.Forms.ToolStripSeparator _sepMgmt1;
        private System.Windows.Forms.ToolStripLabel _lblM;
        private System.Windows.Forms.ToolStripComboBox _cmbMaps;
        private System.Windows.Forms.ToolStripButton _btnNewMap;
        private System.Windows.Forms.ToolStripButton _btnCloneMap;
        private System.Windows.Forms.ToolStripButton _btnDeleteMap;
        private System.Windows.Forms.ToolStripButton _btnRenameMap;
        private System.Windows.Forms.ToolStripButton _btnResizeMap;
        private System.Windows.Forms.ToolStripSeparator _sepMgmt2;
        private System.Windows.Forms.ToolStripButton _btnExport;
        private System.Windows.Forms.ToolStripButton _btnImport;
        private System.Windows.Forms.ToolStripButton _btnPng;
        private System.Windows.Forms.ToolStrip _tsTools;
        private System.Windows.Forms.ToolStripLabel _lblTools;
        private System.Windows.Forms.ToolStripButton _btnToolPencil;
        private System.Windows.Forms.ToolStripButton _btnToolEraser;
        private System.Windows.Forms.ToolStripButton _btnToolFill;
        private System.Windows.Forms.ToolStripButton _btnToolRect;
        private System.Windows.Forms.ToolStripButton _btnToolRectFill;
        private System.Windows.Forms.ToolStripButton _btnToolPicker;
        private System.Windows.Forms.ToolStripButton _btnClear;
        private System.Windows.Forms.ToolStripSeparator _sepTools1;
        private System.Windows.Forms.ToolStripButton _btnUndo;
        private System.Windows.Forms.ToolStripButton _btnRedo;
        private System.Windows.Forms.ToolStripSeparator _sepTools2;
        private System.Windows.Forms.ToolStripButton _btnGrid;
        private System.Windows.Forms.ToolStripSeparator _sepTools3;
        private System.Windows.Forms.ToolStripButton _btnZoomOut;
        private System.Windows.Forms.ToolStripLabel _lblZoom;
        private System.Windows.Forms.ToolStripButton _btnZoomIn;
        private System.Windows.Forms.Panel _rightPanel;
        private System.Windows.Forms.Label _lblTileTitle;
        private System.Windows.Forms.Panel _previewBox;
        private System.Windows.Forms.PictureBox _picActiveTile;
        private System.Windows.Forms.Label _lblActiveTileName;
        private System.Windows.Forms.Label _lblPalTitle;
        private System.Windows.Forms.Label _lblTilesPerMap;
        private System.Windows.Forms.NumericUpDown _numTilesPerMap;
        private System.Windows.Forms.Label _lblDragHint;
        private System.Windows.Forms.Panel _pnlAssignedTiles;
        private System.Windows.Forms.Label _lblLibraryTitle;
        private System.Windows.Forms.Button _btnAddTile;
        private System.Windows.Forms.Button _btnGoToTileEditor;
        private System.Windows.Forms.Panel _pnlLibraryTiles;
        private System.Windows.Forms.Panel _centerPanel;
        private ZXSpectrumSpriteGenerator.UI.Controls.MapCanvas _canvas;
    }
}
