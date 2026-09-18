using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ZXSpectrumSpriteGenerator.Models;
using ZXSpectrumSpriteGenerator.Services;

namespace ZXSpectrumSpriteGenerator.UI
{
    /// <summary>
    /// Finestra per aggiungere più sprite o interi set di nemici (da preset, vuoti a più frame, o da dati assembly).
    /// </summary>
    public partial class AddEnemiesDialog : Form
    {
        private readonly SpriteProject _project;

        public List<Sprite> NewlyAddedSprites { get; private set; } = new List<Sprite>();

        public AddEnemiesDialog() : this(null)
        {
        }

        public AddEnemiesDialog(SpriteProject project)
        {
            _project = project;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;
            InitRuntimeData();
        }

        private void InitRuntimeData()
        {
            // Popola i preset
            _cmbPresets.Items.Clear();
            foreach (var p in EnemyPresets.AllPresets)
            {
                _cmbPresets.Items.Add(p.DisplayName);
            }
            if (_cmbPresets.Items.Count > 0)
            {
                _cmbPresets.SelectedIndex = 0;
            }

            // Prefissi di default
            int nextEnemyNum = (_project != null ? _project.Sprites.Count / 4 : 0) + 1;
            string defaultPrefix = "Nemico_" + nextEnemyNum;
            _txtPresetPrefix.Text = defaultPrefix;
            _txtEmptyPrefix.Text = defaultPrefix;
            _txtPastePrefix.Text = defaultPrefix;

            // Popola Ink e Paper
            _cmbEmptyInk.Items.Clear();
            _cmbEmptyPaper.Items.Clear();
            for (int i = 0; i < 8; i++)
            {
                _cmbEmptyInk.Items.Add($"{i}: {ZxSpectrumPalette.ColorNames[i]}");
                _cmbEmptyPaper.Items.Add($"{i}: {ZxSpectrumPalette.ColorNames[i]}");
            }
            _cmbEmptyInk.SelectedIndex = 7; // White
            _cmbEmptyPaper.SelectedIndex = 0; // Black

            UpdatePresetPreview();
        }

        private void CmbPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePresetPreview();
        }

        private void BtnPresetCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnPasteFromClipboard_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                _txtPasteCode.Text = Clipboard.GetText();
                UpdatePasteStats();
            }
        }

        private void TxtPasteCode_TextChanged(object sender, EventArgs e)
        {
            UpdatePasteStats();
        }

        private void UpdatePresetPreview()
        {
            int idx = _cmbPresets.SelectedIndex;
            if (idx >= 0 && idx < EnemyPresets.AllPresets.Count)
            {
                var p = EnemyPresets.AllPresets[idx];
                _lblPresetDesc.Text = p.Description;
                _pnlPresetPreview.Invalidate();
            }
        }

        private void PnlPresetPreview_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            int idx = _cmbPresets.SelectedIndex;
            if (idx < 0 || idx >= EnemyPresets.AllPresets.Count) return;

            var preset = EnemyPresets.AllPresets[idx];
            int scale = 6;
            int cardW = 16 * scale;
            int cardH = 16 * scale;
            int x = 20;

            for (int f = 0; f < preset.Frames.Count; f++)
            {
                var frame = preset.Frames[f];
                var s = new Sprite(16, 16, frame.FrameName);
                s.SetFromBytesRowByRow(frame.Data);
                s.InkColor = preset.DefaultInk;
                s.PaperColor = preset.DefaultPaper;

                using (Bitmap bmp = s.RenderToBitmap(scale))
                {
                    g.DrawImage(bmp, x, 15);
                }

                using (Pen p = new Pen(Color.FromArgb(80, 80, 100), 1f))
                {
                    g.DrawRectangle(p, x, 15, cardW, cardH);
                }

                using (Font fnt = new Font("Segoe UI", 8f))
                using (Brush b = new SolidBrush(Color.White))
                {
                    g.DrawString($"Frame {f + 1}:\n{frame.FrameName}", fnt, b, x, 15 + cardH + 6);
                }

                x += cardW + 35;
            }
        }

        private void BtnAddPreset_Click(object sender, EventArgs e)
        {
            if (_project == null) return;
            int idx = _cmbPresets.SelectedIndex;
            if (idx < 0 || idx >= EnemyPresets.AllPresets.Count) return;

            var preset = EnemyPresets.AllPresets[idx];
            string prefix = _txtPresetPrefix.Text.Trim();
            if (string.IsNullOrEmpty(prefix)) prefix = "Nemico";

            NewlyAddedSprites.Clear();

            for (int f = 0; f < preset.Frames.Count; f++)
            {
                var frame = preset.Frames[f];
                string name = $"{prefix}_{frame.FrameName}";
                var s = new Sprite(16, 16, name)
                {
                    GroupName = prefix,
                    InkColor = preset.DefaultInk,
                    PaperColor = preset.DefaultPaper,
                    Bright = true
                };
                s.SetFromBytesRowByRow(frame.Data);
                _project.Sprites.Add(s);
                NewlyAddedSprites.Add(s);
            }

            _project.SelectedIndex = _project.Sprites.Count - NewlyAddedSprites.Count;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnAddEmpty_Click(object sender, EventArgs e)
        {
            if (_project == null) return;
            int count = (int)_numFrameCount.Value;
            string prefix = _txtEmptyPrefix.Text.Trim();
            if (string.IsNullOrEmpty(prefix)) prefix = "Nemico";

            string[] dirNames = new string[] { "Nord", "Est", "Sud", "Ovest" };

            NewlyAddedSprites.Clear();

            for (int i = 0; i < count; i++)
            {
                string suffix = (_rbDirections.Checked && i < dirNames.Length) ? $"_{dirNames[i]}" : $"_{i}";
                string name = $"{prefix}{suffix}";

                var s = new Sprite(_project.DefaultWidth, _project.DefaultHeight, name)
                {
                    GroupName = prefix,
                    InkColor = _cmbEmptyInk.SelectedIndex,
                    PaperColor = _cmbEmptyPaper.SelectedIndex,
                    Bright = _chkEmptyBright.Checked
                };

                _project.Sprites.Add(s);
                NewlyAddedSprites.Add(s);
            }

            _project.SelectedIndex = _project.Sprites.Count - NewlyAddedSprites.Count;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void UpdatePasteStats()
        {
            int defaultW = _project?.DefaultWidth ?? 16;
            int defaultH = _project?.DefaultHeight ?? 16;

            var bytes = SpriteImporter.ParseBytesFromText(_txtPasteCode.Text);
            int bytesPerSprite = (defaultW / 8) * defaultH;
            int count = bytesPerSprite > 0 ? bytes.Count / bytesPerSprite : 0;
            int rem = bytesPerSprite > 0 ? bytes.Count % bytesPerSprite : 0;

            string info = $"Dati rilevati: {bytes.Count} byte | Sprite completi ({defaultW}x{defaultH} px): {count}";
            if (rem > 0) info += $" (+{rem} byte parziali)";
            _lblPasteStats.Text = info;
            _btnAddPasted.Enabled = (count > 0);
        }

        private void BtnAddPasted_Click(object sender, EventArgs e)
        {
            if (_project == null) return;
            string prefix = _txtPastePrefix.Text.Trim();
            if (string.IsNullOrEmpty(prefix)) prefix = "Nemico";

            var imported = SpriteImporter.ImportFromText(_txtPasteCode.Text, _project.DefaultWidth, _project.DefaultHeight, prefix);
            if (imported.Count == 0)
            {
                MessageBox.Show("Nessun dato sprite valido trovato.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NewlyAddedSprites.Clear();
            foreach (var s in imported)
            {
                s.GroupName = prefix;
                _project.Sprites.Add(s);
                NewlyAddedSprites.Add(s);
            }

            _project.SelectedIndex = _project.Sprites.Count - NewlyAddedSprites.Count;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
