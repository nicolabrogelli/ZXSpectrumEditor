namespace ZXSpectrumSpriteGenerator.UI
{
    partial class ImportExportDialog
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
            this._tabCont = new System.Windows.Forms.TabControl();
            this._tabExport = new System.Windows.Forms.TabPage();
            this._txtExportCode = new System.Windows.Forms.TextBox();
            this._pnlExportTop = new System.Windows.Forms.Panel();
            this._btnSaveExportFile = new System.Windows.Forms.Button();
            this._btnCopyExport = new System.Windows.Forms.Button();
            this._chkIncludeComments = new System.Windows.Forms.CheckBox();
            this._chkIncludeLabels = new System.Windows.Forms.CheckBox();
            this._chkExportAll = new System.Windows.Forms.CheckBox();
            this._cmbMaskMode = new System.Windows.Forms.ComboBox();
            this._lblMaskMode = new System.Windows.Forms.Label();
            this._cmbDirective = new System.Windows.Forms.ComboBox();
            this._lblDirective = new System.Windows.Forms.Label();
            this._cmbNumberFormat = new System.Windows.Forms.ComboBox();
            this._lblNumberFormat = new System.Windows.Forms.Label();
            this._cmbExportType = new System.Windows.Forms.ComboBox();
            this._lblExportType = new System.Windows.Forms.Label();
            this._tabImport = new System.Windows.Forms.TabPage();
            this._txtImportCode = new System.Windows.Forms.TextBox();
            this._pnlImportTop = new System.Windows.Forms.Panel();
            this._btnDoImport = new System.Windows.Forms.Button();
            this._lblImportStats = new System.Windows.Forms.Label();
            this._btnLoadBinaryFile = new System.Windows.Forms.Button();
            this._btnPasteClipboard = new System.Windows.Forms.Button();
            this._numImportH = new System.Windows.Forms.NumericUpDown();
            this._lblImportH = new System.Windows.Forms.Label();
            this._numImportW = new System.Windows.Forms.NumericUpDown();
            this._lblImportW = new System.Windows.Forms.Label();
            this._tabCont.SuspendLayout();
            this._tabExport.SuspendLayout();
            this._pnlExportTop.SuspendLayout();
            this._tabImport.SuspendLayout();
            this._pnlImportTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numImportH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._numImportW)).BeginInit();
            this.SuspendLayout();
            // 
            // _tabCont
            // 
            this._tabCont.Controls.Add(this._tabExport);
            this._tabCont.Controls.Add(this._tabImport);
            this._tabCont.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabCont.Location = new System.Drawing.Point(0, 0);
            this._tabCont.Name = "_tabCont";
            this._tabCont.SelectedIndex = 0;
            this._tabCont.Size = new System.Drawing.Size(804, 561);
            this._tabCont.TabIndex = 0;
            // 
            // _tabExport
            // 
            this._tabExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(38)))));
            this._tabExport.Controls.Add(this._txtExportCode);
            this._tabExport.Controls.Add(this._pnlExportTop);
            this._tabExport.Location = new System.Drawing.Point(4, 24);
            this._tabExport.Name = "_tabExport";
            this._tabExport.Padding = new System.Windows.Forms.Padding(3);
            this._tabExport.Size = new System.Drawing.Size(796, 533);
            this._tabExport.TabIndex = 0;
            this._tabExport.Text = "Esporta Codice / Dati";
            // 
            // _txtExportCode
            // 
            this._txtExportCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(24)))));
            this._txtExportCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this._txtExportCode.Font = new System.Drawing.Font("Consolas", 10F);
            this._txtExportCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this._txtExportCode.Location = new System.Drawing.Point(3, 98);
            this._txtExportCode.Multiline = true;
            this._txtExportCode.Name = "_txtExportCode";
            this._txtExportCode.ReadOnly = true;
            this._txtExportCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._txtExportCode.Size = new System.Drawing.Size(790, 432);
            this._txtExportCode.TabIndex = 1;
            // 
            // _pnlExportTop
            // 
            this._pnlExportTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(32)))));
            this._pnlExportTop.Controls.Add(this._btnSaveExportFile);
            this._pnlExportTop.Controls.Add(this._btnCopyExport);
            this._pnlExportTop.Controls.Add(this._chkIncludeComments);
            this._pnlExportTop.Controls.Add(this._chkIncludeLabels);
            this._pnlExportTop.Controls.Add(this._chkExportAll);
            this._pnlExportTop.Controls.Add(this._cmbMaskMode);
            this._pnlExportTop.Controls.Add(this._lblMaskMode);
            this._pnlExportTop.Controls.Add(this._cmbDirective);
            this._pnlExportTop.Controls.Add(this._lblDirective);
            this._pnlExportTop.Controls.Add(this._cmbNumberFormat);
            this._pnlExportTop.Controls.Add(this._lblNumberFormat);
            this._pnlExportTop.Controls.Add(this._cmbExportType);
            this._pnlExportTop.Controls.Add(this._lblExportType);
            this._pnlExportTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._pnlExportTop.Location = new System.Drawing.Point(3, 3);
            this._pnlExportTop.Name = "_pnlExportTop";
            this._pnlExportTop.Size = new System.Drawing.Size(790, 95);
            this._pnlExportTop.TabIndex = 0;
            // 
            // _btnSaveExportFile
            // 
            this._btnSaveExportFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this._btnSaveExportFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnSaveExportFile.ForeColor = System.Drawing.Color.White;
            this._btnSaveExportFile.Location = new System.Drawing.Point(635, 46);
            this._btnSaveExportFile.Name = "_btnSaveExportFile";
            this._btnSaveExportFile.Size = new System.Drawing.Size(150, 32);
            this._btnSaveExportFile.TabIndex = 12;
            this._btnSaveExportFile.Text = "Salva su File...";
            this._btnSaveExportFile.UseVisualStyleBackColor = false;
            this._btnSaveExportFile.Click += new System.EventHandler(this.BtnSaveExportFile_Click);
            // 
            // _btnCopyExport
            // 
            this._btnCopyExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this._btnCopyExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnCopyExport.ForeColor = System.Drawing.Color.White;
            this._btnCopyExport.Location = new System.Drawing.Point(470, 46);
            this._btnCopyExport.Name = "_btnCopyExport";
            this._btnCopyExport.Size = new System.Drawing.Size(150, 32);
            this._btnCopyExport.TabIndex = 11;
            this._btnCopyExport.Text = "Copia negli Appunti";
            this._btnCopyExport.UseVisualStyleBackColor = false;
            this._btnCopyExport.Click += new System.EventHandler(this.BtnCopyExport_Click);
            // 
            // _chkIncludeComments
            // 
            this._chkIncludeComments.AutoSize = true;
            this._chkIncludeComments.Checked = true;
            this._chkIncludeComments.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkIncludeComments.ForeColor = System.Drawing.Color.White;
            this._chkIncludeComments.Location = new System.Drawing.Point(300, 50);
            this._chkIncludeComments.Name = "_chkIncludeComments";
            this._chkIncludeComments.Size = new System.Drawing.Size(117, 19);
            this._chkIncludeComments.TabIndex = 10;
            this._chkIncludeComments.Text = "Includi commenti";
            this._chkIncludeComments.UseVisualStyleBackColor = true;
            this._chkIncludeComments.CheckedChanged += new System.EventHandler(this.ChkIncludeComments_CheckedChanged);
            // 
            // _chkIncludeLabels
            // 
            this._chkIncludeLabels.AutoSize = true;
            this._chkIncludeLabels.Checked = true;
            this._chkIncludeLabels.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkIncludeLabels.ForeColor = System.Drawing.Color.White;
            this._chkIncludeLabels.Location = new System.Drawing.Point(175, 50);
            this._chkIncludeLabels.Name = "_chkIncludeLabels";
            this._chkIncludeLabels.Size = new System.Drawing.Size(112, 19);
            this._chkIncludeLabels.TabIndex = 9;
            this._chkIncludeLabels.Text = "Includi etichette";
            this._chkIncludeLabels.UseVisualStyleBackColor = true;
            this._chkIncludeLabels.CheckedChanged += new System.EventHandler(this.ChkIncludeLabels_CheckedChanged);
            // 
            // _chkExportAll
            // 
            this._chkExportAll.AutoSize = true;
            this._chkExportAll.Checked = true;
            this._chkExportAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this._chkExportAll.ForeColor = System.Drawing.Color.White;
            this._chkExportAll.Location = new System.Drawing.Point(15, 50);
            this._chkExportAll.Name = "_chkExportAll";
            this._chkExportAll.Size = new System.Drawing.Size(147, 19);
            this._chkExportAll.TabIndex = 8;
            this._chkExportAll.Text = "Esporta tutti gli sprite";
            this._chkExportAll.UseVisualStyleBackColor = true;
            this._chkExportAll.CheckedChanged += new System.EventHandler(this.ChkExportAll_CheckedChanged);
            // 
            // _cmbMaskMode
            // 
            this._cmbMaskMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbMaskMode.FormattingEnabled = true;
            this._cmbMaskMode.Items.AddRange(new object[] {
            "Nessuna",
            "Sprite + Mask",
            "Mask + Sprite",
            "Blocco Separato"});
            this._cmbMaskMode.Location = new System.Drawing.Point(675, 8);
            this._cmbMaskMode.Name = "_cmbMaskMode";
            this._cmbMaskMode.Size = new System.Drawing.Size(120, 23);
            this._cmbMaskMode.TabIndex = 7;
            this._cmbMaskMode.SelectedIndexChanged += new System.EventHandler(this.CmbMaskMode_SelectedIndexChanged);
            // 
            // _lblMaskMode
            // 
            this._lblMaskMode.AutoSize = true;
            this._lblMaskMode.ForeColor = System.Drawing.Color.LightGray;
            this._lblMaskMode.Location = new System.Drawing.Point(605, 12);
            this._lblMaskMode.Name = "_lblMaskMode";
            this._lblMaskMode.Size = new System.Drawing.Size(64, 15);
            this._lblMaskMode.TabIndex = 6;
            this._lblMaskMode.Text = "Maschera:";
            // 
            // _cmbDirective
            // 
            this._cmbDirective.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbDirective.FormattingEnabled = true;
            this._cmbDirective.Items.AddRange(new object[] {
            "db",
            "defb",
            "DEFB",
            ".byte"});
            this._cmbDirective.Location = new System.Drawing.Point(500, 8);
            this._cmbDirective.Name = "_cmbDirective";
            this._cmbDirective.Size = new System.Drawing.Size(90, 23);
            this._cmbDirective.TabIndex = 5;
            this._cmbDirective.SelectedIndexChanged += new System.EventHandler(this.CmbDirective_SelectedIndexChanged);
            // 
            // _lblDirective
            // 
            this._lblDirective.AutoSize = true;
            this._lblDirective.ForeColor = System.Drawing.Color.LightGray;
            this._lblDirective.Location = new System.Drawing.Point(435, 12);
            this._lblDirective.Name = "_lblDirective";
            this._lblDirective.Size = new System.Drawing.Size(56, 15);
            this._lblDirective.TabIndex = 4;
            this._lblDirective.Text = "Direttiva:";
            // 
            // _cmbNumberFormat
            // 
            this._cmbNumberFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbNumberFormat.FormattingEnabled = true;
            this._cmbNumberFormat.Items.AddRange(new object[] {
            "Decimale (0,255)",
            "Hex $FF",
            "Hex 0xFF",
            "Binario %10101010"});
            this._cmbNumberFormat.Location = new System.Drawing.Point(290, 8);
            this._cmbNumberFormat.Name = "_cmbNumberFormat";
            this._cmbNumberFormat.Size = new System.Drawing.Size(130, 23);
            this._cmbNumberFormat.TabIndex = 3;
            this._cmbNumberFormat.SelectedIndexChanged += new System.EventHandler(this.CmbNumberFormat_SelectedIndexChanged);
            // 
            // _lblNumberFormat
            // 
            this._lblNumberFormat.AutoSize = true;
            this._lblNumberFormat.ForeColor = System.Drawing.Color.LightGray;
            this._lblNumberFormat.Location = new System.Drawing.Point(230, 12);
            this._lblNumberFormat.Name = "_lblNumberFormat";
            this._lblNumberFormat.Size = new System.Drawing.Size(52, 15);
            this._lblNumberFormat.TabIndex = 2;
            this._lblNumberFormat.Text = "Numeri:";
            // 
            // _cmbExportType
            // 
            this._cmbExportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmbExportType.FormattingEnabled = true;
            this._cmbExportType.Items.AddRange(new object[] {
            "Assembly Z80",
            "C / Z88DK Header",
            "Sinclair BASIC (DATA)",
            "Binario Grezzo (.bin)"});
            this._cmbExportType.Location = new System.Drawing.Point(75, 8);
            this._cmbExportType.Name = "_cmbExportType";
            this._cmbExportType.Size = new System.Drawing.Size(140, 23);
            this._cmbExportType.TabIndex = 1;
            this._cmbExportType.SelectedIndexChanged += new System.EventHandler(this.CmbExportType_SelectedIndexChanged);
            // 
            // _lblExportType
            // 
            this._lblExportType.AutoSize = true;
            this._lblExportType.ForeColor = System.Drawing.Color.LightGray;
            this._lblExportType.Location = new System.Drawing.Point(15, 12);
            this._lblExportType.Name = "_lblExportType";
            this._lblExportType.Size = new System.Drawing.Size(55, 15);
            this._lblExportType.TabIndex = 0;
            this._lblExportType.Text = "Formato:";
            // 
            // _tabImport
            // 
            this._tabImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(38)))));
            this._tabImport.Controls.Add(this._txtImportCode);
            this._tabImport.Controls.Add(this._pnlImportTop);
            this._tabImport.Location = new System.Drawing.Point(4, 24);
            this._tabImport.Name = "_tabImport";
            this._tabImport.Padding = new System.Windows.Forms.Padding(3);
            this._tabImport.Size = new System.Drawing.Size(796, 533);
            this._tabImport.TabIndex = 1;
            this._tabImport.Text = "Importa Dati / Assembly";
            // 
            // _txtImportCode
            // 
            this._txtImportCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(24)))));
            this._txtImportCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this._txtImportCode.Font = new System.Drawing.Font("Consolas", 10F);
            this._txtImportCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this._txtImportCode.Location = new System.Drawing.Point(3, 83);
            this._txtImportCode.Multiline = true;
            this._txtImportCode.Name = "_txtImportCode";
            this._txtImportCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._txtImportCode.Size = new System.Drawing.Size(790, 447);
            this._txtImportCode.TabIndex = 1;
            this._txtImportCode.TextChanged += new System.EventHandler(this.TxtImportCode_TextChanged);
            // 
            // _pnlImportTop
            // 
            this._pnlImportTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(32)))));
            this._pnlImportTop.Controls.Add(this._btnDoImport);
            this._pnlImportTop.Controls.Add(this._lblImportStats);
            this._pnlImportTop.Controls.Add(this._btnLoadBinaryFile);
            this._pnlImportTop.Controls.Add(this._btnPasteClipboard);
            this._pnlImportTop.Controls.Add(this._numImportH);
            this._pnlImportTop.Controls.Add(this._lblImportH);
            this._pnlImportTop.Controls.Add(this._numImportW);
            this._pnlImportTop.Controls.Add(this._lblImportW);
            this._pnlImportTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._pnlImportTop.Location = new System.Drawing.Point(3, 3);
            this._pnlImportTop.Name = "_pnlImportTop";
            this._pnlImportTop.Size = new System.Drawing.Size(790, 80);
            this._pnlImportTop.TabIndex = 0;
            // 
            // _btnDoImport
            // 
            this._btnDoImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(150)))), ((int)(((byte)(80)))));
            this._btnDoImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnDoImport.ForeColor = System.Drawing.Color.White;
            this._btnDoImport.Location = new System.Drawing.Point(510, 44);
            this._btnDoImport.Name = "_btnDoImport";
            this._btnDoImport.Size = new System.Drawing.Size(160, 30);
            this._btnDoImport.TabIndex = 7;
            this._btnDoImport.Text = "Importa nel Progetto";
            this._btnDoImport.UseVisualStyleBackColor = false;
            this._btnDoImport.Click += new System.EventHandler(this.BtnDoImport_Click);
            // 
            // _lblImportStats
            // 
            this._lblImportStats.AutoSize = true;
            this._lblImportStats.ForeColor = System.Drawing.Color.Yellow;
            this._lblImportStats.Location = new System.Drawing.Point(15, 50);
            this._lblImportStats.Name = "_lblImportStats";
            this._lblImportStats.Size = new System.Drawing.Size(193, 15);
            this._lblImportStats.TabIndex = 6;
            this._lblImportStats.Text = "Byte rilevati: 0 | Sprite generati: 0";
            // 
            // _btnLoadBinaryFile
            // 
            this._btnLoadBinaryFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this._btnLoadBinaryFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnLoadBinaryFile.ForeColor = System.Drawing.Color.White;
            this._btnLoadBinaryFile.Location = new System.Drawing.Point(510, 8);
            this._btnLoadBinaryFile.Name = "_btnLoadBinaryFile";
            this._btnLoadBinaryFile.Size = new System.Drawing.Size(160, 32);
            this._btnLoadBinaryFile.TabIndex = 5;
            this._btnLoadBinaryFile.Text = "Carica da File Binario...";
            this._btnLoadBinaryFile.UseVisualStyleBackColor = false;
            this._btnLoadBinaryFile.Click += new System.EventHandler(this.BtnLoadBinaryFile_Click);
            // 
            // _btnPasteClipboard
            // 
            this._btnPasteClipboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(150)))));
            this._btnPasteClipboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnPasteClipboard.ForeColor = System.Drawing.Color.White;
            this._btnPasteClipboard.Location = new System.Drawing.Point(350, 8);
            this._btnPasteClipboard.Name = "_btnPasteClipboard";
            this._btnPasteClipboard.Size = new System.Drawing.Size(150, 32);
            this._btnPasteClipboard.TabIndex = 4;
            this._btnPasteClipboard.Text = "Incolla da Appunti";
            this._btnPasteClipboard.UseVisualStyleBackColor = false;
            this._btnPasteClipboard.Click += new System.EventHandler(this.BtnPasteClipboard_Click);
            // 
            // _numImportH
            // 
            this._numImportH.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this._numImportH.Location = new System.Drawing.Point(265, 12);
            this._numImportH.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this._numImportH.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this._numImportH.Name = "_numImportH";
            this._numImportH.Size = new System.Drawing.Size(60, 23);
            this._numImportH.TabIndex = 3;
            this._numImportH.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this._numImportH.ValueChanged += new System.EventHandler(this.NumImportH_ValueChanged);
            // 
            // _lblImportH
            // 
            this._lblImportH.AutoSize = true;
            this._lblImportH.ForeColor = System.Drawing.Color.LightGray;
            this._lblImportH.Location = new System.Drawing.Point(190, 14);
            this._lblImportH.Name = "_lblImportH";
            this._lblImportH.Size = new System.Drawing.Size(68, 15);
            this._lblImportH.TabIndex = 2;
            this._lblImportH.Text = "Altezza px:";
            // 
            // _numImportW
            // 
            this._numImportW.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this._numImportW.Location = new System.Drawing.Point(110, 12);
            this._numImportW.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this._numImportW.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this._numImportW.Name = "_numImportW";
            this._numImportW.Size = new System.Drawing.Size(60, 23);
            this._numImportW.TabIndex = 1;
            this._numImportW.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this._numImportW.ValueChanged += new System.EventHandler(this.NumImportW_ValueChanged);
            // 
            // _lblImportW
            // 
            this._lblImportW.AutoSize = true;
            this._lblImportW.ForeColor = System.Drawing.Color.LightGray;
            this._lblImportW.Location = new System.Drawing.Point(15, 14);
            this._lblImportW.Name = "_lblImportW";
            this._lblImportW.Size = new System.Drawing.Size(83, 15);
            this._lblImportW.TabIndex = 0;
            this._lblImportW.Text = "Larghezza px:";
            // 
            // ImportExportDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(804, 561);
            this.Controls.Add(this._tabCont);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "ImportExportDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Importa / Esporta Sprite ZX Spectrum";
            this._tabCont.ResumeLayout(false);
            this._tabExport.ResumeLayout(false);
            this._tabExport.PerformLayout();
            this._pnlExportTop.ResumeLayout(false);
            this._pnlExportTop.PerformLayout();
            this._tabImport.ResumeLayout(false);
            this._tabImport.PerformLayout();
            this._pnlImportTop.ResumeLayout(false);
            this._pnlImportTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numImportH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._numImportW)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl _tabCont;
        private System.Windows.Forms.TabPage _tabExport;
        private System.Windows.Forms.Panel _pnlExportTop;
        private System.Windows.Forms.Label _lblExportType;
        private System.Windows.Forms.ComboBox _cmbExportType;
        private System.Windows.Forms.Label _lblNumberFormat;
        private System.Windows.Forms.ComboBox _cmbNumberFormat;
        private System.Windows.Forms.Label _lblDirective;
        private System.Windows.Forms.ComboBox _cmbDirective;
        private System.Windows.Forms.Label _lblMaskMode;
        private System.Windows.Forms.ComboBox _cmbMaskMode;
        private System.Windows.Forms.CheckBox _chkExportAll;
        private System.Windows.Forms.CheckBox _chkIncludeLabels;
        private System.Windows.Forms.CheckBox _chkIncludeComments;
        private System.Windows.Forms.Button _btnCopyExport;
        private System.Windows.Forms.Button _btnSaveExportFile;
        private System.Windows.Forms.TextBox _txtExportCode;
        private System.Windows.Forms.TabPage _tabImport;
        private System.Windows.Forms.Panel _pnlImportTop;
        private System.Windows.Forms.Label _lblImportW;
        private System.Windows.Forms.NumericUpDown _numImportW;
        private System.Windows.Forms.Label _lblImportH;
        private System.Windows.Forms.NumericUpDown _numImportH;
        private System.Windows.Forms.Button _btnPasteClipboard;
        private System.Windows.Forms.Button _btnLoadBinaryFile;
        private System.Windows.Forms.Label _lblImportStats;
        private System.Windows.Forms.Button _btnDoImport;
        private System.Windows.Forms.TextBox _txtImportCode;
    }
}
