using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Days
{
    public static class Day15
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");

        private static int[][] _map;
        private static int _width;
        private static int _height;
        
        public static object Part1()
        {
            var lines = Helper.GetInput(_inputPath);
            _map = lines
                .Select(item =>
                    item.Select(inner => int.Parse(inner.ToString()))
                        .ToArray())
                .ToArray();

            _width = _map[0].Length;
            _height = _map.Length;

            var costs = new Dictionary<(int, int), int> { { (0, 0), 0 } };
            var visited = new HashSet<(int, int)>();
            var queue = new Queue<(int, int, int)>();
            queue.Enqueue((0, 0, 0));
            while (queue.Count > 0)
            {
                queue = new Queue<(int, int, int)>(queue.OrderBy(item => item.Item3));
                var (x, y, cost) = queue.Dequeue();
                if (x < 0 || x >= _width || y < 0 || y >= _height)
                {
                    continue;
                }
                
                if (visited.Contains((x, y)))
                {
                    continue;
                }

                visited.Add((x, y));
                
                if (x == _width - 1 && y == _height - 1)
                {
                    break;
                }
                
                foreach (var (xx, yy) in Helper.SideAdjacent((x, y), _width, _height))
                {
                    if (visited.Contains((xx, yy)))
                    {
                        continue;
                    }
                    
                    var next = cost + _map[xx][yy];
                    if (next < costs.GetOrDefault((xx, yy), int.MaxValue))
                    {
                        costs[(xx, yy)] = next;
                        queue.Enqueue((xx, yy, next));
                    }
                }
            }
            
            return costs[(_width - 1, _height - 1)];
        }
        
        public static object Part2()
        {
            var width = 5 * _width;
            var height = 5 * _height;
            var costs = new Dictionary<(int, int), int> { { (0, 0), 0 } };
            var visited = new HashSet<(int, int)>();
            var queue = new Queue<(int, int, int)>();
            queue.Enqueue((0, 0, 0));
            while (queue.Count > 0)
            {
                queue = new Queue<(int, int, int)>(queue.OrderBy(item => item.Item3));
                var (x, y, cost) = queue.Dequeue();
                
                if (x < 0 || x >= width || y < 0 || y >= height)
                {
                    continue;
                }
                
                if (visited.Contains((x, y)))
                {
                    continue;
                }

                visited.Add((x, y));
                
                if (x == width - 1 && y == height - 1)
                {
                    break;
                }

                foreach (var (xx, yy) in Helper.SideAdjacent((x, y), width, height))
                {
                    if (visited.Contains((xx, yy)))
                    {
                        continue;
                    }

                    var nextCost = (_map[xx % _width][yy % _height] + xx / _width + yy / _height - 1) % 9 + 1;
                    var next = cost + nextCost;
                    if (next < costs.GetOrDefault((xx, yy), int.MaxValue))
                    {
                        costs[(xx, yy)] = next;
                        queue.Enqueue((xx, yy, next));
                    }
                }
            }
            
            return costs[(width - 1, height - 1)];
        }
    }
}

