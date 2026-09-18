namespace ZXSpectrumSpriteGenerator.UI
{
    partial class TileEditorView
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this._topBar = new System.Windows.Forms.Panel();
            this._tsTools = new System.Windows.Forms.ToolStrip();
            this._btnBackToMap = new System.Windows.Forms.ToolStripButton();
            this._btnBackToSprites = new System.Windows.Forms.ToolStripButton();
            this._sep1 = new System.Windows.Forms.ToolStripSeparator();
            this._lblTools = new System.Windows.Forms.ToolStripLabel();
            this._btnToolPencil = new System.Windows.Forms.ToolStripButton();
            this._btnToolEraser = new System.Windows.Forms.ToolStripButton();
            this._btnToolLine = new System.Windows.Forms.ToolStripButton();
            this._btnToolRect = new System.Windows.Forms.ToolStripButton();
            this._btnToolRectFill = new System.Windows.Forms.ToolStripButton();
            this._btnToolCircle = new System.Windows.Forms.ToolStripButton();
            this._btnToolCircleFill = new System.Windows.Forms.ToolStripButton();
            this._btnToolFill = new System.Windows.Forms.ToolStripButton();
            this._btnToolPicker = new System.Windows.Forms.ToolStripButton();
            this._sep2 = new System.Windows.Forms.ToolStripSeparator();
            this._btnUndo = new System.Windows.Forms.ToolStripButton();
            this._btnRedo = new System.Windows.Forms.ToolStripButton();
            this._sep3 = new System.Windows.Forms.ToolStripSeparator();
            this._btnFlipH = new System.Windows.Forms.ToolStripButton();
            this._btnFlipV = new System.Windows.Forms.ToolStripButton();
            this._btnRotate = new System.Windows.Forms.ToolStripButton();
            this._btnInvert = new System.Windows.Forms.ToolStripButton();
            this._btnClear = new System.Windows.Forms.ToolStripButton();
            this._sep4 = new System.Windows.Forms.ToolStripSeparator();
            this._btnGrid = new System.Windows.Forms.ToolStripButton();
            this._btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this._lblZoom = new System.Windows.Forms.ToolStripLabel();
            this._btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this._bottomPanel = new System.Windows.Forms.Panel();
            this._filmstrip = new ZXSpectrumSpriteGenerator.UI.Controls.SpriteFilmstrip();
            this._filmstripBar = new System.Windows.Forms.ToolStrip();
            this._lblTilesetTitle = new System.Windows.Forms.ToolStripLabel();
            this._btnAddTile = new System.Windows.Forms.ToolStripButton();
            this._btnImportTiles = new System.Windows.Forms.ToolStripButton();
            this._btnPasteClipboard = new System.Windows.Forms.ToolStripButton();
            this._sepFs1 = new System.Windows.Forms.ToolStripSeparator();
            this._btnDuplicateTile = new System.Windows.Forms.ToolStripButton();
            this._btnDeleteTile = new System.Windows.Forms.ToolStripButton();
            this._btnMoveLeft = new System.Windows.Forms.ToolStripButton();
            this._btnMoveRight = new System.Windows.Forms.ToolStripButton();
            this._rightPanel = new System.Windows.Forms.Panel();
            this._btnGoToMap = new System.Windows.Forms.Button();
            this._btnCopyAsm = new System.Windows.Forms.Button();
            this._txtQuickAsm = new System.Windows.Forms.TextBox();
            this._lblAsmTitle = new System.Windows.Forms.Label();
            this._pnlPatternPreview = new System.Windows.Forms.Panel();
            this._lblPatternTitle = new System.Windows.Forms.Label();
            this._pnlPreviewBox = new System.Windows.Forms.Panel();
            this._lblInfo = new System.Windows.Forms.Label();
            this._txtName = new System.Windows.Forms.TextBox();
            this._lblName = new System.Windows.Forms.Label();
            this._lblTilePropTitle = new System.Windows.Forms.Label();
            this._centerContainer = new System.Windows.Forms.Panel();
            this._centerPanel = new System.Windows.Forms.Panel();
            this._lblCoords = new System.Windows.Forms.Label();
            this._canvas = new ZXSpectrumSpriteGenerator.UI.Controls.SpriteCanvas();
            this._leftColorBar = new System.Windows.Forms.Panel();
            this._chkFlash = new System.Windows.Forms.CheckBox();
            this._chkBright = new System.Windows.Forms.CheckBox();
            this._pnlPaperColors = new System.Windows.Forms.FlowLayoutPanel();
            this._lblPaperTitle = new System.Windows.Forms.Label();
            this._pnlInkColors = new System.Windows.Forms.FlowLayoutPanel();
            this._lblInkTitle = new System.Windows.Forms.Label();
            this._topBar.SuspendLayout();
            this._tsTools.SuspendLayout();
            this._bottomPanel.SuspendLayout();
            this._filmstripBar.SuspendLayout();
            this._rightPanel.SuspendLayout();
            this._centerContainer.SuspendLayout();
            this._centerPanel.SuspendLayout();
            this._leftColorBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _topBar
            // 
            this._topBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(34)))));
            this._topBar.Controls.Add(this._tsTools);
            this._topBar.Dock = System.Windows.Forms.DockStyle.Top;
            this._topBar.Location = new System.Drawing.Point(0, 0);
            this._topBar.Name = "_topBar";
            this._topBar.Size = new System.Drawing.Size(1000, 36);
            this._topBar.TabIndex = 0;
            // 
            // _tsTools
            // 
            this._tsTools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(28)))));
            this._tsTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tsTools.ForeColor = System.Drawing.Color.White;
            this._tsTools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._tsTools.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._tsTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnBackToMap,
            this._btnBackToSprites,
            this._sep1,
            this._lblTools,
            this._btnToolPencil,
            this._btnToolEraser,
            this._btnToolLine,
            this._btnToolRect,
            this._btnToolRectFill,
            this._btnToolCircle,
            this._btnToolCircleFill,
            this._btnToolFill,
            this._btnToolPicker,
            this._sep2,
            this._btnUndo,
            this._btnRedo,
            this._sep3,
            this._btnFlipH,
            this._btnFlipV,
            this._btnRotate,
            this._btnInvert,
            this._btnClear,
            this._sep4,
            this._btnGrid,
            this._btnZoomOut,
            this._lblZoom,
            this._btnZoomIn});
            this._tsTools.Location = new System.Drawing.Point(0, 0);
            this._tsTools.Name = "_tsTools";
            this._tsTools.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this._tsTools.Size = new System.Drawing.Size(1000, 36);
            this._tsTools.TabIndex = 0;
            // 
            // _btnBackToMap
            // 
            this._btnBackToMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(100)))), ((int)(((byte)(70)))));
            this._btnBackToMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnBackToMap.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._btnBackToMap.ForeColor = System.Drawing.Color.White;
            this._btnBackToMap.Name = "_btnBackToMap";
            this._btnBackToMap.Size = new System.Drawing.Size(217, 31);
            this._btnBackToMap.Text = "🗺️ Torna alla Mappa (F4)";
            this._btnBackToMap.Click += new System.EventHandler(this.BtnBackToMap_Click);
            // 
            // _btnBackToSprites
            // 
            this._btnBackToSprites.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(50)))), ((int)(((byte)(95)))));
            this._btnBackToSprites.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnBackToSprites.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._btnBackToSprites.ForeColor = System.Drawing.Color.White;
            this._btnBackToSprites.Name = "_btnBackToSprites";
            this._btnBackToSprites.Size = new System.Drawing.Size(212, 31);
            this._btnBackToSprites.Text = "👾 Torna agli Sprite (F2)";
            this._btnBackToSprites.Click += new System.EventHandler(this.BtnBackToSprites_Click);
            // 
            // _sep1
            // 
            this._sep1.Name = "_sep1";
            this._sep1.Size = new System.Drawing.Size(6, 36);
            // 
            // _lblTools
            // 
            this._lblTools.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._lblTools.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this._lblTools.Name = "_lblTools";
            this._lblTools.Size = new System.Drawing.Size(85, 31);
            this._lblTools.Text = "Disegno: ";
            // 
            // _btnToolPencil
            // 
            this._btnToolPencil.Checked = true;
            this._btnToolPencil.CheckState = System.Windows.Forms.CheckState.Checked;
            this._btnToolPencil.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolPencil.Name = "_btnToolPencil";
            this._btnToolPencil.Size = new System.Drawing.Size(96, 31);
            this._btnToolPencil.Text = "✏️ Matita";
            this._btnToolPencil.Click += new System.EventHandler(this.BtnToolPencil_Click);
            // 
            // _btnToolEraser
            // 
            this._btnToolEraser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolEraser.Name = "_btnToolEraser";
            this._btnToolEraser.Size = new System.Drawing.Size(110, 31);
            this._btnToolEraser.Text = "🧹 Gomma";
            this._btnToolEraser.Click += new System.EventHandler(this.BtnToolEraser_Click);
            // 
            // _btnToolLine
            // 
            this._btnToolLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolLine.Name = "_btnToolLine";
            this._btnToolLine.Size = new System.Drawing.Size(86, 31);
            this._btnToolLine.Text = "📏 Linea";
            this._btnToolLine.Click += new System.EventHandler(this.BtnToolLine_Click);
            // 
            // _btnToolRect
            // 
            this._btnToolRect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolRect.Name = "_btnToolRect";
            this._btnToolRect.Size = new System.Drawing.Size(72, 31);
            this._btnToolRect.Text = "⬜ Rett.";
            this._btnToolRect.Click += new System.EventHandler(this.BtnToolRect_Click);
            // 
            // _btnToolRectFill
            // 
            this._btnToolRectFill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolRectFill.Name = "_btnToolRectFill";
            this._btnToolRectFill.Size = new System.Drawing.Size(105, 29);
            this._btnToolRectFill.Text = "⬛ R.Pieno";
            this._btnToolRectFill.Click += new System.EventHandler(this.BtnToolRectFill_Click);
            // 
            // _btnToolCircle
            // 
            this._btnToolCircle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolCircle.Name = "_btnToolCircle";
            this._btnToolCircle.Size = new System.Drawing.Size(105, 29);
            this._btnToolCircle.Text = "⚪ Cerchio";
            this._btnToolCircle.Click += new System.EventHandler(this.BtnToolCircle_Click);
            // 
            // _btnToolCircleFill
            // 
            this._btnToolCircleFill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolCircleFill.Name = "_btnToolCircleFill";
            this._btnToolCircleFill.Size = new System.Drawing.Size(105, 29);
            this._btnToolCircleFill.Text = "⚫ C.Pieno";
            this._btnToolCircleFill.Click += new System.EventHandler(this.BtnToolCircleFill_Click);
            // 
            // _btnToolFill
            // 
            this._btnToolFill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolFill.Name = "_btnToolFill";
            this._btnToolFill.Size = new System.Drawing.Size(123, 29);
            this._btnToolFill.Text = "🪣 Secchiello";
            this._btnToolFill.Click += new System.EventHandler(this.BtnToolFill_Click);
            // 
            // _btnToolPicker
            // 
            this._btnToolPicker.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolPicker.Name = "_btnToolPicker";
            this._btnToolPicker.Size = new System.Drawing.Size(101, 29);
            this._btnToolPicker.Text = "💧 Pipetta";
            this._btnToolPicker.Click += new System.EventHandler(this.BtnToolPicker_Click);
            // 
            // _sep2
            // 
            this._sep2.Name = "_sep2";
            this._sep2.Size = new System.Drawing.Size(6, 36);
            // 
            // _btnUndo
            // 
            this._btnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnUndo.Name = "_btnUndo";
            this._btnUndo.Size = new System.Drawing.Size(95, 29);
            this._btnUndo.Text = "↶ Annulla";
            this._btnUndo.Click += new System.EventHandler(this.BtnUndo_Click);
            // 
            // _btnRedo
            // 
            this._btnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnRedo.Name = "_btnRedo";
            this._btnRedo.Size = new System.Drawing.Size(81, 29);
            this._btnRedo.Text = "↷ Ripeti";
            this._btnRedo.Click += new System.EventHandler(this.BtnRedo_Click);
            // 
            // _sep3
            // 
            this._sep3.Name = "_sep3";
            this._sep3.Size = new System.Drawing.Size(6, 36);
            // 
            // _btnFlipH
            // 
            this._btnFlipH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnFlipH.Name = "_btnFlipH";
            this._btnFlipH.Size = new System.Drawing.Size(65, 29);
            this._btnFlipH.Text = "↔ Flip";
            this._btnFlipH.ToolTipText = "Specchia Orizzontalmente";
            this._btnFlipH.Click += new System.EventHandler(this.BtnFlipH_Click);
            // 
            // _btnFlipV
            // 
            this._btnFlipV.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnFlipV.Name = "_btnFlipV";
            this._btnFlipV.Size = new System.Drawing.Size(58, 29);
            this._btnFlipV.Text = "↕ Flip";
            this._btnFlipV.ToolTipText = "Specchia Verticalmente";
            this._btnFlipV.Click += new System.EventHandler(this.BtnFlipV_Click);
            // 
            // _btnRotate
            // 
            this._btnRotate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnRotate.Name = "_btnRotate";
            this._btnRotate.Size = new System.Drawing.Size(63, 29);
            this._btnRotate.Text = "↻ 90°";
            this._btnRotate.ToolTipText = "Ruota di 90 gradi orario";
            this._btnRotate.Click += new System.EventHandler(this.BtnRotate_Click);
            // 
            // _btnInvert
            // 
            this._btnInvert.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnInvert.Name = "_btnInvert";
            this._btnInvert.Size = new System.Drawing.Size(95, 29);
            this._btnInvert.Text = "🌓 Inverti";
            this._btnInvert.Click += new System.EventHandler(this.BtnInvert_Click);
            // 
            // _btnClear
            // 
            this._btnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnClear.Name = "_btnClear";
            this._btnClear.Size = new System.Drawing.Size(64, 29);
            this._btnClear.Text = "Pulisci";
            this._btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // _sep4
            // 
            this._sep4.Name = "_sep4";
            this._sep4.Size = new System.Drawing.Size(6, 36);
            // 
            // _btnGrid
            // 
            this._btnGrid.Checked = true;
            this._btnGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this._btnGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnGrid.Name = "_btnGrid";
            this._btnGrid.Size = new System.Drawing.Size(66, 29);
            this._btnGrid.Text = "Griglia";
            this._btnGrid.Click += new System.EventHandler(this.BtnGrid_Click);
            // 
            // _btnZoomOut
            // 
            this._btnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnZoomOut.Name = "_btnZoomOut";
            this._btnZoomOut.Size = new System.Drawing.Size(34, 29);
            this._btnZoomOut.Text = "-";
            this._btnZoomOut.Click += new System.EventHandler(this.BtnZoomOut_Click);
            // 
            // _lblZoom
            // 
            this._lblZoom.ForeColor = System.Drawing.Color.LightGray;
            this._lblZoom.Name = "_lblZoom";
            this._lblZoom.Size = new System.Drawing.Size(40, 25);
            this._lblZoom.Text = "32x";
            // 
            // _btnZoomIn
            // 
            this._btnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnZoomIn.Name = "_btnZoomIn";
            this._btnZoomIn.Size = new System.Drawing.Size(34, 29);
            this._btnZoomIn.Text = "+";
            this._btnZoomIn.Click += new System.EventHandler(this.BtnZoomIn_Click);
            // 
            // _bottomPanel
            // 
            this._bottomPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(26)))));
            this._bottomPanel.Controls.Add(this._filmstrip);
            this._bottomPanel.Controls.Add(this._filmstripBar);
            this._bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._bottomPanel.Location = new System.Drawing.Point(0, 520);
            this._bottomPanel.Name = "_bottomPanel";
            this._bottomPanel.Size = new System.Drawing.Size(1000, 130);
            this._bottomPanel.TabIndex = 1;
            // 
            // _filmstrip
            // 
            this._filmstrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(24)))));
            this._filmstrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filmstrip.IsTilesMode = true;
            this._filmstrip.Location = new System.Drawing.Point(0, 34);
            this._filmstrip.Name = "_filmstrip";
            this._filmstrip.ScrollOffsetX = 0;
            this._filmstrip.Size = new System.Drawing.Size(1000, 96);
            this._filmstrip.TabIndex = 1;
            this._filmstrip.SelectedIndexChanged += new System.EventHandler(this.Filmstrip_SelectedIndexChanged);
            // 
            // _filmstripBar
            // 
            this._filmstripBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(34)))));
            this._filmstripBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._filmstripBar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._filmstripBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._lblTilesetTitle,
            this._btnAddTile,
            this._btnImportTiles,
            this._btnPasteClipboard,
            this._sepFs1,
            this._btnDuplicateTile,
            this._btnDeleteTile,
            this._btnMoveLeft,
            this._btnMoveRight});
            this._filmstripBar.Location = new System.Drawing.Point(0, 0);
            this._filmstripBar.Name = "_filmstripBar";
            this._filmstripBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this._filmstripBar.Size = new System.Drawing.Size(1000, 34);
            this._filmstripBar.TabIndex = 0;
            // 
            // _lblTilesetTitle
            // 
            this._lblTilesetTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._lblTilesetTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this._lblTilesetTitle.Name = "_lblTilesetTitle";
            this._lblTilesetTitle.Size = new System.Drawing.Size(169, 29);
            this._lblTilesetTitle.Text = "🧱 TILESET (8x8): ";
            // 
            // _btnAddTile
            // 
            this._btnAddTile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(60)))));
            this._btnAddTile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnAddTile.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._btnAddTile.ForeColor = System.Drawing.Color.White;
            this._btnAddTile.Name = "_btnAddTile";
            this._btnAddTile.Size = new System.Drawing.Size(99, 29);
            this._btnAddTile.Text = "➕ +1 Tile";
            this._btnAddTile.Click += new System.EventHandler(this.BtnAddTile_Click);
            // 
            // _btnImportTiles
            // 
            this._btnImportTiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this._btnImportTiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnImportTiles.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._btnImportTiles.ForeColor = System.Drawing.Color.White;
            this._btnImportTiles.Name = "_btnImportTiles";
            this._btnImportTiles.Size = new System.Drawing.Size(203, 29);
            this._btnImportTiles.Text = "📚 Libreria / Importa...";
            this._btnImportTiles.Click += new System.EventHandler(this.BtnImportTiles_Click);
            // 
            // _btnPasteClipboard
            // 
            this._btnPasteClipboard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnPasteClipboard.ForeColor = System.Drawing.Color.White;
            this._btnPasteClipboard.Name = "_btnPasteClipboard";
            this._btnPasteClipboard.Size = new System.Drawing.Size(191, 29);
            this._btnPasteClipboard.Text = "📋 Incolla da Appunti";
            this._btnPasteClipboard.Click += new System.EventHandler(this.BtnPasteClipboard_Click);
            // 
            // _sepFs1
            // 
            this._sepFs1.Name = "_sepFs1";
            this._sepFs1.Size = new System.Drawing.Size(6, 34);
            // 
            // _btnDuplicateTile
            // 
            this._btnDuplicateTile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnDuplicateTile.ForeColor = System.Drawing.Color.White;
            this._btnDuplicateTile.Name = "_btnDuplicateTile";
            this._btnDuplicateTile.Size = new System.Drawing.Size(75, 29);
            this._btnDuplicateTile.Text = "Duplica";
            this._btnDuplicateTile.Click += new System.EventHandler(this.BtnDuplicateTile_Click);
            // 
            // _btnDeleteTile
            // 
            this._btnDeleteTile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnDeleteTile.ForeColor = System.Drawing.Color.White;
            this._btnDeleteTile.Name = "_btnDeleteTile";
            this._btnDeleteTile.Size = new System.Drawing.Size(72, 29);
            this._btnDeleteTile.Text = "Elimina";
            this._btnDeleteTile.Click += new System.EventHandler(this.BtnDeleteTile_Click);
            // 
            // _btnMoveLeft
            // 
            this._btnMoveLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnMoveLeft.ForeColor = System.Drawing.Color.White;
            this._btnMoveLeft.Name = "_btnMoveLeft";
            this._btnMoveLeft.Size = new System.Drawing.Size(34, 29);
            this._btnMoveLeft.Text = "◄";
            this._btnMoveLeft.ToolTipText = "Sposta tile verso sinistra";
            this._btnMoveLeft.Click += new System.EventHandler(this.BtnMoveLeft_Click);
            // 
            // _btnMoveRight
            // 
            this._btnMoveRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnMoveRight.ForeColor = System.Drawing.Color.White;
            this._btnMoveRight.Name = "_btnMoveRight";
            this._btnMoveRight.Size = new System.Drawing.Size(34, 29);
            this._btnMoveRight.Text = "►";
            this._btnMoveRight.ToolTipText = "Sposta tile verso destra";
            this._btnMoveRight.Click += new System.EventHandler(this.BtnMoveRight_Click);
            // 
            // _rightPanel
            // 
            this._rightPanel.AutoScroll = true;
            this._rightPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(31)))));
            this._rightPanel.Controls.Add(this._btnGoToMap);
            this._rightPanel.Controls.Add(this._btnCopyAsm);
            this._rightPanel.Controls.Add(this._txtQuickAsm);
            this._rightPanel.Controls.Add(this._lblAsmTitle);
            this._rightPanel.Controls.Add(this._pnlPatternPreview);
            this._rightPanel.Controls.Add(this._lblPatternTitle);
            this._rightPanel.Controls.Add(this._pnlPreviewBox);
            this._rightPanel.Controls.Add(this._lblInfo);
            this._rightPanel.Controls.Add(this._txtName);
            this._rightPanel.Controls.Add(this._lblName);
            this._rightPanel.Controls.Add(this._lblTilePropTitle);
            this._rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this._rightPanel.Location = new System.Drawing.Point(740, 36);
            this._rightPanel.Name = "_rightPanel";
            this._rightPanel.Padding = new System.Windows.Forms.Padding(12);
            this._rightPanel.Size = new System.Drawing.Size(260, 484);
            this._rightPanel.TabIndex = 2;
            // 
            // _btnGoToMap
            // 
            this._btnGoToMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(120)))), ((int)(((byte)(80)))));
            this._btnGoToMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnGoToMap.Font = new System.Drawing.Font("Segoe UI", 8.8F, System.Drawing.FontStyle.Bold);
            this._btnGoToMap.ForeColor = System.Drawing.Color.White;
            this._btnGoToMap.Location = new System.Drawing.Point(12, 440);
            this._btnGoToMap.Name = "_btnGoToMap";
            this._btnGoToMap.Size = new System.Drawing.Size(236, 32);
            this._btnGoToMap.TabIndex = 10;
            this._btnGoToMap.Text = "🗺️ Usa nella Mappa (F4)";
            this._btnGoToMap.UseVisualStyleBackColor = false;
            this._btnGoToMap.Click += new System.EventHandler(this.BtnBackToMap_Click);
            // 
            // _btnCopyAsm
            // 
            this._btnCopyAsm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(70)))), ((int)(((byte)(95)))));
            this._btnCopyAsm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnCopyAsm.Font = new System.Drawing.Font("Segoe UI", 8.2F, System.Drawing.FontStyle.Bold);
            this._btnCopyAsm.ForeColor = System.Drawing.Color.White;
            this._btnCopyAsm.Location = new System.Drawing.Point(12, 400);
            this._btnCopyAsm.Name = "_btnCopyAsm";
            this._btnCopyAsm.Size = new System.Drawing.Size(236, 26);
            this._btnCopyAsm.TabIndex = 9;
            this._btnCopyAsm.Text = "📋 Copia Byte Assembly";
            this._btnCopyAsm.UseVisualStyleBackColor = false;
            this._btnCopyAsm.Click += new System.EventHandler(this.BtnCopyAsm_Click);
            // 
            // _txtQuickAsm
            // 
            this._txtQuickAsm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(20)))));
            this._txtQuickAsm.Font = new System.Drawing.Font("Consolas", 8F);
            this._txtQuickAsm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(180)))));
            this._txtQuickAsm.Location = new System.Drawing.Point(12, 335);
            this._txtQuickAsm.Multiline = true;
            this._txtQuickAsm.Name = "_txtQuickAsm";
            this._txtQuickAsm.ReadOnly = true;
            this._txtQuickAsm.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._txtQuickAsm.Size = new System.Drawing.Size(236, 60);
            this._txtQuickAsm.TabIndex = 8;
            // 
            // _lblAsmTitle
            // 
            this._lblAsmTitle.AutoSize = true;
            this._lblAsmTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._lblAsmTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this._lblAsmTitle.Location = new System.Drawing.Point(12, 315);
            this._lblAsmTitle.Name = "_lblAsmTitle";
            this._lblAsmTitle.Size = new System.Drawing.Size(217, 23);
            this._lblAsmTitle.TabIndex = 7;
            this._lblAsmTitle.Text = "DATI ASSEMBLY (8 BYTE):";
            // 
            // _pnlPatternPreview
            // 
            this._pnlPatternPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(22)))));
            this._pnlPatternPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pnlPatternPreview.Location = new System.Drawing.Point(12, 230);
            this._pnlPatternPreview.Name = "_pnlPatternPreview";
            this._pnlPatternPreview.Size = new System.Drawing.Size(236, 72);
            this._pnlPatternPreview.TabIndex = 6;
            this._pnlPatternPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlPatternPreview_Paint);
            // 
            // _lblPatternTitle
            // 
            this._lblPatternTitle.AutoSize = true;
            this._lblPatternTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._lblPatternTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this._lblPatternTitle.Location = new System.Drawing.Point(12, 210);
            this._lblPatternTitle.Name = "_lblPatternTitle";
            this._lblPatternTitle.Size = new System.Drawing.Size(212, 23);
            this._lblPatternTitle.TabIndex = 5;
            this._lblPatternTitle.Text = "MOTIVO RIPETUTO (4x2):";
            // 
            // _pnlPreviewBox
            // 
            this._pnlPreviewBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(22)))));
            this._pnlPreviewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pnlPreviewBox.Location = new System.Drawing.Point(12, 130);
            this._pnlPreviewBox.Name = "_pnlPreviewBox";
            this._pnlPreviewBox.Size = new System.Drawing.Size(236, 68);
            this._pnlPreviewBox.TabIndex = 4;
            this._pnlPreviewBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlPreviewBox_Paint);
            // 
            // _lblInfo
            // 
            this._lblInfo.ForeColor = System.Drawing.Color.LightGray;
            this._lblInfo.Location = new System.Drawing.Point(12, 85);
            this._lblInfo.Name = "_lblInfo";
            this._lblInfo.Size = new System.Drawing.Size(236, 38);
            this._lblInfo.TabIndex = 3;
            this._lblInfo.Text = "Dimensione: 8x8 px\r\nMemoria video: 8 byte (1 carattere)";
            // 
            // _txtName
            // 
            this._txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(22)))));
            this._txtName.ForeColor = System.Drawing.Color.White;
            this._txtName.Location = new System.Drawing.Point(12, 50);
            this._txtName.Name = "_txtName";
            this._txtName.Size = new System.Drawing.Size(236, 31);
            this._txtName.TabIndex = 2;
            this._txtName.TextChanged += new System.EventHandler(this.TxtName_TextChanged);
            // 
            // _lblName
            // 
            this._lblName.AutoSize = true;
            this._lblName.ForeColor = System.Drawing.Color.LightGray;
            this._lblName.Location = new System.Drawing.Point(12, 32);
            this._lblName.Name = "_lblName";
            this._lblName.Size = new System.Drawing.Size(96, 25);
            this._lblName.TabIndex = 1;
            this._lblName.Text = "Nome Tile:";
            // 
            // _lblTilePropTitle
            // 
            this._lblTilePropTitle.AutoSize = true;
            this._lblTilePropTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._lblTilePropTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this._lblTilePropTitle.Location = new System.Drawing.Point(12, 10);
            this._lblTilePropTitle.Name = "_lblTilePropTitle";
            this._lblTilePropTitle.Size = new System.Drawing.Size(201, 25);
            this._lblTilePropTitle.TabIndex = 0;
            this._lblTilePropTitle.Text = "PROPRIETÀ TILE (8x8)";
            // 
            // _centerContainer
            // 
            this._centerContainer.Controls.Add(this._centerPanel);
            this._centerContainer.Controls.Add(this._leftColorBar);
            this._centerContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._centerContainer.Location = new System.Drawing.Point(0, 36);
            this._centerContainer.Name = "_centerContainer";
            this._centerContainer.Size = new System.Drawing.Size(740, 484);
            this._centerContainer.TabIndex = 3;
            // 
            // _centerPanel
            // 
            this._centerPanel.AutoScroll = true;
            this._centerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(20)))));
            this._centerPanel.Controls.Add(this._lblCoords);
            this._centerPanel.Controls.Add(this._canvas);
            this._centerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._centerPanel.Location = new System.Drawing.Point(130, 0);
            this._centerPanel.Name = "_centerPanel";
            this._centerPanel.Size = new System.Drawing.Size(610, 484);
            this._centerPanel.TabIndex = 1;
            this._centerPanel.Resize += new System.EventHandler(this.CenterPanel_Resize);
            // 
            // _lblCoords
            // 
            this._lblCoords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._lblCoords.AutoSize = true;
            this._lblCoords.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this._lblCoords.Location = new System.Drawing.Point(10, 460);
            this._lblCoords.Name = "_lblCoords";
            this._lblCoords.Size = new System.Drawing.Size(89, 25);
            this._lblCoords.TabIndex = 1;
            this._lblCoords.Text = "Pixel: (-, -)";
            // 
            // _canvas
            // 
            this._canvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(38)))));
            this._canvas.CellSize = 32;
            this._canvas.CurrentTool = ZXSpectrumSpriteGenerator.UI.Controls.DrawingTool.Pencil;
            this._canvas.Cursor = System.Windows.Forms.Cursors.Cross;
            this._canvas.Location = new System.Drawing.Point(150, 60);
            this._canvas.Name = "_canvas";
            this._canvas.ShowCharGrid = false;
            this._canvas.ShowPixelGrid = true;
            this._canvas.Size = new System.Drawing.Size(256, 256);
            this._canvas.TabIndex = 0;
            this._canvas.HoverCoordinatesChanged += new System.EventHandler<ZXSpectrumSpriteGenerator.UI.Controls.HoverCoordinateEventArgs>(this.Canvas_HoverCoordinatesChanged);
            this._canvas.SpriteChanged += new System.EventHandler(this.Canvas_SpriteChanged);
            // 
            // _leftColorBar
            // 
            this._leftColorBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(26)))));
            this._leftColorBar.Controls.Add(this._chkFlash);
            this._leftColorBar.Controls.Add(this._chkBright);
            this._leftColorBar.Controls.Add(this._pnlPaperColors);
            this._leftColorBar.Controls.Add(this._lblPaperTitle);
            this._leftColorBar.Controls.Add(this._pnlInkColors);
            this._leftColorBar.Controls.Add(this._lblInkTitle);
            this._leftColorBar.Dock = System.Windows.Forms.DockStyle.Left;
            this._leftColorBar.Location = new System.Drawing.Point(0, 0);
            this._leftColorBar.Name = "_leftColorBar";
            this._leftColorBar.Padding = new System.Windows.Forms.Padding(8);
            this._leftColorBar.Size = new System.Drawing.Size(130, 484);
            this._leftColorBar.TabIndex = 0;
            // 
            // _chkFlash
            // 
            this._chkFlash.AutoSize = true;
            this._chkFlash.ForeColor = System.Drawing.Color.White;
            this._chkFlash.Location = new System.Drawing.Point(10, 345);
            this._chkFlash.Name = "_chkFlash";
            this._chkFlash.Size = new System.Drawing.Size(78, 29);
            this._chkFlash.TabIndex = 5;
            this._chkFlash.Text = "Flash";
            this._chkFlash.UseVisualStyleBackColor = true;
            this._chkFlash.CheckedChanged += new System.EventHandler(this._chkFlash_CheckedChanged);
            // 
            // _chkBright
            // 
            this._chkBright.AutoSize = true;
            this._chkBright.Checked = true;
            this._chkBright.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkBright.ForeColor = System.Drawing.Color.White;
            this._chkBright.Location = new System.Drawing.Point(10, 310);
            this._chkBright.Name = "_chkBright";
            this._chkBright.Size = new System.Drawing.Size(132, 29);
            this._chkBright.TabIndex = 4;
            this._chkBright.Text = "Bright (Brill.)";
            this._chkBright.UseVisualStyleBackColor = true;
            this._chkBright.CheckedChanged += new System.EventHandler(this.ChkBright_CheckedChanged);
            // 
            // _pnlPaperColors
            // 
            this._pnlPaperColors.Location = new System.Drawing.Point(8, 185);
            this._pnlPaperColors.Name = "_pnlPaperColors";
            this._pnlPaperColors.Size = new System.Drawing.Size(112, 110);
            this._pnlPaperColors.TabIndex = 3;
            // 
            // _lblPaperTitle
            // 
            this._lblPaperTitle.AutoSize = true;
            this._lblPaperTitle.Font = new System.Drawing.Font("Segoe UI", 8.2F, System.Drawing.FontStyle.Bold);
            this._lblPaperTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this._lblPaperTitle.Location = new System.Drawing.Point(8, 168);
            this._lblPaperTitle.Name = "_lblPaperTitle";
            this._lblPaperTitle.Size = new System.Drawing.Size(149, 23);
            this._lblPaperTitle.TabIndex = 2;
            this._lblPaperTitle.Text = "PAPER (SFONDO)";
            // 
            // _pnlInkColors
            // 
            this._pnlInkColors.Location = new System.Drawing.Point(8, 30);
            this._pnlInkColors.Name = "_pnlInkColors";
            this._pnlInkColors.Size = new System.Drawing.Size(112, 110);
            this._pnlInkColors.TabIndex = 1;
            // 
            // _lblInkTitle
            // 
            this._lblInkTitle.AutoSize = true;
            this._lblInkTitle.Font = new System.Drawing.Font("Segoe UI", 8.2F, System.Drawing.FontStyle.Bold);
            this._lblInkTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this._lblInkTitle.Location = new System.Drawing.Point(8, 12);
            this._lblInkTitle.Name = "_lblInkTitle";
            this._lblInkTitle.Size = new System.Drawing.Size(131, 23);
            this._lblInkTitle.TabIndex = 0;
            this._lblInkTitle.Text = "INK (DISEGNO)";
            // 
            // TileEditorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(24)))));
            this.Controls.Add(this._centerContainer);
            this.Controls.Add(this._rightPanel);
            this.Controls.Add(this._bottomPanel);
            this.Controls.Add(this._topBar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "TileEditorView";
            this.Size = new System.Drawing.Size(1000, 650);
            this._topBar.ResumeLayout(false);
            this._topBar.PerformLayout();
            this._tsTools.ResumeLayout(false);
            this._tsTools.PerformLayout();
            this._bottomPanel.ResumeLayout(false);
            this._bottomPanel.PerformLayout();
            this._filmstripBar.ResumeLayout(false);
            this._filmstripBar.PerformLayout();
            this._rightPanel.ResumeLayout(false);
            this._rightPanel.PerformLayout();
            this._centerContainer.ResumeLayout(false);
            this._centerPanel.ResumeLayout(false);
            this._centerPanel.PerformLayout();
            this._leftColorBar.ResumeLayout(false);
            this._leftColorBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _topBar;
        private System.Windows.Forms.ToolStrip _tsTools;
        private System.Windows.Forms.ToolStripButton _btnBackToMap;
        private System.Windows.Forms.ToolStripButton _btnBackToSprites;
        private System.Windows.Forms.ToolStripSeparator _sep1;
        private System.Windows.Forms.ToolStripLabel _lblTools;
        private System.Windows.Forms.ToolStripButton _btnToolPencil;
        private System.Windows.Forms.ToolStripButton _btnToolEraser;
        private System.Windows.Forms.ToolStripButton _btnToolLine;
        private System.Windows.Forms.ToolStripButton _btnToolRect;
        private System.Windows.Forms.ToolStripButton _btnToolRectFill;
        private System.Windows.Forms.ToolStripButton _btnToolCircle;
        private System.Windows.Forms.ToolStripButton _btnToolCircleFill;
        private System.Windows.Forms.ToolStripButton _btnToolFill;
        private System.Windows.Forms.ToolStripButton _btnToolPicker;
        private System.Windows.Forms.ToolStripSeparator _sep2;
        private System.Windows.Forms.ToolStripButton _btnUndo;
        private System.Windows.Forms.ToolStripButton _btnRedo;
        private System.Windows.Forms.ToolStripSeparator _sep3;
        private System.Windows.Forms.ToolStripButton _btnFlipH;
        private System.Windows.Forms.ToolStripButton _btnFlipV;
        private System.Windows.Forms.ToolStripButton _btnRotate;
        private System.Windows.Forms.ToolStripButton _btnInvert;
        private System.Windows.Forms.ToolStripButton _btnClear;
        private System.Windows.Forms.ToolStripSeparator _sep4;
        private System.Windows.Forms.ToolStripButton _btnGrid;
        private System.Windows.Forms.ToolStripButton _btnZoomOut;
        private System.Windows.Forms.ToolStripLabel _lblZoom;
        private System.Windows.Forms.ToolStripButton _btnZoomIn;
        private System.Windows.Forms.Panel _bottomPanel;
        private ZXSpectrumSpriteGenerator.UI.Controls.SpriteFilmstrip _filmstrip;
        private System.Windows.Forms.ToolStrip _filmstripBar;
        private System.Windows.Forms.ToolStripLabel _lblTilesetTitle;
        private System.Windows.Forms.ToolStripButton _btnAddTile;
        private System.Windows.Forms.ToolStripButton _btnImportTiles;
        private System.Windows.Forms.ToolStripButton _btnPasteClipboard;
        private System.Windows.Forms.ToolStripSeparator _sepFs1;
        private System.Windows.Forms.ToolStripButton _btnDuplicateTile;
        private System.Windows.Forms.ToolStripButton _btnDeleteTile;
        private System.Windows.Forms.ToolStripButton _btnMoveLeft;
        private System.Windows.Forms.ToolStripButton _btnMoveRight;
        private System.Windows.Forms.Panel _rightPanel;
        private System.Windows.Forms.Label _lblTilePropTitle;
        private System.Windows.Forms.Label _lblName;
        private System.Windows.Forms.TextBox _txtName;
        private System.Windows.Forms.Label _lblInfo;
        private System.Windows.Forms.Panel _pnlPreviewBox;
        private System.Windows.Forms.Label _lblPatternTitle;
        private System.Windows.Forms.Panel _pnlPatternPreview;
        private System.Windows.Forms.Label _lblAsmTitle;
        private System.Windows.Forms.TextBox _txtQuickAsm;
        private System.Windows.Forms.Button _btnCopyAsm;
        private System.Windows.Forms.Button _btnGoToMap;
        private System.Windows.Forms.Panel _centerContainer;
        private System.Windows.Forms.Panel _centerPanel;
        private ZXSpectrumSpriteGenerator.UI.Controls.SpriteCanvas _canvas;
        private System.Windows.Forms.Panel _leftColorBar;
        private System.Windows.Forms.Label _lblInkTitle;
        private System.Windows.Forms.FlowLayoutPanel _pnlInkColors;
        private System.Windows.Forms.Label _lblPaperTitle;
        private System.Windows.Forms.FlowLayoutPanel _pnlPaperColors;
        private System.Windows.Forms.CheckBox _chkBright;
        private System.Windows.Forms.Label _lblCoords;
        private System.Windows.Forms.CheckBox _chkFlash;
    }
}
