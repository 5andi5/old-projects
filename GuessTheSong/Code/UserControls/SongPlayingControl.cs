using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GuessTheSong.UserControls
{
    public partial class SongPlayingControl : RoundUserControlBase
    {
        private SongRoundSettings m_Settings = null;

        public SongPlayingControl()
            : base(true)
        {
            InitializeComponent();
            m_Settings = null;
            btnPlaySong.Visible = false;
            btnShowAnswer.Visible = false;
        }

        public void LoadSettings(SongRoundSettings settings)
        {
            m_Settings = settings;
            if (m_Settings == null)
            {
                btnPlaySong.Visible = false;
                btnShowAnswer.Visible = false;
            }
            else
            {
                btnPlaySong.Visible = true;
                btnShowAnswer.Visible = settings.HasAnswer;
            }
        }

        private void btnPlaySong_Click(object sender, EventArgs e)
        {
            if (m_Settings != null)
            {
                AudioPlayerSettings processSettings = m_Settings.SongPlayerSettings;
                processSettings.SetAudioFileName(m_Settings.SongFileName);
                try
                {
                    ProcessExecuter.Execute(processSettings);
                }
                catch (Exception ex)
                {
                    string errorMessage = string.Format("Atgadījās kļūda, mēģinot atskaņot audio failu: {0}\r\n\r\nAtskaņotājs: {1}\r\nParametri: {2}\r\nAudio fails: {3}",
                        ex.Message, processSettings.ProcessName, processSettings.GetParametersString(), m_Settings.SongFileName);
                    MessageBox.Show(errorMessage, "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnShowAnswer_Click(object sender, EventArgs e)
        {
            MessageBox.Show(m_Settings.Answer, "Pareizā atbilde", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        public override void LoadSettings(RoundSettingsBase settings)
        {
            if (!(settings is SongRoundSettings))
            {
                throw new Exception("SongPlayingControl accepts only SongRoundSettings class as parameter");
            }
            LoadSettings((SongRoundSettings)settings);
        }
    }
}
