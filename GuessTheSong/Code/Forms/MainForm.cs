using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GuessTheSong.UserControls;
using GuessTheSong.Properties;
using GuessTheSong.Forms;
using System.IO;

namespace GuessTheSong
{

    public partial class MainForm : Form
    {
        private TeamsInfo m_Info;
        private int m_NavigationPanelMinSize;
        private ProgramSettings m_ProgramSetings = null;
        private bool m_GameHasBeenLoaded = false;

        #region Global

        public MainForm()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                m_ProgramSetings = ProgramSettingsManager.LoadSettings();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
                m_ProgramSetings = ProgramSettingsManager.GetDefaultSettings();
            }

            m_Info = new TeamsInfo(lblTeam1Score, lblTeam2Score, lblTeam1Name, lblTeam2Name);
            ChangeTeam1Name(m_ProgramSetings.Team1Name);
            ChangeTeam2Name(m_ProgramSetings.Team2Name);
            txtVideoPlayer.Text = m_ProgramSetings.VideoPlayerName;
            txtVideoPlayerParameters.Text = m_ProgramSetings.VideoPlayerParameters;
            txtAudioPlayer.Text = m_ProgramSetings.AudioPlayerName;
            txtAudioPlayerParameters.Text = m_ProgramSetings.AudioPlayerParameters;
            m_NavigationPanelMinSize = splitContainer1.Panel2MinSize;
            chbShowNavigationBar.Checked = m_ProgramSetings.ShowNavigationBar;
            SetTeamNameFont(m_ProgramSetings.TeamNameFont);
            SetTeamScoreFont(m_ProgramSetings.TeamScoreFont);
            SetSongFullTextFont(m_ProgramSetings.SongFullTextFont);
            WordGuessingControl.DisplayFont = m_ProgramSetings.WordGuessingFont;
            chbRemovePlayedGames.Checked = m_ProgramSetings.ExcludeOpenedGames;
            LoadBackgroundImage(m_ProgramSetings.BackgroundImagePath);
            chbFullScreen.Checked = m_ProgramSetings.FullScreen;
        }

        private void ShowErrorMessage(Exception ex)
        {
            MessageBox.Show(ex.Message, "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                m_ProgramSetings.AudioPlayerName = txtAudioPlayer.Text;
                m_ProgramSetings.AudioPlayerParameters = txtAudioPlayerParameters.Text;
                m_ProgramSetings.VideoPlayerName = txtVideoPlayer.Text;
                m_ProgramSetings.VideoPlayerParameters = txtVideoPlayerParameters.Text;
                m_ProgramSetings.FullScreen = chbFullScreen.Checked;
                ProgramSettingsManager.SaveSettings(m_ProgramSetings);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Score

        private void lblTeam2Score_Click(object sender, EventArgs e)
        {
            MouseEventArgs args = e as MouseEventArgs;
            if (args != null)
            {
                if (args.Button == MouseButtons.Left)
                {
                    m_Info.Team2Score++;
                }
                else if (args.Button == MouseButtons.Right)
                {
                    m_Info.Team2Score--;
                }
            }
        }


        private void lblTeam1Score_Click(object sender, EventArgs e)
        {
            MouseEventArgs args = e as MouseEventArgs;
            if (args != null)
            {
                if (args.Button == MouseButtons.Left)
                {
                    m_Info.Team1Score++;
                }
                else if (args.Button == MouseButtons.Right)
                {
                    m_Info.Team1Score--;
                }
            }
        }

        #endregion

        #region Team name changing

        private void lblTeam1Name_DoubleClick(object sender, EventArgs e)
        {
            StringEnterForm form = new StringEnterForm("Ievadiet komandas nosaukumu:", lblTeam1Name.Text);
            form.OnFormClosingWithValue += ChangeTeam1Name;
            form.ShowDialog();
        }

        private void ChangeTeam1Name(string newName)
        {
            lblTeam1Name.Text = newName;
            txtFirstTeamName.Text = newName;
            m_ProgramSetings.Team1Name = newName;
            ChangeTeamTabNames(newName, 1);
        }

        private void ChangeTeam2Name(string newName)
        {
            lblTeam2Name.Text = newName;
            txtSecondTeamName.Text = newName;
            m_ProgramSetings.Team2Name = newName;
            ChangeTeamTabNames(newName, 2);
        }

        private void lblTeam2Name_DoubleClick(object sender, EventArgs e)
        {
            StringEnterForm form = new StringEnterForm("Ievadiet komandas nosaukumu:", lblTeam2Name.Text);
            form.OnFormClosingWithValue += ChangeTeam2Name;
            form.ShowDialog();
        }

        private void ChangeTeamTabNames(string newName, int teamNumber)
        {
            foreach (Control control in RoundsTabControl.Controls)
            {
                if (control is TabPage && (control != SettingsTabPage))
                {
                    if (control.Controls.Count > 0)
                    {
                        Control teamsTabControl = control.Controls[0];
                        if ((teamsTabControl != null) && (teamsTabControl is TabControl))
                        {
                            if (teamsTabControl.Controls.Count >= teamNumber - 1)
                            {
                                Control teamPage = teamsTabControl.Controls[teamNumber - 1];
                                if (teamPage is TabPage)
                                {
                                    ((TabPage)teamPage).Text = newName;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void txtFirstTeamName_Leave(object sender, EventArgs e)
        {
            ChangeTeam1Name(txtFirstTeamName.Text);
        }

        private void txtSecondTeamName_Leave(object sender, EventArgs e)
        {
            ChangeTeam2Name(txtSecondTeamName.Text);
        }

        #endregion

        #region Game loading

        private void btnLoadNewGame_Click(object sender, EventArgs e)
        {
            LoadNewGame();
        }

        private void LoadNewGame()
        {
            try
            {
                GetSongPlayerSettings getSongPlayerNameFunction = new GetSongPlayerSettings(GetSongPlayerInfo);
                GetVideoPlayerSettings getMoviePlayerNameFunction = new GetVideoPlayerSettings(GetVidePlayerInfo);
                GameSettings settings = GameSettingsReader.GetGameSettings(chbRemovePlayedGames.Checked, getSongPlayerNameFunction, getMoviePlayerNameFunction);
                GenerateTabsForRounds(settings, lblTeam1Name.Text, lblTeam2Name.Text);
                m_Info.Team1Score = 0;
                m_Info.Team2Score = 0;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }
        }

        private AudioPlayerSettings GetSongPlayerInfo()
        {
            AudioPlayerSettings settings = new AudioPlayerSettings();
            settings.ProcessName = txtAudioPlayer.Text;
            settings.ParametersPattern = txtAudioPlayerParameters.Text;
            return settings;
        }

        private VideoPlayerSettings GetVidePlayerInfo()
        {
            VideoPlayerSettings settings = new VideoPlayerSettings();
            settings.ProcessName = txtVideoPlayer.Text;
            settings.ParametersPattern = txtVideoPlayerParameters.Text;
            return settings;
        }

        #region AddTabs

        private RoundUserControlBase CreateControlForRoundSettings(RoundSettingsBase settings)
        {
            RoundUserControlBase control;
            if (settings is ImageRoundSettings)
            {
                control = new ImageDisplayControl();
            }
            else if (settings is SongRoundSettings)
            {
                control = new SongPlayingControl();
            }
            else if (settings is VideoRoundSettings)
            {
                control = new VideoPlayingControl();
            }
            else if (settings is WordGuessingRoundSettings)
            {
                control = new WordGuessingControl();
            }
            else
            {
                throw new Exception(string.Format(
                    "Unknown round settings type: {0}", settings.GetType().Name));
            }
            return control;
        }

        private void AddTab(int index, BothTeamSettings roundSettings, string team1Name, string team2Name)
        {
            TabPage roundTabPage = new TabPage();
            TabControl teamsTabControl = new TabControl();
            TabPage team1TabPage = new TabPage();
            TabPage team2TabPage = new TabPage();
            RoundUserControlBase team1Control = CreateControlForRoundSettings(roundSettings.Team1Settings);
            RoundUserControlBase team2Control = CreateControlForRoundSettings(roundSettings.Team2Settings);

            roundTabPage.SuspendLayout();
            teamsTabControl.SuspendLayout();
            team1TabPage.SuspendLayout();
            team2TabPage.SuspendLayout();
            team1Control.SuspendLayout();
            team2Control.SuspendLayout();

            roundTabPage.Controls.Add(teamsTabControl);
            roundTabPage.Name = "TeamTabControl_" + index.ToString();
            roundTabPage.Padding = new System.Windows.Forms.Padding(3);
            roundTabPage.Text = roundSettings.RoundName;
            roundTabPage.UseVisualStyleBackColor = true;

            teamsTabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            teamsTabControl.Controls.Add(team1TabPage);
            teamsTabControl.Controls.Add(team2TabPage);
            teamsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            teamsTabControl.Name = "TeamsTabControl_" + index.ToString();
            teamsTabControl.SelectedIndex = 0;

            team1TabPage.Controls.Add(team1Control);
            team1TabPage.Name = "Team1TabPage_" + index.ToString();
            team1TabPage.Padding = new System.Windows.Forms.Padding(3);
            team1TabPage.Text = team1Name;
            team1TabPage.UseVisualStyleBackColor = true;

            team1Control.Dock = System.Windows.Forms.DockStyle.Fill;
            team1Control.Name = "Team1UserControl_" + index.ToString();

            team2TabPage.Controls.Add(team2Control);
            team2TabPage.Name = "Team2TabPage_" + index.ToString();
            team2TabPage.Padding = new System.Windows.Forms.Padding(3);
            team2TabPage.Text = team2Name;
            team2TabPage.UseVisualStyleBackColor = true;

            team2Control.Dock = System.Windows.Forms.DockStyle.Fill;
            team2Control.Name = "Team2UserControl_" + index.ToString();

            RoundsTabControl.Controls.Add(roundTabPage);

            team1Control.LoadSettings(roundSettings.Team1Settings);
            team2Control.LoadSettings(roundSettings.Team2Settings);

            team2Control.ResumeLayout(false);
            team1Control.ResumeLayout(false);
            team2TabPage.ResumeLayout(false);
            team1TabPage.ResumeLayout(false);
            teamsTabControl.ResumeLayout(false);
            roundTabPage.ResumeLayout(false);
        }

        private void GenerateTabsForRounds(GameSettings settings, string team1Name, string team2Name)
        {
            this.SuspendLayout();
            RoundsTabControl.SuspendLayout();

            DeletePreviousGameTabs();

            for (int index = 0; index < settings.Rounds.Count; index++)
            {
                AddTab(index + 1, settings.Rounds[index], team1Name, team2Name);
            }

            this.RoundsTabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void DeletePreviousGameTabs()
        {
            for (int index = RoundsTabControl.Controls.Count - 1; index >= 0; index--)
            {
                Control control = RoundsTabControl.Controls[index];
                if (control is TabPage)
                {
                    if (control != this.SettingsTabPage)
                    {
                        RemoveSubControls(control);
                        RoundsTabControl.Controls.RemoveAt(index);
                    }
                }
            }
        }

        private void RemoveSubControls(Control controlToDelete)
        {
            for (int index = controlToDelete.Controls.Count - 1; index >= 0; index--)
            {
                RemoveSubControls(controlToDelete.Controls[index]);
                controlToDelete.Controls.RemoveAt(index);
            }
        }

        #endregion

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            TryLoadGame();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TryLoadGame();
        }

        private void TryLoadGame()
        {

            if (m_GameHasBeenLoaded)
            {
                if (MessageBox.Show("Vai tiešām ielādēt jaunu spēli?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    LoadNewGame();
                    ToFirstStage();
                }
            }
            else
            {
                LoadNewGame();
                ToFirstStage();
                m_GameHasBeenLoaded = true;
            }
        }

        private void chbRemovePlayedGames_CheckedChanged(object sender, EventArgs e)
        {
            m_ProgramSetings.ExcludeOpenedGames = chbRemovePlayedGames.Checked;
        }

        #endregion

        #region Player settings

        private void btnInsertVideoFileName_Click(object sender, EventArgs e)
        {
            txtVideoPlayerParameters.Text =
                txtVideoPlayerParameters.Text.Insert(txtVideoPlayerParameters.SelectionStart, VideoPlayerSettings.VideoFileNameMarker);
        }

        private void btnGetVideoPlayerPath_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtVideoPlayer.Text = openFileDialog1.FileName;
            }
        }

        private void btnGetAudioPlayerName_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtAudioPlayer.Text = openFileDialog1.FileName;
            }
        }

        private void btnInsertAudioFileName_Click(object sender, EventArgs e)
        {
            txtAudioPlayerParameters.Text =
                txtAudioPlayerParameters.Text.Insert(txtAudioPlayerParameters.SelectionStart, AudioPlayerSettings.AudioFileNameMarker);
        }

        #endregion

        #region Tab navigation

        private void ToLastStage()
        {
            MoveToNewRoundEnd(GetLastRoundIndex());
        }

        private void ToFirstStage()
        {
            MoveToNewRound(GetFirstRoundIndex());
        }

        private void ToNextStage()
        {
            if (SettingsTabActive())
            {
                MoveToNewRound(GetFirstRoundIndex());
            }
            else
            {
                TabControl subTabControl = GetSubTabControl(RoundsTabControl.SelectedIndex);
                if (subTabControl != null)
                {
                    if (subTabControl.SelectedIndex == subTabControl.TabCount - 1)
                    {
                        if (!LastRoundTabActive())
                        {
                            MoveToNewRound(RoundsTabControl.SelectedIndex + 1);
                        }
                        else
                        {
                            MessageBox.Show("Beigas klāt", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                    else
                    {
                        subTabControl.SelectedIndex++;
                    }
                }
            }
        }

        private void MoveToNewRound(int nextRoundIndex)
        {
            RoundsTabControl.SelectedIndex = nextRoundIndex;
            TabControl newSubTabControl = GetSubTabControl(RoundsTabControl.SelectedIndex);
            if (newSubTabControl != null)
            {
                newSubTabControl.SelectedIndex = 0;
            }
        }

        private void MoveToNewRoundEnd(int previousRoundIndex)
        {
            RoundsTabControl.SelectedIndex = previousRoundIndex;
            TabControl subTabControl = GetSubTabControl(RoundsTabControl.SelectedIndex);
            if (subTabControl != null)
            {
                subTabControl.SelectedIndex = subTabControl.TabCount - 1;
            }
        }


        private void ToPreviousStage()
        {
            if (SettingsTabActive())
            {
                MoveToNewRoundEnd(GetLastRoundIndex());
            }
            else
            {
                TabControl subTabControl = GetSubTabControl(RoundsTabControl.SelectedIndex);
                if (subTabControl != null)
                {
                    if (subTabControl.SelectedIndex == 0)
                    {
                        if (!FirstRoundTabActive())
                        {
                            MoveToNewRoundEnd(RoundsTabControl.SelectedIndex - 1);
                        }
                    }
                    else
                    {
                        subTabControl.SelectedIndex--;
                    }
                }
            }
        }

        private int GetLastRoundIndex()
        {
            return RoundsTabControl.TabCount - 1;
        }

        private int GetFirstRoundIndex()
        {
            //settings tab + 1
            return 1;
        }

        private TabControl GetSubTabControl(int tabIndex)
        {
            TabControl subTabControl = null;
            if (tabIndex >= 0 && tabIndex < RoundsTabControl.TabCount && !IsSettingsTab(tabIndex))
            {
                subTabControl = RoundsTabControl.TabPages[tabIndex].Controls[0] as TabControl;
            }
            return subTabControl;
        }

        private bool SettingsTabActive()
        {
            return IsSettingsTab(RoundsTabControl.SelectedIndex);
        }

        private bool IsSettingsTab(int index)
        {
            return index == 0;
        }

        private bool LastRoundTabActive()
        {
            return RoundsTabControl.SelectedIndex == GetLastRoundIndex();
        }

        private bool FirstRoundTabActive()
        {
            return RoundsTabControl.SelectedIndex == GetFirstRoundIndex();
        }

        private void btnFirstStage_Click(object sender, EventArgs e)
        {
            ToFirstStage();
        }

        private void btnPreviousStage_Click(object sender, EventArgs e)
        {
            ToPreviousStage();
        }

        private void btnNextStage_Click(object sender, EventArgs e)
        {
            ToNextStage();
        }

        private void btnLastStage_Click(object sender, EventArgs e)
        {
            ToLastStage();
        }


        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            ToNextStage();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ToPreviousStage();
        }

        #endregion

        #region Show/hide navigation panel

        private void HideNavigationPanel()
        {
            splitContainer1.Panel2MinSize = 0;
            //tableLayoutPanel1.Show();
            tableLayoutPanel1.Visible = false;
            //splitContainer1.SplitterDistance += m_NavigationPanelMinSize;//splitContainer1.Panel2.Height;
            splitContainer1.SplitterDistance = splitContainer1.Height;
        }


        private void ShowNavigationPanel()
        {
            splitContainer1.Panel2MinSize = m_NavigationPanelMinSize;
            //tableLayoutPanel1.Hide();
            tableLayoutPanel1.Visible = true;
            splitContainer1.SplitterDistance -= m_NavigationPanelMinSize;// splitContainer1.Panel2.Height;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            m_ProgramSetings.ShowNavigationBar = chbShowNavigationBar.Checked;
            if (chbShowNavigationBar.Checked)
            {
                ShowNavigationPanel();
            }
            else
            {
                HideNavigationPanel();
            }
        }

        #endregion

        #region Font processing

        private void SetWordGuessingControlFont(Font value)
        {
            foreach (TabPage roundTab in this.RoundsTabControl.TabPages)
            {
                foreach (Control teamTabControlCandidate in roundTab.Controls)
                {
                    if (teamTabControlCandidate is TabControl)
                    {
                        TabControl teamTabControl = (TabControl)teamTabControlCandidate;
                        foreach (TabPage teamTab in teamTabControl.TabPages)
                        {
                            foreach (Control wgcCandidate in teamTab.Controls)
                            {
                                if (wgcCandidate is WordGuessingControl)
                                {
                                    ((WordGuessingControl)wgcCandidate).Font = value;
                                }
                            }
                        }
                    }
                }
            }
        }


        private void SetTeamNameFont(Font value)
        {
            m_ProgramSetings.TeamNameFont = value;
            lblTeam1Name.Font = m_ProgramSetings.TeamNameFont;
            lblTeam2Name.Font = m_ProgramSetings.TeamNameFont;
        }

        private void SetTeamScoreFont(Font value)
        {
            m_ProgramSetings.TeamScoreFont = value;
            lblTeam1Score.Font = m_ProgramSetings.TeamScoreFont;
            lblTeam2Score.Font = m_ProgramSetings.TeamScoreFont;
        }

        private void btnTeamScoresFont_Click(object sender, EventArgs e)
        {
            fdFonts.Font = m_ProgramSetings.TeamScoreFont;
            if (fdFonts.ShowDialog() == DialogResult.OK)
            {
                SetTeamScoreFont(fdFonts.Font);
            }
        }

        private void btnWordGuessingControlFont_Click(object sender, EventArgs e)
        {
            fdFonts.Font = m_ProgramSetings.WordGuessingFont;
            if (fdFonts.ShowDialog() == DialogResult.OK)
            {
                WordGuessingControl.DisplayFont = fdFonts.Font;
                m_ProgramSetings.WordGuessingFont = fdFonts.Font;
                SetWordGuessingControlFont(fdFonts.Font);
            }
        }

        private void btnTeamNameLabelFont_Click(object sender, EventArgs e)
        {
            fdFonts.Font = m_ProgramSetings.TeamNameFont;
            if (fdFonts.ShowDialog() == DialogResult.OK)
            {
                SetTeamNameFont(fdFonts.Font);
            }
        }

        private void btnSongFullTextFont_Click(object sender, EventArgs e)
        {
            fdFonts.Font = m_ProgramSetings.SongFullTextFont;
            if (fdFonts.ShowDialog() == DialogResult.OK)
            {
                WordGuessingControl.DisplayFont = fdFonts.Font;
                m_ProgramSetings.SongFullTextFont = fdFonts.Font;
                SetSongFullTextFont(fdFonts.Font);
            }
        }

        private void SetSongFullTextFont(Font font)
        {
            TextDisplayForm.TextFont = font;
        }

        #endregion

        #region Edit background image

        private void btnOpenBackgroundPattern_Click(object sender, EventArgs e)
        {
            if (ofdBackgroundImage.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string executableDirectory = Application.ExecutablePath;
                    executableDirectory = Path.GetDirectoryName(executableDirectory);
                    string path = ofdBackgroundImage.FileName;
                    path = EvaluateRelativePath(executableDirectory, path);
                    m_ProgramSetings.BackgroundImagePath = path;
                    LoadBackgroundImage(path);
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex);
                }
            }
        }

        private static string EvaluateRelativePath(string mainDirPath, string absoluteFilePath)
        {
            string[] firstPathParts = mainDirPath.Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);
            string[] secondPathParts = absoluteFilePath.Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);

            int sameCounter = 0;
            for (int i = 0; i < Math.Min(firstPathParts.Length, secondPathParts.Length); i++)
            {
                if (!firstPathParts[i].ToLower().Equals(secondPathParts[i].ToLower()))
                {
                    break;
                }
                sameCounter++;
            }

            if (sameCounter == 0)
            {
                return absoluteFilePath;
            }

            string newPath = String.Empty;
            for (int i = sameCounter; i < firstPathParts.Length; i++)
            {
                if (i > sameCounter)
                {
                    newPath += Path.DirectorySeparatorChar;
                }
                newPath += "..";
            }
            if (newPath.Length == 0)
            {
                newPath = ".";
            }
            for (int i = sameCounter; i < secondPathParts.Length; i++)
            {
                newPath += Path.DirectorySeparatorChar;
                newPath += secondPathParts[i];
            }
            return newPath;
        }

        private void LoadBackgroundImage(string filePath)
        {
            Image image = null;
            if (string.IsNullOrEmpty(filePath))
            {
                image = Resources.Pattern;
            }
            else
            {
                try
                {
                    image = Image.FromFile(filePath);
                }
                catch (Exception ex)
                {
                    string message = string.Format("Kļūda ielādējot fona attēlu: {0}", ex.Message);
                    Exception newEx = new Exception(message, ex);
                    ShowErrorMessage(newEx);
                }
            }
            if (image != null)
            {
                SetBackgroundImage(image);
            }
        }



        private void SetBackgroundImage(Image image)
        {
            tableLayoutPanel1.BackgroundImage = image;
            splitContainer3.BackgroundImage = image;
            TextDisplayForm.BgImage = image;
            RoundUserControlBase.RoundBackgroundImage = image;
            foreach (TabPage page in RoundsTabControl.TabPages)
            {
                foreach (Control control in page.Controls)
                {
                    if (control is TabControl)
                    {
                        TabControl userTabControl = (TabControl)control;
                        foreach (TabPage userPage in userTabControl.TabPages)
                        {
                            foreach (Control userControl in userPage.Controls)
                            {
                                TrySetBackground(userControl);
                            }
                        }
                    }
                }
            }
        }

        private void TrySetBackground(Control control)
        {
            if (control is RoundUserControlBase)
            {
                RoundUserControlBase roundControl = (RoundUserControlBase)control;
                roundControl.SetBackgroundImage();
            }
        }

        #endregion

        #region Full screen mode
        private void chbFullScreen_CheckedChanged(object sender, EventArgs e)
        {
            if (chbFullScreen.Checked)
            {
                EnterFullScreenMode();
            }
            else
            {
                ExitFullScreenMode();
            }
        }

        private void EnterFullScreenMode()
        {
            this.SuspendLayout();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.ResumeLayout();
        }

        private void ExitFullScreenMode()
        {
            this.SuspendLayout();
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.TopMost = false;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Normal;
            this.ResumeLayout();
        }
        #endregion
    }
}
