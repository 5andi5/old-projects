using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuessTheSong
{
    public abstract class ProcessSettingsBase
    {
        private string m_ProcessName;
        public string ProcessName
        {
            get
            {
                return m_ProcessName;
            }
            set
            {
                m_ProcessName = value;
            }
        }

        public string m_ParametersPattern;
        public string ParametersPattern
        {
            get
            {
                return m_ParametersPattern;
            }
            set
            {
                m_ParametersPattern = value;
            }
        }

        protected string FormatParameters(Dictionary<string, string> markersWithValues)
        {
            string parameters=m_ParametersPattern;
            foreach(KeyValuePair<string, string> value in markersWithValues){
                parameters = parameters.Replace(value.Key, value.Value);
            }
            return parameters;
        }

        public abstract string GetParametersString();
    }
}
