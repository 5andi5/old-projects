using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GuessTheSong
{    
    public delegate VideoPlayerSettings GetVideoPlayerSettings();

    public class VideoRoundSettings : RoundSettingsBase
    {
        public static new readonly string DefaultRoundName = "Tauta dungo";        

        private event GetVideoPlayerSettings m_GetVideoPlayerSettings;
        public VideoPlayerSettings VideoPlayerSettings
        {
            get
            {
                VideoPlayerSettings settings= m_GetVideoPlayerSettings();
                return settings;
            }
        }

        private string m_VideoFileName;
        public string VideoFileName
        {
            get { return m_VideoFileName; }
            set { m_VideoFileName = value; }
        }

        private bool m_HasAnswer;
        public bool HasAnswer
        {
            get { return m_HasAnswer; }
        }

        private string m_Answer;
        public string Answer
        {
            get { return m_Answer; }
            set
            {
                m_Answer = value;
                if (string.IsNullOrEmpty(m_Answer))
                {
                    m_HasAnswer = false;
                }
                else
                {
                    m_HasAnswer = true;
                }
            }
        }
    
        public VideoRoundSettings(string videoFileName, string answer, string settingsFilePath, string videoDirectory, GetVideoPlayerSettings videoPlayerNameGettingFunction)
        {
            if(string.IsNullOrEmpty(videoFileName)){
                throw new InvalidGameSettingsException("Nav norādīts video fails", settingsFilePath);
            }

            if (videoPlayerNameGettingFunction == null)
            {
                throw new Exception("Movie player name getting method must be specified");
            }
            m_GetVideoPlayerSettings += videoPlayerNameGettingFunction;

            m_VideoFileName = Path.Combine(videoDirectory, videoFileName);

            if (!File.Exists(m_VideoFileName))
            {
                throw new InvalidGameSettingsException(string.Format("Video fails '{0}' neeksistē", m_VideoFileName), settingsFilePath);
            }

            Answer = answer;
        }
    }
}
