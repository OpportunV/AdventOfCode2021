using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Days
{
    public class Day3
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");
        
        public static object Part1()
        {
            var lines = Helper.GetInput(_inputPath);

            var counter = CalculateEntries(lines);

            var gammaRate = "";
            var epsilonRate = "";
            
            for (int i = 0; i < lines[0].Length; i++)
            {
                if (counter[i][0] > counter[i][1])
                {
                    gammaRate += "0";
                    epsilonRate += "1";
                }
                else
                {
                    gammaRate += "1";
                    epsilonRate += "0";
                }
            }

            return Convert.ToInt64(gammaRate, 2) * Convert.ToInt64(epsilonRate, 2);
        }
        
        public static object Part2()
        {
            var lines = Helper.GetInput(_inputPath);

            var oxygen = new List<string>(lines);
            var co2 = new List<string>(lines);
            var curInd = 0;
            
            while (oxygen.Count > 1 || co2.Count > 1)
            {
                Dictionary<int, List<int>> counter;
                if (oxygen.Count > 1)
                {
                    counter = CalculateEntries(oxygen);
                    oxygen = counter[curInd][0] > counter[curInd][1]
                        ? oxygen.Where(item => item[curInd] == '0').ToList()
                        : oxygen.Where(item => item[curInd] == '1').ToList();
                }

                if (co2.Count > 1)
                {
                    counter = CalculateEntries(co2);
                    co2 = counter[curInd][0] <= counter[curInd][1]
                        ? co2.Where(item => item[curInd] == '0').ToList()
                        : co2.Where(item => item[curInd] == '1').ToList();
                }

                curInd++;
            }
            
            return Convert.ToInt64(oxygen[0], 2) * Convert.ToInt64(co2[0], 2);
        }

        private static Dictionary<int, List<int>> CalculateEntries(IReadOnlyList<string> inp)
        {
            var counter = new Dictionary<int, List<int>>();
            for (int i = 0; i < inp[0].Length; i++)
            {
                counter[i] = new List<int> { 0, 0 };
            }

            foreach (var line in inp)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '1')
                    {
                        counter[i][1] += 1;
                    }
                    else
                    {
                        counter[i][0] += 1;
                    }
                }
            }

            return counter;
        }
    }
}