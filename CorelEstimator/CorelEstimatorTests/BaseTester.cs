using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CorelEstimatorTests
{
    public abstract class BaseTester
    {
        private static readonly string TestDirectory = @"D:\SavsDarbs\CorelEstimator\TestsDir\";

        protected string SaveToDisk(string name, byte[] file)
        {
            Directory.Delete(TestDirectory, true);
            Directory.CreateDirectory(TestDirectory);
            string fullPath = Path.Combine(TestDirectory, name);
            File.WriteAllBytes(fullPath, file);
            return fullPath;
        }
    }
}
