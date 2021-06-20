using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MoreLinq;

namespace HelloGithubClassroom
{
    public static class Program
    {
        public static int DivisibleByThreeOrFive(int start, int end)
        {
            return Enumerable.Range(start, end).Sum(x => x % 5 == 0 || x % 3 == 0 ? x : 0);
        }

        public static IEnumerable<KeyValuePair<string, int>> Top20Words(String book)
        {
            String StartOfFile = "C:\\Users\\Daniel Bashary\\source\\repos\\06-linq1-basharyd\\TXT\\";
            return File.ReadAllLines(StartOfFile + book + ".txt")
                .SkipUntil(x => x.ToUpper().Contains("START"))
                .TakeUntil(x => x.ToUpper().Contains("GUTENBERG"))
                .SelectMany(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries)) //todo check
                .GroupBy(w => w.Any(char.IsDigit) ? "" : Regex.Replace(w, @"[^a-zA-Z0-9 -]", "").ToLower())
                .Where(w => w.Key != "")
                .ToDictionary(g => g.Key, g => g.Count())
                .OrderByDescending(g => g.Value)
                .Take(20); ;
        }

        public static String[,] PercentageCreatorAndPrinter()
        {
            List<String> bookNames = new List<string>()
            {
                "A Tale of Two Cities", "Deuteronomy", "Exodus",
                "Genesis", "Hamlet", "Numbers", "Oliver Twist", "Romeo and Juliet", "Vayikra"
            };
            String[,] percentageCount = new String[11, 11];
            for (int i = 0; i < bookNames.Count; i++)
            {
                percentageCount[i + 1, 0] = bookNames[i];
                percentageCount[0, i + 1] = bookNames[i];
                HashSet<String> topWords = new HashSet<string>(Program.Top20Words(bookNames[i])
                    .Select(x => x.Key));
                for (int j = 0; j < bookNames.Count; j++)
                {
                    if (i != j)
                    {
                        HashSet<String> check = new HashSet<string>(topWords);
                        HashSet<String> topWords2 = new HashSet<string>(Program.Top20Words(bookNames[j])
                            .Select(x => x.Key));
                        check.UnionWith(topWords2);
                        double amount = Math.Round(((20.0 - (check.Count - 20.0)) / 20.0) * 100.0);
                        percentageCount[i + 1, j + 1] = amount + "";
                    }
                    else
                    {
                        percentageCount[i + 1, j + 1] = 100 + "";
                    }
                }
            }
            return percentageCount;
        }
    }
}
