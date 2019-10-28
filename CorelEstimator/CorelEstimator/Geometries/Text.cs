using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorelEstimator.Geometries
{
    public class Text : IGeometry
    {
        public double X { get; set; }
        public double Y { get; set; }

        public string TextData { get; set; }

        public GeometryType Type { get { return GeometryType.Text; } }

        public string Descriptor
        {
            get
            {
                return TextData;
            }
        }

        public int CharCount
        {
            get
            {
                if (string.IsNullOrEmpty(TextData))
                {
                    return 0;
                }
                else
                {
                    return TextData.Length;
                }
            }
        }
    }
}
