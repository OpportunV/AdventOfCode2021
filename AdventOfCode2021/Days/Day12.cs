using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Days
{
    public static class Day12
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");

        private static readonly Dictionary<string, List<string>> _caveMap = new Dictionary<string, List<string>>();

        public static object Part1()
        {
            var lines = Helper.GetInput(_inputPath);
            foreach (var line in lines)
            {
                var fromTo = line.Split('-');
                if (!_caveMap.ContainsKey(fromTo[0]))
                {
                    _caveMap[fromTo[0]] = new List<string>();
                }
                
                if (!_caveMap.ContainsKey(fromTo[1]))
                {
                    _caveMap[fromTo[1]] = new List<string>();
                }
                
                
                _caveMap[fromTo[0]].Add(fromTo[1]);
                _caveMap[fromTo[1]].Add(fromTo[0]);
            }

            var queue = new Queue<(string, HashSet<string>)>();
            queue.Enqueue(("start", new HashSet<string>(new[] {"start"})));

            var counter = 0;
            while (queue.Count > 0)
            {
                var (cur, visited) = queue.Dequeue();
                if (cur == "end")
                {
                    counter += 1;
                    continue;
                }

                foreach (var next in _caveMap[cur])
                {
                    if (!visited.Contains(next))
                    {
                        var nextVisited = new HashSet<string>(visited);
                        if (next.All(char.IsLower))
                        {
                            nextVisited.Add(next);
                        }
                        
                        queue.Enqueue((next, nextVisited));
                    }
                }
            }
            
            return counter;
        }
        
        public static object Part2()
        {
            var queue = new Queue<(string, HashSet<string>, bool)>();
            queue.Enqueue(("start", new HashSet<string>(new[] {"start"}), false));

            var counter = 0;
            while (queue.Count > 0)
            {
                var (cur, visited, doubleVisited) = queue.Dequeue();
                if (cur == "end")
                {
                    counter += 1;
                    continue;
                }

                foreach (var next in _caveMap[cur])
                {
                    if (!visited.Contains(next))
                    {
                        var nextVisited = new HashSet<string>(visited);
                        if (next.All(char.IsLower))
                        {
                            nextVisited.Add(next);
                        }
                        
                        queue.Enqueue((next, nextVisited, doubleVisited));
                    }
                    else if (!doubleVisited && next != "start")
                    {
                        queue.Enqueue((next, visited, true));
                    }
                }
            }
            
            return counter;
        }
    }
}

