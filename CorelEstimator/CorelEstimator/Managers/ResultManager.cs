using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CorelEstimator.Entities;
using System.IO;
using CorelEstimator.Geometries;

namespace CorelEstimator.Managers
{
    public static class ResultManager
    {
        private static readonly string FormulaPattern = "=ROUND(2,035-0,983*D{0}+0,000407*E{0}+0,00006572*F{0}+0,0009691*G{0}+0,000419*H{0}-0,001348*I{0}+0,073*J{0}+0,011*K{0}+0,275*L{0}-0,63*M{0}+0,02*N{0}+0,02*O{0}-0,0001371*P{0}-0,017*Q{0}+0,00002715*R{0}-0,011*S{0}+0,307*T{0}; 2)";
        private static readonly string CsvSepearator = "\t";

        public static void SaveEstimations(List<Estimation> estimations, string resultPath)
        {
            var lines = new List<string>(estimations.Count + 1);
            lines.Add(GetHeader());
            int lineNumber = 2;
            foreach (Estimation estimation in estimations)
            {
                lines.Add(ToCsv(estimation, lineNumber));
                lineNumber++;
            }
            File.WriteAllLines(resultPath, lines, Encoding.Unicode);
        }

        private static string GetHeader()
        {
            return string.Format(
                "Fails{0}" +
                "Vērtējums{0}" +
                "Ieteicamais vērtējums{0}" +
                "Labots{0}" +
                "Teksta rindiņu skaits{0}Simbolu skaits{0}" +
                "Līnijas{0}Unikālas līnijas{0}" +
                "Taisnstūri{0}Unikāli taisnstūri{0}" +
                "Daudzstūri{0}"+
                "Apļi{0}Unikāli apļi{0}" +
                "Elipses{0}Unikālas elipses{0}" +
                "Punktu virknes{0}Līniju virknes{0}" +
                "Punktu skaits sarežģītos objektos{0}" +
                "Attēli{0}Unikāli attēli{0}" +
                "Faila pilns ceļš",
                CsvSepearator);
        }

        private static string ToCsv(Estimation estimation, int lineNumber)
        {
            var line = new StringBuilder();
            EstimationResult result = estimation.Result;
            line.Append(estimation.FileName);
            line.Append(CsvSepearator);
            line.Append(CsvSepearator);
            line.AppendFormat(FormulaPattern, lineNumber);
            line.Append(CsvSepearator);
            line.Append(estimation.IsUpdate);
            line.Append(CsvSepearator);
            line.AddCount(GeometryType.Text, result, CsvSepearator);
            line.Append(result.CharCount);
            line.Append(CsvSepearator);
            line.AddCounts(GeometryType.Line, result, CsvSepearator);
            line.AddCounts(GeometryType.Rectangle, result, CsvSepearator);
            line.AddCount(GeometryType.Polygon, result, CsvSepearator);
            line.AddCounts(GeometryType.Circle, result, CsvSepearator);
            line.AddCounts(GeometryType.Ellipse, result, CsvSepearator);            
            line.AddCount(GeometryType.Path, result, CsvSepearator);
            line.AddCount(GeometryType.PolyLine, result, CsvSepearator);            
            line.Append(result.ComplexObjectPointCount);
            line.Append(CsvSepearator);
            line.AddCounts(GeometryType.Image, result, CsvSepearator);
            line.Append(estimation.FilePath);
            return line.ToString();
        }

        public static void AddCounts(this StringBuilder builder, GeometryType type, EstimationResult result, string separator)
        {
            builder.Append(result.GetCount(type));
            builder.Append(separator);
            builder.Append(result.GetUniqueCount(type));
            builder.Append(separator);
        }

        public static void AddCount(this StringBuilder builder, GeometryType type, EstimationResult result, string separator)
        {
            builder.Append(result.GetCount(type));
            builder.Append(separator);
        }
    }
}
