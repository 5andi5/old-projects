using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorelEstimator.Geometries
{
    public class Line:IGeometry
    {        
        public double StartX { get; set; }
        public double StartY { get; set; }

        public double EndX { get; set; }
        public double EndY { get; set; }

        public GeometryType Type { get { return GeometryType.Line; } }

        public string Descriptor
        {
            get
            {
                return (EndX - StartX).ToString() + ";" + (EndY - StartY).ToString();
            }
        }        
    }
}
