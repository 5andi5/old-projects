using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuessTheSong
{
    public class AudioPlayerSettings:ProcessSettingsBase
    {
        public static readonly string AudioFileNameMarker = "##audioName##";

        private string m_AudioFileName = string.Empty;

        public void SetAudioFileName(string fileName)
        {
            m_AudioFileName = fileName;
        }

        public override string GetParametersString()
        {
            Dictionary<string,string> dynamicParameterValues=new Dictionary<string,string>();
            dynamicParameterValues.Add(AudioFileNameMarker, m_AudioFileName);
            string result= base.FormatParameters(dynamicParameterValues);
            return result;
        }
    }
}
