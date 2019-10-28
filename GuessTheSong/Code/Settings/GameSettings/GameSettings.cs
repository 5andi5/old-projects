using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GuessTheSong
{
    public class GameSettings
    {
        private List<BothTeamSettings> m_Rounds;

        public List<BothTeamSettings> Rounds
        {
            get { return m_Rounds; }
            set { m_Rounds = value; }
        }

        public GameSettings()
        {
            m_Rounds = new List<BothTeamSettings>();
        }
    }
}
