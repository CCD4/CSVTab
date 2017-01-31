using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CSV_Kata
{
    [TestFixture]
    public class CsvTabTest
    {
        [Test]
        public void TestFormatOutput()
        {
            string[] header = {"Name", "Strasse", "Ort", "Alter"};
            string[][] input =
            {
                new[] {"Peter Pan", "Am Hang 5", "12345 Einsam", "42"}
            };

            var expected = new List<string>
            {
                "Name     |Strasse  |Ort         |Alter|",
                "---------+---------+------------+-----+",
                "Peter Pan|Am Hang 5|12345 Einsam|42   |"
            };

            int[] coloumLenghts = { 9, 9, 12, 5 };
            var actual = CsvTab.FormatOutput(header, input, coloumLenghts);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestFormatHeaderSeparator()
        {
            string expected = @"---------+---------+------------+-----+";
            int[] maxColumnLengths = { 9, 9, 12, 5 };

            var actual = CsvTab.FormatHeaderSeparator(maxColumnLengths);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestMaxLength()
        {
            string[][] input =
            {
                new[] {"Name", "Strasse", "Ort", "Alter"},
                new[] {"Peter Pan", "Am Hang 5", "12345 Einsam", "42"}
            };

            var expected = CsvTab.MaxLength(input);
            int[] actual = { 9, 9, 12, 5 };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestParse()
        {
            string[] csvinput =
            {
                "Name;Strasse;Ort;Alter"
            };

            string[][] expected = { new[] { "Name", "Strasse", "Ort", "Alter" } };
            var actual = CsvTab.Parse(csvinput);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTabuliere()
        {
            string[] csvinput =
            {
                "Name;Strasse;Ort;Alter",
                "Peter Pan;Am Hang 5;12345 Einsam;42",
                "Maria Schmitz;Kölner Straße 45;50123 Köln;43",
                "Paul Meier;Münchener Weg 1;87654 München;65"
            };

            string[] expected =
            {
                "Name         |Strasse         |Ort          |Alter|",
                "-------------+----------------+-------------+-----+",
                "Peter Pan    |Am Hang 5       |12345 Einsam |42   |",
                "Maria Schmitz|Kölner Straße 45|50123 Köln   |43   |",
                "Paul Meier   |Münchener Weg 1 |87654 München|65   |"
            };

            var actual = CsvTab.Tabelliere(csvinput);
            foreach (var line in actual)
            {
                Console.WriteLine(line);
            }
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestTabuliere2()
        {
            string[] csvinput =
            {
               "Name;Strasse;Ort;Alter"
            };

            string[] expected =
            {
               "Name|Strasse|Ort|Alter|",
               "----+-------+---+-----+"
            };

            var actual = CsvTab.Tabelliere(csvinput);
            foreach (var line in actual)
            {
                Console.WriteLine(line);
            }
            Assert.AreEqual(expected, actual);
        }
    }
}