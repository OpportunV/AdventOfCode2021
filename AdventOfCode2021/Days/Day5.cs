using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Days
{
    public static class Day5
    {
        private static readonly Dictionary<(int, int), int> _map = new Dictionary<(int, int), int>();
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");
        
        public static object Part1()
        {
            var lines = Helper.GetInput(_inputPath);

            foreach (var line in lines)
            {
                var matches = Regex.Matches(line, @"\d+");
                var x1 = int.Parse(matches[0].Value);
                var y1 = int.Parse(matches[1].Value);
                var x2 = int.Parse(matches[2].Value);
                var y2 = int.Parse(matches[3].Value);
                
                (x1, x2) = (Math.Min(x1, x2), Math.Max(x1, x2));
                (y1, y2) = (Math.Min(y1, y2), Math.Max(y1, y2));

                if (x1 == x2)
                {
                    for (int y = y1; y <= y2; y++)
                    {
                        if (!_map.ContainsKey((x1, y)))
                        {
                            _map[(x1, y)] = 0;
                        }
                        
                        _map[(x1, y)]++;
                    }
                }
                
                if (y1 == y2)
                {
                    for (int x = x1; x <= x2; x++)
                    {
                        if (!_map.ContainsKey((x, y1)))
                        {
                            _map[(x, y1)] = 0;
                        }
                        
                        _map[(x, y1)]++;
                    }
                }
            }

            return _map.Count(item => item.Value > 1);
        }
        
        public static object Part2()
        {
            var lines = Helper.GetInput(_inputPath);

            _map.Clear();
            foreach (var line in lines)
            {
                var matches = Regex.Matches(line, @"\d+");
                var x1 = int.Parse(matches[0].Value);
                var y1 = int.Parse(matches[1].Value);
                var x2 = int.Parse(matches[2].Value);
                var y2 = int.Parse(matches[3].Value);
                
                if (x1 == x2)
                {
                    (y1, y2) = (Math.Min(y1, y2), Math.Max(y1, y2));
                    for (int y = y1; y <= y2; y++)
                    {
                        if (!_map.ContainsKey((x1, y)))
                        {
                            _map[(x1, y)] = 0;
                        }
                        
                        _map[(x1, y)]++;
                    }
                } 
                else if (y1 == y2)
                {
                    (x1, x2) = (Math.Min(x1, x2), Math.Max(x1, x2));
                    for (int x = x1; x <= x2; x++)
                    {
                        if (!_map.ContainsKey((x, y1)))
                        {
                            _map[(x, y1)] = 0;
                        }
                        
                        _map[(x, y1)]++;
                    }
                }
                else
                {
                    var stepX = x1 < x2 ? 1 : -1;
                    var stepY = y1 < y2 ? 1 : -1;
                    
                    while (x1 != x2 + stepX && y1 != y2 + stepY)
                    {
                        if (!_map.ContainsKey((x1, y1)))
                        {
                            _map[(x1, y1)] = 0;
                        }
                        
                        _map[(x1, y1)]++;
                        x1 += stepX;
                        y1 += stepY;
                    }
                }
            }

            return _map.Count(item => item.Value > 1);
        }
    }
}

