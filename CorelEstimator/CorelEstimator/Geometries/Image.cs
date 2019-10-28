using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CorelEstimator.Geometries
{
    public class Image : IGeometry
    {
        public double X { get; set; }
        public double Y { get; set; }

        public double Width { get; set; }
        public double Height { get; set; }

        public GeometryType Type { get { return GeometryType.Image; } }

        public string Descriptor
        {
            get
            {
                //Izskatās, ka attēliem šie atribūti nemainās, ja bilde tiek 
                //pārvietota, samazināta, palielināta utt. Izmaiņas tiek pierakstītas
                //ar transformāciju matricas palīdzību (matrix atribūts).
                return string.Format("{0};{1};{2};{3}", X, Y, Width, Height);
            }
        }
    }
}
