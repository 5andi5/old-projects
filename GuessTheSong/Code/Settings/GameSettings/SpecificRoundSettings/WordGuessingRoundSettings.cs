using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GuessTheSong
{

    public class WordGuessingRoundSettings : RoundSettingsBase
    {
        public static new readonly string DefaultRoundName="Dziesmu minēšana";

        private static Random Rnd = new Random();

        public WordGuessingRoundSettings(string[] words, int badWordCount, string fullSongText, string settingsFilePath)
        {
            ValidateWords(words, badWordCount, settingsFilePath);          

            m_BadWordCount = badWordCount;
            m_Words = new WordInfo[words.Length];
            m_FullSongText = fullSongText;
            ReadToWordInfoList(words);

            GenerateBadWords();
        }

        private void ReadToWordInfoList(string[] words)
        {            
            for (int index = 0; index < words.Length; index++)
            {
                m_Words[index]= new WordInfo(words[index]);
            }
        }

        private static void ValidateWords(string[] words, int numberOfBadWords, string settingsFilePath)
        {
            if (words == null || words.Length == 0)
            {
                throw new InvalidGameSettingsException("Nav norādīts neviens vārds", settingsFilePath);
            }
            if (numberOfBadWords < 0)
            {
                throw new InvalidGameSettingsException("Slikto (\"sarkano\") vārdu skaits nedrīkst būt mazāks par nulli", settingsFilePath);
            }
            if (numberOfBadWords > words.Length)
            {
                throw new InvalidGameSettingsException("Slikto (\"sarkano\") vārdu skaits nedrīkst pārsniegt kopējo vārdu skaitu", settingsFilePath);
            }
        }

        private void GenerateBadWords()
        {            
            for (int index = 0; index < m_BadWordCount; index++)
            {
                //int badWordIndex = Rnd.Next(0, m_Words.Length-1);
                int badWordIndex = Rnd.Next(0, m_Words.Length);
                while (m_Words[badWordIndex].IsBadWord)
                {
                    //badWordIndex = Rnd.Next(0, m_Words.Length - 1);
                    badWordIndex = Rnd.Next(0, m_Words.Length);
                }
                m_Words[badWordIndex].IsBadWord = true;
            }
        }

        private int m_BadWordCount;
        public int BadWordCount
        {
            get { return m_BadWordCount; }
        }

        private WordInfo[] m_Words;
        public WordInfo[] Words
        {
            get { return m_Words; }
            set { m_Words = value; }
        }

        private string m_FullSongText = null;
        public string FullSongText{
            get { return m_FullSongText; }
    }

        public bool HasFullSongText
        {
            get { return !string.IsNullOrEmpty(m_FullSongText); }
        }
    }
}
