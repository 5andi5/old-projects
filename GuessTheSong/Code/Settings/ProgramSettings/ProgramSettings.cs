using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GuessTheSong
{
    [Serializable]
    public class ProgramSettings
    {
        private Font m_TeamNameFont;
        private Font m_TeamScoreFont;
        private Font m_WordGuessingFont;
        private Font m_SongFullTextFont;
        private SerializableFont m_TeamNameSerializableFont;
        private SerializableFont m_TeamScoreSerializableFont;
        private SerializableFont m_WordGuessingSerializableFont;
        private SerializableFont m_SongFullTextSerializableFont;

        public string Team1Name { get; set; }
        public string Team2Name { get; set; }
        public bool ShowNavigationBar { get; set; }
        public string BackgroundImagePath { get; set; }
        public string VideoPlayerName { get; set; }
        public string VideoPlayerParameters { get; set; }
        public string AudioPlayerName { get; set; }
        public string AudioPlayerParameters { get; set; }
        public bool ExcludeOpenedGames { get; set; }
        public bool FullScreen { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public Font TeamNameFont
        {
            get
            {
                return m_TeamNameFont;
            }
            set
            {
                m_TeamNameFont = value;
                m_TeamNameSerializableFont.Font = m_TeamNameFont;
            }
        }

        public SerializableFont TeamNameSerializableFont
        {
            get { return m_TeamNameSerializableFont; }
            set
            {
                m_TeamNameSerializableFont = value;
                m_TeamNameFont = m_TeamNameSerializableFont.Font;
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public Font TeamScoreFont
        {
            get
            {
                return m_TeamScoreFont;
            }
            set
            {
                m_TeamScoreFont = value;
                m_TeamScoreSerializableFont.Font = m_TeamScoreFont;
            }
        }

        public SerializableFont TeamScoreSerializableFont
        {
            get { return m_TeamScoreSerializableFont; }
            set
            {
                m_TeamScoreSerializableFont = value;
                m_TeamScoreFont = m_TeamScoreSerializableFont.Font;
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public Font WordGuessingFont
        {
            get
            {
                return m_WordGuessingFont;
            }
            set
            {
                m_WordGuessingFont = value;
                m_WordGuessingSerializableFont.Font = m_WordGuessingFont;
            }
        }

        public SerializableFont WordGuessingSerializableFont
        {
            get { return m_WordGuessingSerializableFont; }
            set
            {
                m_WordGuessingSerializableFont = value;
                m_WordGuessingFont = m_WordGuessingSerializableFont.Font;
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public Font SongFullTextFont
        {
            get
            {
                return m_SongFullTextFont;
            }
            set
            {
                m_SongFullTextFont = value;
                m_SongFullTextSerializableFont.Font = m_SongFullTextFont;
            }
        }

        public SerializableFont SongFullTextSerializableFont
        {
            get { return m_SongFullTextSerializableFont; }
            set
            {
                m_SongFullTextSerializableFont = value;
                m_SongFullTextFont = m_SongFullTextSerializableFont.Font;
            }
        }

        public ProgramSettings()
        {
            Team1Name = null;
            Team2Name = null;
            ShowNavigationBar = false;
            m_TeamNameFont = null;
            m_TeamNameSerializableFont = null;
            m_TeamScoreFont = null;
            m_TeamScoreSerializableFont = null;
            m_WordGuessingFont = null;
            m_WordGuessingSerializableFont = null;
            m_SongFullTextFont = null;
            m_SongFullTextSerializableFont = null;
            BackgroundImagePath = null;
            VideoPlayerName = null;
            VideoPlayerParameters = null;
            AudioPlayerName = null;
            AudioPlayerParameters = null;
            ExcludeOpenedGames = false;
            FullScreen = false;
        }

        public void SetDefault()
        {
            Team1Name = "Komanda 1";
            Team2Name = "Komanda 2";
            ShowNavigationBar = false;
            m_TeamNameFont = new Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            m_TeamNameSerializableFont = new SerializableFont();
            m_TeamNameSerializableFont.Font = m_TeamNameFont;
            m_TeamScoreFont = new Font("Microsoft Sans Serif", 72F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            m_TeamScoreSerializableFont = new SerializableFont();
            m_TeamScoreSerializableFont.Font = m_TeamScoreFont;
            m_WordGuessingFont = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(186)));
            m_WordGuessingSerializableFont = new SerializableFont();
            m_WordGuessingSerializableFont.Font = m_WordGuessingFont;
            m_SongFullTextFont = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(186)));
            m_SongFullTextSerializableFont = new SerializableFont();
            m_SongFullTextSerializableFont.Font = m_SongFullTextFont;
            BackgroundImagePath = null;
            VideoPlayerName = @"C:\Program Files\Windows Media Player\wmplayer.exe";
            VideoPlayerParameters = "\"" + VideoPlayerSettings.VideoFileNameMarker + "\" /fullscreen";
            AudioPlayerName = @"c:\Program Files\VideoLAN\VLC\vlc.exe";
            AudioPlayerParameters = "\"" + AudioPlayerSettings.AudioFileNameMarker + "\" --qt-start-minimized";
            ExcludeOpenedGames = false;
            FullScreen = false;
        }
    }
}