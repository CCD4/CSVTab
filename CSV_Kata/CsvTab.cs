using System.Collections.Generic;
using System.Linq;

namespace CSV_Kata
{
    internal class CsvTab
    {
        /// <summary>
        /// Teständerung 
        /// noch eine Teständerung
        /// </summary>
        /// <param name="CSV_zeilen"></param>
        /// <returns></returns>
        public static IEnumerable<string> Tabelliere(IEnumerable<string> CSV_zeilen)
        {
            var split = Split(CSV_zeilen);
            var maxColumnLengths = MaxLength(split); // test
            var result = FormatOutput(split, maxColumnLengths);
            return result;
        }

        internal static IEnumerable<IEnumerable<string>> Split(IEnumerable<string> CSV_zeilen)
        {
            return CSV_zeilen.Select(zeile => zeile.Split(';'));
        }

        internal static int[] MaxLength(IEnumerable<IEnumerable<string>> splitLines)
        {
            var columnCounts = splitLines.First().Count();
            var maxColumnLengths = new int[columnCounts];

            foreach (var line in splitLines)
            {
                for (var i = 0; i < columnCounts; i++)
                {
                    var columnLength = line.ElementAt(i).Length;
                    if (columnLength > maxColumnLengths[i])
                        maxColumnLengths[i] = columnLength;
                }
            }
            return maxColumnLengths;
        }

        internal static IEnumerable<string> FormatOutput(IEnumerable<IEnumerable<string>> splitLines,
            int[] maxColumnLengths)
        {
            var columnCounts = splitLines.First().Count();
            var result = new List<string>();

            
            foreach (var line in splitLines)
            {
                var formatedLine = string.Empty;
                for (var i = 0; i < columnCounts; i++)
                {
                    var column = line.ElementAt(i);
                    formatedLine += column.PadRight(maxColumnLengths[i]) + "|";
                }
                result.Add(formatedLine);
                if (result.Count == 1)
                    result.Add(FormatOutputHeader(maxColumnLengths));
            }
            return result;
        }

        internal static string FormatOutputHeader(int[] maxColumnLengths)
        {
            var result = string.Empty;
            for (var i = 0; i < maxColumnLengths.Length; i++)
            {
                result = result + string.Empty.PadRight(maxColumnLengths[i], '-') + '+';
            }
            return result;
        }
    }
}