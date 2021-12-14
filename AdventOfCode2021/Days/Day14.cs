using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Days
{
    public static class Day14
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");

        private static readonly Dictionary<string, string> _rules = new Dictionary<string, string>();

        public static object Part1()
        {
            var lines = Helper.GetInput(_inputPath);
            var polymer = lines[0];

            for (int i = 2; i < lines.Length; i++)
            {
                var split = lines[i].Split(new[] { " -> " }, StringSplitOptions.None);
                _rules[split[0]] = split[1];
            }

            for (int _ = 0; _ < 10; _++)
            {
                var newPolymer = new StringBuilder(polymer);
                var insertionAddition = 0;
                for (int i = 0; i < polymer.Length - 1; i++)
                {
                    var pair = $"{polymer[i]}{polymer[i + 1]}";
                    if (_rules.ContainsKey(pair))
                    {
                        newPolymer.Insert(i + 1 + insertionAddition, _rules[pair]);
                        insertionAddition += 1;
                    }
                }

                polymer = newPolymer.ToString();
            }

            var counter = new Dictionary<char, long>();
            foreach (var chr in polymer)
            {
                if (!counter.ContainsKey(chr))
                {
                    counter[chr] = 0;
                }

                counter[chr] += 1;
            }

            return counter.Max(item => item.Value) - counter.Min(item => item.Value);;
        }
        
        public static object Part2()
        {
            var lines = Helper.GetInput(_inputPath);
            var polymer = lines[0];
            
            var pairCounter = new Dictionary<string, long>();
            for (var i = 0; i < polymer.Length - 1; i++)
            {
                var pair = $"{polymer[i]}{polymer[i + 1]}";
                if (!pairCounter.ContainsKey(pair))
                {
                    pairCounter[pair] = 0;
                }

                pairCounter[pair] += 1;
            }
            
            for (int k = 0; k < 40; k++)
            {
                var newPairCounter = new Dictionary<string, long>();
                foreach (var keyValue in pairCounter)
                {
                    var pair = keyValue.Key;
                    var value = keyValue.Value;
                    var firstPair = $"{pair[0]}{_rules[pair]}";
                    var secondPair = $"{_rules[pair]}{pair[1]}";
                    
                    if (!newPairCounter.ContainsKey(firstPair))
                    {
                        newPairCounter[firstPair] = 0;
                    }
                    
                    if (!newPairCounter.ContainsKey(secondPair))
                    {
                        newPairCounter[secondPair] = 0;
                    }
                    
                    
                    newPairCounter[firstPair] += value;
                    newPairCounter[secondPair] += value;
                }

                pairCounter = newPairCounter;
            }

            var counter = new Dictionary<char, long> { { polymer[0], 1 } };
            foreach (var keyValue in pairCounter)
            {
                var key = keyValue.Key[1];
                var value = keyValue.Value;
                if (!counter.ContainsKey(key))
                {
                    counter[key] = 0;
                }

                counter[key] += value;
            }

            return counter.Max(item => item.Value) - counter.Min(item => item.Value);;
        }
    }
}

