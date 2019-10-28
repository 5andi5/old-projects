using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GuessTheSong
{
    public delegate AudioPlayerSettings GetSongPlayerSettings();

    public class SongRoundSettings : RoundSettingsBase
    {
        public static new readonly string DefaultRoundName="Dziesmu mikslis";    

        private event GetSongPlayerSettings m_GetSongPlayerSettings;
        public AudioPlayerSettings SongPlayerSettings
        {
            get
            {
                AudioPlayerSettings settings= m_GetSongPlayerSettings();
                return settings;
            }
        }

        private string m_SongFileName;
        public string SongFileName
        {
            get { return m_SongFileName; }
            set { m_SongFileName = value; }
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
    

        public SongRoundSettings(string songFileName, string answer, string settingsFilePath, string songsDirectory, GetSongPlayerSettings songPlayerNameGettingFunction)
        {
            if(string.IsNullOrEmpty(songFileName)){
                throw new InvalidGameSettingsException("Nav norādīts dziesmas fails", settingsFilePath);
            }

            if (songPlayerNameGettingFunction == null)
            {
                throw new Exception("Song player name getting method must be specified");
            }
            m_GetSongPlayerSettings += songPlayerNameGettingFunction;

            m_SongFileName = Path.Combine(songsDirectory, songFileName);

            if (!File.Exists(m_SongFileName))
            {
                throw new InvalidGameSettingsException(string.Format("Dziesmas fails '{0}' neeksistē", m_SongFileName), settingsFilePath);
            }

            Answer = answer;
        }
    }
}
