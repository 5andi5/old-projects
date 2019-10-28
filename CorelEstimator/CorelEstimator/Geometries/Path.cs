using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorelEstimator.Geometries
{
    public class Path : IGeometry
    {
        public string PathData { get; set; }
        
        //Property should not be accessed many times,
        //therefore calculation on access should not affect performance.
        public int ElementCount
        {
            get
            {
                if (string.IsNullOrEmpty(PathData))
                {
                    return 0;
                }
                string[] elements = PathData.Split(new char[]{
                'm', 'z', 'l', 'h', 'v', 'c', 's', 'q', 't', 'a',
                'M', 'Z', 'L', 'H', 'V', 'C', 'S', 'Q', 'T', 'A'},
                    StringSplitOptions.RemoveEmptyEntries);
                return elements.Length;
            }
        }

        public GeometryType Type { get { return GeometryType.Path; } }

        //Path looks too dificult to analyze (see http://www.w3.org/TR/SVG/paths.html#PathData)
        //and looks like there is not a great probability of multiple equal paths.
        public string Descriptor
        {
            get
            {
                return PathData;
            }
        }       
    }
}
