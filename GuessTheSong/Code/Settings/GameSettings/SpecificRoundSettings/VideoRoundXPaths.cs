using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuessTheSong
{
    public static class VideoRoundXPaths
    {
        public static string Team1MovieXPath = "team1/@videoName";
        public static string Team1AnswerXPath = "team1/answer";
        public static string Team2MovieXPath = "team2/@videoName";
        public static string Team2AnswerXPath = "team2/answer";
        public static string RoundNodeName = "videoRound";
    }
}
