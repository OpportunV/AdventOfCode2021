using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Days;

public static class Day20
{
    private static readonly string _algo;
    private static readonly Dictionary<(int row, int col), char> _image = new();

    private static readonly string _inputPath = Path.Combine("input",
        $"{System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType?.Name}.txt");

    static Day20()
    {
        var split = File.ReadAllText(_inputPath)
            .Replace("\n\n", "|")
            .Split('|');

        _algo = split[0];
        var image = split[1].Split('\n').Select(line => line.ToCharArray()).ToArray();
        for (var row = 0; row < image.Length; row++)
        {
            for (var col = 0; col < image[0].Length; col++)
            {
                _image[(row, col)] = image[row][col];
            }
        }
    }

    public static object Part1()
    {
        return Simulate(2);
    }

    public static object Part2()
    {
        return Simulate(50);
    }

    private static int Simulate(int n)
    {
        var image = new Dictionary<(int row, int col), char>(_image);
        for (var i = 0; i < n; i++)
        {
            image = Enhance(image, GetDefault(i));
        }

        return image.Values.Count(val => val == '#');
    }

    private static char GetDefault(int i)
    {
        return _algo[0] == '#' && _algo[_algo.Length - 1] == '.' && i % 2 == 1 ? '1' : '0';
    }

    private static Dictionary<(int row, int col), char> Enhance(Dictionary<(int row, int col), char> image,
        char def)
    {
        var minRow = image.Keys.Min(k => k.row) - 1;
        var minCol = image.Keys.Min(k => k.col) - 1;
        var maxRow = image.Keys.Max(k => k.row) + 1;
        var maxCol = image.Keys.Max(k => k.col) + 1;
        var newImage = new Dictionary<(int row, int col), char>();
        for (var row = minRow; row <= maxRow; row++)
        {
            for (var col = minCol; col <= maxCol; col++)
            {
                var index = GetIndex(row, col, image, def);
                newImage[(row, col)] = _algo[index];
            }
        }

        return newImage;
    }

    private static int GetIndex(int row, int col, Dictionary<(int row, int col), char> image, char def)
    {
        var indStr = new StringBuilder();
        for (var i = -1; i <= 1; i++)
        {
            for (var j = -1; j <= 1; j++)
            {
                if (image.ContainsKey((row + i, col + j)))
                {
                    indStr.Append(image[(row + i, col + j)] == '.' ? '0' : '1');
                }
                else
                {
                    indStr.Append(def);
                }
            }
        }

        return Convert.ToInt32(indStr.ToString(), 2);
    }
}