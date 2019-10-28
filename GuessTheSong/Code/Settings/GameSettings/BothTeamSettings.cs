using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GuessTheSong
{
    public class BothTeamSettings
    {
        private RoundSettingsBase m_Team1Settings;
        public RoundSettingsBase Team1Settings
        {
            get { return m_Team1Settings; }
            set { m_Team1Settings = value; }
        }

        private RoundSettingsBase m_Team2Settings;
        public RoundSettingsBase Team2Settings
        {
            get { return m_Team2Settings; }
            set { m_Team2Settings = value; }
        }

        private string m_RoundName;
        public string RoundName
        {
            get { return m_RoundName; }
            set { m_RoundName = value; }
        }

        public BothTeamSettings(string roundName)
        {
            m_RoundName = roundName;
        }
    }   
}
