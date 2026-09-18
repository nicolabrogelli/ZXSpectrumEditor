using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ZXSpectrumSpriteGenerator.Models;
using ZXSpectrumSpriteGenerator.Services;
using ZXSpectrumSpriteGenerator.UI.Controls;

namespace ZXSpectrumSpriteGenerator.UI
{
    public partial class MainForm : Form
    {
        private SpriteProject _project;
        private bool _isAnimPlaying = false;
        private bool _isUpdatingName = false;
        private int _animStepIndex = 0;
        private bool _isUpdatingJumpCombo = false;
        private string _currentFilePath = null;
        private bool _isModified = false;

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;

            InitRuntimeControls();
            LoadProject(SpriteProject.CreateDefault());
            _isModified = false;
            UpdateWindowTitle();
        }

        private void InitRuntimeControls()
        {
            _cmbInk.Items.Clear();
            _cmbPaper.Items.Clear();
            for (int i = 0; i < 8; i++)
            {
                _cmbInk.Items.Add($"{i}: {ZxSpectrumPalette.ColorNames[i]}");
                _cmbPaper.Items.Add($"{i}: {ZxSpectrumPalette.ColorNames[i]}");
            }
            _cmbInk.SelectedIndex = 7; // White
            _cmbPaper.SelectedIndex = 0; // Black

            if (_cmbAnimScope.Items.Count > 0)
            {
                _cmbAnimScope.SelectedIndex = 0;
            }

            _mSubPresets.DropDownItems.Clear();
            foreach (var p in EnemyPresets.AllPresets)
            {
                var curPreset = p;
                _mSubPresets.DropDownItems.Add(new ToolStripMenuItem(curPreset.DisplayName, null, (s, e) => ActionAddEnemyPreset(curPreset)));
            }

            LayoutNavBar();
        }

        private void SetTool(DrawingTool tool, ToolStripButton activeBtn)
        {
            _canvas.CurrentTool = tool;
            _lblTool.Text = $"Strumento: {activeBtn.Text}";

            _btnToolPencil.Checked = false;
            _btnToolLine.Checked = false;
            _btnToolRect.Checked = false;
            _btnToolRectFill.Checked = false;
            _btnToolCircle.Checked = false;
            _btnToolCircleFill.Checked = false;
            _btnToolFill.Checked = false;

            activeBtn.Checked = true;
        }

        private void SetModified(bool modified = true)
        {
            _isModified = modified;
            UpdateWindowTitle();
        }

        private void UpdateWindowTitle()
        {
            string fileName = string.IsNullOrEmpty(_currentFilePath) ? "Senza Titolo" : Path.GetFileName(_currentFilePath);
            string modStar = _isModified ? " *" : "";
            this.Text = $"ZX Spectrum Sprite Generator & Studio - [{fileName}]{modStar}";
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!PromptSaveChanges())
            {
                e.Cancel = true;
            }
        }

        private bool PromptSaveChanges()
        {
            if (!_isModified) return true;

            var result = MessageBox.Show(
                "Ci sono modifiche non salvate nel progetto corrente.\nVuoi salvare le modifiche prima di procedere?",
                "Salvataggio Progetto",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                return ActionSaveProject();
            }
            else if (result == DialogResult.No)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void LoadProject(SpriteProject project)
        {
            _project = project;
            _filmstrip.Project = _project;
            if (_tileEditorView != null)
            {
                _tileEditorView.Project = _project;
            }
            if (_mapEditorView != null)
            {
                _mapEditorView.Project = _project;
            }
            UpdateEnemyJumpDropdown();
            SelectSprite(_project.SelectedIndex);
            _filmstrip.ScrollToStart();
            UpdateFilmstripScrollBar();
            SetModified(false);
        }

        private void SelectSprite(int index)
        {
            if (_project == null || _project.Sprites.Count == 0) return;

            _project.SelectedIndex = Math.Max(0, Math.Min(_project.Sprites.Count - 1, index));
            var sprite = _project.CurrentSprite;

            _canvas.CurrentSprite = sprite;
            CenterCanvas();

            if (sprite != null)
            {
                if (_txtCurrentSpriteName != null)
                {
                    _isUpdatingName = true;
                    _txtCurrentSpriteName.Text = sprite.Name;
                    _isUpdatingName = false;
                }

                _cmbInk.SelectedIndex = Math.Max(0, Math.Min(7, sprite.InkColor));
                _cmbPaper.SelectedIndex = Math.Max(0, Math.Min(7, sprite.PaperColor));
                _chkBright.Checked = sprite.Bright;
                _chkFlash.Checked = sprite.Flash;

                int bytes = (sprite.Width / 8) * sprite.Height;
                int charW = sprite.Width / 8;
                int charH = (sprite.Height + 7) / 8;

                _lblDimensions.Text = $"Dim: {sprite.Width}x{sprite.Height} px ({charW}x{charH} celle)";
                _lblTotalBytes.Text = $"Bytes: {bytes}";
            }

            _filmstrip.EnsureVisible(_project.SelectedIndex);
            _filmstrip.Invalidate();
            UpdatePreviews();
            UpdateQuickAsm();
            _animStepIndex = 0;
            UpdateAnimFrameDisplay();
            UpdateFilmstripScrollBar();
            UpdateFilmstripInfo();
            SyncEnemyJumpSelection();
        }

        private void UpdateFilmstripScrollBar()
        {
            if (_project == null || _filmstrip == null || _filmstripScrollBar == null) return;

            int maxScroll = _filmstrip.MaxScrollOffsetX;
            int visibleWidth = _filmstrip.ClientSize.Width;

            if (maxScroll > 0 && visibleWidth > 0)
            {
                _filmstripScrollBar.Enabled = true;
                _filmstripScrollBar.Minimum = 0;
                _filmstripScrollBar.LargeChange = Math.Max(1, visibleWidth);
                _filmstripScrollBar.SmallChange = _filmstrip.ItemWidth + _filmstrip.Spacing;
                _filmstripScrollBar.Maximum = maxScroll + _filmstripScrollBar.LargeChange - 1;

                int val = Math.Max(0, Math.Min(maxScroll, _filmstrip.ScrollOffsetX));
                if (_filmstripScrollBar.Value != val)
                {
                    _filmstripScrollBar.Value = val;
                }
            }
            else
            {
                _filmstripScrollBar.Enabled = false;
                _filmstripScrollBar.Minimum = 0;
                _filmstripScrollBar.Maximum = 0;
                _filmstripScrollBar.Value = 0;
            }
        }

        private void UpdateFilmstripInfo()
        {
            if (_project == null || _lblFilmstripInfo == null) return;
            if (_project.Sprites.Count == 0)
            {
                _lblFilmstripInfo.Text = "Sprite: 0/0";
                return;
            }

            int idx = _project.SelectedIndex;
            var s = _project.CurrentSprite;
            string grp = s != null && !string.IsNullOrEmpty(s.GroupName) ? s.GroupName : "Nemico";

            var enemyIndices = GetAnimationFramesIndices();
            int relFrame = enemyIndices.IndexOf(idx) + 1;
            if (relFrame <= 0) relFrame = (idx % 4) + 1;
            int totalRel = Math.Max(relFrame, enemyIndices.Count > 0 ? enemyIndices.Count : 4);

            _lblFilmstripInfo.Text = $"Sprite {idx + 1}/{_project.Sprites.Count} [{grp} {relFrame}/{totalRel}]";
        }

        private void UpdateEnemyJumpDropdown()
        {
            if (_project == null || _cmbJumpEnemy == null) return;

            _isUpdatingJumpCombo = true;
            _cmbJumpEnemy.Items.Clear();

            string lastGroup = null;
            int firstIdx = 0;
            int count = 0;

            for (int i = 0; i < _project.Sprites.Count; i++)
            {
                string grp = _project.Sprites[i].GroupName;
                if (string.IsNullOrEmpty(grp)) grp = $"Nemico {(i / 4) + 1}";

                if (lastGroup == null)
                {
                    lastGroup = grp;
                    firstIdx = i;
                    count = 1;
                }
                else if (grp == lastGroup)
                {
                    count++;
                }
                else
                {
                    _cmbJumpEnemy.Items.Add(new EnemyJumpItem { GroupName = lastGroup, FirstIndex = firstIdx, Count = count });
                    lastGroup = grp;
                    firstIdx = i;
                    count = 1;
                }
            }

            if (lastGroup != null && count > 0)
            {
                _cmbJumpEnemy.Items.Add(new EnemyJumpItem { GroupName = lastGroup, FirstIndex = firstIdx, Count = count });
            }

            SyncEnemyJumpSelection();
            _isUpdatingJumpCombo = false;
        }

        private void SyncEnemyJumpSelection()
        {
            if (_project == null || _cmbJumpEnemy == null || _cmbJumpEnemy.Items.Count == 0) return;
            int currentIdx = _project.SelectedIndex;

            for (int i = 0; i < _cmbJumpEnemy.Items.Count; i++)
            {
                if (_cmbJumpEnemy.Items[i] is EnemyJumpItem item)
                {
                    if (currentIdx >= item.FirstIndex && currentIdx < item.FirstIndex + item.Count)
                    {
                        if (_cmbJumpEnemy.SelectedIndex != i)
                        {
                            _isUpdatingJumpCombo = true;
                            _cmbJumpEnemy.SelectedIndex = i;
                            _isUpdatingJumpCombo = false;
                        }
                        return;
                    }
                }
            }

            if (_cmbJumpEnemy.Items.Count > 0 && _cmbJumpEnemy.SelectedIndex < 0)
            {
                _isUpdatingJumpCombo = true;
                _cmbJumpEnemy.SelectedIndex = 0;
                _isUpdatingJumpCombo = false;
            }
        }

        private class EnemyJumpItem
        {
            public string GroupName { get; set; }
            public int FirstIndex { get; set; }
            public int Count { get; set; }
            public override string ToString() => $"{GroupName} ({Count} frame)";
        }

        private void CenterCanvas()
        {
            if (_canvas == null || _centerPanel == null) return;
            int x = Math.Max(10, (_centerPanel.ClientSize.Width - _canvas.Width) / 2);
            int y = Math.Max(10, (_centerPanel.ClientSize.Height - _canvas.Height) / 2);
            _canvas.Location = new Point(x, y);
        }

        private void Canvas_HoverCoordinatesChanged(object sender, HoverCoordinateEventArgs e)
        {
            _lblCoords.Text = $"Pixel: ({e.PixelX}, {e.PixelY})";
            _lblCharCoords.Text = $"Cella ZX: ({e.CharX}, {e.CharY})";
        }

        private void Canvas_SpriteChanged(object sender, EventArgs e)
        {
            SetModified(true);
            _filmstrip.Invalidate();
            UpdatePreviews();
            UpdateQuickAsm();
        }

        private void Filmstrip_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectSprite(_filmstrip.Project.SelectedIndex);
        }

        private void UpdatePreviews()
        {
            if (_project?.CurrentSprite == null) return;

            var old1 = _picPreview1x.Image;
            var old4 = _picPreview4x.Image;

            _picPreview1x.Image = _project.CurrentSprite.RenderToBitmap(1);
            _picPreview4x.Image = _project.CurrentSprite.RenderToBitmap(4);

            old1?.Dispose();
            old4?.Dispose();
        }

        private void UpdateQuickAsm()
        {
            if (_project?.CurrentSprite == null)
            {
                _txtQuickAsm.Text = string.Empty;
                return;
            }

            var opt = new ExportOptions
            {
                ExportAllSprites = false,
                IncludeLabels = true,
                IncludeComments = false,
                Directive = AsmDirective.Db,
                NumberFormat = NumberFormat.Decimal,
                BytesPerLine = 16
            };
            _txtQuickAsm.Text = SpriteExporter.ExportToAssembly(_project, opt).Trim();
        }

        public void SwitchToTileEditor(int tileIndex = -1)
        {
            if (_mainTabs != null && _tabTiles != null)
            {
                _mainTabs.SelectedTab = _tabTiles;
                if (tileIndex >= 0 && _project != null && tileIndex < _project.Tiles.Count)
                {
                    _project.SelectedTileIndex = tileIndex;
                    _tileEditorView.SelectTile(tileIndex);
                }
            }
        }

        public void SwitchToMapEditor()
        {
            if (_mainTabs != null && _tabMaps != null)
            {
                _mainTabs.SelectedTab = _tabMaps;
            }
        }

        public void SwitchToSpriteEditor()
        {
            if (_mainTabs != null && _tabSprites != null)
            {
                _mainTabs.SelectedTab = _tabSprites;
            }
        }

        private void MainTabs_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle r = _mainTabs.GetTabRect(e.Index);
            bool isSelected = (_mainTabs.SelectedIndex == e.Index);

            Color bg = isSelected ? Color.FromArgb(0, 110, 190) : Color.FromArgb(36, 36, 44);
            using (SolidBrush b = new SolidBrush(bg))
            {
                g.FillRectangle(b, r);
            }

            if (isSelected)
            {
                using (Pen p = new Pen(Color.FromArgb(0, 220, 255), 3f))
                {
                    g.DrawLine(p, r.Left, r.Bottom - 1, r.Right, r.Bottom - 1);
                }
            }

            string text = _mainTabs.TabPages[e.Index].Text;
            using (Font f = new Font("Segoe UI", 9.2f, isSelected ? FontStyle.Bold : FontStyle.Regular))
            using (SolidBrush tb = new SolidBrush(isSelected ? Color.White : Color.FromArgb(190, 190, 205)))
            {
                StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString(text, f, tb, r, sf);
            }
        }

        private void MainTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isMapTab = (_mainTabs.SelectedTab == _tabMaps);
            bool isTileTab = (_mainTabs.SelectedTab == _tabTiles);
            bool isSpriteTab = (_mainTabs.SelectedTab == _tabSprites);
            if (_mTrans != null) _mTrans.Enabled = isSpriteTab;
            if (_mPreset != null) _mPreset.Enabled = isSpriteTab;
            if (_mSpriteClear != null) _mSpriteClear.Enabled = isSpriteTab;
            if (_mSpriteInvert != null) _mSpriteInvert.Enabled = isSpriteTab;
            if (_mSpriteAddEnemies != null) _mSpriteAddEnemies.Enabled = isSpriteTab;
            if (_mSpritePasteNemico != null) _mSpritePasteNemico.Enabled = isSpriteTab;
            if (_statusStrip != null) _statusStrip.Visible = isSpriteTab;

            if (isMapTab)
            {
                _mapEditorView.Project = _project;
                _mapEditorView.RefreshView();
            }
            else if (isTileTab)
            {
                _tileEditorView.Project = _project;
                _tileEditorView.RefreshView();
            }
            else if (_mainTabs.SelectedTab == _tabSprites)
            {
                _canvas.Invalidate();
                _filmstrip.Invalidate();
                if (_project != null && _project.Sprites.Count > 0)
                {
                    SelectSprite(_project.SelectedIndex);
                }
            }
        }

        private void LayoutNavBar()
        {
            if (_filmstripNavBar == null || _filmstripScrollBar == null) return;
            int infoWidth = 210;
            int rightControlsWidth = 64 + 4 + 28 + 8 + infoWidth;
            int scrollWidth = Math.Max(50, _filmstripNavBar.ClientSize.Width - 104 - rightControlsWidth);
            _filmstripScrollBar.Width = scrollWidth;
            _btnScrollRight.Left = 104 + scrollWidth + 4;
            _btnScrollEnd.Left = _btnScrollRight.Right + 4;
            _lblFilmstripInfo.Left = _btnScrollEnd.Right + 8;
        }

        private void FilmstripNavBar_Resize(object sender, EventArgs e) => LayoutNavBar();

        private void CenterPanel_Resize(object sender, EventArgs e) => CenterCanvas();

        private void FilmstripScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            _filmstrip.ScrollOffsetX = _filmstripScrollBar.Value;
            UpdateFilmstripInfo();
        }

        private void Filmstrip_ScrollChanged(object sender, EventArgs e)
        {
            UpdateFilmstripScrollBar();
            UpdateFilmstripInfo();
        }

        private void Filmstrip_Resize(object sender, EventArgs e)
        {
            UpdateFilmstripScrollBar();
        }

        private void BtnToggleGrid_Click(object sender, EventArgs e)
        {
            _btnToggleGrid.Checked = !_btnToggleGrid.Checked;
            _canvas.ShowPixelGrid = _btnToggleGrid.Checked;
        }

        private void BtnToggleCharGrid_Click(object sender, EventArgs e)
        {
            _btnToggleCharGrid.Checked = !_btnToggleCharGrid.Checked;
            _canvas.ShowCharGrid = _btnToggleCharGrid.Checked;
        }

        private void BtnZoomIn_Click(object sender, EventArgs e)
        {
            _canvas.CellSize += 4;
            CenterCanvas();
        }

        private void BtnZoomOut_Click(object sender, EventArgs e)
        {
            _canvas.CellSize -= 4;
            CenterCanvas();
        }

        private void TxtCurrentSpriteName_TextChanged(object sender, EventArgs e)
        {
            if (_project?.CurrentSprite != null && !_isUpdatingName)
            {
                _project.CurrentSprite.Name = _txtCurrentSpriteName.Text;
                SetModified(true);
                _filmstrip.Invalidate();
                UpdateQuickAsm();
            }
        }

        private void CmbInk_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_project?.CurrentSprite != null)
            {
                _project.CurrentSprite.InkColor = _cmbInk.SelectedIndex;
                _canvas.Invalidate();
                UpdatePreviews();
            }
        }

        private void CmbPaper_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_project?.CurrentSprite != null)
            {
                _project.CurrentSprite.PaperColor = _cmbPaper.SelectedIndex;
                _canvas.Invalidate();
                UpdatePreviews();
            }
        }

        private void ChkBright_CheckedChanged(object sender, EventArgs e)
        {
            if (_project?.CurrentSprite != null)
            {
                _project.CurrentSprite.Bright = _chkBright.Checked;
                _canvas.Invalidate();
                UpdatePreviews();
                SetModified(true);
            }
        }

        private void ChkFlash_CheckedChanged(object sender, EventArgs e)
        {
            if (_project?.CurrentSprite != null)
            {
                _project.CurrentSprite.Flash = _chkFlash.Checked;
                _canvas.Invalidate();
                UpdatePreviews();
                SetModified(true);
            }
        }

        private void CmbAnimScope_SelectedIndexChanged(object sender, EventArgs e)
        {
            _animStepIndex = 0;
            UpdateAnimFrameDisplay();
        }

        private void TbAnimFps_ValueChanged(object sender, EventArgs e)
        {
            _lblFps.Text = $"{_tbAnimFps.Value} fps";
            if (_animTimer != null)
            {
                _animTimer.Interval = Math.Max(20, 1000 / _tbAnimFps.Value);
            }
        }

        private void CmbJumpEnemy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isUpdatingJumpCombo && _cmbJumpEnemy.SelectedItem is EnemyJumpItem itm)
            {
                _project.SelectedIndex = itm.FirstIndex;
                SelectSprite(itm.FirstIndex);
                _filmstrip.CenterSprite(itm.FirstIndex);
            }
        }

        private void ActionNewProject_Click(object sender, EventArgs e) => ActionNewProject();
        private void ActionOpenProject_Click(object sender, EventArgs e) => ActionOpenProject();
        private void ActionSaveProject_Click(object sender, EventArgs e) => ActionSaveProject();
        private void ActionSaveProjectAs_Click(object sender, EventArgs e) => ActionSaveProjectAs();
        private void ActionImport_Click(object sender, EventArgs e) => ActionImport();
        private void ActionExport_Click(object sender, EventArgs e) => ActionExport();
        private void ActionExportPng_Click(object sender, EventArgs e) => ActionExportPng();
        private void ActionExit_Click(object sender, EventArgs e) => this.Close();

        private void ActionUndo_Click(object sender, EventArgs e)
        {
            if (_mainTabs?.SelectedTab == _tabMaps)
                _mapEditorView.ActionUndo();
            else if (_mainTabs?.SelectedTab == _tabTiles)
                _tileEditorView.ActionUndo();
            else
                ActionUndo();
        }

        private void ActionRedo_Click(object sender, EventArgs e)
        {
            if (_mainTabs?.SelectedTab == _tabMaps)
                _mapEditorView.ActionRedo();
            else if (_mainTabs?.SelectedTab == _tabTiles)
                _tileEditorView.ActionRedo();
            else
                ActionRedo();
        }

        private void ActionClear_Click(object sender, EventArgs e) => ActionClear();
        private void ActionInvert_Click(object sender, EventArgs e) => ActionInvert();
        private void ActionAddEnemies_Click(object sender, EventArgs e) => ActionAddEnemies();
        private void ActionPasteEnemyFromClipboard_Click(object sender, EventArgs e) => ActionPasteEnemyFromClipboard();
        private void ActionQuickCopyAsm_Click(object sender, EventArgs e) => ActionQuickCopyAsm();

        private void SwitchToSpriteEditor_Click(object sender, EventArgs e) => SwitchToSpriteEditor();
        private void SwitchToTileEditor_Click(object sender, EventArgs e) => SwitchToTileEditor();
        private void SwitchToMapEditor_Click(object sender, EventArgs e) => SwitchToMapEditor();

        private void ActionFlipH_Click(object sender, EventArgs e) => ActionFlipH();
        private void ActionFlipV_Click(object sender, EventArgs e) => ActionFlipV();
        private void ActionRotateCW_Click(object sender, EventArgs e) => ActionRotate(true);
        private void ActionRotateCCW_Click(object sender, EventArgs e) => ActionRotate(false);
        private void ActionShiftUp_Click(object sender, EventArgs e) => ActionShift(0, -1);
        private void ActionShiftDown_Click(object sender, EventArgs e) => ActionShift(0, 1);
        private void ActionShiftLeft_Click(object sender, EventArgs e) => ActionShift(-1, 0);
        private void ActionShiftRight_Click(object sender, EventArgs e) => ActionShift(1, 0);

        private void ActionPresetEnemy_Click(object sender, EventArgs e)
        {
            if (PromptSaveChanges()) LoadProject(SpriteProject.CreateDefault());
        }

        private void ActionPresetWalker_Click(object sender, EventArgs e)
        {
            if (PromptSaveChanges()) LoadWalkerPreset();
        }

        private void ActionNewMap_Click(object sender, EventArgs e)
        {
            SwitchToMapEditor();
            _mapEditorView.ActionNewMap();
        }

        private void ActionDupMap_Click(object sender, EventArgs e)
        {
            SwitchToMapEditor();
            _mapEditorView.ActionDuplicateMap();
        }

        private void ActionDelMap_Click(object sender, EventArgs e)
        {
            SwitchToMapEditor();
            _mapEditorView.ActionDeleteMap();
        }

        private void ActionRenameMap_Click(object sender, EventArgs e)
        {
            SwitchToMapEditor();
            _mapEditorView.ActionRenameMap();
        }

        private void ActionResizeMap_Click(object sender, EventArgs e)
        {
            SwitchToMapEditor();
            _mapEditorView.ActionResizeMap();
        }

        private void ActionClearMap_Click(object sender, EventArgs e)
        {
            SwitchToMapEditor();
            _mapEditorView.ActionClearMap();
        }

        private void ActionExpMap_Click(object sender, EventArgs e)
        {
            SwitchToMapEditor();
            _mapEditorView.ActionExportMap();
        }

        private void ActionExpPng_Click(object sender, EventArgs e)
        {
            SwitchToMapEditor();
            _mapEditorView.ActionExportPng();
        }

        private void ActionAbout_Click(object sender, EventArgs e) => ShowAboutDialog();

        private void BtnToolPencil_Click(object sender, EventArgs e) => SetTool(DrawingTool.Pencil, _btnToolPencil);
        private void BtnToolLine_Click(object sender, EventArgs e) => SetTool(DrawingTool.Line, _btnToolLine);
        private void BtnToolRect_Click(object sender, EventArgs e) => SetTool(DrawingTool.RectangleOutline, _btnToolRect);
        private void BtnToolRectFill_Click(object sender, EventArgs e) => SetTool(DrawingTool.RectangleFilled, _btnToolRectFill);
        private void BtnToolCircle_Click(object sender, EventArgs e) => SetTool(DrawingTool.CircleOutline, _btnToolCircle);
        private void BtnToolCircleFill_Click(object sender, EventArgs e) => SetTool(DrawingTool.CircleFilled, _btnToolCircleFill);
        private void BtnToolFill_Click(object sender, EventArgs e) => SetTool(DrawingTool.FloodFill, _btnToolFill);

        private void ActionAddSprite_Click(object sender, EventArgs e) => ActionAddSprite();
        private void ActionDuplicateSprite_Click(object sender, EventArgs e) => ActionDuplicateSprite();
        private void ActionDeleteSprite_Click(object sender, EventArgs e) => ActionDeleteSprite();
        private void ActionMoveSpriteLeft_Click(object sender, EventArgs e) => ActionMoveSpriteLeft();
        private void ActionMoveSpriteRight_Click(object sender, EventArgs e) => ActionMoveSpriteRight();

        private void BtnScrollStart_Click(object sender, EventArgs e) => _filmstrip.ScrollToStart();
        private void BtnScrollLeft_Click(object sender, EventArgs e) => _filmstrip.ScrollLeft(3);
        private void BtnScrollRight_Click(object sender, EventArgs e) => _filmstrip.ScrollRight(3);
        private void BtnScrollEnd_Click(object sender, EventArgs e) => _filmstrip.ScrollToEnd();

        private void MapEditorView_ProjectModified(object sender, EventArgs e) => SetModified(true);
        private void MapEditorView_RequestSwitchToSprites(object sender, EventArgs e) => SwitchToSpriteEditor();
        private void MapEditorView_RequestSwitchToTiles(object sender, int tileIndex) => SwitchToTileEditor(tileIndex);

        private void TileEditorView_ProjectModified(object sender, EventArgs e) => SetModified(true);
        private void TileEditorView_RequestSwitchToMaps(object sender, EventArgs e) => SwitchToMapEditor();
        private void TileEditorView_RequestSwitchToSprites(object sender, EventArgs e) => SwitchToSpriteEditor();

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                SwitchToSpriteEditor();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F4)
            {
                SwitchToMapEditor();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Z)
            {
                if (_mainTabs?.SelectedTab == _tabMaps)
                    _mapEditorView.ActionUndo();
            else if (_mainTabs?.SelectedTab == _tabTiles)
                _tileEditorView.ActionUndo();
                else
                    ActionUndo();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                if (_mainTabs?.SelectedTab == _tabMaps)
                    _mapEditorView.ActionRedo();
            else if (_mainTabs?.SelectedTab == _tabTiles)
                _tileEditorView.ActionRedo();
                else
                    ActionRedo();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.I)
            {
                if (_mainTabs?.SelectedTab == _tabSprites)
                {
                    ActionInvert();
                    e.Handled = true;
                }
            }
            else if (e.KeyCode == Keys.B && _mainTabs?.SelectedTab == _tabSprites)
            {
                SetTool(DrawingTool.Pencil, _btnToolPencil);
            }
            else if (e.KeyCode == Keys.L && _mainTabs?.SelectedTab == _tabSprites)
            {
                SetTool(DrawingTool.Line, _btnToolLine);
            }
            else if (e.KeyCode == Keys.R && _mainTabs?.SelectedTab == _tabSprites)
            {
                SetTool(DrawingTool.RectangleOutline, _btnToolRect);
            }
            else if (e.KeyCode == Keys.F && _mainTabs?.SelectedTab == _tabSprites)
            {
                SetTool(DrawingTool.FloodFill, _btnToolFill);
            }
            else if (e.KeyCode == Keys.Left && e.Alt && _mainTabs?.SelectedTab == _tabSprites)
            {
                ActionShift(-1, 0);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right && e.Alt && _mainTabs?.SelectedTab == _tabSprites)
            {
                ActionShift(1, 0);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up && e.Alt && _mainTabs?.SelectedTab == _tabSprites)
            {
                ActionShift(0, -1);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down && e.Alt && _mainTabs?.SelectedTab == _tabSprites)
            {
                ActionShift(0, 1);
                e.Handled = true;
            }
        }

        // Actions
        private void ActionNewProject()
        {
            if (!PromptSaveChanges()) return;

            using (Form dlg = new Form())
            {
                dlg.Text = "Nuovo Progetto Sprite ZX Spectrum";
                dlg.Size = new Size(340, 220);
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.MaximizeBox = false;
                dlg.MinimizeBox = false;
                dlg.BackColor = Color.FromArgb(35, 35, 40);
                dlg.ForeColor = Color.White;

                Label lbl = new Label { Text = "Dimensione sprite predefinita:", Left = 20, Top = 20, AutoSize = true };
                ComboBox cmb = new ComboBox { Left = 20, Top = 48, Width = 280, DropDownStyle = ComboBoxStyle.DropDownList };
                cmb.Items.AddRange(new object[] {
                    "16 x 16 px (2x2 caratteri, 32 byte) [Consigliato Nemico/Player]",
                    "8 x 8 px (1x1 carattere, 8 byte) [Icone/Proiettili]",
                    "24 x 24 px (3x3 caratteri, 72 byte) [Boss]",
                    "32 x 32 px (4x4 caratteri, 128 byte) [Sprite Grandi]",
                    "16 x 24 px (2x3 caratteri, 48 byte)"
                });
                cmb.SelectedIndex = 0;

                Button btnOk = new Button { Text = "Crea Progetto", Left = 160, Top = 120, Width = 140, Height = 32, BackColor = Color.FromArgb(0, 122, 204), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                btnOk.Click += (s, e) => { dlg.DialogResult = DialogResult.OK; dlg.Close(); };

                dlg.Controls.AddRange(new Control[] { lbl, cmb, btnOk });

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    int w = 16, h = 16;
                    switch (cmb.SelectedIndex)
                    {
                        case 0: w = 16; h = 16; break;
                        case 1: w = 8; h = 8; break;
                        case 2: w = 24; h = 24; break;
                        case 3: w = 32; h = 32; break;
                        case 4: w = 16; h = 24; break;
                    }

                    var newProj = new SpriteProject { DefaultWidth = w, DefaultHeight = h };
                    newProj.AddSprite("Sprite_0");
                    _currentFilePath = null;
                    LoadProject(newProj);
                    SetModified(false);
                }
            }
        }

        private void ActionOpenProject()
        {
            if (!PromptSaveChanges()) return;

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = ProjectStorage.FileFilter;
                ofd.Title = "Apri Progetto Sprite ZX Spectrum";
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        var proj = ProjectStorage.LoadFromFile(ofd.FileName);
                        _currentFilePath = ofd.FileName;
                        LoadProject(proj);
                        SetModified(false);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Impossibile aprire il file di progetto:\n{ex.Message}", "Errore Apertura", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private bool ActionSaveProject()
        {
            if (string.IsNullOrEmpty(_currentFilePath))
            {
                return ActionSaveProjectAs();
            }

            try
            {
                ProjectStorage.SaveToFile(_currentFilePath, _project);
                SetModified(false);
                MessageBox.Show($"Progetto salvato con successo:\n{_currentFilePath}", "Salvataggio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante il salvataggio:\n{ex.Message}", "Errore Salvataggio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool ActionSaveProjectAs()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = ProjectStorage.FileFilter;
                sfd.DefaultExt = "zxproj";
                sfd.Title = "Salva Progetto Sprite ZX Spectrum";
                if (!string.IsNullOrEmpty(_currentFilePath))
                {
                    sfd.FileName = Path.GetFileName(_currentFilePath);
                    sfd.InitialDirectory = Path.GetDirectoryName(_currentFilePath);
                }
                else
                {
                    sfd.FileName = "nemico_zx.zxproj";
                }

                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        ProjectStorage.SaveToFile(sfd.FileName, _project);
                        _currentFilePath = sfd.FileName;
                        SetModified(false);
                        MessageBox.Show($"Progetto salvato con successo:\n{_currentFilePath}", "Salvataggio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Errore durante il salvataggio:\n{ex.Message}", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                return false;
            }
        }

        private void ActionUndo()
        {
            _canvas.Undo();
            SetModified(true);
            _filmstrip.Invalidate();
            UpdatePreviews();
            UpdateQuickAsm();
        }

        private void ActionRedo()
        {
            _canvas.Redo();
            SetModified(true);
            _filmstrip.Invalidate();
            UpdatePreviews();
            UpdateQuickAsm();
        }

        private void ActionClear()
        {
            if (_project?.CurrentSprite == null) return;
            _canvas.PushUndoState();
            _project.CurrentSprite.Clear();
            SetModified(true);
            _canvas.Invalidate();
            _filmstrip.Invalidate();
            UpdatePreviews();
            UpdateQuickAsm();
        }

        private void ActionInvert()
        {
            if (_project?.CurrentSprite == null) return;
            _canvas.PushUndoState();
            _project.CurrentSprite.Invert();
            SetModified(true);
            _canvas.Invalidate();
            _filmstrip.Invalidate();
            UpdatePreviews();
            UpdateQuickAsm();
        }

        private void ActionFlipH()
        {
            if (_project?.CurrentSprite == null) return;
            _canvas.PushUndoState();
            _project.CurrentSprite.FlipHorizontal();
            SetModified(true);
            _canvas.Invalidate();
            _filmstrip.Invalidate();
            UpdatePreviews();
            UpdateQuickAsm();
        }

        private void ActionFlipV()
        {
            if (_project?.CurrentSprite == null) return;
            _canvas.PushUndoState();
            _project.CurrentSprite.FlipVertical();
            SetModified(true);
            _canvas.Invalidate();
            _filmstrip.Invalidate();
            UpdatePreviews();
            UpdateQuickAsm();
        }

        private void ActionRotate(bool cw)
        {
            if (_project?.CurrentSprite == null) return;
            _canvas.PushUndoState();
            _project.CurrentSprite.Rotate90(cw);
            SetModified(true);
            _canvas.Invalidate();
            _filmstrip.Invalidate();
            UpdatePreviews();
            UpdateQuickAsm();
        }

        private void ActionShift(int dx, int dy)
        {
            if (_project?.CurrentSprite == null) return;
            _canvas.PushUndoState();
            _project.CurrentSprite.Shift(dx, dy, wrap: false);
            SetModified(true);
            _canvas.Invalidate();
            _filmstrip.Invalidate();
            UpdatePreviews();
            UpdateQuickAsm();
        }

        private void ActionAddSprite()
        {
            if (_project == null) return;
            _project.AddSprite();
            SetModified(true);
            UpdateEnemyJumpDropdown();
            SelectSprite(_project.SelectedIndex);
            _filmstrip.CenterSprite(_project.SelectedIndex);
        }

        private void ActionDuplicateSprite()
        {
            if (_project == null) return;
            _project.DuplicateCurrent();
            SetModified(true);
            UpdateEnemyJumpDropdown();
            SelectSprite(_project.SelectedIndex);
            _filmstrip.CenterSprite(_project.SelectedIndex);
        }

        private void ActionDeleteSprite()
        {
            if (_project == null || _project.Sprites.Count <= 1)
            {
                MessageBox.Show("Il progetto deve contenere almeno uno sprite.", "Avviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Vuoi eliminare lo sprite selezionato?", "Conferma", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _project.RemoveCurrent();
                SetModified(true);
                UpdateEnemyJumpDropdown();
                SelectSprite(_project.SelectedIndex);
                _filmstrip.EnsureVisible(_project.SelectedIndex);
            }
        }

        private void ActionMoveSpriteLeft()
        {
            if (_project == null) return;
            if (_project.MoveCurrentUp())
            {
                SetModified(true);
                UpdateEnemyJumpDropdown();
                SelectSprite(_project.SelectedIndex);
                _filmstrip.EnsureVisible(_project.SelectedIndex);
            }
        }

        private void ActionMoveSpriteRight()
        {
            if (_project == null) return;
            if (_project.MoveCurrentDown())
            {
                SetModified(true);
                UpdateEnemyJumpDropdown();
                SelectSprite(_project.SelectedIndex);
                _filmstrip.EnsureVisible(_project.SelectedIndex);
            }
        }

        private void ActionAddEnemies()
        {
            using (var dlg = new AddEnemiesDialog(_project))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK && dlg.NewlyAddedSprites.Count > 0)
                {
                    SetModified(true);
                    UpdateEnemyJumpDropdown();
                    SelectSprite(_project.SelectedIndex);
                    _filmstrip.CenterSprite(_project.SelectedIndex);
                    MessageBox.Show($"Aggiunti con successo {dlg.NewlyAddedSprites.Count} sprite al progetto!", "Nemici Aggiunti", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ActionPasteEnemyFromClipboard()
        {
            if (!Clipboard.ContainsText())
            {
                MessageBox.Show("Gli appunti di Windows non contengono testo da incollare.", "Appunti Vuoti", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string text = Clipboard.GetText();
            string prefix = $"Nemico {(_project.Sprites.Count / 4 + 1)}";
            var sprites = SpriteImporter.ImportFromText(text, _project.DefaultWidth, _project.DefaultHeight, prefix);

            if (sprites.Count == 0)
            {
                MessageBox.Show("Nessun dato di sprite ZX Spectrum valido trovato negli appunti (attese direttive 'db' o sequenze di byte).", "Dati Non Validi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var s in sprites)
            {
                s.GroupName = prefix;
                _project.Sprites.Add(s);
            }

            SetModified(true);
            _project.SelectedIndex = _project.Sprites.Count - sprites.Count;
            UpdateEnemyJumpDropdown();
            SelectSprite(_project.SelectedIndex);
            _filmstrip.CenterSprite(_project.SelectedIndex);

            MessageBox.Show($"Aggiunti con successo {sprites.Count} sprite nemico al progetto!", "Importazione Riuscita", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ActionAddEnemyPreset(EnemyPreset preset)
        {
            if (preset == null) return;
            string prefix = $"Nemico {(_project.Sprites.Count / 4 + 1)}";

            foreach (var frame in preset.Frames)
            {
                var s = new Sprite(16, 16, $"{prefix}_{frame.FrameName}")
                {
                    GroupName = prefix,
                    InkColor = preset.DefaultInk,
                    PaperColor = preset.DefaultPaper,
                    Bright = true
                };
                s.SetFromBytesRowByRow(frame.Data);
                _project.Sprites.Add(s);
            }

            SetModified(true);
            _project.SelectedIndex = _project.Sprites.Count - preset.Frames.Count;
            UpdateEnemyJumpDropdown();
            SelectSprite(_project.SelectedIndex);
            _filmstrip.CenterSprite(_project.SelectedIndex);

            MessageBox.Show($"Aggiunti {preset.Frames.Count} sprite dal preset '{preset.DisplayName}'!", "Preset Nemico Aggiunto", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ActionImport()
        {
            using (var dlg = new ImportExportDialog(_project, openImportTab: true))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK && dlg.ProjectWasModified)
                {
                    SelectSprite(_project.SelectedIndex);
                }
            }
        }

        private void ActionExport()
        {
            using (var dlg = new ImportExportDialog(_project, openImportTab: false))
            {
                dlg.ShowDialog(this);
            }
        }

        private void ActionQuickCopyAsm()
        {
            if (_project?.CurrentSprite == null) return;
            var opt = new ExportOptions
            {
                ExportAllSprites = false,
                IncludeLabels = true,
                IncludeComments = true,
                Directive = AsmDirective.Db,
                NumberFormat = NumberFormat.Decimal,
                BytesPerLine = 16
            };
            string asm = SpriteExporter.ExportToAssembly(_project, opt);
            Clipboard.SetText(asm);
            MessageBox.Show("Dati Assembly dello sprite copiati negli appunti!", "Copia Riuscita", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ActionExportPng()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PNG Image (*.png)|*.png";
                sfd.FileName = "spritesheet.png";
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    SpriteExporter.ExportSpritesheetPng(sfd.FileName, _project, scale: 2);
                    MessageBox.Show($"Spritesheet salvato:\n{sfd.FileName}", "Esportazione Completata", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private List<int> GetAnimationFramesIndices()
        {
            List<int> indices = new List<int>();
            if (_project == null || _project.Sprites.Count == 0) return indices;

            int scope = _cmbAnimScope?.SelectedIndex ?? 0;

            if (scope == 0) // Nemico Selezionato (solo questo nemico / set)
            {
                var current = _project.CurrentSprite;
                string targetGroup = current != null ? current.GroupName : null;

                if (!string.IsNullOrEmpty(targetGroup))
                {
                    for (int i = 0; i < _project.Sprites.Count; i++)
                    {
                        if (_project.Sprites[i].GroupName == targetGroup)
                        {
                            indices.Add(i);
                        }
                    }
                }

                // Se non ha gruppo o ha 1 solo sprite, raggruppa per blocchi di 4
                if (indices.Count <= 1)
                {
                    indices.Clear();
                    int baseIdx = (_project.SelectedIndex / 4) * 4;
                    for (int i = baseIdx; i < Math.Min(baseIdx + 4, _project.Sprites.Count); i++)
                    {
                        indices.Add(i);
                    }
                }
            }
            else // Tutti gli sprite del progetto
            {
                for (int i = 0; i < _project.Sprites.Count; i++)
                {
                    indices.Add(i);
                }
            }

            if (indices.Count == 0 && _project.Sprites.Count > 0)
            {
                indices.Add(Math.Max(0, Math.Min(_project.Sprites.Count - 1, _project.SelectedIndex)));
            }

            return indices;
        }

        private void UpdateAnimFrameDisplay()
        {
            var indices = GetAnimationFramesIndices();
            if (indices.Count == 0)
            {
                _lblAnimFrame.Text = "Frame: 0/0";
                return;
            }

            int step = Math.Max(0, Math.Min(indices.Count - 1, _animStepIndex));
            int spriteIndex = indices[step];
            var sprite = _project.Sprites[spriteIndex];
            string grp = !string.IsNullOrEmpty(sprite.GroupName) ? $" [{sprite.GroupName}]" : "";
            _lblAnimFrame.Text = $"Frame: {step + 1}/{indices.Count}{grp}";
        }

        private void BtnPlayAnim_Click(object sender, EventArgs e)
        {
            _isAnimPlaying = !_isAnimPlaying;
            if (_isAnimPlaying)
            {
                _btnPlayAnim.Text = "⏹ Ferma";
                _btnPlayAnim.BackColor = Color.FromArgb(180, 50, 50);
                _animStepIndex = 0;
                UpdateAnimFrameDisplay();
                _animTimer.Start();
            }
            else
            {
                _btnPlayAnim.Text = "▶ Avvia Animazione";
                _btnPlayAnim.BackColor = Color.FromArgb(40, 130, 80);
                _animTimer.Stop();
                SelectSprite(_project.SelectedIndex);
            }
        }

        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            if (_project == null || _project.Sprites.Count == 0) return;

            var indices = GetAnimationFramesIndices();
            if (indices.Count == 0) return;

            _animStepIndex = (_animStepIndex + 1) % indices.Count;
            int spriteIndex = indices[_animStepIndex];
            var animSprite = _project.Sprites[spriteIndex];

            string grp = !string.IsNullOrEmpty(animSprite.GroupName) ? $" [{animSprite.GroupName}]" : "";
            _lblAnimFrame.Text = $"Frame: {_animStepIndex + 1}/{indices.Count}{grp}";

            var old1 = _picPreview1x.Image;
            var old4 = _picPreview4x.Image;

            _picPreview1x.Image = animSprite.RenderToBitmap(1);
            _picPreview4x.Image = animSprite.RenderToBitmap(4);

            old1?.Dispose();
            old4?.Dispose();
        }

        private void LoadWalkerPreset()
        {
            // Preset animazione camminata 16x16
            var proj = new SpriteProject { Title = "Personaggio Camminata" };
            proj.Sprites.Clear();

            // 4 frame stilizzati
            for (int i = 0; i < 4; i++)
            {
                var s = new Sprite(16, 16, $"Cammina_{i}");
                s.InkColor = 6; // Yellow
                s.PaperColor = 1; // Blue
                s.Bright = true;
                // Disegna testa e corpo base
                for (int x = 6; x <= 9; x++)
                    for (int y = 2; y <= 5; y++) s.SetPixel(x, y, true);
                for (int y = 6; y <= 11; y++)
                {
                    s.SetPixel(7, y, true);
                    s.SetPixel(8, y, true);
                }
                // Gambe che camminano
                if (i == 0) { s.SetPixel(6, 12, true); s.SetPixel(6, 13, true); s.SetPixel(9, 12, true); s.SetPixel(9, 13, true); }
                else if (i == 1) { s.SetPixel(5, 12, true); s.SetPixel(4, 13, true); s.SetPixel(10, 12, true); s.SetPixel(11, 13, true); }
                else if (i == 2) { s.SetPixel(7, 12, true); s.SetPixel(7, 13, true); s.SetPixel(8, 12, true); s.SetPixel(8, 13, true); }
                else if (i == 3) { s.SetPixel(6, 12, true); s.SetPixel(5, 13, true); s.SetPixel(9, 12, true); s.SetPixel(10, 13, true); }

                proj.Sprites.Add(s);
            }
            proj.SelectedIndex = 0;
            LoadProject(proj);
        }

        private void ShowAboutDialog()
        {
            string msg = "ZX SPECTRUM SPRITE GENERATOR & STUDIO\n\n" +
                         "Programma per la creazione, il disegno, l'animazione e l'esportazione di sprite software per Sinclair ZX Spectrum (Z80).\n\n" +
                         "Caratteristiche:\n" +
                         "• Griglia di disegno pixel-art con evidenziazione celle 8x8 ZX Spectrum\n" +
                         "• Strumenti: Matita, Linea, Rettangoli, Cerchi, Secchiello di riempimento\n" +
                         "• Trasformazioni: Flip Orizzontale/Verticale, Rotazione 90°, Scorrimento pixel\n" +
                         "• Supporto Multi-sprite / Filmstrip per animazioni a frame\n" +
                         "• Palette ZX Spectrum con selezione Paper, Ink e Bright\n" +
                         "• Player per testare l'animazione in tempo reale a diverse velocità FPS\n" +
                         "• Esportazione istantanea in Assembly Z80 (db/defb), C/Z88DK, Sinclair BASIC (DATA) e file binari (.bin)\n" +
                         "• Generazione maschera per istruzioni AND/OR Z80 dello Spectrum\n" +
                         "• Precaricato con i dati dei 4 sprite nemico (16x16 pixel, 32 byte ciascuno).\n\n" +
                         "Sviluppato per programmatori retro Sinclair ZX Spectrum.";

            MessageBox.Show(msg, "Informazioni sul Programma", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
