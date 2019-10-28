using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuessTheSong
{
    public class WordInfo
    {
        public WordInfo(string word)
        {
            Word = word;
            IsBadWord = false;
            IsHidden = true;
        }

        public string Word;
        public bool IsBadWord;
        public bool IsHidden;
    }
}

