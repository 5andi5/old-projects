using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CorelEstimator.Geometries;

namespace CorelEstimator.Entities
{
    public class EstimationResult
    {
        private Dictionary<GeometryType, int> _counts;
        private Dictionary<GeometryType, int> _uniqueCounts;

        public int CharCount { get; set; }
        public int ComplexObjectPointCount { get; set; }

        public EstimationResult()
        {
            _counts = new Dictionary<GeometryType, int>();
            _uniqueCounts = new Dictionary<GeometryType, int>();
            CharCount = 0;
            ComplexObjectPointCount = 0;
        }

        public int GetCount(GeometryType geometryType)
        {
            return _counts[geometryType];
        }

        public void SetCount(GeometryType geometryType, int value)
        {
            if (_counts.ContainsKey(geometryType))
            {
                _counts[geometryType] = value;
            }
            else
            {
                _counts.Add(geometryType, value);
            }
        }

        public int GetUniqueCount(GeometryType geometryType)
        {
            return _uniqueCounts[geometryType];
        }

        public void SetUniqueCount(GeometryType geometryType, int value)
        {
            if (_uniqueCounts.ContainsKey(geometryType))
            {
                _uniqueCounts[geometryType] = value;
            }
            else
            {
                _uniqueCounts.Add(geometryType, value);
            }
        }
    }
}
