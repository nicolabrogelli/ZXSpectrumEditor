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
            this._cmbMaps = new System.Windows.Forms.ComboBox();
            this._hostCombo = new System.Windows.Forms.ToolStripControlHost(this._cmbMaps);
            this._btnNewMap = new System.Windows.Forms.ToolStripButton();
            this._btnCloneMap = new System.Windows.Forms.ToolStripButton();
            this._btnDeleteMap = new System.Windows.Forms.ToolStripButton();
            this._btnRenameMap = new System.Windows.Forms.ToolStripButton();
            this._btnResizeMap = new System.Windows.Forms.ToolStripButton();
            this._sepMgmt2 = new System.Windows.Forms.ToolStripSeparator();
            this._btnExport = new System.Windows.Forms.ToolStripButton();
            this._btnPng = new System.Windows.Forms.ToolStripButton();
            this._rightPanel = new System.Windows.Forms.Panel();
            this._btnCopyAsm = new System.Windows.Forms.Button();
            this._txtQuickMapAsm = new System.Windows.Forms.TextBox();
            this._lblAsmTitle = new System.Windows.Forms.Label();
            this._lblHoverCoords = new System.Windows.Forms.Label();
            this._lblMemory = new System.Windows.Forms.Label();
            this._lblPixels = new System.Windows.Forms.Label();
            this._lblDimensions = new System.Windows.Forms.Label();
            this._lblSpecTitle = new System.Windows.Forms.Label();
            this._pnlTileset = new System.Windows.Forms.Panel();
            this._lblPalTitle = new System.Windows.Forms.Label();
            this._btnAddTile = new System.Windows.Forms.Button();
            this._btnGoToTileEditor = new System.Windows.Forms.Button();
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
            this._topBar.Size = new System.Drawing.Size(950, 68);
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
            this._tsTools.Location = new System.Drawing.Point(0, 34);
            this._tsTools.Name = "_tsTools";
            this._tsTools.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this._tsTools.Size = new System.Drawing.Size(950, 34);
            this._tsTools.TabIndex = 1;
            // 
            // _lblTools
            // 
            this._lblTools.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._lblTools.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this._lblTools.Name = "_lblTools";
            this._lblTools.Size = new System.Drawing.Size(64, 31);
            this._lblTools.Text = "Strumenti: ";
            // 
            // _btnToolPencil
            // 
            this._btnToolPencil.Checked = true;
            this._btnToolPencil.CheckState = System.Windows.Forms.CheckState.Checked;
            this._btnToolPencil.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolPencil.Name = "_btnToolPencil";
            this._btnToolPencil.Size = new System.Drawing.Size(56, 31);
            this._btnToolPencil.Text = "✏️ Matita";
            this._btnToolPencil.Click += new System.EventHandler(this.BtnToolPencil_Click);
            // 
            // _btnToolEraser
            // 
            this._btnToolEraser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolEraser.Name = "_btnToolEraser";
            this._btnToolEraser.Size = new System.Drawing.Size(63, 31);
            this._btnToolEraser.Text = "🧹 Gomma";
            this._btnToolEraser.Click += new System.EventHandler(this.BtnToolEraser_Click);
            // 
            // _btnToolFill
            // 
            this._btnToolFill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolFill.Name = "_btnToolFill";
            this._btnToolFill.Size = new System.Drawing.Size(73, 31);
            this._btnToolFill.Text = "🪣 Secchiello";
            this._btnToolFill.Click += new System.EventHandler(this.BtnToolFill_Click);
            // 
            // _btnToolRect
            // 
            this._btnToolRect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolRect.Name = "_btnToolRect";
            this._btnToolRect.Size = new System.Drawing.Size(78, 31);
            this._btnToolRect.Text = "⬜ Rettangolo";
            this._btnToolRect.Click += new System.EventHandler(this.BtnToolRect_Click);
            // 
            // _btnToolRectFill
            // 
            this._btnToolRectFill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolRectFill.Name = "_btnToolRectFill";
            this._btnToolRectFill.Size = new System.Drawing.Size(79, 31);
            this._btnToolRectFill.Text = "⬛ Rett. Pieno";
            this._btnToolRectFill.Click += new System.EventHandler(this.BtnToolRectFill_Click);
            // 
            // _btnToolPicker
            // 
            this._btnToolPicker.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolPicker.Name = "_btnToolPicker";
            this._btnToolPicker.Size = new System.Drawing.Size(59, 31);
            this._btnToolPicker.Text = "💧 Pipetta";
            this._btnToolPicker.Click += new System.EventHandler(this.BtnToolPicker_Click);
            // 
            // _btnClear
            // 
            this._btnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnClear.Name = "_btnClear";
            this._btnClear.Size = new System.Drawing.Size(46, 31);
            this._btnClear.Text = "Pulisci";
            this._btnClear.ToolTipText = "Cancella l\'intera mappa con tile vuoto";
            this._btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // _sepTools1
            // 
            this._sepTools1.Name = "_sepTools1";
            this._sepTools1.Size = new System.Drawing.Size(6, 34);
            // 
            // _btnUndo
            // 
            this._btnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnUndo.Name = "_btnUndo";
            this._btnUndo.Size = new System.Drawing.Size(61, 31);
            this._btnUndo.Text = "↶ Annulla";
            this._btnUndo.ToolTipText = "Annulla ultima modifica mappa";
            this._btnUndo.Click += new System.EventHandler(this.BtnUndo_Click);
            // 
            // _btnRedo
            // 
            this._btnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnRedo.Name = "_btnRedo";
            this._btnRedo.Size = new System.Drawing.Size(53, 31);
            this._btnRedo.Text = "↷ Ripeti";
            this._btnRedo.ToolTipText = "Ripeti modifica mappa";
            this._btnRedo.Click += new System.EventHandler(this.BtnRedo_Click);
            // 
            // _sepTools2
            // 
            this._sepTools2.Name = "_sepTools2";
            this._sepTools2.Size = new System.Drawing.Size(6, 34);
            // 
            // _btnGrid
            // 
            this._btnGrid.Checked = true;
            this._btnGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this._btnGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnGrid.Name = "_btnGrid";
            this._btnGrid.Size = new System.Drawing.Size(67, 31);
            this._btnGrid.Text = "Griglia Tile";
            this._btnGrid.Click += new System.EventHandler(this.BtnGrid_Click);
            // 
            // _sepTools3
            // 
            this._sepTools3.Name = "_sepTools3";
            this._sepTools3.Size = new System.Drawing.Size(6, 34);
            // 
            // _btnZoomOut
            // 
            this._btnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnZoomOut.Name = "_btnZoomOut";
            this._btnZoomOut.Size = new System.Drawing.Size(53, 31);
            this._btnZoomOut.Text = "Zoom -";
            this._btnZoomOut.Click += new System.EventHandler(this.BtnZoomOut_Click);
            // 
            // _lblZoom
            // 
            this._lblZoom.ForeColor = System.Drawing.Color.LightGray;
            this._lblZoom.Name = "_lblZoom";
            this._lblZoom.Size = new System.Drawing.Size(19, 31);
            this._lblZoom.Text = "2x";
            // 
            // _btnZoomIn
            // 
            this._btnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnZoomIn.Name = "_btnZoomIn";
            this._btnZoomIn.Size = new System.Drawing.Size(56, 31);
            this._btnZoomIn.Text = "Zoom +";
            this._btnZoomIn.Click += new System.EventHandler(this.BtnZoomIn_Click);
            // 
            // _tsMgmt
            // 
            this._tsMgmt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this._tsMgmt.Dock = System.Windows.Forms.DockStyle.Top;
            this._tsMgmt.ForeColor = System.Drawing.Color.White;
            this._tsMgmt.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._tsMgmt.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnGoToTiles,
            this._btnBackToSprites,
            this._sepMgmt1,
            this._lblM,
            this._hostCombo,
            this._btnNewMap,
            this._btnCloneMap,
            this._btnDeleteMap,
            this._btnRenameMap,
            this._btnResizeMap,
            this._sepMgmt2,
            this._btnExport,
            this._btnPng});
            this._tsMgmt.Location = new System.Drawing.Point(0, 0);
            this._tsMgmt.Name = "_tsMgmt";
            this._tsMgmt.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this._tsMgmt.Size = new System.Drawing.Size(950, 34);
            this._tsMgmt.TabIndex = 0;
            // 
            // _btnGoToTiles
            // 
            this._btnGoToTiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this._btnGoToTiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnGoToTiles.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._btnGoToTiles.ForeColor = System.Drawing.Color.White;
            this._btnGoToTiles.Name = "_btnGoToTiles";
            this._btnGoToTiles.Size = new System.Drawing.Size(145, 31);
            this._btnGoToTiles.Text = "🧱 Editor Tiles (F3)";
            this._btnGoToTiles.ToolTipText = "Apri l'Editor dedicato dei Tile 8x8 (Tasto rapido: F3)";
            this._btnGoToTiles.Click += new System.EventHandler(this.BtnGoToTiles_Click);
            // 
            // _btnBackToSprites
            // 
            this._btnBackToSprites.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(50)))), ((int)(((byte)(95)))));
            this._btnBackToSprites.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnBackToSprites.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._btnBackToSprites.ForeColor = System.Drawing.Color.White;
            this._btnBackToSprites.Name = "_btnBackToSprites";
            this._btnBackToSprites.Size = new System.Drawing.Size(120, 31);
            this._btnBackToSprites.Text = "👾 Sprite (F2)";
            this._btnBackToSprites.ToolTipText = "Torna all'Editor di Disegno Sprite e Nemici (Tasto rapido: F2)";
            this._btnBackToSprites.Click += new System.EventHandler(this.BtnBackToSprites_Click);
            // 
            // _sepMgmt1
            // 
            this._sepMgmt1.Name = "_sepMgmt1";
            this._sepMgmt1.Size = new System.Drawing.Size(6, 34);
            // 
            // _lblM
            // 
            this._lblM.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._lblM.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this._lblM.Name = "_lblM";
            this._lblM.Size = new System.Drawing.Size(65, 31);
            this._lblM.Text = "🗺️ Mappa: ";
            // 
            // _cmbMaps
            // 
            this._cmbMaps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this._cmbMaps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbMaps.ForeColor = System.Drawing.Color.White;
            this._cmbMaps.Name = "_cmbMaps";
            this._cmbMaps.Size = new System.Drawing.Size(180, 23);
            this._cmbMaps.SelectedIndexChanged += new System.EventHandler(this.CmbMaps_SelectedIndexChanged);
            // 
            // _hostCombo
            // 
            this._hostCombo.Name = "_hostCombo";
            this._hostCombo.Size = new System.Drawing.Size(184, 31);
            // 
            // _btnNewMap
            // 
            this._btnNewMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this._btnNewMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnNewMap.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._btnNewMap.ForeColor = System.Drawing.Color.White;
            this._btnNewMap.Name = "_btnNewMap";
            this._btnNewMap.Size = new System.Drawing.Size(111, 31);
            this._btnNewMap.Text = "➕ Nuova Mappa...";
            this._btnNewMap.Click += new System.EventHandler(this.BtnNewMap_Click);
            // 
            // _btnCloneMap
            // 
            this._btnCloneMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnCloneMap.Name = "_btnCloneMap";
            this._btnCloneMap.Size = new System.Drawing.Size(67, 31);
            this._btnCloneMap.Text = "📋 Duplica";
            this._btnCloneMap.ToolTipText = "Duplica la mappa corrente";
            this._btnCloneMap.Click += new System.EventHandler(this.BtnCloneMap_Click);
            // 
            // _btnDeleteMap
            // 
            this._btnDeleteMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnDeleteMap.Name = "_btnDeleteMap";
            this._btnDeleteMap.Size = new System.Drawing.Size(64, 31);
            this._btnDeleteMap.Text = "🗑️ Elimina";
            this._btnDeleteMap.ToolTipText = "Elimina la mappa corrente";
            this._btnDeleteMap.Click += new System.EventHandler(this.BtnDeleteMap_Click);
            // 
            // _btnRenameMap
            // 
            this._btnRenameMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnRenameMap.Name = "_btnRenameMap";
            this._btnRenameMap.Size = new System.Drawing.Size(76, 31);
            this._btnRenameMap.Text = "✏️ Rinomina";
            this._btnRenameMap.ToolTipText = "Rinomina la mappa corrente";
            this._btnRenameMap.Click += new System.EventHandler(this.BtnRenameMap_Click);
            // 
            // _btnResizeMap
            // 
            this._btnResizeMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnResizeMap.Name = "_btnResizeMap";
            this._btnResizeMap.Size = new System.Drawing.Size(92, 31);
            this._btnResizeMap.Text = "📐 Dimensioni...";
            this._btnResizeMap.ToolTipText = "Modifica dimensioni mappa in celle tile";
            this._btnResizeMap.Click += new System.EventHandler(this.BtnResizeMap_Click);
            // 
            // _sepMgmt2
            // 
            this._sepMgmt2.Name = "_sepMgmt2";
            this._sepMgmt2.Size = new System.Drawing.Size(6, 34);
            // 
            // _btnExport
            // 
            this._btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(130)))), ((int)(((byte)(80)))));
            this._btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnExport.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._btnExport.ForeColor = System.Drawing.Color.White;
            this._btnExport.Name = "_btnExport";
            this._btnExport.Size = new System.Drawing.Size(147, 31);
            this._btnExport.Text = "⚡ Esporta Dati Mappa...";
            this._btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // _btnPng
            // 
            this._btnPng.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnPng.Name = "_btnPng";
            this._btnPng.Size = new System.Drawing.Size(79, 31);
            this._btnPng.Text = "📷 Salva PNG...";
            this._btnPng.Click += new System.EventHandler(this.BtnPng_Click);
            // 
            // _rightPanel
            // 
            this._rightPanel.AutoScroll = true;
            this._rightPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(31)))));
            this._rightPanel.Controls.Add(this._btnCopyAsm);
            this._rightPanel.Controls.Add(this._txtQuickMapAsm);
            this._rightPanel.Controls.Add(this._lblAsmTitle);
            this._rightPanel.Controls.Add(this._lblHoverCoords);
            this._rightPanel.Controls.Add(this._lblMemory);
            this._rightPanel.Controls.Add(this._lblPixels);
            this._rightPanel.Controls.Add(this._lblDimensions);
            this._rightPanel.Controls.Add(this._lblSpecTitle);
            this._rightPanel.Controls.Add(this._pnlTileset);
            this._rightPanel.Controls.Add(this._btnAddTile);
            this._rightPanel.Controls.Add(this._btnGoToTileEditor);
            this._rightPanel.Controls.Add(this._lblPalTitle);
            this._rightPanel.Controls.Add(this._previewBox);
            this._rightPanel.Controls.Add(this._lblTileTitle);
            this._rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this._rightPanel.Location = new System.Drawing.Point(660, 68);
            this._rightPanel.Name = "_rightPanel";
            this._rightPanel.Padding = new System.Windows.Forms.Padding(10);
            this._rightPanel.Size = new System.Drawing.Size(290, 582);
            this._rightPanel.TabIndex = 1;
            // 
            // _btnCopyAsm
            // 
            this._btnCopyAsm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(70)))), ((int)(((byte)(95)))));
            this._btnCopyAsm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnCopyAsm.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._btnCopyAsm.ForeColor = System.Drawing.Color.White;
            this._btnCopyAsm.Location = new System.Drawing.Point(10, 567);
            this._btnCopyAsm.Name = "_btnCopyAsm";
            this._btnCopyAsm.Size = new System.Drawing.Size(260, 28);
            this._btnCopyAsm.TabIndex = 11;
            this._btnCopyAsm.Text = "📋 Copia Dati Assembly";
            this._btnCopyAsm.UseVisualStyleBackColor = false;
            this._btnCopyAsm.Click += new System.EventHandler(this.BtnCopyAsm_Click);
            // 
            // _txtQuickMapAsm
            // 
            this._txtQuickMapAsm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(20)))));
            this._txtQuickMapAsm.Font = new System.Drawing.Font("Consolas", 8.2F);
            this._txtQuickMapAsm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(180)))));
            this._txtQuickMapAsm.Location = new System.Drawing.Point(10, 439);
            this._txtQuickMapAsm.Multiline = true;
            this._txtQuickMapAsm.Name = "_txtQuickMapAsm";
            this._txtQuickMapAsm.ReadOnly = true;
            this._txtQuickMapAsm.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._txtQuickMapAsm.Size = new System.Drawing.Size(260, 120);
            this._txtQuickMapAsm.TabIndex = 10;
            // 
            // _lblAsmTitle
            // 
            this._lblAsmTitle.AutoSize = true;
            this._lblAsmTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._lblAsmTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this._lblAsmTitle.Location = new System.Drawing.Point(10, 417);
            this._lblAsmTitle.Name = "_lblAsmTitle";
            this._lblAsmTitle.Size = new System.Drawing.Size(166, 15);
            this._lblAsmTitle.TabIndex = 9;
            this._lblAsmTitle.Text = "DATI ASSEMBLY DEFB (MAPPA)";
            // 
            // _lblHoverCoords
            // 
            this._lblHoverCoords.AutoSize = true;
            this._lblHoverCoords.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this._lblHoverCoords.Location = new System.Drawing.Point(10, 391);
            this._lblHoverCoords.Name = "_lblHoverCoords";
            this._lblHoverCoords.Size = new System.Drawing.Size(149, 15);
            this._lblHoverCoords.TabIndex = 8;
            this._lblHoverCoords.Text = "Cella: (0, 0) | Pixel: (0, 0)";
            // 
            // _lblMemory
            // 
            this._lblMemory.AutoSize = true;
            this._lblMemory.ForeColor = System.Drawing.Color.LightGray;
            this._lblMemory.Location = new System.Drawing.Point(10, 372);
            this._lblMemory.Name = "_lblMemory";
            this._lblMemory.Size = new System.Drawing.Size(127, 15);
            this._lblMemory.TabIndex = 7;
            this._lblMemory.Text = "Memoria Z80: 768 byte";
            // 
            // _lblPixels
            // 
            this._lblPixels.AutoSize = true;
            this._lblPixels.ForeColor = System.Drawing.Color.LightGray;
            this._lblPixels.Location = new System.Drawing.Point(10, 353);
            this._lblPixels.Name = "_lblPixels";
            this._lblPixels.Size = new System.Drawing.Size(169, 15);
            this._lblPixels.TabIndex = 6;
            this._lblPixels.Text = "Pixel: 256x192 px (Schermo ZX)";
            // 
            // _lblDimensions
            // 
            this._lblDimensions.AutoSize = true;
            this._lblDimensions.ForeColor = System.Drawing.Color.LightGray;
            this._lblDimensions.Location = new System.Drawing.Point(10, 334);
            this._lblDimensions.Name = "_lblDimensions";
            this._lblDimensions.Size = new System.Drawing.Size(127, 15);
            this._lblDimensions.TabIndex = 5;
            this._lblDimensions.Text = "Dimensioni: 32x24 celle (8x8)";
            // 
            // _lblSpecTitle
            // 
            this._lblSpecTitle.AutoSize = true;
            this._lblSpecTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._lblSpecTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this._lblSpecTitle.Location = new System.Drawing.Point(10, 312);
            this._lblSpecTitle.Name = "_lblSpecTitle";
            this._lblSpecTitle.Size = new System.Drawing.Size(117, 15);
            this._lblSpecTitle.TabIndex = 4;
            this._lblSpecTitle.Text = "SPECIFICHE MAPPA";
            // 
            // _pnlTileset
            // 
            this._pnlTileset.AutoScroll = true;
            this._pnlTileset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(22)))));
            this._pnlTileset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pnlTileset.Location = new System.Drawing.Point(10, 154);
            this._pnlTileset.Name = "_pnlTileset";
            this._pnlTileset.Size = new System.Drawing.Size(260, 148);
            this._pnlTileset.TabIndex = 3;
            // 
            // _btnAddTile
            // 
            this._btnAddTile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this._btnAddTile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnAddTile.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._btnAddTile.ForeColor = System.Drawing.Color.White;
            this._btnAddTile.Location = new System.Drawing.Point(10, 122);
            this._btnAddTile.Name = "_btnAddTile";
            this._btnAddTile.Size = new System.Drawing.Size(126, 26);
            this._btnAddTile.TabIndex = 12;
            this._btnAddTile.Text = "➕ Agg. Tile...";
            this._btnAddTile.UseVisualStyleBackColor = false;
            this._btnAddTile.Click += new System.EventHandler(this.BtnAddTile_Click);
            // 
            // _btnGoToTileEditor
            // 
            this._btnGoToTileEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(75)))), ((int)(((byte)(115)))));
            this._btnGoToTileEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnGoToTileEditor.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._btnGoToTileEditor.ForeColor = System.Drawing.Color.White;
            this._btnGoToTileEditor.Location = new System.Drawing.Point(140, 122);
            this._btnGoToTileEditor.Name = "_btnGoToTileEditor";
            this._btnGoToTileEditor.Size = new System.Drawing.Size(130, 26);
            this._btnGoToTileEditor.TabIndex = 13;
            this._btnGoToTileEditor.Text = "🧱 Editor (F3)";
            this._btnGoToTileEditor.UseVisualStyleBackColor = false;
            this._btnGoToTileEditor.Click += new System.EventHandler(this.BtnGoToTiles_Click);
            // 
            // _lblPalTitle
            // 
            this._lblPalTitle.AutoSize = true;
            this._lblPalTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._lblPalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this._lblPalTitle.Location = new System.Drawing.Point(10, 100);
            this._lblPalTitle.Name = "_lblPalTitle";
            this._lblPalTitle.Size = new System.Drawing.Size(150, 15);
            this._lblPalTitle.TabIndex = 2;
            this._lblPalTitle.Text = "PALETTE TILESET (8x8)";
            // 
            // _previewBox
            // 
            this._previewBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this._previewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._previewBox.Controls.Add(this._lblActiveTileName);
            this._previewBox.Controls.Add(this._picActiveTile);
            this._previewBox.Location = new System.Drawing.Point(10, 32);
            this._previewBox.Name = "_previewBox";
            this._previewBox.Size = new System.Drawing.Size(260, 60);
            this._previewBox.TabIndex = 1;
            // 
            // _lblActiveTileName
            // 
            this._lblActiveTileName.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._lblActiveTileName.ForeColor = System.Drawing.Color.Yellow;
            this._lblActiveTileName.Location = new System.Drawing.Point(65, 16);
            this._lblActiveTileName.Name = "_lblActiveTileName";
            this._lblActiveTileName.Size = new System.Drawing.Size(185, 30);
            this._lblActiveTileName.TabIndex = 1;
            this._lblActiveTileName.Text = "Tile #1: Nemico_Pos1_Nord";
            // 
            // _picActiveTile
            // 
            this._picActiveTile.BackColor = System.Drawing.Color.Black;
            this._picActiveTile.Location = new System.Drawing.Point(8, 6);
            this._picActiveTile.Name = "_picActiveTile";
            this._picActiveTile.Size = new System.Drawing.Size(48, 48);
            this._picActiveTile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._picActiveTile.TabIndex = 0;
            this._picActiveTile.TabStop = false;
            // 
            // _lblTileTitle
            // 
            this._lblTileTitle.AutoSize = true;
            this._lblTileTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._lblTileTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this._lblTileTitle.Location = new System.Drawing.Point(10, 8);
            this._lblTileTitle.Name = "_lblTileTitle";
            this._lblTileTitle.Size = new System.Drawing.Size(76, 15);
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
            this._centerPanel.Size = new System.Drawing.Size(660, 582);
            this._centerPanel.TabIndex = 2;
            this._centerPanel.Resize += new System.EventHandler(this.CenterPanel_Resize);
            // 
            // _canvas
            // 
            this._canvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(18)))));
            this._canvas.CurrentTool = ZXSpectrumSpriteGenerator.UI.Controls.MapDrawingTool.Pencil;
            this._canvas.Location = new System.Drawing.Point(10, 10);
            this._canvas.Name = "_canvas";
            this._canvas.ScaleFactor = 2;
            this._canvas.ShowGrid = true;
            this._canvas.Size = new System.Drawing.Size(512, 384);
            this._canvas.TabIndex = 0;
            this._canvas.Text = "mapCanvas";
            this._canvas.HoverCoordinatesChanged += new System.EventHandler<ZXSpectrumSpriteGenerator.UI.Controls.MapHoverEventArgs>(this.Canvas_HoverCoordinatesChanged);
            this._canvas.MapChanged += new System.EventHandler(this.Canvas_MapChanged);
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
            this.Size = new System.Drawing.Size(950, 650);
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
        private System.Windows.Forms.ComboBox _cmbMaps;
        private System.Windows.Forms.ToolStripControlHost _hostCombo;
        private System.Windows.Forms.ToolStripButton _btnNewMap;
        private System.Windows.Forms.ToolStripButton _btnCloneMap;
        private System.Windows.Forms.ToolStripButton _btnDeleteMap;
        private System.Windows.Forms.ToolStripButton _btnRenameMap;
        private System.Windows.Forms.ToolStripButton _btnResizeMap;
        private System.Windows.Forms.ToolStripSeparator _sepMgmt2;
        private System.Windows.Forms.ToolStripButton _btnExport;
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
        private System.Windows.Forms.Button _btnAddTile;
        private System.Windows.Forms.Button _btnGoToTileEditor;
        private System.Windows.Forms.Panel _pnlTileset;
        private System.Windows.Forms.Label _lblSpecTitle;
        private System.Windows.Forms.Label _lblDimensions;
        private System.Windows.Forms.Label _lblPixels;
        private System.Windows.Forms.Label _lblMemory;
        private System.Windows.Forms.Label _lblHoverCoords;
        private System.Windows.Forms.Label _lblAsmTitle;
        private System.Windows.Forms.TextBox _txtQuickMapAsm;
        private System.Windows.Forms.Button _btnCopyAsm;
        private System.Windows.Forms.Panel _centerPanel;
        private ZXSpectrumSpriteGenerator.UI.Controls.MapCanvas _canvas;
    }
}
