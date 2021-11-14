using System.IO;
using System.Threading;
using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Days
{
    public static class Day1
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");
        
        public static object Part1()
        {
            string[] lines;
            try
            {
                lines = Helper.GetInput(_inputPath);
            }
            catch (FileNotFoundException)
            {
                return -1;
            }
            
            return -1;
        }
        
        public static object Part2()
        {
            string[] lines;
            try
            {
                lines = Helper.GetInput(_inputPath);
            }
            catch (FileNotFoundException)
            {
                return -1;
            }
            
            return -1;
        }
    }
}