namespace ZXSpectrumSpriteGenerator.UI
{
    partial class AddTilesDialog
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
            this._tabControl = new System.Windows.Forms.TabControl();
            this._tabPresets = new System.Windows.Forms.TabPage();
            this._chkPresetFlash = new System.Windows.Forms.CheckBox();
            this._btnAddAllPresets = new System.Windows.Forms.Button();
            this._btnPresetCancel = new System.Windows.Forms.Button();
            this._btnAddPreset = new System.Windows.Forms.Button();
            this._chkPresetBright = new System.Windows.Forms.CheckBox();
            this._cmbPresetPaper = new System.Windows.Forms.ComboBox();
            this._lblPresetPaper = new System.Windows.Forms.Label();
            this._cmbPresetInk = new System.Windows.Forms.ComboBox();
            this._lblPresetInk = new System.Windows.Forms.Label();
            this._pnlPresetPreview = new System.Windows.Forms.Panel();
            this._lblPresetPreviewTitle = new System.Windows.Forms.Label();
            this._txtPresetName = new System.Windows.Forms.TextBox();
            this._lblPresetNameTitle = new System.Windows.Forms.Label();
            this._lblPresetDesc = new System.Windows.Forms.Label();
            this._cmbPresets = new System.Windows.Forms.ComboBox();
            this._lblPresetChoose = new System.Windows.Forms.Label();
            this._tabEmpty = new System.Windows.Forms.TabPage();
            this._chkEmptyFlash = new System.Windows.Forms.CheckBox();
            this._btnEmptyCancel = new System.Windows.Forms.Button();
            this._btnAddEmpty = new System.Windows.Forms.Button();
            this._chkEmptyBright = new System.Windows.Forms.CheckBox();
            this._cmbEmptyPaper = new System.Windows.Forms.ComboBox();
            this._lblEmptyPaperTitle = new System.Windows.Forms.Label();
            this._cmbEmptyInk = new System.Windows.Forms.ComboBox();
            this._lblEmptyInkTitle = new System.Windows.Forms.Label();
            this._numTileCount = new System.Windows.Forms.NumericUpDown();
            this._lblEmptyCountTitle = new System.Windows.Forms.Label();
            this._txtEmptyPrefix = new System.Windows.Forms.TextBox();
            this._lblEmptyPrefixTitle = new System.Windows.Forms.Label();
            this._tabPaste = new System.Windows.Forms.TabPage();
            this._chkPasteFlash = new System.Windows.Forms.CheckBox();
            this._btnPasteCancel = new System.Windows.Forms.Button();
            this._btnAddPasted = new System.Windows.Forms.Button();
            this._lblPasteStats = new System.Windows.Forms.Label();
            this._lblPasteInk = new System.Windows.Forms.Label();
            this._cmbPasteInk = new System.Windows.Forms.ComboBox();
            this._lblPastePaper = new System.Windows.Forms.Label();
            this._cmbPastePaper = new System.Windows.Forms.ComboBox();
            this._chkPasteBright = new System.Windows.Forms.CheckBox();
            this._txtPastePrefix = new System.Windows.Forms.TextBox();
            this._lblPastePrefixTitle = new System.Windows.Forms.Label();
            this._txtPasteCode = new System.Windows.Forms.TextBox();
            this._btnPasteFromClipboard = new System.Windows.Forms.Button();
            this._lblPasteInfo = new System.Windows.Forms.Label();
            this._tabControl.SuspendLayout();
            this._tabPresets.SuspendLayout();
            this._tabEmpty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numTileCount)).BeginInit();
            this._tabPaste.SuspendLayout();
            this.SuspendLayout();
            // 
            // _tabControl
            // 
            this._tabControl.Controls.Add(this._tabPresets);
            this._tabControl.Controls.Add(this._tabEmpty);
            this._tabControl.Controls.Add(this._tabPaste);
            this._tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabControl.Location = new System.Drawing.Point(0, 0);
            this._tabControl.Name = "_tabControl";
            this._tabControl.SelectedIndex = 0;
            this._tabControl.Size = new System.Drawing.Size(615, 491);
            this._tabControl.TabIndex = 0;
            // 
            // _tabPresets
            // 
            this._tabPresets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(34)))));
            this._tabPresets.Controls.Add(this._chkPresetFlash);
            this._tabPresets.Controls.Add(this._btnAddAllPresets);
            this._tabPresets.Controls.Add(this._btnPresetCancel);
            this._tabPresets.Controls.Add(this._btnAddPreset);
            this._tabPresets.Controls.Add(this._chkPresetBright);
            this._tabPresets.Controls.Add(this._cmbPresetPaper);
            this._tabPresets.Controls.Add(this._lblPresetPaper);
            this._tabPresets.Controls.Add(this._cmbPresetInk);
            this._tabPresets.Controls.Add(this._lblPresetInk);
            this._tabPresets.Controls.Add(this._pnlPresetPreview);
            this._tabPresets.Controls.Add(this._lblPresetPreviewTitle);
            this._tabPresets.Controls.Add(this._txtPresetName);
            this._tabPresets.Controls.Add(this._lblPresetNameTitle);
            this._tabPresets.Controls.Add(this._lblPresetDesc);
            this._tabPresets.Controls.Add(this._cmbPresets);
            this._tabPresets.Controls.Add(this._lblPresetChoose);
            this._tabPresets.Location = new System.Drawing.Point(4, 24);
            this._tabPresets.Name = "_tabPresets";
            this._tabPresets.Padding = new System.Windows.Forms.Padding(12);
            this._tabPresets.Size = new System.Drawing.Size(607, 463);
            this._tabPresets.TabIndex = 0;
            this._tabPresets.Text = "📚 Libreria Preset Tiles (8x8)";
            // 
            // _chkPresetFlash
            // 
            this._chkPresetFlash.AutoSize = true;
            this._chkPresetFlash.ForeColor = System.Drawing.Color.White;
            this._chkPresetFlash.Location = new System.Drawing.Point(340, 325);
            this._chkPresetFlash.Name = "_chkPresetFlash";
            this._chkPresetFlash.Size = new System.Drawing.Size(124, 19);
            this._chkPresetFlash.TabIndex = 15;
            this._chkPresetFlash.Text = "Flash (Lampeggio)";
            this._chkPresetFlash.UseVisualStyleBackColor = true;
            // 
            // _btnAddAllPresets
            // 
            this._btnAddAllPresets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(80)))), ((int)(((byte)(120)))));
            this._btnAddAllPresets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnAddAllPresets.ForeColor = System.Drawing.Color.White;
            this._btnAddAllPresets.Location = new System.Drawing.Point(16, 380);
            this._btnAddAllPresets.Name = "_btnAddAllPresets";
            this._btnAddAllPresets.Size = new System.Drawing.Size(185, 32);
            this._btnAddAllPresets.TabIndex = 14;
            this._btnAddAllPresets.Text = "➕ Aggiungi Tutti i Preset (12)";
            this._btnAddAllPresets.UseVisualStyleBackColor = false;
            this._btnAddAllPresets.Click += new System.EventHandler(this.BtnAddAllPresets_Click);
            // 
            // _btnPresetCancel
            // 
            this._btnPresetCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this._btnPresetCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnPresetCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnPresetCancel.ForeColor = System.Drawing.Color.White;
            this._btnPresetCancel.Location = new System.Drawing.Point(440, 380);
            this._btnPresetCancel.Name = "_btnPresetCancel";
            this._btnPresetCancel.Size = new System.Drawing.Size(95, 32);
            this._btnPresetCancel.TabIndex = 13;
            this._btnPresetCancel.Text = "Annulla";
            this._btnPresetCancel.UseVisualStyleBackColor = false;
            // 
            // _btnAddPreset
            // 
            this._btnAddPreset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this._btnAddPreset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnAddPreset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._btnAddPreset.ForeColor = System.Drawing.Color.White;
            this._btnAddPreset.Location = new System.Drawing.Point(260, 380);
            this._btnAddPreset.Name = "_btnAddPreset";
            this._btnAddPreset.Size = new System.Drawing.Size(170, 32);
            this._btnAddPreset.TabIndex = 12;
            this._btnAddPreset.Text = "➕ Aggiungi al Tileset";
            this._btnAddPreset.UseVisualStyleBackColor = false;
            this._btnAddPreset.Click += new System.EventHandler(this.BtnAddPreset_Click);
            // 
            // _chkPresetBright
            // 
            this._chkPresetBright.AutoSize = true;
            this._chkPresetBright.Checked = true;
            this._chkPresetBright.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkPresetBright.ForeColor = System.Drawing.Color.White;
            this._chkPresetBright.Location = new System.Drawing.Point(340, 290);
            this._chkPresetBright.Name = "_chkPresetBright";
            this._chkPresetBright.Size = new System.Drawing.Size(112, 19);
            this._chkPresetBright.TabIndex = 11;
            this._chkPresetBright.Text = "Bright (Brillante)";
            this._chkPresetBright.UseVisualStyleBackColor = true;
            this._chkPresetBright.CheckedChanged += new System.EventHandler(this.PresetColorChanged);
            // 
            // _cmbPresetPaper
            // 
            this._cmbPresetPaper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this._cmbPresetPaper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbPresetPaper.ForeColor = System.Drawing.Color.White;
            this._cmbPresetPaper.Location = new System.Drawing.Point(180, 288);
            this._cmbPresetPaper.Name = "_cmbPresetPaper";
            this._cmbPresetPaper.Size = new System.Drawing.Size(140, 23);
            this._cmbPresetPaper.TabIndex = 10;
            this._cmbPresetPaper.SelectedIndexChanged += new System.EventHandler(this.PresetColorChanged);
            // 
            // _lblPresetPaper
            // 
            this._lblPresetPaper.AutoSize = true;
            this._lblPresetPaper.ForeColor = System.Drawing.Color.LightGray;
            this._lblPresetPaper.Location = new System.Drawing.Point(180, 270);
            this._lblPresetPaper.Name = "_lblPresetPaper";
            this._lblPresetPaper.Size = new System.Drawing.Size(78, 15);
            this._lblPresetPaper.TabIndex = 9;
            this._lblPresetPaper.Text = "Colore Paper:";
            // 
            // _cmbPresetInk
            // 
            this._cmbPresetInk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this._cmbPresetInk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbPresetInk.ForeColor = System.Drawing.Color.White;
            this._cmbPresetInk.Location = new System.Drawing.Point(16, 288);
            this._cmbPresetInk.Name = "_cmbPresetInk";
            this._cmbPresetInk.Size = new System.Drawing.Size(140, 23);
            this._cmbPresetInk.TabIndex = 8;
            this._cmbPresetInk.SelectedIndexChanged += new System.EventHandler(this.PresetColorChanged);
            // 
            // _lblPresetInk
            // 
            this._lblPresetInk.AutoSize = true;
            this._lblPresetInk.ForeColor = System.Drawing.Color.LightGray;
            this._lblPresetInk.Location = new System.Drawing.Point(16, 270);
            this._lblPresetInk.Name = "_lblPresetInk";
            this._lblPresetInk.Size = new System.Drawing.Size(64, 15);
            this._lblPresetInk.TabIndex = 7;
            this._lblPresetInk.Text = "Colore Ink:";
            // 
            // _pnlPresetPreview
            // 
            this._pnlPresetPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(20)))));
            this._pnlPresetPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pnlPresetPreview.Location = new System.Drawing.Point(16, 155);
            this._pnlPresetPreview.Name = "_pnlPresetPreview";
            this._pnlPresetPreview.Size = new System.Drawing.Size(520, 100);
            this._pnlPresetPreview.TabIndex = 6;
            this._pnlPresetPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlPresetPreview_Paint);
            // 
            // _lblPresetPreviewTitle
            // 
            this._lblPresetPreviewTitle.AutoSize = true;
            this._lblPresetPreviewTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this._lblPresetPreviewTitle.Location = new System.Drawing.Point(16, 135);
            this._lblPresetPreviewTitle.Name = "_lblPresetPreviewTitle";
            this._lblPresetPreviewTitle.Size = new System.Drawing.Size(206, 15);
            this._lblPresetPreviewTitle.TabIndex = 5;
            this._lblPresetPreviewTitle.Text = "Anteprima Grafica (1x, 4x, 8x e Muro):";
            // 
            // _txtPresetName
            // 
            this._txtPresetName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this._txtPresetName.ForeColor = System.Drawing.Color.White;
            this._txtPresetName.Location = new System.Drawing.Point(340, 32);
            this._txtPresetName.Name = "_txtPresetName";
            this._txtPresetName.Size = new System.Drawing.Size(195, 23);
            this._txtPresetName.TabIndex = 4;
            // 
            // _lblPresetNameTitle
            // 
            this._lblPresetNameTitle.AutoSize = true;
            this._lblPresetNameTitle.ForeColor = System.Drawing.Color.LightGray;
            this._lblPresetNameTitle.Location = new System.Drawing.Point(340, 12);
            this._lblPresetNameTitle.Name = "_lblPresetNameTitle";
            this._lblPresetNameTitle.Size = new System.Drawing.Size(64, 15);
            this._lblPresetNameTitle.TabIndex = 3;
            this._lblPresetNameTitle.Text = "Nome Tile:";
            // 
            // _lblPresetDesc
            // 
            this._lblPresetDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this._lblPresetDesc.Location = new System.Drawing.Point(16, 65);
            this._lblPresetDesc.Name = "_lblPresetDesc";
            this._lblPresetDesc.Size = new System.Drawing.Size(520, 60);
            this._lblPresetDesc.TabIndex = 2;
            this._lblPresetDesc.Text = "Descrizione del preset selezionato...";
            // 
            // _cmbPresets
            // 
            this._cmbPresets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this._cmbPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbPresets.ForeColor = System.Drawing.Color.White;
            this._cmbPresets.Location = new System.Drawing.Point(16, 32);
            this._cmbPresets.Name = "_cmbPresets";
            this._cmbPresets.Size = new System.Drawing.Size(305, 23);
            this._cmbPresets.TabIndex = 1;
            this._cmbPresets.SelectedIndexChanged += new System.EventHandler(this.CmbPresets_SelectedIndexChanged);
            // 
            // _lblPresetChoose
            // 
            this._lblPresetChoose.AutoSize = true;
            this._lblPresetChoose.ForeColor = System.Drawing.Color.LightGray;
            this._lblPresetChoose.Location = new System.Drawing.Point(16, 12);
            this._lblPresetChoose.Name = "_lblPresetChoose";
            this._lblPresetChoose.Size = new System.Drawing.Size(94, 15);
            this._lblPresetChoose.TabIndex = 0;
            this._lblPresetChoose.Text = "Seleziona Preset:";
            // 
            // _tabEmpty
            // 
            this._tabEmpty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(34)))));
            this._tabEmpty.Controls.Add(this._chkEmptyFlash);
            this._tabEmpty.Controls.Add(this._btnEmptyCancel);
            this._tabEmpty.Controls.Add(this._btnAddEmpty);
            this._tabEmpty.Controls.Add(this._chkEmptyBright);
            this._tabEmpty.Controls.Add(this._cmbEmptyPaper);
            this._tabEmpty.Controls.Add(this._lblEmptyPaperTitle);
            this._tabEmpty.Controls.Add(this._cmbEmptyInk);
            this._tabEmpty.Controls.Add(this._lblEmptyInkTitle);
            this._tabEmpty.Controls.Add(this._numTileCount);
            this._tabEmpty.Controls.Add(this._lblEmptyCountTitle);
            this._tabEmpty.Controls.Add(this._txtEmptyPrefix);
            this._tabEmpty.Controls.Add(this._lblEmptyPrefixTitle);
            this._tabEmpty.Location = new System.Drawing.Point(4, 24);
            this._tabEmpty.Name = "_tabEmpty";
            this._tabEmpty.Padding = new System.Windows.Forms.Padding(12);
            this._tabEmpty.Size = new System.Drawing.Size(607, 463);
            this._tabEmpty.TabIndex = 1;
            this._tabEmpty.Text = "⬜ Nuovo Set Tile Vuoti";
            // 
            // _chkEmptyFlash
            // 
            this._chkEmptyFlash.AutoSize = true;
            this._chkEmptyFlash.ForeColor = System.Drawing.Color.White;
            this._chkEmptyFlash.Location = new System.Drawing.Point(25, 275);
            this._chkEmptyFlash.Name = "_chkEmptyFlash";
            this._chkEmptyFlash.Size = new System.Drawing.Size(124, 19);
            this._chkEmptyFlash.TabIndex = 11;
            this._chkEmptyFlash.Text = "Flash (Lampeggio)";
            this._chkEmptyFlash.UseVisualStyleBackColor = true;
            // 
            // _btnEmptyCancel
            // 
            this._btnEmptyCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this._btnEmptyCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnEmptyCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnEmptyCancel.ForeColor = System.Drawing.Color.White;
            this._btnEmptyCancel.Location = new System.Drawing.Point(440, 345);
            this._btnEmptyCancel.Name = "_btnEmptyCancel";
            this._btnEmptyCancel.Size = new System.Drawing.Size(95, 32);
            this._btnEmptyCancel.TabIndex = 10;
            this._btnEmptyCancel.Text = "Annulla";
            this._btnEmptyCancel.UseVisualStyleBackColor = false;
            // 
            // _btnAddEmpty
            // 
            this._btnAddEmpty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this._btnAddEmpty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnAddEmpty.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._btnAddEmpty.ForeColor = System.Drawing.Color.White;
            this._btnAddEmpty.Location = new System.Drawing.Point(260, 345);
            this._btnAddEmpty.Name = "_btnAddEmpty";
            this._btnAddEmpty.Size = new System.Drawing.Size(170, 32);
            this._btnAddEmpty.TabIndex = 9;
            this._btnAddEmpty.Text = "Crea Tile Vuoti";
            this._btnAddEmpty.UseVisualStyleBackColor = false;
            this._btnAddEmpty.Click += new System.EventHandler(this.BtnAddEmpty_Click);
            // 
            // _chkEmptyBright
            // 
            this._chkEmptyBright.AutoSize = true;
            this._chkEmptyBright.Checked = true;
            this._chkEmptyBright.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkEmptyBright.ForeColor = System.Drawing.Color.White;
            this._chkEmptyBright.Location = new System.Drawing.Point(25, 240);
            this._chkEmptyBright.Name = "_chkEmptyBright";
            this._chkEmptyBright.Size = new System.Drawing.Size(187, 19);
            this._chkEmptyBright.TabIndex = 8;
            this._chkEmptyBright.Text = "Bright (Alta Luminosità Colori)";
            this._chkEmptyBright.UseVisualStyleBackColor = true;
            // 
            // _cmbEmptyPaper
            // 
            this._cmbEmptyPaper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this._cmbEmptyPaper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbEmptyPaper.ForeColor = System.Drawing.Color.White;
            this._cmbEmptyPaper.Location = new System.Drawing.Point(250, 185);
            this._cmbEmptyPaper.Name = "_cmbEmptyPaper";
            this._cmbEmptyPaper.Size = new System.Drawing.Size(190, 23);
            this._cmbEmptyPaper.TabIndex = 7;
            // 
            // _lblEmptyPaperTitle
            // 
            this._lblEmptyPaperTitle.AutoSize = true;
            this._lblEmptyPaperTitle.ForeColor = System.Drawing.Color.LightGray;
            this._lblEmptyPaperTitle.Location = new System.Drawing.Point(250, 165);
            this._lblEmptyPaperTitle.Name = "_lblEmptyPaperTitle";
            this._lblEmptyPaperTitle.Size = new System.Drawing.Size(78, 15);
            this._lblEmptyPaperTitle.TabIndex = 6;
            this._lblEmptyPaperTitle.Text = "Colore Paper:";
            // 
            // _cmbEmptyInk
            // 
            this._cmbEmptyInk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this._cmbEmptyInk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbEmptyInk.ForeColor = System.Drawing.Color.White;
            this._cmbEmptyInk.Location = new System.Drawing.Point(25, 185);
            this._cmbEmptyInk.Name = "_cmbEmptyInk";
            this._cmbEmptyInk.Size = new System.Drawing.Size(190, 23);
            this._cmbEmptyInk.TabIndex = 5;
            // 
            // _lblEmptyInkTitle
            // 
            this._lblEmptyInkTitle.AutoSize = true;
            this._lblEmptyInkTitle.ForeColor = System.Drawing.Color.LightGray;
            this._lblEmptyInkTitle.Location = new System.Drawing.Point(25, 165);
            this._lblEmptyInkTitle.Name = "_lblEmptyInkTitle";
            this._lblEmptyInkTitle.Size = new System.Drawing.Size(64, 15);
            this._lblEmptyInkTitle.TabIndex = 4;
            this._lblEmptyInkTitle.Text = "Colore Ink:";
            // 
            // _numTileCount
            // 
            this._numTileCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this._numTileCount.ForeColor = System.Drawing.Color.White;
            this._numTileCount.Location = new System.Drawing.Point(25, 110);
            this._numTileCount.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this._numTileCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numTileCount.Name = "_numTileCount";
            this._numTileCount.Size = new System.Drawing.Size(120, 23);
            this._numTileCount.TabIndex = 3;
            this._numTileCount.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // _lblEmptyCountTitle
            // 
            this._lblEmptyCountTitle.AutoSize = true;
            this._lblEmptyCountTitle.ForeColor = System.Drawing.Color.LightGray;
            this._lblEmptyCountTitle.Location = new System.Drawing.Point(25, 90);
            this._lblEmptyCountTitle.Name = "_lblEmptyCountTitle";
            this._lblEmptyCountTitle.Size = new System.Drawing.Size(133, 15);
            this._lblEmptyCountTitle.TabIndex = 2;
            this._lblEmptyCountTitle.Text = "Numero di Tile (8x8 px):";
            // 
            // _txtEmptyPrefix
            // 
            this._txtEmptyPrefix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this._txtEmptyPrefix.ForeColor = System.Drawing.Color.White;
            this._txtEmptyPrefix.Location = new System.Drawing.Point(25, 45);
            this._txtEmptyPrefix.Name = "_txtEmptyPrefix";
            this._txtEmptyPrefix.Size = new System.Drawing.Size(260, 23);
            this._txtEmptyPrefix.TabIndex = 1;
            this._txtEmptyPrefix.Text = "Tile_";
            // 
            // _lblEmptyPrefixTitle
            // 
            this._lblEmptyPrefixTitle.AutoSize = true;
            this._lblEmptyPrefixTitle.ForeColor = System.Drawing.Color.LightGray;
            this._lblEmptyPrefixTitle.Location = new System.Drawing.Point(25, 25);
            this._lblEmptyPrefixTitle.Name = "_lblEmptyPrefixTitle";
            this._lblEmptyPrefixTitle.Size = new System.Drawing.Size(72, 15);
            this._lblEmptyPrefixTitle.TabIndex = 0;
            this._lblEmptyPrefixTitle.Text = "Prefisso Tile:";
            // 
            // _tabPaste
            // 
            this._tabPaste.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(34)))));
            this._tabPaste.Controls.Add(this._chkPasteFlash);
            this._tabPaste.Controls.Add(this._btnPasteCancel);
            this._tabPaste.Controls.Add(this._btnAddPasted);
            this._tabPaste.Controls.Add(this._lblPasteStats);
            this._tabPaste.Controls.Add(this._lblPasteInk);
            this._tabPaste.Controls.Add(this._cmbPasteInk);
            this._tabPaste.Controls.Add(this._lblPastePaper);
            this._tabPaste.Controls.Add(this._cmbPastePaper);
            this._tabPaste.Controls.Add(this._chkPasteBright);
            this._tabPaste.Controls.Add(this._txtPastePrefix);
            this._tabPaste.Controls.Add(this._lblPastePrefixTitle);
            this._tabPaste.Controls.Add(this._txtPasteCode);
            this._tabPaste.Controls.Add(this._btnPasteFromClipboard);
            this._tabPaste.Controls.Add(this._lblPasteInfo);
            this._tabPaste.Location = new System.Drawing.Point(4, 24);
            this._tabPaste.Name = "_tabPaste";
            this._tabPaste.Padding = new System.Windows.Forms.Padding(12);
            this._tabPaste.Size = new System.Drawing.Size(607, 463);
            this._tabPaste.TabIndex = 2;
            this._tabPaste.Text = "📋 Incolla Assembly (db/defb)";
            // 
            // _chkPasteFlash
            // 
            this._chkPasteFlash.AutoSize = true;
            this._chkPasteFlash.ForeColor = System.Drawing.Color.White;
            this._chkPasteFlash.Location = new System.Drawing.Point(340, 345);
            this._chkPasteFlash.Name = "_chkPasteFlash";
            this._chkPasteFlash.Size = new System.Drawing.Size(124, 19);
            this._chkPasteFlash.TabIndex = 13;
            this._chkPasteFlash.Text = "Flash (Lampeggio)";
            this._chkPasteFlash.UseVisualStyleBackColor = true;
            this._chkPasteFlash.Visible = false;
            // 
            // _btnPasteCancel
            // 
            this._btnPasteCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(65)))));
            this._btnPasteCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnPasteCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnPasteCancel.ForeColor = System.Drawing.Color.White;
            this._btnPasteCancel.Location = new System.Drawing.Point(440, 388);
            this._btnPasteCancel.Name = "_btnPasteCancel";
            this._btnPasteCancel.Size = new System.Drawing.Size(95, 32);
            this._btnPasteCancel.TabIndex = 7;
            this._btnPasteCancel.Text = "Annulla";
            this._btnPasteCancel.UseVisualStyleBackColor = false;
            // 
            // _btnAddPasted
            // 
            this._btnAddPasted.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this._btnAddPasted.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnAddPasted.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._btnAddPasted.ForeColor = System.Drawing.Color.White;
            this._btnAddPasted.Location = new System.Drawing.Point(260, 388);
            this._btnAddPasted.Name = "_btnAddPasted";
            this._btnAddPasted.Size = new System.Drawing.Size(170, 32);
            this._btnAddPasted.TabIndex = 6;
            this._btnAddPasted.Text = "📥 Importa Tile";
            this._btnAddPasted.UseVisualStyleBackColor = false;
            this._btnAddPasted.Click += new System.EventHandler(this.BtnAddPasted_Click);
            // 
            // _lblPasteStats
            // 
            this._lblPasteStats.AutoSize = true;
            this._lblPasteStats.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this._lblPasteStats.Location = new System.Drawing.Point(16, 310);
            this._lblPasteStats.Name = "_lblPasteStats";
            this._lblPasteStats.Size = new System.Drawing.Size(167, 15);
            this._lblPasteStats.TabIndex = 5;
            this._lblPasteStats.Text = "Dati riconosciuti: 0 byte (0 tile)";
            // 
            // _lblPasteInk
            // 
            this._lblPasteInk.AutoSize = true;
            this._lblPasteInk.ForeColor = System.Drawing.Color.LightGray;
            this._lblPasteInk.Location = new System.Drawing.Point(16, 255);
            this._lblPasteInk.Name = "_lblPasteInk";
            this._lblPasteInk.Size = new System.Drawing.Size(64, 15);
            this._lblPasteInk.TabIndex = 8;
            this._lblPasteInk.Text = "Colore Ink:";
            // 
            // _cmbPasteInk
            // 
            this._cmbPasteInk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this._cmbPasteInk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbPasteInk.ForeColor = System.Drawing.Color.White;
            this._cmbPasteInk.Location = new System.Drawing.Point(16, 275);
            this._cmbPasteInk.Name = "_cmbPasteInk";
            this._cmbPasteInk.Size = new System.Drawing.Size(140, 23);
            this._cmbPasteInk.TabIndex = 9;
            // 
            // _lblPastePaper
            // 
            this._lblPastePaper.AutoSize = true;
            this._lblPastePaper.ForeColor = System.Drawing.Color.LightGray;
            this._lblPastePaper.Location = new System.Drawing.Point(170, 255);
            this._lblPastePaper.Name = "_lblPastePaper";
            this._lblPastePaper.Size = new System.Drawing.Size(78, 15);
            this._lblPastePaper.TabIndex = 10;
            this._lblPastePaper.Text = "Colore Paper:";
            // 
            // _cmbPastePaper
            // 
            this._cmbPastePaper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this._cmbPastePaper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbPastePaper.ForeColor = System.Drawing.Color.White;
            this._cmbPastePaper.Location = new System.Drawing.Point(170, 275);
            this._cmbPastePaper.Name = "_cmbPastePaper";
            this._cmbPastePaper.Size = new System.Drawing.Size(140, 23);
            this._cmbPastePaper.TabIndex = 11;
            // 
            // _chkPasteBright
            // 
            this._chkPasteBright.AutoSize = true;
            this._chkPasteBright.Checked = true;
            this._chkPasteBright.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkPasteBright.ForeColor = System.Drawing.Color.White;
            this._chkPasteBright.Location = new System.Drawing.Point(340, 310);
            this._chkPasteBright.Name = "_chkPasteBright";
            this._chkPasteBright.Size = new System.Drawing.Size(112, 19);
            this._chkPasteBright.TabIndex = 12;
            this._chkPasteBright.Text = "Bright (Brillante)";
            this._chkPasteBright.UseVisualStyleBackColor = true;
            this._chkPasteBright.Visible = false;
            // 
            // _txtPastePrefix
            // 
            this._txtPastePrefix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(25)))));
            this._txtPastePrefix.ForeColor = System.Drawing.Color.White;
            this._txtPastePrefix.Location = new System.Drawing.Point(340, 275);
            this._txtPastePrefix.Name = "_txtPastePrefix";
            this._txtPastePrefix.Size = new System.Drawing.Size(195, 23);
            this._txtPastePrefix.TabIndex = 4;
            this._txtPastePrefix.Text = "Tile_Imp_";
            // 
            // _lblPastePrefixTitle
            // 
            this._lblPastePrefixTitle.AutoSize = true;
            this._lblPastePrefixTitle.ForeColor = System.Drawing.Color.LightGray;
            this._lblPastePrefixTitle.Location = new System.Drawing.Point(340, 255);
            this._lblPastePrefixTitle.Name = "_lblPastePrefixTitle";
            this._lblPastePrefixTitle.Size = new System.Drawing.Size(72, 15);
            this._lblPastePrefixTitle.TabIndex = 3;
            this._lblPastePrefixTitle.Text = "Prefisso Tile:";
            // 
            // _txtPasteCode
            // 
            this._txtPasteCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(20)))));
            this._txtPasteCode.Font = new System.Drawing.Font("Consolas", 8.8F);
            this._txtPasteCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(180)))));
            this._txtPasteCode.Location = new System.Drawing.Point(16, 45);
            this._txtPasteCode.Multiline = true;
            this._txtPasteCode.Name = "_txtPasteCode";
            this._txtPasteCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._txtPasteCode.Size = new System.Drawing.Size(520, 200);
            this._txtPasteCode.TabIndex = 2;
            this._txtPasteCode.TextChanged += new System.EventHandler(this.TxtPasteCode_TextChanged);
            // 
            // _btnPasteFromClipboard
            // 
            this._btnPasteFromClipboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(70)))), ((int)(((byte)(110)))));
            this._btnPasteFromClipboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnPasteFromClipboard.ForeColor = System.Drawing.Color.White;
            this._btnPasteFromClipboard.Location = new System.Drawing.Point(365, 10);
            this._btnPasteFromClipboard.Name = "_btnPasteFromClipboard";
            this._btnPasteFromClipboard.Size = new System.Drawing.Size(170, 28);
            this._btnPasteFromClipboard.TabIndex = 1;
            this._btnPasteFromClipboard.Text = "📋 Incolla da Appunti";
            this._btnPasteFromClipboard.UseVisualStyleBackColor = false;
            this._btnPasteFromClipboard.Click += new System.EventHandler(this.BtnPasteFromClipboard_Click);
            // 
            // _lblPasteInfo
            // 
            this._lblPasteInfo.AutoSize = true;
            this._lblPasteInfo.ForeColor = System.Drawing.Color.LightGray;
            this._lblPasteInfo.Location = new System.Drawing.Point(16, 17);
            this._lblPasteInfo.Name = "_lblPasteInfo";
            this._lblPasteInfo.Size = new System.Drawing.Size(290, 15);
            this._lblPasteInfo.TabIndex = 0;
            this._lblPasteInfo.Text = "Incolla dati Assembly Z80 (8 byte per ciascun tile 8x8):";
            // 
            // AddTilesDialog
            // 
            this.AcceptButton = this._btnAddPreset;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(36)))));
            this.CancelButton = this._btnPresetCancel;
            this.ClientSize = new System.Drawing.Size(615, 491);
            this.Controls.Add(this._tabControl);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddTilesDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Aggiungi / Importa Tiles (8x8) ZX Spectrum";
            this._tabControl.ResumeLayout(false);
            this._tabPresets.ResumeLayout(false);
            this._tabPresets.PerformLayout();
            this._tabEmpty.ResumeLayout(false);
            this._tabEmpty.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numTileCount)).EndInit();
            this._tabPaste.ResumeLayout(false);
            this._tabPaste.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.TabPage _tabPresets;
        private System.Windows.Forms.Label _lblPresetChoose;
        private System.Windows.Forms.ComboBox _cmbPresets;
        private System.Windows.Forms.Label _lblPresetDesc;
        private System.Windows.Forms.Label _lblPresetNameTitle;
        private System.Windows.Forms.TextBox _txtPresetName;
        private System.Windows.Forms.Label _lblPresetPreviewTitle;
        private System.Windows.Forms.Panel _pnlPresetPreview;
        private System.Windows.Forms.Label _lblPresetInk;
        private System.Windows.Forms.ComboBox _cmbPresetInk;
        private System.Windows.Forms.Label _lblPresetPaper;
        private System.Windows.Forms.ComboBox _cmbPresetPaper;
        private System.Windows.Forms.CheckBox _chkPresetBright;
        private System.Windows.Forms.Button _btnAddPreset;
        private System.Windows.Forms.Button _btnAddAllPresets;
        private System.Windows.Forms.Button _btnPresetCancel;
        private System.Windows.Forms.TabPage _tabEmpty;
        private System.Windows.Forms.Label _lblEmptyPrefixTitle;
        private System.Windows.Forms.TextBox _txtEmptyPrefix;
        private System.Windows.Forms.Label _lblEmptyCountTitle;
        private System.Windows.Forms.NumericUpDown _numTileCount;
        private System.Windows.Forms.Label _lblEmptyInkTitle;
        private System.Windows.Forms.ComboBox _cmbEmptyInk;
        private System.Windows.Forms.Label _lblEmptyPaperTitle;
        private System.Windows.Forms.ComboBox _cmbEmptyPaper;
        private System.Windows.Forms.CheckBox _chkEmptyBright;
        private System.Windows.Forms.Button _btnAddEmpty;
        private System.Windows.Forms.Button _btnEmptyCancel;
        private System.Windows.Forms.TabPage _tabPaste;
        private System.Windows.Forms.Label _lblPasteInfo;
        private System.Windows.Forms.Button _btnPasteFromClipboard;
        private System.Windows.Forms.TextBox _txtPasteCode;
        private System.Windows.Forms.Label _lblPastePrefixTitle;
        private System.Windows.Forms.TextBox _txtPastePrefix;
        private System.Windows.Forms.Label _lblPasteStats;
        private System.Windows.Forms.Label _lblPasteInk;
        private System.Windows.Forms.ComboBox _cmbPasteInk;
        private System.Windows.Forms.Label _lblPastePaper;
        private System.Windows.Forms.ComboBox _cmbPastePaper;
        private System.Windows.Forms.CheckBox _chkPasteBright;
        private System.Windows.Forms.Button _btnAddPasted;
        private System.Windows.Forms.Button _btnPasteCancel;
        private System.Windows.Forms.CheckBox _chkPresetFlash;
        private System.Windows.Forms.CheckBox _chkPasteFlash;
        private System.Windows.Forms.CheckBox _chkEmptyFlash;
    }
}
