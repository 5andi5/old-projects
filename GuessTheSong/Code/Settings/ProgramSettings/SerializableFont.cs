using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GuessTheSong
{
    [Serializable]
    public class SerializableFont
    {
        public string FontFamily { get; set; }
        public float Size { get; set; }
        public FontStyle FontStyle { get; set; }
        public GraphicsUnit Unit { get; set; }
        public byte CharSet { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        public Font Font
        {
            get
            {
                Font result = new Font(FontFamily, Size, FontStyle, Unit, CharSet);
                return result;
            }
            set
            {
                FontFamily = value.FontFamily.Name;
                Size = value.Size;
                FontStyle = value.Style;
                Unit = value.Unit;
                CharSet = value.GdiCharSet;
            }
        }

        public SerializableFont()
        {            
        }
    }
}
