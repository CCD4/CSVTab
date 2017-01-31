using System.Collections.Generic;
using System.Linq;

namespace CSV_Kata
{
    internal class CsvTab
    {
        public static IEnumerable<string> Tabelliere(IEnumerable<string> csvZeilen)
        {
            var records = Parse(csvZeilen);
            return Formatiere(records);
        }

        private static IEnumerable<string> Formatiere(string[][] records)
        {
            var headerLine = ExtractHeaderLine(records);
            var lines = ExtractLines(records);
            var maxColumnLengths = MaxLength(records);
            var result = FormatOutput(headerLine, lines, maxColumnLengths);
            return result;
        }

        private static IEnumerable<string> ExtractHeaderLine(IEnumerable<IEnumerable<string>> allLines)
        {
            return allLines.First();
        }

        private static IEnumerable<IEnumerable<string>> ExtractLines(IEnumerable<IEnumerable<string>> allLines)
        {
            return allLines.Skip(1);
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

        internal static IEnumerable<string> FormatOutput(IEnumerable<string> headerLine, IEnumerable<IEnumerable<string>> lines, int[] maxColumnLengths)
        {
            var result = new List<string>();
            result.AddRange(FormatHeader(headerLine, maxColumnLengths));
            result.AddRange(FormatLines(lines, maxColumnLengths));
            return result;
        }

        private static IEnumerable<string> FormatLines(IEnumerable<IEnumerable<string>> splitLines, int[] maxColumnLengths)
        {
            foreach (var line in splitLines)
            {
                var formatedLine = FormatLine(maxColumnLengths, line);
                yield return formatedLine;
            }
        }

        private static IEnumerable<string> FormatHeader(IEnumerable<string> headerLine, int[] maxColumnLengths)
        {
            yield return FormatLine(maxColumnLengths, headerLine);
            yield return FormatHeaderSeparator(maxColumnLengths);
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