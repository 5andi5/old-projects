using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuessTheSong
{
    public class GameSettingsNotFoundException:Exception
    {
        public GameSettingsNotFoundException(string message) : base(message) { }
    }
}
