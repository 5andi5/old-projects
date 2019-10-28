using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GuessTheSong
{
    
    public class ImageRoundSettings:RoundSettingsBase
    {
        public static new readonly string DefaultRoundName = "Attēlu virknēšana";

        private string[] m_ImageNames;
        public string[] ImageNames
        {
            get { return m_ImageNames; }
            set { m_ImageNames = value; }
        }


        public ImageRoundSettings(string[] imageNames, string settingsFilePath, string imagesDirectory)
        {
            if(imageNames==null||imageNames.Length==0){
                throw new InvalidGameSettingsException("Nav norādīts neviens attēls", settingsFilePath);
            }

            m_ImageNames= GenerateFullPaths(imageNames, imagesDirectory);

            ValidateImageExistance(m_ImageNames, settingsFilePath);
        }

        private string[] GenerateFullPaths(string[] imageNames, string imagesDirectory)
        {
            string[] fullPathsList= new string[imageNames.Length];
            for (int index = 0; index < imageNames.Length; index++)
            {
                string imageFullPath = Path.Combine(imagesDirectory, imageNames[index]);
                fullPathsList[index] = imageFullPath;
            }
            return fullPathsList;
        }

        private static void ValidateImageExistance(string[] imagePaths, string settingsFilePath)
        {
            foreach (string path in imagePaths)
            {
                if (!File.Exists(path))
                {
                    throw new InvalidGameSettingsException(string.Format("Attēls '{0}' neeksistē", path), settingsFilePath);
                }
            }
        }
    }
}
