using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GuessTheSong
{
    public static class DirectoryNames
    {
        private static readonly string c_GamesSettingsDirectory = "NewGames";
        public static string GamesSettingsDirectory
        {
            get { return GetDirectoryFullPath(c_GamesSettingsDirectory); }
        }

        private static readonly string c_PlayedGamesSettingsDirectory = "PlayedGames";
        public static string PlayedGamesSettingsDirectory
        {
            get { return GetDirectoryFullPath(c_PlayedGamesSettingsDirectory);}
        }

        private static readonly string c_SettingsFilesNamePattern = "*.xml";
        public static string SettingsFilesNamePattern
        {
            get { return c_SettingsFilesNamePattern;}
        }

        private static readonly string c_ImagesDirectory = "Images";
        public static string ImagesDirectory
        {
            get { return GetDirectoryFullPath(c_ImagesDirectory);}
        }

        private static readonly string c_SongsDirectory = "Songs";
        public static string SongsDirectory
        {
            get { return GetDirectoryFullPath(c_SongsDirectory);}
        }

        private static readonly string c_MoviesDirectory = "Videos";
        public static string MoviesDirectory
        {
            get { return GetDirectoryFullPath(c_MoviesDirectory);}
        }

        private static readonly string c_ProgramSettingsFileName = "settings.xml";
        public static string ProgramSettingsPath
        {
            get { return GetDirectoryFullPath(c_ProgramSettingsFileName); }
        }

        private static string GetDirectoryFullPath(string directoryName)
        {
            string exePath= System.Windows.Forms.Application.StartupPath;
            string fullPath = Path.Combine(exePath, directoryName);
            return fullPath;
        }
    }
}
