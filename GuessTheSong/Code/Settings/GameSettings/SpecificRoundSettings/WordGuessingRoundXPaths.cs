using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuessTheSong
{
    public static class WordGuessingRoundXPaths
    {        
        public static string BadWordCountXPath = "@badWordCount";
        public static string Team1WordsXPath = "team1/word/@name";
        public static string Team2WordsXPath = "team2/word/@name";
        public static string RoundNodeName = "guessTheSongRound";
        public static string Team1FullSongTextXPath = "team1/fullSong";
        public static string Team2FullSongTextXPath = "team2/fullSong";
    }
}
