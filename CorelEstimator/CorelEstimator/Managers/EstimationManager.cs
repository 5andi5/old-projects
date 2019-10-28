using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CorelEstimator.Entities;
using CorelEstimator.Geometries;

namespace CorelEstimator.Managers
{
    public static class EstimationManager
    {
        public static void Estimate(Estimation estimation)
        {
            EstimationResult result = new EstimationResult();
            foreach (GeometryType geometryType in Enum.GetValues(typeof(GeometryType)))
            {
                int totalCount, uniqueCount;
                Count(estimation.Geometries, geometryType, out totalCount, out uniqueCount);
                result.SetCount(geometryType, totalCount);
                result.SetUniqueCount(geometryType, uniqueCount);
            }
            result.CharCount = CountChars(estimation.Geometries);
            result.ComplexObjectPointCount = CountPoints(estimation.Geometries);

            estimation.Result = result;
        }

        private static void Count(List<IGeometry> geometries, GeometryType type, out int totalCount, out int uniqueCount)
        {
            IOrderedEnumerable<IGeometry> currentGeometries = geometries.Where(x => x.Type == type).OrderBy(x => x.Descriptor);
            totalCount = 0;
            uniqueCount = 0;
            string previousDescriptor = null;
            foreach (IGeometry geometry in currentGeometries)
            {
                totalCount++;
                if (previousDescriptor != geometry.Descriptor)
                {
                    uniqueCount++;
                }
                previousDescriptor = geometry.Descriptor;
            }
        }

        private static int CountChars(List<IGeometry> geometries)
        {
            return geometries
                .Where(x => x.Type == GeometryType.Text)
                .Cast<Text>()
                .Sum(x => x.CharCount);
        }

        private static int CountPoints(List<IGeometry> geometries)
        {
            int result = 0;
            result += geometries
                .Where(x => x.Type == GeometryType.Path)
                .Cast<Path>()
                .Sum(x => x.ElementCount);
            result += geometries
                .Where(x => x.Type == GeometryType.Polygon)
                .Cast<Polygon>()
                .Sum(x => x.PointCount);
            result += geometries
                .Where(x => x.Type == GeometryType.PolyLine)
                .Cast<PolyLine>()
                .Sum(x => x.PointCount);
            return result;
        }
    }
}
