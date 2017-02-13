using System.Collections.Generic;
using System.Linq;

namespace CSV_Kata
{
    internal class CsvTab
    {
        public static IEnumerable<string> Tabelliere(IEnumerable<string> csvZeilen)
        {
            string[][] records = Parse(csvZeilen);
            return Formatiere(records);
        }

        private static IEnumerable<string> Formatiere(string[][] records)
        {
            var maxColumnLengths = MaxLength(records);
            var lines = FormatLines(records, maxColumnLengths).ToList();
            var separator = FormatHeaderSeparator(maxColumnLengths);
            InsertSeparator(lines, separator);
            return lines;
        }

        private static void InsertSeparator(List<string> lines, string separator)
        {
            lines.Insert(1, separator);
        }

        internal static string[][] Parse(IEnumerable<string> csvZeilen)
        {
            return csvZeilen.Select(zeile => zeile.Split(';')).ToArray();
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

        private static IEnumerable<string> FormatLines(IEnumerable<IEnumerable<string>> splitLines, int[] maxColumnLengths)
        {
            foreach (var line in splitLines)
            {
                var formatedLine = FormatLine(maxColumnLengths, line);
                yield return formatedLine;
            }
        }

        internal static string FormatLine(int[] maxColumnLengths, IEnumerable<string> line)
        {
            var formatedLine = string.Empty;
            var columnCounts = line.Count();

            for (var i = 0; i < columnCounts; i++)
            {
                var column = line.ElementAt(i);
                formatedLine += column.PadRight(maxColumnLengths[i]) + "|";
            }
            return formatedLine;
        }

        internal static string FormatHeaderSeparator(int[] maxColumnLengths)
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