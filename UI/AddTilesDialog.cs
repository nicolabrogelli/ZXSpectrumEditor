using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ZXSpectrumSpriteGenerator.Models;
using ZXSpectrumSpriteGenerator.Services;

namespace ZXSpectrumSpriteGenerator.UI
{
    /// <summary>
    /// Finestra di dialogo per aggiungere e importare tile 8x8 pixel per le mappe di gioco.
    /// Supporta preset ufficiali ZX Spectrum, generazione di tile vuoti e importazione Assembly/dati grezzi.
    /// </summary>
    public partial class AddTilesDialog : Form
    {
        private readonly SpriteProject _project;

        public List<Tile> NewlyAddedTiles { get; private set; } = new List<Tile>();

        public AddTilesDialog() : this(null)
        {
        }

        public AddTilesDialog(SpriteProject project)
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
            // Popola colori Ink e Paper per preset, empty e paste
            _cmbPresetInk.Items.Clear();
            _cmbPresetPaper.Items.Clear();
            _cmbEmptyInk.Items.Clear();
            _cmbEmptyPaper.Items.Clear();
            _cmbPasteInk.Items.Clear();
            _cmbPastePaper.Items.Clear();
          

            for (int i = 0; i < 8; i++)
            {
                string cName = $"{i}: {ZxSpectrumPalette.ColorNames[i]}";
                _cmbPresetInk.Items.Add(cName);
                _cmbPresetPaper.Items.Add(cName);
                _cmbEmptyInk.Items.Add(cName);
                _cmbEmptyPaper.Items.Add(cName);
                _cmbPasteInk.Items.Add(cName);
                _cmbPastePaper.Items.Add(cName);
            }

            int initialInk = _project?.CurrentTile != null ? _project.CurrentTile.InkColor : 7;
            int initialPaper = _project?.CurrentTile != null ? _project.CurrentTile.PaperColor : 0;
            bool initialBright = _project?.CurrentTile != null ? _project.CurrentTile.Bright : true;

            _cmbPresetInk.SelectedIndex = 7; // White (sarà aggiornato dalla selezione del preset)
            _cmbPresetPaper.SelectedIndex = 0; // Black
            _cmbEmptyInk.SelectedIndex = initialInk;
            _cmbEmptyPaper.SelectedIndex = initialPaper;
            _chkEmptyBright.Checked = initialBright;
            _cmbPasteInk.SelectedIndex = initialInk;
            _cmbPastePaper.SelectedIndex = initialPaper;
            _chkPasteBright.Checked = initialBright;

            // Popola i preset
            _cmbPresets.Items.Clear();
            foreach (var p in TilePresets.AllPresets)
            {
                _cmbPresets.Items.Add(p.DisplayName);
            }
            if (_cmbPresets.Items.Count > 0)
            {
                _cmbPresets.SelectedIndex = 0;
            }

            int nextNum = (_project != null ? _project.Tiles.Count : 0) + 1;
            _txtEmptyPrefix.Text = $"Tile_{nextNum}_";
            _txtPastePrefix.Text = $"Tile_Imp_{nextNum}_";

            UpdatePresetDisplay();
        }

        private void CmbPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePresetDisplay();
        }

        private void UpdatePresetDisplay()
        {
            int idx = _cmbPresets.SelectedIndex;
            if (idx >= 0 && idx < TilePresets.AllPresets.Count)
            {
                var p = TilePresets.AllPresets[idx];
                _txtPresetName.Text = p.DisplayName.Replace(" / ", "_").Replace(" ", "_");
                _lblPresetDesc.Text = p.Description;
                if (_cmbPresetInk.Items.Count > 0)
                    _cmbPresetInk.SelectedIndex = Math.Max(0, Math.Min(7, p.DefaultInk));
                if (_cmbPresetPaper.Items.Count > 0)
                    _cmbPresetPaper.SelectedIndex = Math.Max(0, Math.Min(7, p.DefaultPaper));
                _chkPresetBright.Checked = p.Bright;
                _pnlPresetPreview.Invalidate();
            }
        }

        private void PresetColorChanged(object sender, EventArgs e)
        {
            _pnlPresetPreview.Invalidate();
        }

        private void PnlPresetPreview_Paint(object sender, PaintEventArgs e)
        {
            int idx = _cmbPresets.SelectedIndex;
            if (idx < 0 || idx >= TilePresets.AllPresets.Count) return;

            var p = TilePresets.AllPresets[idx];
            Graphics g = e.Graphics;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;

            int ink = _cmbPresetInk.SelectedIndex >= 0 ? _cmbPresetInk.SelectedIndex : p.DefaultInk;
            int paper = _cmbPresetPaper.SelectedIndex >= 0 ? _cmbPresetPaper.SelectedIndex : p.DefaultPaper;
            bool bright = _chkPresetBright.Checked;

            Tile tempTile = new Tile(p.DisplayName, p.Data)
            {
                InkColor = ink,
                PaperColor = paper,
                Bright = bright,
                Flash = p.Flash
            };

            // Disegna anteprima 1x (8x8)
            using (Bitmap b1 = tempTile.RenderToBitmap(1))
            {
                g.DrawImage(b1, 20, 36);
            }
            using (var b = new SolidBrush(Color.Gray))
            {
                g.DrawString("1x (8x8)", Font, b, 12, 16);
            }

            // Disegna anteprima 4x (32x32)
            using (Bitmap b4 = tempTile.RenderToBitmap(4))
            {
                g.DrawImage(b4, 90, 24);
            }
            using (var b = new SolidBrush(Color.Gray))
            {
                g.DrawString("4x (32x32)", Font, b, 82, 60);
            }

            // Disegna anteprima 8x (64x64)
            using (Bitmap b8 = tempTile.RenderToBitmap(8))
            {
                g.DrawImage(b8, 175, 12);
            }
            using (var b = new SolidBrush(Color.Gray))
            {
                g.DrawString("8x (64x64)", Font, b, 185, 80);
            }

            // Disegna anteprima composizione (muro 4x2 tile)
            using (Bitmap b2 = tempTile.RenderToBitmap(3))
            {
                int startX = 290;
                int startY = 16;
                int step = 8 * 3;
                for (int ry = 0; ry < 2; ry++)
                {
                    for (int rx = 0; rx < 6; rx++)
                    {
                        g.DrawImage(b2, startX + rx * step, startY + ry * step);
                    }
                }
            }
            using (var b = new SolidBrush(Color.FromArgb(0, 220, 255)))
            {
                g.DrawString("Composizione Muro / Superficie:", Font, b, 290, 68);
            }
        }

        private void BtnAddPreset_Click(object sender, EventArgs e)
        {
            int idx = _cmbPresets.SelectedIndex;
            if (idx < 0 || idx >= TilePresets.AllPresets.Count) return;

            var p = TilePresets.AllPresets[idx];
            string tName = _txtPresetName.Text.Trim();
            if (string.IsNullOrEmpty(tName)) tName = p.DisplayName;

            int ink = _cmbPresetInk.SelectedIndex >= 0 ? _cmbPresetInk.SelectedIndex : p.DefaultInk;
            int paper = _cmbPresetPaper.SelectedIndex >= 0 ? _cmbPresetPaper.SelectedIndex : p.DefaultPaper;
            bool bright = _chkPresetBright.Checked;
            bool flash = _chkPresetFlash.Checked;

            Tile tile = new Tile(tName, p.Data)
            {
                InkColor = ink,
                PaperColor = paper,
                Bright = bright,
                Flash = flash //p.Flash
            };

            NewlyAddedTiles.Add(tile);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnAddAllPresets_Click(object sender, EventArgs e)
        {
            foreach (var p in TilePresets.AllPresets)
            {
                Tile tile = new Tile(p.DisplayName, p.Data)
                {
                    InkColor = p.DefaultInk,
                    PaperColor = p.DefaultPaper,
                    Bright = p.Bright,
                    Flash = p.Flash
                };
                NewlyAddedTiles.Add(tile);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnAddEmpty_Click(object sender, EventArgs e)
        {
            int count = (int)_numTileCount.Value;
            string prefix = _txtEmptyPrefix.Text.Trim();
            if (string.IsNullOrEmpty(prefix)) prefix = "Tile_";

            int ink = _cmbEmptyInk.SelectedIndex >= 0 ? _cmbEmptyInk.SelectedIndex : 7;
            int paper = _cmbEmptyPaper.SelectedIndex >= 0 ? _cmbEmptyPaper.SelectedIndex : 0;
            bool bright = _chkEmptyBright.Checked;
            bool flash = _chkEmptyFlash.Checked;

            for (int i = 0; i < count; i++)
            {
                string name = $"{prefix}{i + 1}";
                Tile t = new Tile(name)
                {
                    InkColor = ink,
                    PaperColor = paper,
                    Bright = bright,
                    Flash = flash

                };
                NewlyAddedTiles.Add(t);
            }

            DialogResult = DialogResult.OK;
            Close();
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

        private bool _isAutoDetectingPasteColors = false;

        private void UpdatePasteStats()
        {
            if (!_isAutoDetectingPasteColors)
            {
                _isAutoDetectingPasteColors = true;
                try
                {
                    if (SpriteImporter.TryExtractColorsFromText(_txtPasteCode.Text, out int detectedInk, out int detectedPaper, out bool? detectedBright))
                    {
                        if (detectedInk >= 0 && detectedInk < 8 && _cmbPasteInk.Items.Count > 0)
                            _cmbPasteInk.SelectedIndex = detectedInk;
                        if (detectedPaper >= 0 && detectedPaper < 8 && _cmbPastePaper.Items.Count > 0)
                            _cmbPastePaper.SelectedIndex = detectedPaper;
                        if (detectedBright.HasValue)
                            _chkPasteBright.Checked = detectedBright.Value;
                    }
                }
                finally
                {
                    _isAutoDetectingPasteColors = false;
                }
            }

            List<byte> bytes = SpriteImporter.ParseBytesFromText(_txtPasteCode.Text);
            bool is9Bytes = bytes.Count > 0 && (bytes.Count % 9 == 0) && (bytes.Count % 8 != 0);
            int bytesPerTile = is9Bytes ? 9 : 8;
            int tilesCount = bytesPerTile > 0 ? bytes.Count / bytesPerTile : 0;
            int remaining = bytesPerTile > 0 ? bytes.Count % bytesPerTile : 0;

            string info = $"Dati riconosciuti: {bytes.Count} byte ({tilesCount} tile {(is9Bytes ? "con attributo colore" : "da 8x8")})";
            if (remaining > 0)
            {
                info += $" + {remaining} byte parziali (arrotondati a {tilesCount + 1} tile)";
            }
            _lblPasteStats.Text = info;
            _lblPasteStats.ForeColor = tilesCount > 0 ? Color.FromArgb(0, 255, 180) : Color.FromArgb(255, 150, 150);
        }

        private void BtnAddPasted_Click(object sender, EventArgs e)
        {
            string prefix = _txtPastePrefix.Text.Trim();
            if (string.IsNullOrEmpty(prefix)) prefix = "Tile_Imp_";

            int selInk = _cmbPasteInk.SelectedIndex >= 0 ? _cmbPasteInk.SelectedIndex : 7;
            int selPaper = _cmbPastePaper.SelectedIndex >= 0 ? _cmbPastePaper.SelectedIndex : 0;
            bool selBright = _chkPasteBright.Checked;

            var tiles = SpriteImporter.ImportTilesFromText(_txtPasteCode.Text, prefix, selInk, selPaper, selBright);
            if (tiles.Count == 0)
            {
                MessageBox.Show("Nessun dato byte valido trovato nel testo incollato.\nAssicurati di includere dichiarazioni 'db', 'defb' o liste di byte separati da virgole.", "Nessun Dato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var t in tiles)
            {
                NewlyAddedTiles.Add(t);
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
