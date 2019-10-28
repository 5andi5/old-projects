using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuessTheSong
{
    public class InvalidGameSettingsException:Exception
    {
        public InvalidGameSettingsException(string message, string filePath):
            base(string.Format("Nekorekta spēles konfigurācija: \r\n{0}\r\n\r\nKonfigurācijas fails: '{1}'",
                message, filePath))
        { }
    }
}
