using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CorelEstimator.Entities;
using System.Text.RegularExpressions;

namespace CorelEstimator.Managers
{
    public static class PageManager
    {
        public static List<Estimation> MergePages(List<Estimation> estimations)
        {
            Dictionary<string, List<Estimation>> uniqueEstimations = GroupSingleFilePages(estimations);
            List<Estimation> result = MergeSingleFilePages(uniqueEstimations);
            return result;
        }

        private static Dictionary<string, List<Estimation>> GroupSingleFilePages(List<Estimation> estimations)
        {
            var uniqueEstimations = new Dictionary<string, List<Estimation>>();
            foreach (Estimation estimation in estimations)
            {
                Match multiPagePostfix = Regex.Match(estimation.FilePath, @"-[\d]+\.svg$");
                if (multiPagePostfix.Success)
                {
                    string postfix = multiPagePostfix.Value;
                    string newPath = estimation.FilePath.Substring(0, estimation.FilePath.Length - postfix.Length);
                    newPath += ".svg";
                    if (uniqueEstimations.ContainsKey(newPath))
                    {
                        uniqueEstimations[newPath].Add(estimation);
                    }
                    else
                    {
                        uniqueEstimations.Add(newPath, new List<Estimation> { estimation });
                    }
                }
                else
                {
                    uniqueEstimations.Add(estimation.FilePath, new List<Estimation> { estimation });
                }
            }
            return uniqueEstimations;
        }

        private static List<Estimation> MergeSingleFilePages(Dictionary<string, List<Estimation>> uniqueEstimations)
        {
            var result = new List<Estimation>(uniqueEstimations.Count);
            foreach (KeyValuePair<string, List<Estimation>> item in uniqueEstimations)
            {
                Estimation wholeFileEstim = item.Value.First();
                wholeFileEstim.FilePath = item.Key;
                wholeFileEstim.PageCount = 1;
                for (int index = 1; index < item.Value.Count; index++)
                {
                    wholeFileEstim.PageCount++;
                    Estimation page = item.Value[index];
                    wholeFileEstim.Geometries.AddRange(page.Geometries);
                }
                result.Add(wholeFileEstim);
            }
            return result;
        }
    }
}
