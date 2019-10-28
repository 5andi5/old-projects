using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorelEstimator.Geometries
{
    public class Ellipse : IGeometry
    {
        public double X { get; set; }
        public double Y { get; set; }

        public double XRadius { get; set; }
        public double YRadius { get; set; }

        public GeometryType Type { get { return GeometryType.Ellipse; } }

        public string Descriptor
        {
            get
            {
                return XRadius.ToString() + ";" + YRadius.ToString();
            }
        }
    }
}
