using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorelEstimator.Geometries
{
    public class PolyLine : IGeometry
    {
        public string PointData { get; set; }

        public int PointCount
        {
            get
            {
                if (string.IsNullOrEmpty(PointData))
                {
                    return 0;
                }
                string[] points = PointData.Split(
                    new Char[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries);
                return points.Length;
            }
        }

        public GeometryType Type { get { return GeometryType.PolyLine; } }

        //Better don't bother with equality comparision.
        public string Descriptor
        {
            get
            {
                return PointData;
            }
        }
    }
}
