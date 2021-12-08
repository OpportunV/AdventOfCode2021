using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Days
{
    public static class Day8
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");

        private static readonly Dictionary<string, int> _numbers = new Dictionary<string, int>
        {
            { "abcefg", 0 },
            { "cf", 1 },
            { "acdeg", 2 },
            { "acdfg", 3 },
            { "bcdf", 4 },
            { "abdfg", 5 },
            { "abdefg", 6 },
            { "acf", 7 },
            { "abcdefg", 8 },
            { "abcdfg", 9 },
        };

        public static object Part1()
        {
            var lines = Helper.GetInput(_inputPath);
            var ans = lines.Select(line => line.Split(new[] { " | " }, StringSplitOptions.None)[1]
                    .Split(' '))
                .Select(output => output.Count(item =>
                    item.Length == 2 || item.Length == 3 || item.Length == 4 || item.Length == 7)).Sum();

            return ans;
        }
        
        public static object Part2()
        {
            var lines = Helper.GetInput(_inputPath);
            var ans = 0;
            foreach (var line in lines)
            {
                var split = line.Split(new[] { " | " }, StringSplitOptions.None);
                var left = split[0].Split(' ');
                var output = split[1].Split(' ');
                ans += GetOutput(left, output);
            }
            return ans;
        }

        private static int GetOutput(string[] left, string[] output)
        {
            var connections = new Dictionary<string, string>();
            var one = new List<char>();
            var bd = new List<char>();
            foreach (var num in left)
            {
                if (num.Length == 2)
                {
                    one = num.ToList();
                    break;
                }
            }

            foreach (var num in left)
            {
                if (num.Length == 3)
                {
                    var a = num.ToHashSet().Except(one).First().ToString();
                    connections[a] = "a";
                }
            }

            foreach (var num in left)
            {
                if (num.Length == 6)
                {
                    if (num.Count(item => one.Contains(item)) == 1)
                    {
                        var first = one[0];
                        var second = one[1];
                        if (num.Contains(first))
                        {
                            connections[first.ToString()] = "f";
                            connections[second.ToString()] = "c";
                        }
                        else
                        {
                            connections[first.ToString()] = "c";
                            connections[second.ToString()] = "f";
                        }
                    }
                }
            }

            foreach (var num in left)
            {
                if (num.Length == 4)
                {
                    bd = num.Except(one).ToList();
                }
            }
            
            foreach (var num in left)
            {
                if (num.Length == 6)
                {
                    var hasFirst = num.Contains(bd[0]);
                    var hasSecond = num.Contains(bd[1]);
                    if (hasFirst != hasSecond)
                    {
                        if (hasFirst)
                        {
                            connections[bd[0].ToString()] = "b";
                            connections[bd[1].ToString()] = "d";
                        }
                        else
                        {
                            connections[bd[1].ToString()] = "b";
                            connections[bd[0].ToString()] = "d";
                        }
                    }
                }
            }

            var last = new[] {"a", "b", "c", "d", "e", "f", "g"}.Except(connections.Keys).ToList();
            
            foreach (var num in left)
            {
                if (num.Length == 6)
                {
                    var hasFirst = num.Contains(last[0]);
                    var hasSecond = num.Contains(last[1]);
                    if (hasFirst != hasSecond)
                    {
                        if (hasFirst)
                        {
                            connections[last[0]] = "g";
                            connections[last[1]] = "e";
                        }
                        else
                        {
                            connections[last[1]] = "g";
                            connections[last[0]] = "e";
                        }
                    }
                }
            }

            var res = "";
            foreach (var num in output)
            {
                var trueNum = string.Join("", num.Select(item => connections[item.ToString()])).Sorted();
                res += _numbers[trueNum];
            }

            return int.Parse(res);
        }
    }
}

