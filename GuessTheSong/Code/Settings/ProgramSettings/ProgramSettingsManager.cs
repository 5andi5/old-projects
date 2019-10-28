using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace GuessTheSong
{
    static class ProgramSettingsManager
    {
        public static ProgramSettings GetDefaultSettings() { 
            ProgramSettings settings=new ProgramSettings();
            settings.SetDefault();
            return settings;
        }

        public static ProgramSettings LoadSettings() {
            ProgramSettings settings;
            string settingsPath=DirectoryNames.ProgramSettingsPath;
            if (File.Exists(settingsPath))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ProgramSettings));
                    FileStream stream = new FileStream(settingsPath, FileMode.Open, FileAccess.Read);
                    settings=(ProgramSettings)serializer.Deserialize(stream);
                    stream.Close();
                }
                catch (Exception ex)
                {
                    string errorMessage = string.Format("Atgadījās kļūda, ielādējot programmas iestatījumus no '{0}': {1}",
                        settingsPath, ex.Message);
                    throw new Exception(errorMessage);
                }
            }
            else
            {
                settings = new ProgramSettings();
                settings.SetDefault();
            }
            return settings;
        }

        public static void SaveSettings(ProgramSettings settings)
        {
            string settingsPath = DirectoryNames.ProgramSettingsPath;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ProgramSettings));
                FileStream stream = new FileStream(settingsPath, FileMode.Create, FileAccess.Write);
                serializer.Serialize(stream, settings);
                stream.Close();
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("Atgadījās kļūda, saglabājot programmas iestatījumus : {0}", ex.Message);
                throw new Exception(errorMessage);
            }            
        }
    }
}
