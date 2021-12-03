using System.IO;
using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Days
{
    public class Day2
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");
        
        public static object Part1()
        {
            var lines = Helper.GetInput(_inputPath);

            long xPos = 0;
            long depth = 0;

            foreach (var line in lines)
            {
                var tmp = line.Split(' ');
                var cmd = tmp[0];
                var value = long.Parse(tmp[1]);

                switch (cmd)
                {
                    case "forward":
                        xPos += value;
                        break;
                    case "down":
                        depth += value;
                        break;
                    case "up":
                        depth -= value;
                        break;
                    
                }
            }
            
            return xPos * depth;
        }
        
        public static object Part2()
        {
            var lines = Helper.GetInput(_inputPath);
            
            long xPos = 0;
            long depth = 0;
            long aim = 0;

            foreach (var line in lines)
            {
                var tmp = line.Split(' ');
                var cmd = tmp[0];
                var value = long.Parse(tmp[1]);

                switch (cmd)
                {
                    case "forward":
                        xPos += value;
                        depth += aim * value;
                        break;
                    case "down":
                        aim += value;
                        break;
                    case "up":
                        aim -= value;
                        break;
                    
                }
            }
            
            return xPos * depth;
        }
    }
}