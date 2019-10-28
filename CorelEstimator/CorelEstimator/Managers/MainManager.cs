using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using CorelEstimator.Entities;
using CorelEstimator.Exceptions;

namespace CorelEstimator.Managers
{
    internal static class MainManager
    {
        private static List<Estimation> _estimations = new List<Estimation>();
        private static string _resultPath = null;

        internal static ReadOnlyCollection<Estimation> Estimations
        {
            get { return _estimations.AsReadOnly(); }
        }      

        internal static void AddFilesToEstimate(string[] paths)
        {
            _estimations = paths
                .AsEnumerable()                
                .Concat(_estimations.Select(x => x.FilePath))
                .Distinct()
                .Select(x => new Estimation(x))
                .Where(x => !x.FileName.StartsWith("Backup_of_"))
                .OrderBy(x => x.FileName).ToList();
        }

        internal static void RemoveFileToEstimate(int itemIndex)
        {
            _estimations.RemoveAt(itemIndex);
        }

        internal static void RemoveAllFiles()
        {
            _estimations.Clear();
        }

        internal static bool HaveFilesToEstimate()
        {
            return _estimations.Count > 0;
        }

        internal static bool HaveResultPath()
        {
            return !string.IsNullOrEmpty(_resultPath);
        }

        internal static void SetResultPath(string resultPath)
        {
            _resultPath = resultPath;
        }

        internal static void Estimate()
        {
            foreach (Estimation estimation in _estimations)
            {
                try
                {
                    InfoReadingManager.ReadSvcInfo(estimation);
                }
                catch(Exception ex)
                {
                    string message = String.Format("Kļūda, nolasot datni '{0}':\r\n{1}", estimation.FilePath, ex.ToString());
                    throw new GenericException(message);
                }
            }
            _estimations = PageManager.MergePages(_estimations);
            foreach (Estimation estimation in _estimations)
            {
                EstimationManager.Estimate(estimation);
            }
        }

        internal static void SaveEstimations(string resultPath)
        {
            ResultManager.SaveEstimations(_estimations, resultPath);
        }
    }
}
