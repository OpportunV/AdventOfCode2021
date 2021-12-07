using System;
using System.IO;
using System.Linq;
using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Days
{
    public static class Day7
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");
        
        public static object Part1()
        {
            var lines = Helper.GetInput(_inputPath);
            var nums = lines[0].Split(',').Select(long.Parse).ToArray();
            
            Array.Sort(nums);
            var median = nums[nums.Length / 2];
            return nums.Sum(item => Math.Abs(item - median));
        }
        
        public static object Part2()
        {
            var lines = Helper.GetInput(_inputPath);
            var nums = lines[0].Split(',').Select(long.Parse).ToArray();

            var mean = (long) nums.Average();
            return nums.Sum(item => GetMovingCost(item, mean));
        }

        private static long GetMovingCost(long current, long target)
        {
            var value = Math.Abs(current - target);
            return value * (value + 1) / 2;
        }
    }
}

