using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreLinq;
using HelloGithubClassroom;

namespace LinqTest
{
    [TestClass]
    public class LinqTest
    {

        [TestMethod]
        public void PartOne()
        {
            Program.DivisibleByThreeOrFive(1, 9).Should().Be(23);
        }



        [TestMethod]
        public void PartTwo()
        {
            int testSum = 0;
            for (int i = 0; i < 1000; i++) { if (i % 5 == 0 || i % 3 == 0) { testSum += i; } }
            Program.DivisibleByThreeOrFive(1, 999).Should().Be(testSum);
        }

        [TestMethod]
        public void PartThreeTester()
        {
            String StartOfFile = "C:\\Users\\Daniel Bashary\\source\\repos\\06-linq1-basharyd\\TXT\\";
            List<String> bookNames = new List<string>() {"A Tale of Two Cities", "Deuteronomy", "Exodus", 
                "Genesis","Hamlet","Numbers","Oliver Twist", "Romeo and Juliet", "Vayikra"};
            var checker = File.ReadAllLines(StartOfFile + "Hamlet" + ".txt")
                .SkipUntil(x => x.ToUpper().Contains("START"))
                .TakeUntil(x => x.ToUpper().Contains("GUTENBERG"))
                .SelectMany(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries)) //todo check
                //.TakeUntil(line => !line.Equals("end"))
                .GroupBy(w => w.Any(char.IsDigit) ? "" : Regex.Replace(w, @"[^a-zA-Z0-9 -]", "").ToLower())
                .Where(w => w.Key != "")
                .ToDictionary(g => g.Key, g => g.Count())
                .OrderByDescending(g => g.Value)
                .Take(20);
            checker.Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(Console.WriteLine);
        }
        [TestMethod]
        public void PartText()
        {
            List<String> bookNames = new List<string>() {"A Tale of Two Cities", "Deuteronomy", "Exodus",
                "Genesis","Hamlet","Numbers","Oliver Twist", "Romeo and Juliet", "Vayikra"};
            foreach (var book in bookNames)
            {
                Console.WriteLine("BOOK      " + book);
                Console.WriteLine("--------------------------");
                Program.Top20Words(book).Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(Console.WriteLine);
            }
        }

        [TestMethod]
        public void PrintTableOfPercentages()
        {
            String[,] percentageCount = Program.PercentageCreatorAndPrinter();
            var rowCount = percentageCount.GetLength(0);
            var colCount = percentageCount.GetLength(1);
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                    Console.Write(String.Format("{0}\t", percentageCount[row, col]));
                Console.WriteLine();
            }
        }

    }
}
