using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Days
{
    public static class Day13
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");

        private static readonly List<(int, int)> _coordinates = new List<(int, int)>();
        private static readonly List<(string, int)> _flips = new List<(string, int)>();
        private static readonly Dictionary<(int, int), bool> _paper = new Dictionary<(int, int), bool>();

        public static object Part1()
        {
            var lines = Helper.GetInput(_inputPath);
            var flips = false;
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    flips = true;
                    continue;
                }

                if (flips)
                {
                    var split = line.Split();
                    var flip = split[2].Split('=');
                    _flips.Add((flip[0], int.Parse(flip[1])));
                }
                else
                {
                    var split = line.Split(',');
                    _coordinates.Add((int.Parse(split[0]), int.Parse(split[1])));
                }
            }

            foreach (var (x, y) in _coordinates)
            {
                _paper[(x, y)] = true;
            }


            var (axis, value) = _flips.First();
            var coords = _paper.Keys.ToList();
            if (axis == "x")
            {
                foreach (var (x, y) in coords)
                {
                    if (x > value)
                    {
                        _paper.Remove((x, y));
                        _paper[(2 * value - x, y)] = true;
                    }
                }
            }
            else
            {
                foreach (var (x, y) in coords)
                {
                    if (y > value)
                    {
                        _paper.Remove((x, y));
                        _paper[(x, 2 * value - y)] = true;
                    }
                }
            }
            
            return _paper.Count;
        }
        
        public static object Part2()
        {
            foreach (var (x, y) in _coordinates)
            {
                _paper[(x, y)] = true;
            }


            foreach (var (axis, value) in _flips)
            {
                var coords = _paper.Keys.ToList();
                if (axis == "x")
                {
                    foreach (var (x, y) in coords)
                    {
                        if (x > value)
                        {
                            _paper.Remove((x, y));
                            _paper[(2 * value - x, y)] = true;
                        }
                    }
                }
                else
                {
                    foreach (var (x, y) in coords)
                    {
                        if (y > value)
                        {
                            _paper.Remove((x, y));
                            _paper[(x, 2 * value - y)] = true;
                        }
                    }
                }
            }

            var xMax = _paper.Keys.Max(item => item.Item1);
            var yMax = _paper.Keys.Max(item => item.Item2);

            var ans = "";
            for (int y = 0; y <= yMax; y++)
            {
                for (int x = 0; x <= xMax; x++)
                {
                    ans += _paper.GetOrDefault((x, y), false)
                        ? "#"
                        : " ";
                }

                ans += "\n";
            }
            
            Console.WriteLine(ans);

            return ans;
        }
    }
}

