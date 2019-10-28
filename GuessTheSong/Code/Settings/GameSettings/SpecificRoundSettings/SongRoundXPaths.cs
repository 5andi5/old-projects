using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuessTheSong
{
    public static class SongRoundXPaths
    {
        public static string Team1SongXPath = "team1/@songName";
        public static string Team1AnswerXPath = "team1/answer";
        public static string Team2SongXPath = "team2/@songName";
        public static string Team2AnswerXPath = "team2/answer";
        public static string RoundNodeName = "songRound";
    }
}
