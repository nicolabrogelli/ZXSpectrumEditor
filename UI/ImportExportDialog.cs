using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ZXSpectrumSpriteGenerator.Models;
using ZXSpectrumSpriteGenerator.Services;

namespace ZXSpectrumSpriteGenerator.UI
{
    public partial class ImportExportDialog : Form
    {
        private readonly SpriteProject _project;
        public bool ProjectWasModified { get; private set; } = false;

        public ImportExportDialog() : this(null, false)
        {
        }

        public ImportExportDialog(SpriteProject project, bool openImportTab = false)
        {
            _project = project;
            InitializeComponent();
            if (_project != null)
            {
                _numImportW.Value = _project.DefaultWidth;
                _numImportH.Value = _project.DefaultHeight;
            }
            if (_cmbExportType.Items.Count > 0) _cmbExportType.SelectedIndex = 0;
            if (_cmbNumberFormat.Items.Count > 0) _cmbNumberFormat.SelectedIndex = 0;
            if (_cmbDirective.Items.Count > 0) _cmbDirective.SelectedIndex = 0;
            if (_cmbMaskMode.Items.Count > 0) _cmbMaskMode.SelectedIndex = 0;

            if (openImportTab)
            {
                _tabCont.SelectedTab = _tabImport;
            }
            UpdateExportCode();
        }

        private void CmbExportType_SelectedIndexChanged(object sender, EventArgs e) => UpdateExportCode();
        private void CmbNumberFormat_SelectedIndexChanged(object sender, EventArgs e) => UpdateExportCode();
        private void CmbDirective_SelectedIndexChanged(object sender, EventArgs e) => UpdateExportCode();
        private void CmbMaskMode_SelectedIndexChanged(object sender, EventArgs e) => UpdateExportCode();
        private void ChkExportAll_CheckedChanged(object sender, EventArgs e) => UpdateExportCode();
        private void ChkIncludeLabels_CheckedChanged(object sender, EventArgs e) => UpdateExportCode();
        private void ChkIncludeComments_CheckedChanged(object sender, EventArgs e) => UpdateExportCode();
        private void NumImportW_ValueChanged(object sender, EventArgs e) => UpdateImportStats();
        private void NumImportH_ValueChanged(object sender, EventArgs e) => UpdateImportStats();
        private void TxtImportCode_TextChanged(object sender, EventArgs e) => UpdateImportStats();

        private void BtnPasteClipboard_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                _txtImportCode.Text = Clipboard.GetText();
                UpdateImportStats();
            }
        }

        private void UpdateExportCode()
        {
            if (_project == null) return;

            ExportOptions opt = new ExportOptions
            {
                ExportAllSprites = _chkExportAll.Checked,
                IncludeLabels = _chkIncludeLabels.Checked,
                IncludeComments = _chkIncludeComments.Checked,
                NumberFormat = (NumberFormat)Math.Max(0, _cmbNumberFormat.SelectedIndex),
                Directive = (AsmDirective)Math.Max(0, _cmbDirective.SelectedIndex),
                MaskMode = (MaskMode)Math.Max(0, _cmbMaskMode.SelectedIndex)
            };

            switch (_cmbExportType.SelectedIndex)
            {
                case 0: // Assembly
                    _txtExportCode.Text = SpriteExporter.ExportToAssembly(_project, opt);
                    break;
                case 1: // C
                    _txtExportCode.Text = SpriteExporter.ExportToC(_project, opt.ExportAllSprites);
                    break;
                case 2: // BASIC
                    _txtExportCode.Text = SpriteExporter.ExportToBasic(_project);
                    break;
                case 3: // Binario (rappresentato come lista di byte)
                    _txtExportCode.Text = SpriteExporter.ExportToAssembly(_project, new ExportOptions
                    {
                        ExportAllSprites = opt.ExportAllSprites,
                        IncludeLabels = false,
                        IncludeComments = false,
                        Directive = AsmDirective.Db,
                        NumberFormat = NumberFormat.Decimal
                    });
                    break;
            }
        }

        private void UpdateImportStats()
        {
            var bytes = SpriteImporter.ParseBytesFromText(_txtImportCode.Text);
            int w = (int)_numImportW.Value;
            int h = (int)_numImportH.Value;
            int bytesPerSprite = (w / 8) * h;

            int count = bytesPerSprite > 0 ? bytes.Count / bytesPerSprite : 0;
            int rem = bytesPerSprite > 0 ? bytes.Count % bytesPerSprite : 0;

            string info = $"Byte rilevati: {bytes.Count} | Sprite completi ({w}x{h} px): {count}";
            if (rem > 0) info += $" (+{rem} byte residui)";
            _lblImportStats.Text = info;
        }

        private void BtnCopyExport_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_txtExportCode.Text))
            {
                Clipboard.SetText(_txtExportCode.Text);
                MessageBox.Show("Dati copiati negli appunti con successo!", "Copia Riuscita", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnSaveExportFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                switch (_cmbExportType.SelectedIndex)
                {
                    case 0:
                        sfd.Filter = "Assembly Files (*.asm;*.inc)|*.asm;*.inc|All Files (*.*)|*.*";
                        sfd.FileName = "sprites.asm";
                        break;
                    case 1:
                        sfd.Filter = "C Header Files (*.h)|*.h|All Files (*.*)|*.*";
                        sfd.FileName = "sprites.h";
                        break;
                    case 2:
                        sfd.Filter = "Sinclair BASIC / Text (*.bas;*.txt)|*.bas;*.txt|All Files (*.*)|*.*";
                        sfd.FileName = "sprites.bas";
                        break;
                    case 3:
                        sfd.Filter = "Raw Binary (*.bin)|*.bin|All Files (*.*)|*.*";
                        sfd.FileName = "sprites.bin";
                        break;
                }

                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    if (_cmbExportType.SelectedIndex == 3)
                    {
                        SpriteExporter.ExportToBinaryFile(sfd.FileName, _project, _chkExportAll.Checked);
                    }
                    else
                    {
                        File.WriteAllText(sfd.FileName, _txtExportCode.Text);
                    }
                    MessageBox.Show($"File salvato con successo:\n{sfd.FileName}", "Salvataggio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnLoadBinaryFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Binary Files (*.bin;*.dat;*.spr)|*.bin;*.dat;*.spr|All Files (*.*)|*.*";
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    byte[] bytes = File.ReadAllBytes(ofd.FileName);
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    for (int i = 0; i < bytes.Length; i += 16)
                    {
                        sb.Append("db ");
                        int c = Math.Min(16, bytes.Length - i);
                        for (int j = 0; j < c; j++)
                        {
                            sb.Append(bytes[i + j]);
                            if (j < c - 1) sb.Append(",");
                        }
                        sb.AppendLine();
                    }
                    _txtImportCode.Text = sb.ToString();
                }
            }
        }

        private void BtnDoImport_Click(object sender, EventArgs e)
        {
            int w = (int)_numImportW.Value;
            int h = (int)_numImportH.Value;

            var importedSprites = SpriteImporter.ImportFromText(_txtImportCode.Text, w, h, "Nemico_Import");
            if (importedSprites.Count == 0)
            {
                MessageBox.Show("Nessun dato valido trovato nel testo di input.", "Errore Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var res = MessageBox.Show(
                $"Trovati {importedSprites.Count} sprite ({w}x{h}).\n\nVuoi SOSTITUIRE l'intero progetto esistente?\n(Scegli 'No' per aggiungere gli sprite in coda al progetto)",
                "Conferma Importazione",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (res == DialogResult.Cancel) return;

            if (res == DialogResult.Yes)
            {
                _project.Sprites.Clear();
                _project.DefaultWidth = w;
                _project.DefaultHeight = h;
            }

            foreach (var s in importedSprites)
            {
                _project.Sprites.Add(s);
            }
            _project.SelectedIndex = 0;
            ProjectWasModified = true;

            MessageBox.Show($"Importazione completata con successo! ({importedSprites.Count} sprite caricati)", "Import Completato", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
