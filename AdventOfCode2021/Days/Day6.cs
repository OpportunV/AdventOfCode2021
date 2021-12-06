using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Days
{
    public static class Day6
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");
        
        public static object Part1()
        {
            var lines = Helper.GetInput(_inputPath);
            var fish = lines[0].Split(',').Select(int.Parse).ToList();

            for (int i = 0; i < 80; i++)
            {
                var newFish = new List<int>();
                foreach (var single in fish)
                {
                    if (single == 0)
                    {
                        newFish.Add(6);
                        newFish.Add(8);
                    }
                    else
                    {
                        newFish.Add(single - 1);
                    }
                }

                fish = newFish;
            }
            
            return fish.Count;
        }
        
        public static object Part2()
        {
            var lines = Helper.GetInput(_inputPath);
            var fish = lines[0].Split(',').Select(int.Parse).ToList();

            var fishLong = new long[9];
            foreach (var single in fish)
            {
                fishLong[single] += 1;
            }
            
            for (int i = 0; i < 256; i++)
            {
                var newFish = new long[9];
                for (var j = 0; j < fishLong.Length; j++)
                {
                    var current = fishLong[j];
                    if (j == 0)
                    {
                        newFish[6] += current;
                        newFish[8] += current;
                    }
                    else
                    {
                        newFish[j - 1] += current;
                    }
                }

                fishLong = newFish;
            }
            
            return fishLong.Sum();
        }
    }
}

