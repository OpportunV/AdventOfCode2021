using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Days
{
    public static class Day10
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");

        private static readonly Dictionary<char, char> _brackets = new Dictionary<char, char>
        {
            { '(', ')' },
            { '[', ']' },
            { '{', '}' },
            { '<', '>' }
        };
        
        private static readonly Dictionary<char, int> _scoresCorrupted = new Dictionary<char, int>
        {
            { ')', 3 },
            { ']', 57 },
            { '}', 1197 },
            { '>', 25137 }
        };
        
        private static readonly Dictionary<char, int> _scoresAutocomplete = new Dictionary<char, int>
        {
            { ')', 1 },
            { ']', 2 },
            { '}', 3 },
            { '>', 4 }
        };
        
        

        public static object Part1()
        {
            var lines = Helper.GetInput(_inputPath);
            

            return lines.Sum(CheckCorrupted);
        }
        
        public static object Part2()
        {
            var lines = Helper.GetInput(_inputPath);
            var incomplete = lines.Where(item => CheckCorrupted(item) == 0);
            var sorted = incomplete.Select(Autocomplete).OrderBy(item => item).ToArray();
            return sorted[sorted.Length / 2];
        }

        private static int CheckCorrupted(string line)
        {
            var stack = new Stack<char>();
            foreach (var chr in line)
            {
                if (_brackets.ContainsKey(chr))
                {
                    stack.Push(chr);
                }
                else
                {
                    if (_brackets[stack.Peek()] == chr)
                    {
                        stack.Pop();
                    }
                    else
                    {
                        return _scoresCorrupted[chr];
                    }
                }
            }
            
            return 0;
        }

        private static long Autocomplete(string line)
        {
            var stack = new Stack<char>();
            foreach (var chr in line)
            {
                if (_brackets.ContainsKey(chr))
                {
                    stack.Push(chr);
                }
                else
                {
                    if (_brackets[stack.Peek()] == chr)
                    {
                        stack.Pop();
                    }
                }
            }

            long ans = 0;
            while (stack.Count > 0)
            {
                ans *= 5;
                ans += _scoresAutocomplete[_brackets[stack.Pop()]];
            }

            return ans;
        }
    }
}

