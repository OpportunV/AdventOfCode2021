using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Days
{
    public static class Day4
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");
        
        public static object Part1()
        {
            var lines = Helper.GetInput(_inputPath);

            var numbers = lines[0].Split(',').Select(int.Parse).ToArray();
            var boards = new List<Board>();

            for (int i = 2; i < lines.Length; i += 6)
            {
                var boardData = new int[5][];
                for (int j = 0; j < 5; j++)
                {
                    boardData[j] = lines[i + j].Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                }
                
                boards.Add(new Board(boardData));
            }

            foreach (var number in numbers)
            {
                foreach (var board in boards)
                {
                    board.AddNumber(number);
                    if (board.bingo)
                    {
                        return board.SumExceptNumbers * number;
                    }
                }
            }
            
            return -1;
        }
        
        public static object Part2()
        {
            var lines = Helper.GetInput(_inputPath);
            
            var numbers = lines[0].Split(',').Select(int.Parse).ToArray();
            var boards = new List<Board>();

            for (int i = 2; i < lines.Length; i += 6)
            {
                var boardData = new int[5][];
                for (int j = 0; j < 5; j++)
                {
                    boardData[j] = lines[i + j].Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                }
                
                boards.Add(new Board(boardData));
            }

            var bingoCounter = 0;
            foreach (var number in numbers)
            {
                foreach (var board in boards)
                {
                    if (board.bingo)
                    {
                        continue;
                    }
                    board.AddNumber(number);
                    if (board.bingo)
                    {
                        bingoCounter += 1;
                        if (bingoCounter == boards.Count)
                        {
                            return board.SumExceptNumbers * number;
                        }
                    }
                }
            }
            
            return -1;
        }

        private class Board
        {
            public int SumExceptNumbers
            {
                get
                {
                    var ans = _data.Sum(item => item.Sum());
                    ans -= _numbersSum;
                    return ans;
                }
            }

            public bool bingo;
            private readonly int[][] _data;
            private int _numbersSum;
            private readonly List<(int, int)> _numbersPos = new List<(int, int)>();

            public Board(int[][] data)
            {
                _data = data;
            }

            public void AddNumber(int number)
            {
                if (!_data.Any(item => item.Contains(number)))
                {
                    return;
                }
                _numbersSum += number;
                var pos = GetNumberPos(number);
                _numbersPos.Add(pos);
                UpdateBingo();
            }

            private (int, int) GetNumberPos(int number)
            {
                for (int i = 0; i < _data.Length; i++)
                {
                    for (int j = 0; j < _data[0].Length; j++)
                    {
                        if (_data[i][j] == number)
                        {
                            return (i, j);
                        }
                    }
                }

                throw new ArgumentOutOfRangeException();
            }

            private void UpdateBingo()
            {
                for (int i = 0; i < 5; i++)
                {
                    var rows = true;
                    var cols = true;
                    for (int j = 0; j < 5; j++)
                    {
                        if (!_numbersPos.Contains((i, j)))
                        {
                            rows = false;
                        }
                        
                        if (!_numbersPos.Contains((j, i)))
                        {
                            cols = false;
                        }
                    }

                    if (rows || cols)
                    {
                        bingo = true;
                        return;
                    }
                }
            }
        }
    }
}

