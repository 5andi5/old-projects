using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CorelEstimator.Geometries;

namespace CorelEstimator.Entities
{
    public class Estimation
    {
        public string FilePath { get; set; }

        public int PageCount { get; set; }

        public List<IGeometry> Geometries { get; set; }

        public EstimationResult Result { get; set; }

        public string FileName
        {
            get
            {
                if (string.IsNullOrEmpty(FilePath))
                {
                    return "";
                }
                else
                {
                    return System.IO.Path.GetFileName(FilePath);
                }
            }
        }

        public bool IsUpdate
        {
            get
            {
                if (string.IsNullOrEmpty(FilePath))
                {
                    return true;
                }
                else
                {
                    return FilePath.ToLower().Contains("labot");
                }
            }
        }

        public Estimation(string filePath)
        {
            FilePath = filePath;
            PageCount = 1;
            Geometries = new List<IGeometry>();
            Result = null;
        }

        public Estimation()
        {
            FilePath = null;
            PageCount = 1;
            Geometries = new List<IGeometry>();
            Result = null;
        }

        public override string ToString()
        {
            return FileName;
        }
    }
}
