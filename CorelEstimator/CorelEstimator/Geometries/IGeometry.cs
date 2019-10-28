using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CorelEstimator.Geometries
{
    public interface IGeometry
    {
        GeometryType Type { get; }
        string Descriptor { get; }
    }
}
