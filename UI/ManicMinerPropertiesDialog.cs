using System;
using System.Drawing;
using System.Windows.Forms;
using ZXSpectrumSpriteGenerator.Models;

namespace ZXSpectrumSpriteGenerator.UI
{
    /// <summary>
    /// Dialog per la modifica delle proprietà e dei parametri specifici di una Caverna di Manic Miner
    /// conforme alla specifica a 1024 byte di SkoolKit 45056 (Willy spawn, portale, nastro, oggetti, guardiani, aria).
    /// </summary>
    public class ManicMinerPropertiesDialog : Form
    {
        private readonly ManicMinerCavern _cavern;
        private readonly GameMap _map;

        private TextBox _txtName;
        private NumericUpDown _numCavernIndex;
        private ComboBox _cmbBorderColor;
        private NumericUpDown _numAir;

        // Willy
        private NumericUpDown _numWillyX;
        private NumericUpDown _numWillyY;
        private ComboBox _cmbWillyDir;

        // Conveyor
        private NumericUpDown _numConvX;
        private NumericUpDown _numConvY;
        private NumericUpDown _numConvLen;
        private ComboBox _cmbConvDir;

        // Portal
        private NumericUpDown _numPortalX;
        private NumericUpDown _numPortalY;

        // Items & Guardians summary
        private Label _lblItemsCount;

        private Button _btnSave;
        private Button _btnCancel;

        public ManicMinerPropertiesDialog(GameMap map)
        {
            _map = map ?? throw new ArgumentNullException(nameof(map));
            _cavern = map.ManicMinerData ?? new ManicMinerCavern();

            InitializeComponent();
            LoadCavernData();
        }

        private void InitializeComponent()
        {
            this.Text = "Parametri Caverna Manic Miner (SkoolKit 45056 / $B000)";
            this.Size = new Size(540, 560);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(28, 28, 34);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 9f);

            int leftLabel = 20;
            int leftCtrl = 180;
            int ctrlWidth = 310;
            int y = 20;
            int rowH = 34;

            // Header
            Label lblHeader = new Label
            {
                Text = "🕹️ Configurazione Caverna (1024 Byte - Manic Miner)",
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 220, 255),
                Left = leftLabel,
                Top = y,
                Width = 480,
                Height = 26
            };
            this.Controls.Add(lblHeader);
            y += 34;

            // Nome Caverna (max 32 caratteri)
            Label lblName = new Label { Text = "Nome Caverna (max 32):", Left = leftLabel, Top = y + 3, Width = 150 };
            _txtName = new TextBox { Left = leftCtrl, Top = y, Width = ctrlWidth, MaxLength = 32, BackColor = Color.FromArgb(40, 40, 48), ForeColor = Color.White };
            this.Controls.AddRange(new Control[] { lblName, _txtName });
            y += rowH;

            // Indice Caverna (0..19)
            Label lblIndex = new Label { Text = "Numero Caverna (0..19):", Left = leftLabel, Top = y + 3, Width = 150 };
            _numCavernIndex = new NumericUpDown { Left = leftCtrl, Top = y, Width = 90, Minimum = 0, Maximum = 19, BackColor = Color.FromArgb(40, 40, 48), ForeColor = Color.White };
            this.Controls.AddRange(new Control[] { lblIndex, _numCavernIndex });
            y += rowH;

            // Colore Bordo
            Label lblBorder = new Label { Text = "Colore Bordo Spectrum:", Left = leftLabel, Top = y + 3, Width = 150 };
            _cmbBorderColor = new ComboBox { Left = leftCtrl, Top = y, Width = 180, DropDownStyle = ComboBoxStyle.DropDownList, BackColor = Color.FromArgb(40, 40, 48), ForeColor = Color.White };
            for (int i = 0; i < 8; i++) _cmbBorderColor.Items.Add($"{i}: {ZxSpectrumPalette.ColorNames[i]}");
            this.Controls.AddRange(new Control[] { lblBorder, _cmbBorderColor });
            y += rowH;

            // Riserva d'Aria
            Label lblAir = new Label { Text = "Riserva d'Aria Iniziale:", Left = leftLabel, Top = y + 3, Width = 150 };
            _numAir = new NumericUpDown { Left = leftCtrl, Top = y, Width = 90, Minimum = 1, Maximum = 255, Value = 63, BackColor = Color.FromArgb(40, 40, 48), ForeColor = Color.White };
            this.Controls.AddRange(new Control[] { lblAir, _numAir });
            y += rowH + 6;

            // --- Willy Group ---
            GroupBox gbWilly = new GroupBox
            {
                Text = "🚩 Miner Willy (Posizione di Partenza)",
                Left = leftLabel,
                Top = y,
                Width = 480,
                Height = 65,
                ForeColor = Color.FromArgb(0, 255, 160)
            };
            Label lblWX = new Label { Text = "Colonna X (0..31):", Left = 15, Top = 25, AutoSize = true, ForeColor = Color.White };
            _numWillyX = new NumericUpDown { Left = 130, Top = 22, Width = 60, Minimum = 0, Maximum = 31, BackColor = Color.FromArgb(40, 40, 48), ForeColor = Color.White };
            Label lblWY = new Label { Text = "Riga Y (0..15):", Left = 205, Top = 25, AutoSize = true, ForeColor = Color.White };
            _numWillyY = new NumericUpDown { Left = 295, Top = 22, Width = 60, Minimum = 0, Maximum = 15, BackColor = Color.FromArgb(40, 40, 48), ForeColor = Color.White };
            _cmbWillyDir = new ComboBox { Left = 370, Top = 22, Width = 95, DropDownStyle = ComboBoxStyle.DropDownList, BackColor = Color.FromArgb(40, 40, 48), ForeColor = Color.White };
            _cmbWillyDir.Items.AddRange(new object[] { "Destra ►", "Sinistra ◄" });
            gbWilly.Controls.AddRange(new Control[] { lblWX, _numWillyX, lblWY, _numWillyY, _cmbWillyDir });
            this.Controls.Add(gbWilly);
            y += 75;

            // --- Conveyor Group ---
            GroupBox gbConv = new GroupBox
            {
                Text = "⏩ Nastro Trasportatore (Conveyor Belt)",
                Left = leftLabel,
                Top = y,
                Width = 480,
                Height = 65,
                ForeColor = Color.FromArgb(255, 200, 0)
            };
            Label lblCX = new Label { Text = "X:", Left = 15, Top = 25, AutoSize = true, ForeColor = Color.White };
            _numConvX = new NumericUpDown { Left = 35, Top = 22, Width = 55, Minimum = 0, Maximum = 31, BackColor = Color.FromArgb(40, 40, 48), ForeColor = Color.White };
            Label lblCY = new Label { Text = "Y:", Left = 100, Top = 25, AutoSize = true, ForeColor = Color.White };
            _numConvY = new NumericUpDown { Left = 120, Top = 22, Width = 55, Minimum = 0, Maximum = 15, BackColor = Color.FromArgb(40, 40, 48), ForeColor = Color.White };
            Label lblCL = new Label { Text = "Lungh:", Left = 185, Top = 25, AutoSize = true, ForeColor = Color.White };
            _numConvLen = new NumericUpDown { Left = 235, Top = 22, Width = 60, Minimum = 0, Maximum = 32, BackColor = Color.FromArgb(40, 40, 48), ForeColor = Color.White };
            _cmbConvDir = new ComboBox { Left = 310, Top = 22, Width = 155, DropDownStyle = ComboBoxStyle.DropDownList, BackColor = Color.FromArgb(40, 40, 48), ForeColor = Color.White };
            _cmbConvDir.Items.AddRange(new object[] { "Scorrimento Sinistra ◄", "Scorrimento Destra ►" });
            gbConv.Controls.AddRange(new Control[] { lblCX, _numConvX, lblCY, _numConvY, lblCL, _numConvLen, _cmbConvDir });
            this.Controls.Add(gbConv);
            y += 75;

            // --- Portal Group ---
            GroupBox gbPortal = new GroupBox
            {
                Text = "🚪 Portale di Uscita (16x16 Pixel / 2x2 Caratteri)",
                Left = leftLabel,
                Top = y,
                Width = 480,
                Height = 65,
                ForeColor = Color.FromArgb(255, 100, 220)
            };
            Label lblPX = new Label { Text = "Colonna X (0..30):", Left = 15, Top = 25, AutoSize = true, ForeColor = Color.White };
            _numPortalX = new NumericUpDown { Left = 135, Top = 22, Width = 65, Minimum = 0, Maximum = 30, BackColor = Color.FromArgb(40, 40, 48), ForeColor = Color.White };
            Label lblPY = new Label { Text = "Riga Y (0..14):", Left = 220, Top = 25, AutoSize = true, ForeColor = Color.White };
            _numPortalY = new NumericUpDown { Left = 310, Top = 22, Width = 65, Minimum = 0, Maximum = 14, BackColor = Color.FromArgb(40, 40, 48), ForeColor = Color.White };
            gbPortal.Controls.AddRange(new Control[] { lblPX, _numPortalX, lblPY, _numPortalY });
            this.Controls.Add(gbPortal);
            y += 75;

            // Info Items e Guardiani
            _lblItemsCount = new Label
            {
                Text = "💎 Oggetti (Chiavi): 0/5 presenti  |  👾 Guardiani Orizzontali: 0/4",
                Left = leftLabel,
                Top = y,
                Width = 480,
                ForeColor = Color.FromArgb(200, 220, 255),
                Font = new Font("Segoe UI", 8.8f, FontStyle.Italic)
            };
            this.Controls.Add(_lblItemsCount);
            y += 28;

            // Pulsanti Salva / Annulla
            _btnSave = new Button
            {
                Text = "💾 Salva Parametri",
                Left = 240,
                Top = y,
                Width = 140,
                Height = 32,
                BackColor = Color.FromArgb(0, 130, 200),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };
            _btnSave.Click += (s, e) => SaveData();

            _btnCancel = new Button
            {
                Text = "Annulla",
                Left = 390,
                Top = y,
                Width = 110,
                Height = 32,
                BackColor = Color.FromArgb(60, 60, 70),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            _btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            this.Controls.AddRange(new Control[] { _btnSave, _btnCancel });
        }

        private void LoadCavernData()
        {
            _txtName.Text = _cavern.Name;
            _numCavernIndex.Value = Math.Max(0, Math.Min(19, _cavern.CavernIndex));
            _cmbBorderColor.SelectedIndex = Math.Max(0, Math.Min(7, (int)_cavern.BorderColor));
            _numAir.Value = Math.Max(1, Math.Min(255, (int)_cavern.InitialAir));

            _numWillyX.Value = Math.Max(0, Math.Min(31, _cavern.WillyX));
            _numWillyY.Value = Math.Max(0, Math.Min(15, _cavern.WillyY));
            _cmbWillyDir.SelectedIndex = (_cavern.WillyDirection == 0) ? 0 : 1;

            _numConvX.Value = Math.Max(0, Math.Min(31, _cavern.ConveyorX));
            _numConvY.Value = Math.Max(0, Math.Min(15, _cavern.ConveyorY));
            _numConvLen.Value = Math.Max(0, Math.Min(32, _cavern.ConveyorLength));
            _cmbConvDir.SelectedIndex = (_cavern.ConveyorDirection == 0) ? 0 : 1;

            _numPortalX.Value = Math.Max(0, Math.Min(30, _cavern.PortalX));
            _numPortalY.Value = Math.Max(0, Math.Min(14, _cavern.PortalY));

            int itCount = _cavern.Items != null ? _cavern.Items.Count : 0;
            int gCount = _cavern.HorizontalGuardians != null ? _cavern.HorizontalGuardians.Count : 0;
            _lblItemsCount.Text = $"💎 Oggetti (Chiavi): {itCount}/5 presenti  |  👾 Guardiani Orizzontali: {gCount}/4 configurati\n(Usa gli strumenti 💎 e 👾 della barra mappa per aggiungerli/posizionarli)";
        }

        private void SaveData()
        {
            _cavern.Name = _txtName.Text.Trim();
            if (string.IsNullOrEmpty(_cavern.Name)) _cavern.Name = $"Caverna {_numCavernIndex.Value}";
            _map.Name = _cavern.Name;

            _cavern.CavernIndex = (int)_numCavernIndex.Value;
            _cavern.BorderColor = (byte)_cmbBorderColor.SelectedIndex;
            _cavern.InitialAir = (byte)_numAir.Value;

            _cavern.WillyX = (int)_numWillyX.Value;
            _cavern.WillyY = (int)_numWillyY.Value;
            _cavern.WillyPixelY = _cavern.WillyY * 8;
            _cavern.WillyDirection = _cmbWillyDir.SelectedIndex;

            _cavern.ConveyorX = (int)_numConvX.Value;
            _cavern.ConveyorY = (int)_numConvY.Value;
            _cavern.ConveyorLength = (int)_numConvLen.Value;
            _cavern.ConveyorDirection = _cmbConvDir.SelectedIndex;

            _cavern.PortalX = (int)_numPortalX.Value;
            _cavern.PortalY = (int)_numPortalY.Value;

            _map.ManicMinerData = _cavern;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
