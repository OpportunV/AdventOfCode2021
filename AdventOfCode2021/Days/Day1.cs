using System.IO;
using System.Linq;
using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Days
{
    public static class Day1
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");
        
        public static object Part1()
        {
            var lines = Helper.GetInput(_inputPath);

            var numbers = lines.Select(int.Parse).ToArray();

            var ans = 0;
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (numbers[i + 1] - numbers[i] > 0)
                {
                    ans++;
                }
            }
            
            return ans;
        }
        
        public static object Part2()
        {
            var lines = Helper.GetInput(_inputPath);
            
            var numbers = lines.Select(int.Parse).ToArray();

            var ans = 0;
            for (int i = 0; i < numbers.Length - 3; i++)
            {
                if (numbers[i + 3] > numbers[i])
                {
                    ans++;
                }
            }
            
            return ans;
        }
    }
}