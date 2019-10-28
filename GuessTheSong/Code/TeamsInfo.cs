using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GuessTheSong
{
    public class TeamsInfo
    {

        public TeamsInfo(Label team1ScoreLabel, Label team2ScoreLabel, Label team1NameLabel, Label team2NameLabel)
        {
            m_Team1ScoreLabel = team1ScoreLabel;
            m_Team2ScoreLabel = team2ScoreLabel;
            m_Team1NameLabel = team1NameLabel;
            m_Team2NameLabel = team2NameLabel;
        }

        private Label m_Team1ScoreLabel;

        private int m_Team1Score = 0;
        public int Team1Score
        {
            get { return m_Team1Score; }
            set
            {
                if (value >= 0)
                {
                    m_Team1Score = value;
                    m_Team1ScoreLabel.Text = m_Team1Score.ToString();
                }
            }
        }


        private Label m_Team2ScoreLabel;

        private int m_Team2Score = 0;
        public int Team2Score
        {
            get { return m_Team2Score; }
            set
            {
                if (value >= 0)
                {
                    m_Team2Score = value;
                    m_Team2ScoreLabel.Text = m_Team2Score.ToString();
                }
            }
        }


        private Label m_Team1NameLabel;

        private string m_Team1Name = "Zebras";
        public string Team1Name
        {
            get { return m_Team1Name; }
            set
            {
                m_Team1Name = value;
                m_Team1NameLabel.Text = value;
            }
        }


        private Label m_Team2NameLabel;

        private string m_Team2Name = "Kamieļi";
        public string Team2Name
        {
            get { return m_Team2Name; }
            set
            {
                m_Team2Name = value;
                m_Team2NameLabel.Text = m_Team2Name;
            }
        }

        
    }
}
