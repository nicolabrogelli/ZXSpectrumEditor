namespace ZXSpectrumSpriteGenerator.UI
{
    partial class AddEnemiesDialog
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
            this._btnPresetCancel = new System.Windows.Forms.Button();
            this._btnAddPreset = new System.Windows.Forms.Button();
            this._pnlPresetPreview = new System.Windows.Forms.Panel();
            this._lblPresetPreviewTitle = new System.Windows.Forms.Label();
            this._txtPresetPrefix = new System.Windows.Forms.TextBox();
            this._lblPresetPrefixTitle = new System.Windows.Forms.Label();
            this._lblPresetDesc = new System.Windows.Forms.Label();
            this._cmbPresets = new System.Windows.Forms.ComboBox();
            this._lblPresetChoose = new System.Windows.Forms.Label();
            this._tabEmpty = new System.Windows.Forms.TabPage();
            this._btnAddEmpty = new System.Windows.Forms.Button();
            this._chkEmptyBright = new System.Windows.Forms.CheckBox();
            this._cmbEmptyPaper = new System.Windows.Forms.ComboBox();
            this._lblEmptyPaperTitle = new System.Windows.Forms.Label();
            this._cmbEmptyInk = new System.Windows.Forms.ComboBox();
            this._lblEmptyInkTitle = new System.Windows.Forms.Label();
            this._rbNumbered = new System.Windows.Forms.RadioButton();
            this._rbDirections = new System.Windows.Forms.RadioButton();
            this._lblEmptyNamingTitle = new System.Windows.Forms.Label();
            this._numFrameCount = new System.Windows.Forms.NumericUpDown();
            this._lblEmptyCountTitle = new System.Windows.Forms.Label();
            this._txtEmptyPrefix = new System.Windows.Forms.TextBox();
            this._lblEmptyPrefixTitle = new System.Windows.Forms.Label();
            this._tabPaste = new System.Windows.Forms.TabPage();
            this._btnAddPasted = new System.Windows.Forms.Button();
            this._lblPasteStats = new System.Windows.Forms.Label();
            this._txtPastePrefix = new System.Windows.Forms.TextBox();
            this._lblPastePrefixTitle = new System.Windows.Forms.Label();
            this._txtPasteCode = new System.Windows.Forms.TextBox();
            this._btnPasteFromClipboard = new System.Windows.Forms.Button();
            this._lblPasteInfo = new System.Windows.Forms.Label();
            this._tabControl.SuspendLayout();
            this._tabPresets.SuspendLayout();
            this._tabEmpty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numFrameCount)).BeginInit();
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
            this._tabControl.Size = new System.Drawing.Size(704, 481);
            this._tabControl.TabIndex = 0;
            // 
            // _tabPresets
            // 
            this._tabPresets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this._tabPresets.Controls.Add(this._btnPresetCancel);
            this._tabPresets.Controls.Add(this._btnAddPreset);
            this._tabPresets.Controls.Add(this._pnlPresetPreview);
            this._tabPresets.Controls.Add(this._lblPresetPreviewTitle);
            this._tabPresets.Controls.Add(this._txtPresetPrefix);
            this._tabPresets.Controls.Add(this._lblPresetPrefixTitle);
            this._tabPresets.Controls.Add(this._lblPresetDesc);
            this._tabPresets.Controls.Add(this._cmbPresets);
            this._tabPresets.Controls.Add(this._lblPresetChoose);
            this._tabPresets.Location = new System.Drawing.Point(4, 24);
            this._tabPresets.Name = "_tabPresets";
            this._tabPresets.Padding = new System.Windows.Forms.Padding(3);
            this._tabPresets.Size = new System.Drawing.Size(696, 453);
            this._tabPresets.TabIndex = 0;
            this._tabPresets.Text = "Libreria Nemici (Preset)";
            // 
            // _btnPresetCancel
            // 
            this._btnPresetCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this._btnPresetCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnPresetCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnPresetCancel.ForeColor = System.Drawing.Color.White;
            this._btnPresetCancel.Location = new System.Drawing.Point(360, 390);
            this._btnPresetCancel.Name = "_btnPresetCancel";
            this._btnPresetCancel.Size = new System.Drawing.Size(120, 38);
            this._btnPresetCancel.TabIndex = 8;
            this._btnPresetCancel.Text = "Annulla";
            this._btnPresetCancel.UseVisualStyleBackColor = false;
            this._btnPresetCancel.Click += new System.EventHandler(this.BtnPresetCancel_Click);
            // 
            // _btnAddPreset
            // 
            this._btnAddPreset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this._btnAddPreset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnAddPreset.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this._btnAddPreset.ForeColor = System.Drawing.Color.White;
            this._btnAddPreset.Location = new System.Drawing.Point(20, 390);
            this._btnAddPreset.Name = "_btnAddPreset";
            this._btnAddPreset.Size = new System.Drawing.Size(320, 38);
            this._btnAddPreset.TabIndex = 7;
            this._btnAddPreset.Text = "➕ Aggiungi Nemico al Progetto (4 Sprite)";
            this._btnAddPreset.UseVisualStyleBackColor = false;
            this._btnAddPreset.Click += new System.EventHandler(this.BtnAddPreset_Click);
            // 
            // _pnlPresetPreview
            // 
            this._pnlPresetPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(24)))));
            this._pnlPresetPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pnlPresetPreview.Location = new System.Drawing.Point(20, 185);
            this._pnlPresetPreview.Name = "_pnlPresetPreview";
            this._pnlPresetPreview.Size = new System.Drawing.Size(650, 180);
            this._pnlPresetPreview.TabIndex = 6;
            this._pnlPresetPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlPresetPreview_Paint);
            // 
            // _lblPresetPreviewTitle
            // 
            this._lblPresetPreviewTitle.AutoSize = true;
            this._lblPresetPreviewTitle.ForeColor = System.Drawing.Color.LightGray;
            this._lblPresetPreviewTitle.Location = new System.Drawing.Point(20, 160);
            this._lblPresetPreviewTitle.Name = "_lblPresetPreviewTitle";
            this._lblPresetPreviewTitle.Size = new System.Drawing.Size(161, 15);
            this._lblPresetPreviewTitle.TabIndex = 5;
            this._lblPresetPreviewTitle.Text = "Anteprima frame dello sprite:";
            // 
            // _txtPresetPrefix
            // 
            this._txtPresetPrefix.Location = new System.Drawing.Point(125, 122);
            this._txtPresetPrefix.Name = "_txtPresetPrefix";
            this._txtPresetPrefix.Size = new System.Drawing.Size(200, 23);
            this._txtPresetPrefix.TabIndex = 4;
            this._txtPresetPrefix.Text = "Nemico_1";
            // 
            // _lblPresetPrefixTitle
            // 
            this._lblPresetPrefixTitle.AutoSize = true;
            this._lblPresetPrefixTitle.ForeColor = System.Drawing.Color.LightGray;
            this._lblPresetPrefixTitle.Location = new System.Drawing.Point(20, 125);
            this._lblPresetPrefixTitle.Name = "_lblPresetPrefixTitle";
            this._lblPresetPrefixTitle.Size = new System.Drawing.Size(85, 15);
            this._lblPresetPrefixTitle.TabIndex = 3;
            this._lblPresetPrefixTitle.Text = "Prefisso Nome:";
            // 
            // _lblPresetDesc
            // 
            this._lblPresetDesc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this._lblPresetDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this._lblPresetDesc.Location = new System.Drawing.Point(20, 80);
            this._lblPresetDesc.Name = "_lblPresetDesc";
            this._lblPresetDesc.Size = new System.Drawing.Size(650, 35);
            this._lblPresetDesc.TabIndex = 2;
            // 
            // _cmbPresets
            // 
            this._cmbPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbPresets.FormattingEnabled = true;
            this._cmbPresets.Location = new System.Drawing.Point(20, 45);
            this._cmbPresets.Name = "_cmbPresets";
            this._cmbPresets.Size = new System.Drawing.Size(450, 23);
            this._cmbPresets.TabIndex = 1;
            this._cmbPresets.SelectedIndexChanged += new System.EventHandler(this.CmbPresets_SelectedIndexChanged);
            // 
            // _lblPresetChoose
            // 
            this._lblPresetChoose.AutoSize = true;
            this._lblPresetChoose.ForeColor = System.Drawing.Color.LightGray;
            this._lblPresetChoose.Location = new System.Drawing.Point(20, 20);
            this._lblPresetChoose.Name = "_lblPresetChoose";
            this._lblPresetChoose.Size = new System.Drawing.Size(122, 15);
            this._lblPresetChoose.TabIndex = 0;
            this._lblPresetChoose.Text = "Scegli tipo di nemico:";
            // 
            // _tabEmpty
            // 
            this._tabEmpty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this._tabEmpty.Controls.Add(this._btnAddEmpty);
            this._tabEmpty.Controls.Add(this._chkEmptyBright);
            this._tabEmpty.Controls.Add(this._cmbEmptyPaper);
            this._tabEmpty.Controls.Add(this._lblEmptyPaperTitle);
            this._tabEmpty.Controls.Add(this._cmbEmptyInk);
            this._tabEmpty.Controls.Add(this._lblEmptyInkTitle);
            this._tabEmpty.Controls.Add(this._rbNumbered);
            this._tabEmpty.Controls.Add(this._rbDirections);
            this._tabEmpty.Controls.Add(this._lblEmptyNamingTitle);
            this._tabEmpty.Controls.Add(this._numFrameCount);
            this._tabEmpty.Controls.Add(this._lblEmptyCountTitle);
            this._tabEmpty.Controls.Add(this._txtEmptyPrefix);
            this._tabEmpty.Controls.Add(this._lblEmptyPrefixTitle);
            this._tabEmpty.Location = new System.Drawing.Point(4, 24);
            this._tabEmpty.Name = "_tabEmpty";
            this._tabEmpty.Padding = new System.Windows.Forms.Padding(3);
            this._tabEmpty.Size = new System.Drawing.Size(696, 453);
            this._tabEmpty.TabIndex = 1;
            this._tabEmpty.Text = "Nuovo Set Nemico (N Frame)";
            // 
            // _btnAddEmpty
            // 
            this._btnAddEmpty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(150)))), ((int)(((byte)(80)))));
            this._btnAddEmpty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnAddEmpty.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this._btnAddEmpty.ForeColor = System.Drawing.Color.White;
            this._btnAddEmpty.Location = new System.Drawing.Point(20, 301);
            this._btnAddEmpty.Name = "_btnAddEmpty";
            this._btnAddEmpty.Size = new System.Drawing.Size(280, 38);
            this._btnAddEmpty.TabIndex = 12;
            this._btnAddEmpty.Text = "➕ Crea ed Aggiungi Sprite Vuoti";
            this._btnAddEmpty.UseVisualStyleBackColor = false;
            this._btnAddEmpty.Click += new System.EventHandler(this.BtnAddEmpty_Click);
            // 
            // _chkEmptyBright
            // 
            this._chkEmptyBright.AutoSize = true;
            this._chkEmptyBright.Checked = true;
            this._chkEmptyBright.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkEmptyBright.ForeColor = System.Drawing.Color.White;
            this._chkEmptyBright.Location = new System.Drawing.Point(200, 246);
            this._chkEmptyBright.Name = "_chkEmptyBright";
            this._chkEmptyBright.Size = new System.Drawing.Size(155, 19);
            this._chkEmptyBright.TabIndex = 11;
            this._chkEmptyBright.Text = "Luminosità Alta (Bright)";
            this._chkEmptyBright.UseVisualStyleBackColor = true;
            // 
            // _cmbEmptyPaper
            // 
            this._cmbEmptyPaper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbEmptyPaper.FormattingEnabled = true;
            this._cmbEmptyPaper.Location = new System.Drawing.Point(200, 208);
            this._cmbEmptyPaper.Name = "_cmbEmptyPaper";
            this._cmbEmptyPaper.Size = new System.Drawing.Size(150, 23);
            this._cmbEmptyPaper.TabIndex = 10;
            // 
            // _lblEmptyPaperTitle
            // 
            this._lblEmptyPaperTitle.AutoSize = true;
            this._lblEmptyPaperTitle.ForeColor = System.Drawing.Color.LightGray;
            this._lblEmptyPaperTitle.Location = new System.Drawing.Point(20, 211);
            this._lblEmptyPaperTitle.Name = "_lblEmptyPaperTitle";
            this._lblEmptyPaperTitle.Size = new System.Drawing.Size(127, 15);
            this._lblEmptyPaperTitle.TabIndex = 9;
            this._lblEmptyPaperTitle.Text = "Colore Sfondo (Paper):";
            // 
            // _cmbEmptyInk
            // 
            this._cmbEmptyInk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbEmptyInk.FormattingEnabled = true;
            this._cmbEmptyInk.Location = new System.Drawing.Point(200, 173);
            this._cmbEmptyInk.Name = "_cmbEmptyInk";
            this._cmbEmptyInk.Size = new System.Drawing.Size(150, 23);
            this._cmbEmptyInk.TabIndex = 8;
            // 
            // _lblEmptyInkTitle
            // 
            this._lblEmptyInkTitle.AutoSize = true;
            this._lblEmptyInkTitle.ForeColor = System.Drawing.Color.LightGray;
            this._lblEmptyInkTitle.Location = new System.Drawing.Point(20, 176);
            this._lblEmptyInkTitle.Name = "_lblEmptyInkTitle";
            this._lblEmptyInkTitle.Size = new System.Drawing.Size(107, 15);
            this._lblEmptyInkTitle.TabIndex = 7;
            this._lblEmptyInkTitle.Text = "Colore Tratto (Ink):";
            // 
            // _rbNumbered
            // 
            this._rbNumbered.AutoSize = true;
            this._rbNumbered.ForeColor = System.Drawing.Color.White;
            this._rbNumbered.Location = new System.Drawing.Point(200, 131);
            this._rbNumbered.Name = "_rbNumbered";
            this._rbNumbered.Size = new System.Drawing.Size(206, 19);
            this._rbNumbered.TabIndex = 6;
            this._rbNumbered.Text = "Sequenziale numerico (_0, _1, _2...)";
            this._rbNumbered.UseVisualStyleBackColor = true;
            // 
            // _rbDirections
            // 
            this._rbDirections.AutoSize = true;
            this._rbDirections.Checked = true;
            this._rbDirections.ForeColor = System.Drawing.Color.White;
            this._rbDirections.Location = new System.Drawing.Point(200, 103);
            this._rbDirections.Name = "_rbDirections";
            this._rbDirections.Size = new System.Drawing.Size(243, 19);
            this._rbDirections.TabIndex = 5;
            this._rbDirections.TabStop = true;
            this._rbDirections.Text = "4 Direzioni (_Nord, _Est, _Sud, _Ovest)";
            this._rbDirections.UseVisualStyleBackColor = true;
            // 
            // _lblEmptyNamingTitle
            // 
            this._lblEmptyNamingTitle.AutoSize = true;
            this._lblEmptyNamingTitle.ForeColor = System.Drawing.Color.LightGray;
            this._lblEmptyNamingTitle.Location = new System.Drawing.Point(20, 105);
            this._lblEmptyNamingTitle.Name = "_lblEmptyNamingTitle";
            this._lblEmptyNamingTitle.Size = new System.Drawing.Size(78, 15);
            this._lblEmptyNamingTitle.TabIndex = 4;
            this._lblEmptyNamingTitle.Text = "Suffisso nomi:";
            // 
            // _numFrameCount
            // 
            this._numFrameCount.Location = new System.Drawing.Point(200, 62);
            this._numFrameCount.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this._numFrameCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numFrameCount.Name = "_numFrameCount";
            this._numFrameCount.Size = new System.Drawing.Size(80, 23);
            this._numFrameCount.TabIndex = 3;
            this._numFrameCount.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // _lblEmptyCountTitle
            // 
            this._lblEmptyCountTitle.AutoSize = true;
            this._lblEmptyCountTitle.ForeColor = System.Drawing.Color.LightGray;
            this._lblEmptyCountTitle.Location = new System.Drawing.Point(20, 65);
            this._lblEmptyCountTitle.Name = "_lblEmptyCountTitle";
            this._lblEmptyCountTitle.Size = new System.Drawing.Size(134, 15);
            this._lblEmptyCountTitle.TabIndex = 2;
            this._lblEmptyCountTitle.Text = "Numero di sprite/frame:";
            // 
            // _txtEmptyPrefix
            // 
            this._txtEmptyPrefix.Location = new System.Drawing.Point(200, 22);
            this._txtEmptyPrefix.Name = "_txtEmptyPrefix";
            this._txtEmptyPrefix.Size = new System.Drawing.Size(220, 23);
            this._txtEmptyPrefix.TabIndex = 1;
            this._txtEmptyPrefix.Text = "Nemico_1";
            // 
            // _lblEmptyPrefixTitle
            // 
            this._lblEmptyPrefixTitle.AutoSize = true;
            this._lblEmptyPrefixTitle.ForeColor = System.Drawing.Color.LightGray;
            this._lblEmptyPrefixTitle.Location = new System.Drawing.Point(20, 25);
            this._lblEmptyPrefixTitle.Name = "_lblEmptyPrefixTitle";
            this._lblEmptyPrefixTitle.Size = new System.Drawing.Size(136, 15);
            this._lblEmptyPrefixTitle.TabIndex = 0;
            this._lblEmptyPrefixTitle.Text = "Nome base del nemico:";
            // 
            // _tabPaste
            // 
            this._tabPaste.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(33)))));
            this._tabPaste.Controls.Add(this._btnAddPasted);
            this._tabPaste.Controls.Add(this._lblPasteStats);
            this._tabPaste.Controls.Add(this._txtPastePrefix);
            this._tabPaste.Controls.Add(this._lblPastePrefixTitle);
            this._tabPaste.Controls.Add(this._txtPasteCode);
            this._tabPaste.Controls.Add(this._btnPasteFromClipboard);
            this._tabPaste.Controls.Add(this._lblPasteInfo);
            this._tabPaste.Location = new System.Drawing.Point(4, 24);
            this._tabPaste.Name = "_tabPaste";
            this._tabPaste.Padding = new System.Windows.Forms.Padding(3);
            this._tabPaste.Size = new System.Drawing.Size(696, 453);
            this._tabPaste.TabIndex = 2;
            this._tabPaste.Text = "Incolla Dati Assembly";
            // 
            // _btnAddPasted
            // 
            this._btnAddPasted.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(150)))), ((int)(((byte)(80)))));
            this._btnAddPasted.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnAddPasted.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this._btnAddPasted.ForeColor = System.Drawing.Color.White;
            this._btnAddPasted.Location = new System.Drawing.Point(20, 390);
            this._btnAddPasted.Name = "_btnAddPasted";
            this._btnAddPasted.Size = new System.Drawing.Size(320, 38);
            this._btnAddPasted.TabIndex = 6;
            this._btnAddPasted.Text = "➕ Aggiungi Sprite Rilevati al Progetto";
            this._btnAddPasted.UseVisualStyleBackColor = false;
            this._btnAddPasted.Click += new System.EventHandler(this.BtnAddPasted_Click);
            // 
            // _lblPasteStats
            // 
            this._lblPasteStats.AutoSize = true;
            this._lblPasteStats.ForeColor = System.Drawing.Color.Yellow;
            this._lblPasteStats.Location = new System.Drawing.Point(20, 360);
            this._lblPasteStats.Name = "_lblPasteStats";
            this._lblPasteStats.Size = new System.Drawing.Size(269, 15);
            this._lblPasteStats.TabIndex = 5;
            this._lblPasteStats.Text = "Dati rilevati: 0 byte | Sprite completi (16x16 px): 0";
            // 
            // _txtPastePrefix
            // 
            this._txtPastePrefix.Location = new System.Drawing.Point(120, 327);
            this._txtPastePrefix.Name = "_txtPastePrefix";
            this._txtPastePrefix.Size = new System.Drawing.Size(200, 23);
            this._txtPastePrefix.TabIndex = 4;
            this._txtPastePrefix.Text = "Nemico_1";
            // 
            // _lblPastePrefixTitle
            // 
            this._lblPastePrefixTitle.AutoSize = true;
            this._lblPastePrefixTitle.ForeColor = System.Drawing.Color.LightGray;
            this._lblPastePrefixTitle.Location = new System.Drawing.Point(20, 330);
            this._lblPastePrefixTitle.Name = "_lblPastePrefixTitle";
            this._lblPastePrefixTitle.Size = new System.Drawing.Size(85, 15);
            this._lblPastePrefixTitle.TabIndex = 3;
            this._lblPastePrefixTitle.Text = "Prefisso Nome:";
            // 
            // _txtPasteCode
            // 
            this._txtPasteCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(24)))));
            this._txtPasteCode.Font = new System.Drawing.Font("Consolas", 9.5F);
            this._txtPasteCode.ForeColor = System.Drawing.Color.LightGreen;
            this._txtPasteCode.Location = new System.Drawing.Point(20, 45);
            this._txtPasteCode.Multiline = true;
            this._txtPasteCode.Name = "_txtPasteCode";
            this._txtPasteCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._txtPasteCode.Size = new System.Drawing.Size(650, 270);
            this._txtPasteCode.TabIndex = 2;
            this._txtPasteCode.TextChanged += new System.EventHandler(this.TxtPasteCode_TextChanged);
            // 
            // _btnPasteFromClipboard
            // 
            this._btnPasteFromClipboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this._btnPasteFromClipboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnPasteFromClipboard.ForeColor = System.Drawing.Color.White;
            this._btnPasteFromClipboard.Location = new System.Drawing.Point(520, 10);
            this._btnPasteFromClipboard.Name = "_btnPasteFromClipboard";
            this._btnPasteFromClipboard.Size = new System.Drawing.Size(150, 30);
            this._btnPasteFromClipboard.TabIndex = 1;
            this._btnPasteFromClipboard.Text = "📋 Incolla da Appunti";
            this._btnPasteFromClipboard.UseVisualStyleBackColor = false;
            this._btnPasteFromClipboard.Click += new System.EventHandler(this.BtnPasteFromClipboard_Click);
            // 
            // _lblPasteInfo
            // 
            this._lblPasteInfo.AutoSize = true;
            this._lblPasteInfo.ForeColor = System.Drawing.Color.LightGray;
            this._lblPasteInfo.Location = new System.Drawing.Point(20, 15);
            this._lblPasteInfo.Name = "_lblPasteInfo";
            this._lblPasteInfo.Size = new System.Drawing.Size(489, 15);
            this._lblPasteInfo.TabIndex = 0;
            this._lblPasteInfo.Text = "Incolla qui le direttive \'db\' o \'defb\' contenenti i dati dei nemici (es. da listati Z80 o altri sprite):";
            // 
            // AddEnemiesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(38)))));
            this.CancelButton = this._btnPresetCancel;
            this.ClientSize = new System.Drawing.Size(704, 481);
            this.Controls.Add(this._tabControl);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEnemiesDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Aggiungi Sprite Nemici al Progetto";
            this._tabControl.ResumeLayout(false);
            this._tabPresets.ResumeLayout(false);
            this._tabPresets.PerformLayout();
            this._tabEmpty.ResumeLayout(false);
            this._tabEmpty.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numFrameCount)).EndInit();
            this._tabPaste.ResumeLayout(false);
            this._tabPaste.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.TabPage _tabPresets;
        private System.Windows.Forms.TabPage _tabEmpty;
        private System.Windows.Forms.TabPage _tabPaste;
        private System.Windows.Forms.ComboBox _cmbPresets;
        private System.Windows.Forms.Label _lblPresetDesc;
        private System.Windows.Forms.TextBox _txtPresetPrefix;
        private System.Windows.Forms.Panel _pnlPresetPreview;
        private System.Windows.Forms.Button _btnAddPreset;
        private System.Windows.Forms.Button _btnPresetCancel;
        private System.Windows.Forms.Label _lblPresetChoose;
        private System.Windows.Forms.Label _lblPresetPrefixTitle;
        private System.Windows.Forms.Label _lblPresetPreviewTitle;
        private System.Windows.Forms.TextBox _txtEmptyPrefix;
        private System.Windows.Forms.NumericUpDown _numFrameCount;
        private System.Windows.Forms.RadioButton _rbDirections;
        private System.Windows.Forms.RadioButton _rbNumbered;
        private System.Windows.Forms.ComboBox _cmbEmptyInk;
        private System.Windows.Forms.ComboBox _cmbEmptyPaper;
        private System.Windows.Forms.CheckBox _chkEmptyBright;
        private System.Windows.Forms.Button _btnAddEmpty;
        private System.Windows.Forms.Label _lblEmptyPrefixTitle;
        private System.Windows.Forms.Label _lblEmptyCountTitle;
        private System.Windows.Forms.Label _lblEmptyNamingTitle;
        private System.Windows.Forms.Label _lblEmptyInkTitle;
        private System.Windows.Forms.Label _lblEmptyPaperTitle;
        private System.Windows.Forms.TextBox _txtPasteCode;
        private System.Windows.Forms.TextBox _txtPastePrefix;
        private System.Windows.Forms.Label _lblPasteStats;
        private System.Windows.Forms.Button _btnPasteFromClipboard;
        private System.Windows.Forms.Button _btnAddPasted;
        private System.Windows.Forms.Label _lblPasteInfo;
        private System.Windows.Forms.Label _lblPastePrefixTitle;
    }
}
