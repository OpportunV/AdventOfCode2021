using System;
using System.IO;
using System.Text.RegularExpressions;
using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Days
{
    public static class Day17
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");
        
        public static object Part1()
        {
            var lines = Helper.GetInput(_inputPath);
            var matches = Regex.Matches(lines[0], @"-?\d+");
            var x0 = int.Parse(matches[0].Value);
            var x1 = int.Parse(matches[1].Value);
            var y0 = int.Parse(matches[2].Value);
            var y1 = int.Parse(matches[3].Value);

            var ans = 0;
            for (int vx = 0; vx < 150; vx++)
            {
                for (int vy = 0; vy < 500; vy++)
                {
                    var maxY = 0;
                    var x = 0;
                    var y = 0;
                    var curVx = vx;
                    var curVy = vy;
                    var isHit = false;

                    for (int i = 0; i < 1000; i++)
                    {
                        x += curVx;
                        y += curVy;
                        maxY = Math.Max(maxY, y);
                        if (curVx > 0)
                        {
                            curVx -= 1;
                        }
                        else if (curVx < 0)
                        {
                            curVx += 1;
                        }

                        curVy -= 1;

                        if (x0 <= x && x <= x1 && y0 <= y && y <= y1)
                        {
                            isHit = true;
                        }
                    }

                    if (isHit)
                    {
                        ans = Math.Max(ans, maxY);
                    }
                }
            }

            return ans;
        }
        
        public static object Part2()
        {
            var lines = Helper.GetInput(_inputPath);
            var matches = Regex.Matches(lines[0], @"-?\d+");
            var x0 = int.Parse(matches[0].Value);
            var x1 = int.Parse(matches[1].Value);
            var y0 = int.Parse(matches[2].Value);
            var y1 = int.Parse(matches[3].Value);

            var counter = 0;
            for (int vx = 0; vx < 100; vx++)
            {
                for (int vy = -500; vy < 500; vy++)
                {
                    var maxY = 0;
                    var x = 0;
                    var y = 0;
                    var curVx = vx;
                    var curVy = vy;
                    var isHit = false;

                    while (y >= y0 && x <= x1)
                    {
                        x += curVx;
                        y += curVy;
                        maxY = Math.Max(maxY, y);
                        if (curVx > 0)
                        {
                            curVx -= 1;
                        }
                        else if (curVx < 0)
                        {
                            curVx += 1;
                        }

                        curVy -= 1;

                        if (x0 <= x && x <= x1 && y0 <= y && y <= y1)
                        {
                            isHit = true;
                        }
                    }

                    if (isHit)
                    {
                        counter++;
                    }
                }
            }

            return counter;
        }
    }
}

