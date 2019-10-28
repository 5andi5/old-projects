using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace GuessTheSong
{


    public class GameSettingsReader
    {
        private static string c_RoundsXPath = "/game/rounds/round";
        private static string c_RoundNameAttributeName = "name";


        private static XmlDocument GetGameSettingsXml(out string filePath)
        {
            DirectoryInfo dir = new DirectoryInfo(DirectoryNames.GamesSettingsDirectory);
            FileInfo[] files = dir.GetFiles(DirectoryNames.SettingsFilesNamePattern);
            if (files == null || files.Length == 0)
            {
                throw new GameSettingsNotFoundException(string.Format(
                    "Netika atrasts neviens spēles konfigurācijas fails.\r\nMeklēšana notika direktorijā '{0}'", dir.FullName));
            }

            Random rnd = new Random();
            int pickedFileNumber = rnd.Next(0, files.Length - 1);

            XmlDocument result = new XmlDocument();
            filePath = files[pickedFileNumber].FullName;
            result.Load(filePath);
            return result;
        }

        public static GameSettings GetGameSettings(bool moveLoadeGame, GetSongPlayerSettings songPlayerNameGetterFunction, GetVideoPlayerSettings videoPlayerNameGetterFunction)
        {
            if (songPlayerNameGetterFunction == null)
            {
                throw new Exception("Song player name getting function must be specified");
            }
            if (videoPlayerNameGetterFunction == null)
            {
                throw new Exception("Video player name getting function must be specified");
            }

            string filePath;
            XmlDocument gameSettings = GetGameSettingsXml(out filePath);
            GameSettings settings  = LoadAllRoundSettings(gameSettings, filePath, songPlayerNameGetterFunction, videoPlayerNameGetterFunction);

            if (moveLoadeGame)
            {
                string fileName=Path.GetFileName(filePath);
                string destFilePath = Path.Combine(DirectoryNames.PlayedGamesSettingsDirectory, fileName);
                File.Move(filePath, destFilePath);
            }

            return settings;
        }

        private static GameSettings LoadAllRoundSettings(XmlDocument settingsXml, string settingsFilePath, GetSongPlayerSettings songPlayerNameGetterFunction, GetVideoPlayerSettings videoPlayerNameGetterFunction)
        {
            GameSettings parsedSettings = new GameSettings();
            XmlNodeList rounds = settingsXml.SelectNodes(c_RoundsXPath);
            if (rounds.Count == 0)
            {
                throw new InvalidGameSettingsException(string.Format(
                    "Spēles konfigurācijā nav norādīts neviens raunds.{0}({1})",
                    Environment.NewLine, c_RoundsXPath), settingsFilePath);
            }
            foreach (XmlNode round in rounds)
            {
                string roundName = null;
                XmlAttribute roundNameAttribute = round.Attributes[c_RoundNameAttributeName];
                if (roundNameAttribute != null)
                {
                    roundName = roundNameAttribute.InnerText;
                }

                if (round.SelectSingleNode(WordGuessingRoundXPaths.RoundNodeName) != null)
                {
                    XmlNode roundNode=round.SelectSingleNode(WordGuessingRoundXPaths.RoundNodeName) ;
                    BothTeamSettings newRoundSettings = GetWordGuessingRoundSettings(settingsFilePath, roundNode, roundName); 
                    parsedSettings.Rounds.Add(newRoundSettings);
                }
                else if (round.SelectSingleNode(ImageRoundXPaths.RoundNodeName) != null)
                {
                    XmlNode roundNode = round.SelectSingleNode(ImageRoundXPaths.RoundNodeName);
                    BothTeamSettings newRoundSettings = GetImageRoundSettings(settingsFilePath, roundNode, roundName);
                    parsedSettings.Rounds.Add(newRoundSettings);
                }
                else if(round.SelectSingleNode(SongRoundXPaths.RoundNodeName)!=null){
                    XmlNode roundNode = round.SelectSingleNode(SongRoundXPaths.RoundNodeName);
                    BothTeamSettings newRoundSettings = GetSongRoundSettings(settingsFilePath, roundNode, songPlayerNameGetterFunction, roundName);
                    parsedSettings.Rounds.Add(newRoundSettings);
                }
                else if (round.SelectSingleNode(VideoRoundXPaths.RoundNodeName) != null)
                {
                    XmlNode roundNode = round.SelectSingleNode(VideoRoundXPaths.RoundNodeName);
                    BothTeamSettings newRoundSettings = GetVideoRoundSettings(settingsFilePath, roundNode, videoPlayerNameGetterFunction, roundName);
                    parsedSettings.Rounds.Add(newRoundSettings);
                }
                else
                {
                    throw new InvalidGameSettingsException(string.Format("Nezināms spēles raunds{0}{1}",
                        Environment.NewLine, round.InnerXml), settingsFilePath);
                }
            }
            return parsedSettings;
        }

        private static BothTeamSettings GetWordGuessingRoundSettings(string settingsFilePath, XmlNode roundSettingsNode, string roundName)
        {
            if (string.IsNullOrEmpty(roundName))
            {
                roundName = WordGuessingRoundSettings.DefaultRoundName;
            }

            XmlNode badWordCountNode = roundSettingsNode.SelectSingleNode(WordGuessingRoundXPaths.BadWordCountXPath);
            if (badWordCountNode == null)
            {
                throw new InvalidGameSettingsException("Netika atrasts slikto (\"sarkano\") lauku skaits", settingsFilePath);
            }
            int badWordCount = int.Parse(badWordCountNode.Value);

            BothTeamSettings roundSettings = new BothTeamSettings(roundName);
            string fullSongText = null;
            XmlNode songTextNode = roundSettingsNode.SelectSingleNode(WordGuessingRoundXPaths.Team1FullSongTextXPath);
            if (songTextNode != null)
            {
                fullSongText = songTextNode.InnerText;
            }
            string[] words = ReadToStringArray(roundSettingsNode, WordGuessingRoundXPaths.Team1WordsXPath, settingsFilePath, true, false);
            roundSettings.Team1Settings= new WordGuessingRoundSettings(words, badWordCount, fullSongText, settingsFilePath);

            fullSongText = null;
            songTextNode = roundSettingsNode.SelectSingleNode(WordGuessingRoundXPaths.Team2FullSongTextXPath);
            if (songTextNode != null)
            {
                fullSongText = songTextNode.InnerText;
            }
            words = ReadToStringArray(roundSettingsNode, WordGuessingRoundXPaths.Team2WordsXPath, settingsFilePath, true, false);
            roundSettings.Team2Settings= new WordGuessingRoundSettings(words, badWordCount, fullSongText, settingsFilePath);
            return roundSettings;
        }


        private static BothTeamSettings GetSongRoundSettings(string settingsFilePath, XmlNode roundSettingsNode, GetSongPlayerSettings songPlayerNameGetterFunction, string roundName)
        {
            if (string.IsNullOrEmpty(roundName))
            {
                roundName = SongRoundSettings.DefaultRoundName;
            }

            BothTeamSettings roundSettings = new BothTeamSettings(roundName);

            XmlNode node = roundSettingsNode.SelectSingleNode(SongRoundXPaths.Team1SongXPath);
            if (node == null)
            {
                throw new InvalidGameSettingsException("Netika atrasta 1. komandas dziesma", settingsFilePath);
            }
            string songFileName = node.Value;
            string answer = null;
            node = roundSettingsNode.SelectSingleNode(SongRoundXPaths.Team1AnswerXPath);
            if (node != null)
            {
                answer = node.InnerText;
            }
            roundSettings.Team1Settings = new SongRoundSettings(songFileName, answer, settingsFilePath, DirectoryNames.SongsDirectory, songPlayerNameGetterFunction);

            node = roundSettingsNode.SelectSingleNode(SongRoundXPaths.Team2SongXPath);
            if (node == null)
            {
                throw new InvalidGameSettingsException("Netika atrasta 2. komandas dziesma", settingsFilePath);
            }
            songFileName = node.Value;
            answer = null;
            node = roundSettingsNode.SelectSingleNode(SongRoundXPaths.Team2AnswerXPath);
            if (node != null)
            {
                answer = node.InnerText;
            }
            roundSettings.Team2Settings = new SongRoundSettings(songFileName, answer, settingsFilePath, DirectoryNames.SongsDirectory, songPlayerNameGetterFunction);

            return roundSettings;
        }


        private static BothTeamSettings GetVideoRoundSettings(string settingsFilePath, XmlNode roundSettingsNode, GetVideoPlayerSettings videoPlayerNameGetterFunction, string roundName)
        {
            if (string.IsNullOrEmpty(roundName))
            {
                roundName = VideoRoundSettings.DefaultRoundName;
            }

            BothTeamSettings roundSettings = new BothTeamSettings(roundName);

            XmlNode node = roundSettingsNode.SelectSingleNode(VideoRoundXPaths.Team1MovieXPath);
            if (node == null)
            {
                throw new InvalidGameSettingsException("Netika atrasts 1. komandas  video", settingsFilePath);
            }
            string videoFileName = node.Value;
            string answer = null;
            node = roundSettingsNode.SelectSingleNode(VideoRoundXPaths.Team1AnswerXPath);
            if (node != null)
            {
                answer = node.InnerText;
            }
            roundSettings.Team1Settings= new VideoRoundSettings(videoFileName, answer, settingsFilePath, DirectoryNames.MoviesDirectory, videoPlayerNameGetterFunction);

            node = roundSettingsNode.SelectSingleNode(VideoRoundXPaths.Team2MovieXPath);
            if (node == null)
            {
                throw new InvalidGameSettingsException("Netika atrasts 2. komandas video ", settingsFilePath);
            }
            videoFileName = node.Value;
            answer = null;
            node = roundSettingsNode.SelectSingleNode(VideoRoundXPaths.Team2AnswerXPath);
            if (node != null)
            {
                answer = node.InnerText;
            }
            roundSettings.Team2Settings= new VideoRoundSettings(videoFileName, answer, settingsFilePath, DirectoryNames.MoviesDirectory, videoPlayerNameGetterFunction);

            return roundSettings;
        }


        private static BothTeamSettings GetImageRoundSettings(string settingsFilePath, XmlNode roundSettingsNode, string roundName)
        {
            if (string.IsNullOrEmpty(roundName))
            {
                roundName = ImageRoundSettings.DefaultRoundName;
            }

            BothTeamSettings roundSettings = new BothTeamSettings(roundName);

            string[] imageNames = ReadToStringArray(roundSettingsNode, ImageRoundXPaths.Team1ImagesXPath, settingsFilePath, true, false);
            roundSettings .Team1Settings= new ImageRoundSettings(imageNames, settingsFilePath, DirectoryNames.ImagesDirectory);

            imageNames = ReadToStringArray(roundSettingsNode, ImageRoundXPaths.Team2ImagesXPath, settingsFilePath, true, false);
            roundSettings.Team2Settings = new ImageRoundSettings(imageNames, settingsFilePath, DirectoryNames.ImagesDirectory);

            return roundSettings;
        }

        private static string[] ReadToStringArray(XmlNode node, string xPath, string settingsFileName, bool atLeastOneExpected, bool allowEmpty)
        {
            XmlNodeList nodes = node.SelectNodes(xPath);
            if (atLeastOneExpected)
            {
                if (nodes == null || nodes.Count == 0)
                {
                    throw new InvalidGameSettingsException(string.Format("Nav norādīta vērtība elementam '{0}'", xPath), settingsFileName);
                }
            }
            string[] result = new string[nodes.Count];
            for (int index = 0; index < nodes.Count; index++)
            {
                string value = nodes[index].Value;
                if (!allowEmpty && string.IsNullOrEmpty(value))
                {
                    throw new InvalidGameSettingsException(string.Format("Norādīta tukša vērtība (\"\") elementam '{0}'", xPath), settingsFileName);
                }
                result[index] = value;
            }
            return result;
        }

    }
}

