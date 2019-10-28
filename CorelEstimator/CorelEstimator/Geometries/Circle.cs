using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorelEstimator.Geometries
{
    public class Circle : IGeometry
    {
        public double X { get; set; }
        public double Y { get; set; }

        public double Radius { get; set; }

        public GeometryType Type { get { return GeometryType.Circle; } }

        public string Descriptor
        {
            get
            {
                return Radius.ToString();
            }
        }
    }
}
