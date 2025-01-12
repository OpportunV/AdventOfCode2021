using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Days
{
    public static class Day21
    {
        private static readonly Dictionary<(int, int, int, int), (long, long)> _cache = new();
        private const int FieldSize = 10;
        private const int DiceSize = 100;
        private const int TargetScore = 1000;
        private const int TargetScorePart2 = 21;

        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType?.Name}.txt");

        public static object Part1()
        {
            int[] scores = [0, 0];
            var diceRolls = 0;
            var playerInd = 0;
            var pos = GetStartPos();

            while (true)
            {
                var moves = 0;
                for (var i = 0; i < 3; i++)
                {
                    diceRolls++;
                    moves += Mod(diceRolls, DiceSize);
                }

                var score = Mod(pos[playerInd] + moves, FieldSize);

                pos[playerInd] = score;
                scores[playerInd] += score;
                if (scores[playerInd] >= TargetScore)
                {
                    break;
                }

                playerInd = (playerInd + 1) % 2;
            }

            return scores.Min() * diceRolls;
        }

        public static object Part2()
        {
            var pos = GetStartPos();

            var (t0, t1) = CalculateUniverses(pos[0], 0, pos[1], 0);
            return Math.Max(t0, t1);
        }

        private static (long t0, long t1) CalculateUniverses(int p0, int s0, int p1, int s1)
        {
            if (_cache.ContainsKey((p0, s0, p1, s1)))
            {
                return _cache[(p0, s0, p1, s1)];
            }

            long t0 = 0, t1 = 0;
            foreach (var outcome in GetOutcomes())
            {
                var p0New = Mod(outcome.Sum() + p0, FieldSize);
                var s0New = s0 + p0New;
                if (s0New >= TargetScorePart2)
                {
                    t0++;
                }
                else
                {
                    var (t1New, t0New) = CalculateUniverses(p1, s1, p0New, s0New);
                    t0 += t0New;
                    t1 += t1New;
                }
            }

            _cache[(p0, s0, p1, s1)] = (t0, t1);
            return (t0, t1);
        }

        private static int Mod(int val, int mod)
        {
            return val % mod == 0 ? mod : val % mod;
        }

        private static IEnumerable<int[]> GetOutcomes()
        {
            for (var i = 1; i <= 3; i++)
            {
                for (var j = 1; j <= 3; j++)
                {
                    for (var k = 1; k <= 3; k++)
                    {
                        yield return [i, j, k];
                    }
                }
            }
        }

        private static int[] GetStartPos()
        {
            return Helper.GetInput(_inputPath)
                .Select(line => int.Parse(line.Split(':')[1]))
                .ToArray();
        }
    }
}