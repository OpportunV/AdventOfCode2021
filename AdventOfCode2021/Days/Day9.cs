using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Days
{
    public static class Day9
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");
        
        private static readonly HashSet<(int, int)> _seen = new HashSet<(int, int)>();

        private static int _width;
        private static int _height;
        private static string[] _lines;
        
        public static object Part1()
        {
            _lines = Helper.GetInput(_inputPath);

            _height = _lines.Length;
            _width = _lines[0].Length;

            var ans = 0;
            for (int i = 0; i < _height; i++)
            {
                var curLine = _lines[i];
                for (int j = 0; j < _width; j++)
                {
                    var cur = curLine[j];
                    if (Helper.SideAdjacent((i, j), _width, _height).All(item => _lines[item.Item1][item.Item2] > cur))
                    {
                        ans += int.Parse(cur.ToString()) + 1;
                    }
                }
            }

            return ans;
        }
        
        public static object Part2()
        {
            var basinSizes = new List<int>();
            for (int i = 0; i < _height; i++)
            {
                var curLine = _lines[i];
                for (int j = 0; j < _width; j++)
                {
                    if (_seen.Contains((i, j)))
                    {
                        continue;
                    }
                    var cur = curLine[j];
                    if (cur == '9')
                    {
                        continue;
                    }

                    var curBasin = new HashSet<(int, int)>();
                    var queue = new Queue<(int, int)>();
                    queue.Enqueue((i, j));
                    while (queue.Count > 0)
                    {
                        var (x, y) = queue.Dequeue();
                        if (_seen.Contains((x, y)))
                        {
                            continue;
                        }

                        _seen.Add((x, y));
                        curBasin.Add((x, y));
                        foreach (var (xx, yy) in Helper.SideAdjacent((x, y), _width, _height))
                        {
                            if (_lines[xx][yy] != '9')
                            {
                                queue.Enqueue((xx, yy));
                            }
                        }
                    }

                    basinSizes.Add(curBasin.Count);
                }
            }

            return basinSizes.OrderBy(item => -item).Take(3).Aggregate((item, total) => item * total);
        }
    }
}

