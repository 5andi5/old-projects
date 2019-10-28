using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorelEstimator.Geometries
{
    public class Rectangle : IGeometry
    {
        public double StartX { get; set; }
        public double StartY { get; set; }

        public double Width { get; set; }
        public double Height { get; set; }

        public GeometryType Type { get { return GeometryType.Rectangle; } }

        public string Descriptor
        {
            get
            {
                return Width.ToString() + ";" + Height.ToString();
            }
        }
    }
}
