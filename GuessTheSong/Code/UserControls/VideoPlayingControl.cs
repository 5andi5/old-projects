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
    public partial class VideoPlayingControl : RoundUserControlBase
    {
        private VideoRoundSettings m_Settings = null;

        public VideoPlayingControl():base(true)
        {
            InitializeComponent();
            m_Settings = null;
            btnPlayVideo.Visible = false;
            btnShowAnswer.Visible = false;
        }

        public void LoadSettings(VideoRoundSettings settings)
        {
            m_Settings = settings;
            if (m_Settings == null)
            {
                btnPlayVideo.Visible = false;
                btnShowAnswer.Visible = false;
            }
            else
            {
                btnPlayVideo.Visible = true;
                btnShowAnswer.Visible = m_Settings.HasAnswer;
            }
        }

        private void btnPlayVideo_Click(object sender, EventArgs e)
        {
            if (m_Settings != null)
            {
                VideoPlayerSettings processSettings = m_Settings.VideoPlayerSettings;
                processSettings.SetVideoFileName(m_Settings.VideoFileName);
                try
                {
                    ProcessExecuter.Execute(processSettings);
                }
                catch (Exception ex)
                {
                    string errorMessage = string.Format("Atgadījās kļūda mēģinot atskaņot video: {0}\r\n\r\nAtskaņotājs: {1}\r\nParametri: {2}\r\nVideo fails: {3}",
                        ex.Message, processSettings.ProcessName, processSettings.GetParametersString(), m_Settings.VideoFileName);
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
            if (!(settings is VideoRoundSettings))
            {
                throw new Exception("VideoPlayingControl accepts only VideoRoundSettings class as parameter");
            }
            LoadSettings((VideoRoundSettings)settings);
        }
    }
}
