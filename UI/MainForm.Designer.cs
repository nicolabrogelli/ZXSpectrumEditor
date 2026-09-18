namespace ZXSpectrumSpriteGenerator.UI
{
    partial class MainForm
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

        #region Codice generato da Progettazione Windows Form

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this._mFile = new System.Windows.Forms.ToolStripMenuItem();
            this._mNew = new System.Windows.Forms.ToolStripMenuItem();
            this._mOpen = new System.Windows.Forms.ToolStripMenuItem();
            this._mSave = new System.Windows.Forms.ToolStripMenuItem();
            this._mSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this._mFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this._mOpenBin = new System.Windows.Forms.ToolStripMenuItem();
            this._mExport = new System.Windows.Forms.ToolStripMenuItem();
            this._mExportPng = new System.Windows.Forms.ToolStripMenuItem();
            this._mFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this._mExit = new System.Windows.Forms.ToolStripMenuItem();
            this._mEdit = new System.Windows.Forms.ToolStripMenuItem();
            this._mUndo = new System.Windows.Forms.ToolStripMenuItem();
            this._mRedo = new System.Windows.Forms.ToolStripMenuItem();
            this._mEditSep1 = new System.Windows.Forms.ToolStripSeparator();
            this._mSpriteClear = new System.Windows.Forms.ToolStripMenuItem();
            this._mSpriteInvert = new System.Windows.Forms.ToolStripMenuItem();
            this._mEditSep2 = new System.Windows.Forms.ToolStripSeparator();
            this._mSpriteAddEnemies = new System.Windows.Forms.ToolStripMenuItem();
            this._mSpritePasteNemico = new System.Windows.Forms.ToolStripMenuItem();
            this._mEditSep3 = new System.Windows.Forms.ToolStripSeparator();
            this._mCopyAsm = new System.Windows.Forms.ToolStripMenuItem();
            this._mView = new System.Windows.Forms.ToolStripMenuItem();
            this._mViewSprites = new System.Windows.Forms.ToolStripMenuItem();
            this._mViewTiles = new System.Windows.Forms.ToolStripMenuItem();
            this._mViewMaps = new System.Windows.Forms.ToolStripMenuItem();
            this._mTrans = new System.Windows.Forms.ToolStripMenuItem();
            this._mFlipH = new System.Windows.Forms.ToolStripMenuItem();
            this._mFlipV = new System.Windows.Forms.ToolStripMenuItem();
            this._mTransSep1 = new System.Windows.Forms.ToolStripSeparator();
            this._mRotCW = new System.Windows.Forms.ToolStripMenuItem();
            this._mRotCCW = new System.Windows.Forms.ToolStripMenuItem();
            this._mTransSep2 = new System.Windows.Forms.ToolStripSeparator();
            this._mShiftUp = new System.Windows.Forms.ToolStripMenuItem();
            this._mShiftDown = new System.Windows.Forms.ToolStripMenuItem();
            this._mShiftLeft = new System.Windows.Forms.ToolStripMenuItem();
            this._mShiftRight = new System.Windows.Forms.ToolStripMenuItem();
            this._mPreset = new System.Windows.Forms.ToolStripMenuItem();
            this._mAddEnemiesPreset = new System.Windows.Forms.ToolStripMenuItem();
            this._mSubPresets = new System.Windows.Forms.ToolStripMenuItem();
            this._mPresetSep1 = new System.Windows.Forms.ToolStripSeparator();
            this._mPresetEnemy = new System.Windows.Forms.ToolStripMenuItem();
            this._mPresetWalker = new System.Windows.Forms.ToolStripMenuItem();
            this._mMaps = new System.Windows.Forms.ToolStripMenuItem();
            this._mBackToSprites = new System.Windows.Forms.ToolStripMenuItem();
            this._mOpenMapEditor = new System.Windows.Forms.ToolStripMenuItem();
            this._mMapsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this._mNewMap = new System.Windows.Forms.ToolStripMenuItem();
            this._mDupMap = new System.Windows.Forms.ToolStripMenuItem();
            this._mDelMap = new System.Windows.Forms.ToolStripMenuItem();
            this._mRenameMap = new System.Windows.Forms.ToolStripMenuItem();
            this._mResizeMap = new System.Windows.Forms.ToolStripMenuItem();
            this._mClearMap = new System.Windows.Forms.ToolStripMenuItem();
            this._mMapsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this._mExpMap = new System.Windows.Forms.ToolStripMenuItem();
            this._mExpPng = new System.Windows.Forms.ToolStripMenuItem();
            this._mHelp = new System.Windows.Forms.ToolStripMenuItem();
            this._mAbout = new System.Windows.Forms.ToolStripMenuItem();
            this._statusStrip = new System.Windows.Forms.StatusStrip();
            this._lblCoords = new System.Windows.Forms.ToolStripStatusLabel();
            this._lblCharCoords = new System.Windows.Forms.ToolStripStatusLabel();
            this._lblDimensions = new System.Windows.Forms.ToolStripStatusLabel();
            this._lblTotalBytes = new System.Windows.Forms.ToolStripStatusLabel();
            this._lblTool = new System.Windows.Forms.ToolStripStatusLabel();
            this._mainTabs = new System.Windows.Forms.TabControl();
            this._tabSprites = new System.Windows.Forms.TabPage();
            this._centerPanel = new System.Windows.Forms.Panel();
            this._bottomPanel = new System.Windows.Forms.Panel();
            this._filmstripNavBar = new System.Windows.Forms.Panel();
            this._lblFilmstripInfo = new System.Windows.Forms.Label();
            this._btnScrollEnd = new System.Windows.Forms.Button();
            this._btnScrollRight = new System.Windows.Forms.Button();
            this._filmstripScrollBar = new System.Windows.Forms.HScrollBar();
            this._btnScrollLeft = new System.Windows.Forms.Button();
            this._btnScrollStart = new System.Windows.Forms.Button();
            this._filmstripControlBar = new System.Windows.Forms.Panel();
            this._cmbJumpEnemy = new System.Windows.Forms.ComboBox();
            this._lblJump = new System.Windows.Forms.Label();
            this._btnPasteNemico = new System.Windows.Forms.Button();
            this._btnMvR = new System.Windows.Forms.Button();
            this._btnMvL = new System.Windows.Forms.Button();
            this._btnDel = new System.Windows.Forms.Button();
            this._btnClone = new System.Windows.Forms.Button();
            this._btnAdd = new System.Windows.Forms.Button();
            this._btnAddMultiple = new System.Windows.Forms.Button();
            this._lblStripTitle = new System.Windows.Forms.Label();
            this._rightPanel = new System.Windows.Forms.Panel();
            this._btnCopyQuick = new System.Windows.Forms.Button();
            this._txtQuickAsm = new System.Windows.Forms.TextBox();
            this._lblQuickAsmTitle = new System.Windows.Forms.Label();
            this._lblFps = new System.Windows.Forms.Label();
            this._tbAnimFps = new System.Windows.Forms.TrackBar();
            this._lblSpeed = new System.Windows.Forms.Label();
            this._lblAnimFrame = new System.Windows.Forms.Label();
            this._btnPlayAnim = new System.Windows.Forms.Button();
            this._cmbAnimScope = new System.Windows.Forms.ComboBox();
            this._lblScope = new System.Windows.Forms.Label();
            this._lblAnimTitle = new System.Windows.Forms.Label();
            this._prevBox = new System.Windows.Forms.Panel();
            this._picPreview4x = new System.Windows.Forms.PictureBox();
            this._lbl4x = new System.Windows.Forms.Label();
            this._picPreview1x = new System.Windows.Forms.PictureBox();
            this._lbl1x = new System.Windows.Forms.Label();
            this._lblPrevTitle = new System.Windows.Forms.Label();
            this._chkBright = new System.Windows.Forms.CheckBox();
            this._cmbPaper = new System.Windows.Forms.ComboBox();
            this._lblPaper = new System.Windows.Forms.Label();
            this._cmbInk = new System.Windows.Forms.ComboBox();
            this._lblInk = new System.Windows.Forms.Label();
            this._lblColorsTitle = new System.Windows.Forms.Label();
            this._txtCurrentSpriteName = new System.Windows.Forms.TextBox();
            this._lblSpriteNameTitle = new System.Windows.Forms.Label();
            this._mainToolbar = new System.Windows.Forms.ToolStrip();
            this._btnNew = new System.Windows.Forms.ToolStripButton();
            this._btnOpen = new System.Windows.Forms.ToolStripButton();
            this._btnSave = new System.Windows.Forms.ToolStripButton();
            this._sepToolbar1 = new System.Windows.Forms.ToolStripSeparator();
            this._btnToolPencil = new System.Windows.Forms.ToolStripButton();
            this._btnToolLine = new System.Windows.Forms.ToolStripButton();
            this._btnToolRect = new System.Windows.Forms.ToolStripButton();
            this._btnToolRectFill = new System.Windows.Forms.ToolStripButton();
            this._btnToolCircle = new System.Windows.Forms.ToolStripButton();
            this._btnToolCircleFill = new System.Windows.Forms.ToolStripButton();
            this._btnToolFill = new System.Windows.Forms.ToolStripButton();
            this._sepToolbar2 = new System.Windows.Forms.ToolStripSeparator();
            this._btnUndo = new System.Windows.Forms.ToolStripButton();
            this._btnRedo = new System.Windows.Forms.ToolStripButton();
            this._sepToolbar3 = new System.Windows.Forms.ToolStripSeparator();
            this._btnFlipH = new System.Windows.Forms.ToolStripButton();
            this._btnFlipV = new System.Windows.Forms.ToolStripButton();
            this._btnRot = new System.Windows.Forms.ToolStripButton();
            this._btnInv = new System.Windows.Forms.ToolStripButton();
            this._btnClear = new System.Windows.Forms.ToolStripButton();
            this._sepToolbar4 = new System.Windows.Forms.ToolStripSeparator();
            this._btnLeft = new System.Windows.Forms.ToolStripButton();
            this._btnUp = new System.Windows.Forms.ToolStripButton();
            this._btnDown = new System.Windows.Forms.ToolStripButton();
            this._btnRight = new System.Windows.Forms.ToolStripButton();
            this._sepToolbar5 = new System.Windows.Forms.ToolStripSeparator();
            this._btnToggleGrid = new System.Windows.Forms.ToolStripButton();
            this._btnToggleCharGrid = new System.Windows.Forms.ToolStripButton();
            this._sepToolbar6 = new System.Windows.Forms.ToolStripSeparator();
            this._btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this._btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this._sepToolbar7 = new System.Windows.Forms.ToolStripSeparator();
            this._btnQuickExport = new System.Windows.Forms.ToolStripButton();
            this._sepToolbar8 = new System.Windows.Forms.ToolStripSeparator();
            this._btnGoToTiles = new System.Windows.Forms.ToolStripButton();
            this._btnGoToMaps = new System.Windows.Forms.ToolStripButton();
            this._tabTiles = new System.Windows.Forms.TabPage();
            this._tabMaps = new System.Windows.Forms.TabPage();
            this._animTimer = new System.Windows.Forms.Timer(this.components);
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            this._chkFlash = new System.Windows.Forms.CheckBox();
            this._canvas = new ZXSpectrumSpriteGenerator.UI.Controls.SpriteCanvas();
            this._filmstrip = new ZXSpectrumSpriteGenerator.UI.Controls.SpriteFilmstrip();
            this._tileEditorView = new ZXSpectrumSpriteGenerator.UI.TileEditorView();
            this._mapEditorView = new ZXSpectrumSpriteGenerator.UI.MapEditorView();
            this._menuStrip.SuspendLayout();
            this._statusStrip.SuspendLayout();
            this._mainTabs.SuspendLayout();
            this._tabSprites.SuspendLayout();
            this._centerPanel.SuspendLayout();
            this._bottomPanel.SuspendLayout();
            this._filmstripNavBar.SuspendLayout();
            this._filmstripControlBar.SuspendLayout();
            this._rightPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._tbAnimFps)).BeginInit();
            this._prevBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._picPreview4x)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._picPreview1x)).BeginInit();
            this._mainToolbar.SuspendLayout();
            this._tabTiles.SuspendLayout();
            this._tabMaps.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuStrip
            // 
            this._menuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(28)))));
            this._menuStrip.ForeColor = System.Drawing.Color.White;
            this._menuStrip.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this._menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._mFile,
            this._mEdit,
            this._mView,
            this._mTrans,
            this._mPreset,
            this._mMaps,
            this._mHelp});
            this._menuStrip.Location = new System.Drawing.Point(0, 0);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this._menuStrip.Size = new System.Drawing.Size(1303, 36);
            this._menuStrip.TabIndex = 0;
            // 
            // _mFile
            // 
            this._mFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._mNew,
            this._mOpen,
            this._mSave,
            this._mSaveAs,
            this._mFileSep1,
            this._mOpenBin,
            this._mExport,
            this._mExportPng,
            this._mFileSep2,
            this._mExit});
            this._mFile.Name = "_mFile";
            this._mFile.Size = new System.Drawing.Size(54, 32);
            this._mFile.Text = "&File";
            // 
            // _mNew
            // 
            this._mNew.Name = "_mNew";
            this._mNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this._mNew.Size = new System.Drawing.Size(481, 34);
            this._mNew.Text = "&Nuovo Progetto...";
            this._mNew.Click += new System.EventHandler(this.ActionNewProject_Click);
            // 
            // _mOpen
            // 
            this._mOpen.Name = "_mOpen";
            this._mOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this._mOpen.Size = new System.Drawing.Size(481, 34);
            this._mOpen.Text = "&Apri Progetto...";
            this._mOpen.Click += new System.EventHandler(this.ActionOpenProject_Click);
            // 
            // _mSave
            // 
            this._mSave.Name = "_mSave";
            this._mSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this._mSave.Size = new System.Drawing.Size(481, 34);
            this._mSave.Text = "&Salva Progetto";
            this._mSave.Click += new System.EventHandler(this.ActionSaveProject_Click);
            // 
            // _mSaveAs
            // 
            this._mSaveAs.Name = "_mSaveAs";
            this._mSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this._mSaveAs.Size = new System.Drawing.Size(481, 34);
            this._mSaveAs.Text = "Salva Progetto &con Nome...";
            this._mSaveAs.Click += new System.EventHandler(this.ActionSaveProjectAs_Click);
            // 
            // _mFileSep1
            // 
            this._mFileSep1.Name = "_mFileSep1";
            this._mFileSep1.Size = new System.Drawing.Size(478, 6);
            // 
            // _mOpenBin
            // 
            this._mOpenBin.Name = "_mOpenBin";
            this._mOpenBin.Size = new System.Drawing.Size(481, 34);
            this._mOpenBin.Text = "&Importa Dati / Assembly...";
            this._mOpenBin.Click += new System.EventHandler(this.ActionImport_Click);
            // 
            // _mExport
            // 
            this._mExport.Name = "_mExport";
            this._mExport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this._mExport.Size = new System.Drawing.Size(481, 34);
            this._mExport.Text = "&Esporta Codice / Dati...";
            this._mExport.Click += new System.EventHandler(this.ActionExport_Click);
            // 
            // _mExportPng
            // 
            this._mExportPng.Name = "_mExportPng";
            this._mExportPng.Size = new System.Drawing.Size(481, 34);
            this._mExportPng.Text = "Esporta &Spritesheet PNG...";
            this._mExportPng.Click += new System.EventHandler(this.ActionExportPng_Click);
            // 
            // _mFileSep2
            // 
            this._mFileSep2.Name = "_mFileSep2";
            this._mFileSep2.Size = new System.Drawing.Size(478, 6);
            // 
            // _mExit
            // 
            this._mExit.Name = "_mExit";
            this._mExit.Size = new System.Drawing.Size(481, 34);
            this._mExit.Text = "&Esci";
            this._mExit.Click += new System.EventHandler(this.ActionExit_Click);
            // 
            // _mEdit
            // 
            this._mEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._mUndo,
            this._mRedo,
            this._mEditSep1,
            this._mSpriteClear,
            this._mSpriteInvert,
            this._mEditSep2,
            this._mSpriteAddEnemies,
            this._mSpritePasteNemico,
            this._mEditSep3,
            this._mCopyAsm});
            this._mEdit.Name = "_mEdit";
            this._mEdit.Size = new System.Drawing.Size(97, 32);
            this._mEdit.Text = "&Modifica";
            // 
            // _mUndo
            // 
            this._mUndo.Name = "_mUndo";
            this._mUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this._mUndo.Size = new System.Drawing.Size(428, 34);
            this._mUndo.Text = "&Annulla";
            this._mUndo.Click += new System.EventHandler(this.ActionUndo_Click);
            // 
            // _mRedo
            // 
            this._mRedo.Name = "_mRedo";
            this._mRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this._mRedo.Size = new System.Drawing.Size(428, 34);
            this._mRedo.Text = "&Ripeti";
            this._mRedo.Click += new System.EventHandler(this.ActionRedo_Click);
            // 
            // _mEditSep1
            // 
            this._mEditSep1.Name = "_mEditSep1";
            this._mEditSep1.Size = new System.Drawing.Size(425, 6);
            // 
            // _mSpriteClear
            // 
            this._mSpriteClear.Name = "_mSpriteClear";
            this._mSpriteClear.Size = new System.Drawing.Size(428, 34);
            this._mSpriteClear.Text = "&Cancella Sprite";
            this._mSpriteClear.Click += new System.EventHandler(this.ActionClear_Click);
            // 
            // _mSpriteInvert
            // 
            this._mSpriteInvert.Name = "_mSpriteInvert";
            this._mSpriteInvert.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this._mSpriteInvert.Size = new System.Drawing.Size(428, 34);
            this._mSpriteInvert.Text = "&Inverti Colori (NOT)";
            this._mSpriteInvert.Click += new System.EventHandler(this.ActionInvert_Click);
            // 
            // _mEditSep2
            // 
            this._mEditSep2.Name = "_mEditSep2";
            this._mEditSep2.Size = new System.Drawing.Size(425, 6);
            // 
            // _mSpriteAddEnemies
            // 
            this._mSpriteAddEnemies.Name = "_mSpriteAddEnemies";
            this._mSpriteAddEnemies.Size = new System.Drawing.Size(428, 34);
            this._mSpriteAddEnemies.Text = "Aggiungi &Più Sprite / Nemici...";
            this._mSpriteAddEnemies.Click += new System.EventHandler(this.ActionAddEnemies_Click);
            // 
            // _mSpritePasteNemico
            // 
            this._mSpritePasteNemico.Name = "_mSpritePasteNemico";
            this._mSpritePasteNemico.Size = new System.Drawing.Size(428, 34);
            this._mSpritePasteNemico.Text = "Incolla &Nemico da Appunti (db)...";
            this._mSpritePasteNemico.Click += new System.EventHandler(this.ActionPasteEnemyFromClipboard_Click);
            // 
            // _mEditSep3
            // 
            this._mEditSep3.Name = "_mEditSep3";
            this._mEditSep3.Size = new System.Drawing.Size(425, 6);
            // 
            // _mCopyAsm
            // 
            this._mCopyAsm.Name = "_mCopyAsm";
            this._mCopyAsm.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this._mCopyAsm.Size = new System.Drawing.Size(428, 34);
            this._mCopyAsm.Text = "Copia &Assembly negli Appunti";
            this._mCopyAsm.Click += new System.EventHandler(this.ActionQuickCopyAsm_Click);
            // 
            // _mView
            // 
            this._mView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._mViewSprites,
            this._mViewTiles,
            this._mViewMaps});
            this._mView.Name = "_mView";
            this._mView.Size = new System.Drawing.Size(103, 32);
            this._mView.Text = "&Visualizza";
            // 
            // _mViewSprites
            // 
            this._mViewSprites.Name = "_mViewSprites";
            this._mViewSprites.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this._mViewSprites.Size = new System.Drawing.Size(354, 34);
            this._mViewSprites.Text = "👾 &Editor Sprite";
            this._mViewSprites.Click += new System.EventHandler(this.SwitchToSpriteEditor_Click);
            // 
            // _mViewTiles
            // 
            this._mViewTiles.Name = "_mViewTiles";
            this._mViewTiles.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this._mViewTiles.Size = new System.Drawing.Size(354, 34);
            this._mViewTiles.Text = "🧱 &Editor Tileset 8x8 (F3)";
            this._mViewTiles.Click += new System.EventHandler(this.SwitchToTileEditor_Click);
            // 
            // _mViewMaps
            // 
            this._mViewMaps.Name = "_mViewMaps";
            this._mViewMaps.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this._mViewMaps.Size = new System.Drawing.Size(354, 34);
            this._mViewMaps.Text = "🗺️ &Editor Mappe di Gioco";
            this._mViewMaps.Click += new System.EventHandler(this.SwitchToMapEditor_Click);
            // 
            // _mTrans
            // 
            this._mTrans.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._mFlipH,
            this._mFlipV,
            this._mTransSep1,
            this._mRotCW,
            this._mRotCCW,
            this._mTransSep2,
            this._mShiftUp,
            this._mShiftDown,
            this._mShiftLeft,
            this._mShiftRight});
            this._mTrans.Name = "_mTrans";
            this._mTrans.Size = new System.Drawing.Size(106, 32);
            this._mTrans.Text = "&Trasforma";
            // 
            // _mFlipH
            // 
            this._mFlipH.Name = "_mFlipH";
            this._mFlipH.Size = new System.Drawing.Size(338, 34);
            this._mFlipH.Text = "Specchia &Orizzontale (Flip H)";
            this._mFlipH.Click += new System.EventHandler(this.ActionFlipH_Click);
            // 
            // _mFlipV
            // 
            this._mFlipV.Name = "_mFlipV";
            this._mFlipV.Size = new System.Drawing.Size(338, 34);
            this._mFlipV.Text = "Specchia &Verticale (Flip V)";
            this._mFlipV.Click += new System.EventHandler(this.ActionFlipV_Click);
            // 
            // _mTransSep1
            // 
            this._mTransSep1.Name = "_mTransSep1";
            this._mTransSep1.Size = new System.Drawing.Size(335, 6);
            // 
            // _mRotCW
            // 
            this._mRotCW.Name = "_mRotCW";
            this._mRotCW.Size = new System.Drawing.Size(338, 34);
            this._mRotCW.Text = "&Ruota 90° Orario";
            this._mRotCW.Click += new System.EventHandler(this.ActionRotateCW_Click);
            // 
            // _mRotCCW
            // 
            this._mRotCCW.Name = "_mRotCCW";
            this._mRotCCW.Size = new System.Drawing.Size(338, 34);
            this._mRotCCW.Text = "Ruota 90° &Antiorario";
            this._mRotCCW.Click += new System.EventHandler(this.ActionRotateCCW_Click);
            // 
            // _mTransSep2
            // 
            this._mTransSep2.Name = "_mTransSep2";
            this._mTransSep2.Size = new System.Drawing.Size(335, 6);
            // 
            // _mShiftUp
            // 
            this._mShiftUp.Name = "_mShiftUp";
            this._mShiftUp.Size = new System.Drawing.Size(338, 34);
            this._mShiftUp.Text = "Sposta in &Alto";
            this._mShiftUp.Click += new System.EventHandler(this.ActionShiftUp_Click);
            // 
            // _mShiftDown
            // 
            this._mShiftDown.Name = "_mShiftDown";
            this._mShiftDown.Size = new System.Drawing.Size(338, 34);
            this._mShiftDown.Text = "Sposta in &Basso";
            this._mShiftDown.Click += new System.EventHandler(this.ActionShiftDown_Click);
            // 
            // _mShiftLeft
            // 
            this._mShiftLeft.Name = "_mShiftLeft";
            this._mShiftLeft.Size = new System.Drawing.Size(338, 34);
            this._mShiftLeft.Text = "Sposta a &Sinistra";
            this._mShiftLeft.Click += new System.EventHandler(this.ActionShiftLeft_Click);
            // 
            // _mShiftRight
            // 
            this._mShiftRight.Name = "_mShiftRight";
            this._mShiftRight.Size = new System.Drawing.Size(338, 34);
            this._mShiftRight.Text = "Sposta a &Destra";
            this._mShiftRight.Click += new System.EventHandler(this.ActionShiftRight_Click);
            // 
            // _mPreset
            // 
            this._mPreset.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._mAddEnemiesPreset,
            this._mSubPresets,
            this._mPresetSep1,
            this._mPresetEnemy,
            this._mPresetWalker});
            this._mPreset.Name = "_mPreset";
            this._mPreset.Size = new System.Drawing.Size(76, 32);
            this._mPreset.Text = "&Preset";
            // 
            // _mAddEnemiesPreset
            // 
            this._mAddEnemiesPreset.Name = "_mAddEnemiesPreset";
            this._mAddEnemiesPreset.Size = new System.Drawing.Size(473, 34);
            this._mAddEnemiesPreset.Text = "➕ &Aggiungi Set Nemico al Progetto...";
            this._mAddEnemiesPreset.Click += new System.EventHandler(this.ActionAddEnemies_Click);
            // 
            // _mSubPresets
            // 
            this._mSubPresets.Name = "_mSubPresets";
            this._mSubPresets.Size = new System.Drawing.Size(473, 34);
            this._mSubPresets.Text = "Aggiungi &Preset Rapido";
            // 
            // _mPresetSep1
            // 
            this._mPresetSep1.Name = "_mPresetSep1";
            this._mPresetSep1.Size = new System.Drawing.Size(470, 6);
            // 
            // _mPresetEnemy
            // 
            this._mPresetEnemy.Name = "_mPresetEnemy";
            this._mPresetEnemy.Size = new System.Drawing.Size(473, 34);
            this._mPresetEnemy.Text = "&Reimposta Progetto su Nemico Originale";
            this._mPresetEnemy.Click += new System.EventHandler(this.ActionPresetEnemy_Click);
            // 
            // _mPresetWalker
            // 
            this._mPresetWalker.Name = "_mPresetWalker";
            this._mPresetWalker.Size = new System.Drawing.Size(473, 34);
            this._mPresetWalker.Text = "&Reimposta su Personaggio Camminata 16x16";
            this._mPresetWalker.Click += new System.EventHandler(this.ActionPresetWalker_Click);
            // 
            // _mMaps
            // 
            this._mMaps.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._mBackToSprites,
            this._mOpenMapEditor,
            this._mMapsSep1,
            this._mNewMap,
            this._mDupMap,
            this._mDelMap,
            this._mRenameMap,
            this._mResizeMap,
            this._mClearMap,
            this._mMapsSep2,
            this._mExpMap,
            this._mExpPng});
            this._mMaps.Name = "_mMaps";
            this._mMaps.Size = new System.Drawing.Size(84, 32);
            this._mMaps.Text = "&Mappe";
            this._mMaps.Visible = false;
            // 
            // _mBackToSprites
            // 
            this._mBackToSprites.Name = "_mBackToSprites";
            this._mBackToSprites.Size = new System.Drawing.Size(460, 34);
            this._mBackToSprites.Text = "👾 &Torna all\'Editor Sprite (F3)";
            this._mBackToSprites.Click += new System.EventHandler(this.SwitchToSpriteEditor_Click);
            // 
            // _mOpenMapEditor
            // 
            this._mOpenMapEditor.Name = "_mOpenMapEditor";
            this._mOpenMapEditor.Size = new System.Drawing.Size(460, 34);
            this._mOpenMapEditor.Text = "🗺️ Visualizza &Editor Mappe (F4)";
            this._mOpenMapEditor.Click += new System.EventHandler(this.SwitchToMapEditor_Click);
            // 
            // _mMapsSep1
            // 
            this._mMapsSep1.Name = "_mMapsSep1";
            this._mMapsSep1.Size = new System.Drawing.Size(457, 6);
            // 
            // _mNewMap
            // 
            this._mNewMap.Name = "_mNewMap";
            this._mNewMap.Size = new System.Drawing.Size(460, 34);
            this._mNewMap.Text = "➕ &Nuova Mappa...";
            this._mNewMap.Click += new System.EventHandler(this.ActionNewMap_Click);
            // 
            // _mDupMap
            // 
            this._mDupMap.Name = "_mDupMap";
            this._mDupMap.Size = new System.Drawing.Size(460, 34);
            this._mDupMap.Text = "📋 &Duplica Mappa Corrente";
            this._mDupMap.Click += new System.EventHandler(this.ActionDupMap_Click);
            // 
            // _mDelMap
            // 
            this._mDelMap.Name = "_mDelMap";
            this._mDelMap.Size = new System.Drawing.Size(460, 34);
            this._mDelMap.Text = "🗑️ &Elimina Mappa Corrente";
            this._mDelMap.Click += new System.EventHandler(this.ActionDelMap_Click);
            // 
            // _mRenameMap
            // 
            this._mRenameMap.Name = "_mRenameMap";
            this._mRenameMap.Size = new System.Drawing.Size(460, 34);
            this._mRenameMap.Text = "✏️ &Rinomina Mappa Corrente...";
            this._mRenameMap.Click += new System.EventHandler(this.ActionRenameMap_Click);
            // 
            // _mResizeMap
            // 
            this._mResizeMap.Name = "_mResizeMap";
            this._mResizeMap.Size = new System.Drawing.Size(460, 34);
            this._mResizeMap.Text = "📐 &Dimensioni Mappa...";
            this._mResizeMap.Click += new System.EventHandler(this.ActionResizeMap_Click);
            // 
            // _mClearMap
            // 
            this._mClearMap.Name = "_mClearMap";
            this._mClearMap.Size = new System.Drawing.Size(460, 34);
            this._mClearMap.Text = "🧹 &Pulisci Mappa Corrente";
            this._mClearMap.Click += new System.EventHandler(this.ActionClearMap_Click);
            // 
            // _mMapsSep2
            // 
            this._mMapsSep2.Name = "_mMapsSep2";
            this._mMapsSep2.Size = new System.Drawing.Size(457, 6);
            // 
            // _mExpMap
            // 
            this._mExpMap.Name = "_mExpMap";
            this._mExpMap.Size = new System.Drawing.Size(460, 34);
            this._mExpMap.Text = "⚡ &Esporta Dati Mappa (ASM / C / BASIC)...";
            this._mExpMap.Click += new System.EventHandler(this.ActionExpMap_Click);
            // 
            // _mExpPng
            // 
            this._mExpPng.Name = "_mExpPng";
            this._mExpPng.Size = new System.Drawing.Size(460, 34);
            this._mExpPng.Text = "📷 Salva Mappa come &PNG...";
            this._mExpPng.Click += new System.EventHandler(this.ActionExpPng_Click);
            // 
            // _mHelp
            // 
            this._mHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._mAbout});
            this._mHelp.Name = "_mHelp";
            this._mHelp.Size = new System.Drawing.Size(71, 32);
            this._mHelp.Text = "&Aiuto";
            // 
            // _mAbout
            // 
            this._mAbout.Name = "_mAbout";
            this._mAbout.Size = new System.Drawing.Size(455, 34);
            this._mAbout.Text = "&Informazioni ZX Spectrum Sprite Generator";
            this._mAbout.Click += new System.EventHandler(this.ActionAbout_Click);
            // 
            // _statusStrip
            // 
            this._statusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(28)))));
            this._statusStrip.ForeColor = System.Drawing.Color.LightGray;
            this._statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._lblCoords,
            this._lblCharCoords,
            this._lblDimensions,
            this._lblTotalBytes,
            this._lblTool});
            this._statusStrip.Location = new System.Drawing.Point(0, 865);
            this._statusStrip.Name = "_statusStrip";
            this._statusStrip.Size = new System.Drawing.Size(1303, 32);
            this._statusStrip.TabIndex = 1;
            // 
            // _lblCoords
            // 
            this._lblCoords.AutoSize = false;
            this._lblCoords.Name = "_lblCoords";
            this._lblCoords.Size = new System.Drawing.Size(110, 25);
            this._lblCoords.Text = "Pixel: (0, 0)";
            this._lblCoords.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblCharCoords
            // 
            this._lblCharCoords.AutoSize = false;
            this._lblCharCoords.Name = "_lblCharCoords";
            this._lblCharCoords.Size = new System.Drawing.Size(120, 25);
            this._lblCharCoords.Text = "Cella ZX: (0, 0)";
            this._lblCharCoords.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblDimensions
            // 
            this._lblDimensions.AutoSize = false;
            this._lblDimensions.Name = "_lblDimensions";
            this._lblDimensions.Size = new System.Drawing.Size(150, 25);
            this._lblDimensions.Text = "Dimensione: 16x16";
            this._lblDimensions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblTotalBytes
            // 
            this._lblTotalBytes.AutoSize = false;
            this._lblTotalBytes.Name = "_lblTotalBytes";
            this._lblTotalBytes.Size = new System.Drawing.Size(100, 25);
            this._lblTotalBytes.Text = "Bytes: 32";
            this._lblTotalBytes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblTool
            // 
            this._lblTool.Name = "_lblTool";
            this._lblTool.Size = new System.Drawing.Size(808, 25);
            this._lblTool.Spring = true;
            this._lblTool.Text = "Strumento: Matita";
            this._lblTool.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _mainTabs
            // 
            this._mainTabs.Controls.Add(this._tabSprites);
            this._mainTabs.Controls.Add(this._tabTiles);
            this._mainTabs.Controls.Add(this._tabMaps);
            this._mainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainTabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this._mainTabs.ItemSize = new System.Drawing.Size(230, 32);
            this._mainTabs.Location = new System.Drawing.Point(0, 36);
            this._mainTabs.Name = "_mainTabs";
            this._mainTabs.SelectedIndex = 0;
            this._mainTabs.Size = new System.Drawing.Size(1303, 829);
            this._mainTabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this._mainTabs.TabIndex = 2;
            this._mainTabs.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.MainTabs_DrawItem);
            this._mainTabs.SelectedIndexChanged += new System.EventHandler(this.MainTabs_SelectedIndexChanged);
            // 
            // _tabSprites
            // 
            this._tabSprites.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(28)))));
            this._tabSprites.Controls.Add(this._centerPanel);
            this._tabSprites.Controls.Add(this._bottomPanel);
            this._tabSprites.Controls.Add(this._rightPanel);
            this._tabSprites.Controls.Add(this._mainToolbar);
            this._tabSprites.Location = new System.Drawing.Point(4, 36);
            this._tabSprites.Name = "_tabSprites";
            this._tabSprites.Size = new System.Drawing.Size(1295, 789);
            this._tabSprites.TabIndex = 0;
            this._tabSprites.Text = "👾 Editor Sprite (F2)";
            // 
            // _centerPanel
            // 
            this._centerPanel.AutoScroll = true;
            this._centerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(24)))));
            this._centerPanel.Controls.Add(this._canvas);
            this._centerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._centerPanel.Location = new System.Drawing.Point(0, 42);
            this._centerPanel.Name = "_centerPanel";
            this._centerPanel.Size = new System.Drawing.Size(1015, 579);
            this._centerPanel.TabIndex = 3;
            this._centerPanel.Resize += new System.EventHandler(this.CenterPanel_Resize);
            // 
            // _bottomPanel
            // 
            this._bottomPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(28)))));
            this._bottomPanel.Controls.Add(this._filmstrip);
            this._bottomPanel.Controls.Add(this._filmstripNavBar);
            this._bottomPanel.Controls.Add(this._filmstripControlBar);
            this._bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._bottomPanel.Location = new System.Drawing.Point(0, 621);
            this._bottomPanel.Name = "_bottomPanel";
            this._bottomPanel.Size = new System.Drawing.Size(1015, 168);
            this._bottomPanel.TabIndex = 2;
            // 
            // _filmstripNavBar
            // 
            this._filmstripNavBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(22)))));
            this._filmstripNavBar.Controls.Add(this._lblFilmstripInfo);
            this._filmstripNavBar.Controls.Add(this._btnScrollEnd);
            this._filmstripNavBar.Controls.Add(this._btnScrollRight);
            this._filmstripNavBar.Controls.Add(this._filmstripScrollBar);
            this._filmstripNavBar.Controls.Add(this._btnScrollLeft);
            this._filmstripNavBar.Controls.Add(this._btnScrollStart);
            this._filmstripNavBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._filmstripNavBar.Location = new System.Drawing.Point(0, 142);
            this._filmstripNavBar.Name = "_filmstripNavBar";
            this._filmstripNavBar.Size = new System.Drawing.Size(1015, 26);
            this._filmstripNavBar.TabIndex = 1;
            this._filmstripNavBar.Resize += new System.EventHandler(this.FilmstripNavBar_Resize);
            // 
            // _lblFilmstripInfo
            // 
            this._lblFilmstripInfo.AutoSize = true;
            this._lblFilmstripInfo.Font = new System.Drawing.Font("Segoe UI", 8.2F, System.Drawing.FontStyle.Bold);
            this._lblFilmstripInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(220)))), ((int)(((byte)(100)))));
            this._lblFilmstripInfo.Location = new System.Drawing.Point(404, 5);
            this._lblFilmstripInfo.Name = "_lblFilmstripInfo";
            this._lblFilmstripInfo.Size = new System.Drawing.Size(0, 23);
            this._lblFilmstripInfo.TabIndex = 5;
            // 
            // _btnScrollEnd
            // 
            this._btnScrollEnd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this._btnScrollEnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnScrollEnd.ForeColor = System.Drawing.Color.White;
            this._btnScrollEnd.Location = new System.Drawing.Point(368, 2);
            this._btnScrollEnd.Name = "_btnScrollEnd";
            this._btnScrollEnd.Size = new System.Drawing.Size(28, 22);
            this._btnScrollEnd.TabIndex = 4;
            this._btnScrollEnd.Text = "⏭";
            this._toolTip.SetToolTip(this._btnScrollEnd, "Scorri alla fine della lista (ultimi sprite aggiunti)");
            this._btnScrollEnd.UseVisualStyleBackColor = false;
            this._btnScrollEnd.Click += new System.EventHandler(this.BtnScrollEnd_Click);
            // 
            // _btnScrollRight
            // 
            this._btnScrollRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this._btnScrollRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnScrollRight.ForeColor = System.Drawing.Color.White;
            this._btnScrollRight.Location = new System.Drawing.Point(300, 2);
            this._btnScrollRight.Name = "_btnScrollRight";
            this._btnScrollRight.Size = new System.Drawing.Size(64, 22);
            this._btnScrollRight.TabIndex = 3;
            this._btnScrollRight.Text = "Scorri ▶";
            this._toolTip.SetToolTip(this._btnScrollRight, "Scorri la lista a destra");
            this._btnScrollRight.UseVisualStyleBackColor = false;
            this._btnScrollRight.Click += new System.EventHandler(this.BtnScrollRight_Click);
            // 
            // _filmstripScrollBar
            // 
            this._filmstripScrollBar.Location = new System.Drawing.Point(102, 3);
            this._filmstripScrollBar.Name = "_filmstripScrollBar";
            this._filmstripScrollBar.Size = new System.Drawing.Size(194, 20);
            this._filmstripScrollBar.TabIndex = 2;
            this._filmstripScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.FilmstripScrollBar_Scroll);
            // 
            // _btnScrollLeft
            // 
            this._btnScrollLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this._btnScrollLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnScrollLeft.ForeColor = System.Drawing.Color.White;
            this._btnScrollLeft.Location = new System.Drawing.Point(34, 2);
            this._btnScrollLeft.Name = "_btnScrollLeft";
            this._btnScrollLeft.Size = new System.Drawing.Size(64, 22);
            this._btnScrollLeft.TabIndex = 1;
            this._btnScrollLeft.Text = "◀ Scorri";
            this._toolTip.SetToolTip(this._btnScrollLeft, "Scorri la lista a sinistra");
            this._btnScrollLeft.UseVisualStyleBackColor = false;
            this._btnScrollLeft.Click += new System.EventHandler(this.BtnScrollLeft_Click);
            // 
            // _btnScrollStart
            // 
            this._btnScrollStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this._btnScrollStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnScrollStart.ForeColor = System.Drawing.Color.White;
            this._btnScrollStart.Location = new System.Drawing.Point(4, 2);
            this._btnScrollStart.Name = "_btnScrollStart";
            this._btnScrollStart.Size = new System.Drawing.Size(28, 22);
            this._btnScrollStart.TabIndex = 0;
            this._btnScrollStart.Text = "⏮";
            this._toolTip.SetToolTip(this._btnScrollStart, "Scorri all\'inizio della lista (primo sprite)");
            this._btnScrollStart.UseVisualStyleBackColor = false;
            this._btnScrollStart.Click += new System.EventHandler(this.BtnScrollStart_Click);
            // 
            // _filmstripControlBar
            // 
            this._filmstripControlBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(36)))));
            this._filmstripControlBar.Controls.Add(this._cmbJumpEnemy);
            this._filmstripControlBar.Controls.Add(this._lblJump);
            this._filmstripControlBar.Controls.Add(this._btnPasteNemico);
            this._filmstripControlBar.Controls.Add(this._btnMvR);
            this._filmstripControlBar.Controls.Add(this._btnMvL);
            this._filmstripControlBar.Controls.Add(this._btnDel);
            this._filmstripControlBar.Controls.Add(this._btnClone);
            this._filmstripControlBar.Controls.Add(this._btnAdd);
            this._filmstripControlBar.Controls.Add(this._btnAddMultiple);
            this._filmstripControlBar.Controls.Add(this._lblStripTitle);
            this._filmstripControlBar.Dock = System.Windows.Forms.DockStyle.Top;
            this._filmstripControlBar.Location = new System.Drawing.Point(0, 0);
            this._filmstripControlBar.Name = "_filmstripControlBar";
            this._filmstripControlBar.Size = new System.Drawing.Size(1015, 34);
            this._filmstripControlBar.TabIndex = 0;
            // 
            // _cmbJumpEnemy
            // 
            this._cmbJumpEnemy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this._cmbJumpEnemy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbJumpEnemy.ForeColor = System.Drawing.Color.White;
            this._cmbJumpEnemy.Location = new System.Drawing.Point(665, 5);
            this._cmbJumpEnemy.Name = "_cmbJumpEnemy";
            this._cmbJumpEnemy.Size = new System.Drawing.Size(160, 33);
            this._cmbJumpEnemy.TabIndex = 9;
            this._toolTip.SetToolTip(this._cmbJumpEnemy, "Salta direttamente a qualsiasi nemico nel progetto e centranne la vista");
            this._cmbJumpEnemy.SelectedIndexChanged += new System.EventHandler(this.CmbJumpEnemy_SelectedIndexChanged);
            // 
            // _lblJump
            // 
            this._lblJump.AutoSize = true;
            this._lblJump.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._lblJump.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this._lblJump.Location = new System.Drawing.Point(575, 8);
            this._lblJump.Name = "_lblJump";
            this._lblJump.Size = new System.Drawing.Size(119, 23);
            this._lblJump.TabIndex = 8;
            this._lblJump.Text = "Vai a Nemico:";
            // 
            // _btnPasteNemico
            // 
            this._btnPasteNemico.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(90)))), ((int)(((byte)(130)))));
            this._btnPasteNemico.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnPasteNemico.ForeColor = System.Drawing.Color.White;
            this._btnPasteNemico.Location = new System.Drawing.Point(490, 4);
            this._btnPasteNemico.Name = "_btnPasteNemico";
            this._btnPasteNemico.Size = new System.Drawing.Size(75, 25);
            this._btnPasteNemico.TabIndex = 7;
            this._btnPasteNemico.Text = "📋 Incolla";
            this._toolTip.SetToolTip(this._btnPasteNemico, "Incolla dati sprite o nemico dagli appunti di Windows");
            this._btnPasteNemico.UseVisualStyleBackColor = false;
            this._btnPasteNemico.Click += new System.EventHandler(this.ActionPasteEnemyFromClipboard_Click);
            // 
            // _btnMvR
            // 
            this._btnMvR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(60)))));
            this._btnMvR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnMvR.ForeColor = System.Drawing.Color.White;
            this._btnMvR.Location = new System.Drawing.Point(455, 4);
            this._btnMvR.Name = "_btnMvR";
            this._btnMvR.Size = new System.Drawing.Size(30, 25);
            this._btnMvR.TabIndex = 6;
            this._btnMvR.Text = "►";
            this._toolTip.SetToolTip(this._btnMvR, "Sposta la posizione di questo sprite a destra");
            this._btnMvR.UseVisualStyleBackColor = false;
            this._btnMvR.Click += new System.EventHandler(this.ActionMoveSpriteRight_Click);
            // 
            // _btnMvL
            // 
            this._btnMvL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(60)))));
            this._btnMvL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnMvL.ForeColor = System.Drawing.Color.White;
            this._btnMvL.Location = new System.Drawing.Point(422, 4);
            this._btnMvL.Name = "_btnMvL";
            this._btnMvL.Size = new System.Drawing.Size(30, 25);
            this._btnMvL.TabIndex = 5;
            this._btnMvL.Text = "◄";
            this._toolTip.SetToolTip(this._btnMvL, "Sposta la posizione di questo sprite a sinistra");
            this._btnMvL.UseVisualStyleBackColor = false;
            this._btnMvL.Click += new System.EventHandler(this.ActionMoveSpriteLeft_Click);
            // 
            // _btnDel
            // 
            this._btnDel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this._btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnDel.ForeColor = System.Drawing.Color.White;
            this._btnDel.Location = new System.Drawing.Point(358, 4);
            this._btnDel.Name = "_btnDel";
            this._btnDel.Size = new System.Drawing.Size(60, 25);
            this._btnDel.TabIndex = 4;
            this._btnDel.Text = "Elimina";
            this._toolTip.SetToolTip(this._btnDel, "Elimina il frame selezionato");
            this._btnDel.UseVisualStyleBackColor = false;
            this._btnDel.Click += new System.EventHandler(this.ActionDeleteSprite_Click);
            // 
            // _btnClone
            // 
            this._btnClone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(70)))), ((int)(((byte)(90)))));
            this._btnClone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnClone.ForeColor = System.Drawing.Color.White;
            this._btnClone.Location = new System.Drawing.Point(293, 4);
            this._btnClone.Name = "_btnClone";
            this._btnClone.Size = new System.Drawing.Size(60, 25);
            this._btnClone.TabIndex = 3;
            this._btnClone.Text = "Duplica";
            this._toolTip.SetToolTip(this._btnClone, "Duplica il frame selezionato");
            this._btnClone.UseVisualStyleBackColor = false;
            this._btnClone.Click += new System.EventHandler(this.ActionDuplicateSprite_Click);
            // 
            // _btnAdd
            // 
            this._btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(120)))), ((int)(((byte)(60)))));
            this._btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnAdd.ForeColor = System.Drawing.Color.White;
            this._btnAdd.Location = new System.Drawing.Point(220, 4);
            this._btnAdd.Name = "_btnAdd";
            this._btnAdd.Size = new System.Drawing.Size(68, 25);
            this._btnAdd.TabIndex = 2;
            this._btnAdd.Text = "+ Frame";
            this._toolTip.SetToolTip(this._btnAdd, "Aggiungi un singolo frame vuoto");
            this._btnAdd.UseVisualStyleBackColor = false;
            this._btnAdd.Click += new System.EventHandler(this.ActionAddSprite_Click);
            // 
            // _btnAddMultiple
            // 
            this._btnAddMultiple.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this._btnAddMultiple.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnAddMultiple.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._btnAddMultiple.ForeColor = System.Drawing.Color.White;
            this._btnAddMultiple.Location = new System.Drawing.Point(65, 4);
            this._btnAddMultiple.Name = "_btnAddMultiple";
            this._btnAddMultiple.Size = new System.Drawing.Size(150, 25);
            this._btnAddMultiple.TabIndex = 1;
            this._btnAddMultiple.Text = "➕ Aggiungi Sprite";
            this._toolTip.SetToolTip(this._btnAddMultiple, "Aggiungi nuovi sprite di nemici (preset, frame vuoti o dati assembly)");
            this._btnAddMultiple.UseVisualStyleBackColor = false;
            this._btnAddMultiple.Click += new System.EventHandler(this.ActionAddEnemies_Click);
            // 
            // _lblStripTitle
            // 
            this._lblStripTitle.AutoSize = true;
            this._lblStripTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._lblStripTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this._lblStripTitle.Location = new System.Drawing.Point(8, 8);
            this._lblStripTitle.Name = "_lblStripTitle";
            this._lblStripTitle.Size = new System.Drawing.Size(70, 23);
            this._lblStripTitle.TabIndex = 0;
            this._lblStripTitle.Text = "SPRITE:";
            // 
            // _rightPanel
            // 
            this._rightPanel.AutoScroll = true;
            this._rightPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this._rightPanel.Controls.Add(this._chkFlash);
            this._rightPanel.Controls.Add(this._btnCopyQuick);
            this._rightPanel.Controls.Add(this._txtQuickAsm);
            this._rightPanel.Controls.Add(this._lblQuickAsmTitle);
            this._rightPanel.Controls.Add(this._lblFps);
            this._rightPanel.Controls.Add(this._tbAnimFps);
            this._rightPanel.Controls.Add(this._lblSpeed);
            this._rightPanel.Controls.Add(this._lblAnimFrame);
            this._rightPanel.Controls.Add(this._btnPlayAnim);
            this._rightPanel.Controls.Add(this._cmbAnimScope);
            this._rightPanel.Controls.Add(this._lblScope);
            this._rightPanel.Controls.Add(this._lblAnimTitle);
            this._rightPanel.Controls.Add(this._prevBox);
            this._rightPanel.Controls.Add(this._lblPrevTitle);
            this._rightPanel.Controls.Add(this._chkBright);
            this._rightPanel.Controls.Add(this._cmbPaper);
            this._rightPanel.Controls.Add(this._lblPaper);
            this._rightPanel.Controls.Add(this._cmbInk);
            this._rightPanel.Controls.Add(this._lblInk);
            this._rightPanel.Controls.Add(this._lblColorsTitle);
            this._rightPanel.Controls.Add(this._txtCurrentSpriteName);
            this._rightPanel.Controls.Add(this._lblSpriteNameTitle);
            this._rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this._rightPanel.Location = new System.Drawing.Point(1015, 42);
            this._rightPanel.Name = "_rightPanel";
            this._rightPanel.Padding = new System.Windows.Forms.Padding(10);
            this._rightPanel.Size = new System.Drawing.Size(280, 747);
            this._rightPanel.TabIndex = 1;
            // 
            // _btnCopyQuick
            // 
            this._btnCopyQuick.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this._btnCopyQuick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnCopyQuick.ForeColor = System.Drawing.Color.White;
            this._btnCopyQuick.Location = new System.Drawing.Point(10, 589);
            this._btnCopyQuick.Name = "_btnCopyQuick";
            this._btnCopyQuick.Size = new System.Drawing.Size(240, 28);
            this._btnCopyQuick.TabIndex = 20;
            this._btnCopyQuick.Text = "Copia Assembly Sprite";
            this._btnCopyQuick.UseVisualStyleBackColor = false;
            this._btnCopyQuick.Click += new System.EventHandler(this.ActionQuickCopyAsm_Click);
            // 
            // _txtQuickAsm
            // 
            this._txtQuickAsm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(24)))));
            this._txtQuickAsm.Font = new System.Drawing.Font("Consolas", 8F);
            this._txtQuickAsm.ForeColor = System.Drawing.Color.LightGreen;
            this._txtQuickAsm.Location = new System.Drawing.Point(10, 497);
            this._txtQuickAsm.Multiline = true;
            this._txtQuickAsm.Name = "_txtQuickAsm";
            this._txtQuickAsm.ReadOnly = true;
            this._txtQuickAsm.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._txtQuickAsm.Size = new System.Drawing.Size(240, 85);
            this._txtQuickAsm.TabIndex = 19;
            // 
            // _lblQuickAsmTitle
            // 
            this._lblQuickAsmTitle.AutoSize = true;
            this._lblQuickAsmTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._lblQuickAsmTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this._lblQuickAsmTitle.Location = new System.Drawing.Point(10, 473);
            this._lblQuickAsmTitle.Name = "_lblQuickAsmTitle";
            this._lblQuickAsmTitle.Size = new System.Drawing.Size(219, 23);
            this._lblQuickAsmTitle.TabIndex = 18;
            this._lblQuickAsmTitle.Text = "DATI ASSEMBLY DEFB/DB";
            // 
            // _lblFps
            // 
            this._lblFps.AutoSize = true;
            this._lblFps.ForeColor = System.Drawing.Color.White;
            this._lblFps.Location = new System.Drawing.Point(210, 438);
            this._lblFps.Name = "_lblFps";
            this._lblFps.Size = new System.Drawing.Size(52, 25);
            this._lblFps.TabIndex = 17;
            this._lblFps.Text = "4 fps";
            // 
            // _tbAnimFps
            // 
            this._tbAnimFps.Location = new System.Drawing.Point(95, 435);
            this._tbAnimFps.Maximum = 20;
            this._tbAnimFps.Minimum = 1;
            this._tbAnimFps.Name = "_tbAnimFps";
            this._tbAnimFps.Size = new System.Drawing.Size(110, 69);
            this._tbAnimFps.TabIndex = 16;
            this._tbAnimFps.TickStyle = System.Windows.Forms.TickStyle.None;
            this._tbAnimFps.Value = 4;
            this._tbAnimFps.ValueChanged += new System.EventHandler(this.TbAnimFps_ValueChanged);
            // 
            // _lblSpeed
            // 
            this._lblSpeed.AutoSize = true;
            this._lblSpeed.ForeColor = System.Drawing.Color.LightGray;
            this._lblSpeed.Location = new System.Drawing.Point(10, 438);
            this._lblSpeed.Name = "_lblSpeed";
            this._lblSpeed.Size = new System.Drawing.Size(111, 25);
            this._lblSpeed.TabIndex = 15;
            this._lblSpeed.Text = "Velocità FPS:";
            // 
            // _lblAnimFrame
            // 
            this._lblAnimFrame.AutoSize = true;
            this._lblAnimFrame.ForeColor = System.Drawing.Color.Yellow;
            this._lblAnimFrame.Location = new System.Drawing.Point(158, 406);
            this._lblAnimFrame.Name = "_lblAnimFrame";
            this._lblAnimFrame.Size = new System.Drawing.Size(97, 25);
            this._lblAnimFrame.TabIndex = 14;
            this._lblAnimFrame.Text = "Frame: 1/4";
            // 
            // _btnPlayAnim
            // 
            this._btnPlayAnim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(130)))), ((int)(((byte)(80)))));
            this._btnPlayAnim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnPlayAnim.ForeColor = System.Drawing.Color.White;
            this._btnPlayAnim.Location = new System.Drawing.Point(10, 401);
            this._btnPlayAnim.Name = "_btnPlayAnim";
            this._btnPlayAnim.Size = new System.Drawing.Size(140, 28);
            this._btnPlayAnim.TabIndex = 13;
            this._btnPlayAnim.Text = "▶ Avvia Animazione";
            this._btnPlayAnim.UseVisualStyleBackColor = false;
            this._btnPlayAnim.Click += new System.EventHandler(this.BtnPlayAnim_Click);
            // 
            // _cmbAnimScope
            // 
            this._cmbAnimScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbAnimScope.FormattingEnabled = true;
            this._cmbAnimScope.Items.AddRange(new object[] {
            "Sprite Selezionato (solo questo set)",
            "Tutti i frame del progetto"});
            this._cmbAnimScope.Location = new System.Drawing.Point(60, 371);
            this._cmbAnimScope.Name = "_cmbAnimScope";
            this._cmbAnimScope.Size = new System.Drawing.Size(190, 33);
            this._cmbAnimScope.TabIndex = 12;
            this._cmbAnimScope.SelectedIndexChanged += new System.EventHandler(this.CmbAnimScope_SelectedIndexChanged);
            // 
            // _lblScope
            // 
            this._lblScope.AutoSize = true;
            this._lblScope.ForeColor = System.Drawing.Color.LightGray;
            this._lblScope.Location = new System.Drawing.Point(10, 374);
            this._lblScope.Name = "_lblScope";
            this._lblScope.Size = new System.Drawing.Size(64, 25);
            this._lblScope.TabIndex = 11;
            this._lblScope.Text = "Target:";
            // 
            // _lblAnimTitle
            // 
            this._lblAnimTitle.AutoSize = true;
            this._lblAnimTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._lblAnimTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this._lblAnimTitle.Location = new System.Drawing.Point(10, 349);
            this._lblAnimTitle.Name = "_lblAnimTitle";
            this._lblAnimTitle.Size = new System.Drawing.Size(183, 23);
            this._lblAnimTitle.TabIndex = 10;
            this._lblAnimTitle.Text = "PLAYER ANIMAZIONE";
            // 
            // _prevBox
            // 
            this._prevBox.BackColor = System.Drawing.Color.Black;
            this._prevBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._prevBox.Controls.Add(this._picPreview4x);
            this._prevBox.Controls.Add(this._lbl4x);
            this._prevBox.Controls.Add(this._picPreview1x);
            this._prevBox.Controls.Add(this._lbl1x);
            this._prevBox.Location = new System.Drawing.Point(10, 239);
            this._prevBox.Name = "_prevBox";
            this._prevBox.Size = new System.Drawing.Size(240, 100);
            this._prevBox.TabIndex = 9;
            // 
            // _picPreview4x
            // 
            this._picPreview4x.BackColor = System.Drawing.Color.Black;
            this._picPreview4x.Location = new System.Drawing.Point(110, 26);
            this._picPreview4x.Name = "_picPreview4x";
            this._picPreview4x.Size = new System.Drawing.Size(64, 64);
            this._picPreview4x.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._picPreview4x.TabIndex = 3;
            this._picPreview4x.TabStop = false;
            // 
            // _lbl4x
            // 
            this._lbl4x.AutoSize = true;
            this._lbl4x.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._lbl4x.ForeColor = System.Drawing.Color.Gray;
            this._lbl4x.Location = new System.Drawing.Point(110, 8);
            this._lbl4x.Name = "_lbl4x";
            this._lbl4x.Size = new System.Drawing.Size(120, 20);
            this._lbl4x.TabIndex = 2;
            this._lbl4x.Text = "Scala 4x (Zoom):";
            // 
            // _picPreview1x
            // 
            this._picPreview1x.BackColor = System.Drawing.Color.Black;
            this._picPreview1x.Location = new System.Drawing.Point(10, 26);
            this._picPreview1x.Name = "_picPreview1x";
            this._picPreview1x.Size = new System.Drawing.Size(32, 32);
            this._picPreview1x.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._picPreview1x.TabIndex = 1;
            this._picPreview1x.TabStop = false;
            // 
            // _lbl1x
            // 
            this._lbl1x.AutoSize = true;
            this._lbl1x.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this._lbl1x.ForeColor = System.Drawing.Color.Gray;
            this._lbl1x.Location = new System.Drawing.Point(10, 8);
            this._lbl1x.Name = "_lbl1x";
            this._lbl1x.Size = new System.Drawing.Size(117, 20);
            this._lbl1x.TabIndex = 0;
            this._lbl1x.Text = "Scala 1x (Reale):";
            // 
            // _lblPrevTitle
            // 
            this._lblPrevTitle.AutoSize = true;
            this._lblPrevTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._lblPrevTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this._lblPrevTitle.Location = new System.Drawing.Point(10, 215);
            this._lblPrevTitle.Name = "_lblPrevTitle";
            this._lblPrevTitle.Size = new System.Drawing.Size(196, 23);
            this._lblPrevTitle.TabIndex = 8;
            this._lblPrevTitle.Text = "ANTEPRIMA SCHERMO";
            // 
            // _chkBright
            // 
            this._chkBright.AutoSize = true;
            this._chkBright.Checked = true;
            this._chkBright.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkBright.ForeColor = System.Drawing.Color.White;
            this._chkBright.Location = new System.Drawing.Point(10, 142);
            this._chkBright.Name = "_chkBright";
            this._chkBright.Size = new System.Drawing.Size(222, 29);
            this._chkBright.TabIndex = 7;
            this._chkBright.Text = "Luminosità Alta (Bright)";
            this._chkBright.UseVisualStyleBackColor = true;
            this._chkBright.CheckedChanged += new System.EventHandler(this.ChkBright_CheckedChanged);
            // 
            // _cmbPaper
            // 
            this._cmbPaper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbPaper.FormattingEnabled = true;
            this._cmbPaper.Location = new System.Drawing.Point(105, 114);
            this._cmbPaper.Name = "_cmbPaper";
            this._cmbPaper.Size = new System.Drawing.Size(145, 33);
            this._cmbPaper.TabIndex = 6;
            this._cmbPaper.SelectedIndexChanged += new System.EventHandler(this.CmbPaper_SelectedIndexChanged);
            // 
            // _lblPaper
            // 
            this._lblPaper.AutoSize = true;
            this._lblPaper.ForeColor = System.Drawing.Color.LightGray;
            this._lblPaper.Location = new System.Drawing.Point(10, 117);
            this._lblPaper.Name = "_lblPaper";
            this._lblPaper.Size = new System.Drawing.Size(134, 25);
            this._lblPaper.TabIndex = 5;
            this._lblPaper.Text = "Paper (Sfondo):";
            // 
            // _cmbInk
            // 
            this._cmbInk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbInk.FormattingEnabled = true;
            this._cmbInk.Location = new System.Drawing.Point(105, 86);
            this._cmbInk.Name = "_cmbInk";
            this._cmbInk.Size = new System.Drawing.Size(145, 33);
            this._cmbInk.TabIndex = 4;
            this._cmbInk.SelectedIndexChanged += new System.EventHandler(this.CmbInk_SelectedIndexChanged);
            // 
            // _lblInk
            // 
            this._lblInk.AutoSize = true;
            this._lblInk.ForeColor = System.Drawing.Color.LightGray;
            this._lblInk.Location = new System.Drawing.Point(10, 89);
            this._lblInk.Name = "_lblInk";
            this._lblInk.Size = new System.Drawing.Size(100, 25);
            this._lblInk.TabIndex = 3;
            this._lblInk.Text = "Ink (Tratto):";
            // 
            // _lblColorsTitle
            // 
            this._lblColorsTitle.AutoSize = true;
            this._lblColorsTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._lblColorsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this._lblColorsTitle.Location = new System.Drawing.Point(10, 62);
            this._lblColorsTitle.Name = "_lblColorsTitle";
            this._lblColorsTitle.Size = new System.Drawing.Size(192, 23);
            this._lblColorsTitle.TabIndex = 2;
            this._lblColorsTitle.Text = "COLORI ZX SPECTRUM";
            // 
            // _txtCurrentSpriteName
            // 
            this._txtCurrentSpriteName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(24)))));
            this._txtCurrentSpriteName.ForeColor = System.Drawing.Color.White;
            this._txtCurrentSpriteName.Location = new System.Drawing.Point(10, 32);
            this._txtCurrentSpriteName.Name = "_txtCurrentSpriteName";
            this._txtCurrentSpriteName.Size = new System.Drawing.Size(240, 31);
            this._txtCurrentSpriteName.TabIndex = 1;
            this._txtCurrentSpriteName.TextChanged += new System.EventHandler(this.TxtCurrentSpriteName_TextChanged);
            // 
            // _lblSpriteNameTitle
            // 
            this._lblSpriteNameTitle.AutoSize = true;
            this._lblSpriteNameTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._lblSpriteNameTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this._lblSpriteNameTitle.Location = new System.Drawing.Point(10, 10);
            this._lblSpriteNameTitle.Name = "_lblSpriteNameTitle";
            this._lblSpriteNameTitle.Size = new System.Drawing.Size(237, 23);
            this._lblSpriteNameTitle.TabIndex = 0;
            this._lblSpriteNameTitle.Text = "NOME SPRITE SELEZIONATO";
            // 
            // _mainToolbar
            // 
            this._mainToolbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(42)))));
            this._mainToolbar.ForeColor = System.Drawing.Color.White;
            this._mainToolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._mainToolbar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._mainToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btnNew,
            this._btnOpen,
            this._btnSave,
            this._sepToolbar1,
            this._btnToolPencil,
            this._btnToolLine,
            this._btnToolRect,
            this._btnToolRectFill,
            this._btnToolCircle,
            this._btnToolCircleFill,
            this._btnToolFill,
            this._sepToolbar2,
            this._btnUndo,
            this._btnRedo,
            this._sepToolbar3,
            this._btnFlipH,
            this._btnFlipV,
            this._btnRot,
            this._btnInv,
            this._btnClear,
            this._sepToolbar4,
            this._btnLeft,
            this._btnUp,
            this._btnDown,
            this._btnRight,
            this._sepToolbar5,
            this._btnToggleGrid,
            this._btnToggleCharGrid,
            this._sepToolbar6,
            this._btnZoomIn,
            this._btnZoomOut,
            this._sepToolbar7,
            this._btnQuickExport,
            this._sepToolbar8,
            this._btnGoToTiles,
            this._btnGoToMaps});
            this._mainToolbar.Location = new System.Drawing.Point(0, 0);
            this._mainToolbar.Name = "_mainToolbar";
            this._mainToolbar.Padding = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this._mainToolbar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this._mainToolbar.Size = new System.Drawing.Size(1295, 42);
            this._mainToolbar.TabIndex = 0;
            // 
            // _btnNew
            // 
            this._btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnNew.Name = "_btnNew";
            this._btnNew.Size = new System.Drawing.Size(100, 29);
            this._btnNew.Text = "📄 Nuovo";
            this._btnNew.Click += new System.EventHandler(this.ActionNewProject_Click);
            // 
            // _btnOpen
            // 
            this._btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnOpen.Name = "_btnOpen";
            this._btnOpen.Size = new System.Drawing.Size(154, 29);
            this._btnOpen.Text = "📁 Apri Progetto";
            this._btnOpen.Click += new System.EventHandler(this.ActionOpenProject_Click);
            // 
            // _btnSave
            // 
            this._btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnSave.Name = "_btnSave";
            this._btnSave.Size = new System.Drawing.Size(162, 29);
            this._btnSave.Text = "💾 Salva Progetto";
            this._btnSave.Click += new System.EventHandler(this.ActionSaveProject_Click);
            // 
            // _sepToolbar1
            // 
            this._sepToolbar1.Name = "_sepToolbar1";
            this._sepToolbar1.Size = new System.Drawing.Size(6, 34);
            // 
            // _btnToolPencil
            // 
            this._btnToolPencil.Checked = true;
            this._btnToolPencil.CheckState = System.Windows.Forms.CheckState.Checked;
            this._btnToolPencil.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolPencil.Name = "_btnToolPencil";
            this._btnToolPencil.Size = new System.Drawing.Size(66, 29);
            this._btnToolPencil.Text = "Matita";
            this._btnToolPencil.Click += new System.EventHandler(this.BtnToolPencil_Click);
            // 
            // _btnToolLine
            // 
            this._btnToolLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolLine.Name = "_btnToolLine";
            this._btnToolLine.Size = new System.Drawing.Size(56, 29);
            this._btnToolLine.Text = "Linea";
            this._btnToolLine.Click += new System.EventHandler(this.BtnToolLine_Click);
            // 
            // _btnToolRect
            // 
            this._btnToolRect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolRect.Name = "_btnToolRect";
            this._btnToolRect.Size = new System.Drawing.Size(103, 29);
            this._btnToolRect.Text = "Rettangolo";
            this._btnToolRect.Click += new System.EventHandler(this.BtnToolRect_Click);
            // 
            // _btnToolRectFill
            // 
            this._btnToolRectFill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolRectFill.Name = "_btnToolRectFill";
            this._btnToolRectFill.Size = new System.Drawing.Size(100, 29);
            this._btnToolRectFill.Text = "Rett. Pieno";
            this._btnToolRectFill.Click += new System.EventHandler(this.BtnToolRectFill_Click);
            // 
            // _btnToolCircle
            // 
            this._btnToolCircle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolCircle.Name = "_btnToolCircle";
            this._btnToolCircle.Size = new System.Drawing.Size(75, 29);
            this._btnToolCircle.Text = "Cerchio";
            this._btnToolCircle.Click += new System.EventHandler(this.BtnToolCircle_Click);
            // 
            // _btnToolCircleFill
            // 
            this._btnToolCircleFill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolCircleFill.Name = "_btnToolCircleFill";
            this._btnToolCircleFill.Size = new System.Drawing.Size(124, 29);
            this._btnToolCircleFill.Text = "Cerchio Pieno";
            this._btnToolCircleFill.Click += new System.EventHandler(this.BtnToolCircleFill_Click);
            // 
            // _btnToolFill
            // 
            this._btnToolFill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToolFill.Name = "_btnToolFill";
            this._btnToolFill.Size = new System.Drawing.Size(93, 29);
            this._btnToolFill.Text = "Secchiello";
            this._btnToolFill.Click += new System.EventHandler(this.BtnToolFill_Click);
            // 
            // _sepToolbar2
            // 
            this._sepToolbar2.Name = "_sepToolbar2";
            this._sepToolbar2.Size = new System.Drawing.Size(6, 34);
            // 
            // _btnUndo
            // 
            this._btnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnUndo.Name = "_btnUndo";
            this._btnUndo.Size = new System.Drawing.Size(95, 29);
            this._btnUndo.Text = "↶ Annulla";
            this._btnUndo.Click += new System.EventHandler(this.ActionUndo_Click);
            // 
            // _btnRedo
            // 
            this._btnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnRedo.Name = "_btnRedo";
            this._btnRedo.Size = new System.Drawing.Size(81, 29);
            this._btnRedo.Text = "↷ Ripeti";
            this._btnRedo.Click += new System.EventHandler(this.ActionRedo_Click);
            // 
            // _sepToolbar3
            // 
            this._sepToolbar3.Name = "_sepToolbar3";
            this._sepToolbar3.Size = new System.Drawing.Size(6, 34);
            // 
            // _btnFlipH
            // 
            this._btnFlipH.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnFlipH.Name = "_btnFlipH";
            this._btnFlipH.Size = new System.Drawing.Size(62, 29);
            this._btnFlipH.Text = "Flip H";
            this._btnFlipH.Click += new System.EventHandler(this.ActionFlipH_Click);
            // 
            // _btnFlipV
            // 
            this._btnFlipV.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnFlipV.Name = "_btnFlipV";
            this._btnFlipV.Size = new System.Drawing.Size(60, 29);
            this._btnFlipV.Text = "Flip V";
            this._btnFlipV.Click += new System.EventHandler(this.ActionFlipV_Click);
            // 
            // _btnRot
            // 
            this._btnRot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnRot.Name = "_btnRot";
            this._btnRot.Size = new System.Drawing.Size(95, 29);
            this._btnRot.Text = "Ruota 90°";
            this._btnRot.Click += new System.EventHandler(this.ActionRotateCW_Click);
            // 
            // _btnInv
            // 
            this._btnInv.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnInv.Name = "_btnInv";
            this._btnInv.Size = new System.Drawing.Size(65, 29);
            this._btnInv.Text = "Inverti";
            this._btnInv.Click += new System.EventHandler(this.ActionInvert_Click);
            // 
            // _btnClear
            // 
            this._btnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnClear.Name = "_btnClear";
            this._btnClear.Size = new System.Drawing.Size(64, 29);
            this._btnClear.Text = "Pulisci";
            this._btnClear.Click += new System.EventHandler(this.ActionClear_Click);
            // 
            // _sepToolbar4
            // 
            this._sepToolbar4.Name = "_sepToolbar4";
            this._sepToolbar4.Size = new System.Drawing.Size(6, 25);
            // 
            // _btnLeft
            // 
            this._btnLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnLeft.Name = "_btnLeft";
            this._btnLeft.Size = new System.Drawing.Size(34, 29);
            this._btnLeft.Text = "◄";
            this._btnLeft.Click += new System.EventHandler(this.ActionShiftLeft_Click);
            // 
            // _btnUp
            // 
            this._btnUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnUp.Name = "_btnUp";
            this._btnUp.Size = new System.Drawing.Size(34, 29);
            this._btnUp.Text = "▲";
            this._btnUp.Click += new System.EventHandler(this.ActionShiftUp_Click);
            // 
            // _btnDown
            // 
            this._btnDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnDown.Name = "_btnDown";
            this._btnDown.Size = new System.Drawing.Size(34, 29);
            this._btnDown.Text = "▼";
            this._btnDown.Click += new System.EventHandler(this.ActionShiftDown_Click);
            // 
            // _btnRight
            // 
            this._btnRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnRight.Name = "_btnRight";
            this._btnRight.Size = new System.Drawing.Size(34, 29);
            this._btnRight.Text = "►";
            this._btnRight.Click += new System.EventHandler(this.ActionShiftRight_Click);
            // 
            // _sepToolbar5
            // 
            this._sepToolbar5.Name = "_sepToolbar5";
            this._sepToolbar5.Size = new System.Drawing.Size(6, 25);
            // 
            // _btnToggleGrid
            // 
            this._btnToggleGrid.Checked = true;
            this._btnToggleGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this._btnToggleGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToggleGrid.Name = "_btnToggleGrid";
            this._btnToggleGrid.Size = new System.Drawing.Size(106, 29);
            this._btnToggleGrid.Text = "Griglia Pixel";
            this._btnToggleGrid.Click += new System.EventHandler(this.BtnToggleGrid_Click);
            // 
            // _btnToggleCharGrid
            // 
            this._btnToggleCharGrid.Checked = true;
            this._btnToggleCharGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this._btnToggleCharGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnToggleCharGrid.Name = "_btnToggleCharGrid";
            this._btnToggleCharGrid.Size = new System.Drawing.Size(112, 29);
            this._btnToggleCharGrid.Text = "Celle 8x8 ZX";
            this._btnToggleCharGrid.Click += new System.EventHandler(this.BtnToggleCharGrid_Click);
            // 
            // _sepToolbar6
            // 
            this._sepToolbar6.Name = "_sepToolbar6";
            this._sepToolbar6.Size = new System.Drawing.Size(6, 25);
            // 
            // _btnZoomIn
            // 
            this._btnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnZoomIn.Name = "_btnZoomIn";
            this._btnZoomIn.Size = new System.Drawing.Size(81, 29);
            this._btnZoomIn.Text = "Zoom +";
            this._btnZoomIn.Click += new System.EventHandler(this.BtnZoomIn_Click);
            // 
            // _btnZoomOut
            // 
            this._btnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnZoomOut.Name = "_btnZoomOut";
            this._btnZoomOut.Size = new System.Drawing.Size(76, 29);
            this._btnZoomOut.Text = "Zoom -";
            this._btnZoomOut.Click += new System.EventHandler(this.BtnZoomOut_Click);
            // 
            // _sepToolbar7
            // 
            this._sepToolbar7.Name = "_sepToolbar7";
            this._sepToolbar7.Size = new System.Drawing.Size(6, 25);
            // 
            // _btnQuickExport
            // 
            this._btnQuickExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this._btnQuickExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnQuickExport.ForeColor = System.Drawing.Color.White;
            this._btnQuickExport.Name = "_btnQuickExport";
            this._btnQuickExport.Size = new System.Drawing.Size(143, 29);
            this._btnQuickExport.Text = "⚡ Esporta Dati";
            this._btnQuickExport.Click += new System.EventHandler(this.ActionExport_Click);
            // 
            // _sepToolbar8
            // 
            this._sepToolbar8.Name = "_sepToolbar8";
            this._sepToolbar8.Size = new System.Drawing.Size(6, 25);
            // 
            // _btnGoToTiles
            // 
            this._btnGoToTiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(90)))), ((int)(((byte)(140)))));
            this._btnGoToTiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnGoToTiles.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._btnGoToTiles.ForeColor = System.Drawing.Color.White;
            this._btnGoToTiles.Name = "_btnGoToTiles";
            this._btnGoToTiles.Size = new System.Drawing.Size(169, 27);
            this._btnGoToTiles.Text = "🧱 Editor Tiles (F3)";
            this._btnGoToTiles.ToolTipText = "Passa all\'Editor dei Tile 8x8 (Tasto rapido: F3)";
            this._btnGoToTiles.Click += new System.EventHandler(this.SwitchToTileEditor_Click);
            // 
            // _btnGoToMaps
            // 
            this._btnGoToMaps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(180)))));
            this._btnGoToMaps.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._btnGoToMaps.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this._btnGoToMaps.ForeColor = System.Drawing.Color.White;
            this._btnGoToMaps.Name = "_btnGoToMaps";
            this._btnGoToMaps.Size = new System.Drawing.Size(189, 27);
            this._btnGoToMaps.Text = "🗺️ Editor Mappe (F4)";
            this._btnGoToMaps.ToolTipText = "Passa all\'Editor Mappe di Gioco (Tasto rapido: F4)";
            this._btnGoToMaps.Click += new System.EventHandler(this.SwitchToMapEditor_Click);
            // 
            // _tabTiles
            // 
            this._tabTiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(24)))));
            this._tabTiles.Controls.Add(this._tileEditorView);
            this._tabTiles.Location = new System.Drawing.Point(4, 36);
            this._tabTiles.Name = "_tabTiles";
            this._tabTiles.Size = new System.Drawing.Size(1295, 792);
            this._tabTiles.TabIndex = 1;
            this._tabTiles.Text = "🧱 Editor Tileset (F3)";
            // 
            // _tabMaps
            // 
            this._tabMaps.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(24)))));
            this._tabMaps.Controls.Add(this._mapEditorView);
            this._tabMaps.Location = new System.Drawing.Point(4, 36);
            this._tabMaps.Name = "_tabMaps";
            this._tabMaps.Size = new System.Drawing.Size(1295, 792);
            this._tabMaps.TabIndex = 2;
            this._tabMaps.Text = "🗺️ Editor Mappe di Gioco";
            // 
            // _animTimer
            // 
            this._animTimer.Interval = 250;
            this._animTimer.Tick += new System.EventHandler(this.AnimTimer_Tick);
            // 
            // _chkFlash
            // 
            this._chkFlash.AutoSize = true;
            this._chkFlash.ForeColor = System.Drawing.Color.White;
            this._chkFlash.Location = new System.Drawing.Point(10, 177);
            this._chkFlash.Name = "_chkFlash";
            this._chkFlash.Size = new System.Drawing.Size(78, 29);
            this._chkFlash.TabIndex = 21;
            this._chkFlash.Text = "Flash";
            this._chkFlash.UseVisualStyleBackColor = true;
            this._chkFlash.CheckedChanged += new System.EventHandler(this.ChkFlash_CheckedChanged);
            // 
            // _canvas
            // 
            this._canvas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(20)))));
            this._canvas.CellSize = 24;
            this._canvas.CurrentSprite = null;
            this._canvas.CurrentTool = ZXSpectrumSpriteGenerator.UI.Controls.DrawingTool.Pencil;
            this._canvas.Cursor = System.Windows.Forms.Cursors.Cross;
            this._canvas.Location = new System.Drawing.Point(15, 15);
            this._canvas.Name = "_canvas";
            this._canvas.ShowCharGrid = true;
            this._canvas.ShowPixelGrid = true;
            this._canvas.Size = new System.Drawing.Size(384, 384);
            this._canvas.TabIndex = 0;
            this._canvas.HoverCoordinatesChanged += new System.EventHandler<ZXSpectrumSpriteGenerator.UI.Controls.HoverCoordinateEventArgs>(this.Canvas_HoverCoordinatesChanged);
            this._canvas.SpriteChanged += new System.EventHandler(this.Canvas_SpriteChanged);
            // 
            // _filmstrip
            // 
            this._filmstrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(28)))));
            this._filmstrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this._filmstrip.IsTilesMode = false;
            this._filmstrip.Location = new System.Drawing.Point(0, 34);
            this._filmstrip.Name = "_filmstrip";
            this._filmstrip.ScrollOffsetX = 0;
            this._filmstrip.Size = new System.Drawing.Size(1015, 108);
            this._filmstrip.TabIndex = 2;
            this._filmstrip.SelectedIndexChanged += new System.EventHandler(this.Filmstrip_SelectedIndexChanged);
            this._filmstrip.ScrollChanged += new System.EventHandler(this.Filmstrip_ScrollChanged);
            this._filmstrip.Resize += new System.EventHandler(this.Filmstrip_Resize);
            // 
            // _tileEditorView
            // 
            this._tileEditorView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(24)))));
            this._tileEditorView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tileEditorView.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._tileEditorView.ForeColor = System.Drawing.Color.White;
            this._tileEditorView.Location = new System.Drawing.Point(0, 0);
            this._tileEditorView.Name = "_tileEditorView";
            this._tileEditorView.Size = new System.Drawing.Size(1295, 792);
            this._tileEditorView.TabIndex = 0;
            this._tileEditorView.ProjectModified += new System.EventHandler(this.TileEditorView_ProjectModified);
            this._tileEditorView.RequestSwitchToMaps += new System.EventHandler(this.TileEditorView_RequestSwitchToMaps);
            this._tileEditorView.RequestSwitchToSprites += new System.EventHandler(this.TileEditorView_RequestSwitchToSprites);
            // 
            // _mapEditorView
            // 
            this._mapEditorView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(24)))));
            this._mapEditorView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mapEditorView.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._mapEditorView.ForeColor = System.Drawing.Color.White;
            this._mapEditorView.Location = new System.Drawing.Point(0, 0);
            this._mapEditorView.Name = "_mapEditorView";
            this._mapEditorView.Size = new System.Drawing.Size(1295, 792);
            this._mapEditorView.TabIndex = 0;
            this._mapEditorView.ProjectModified += new System.EventHandler(this.MapEditorView_ProjectModified);
            this._mapEditorView.RequestSwitchToSprites += new System.EventHandler(this.MapEditorView_RequestSwitchToSprites);
            this._mapEditorView.RequestSwitchToTiles += new System.EventHandler<int>(this.MapEditorView_RequestSwitchToTiles);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(1303, 897);
            this.Controls.Add(this._mainTabs);
            this.Controls.Add(this._statusStrip);
            this.Controls.Add(this._menuStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.White;
            this.KeyPreview = true;
            this.MainMenuStrip = this._menuStrip;
            this.MinimumSize = new System.Drawing.Size(950, 650);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZX Spectrum Sprite Generator & Studio";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();
            this._statusStrip.ResumeLayout(false);
            this._statusStrip.PerformLayout();
            this._mainTabs.ResumeLayout(false);
            this._tabSprites.ResumeLayout(false);
            this._tabSprites.PerformLayout();
            this._centerPanel.ResumeLayout(false);
            this._bottomPanel.ResumeLayout(false);
            this._filmstripNavBar.ResumeLayout(false);
            this._filmstripNavBar.PerformLayout();
            this._filmstripControlBar.ResumeLayout(false);
            this._filmstripControlBar.PerformLayout();
            this._rightPanel.ResumeLayout(false);
            this._rightPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._tbAnimFps)).EndInit();
            this._prevBox.ResumeLayout(false);
            this._prevBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._picPreview4x)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._picPreview1x)).EndInit();
            this._mainToolbar.ResumeLayout(false);
            this._mainToolbar.PerformLayout();
            this._tabTiles.ResumeLayout(false);
            this._tabMaps.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _menuStrip;
        private System.Windows.Forms.ToolStripMenuItem _mFile;
        private System.Windows.Forms.ToolStripMenuItem _mNew;
        private System.Windows.Forms.ToolStripMenuItem _mOpen;
        private System.Windows.Forms.ToolStripMenuItem _mSave;
        private System.Windows.Forms.ToolStripMenuItem _mSaveAs;
        private System.Windows.Forms.ToolStripSeparator _mFileSep1;
        private System.Windows.Forms.ToolStripMenuItem _mOpenBin;
        private System.Windows.Forms.ToolStripMenuItem _mExport;
        private System.Windows.Forms.ToolStripMenuItem _mExportPng;
        private System.Windows.Forms.ToolStripSeparator _mFileSep2;
        private System.Windows.Forms.ToolStripMenuItem _mExit;
        private System.Windows.Forms.ToolStripMenuItem _mEdit;
        private System.Windows.Forms.ToolStripMenuItem _mUndo;
        private System.Windows.Forms.ToolStripMenuItem _mRedo;
        private System.Windows.Forms.ToolStripSeparator _mEditSep1;
        private System.Windows.Forms.ToolStripMenuItem _mSpriteClear;
        private System.Windows.Forms.ToolStripMenuItem _mSpriteInvert;
        private System.Windows.Forms.ToolStripSeparator _mEditSep2;
        private System.Windows.Forms.ToolStripMenuItem _mSpriteAddEnemies;
        private System.Windows.Forms.ToolStripMenuItem _mSpritePasteNemico;
        private System.Windows.Forms.ToolStripSeparator _mEditSep3;
        private System.Windows.Forms.ToolStripMenuItem _mCopyAsm;
        private System.Windows.Forms.ToolStripMenuItem _mView;
        private System.Windows.Forms.ToolStripMenuItem _mViewSprites;
        private System.Windows.Forms.ToolStripMenuItem _mViewMaps;
        private System.Windows.Forms.ToolStripMenuItem _mTrans;
        private System.Windows.Forms.ToolStripMenuItem _mFlipH;
        private System.Windows.Forms.ToolStripMenuItem _mFlipV;
        private System.Windows.Forms.ToolStripSeparator _mTransSep1;
        private System.Windows.Forms.ToolStripMenuItem _mRotCW;
        private System.Windows.Forms.ToolStripMenuItem _mRotCCW;
        private System.Windows.Forms.ToolStripSeparator _mTransSep2;
        private System.Windows.Forms.ToolStripMenuItem _mShiftUp;
        private System.Windows.Forms.ToolStripMenuItem _mShiftDown;
        private System.Windows.Forms.ToolStripMenuItem _mShiftLeft;
        private System.Windows.Forms.ToolStripMenuItem _mShiftRight;
        private System.Windows.Forms.ToolStripMenuItem _mPreset;
        private System.Windows.Forms.ToolStripMenuItem _mAddEnemiesPreset;
        private System.Windows.Forms.ToolStripMenuItem _mSubPresets;
        private System.Windows.Forms.ToolStripSeparator _mPresetSep1;
        private System.Windows.Forms.ToolStripMenuItem _mPresetEnemy;
        private System.Windows.Forms.ToolStripMenuItem _mPresetWalker;
        private System.Windows.Forms.ToolStripMenuItem _mMaps;
        private System.Windows.Forms.ToolStripMenuItem _mBackToSprites;
        private System.Windows.Forms.ToolStripMenuItem _mOpenMapEditor;
        private System.Windows.Forms.ToolStripSeparator _mMapsSep1;
        private System.Windows.Forms.ToolStripMenuItem _mNewMap;
        private System.Windows.Forms.ToolStripMenuItem _mDupMap;
        private System.Windows.Forms.ToolStripMenuItem _mDelMap;
        private System.Windows.Forms.ToolStripMenuItem _mRenameMap;
        private System.Windows.Forms.ToolStripMenuItem _mResizeMap;
        private System.Windows.Forms.ToolStripMenuItem _mClearMap;
        private System.Windows.Forms.ToolStripSeparator _mMapsSep2;
        private System.Windows.Forms.ToolStripMenuItem _mExpMap;
        private System.Windows.Forms.ToolStripMenuItem _mExpPng;
        private System.Windows.Forms.ToolStripMenuItem _mHelp;
        private System.Windows.Forms.ToolStripMenuItem _mAbout;
        private System.Windows.Forms.StatusStrip _statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel _lblCoords;
        private System.Windows.Forms.ToolStripStatusLabel _lblCharCoords;
        private System.Windows.Forms.ToolStripStatusLabel _lblDimensions;
        private System.Windows.Forms.ToolStripStatusLabel _lblTotalBytes;
        private System.Windows.Forms.ToolStripStatusLabel _lblTool;
        private System.Windows.Forms.TabControl _mainTabs;
        private System.Windows.Forms.TabPage _tabSprites;
        private System.Windows.Forms.ToolStrip _mainToolbar;
        private System.Windows.Forms.ToolStripButton _btnNew;
        private System.Windows.Forms.ToolStripButton _btnOpen;
        private System.Windows.Forms.ToolStripButton _btnSave;
        private System.Windows.Forms.ToolStripSeparator _sepToolbar1;
        private System.Windows.Forms.ToolStripButton _btnToolPencil;
        private System.Windows.Forms.ToolStripButton _btnToolLine;
        private System.Windows.Forms.ToolStripButton _btnToolRect;
        private System.Windows.Forms.ToolStripButton _btnToolRectFill;
        private System.Windows.Forms.ToolStripButton _btnToolCircle;
        private System.Windows.Forms.ToolStripButton _btnToolCircleFill;
        private System.Windows.Forms.ToolStripButton _btnToolFill;
        private System.Windows.Forms.ToolStripSeparator _sepToolbar2;
        private System.Windows.Forms.ToolStripButton _btnUndo;
        private System.Windows.Forms.ToolStripButton _btnRedo;
        private System.Windows.Forms.ToolStripSeparator _sepToolbar3;
        private System.Windows.Forms.ToolStripButton _btnFlipH;
        private System.Windows.Forms.ToolStripButton _btnFlipV;
        private System.Windows.Forms.ToolStripButton _btnRot;
        private System.Windows.Forms.ToolStripButton _btnInv;
        private System.Windows.Forms.ToolStripButton _btnClear;
        private System.Windows.Forms.ToolStripSeparator _sepToolbar4;
        private System.Windows.Forms.ToolStripButton _btnLeft;
        private System.Windows.Forms.ToolStripButton _btnUp;
        private System.Windows.Forms.ToolStripButton _btnDown;
        private System.Windows.Forms.ToolStripButton _btnRight;
        private System.Windows.Forms.ToolStripSeparator _sepToolbar5;
        private System.Windows.Forms.ToolStripButton _btnToggleGrid;
        private System.Windows.Forms.ToolStripButton _btnToggleCharGrid;
        private System.Windows.Forms.ToolStripSeparator _sepToolbar6;
        private System.Windows.Forms.ToolStripButton _btnZoomIn;
        private System.Windows.Forms.ToolStripButton _btnZoomOut;
        private System.Windows.Forms.ToolStripSeparator _sepToolbar7;
        private System.Windows.Forms.ToolStripButton _btnQuickExport;
        private System.Windows.Forms.ToolStripSeparator _sepToolbar8;
        private System.Windows.Forms.ToolStripButton _btnGoToMaps;
        private System.Windows.Forms.Panel _rightPanel;
        private System.Windows.Forms.Label _lblSpriteNameTitle;
        private System.Windows.Forms.TextBox _txtCurrentSpriteName;
        private System.Windows.Forms.Label _lblColorsTitle;
        private System.Windows.Forms.Label _lblInk;
        private System.Windows.Forms.ComboBox _cmbInk;
        private System.Windows.Forms.Label _lblPaper;
        private System.Windows.Forms.ComboBox _cmbPaper;
        private System.Windows.Forms.CheckBox _chkBright;
        private System.Windows.Forms.Label _lblPrevTitle;
        private System.Windows.Forms.Panel _prevBox;
        private System.Windows.Forms.Label _lbl1x;
        private System.Windows.Forms.PictureBox _picPreview1x;
        private System.Windows.Forms.Label _lbl4x;
        private System.Windows.Forms.PictureBox _picPreview4x;
        private System.Windows.Forms.Label _lblAnimTitle;
        private System.Windows.Forms.Label _lblScope;
        private System.Windows.Forms.ComboBox _cmbAnimScope;
        private System.Windows.Forms.Button _btnPlayAnim;
        private System.Windows.Forms.Label _lblAnimFrame;
        private System.Windows.Forms.Label _lblSpeed;
        private System.Windows.Forms.TrackBar _tbAnimFps;
        private System.Windows.Forms.Label _lblFps;
        private System.Windows.Forms.Label _lblQuickAsmTitle;
        private System.Windows.Forms.TextBox _txtQuickAsm;
        private System.Windows.Forms.Button _btnCopyQuick;
        private System.Windows.Forms.Panel _bottomPanel;
        private System.Windows.Forms.Panel _filmstripControlBar;
        private System.Windows.Forms.Label _lblStripTitle;
        private System.Windows.Forms.Button _btnAddMultiple;
        private System.Windows.Forms.Button _btnAdd;
        private System.Windows.Forms.Button _btnClone;
        private System.Windows.Forms.Button _btnDel;
        private System.Windows.Forms.Button _btnMvL;
        private System.Windows.Forms.Button _btnMvR;
        private System.Windows.Forms.Button _btnPasteNemico;
        private System.Windows.Forms.Label _lblJump;
        private System.Windows.Forms.ComboBox _cmbJumpEnemy;
        private System.Windows.Forms.Panel _filmstripNavBar;
        private System.Windows.Forms.Button _btnScrollStart;
        private System.Windows.Forms.Button _btnScrollLeft;
        private System.Windows.Forms.HScrollBar _filmstripScrollBar;
        private System.Windows.Forms.Button _btnScrollRight;
        private System.Windows.Forms.Button _btnScrollEnd;
        private System.Windows.Forms.Label _lblFilmstripInfo;
        private ZXSpectrumSpriteGenerator.UI.Controls.SpriteFilmstrip _filmstrip;
        private System.Windows.Forms.Panel _centerPanel;
        private ZXSpectrumSpriteGenerator.UI.Controls.SpriteCanvas _canvas;
        private System.Windows.Forms.ToolStripMenuItem _mViewTiles;
        private System.Windows.Forms.ToolStripButton _btnGoToTiles;
        private System.Windows.Forms.TabPage _tabTiles;
        private ZXSpectrumSpriteGenerator.UI.TileEditorView _tileEditorView;
        private System.Windows.Forms.TabPage _tabMaps;
        private ZXSpectrumSpriteGenerator.UI.MapEditorView _mapEditorView;
        private System.Windows.Forms.Timer _animTimer;
        private System.Windows.Forms.ToolTip _toolTip;
        private System.Windows.Forms.CheckBox _chkFlash;
    }
}
