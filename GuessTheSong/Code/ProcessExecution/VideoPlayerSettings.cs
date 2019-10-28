using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuessTheSong
{
    public class VideoPlayerSettings:ProcessSettingsBase
    {
        public static readonly string VideoFileNameMarker = "##videoName##";

        private string m_VideoFileName = string.Empty;

        public void SetVideoFileName(string fileName)
        {
            m_VideoFileName = fileName;
        }

        public override string GetParametersString()
        {
            Dictionary<string,string> dynamicParameterValues=new Dictionary<string,string>();
            dynamicParameterValues.Add(VideoFileNameMarker, m_VideoFileName);
            string result= base.FormatParameters(dynamicParameterValues);
            return result;
        }
    }
}
