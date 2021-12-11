using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Days
{
    public static class Day11
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");

        private static long _flashCounter;
        private static int _width;
        private static int _height;
        private static readonly HashSet<(int, int)> _flashedThisTurn = new HashSet<(int, int)>();

        public static object Part1()
        {
            var lines = Helper.GetInput(_inputPath);
            _width = lines[0].Length;
            _height = lines.Length;
            var octopuses = new int[_height][];
            for (var i = 0; i < lines.Length; i++)
            {
                octopuses[i] = lines[i].Select(item => int.Parse(item.ToString())).ToArray();
            }

            for (int i = 0; i < 100; i++)
            {
                octopuses = Simulate(octopuses);
            }
            
            return _flashCounter;
        }
        
        public static object Part2()
        {
            var lines = Helper.GetInput(_inputPath);
            var octopuses = new int[_height][];
            for (var i = 0; i < lines.Length; i++)
            {
                octopuses[i] = lines[i].Select(item => int.Parse(item.ToString())).ToArray();
            }

            var step = 0;
            while (true)
            {
                _flashCounter = 0;
                octopuses = Simulate(octopuses);
                step += 1;
                if (_flashCounter == 100)
                {
                    return step;
                }
            }

        }

        private static int[][] Simulate(int[][] octopuses)
        {
            var next = new int[_height][];
            for (int i = 0; i < _height; i++)
            {
                next[i] = new int[_width];
                for (int j = 0; j < _width; j++)
                {
                    next[i][j] = octopuses[i][j] + 1;
                }
            }
            
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    if (next[i][j] > 9)
                    {
                        DoFlashes(next, (i, j));
                    }
                }
            }
            
            _flashedThisTurn.Clear();
            return next;
        }

        private static void DoFlashes(int[][] octopuses, (int, int) pos)
        {
            var (i, j) = pos;
            if (_flashedThisTurn.Contains((i, j)))
            {
                return;
            }
            _flashedThisTurn.Add((i, j));
            _flashCounter += 1;
            octopuses[i][j] = 0;

            foreach (var (x, y) in Helper.Adjacent((i, j), _width, _height))
            {
                if (_flashedThisTurn.Contains((x, y)))
                {
                    continue;
                }
                octopuses[x][y] += 1;
                if (octopuses[x][y] > 9)
                {
                    DoFlashes(octopuses, (x, y));
                }
            }
            
        }
    }
}

