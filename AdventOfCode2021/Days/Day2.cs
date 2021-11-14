using System.IO;
using System.Linq;
using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Days
{
    public class Day2
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

        private static void ParseInput(string input, out int min, out int max, out char letter, out string password)
        {
            var splits = input.Split(' ');
            var tmp = splits[0].Split('-');
            (min, max) = (int.Parse(tmp[0]), int.Parse(tmp[1]));
            letter = splits[1].TrimEnd(':').ToCharArray()[0];
            password = splits[2];
        }
    }
}