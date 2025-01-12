using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace AdventOfCode2021.Models.Day19;

public record Scanner
{
    public Vector3 Pos { get; set; }

    public int Rot { get; set; }

    public List<Vector3> Beacons { get; set; }

    public Scanner(Vector3 pos, int rot, List<Vector3> beacons)
    {
        Pos = pos;
        Rot = rot;
        Beacons = beacons;
    }

    public Scanner Rotate()
    {
        return new Scanner(Pos, Rot + 1, Beacons);
    }

    public Scanner Translate(Vector3 diff)
    {
        return new Scanner(Pos + diff, Rot, Beacons);
    }

    public Vector3 Transform(Vector3 coord)
    {
        var diff = coord;

        diff = (Rot % 6) switch
        {
            0 => new Vector3(diff.X, diff.Y, diff.Z),
            1 => new Vector3(-diff.X, diff.Y, -diff.Z),
            2 => new Vector3(diff.Y, -diff.X, diff.Z),
            3 => new Vector3(-diff.Y, diff.X, diff.Z),
            4 => new Vector3(diff.Z, diff.Y, -diff.X),
            5 => new Vector3(-diff.Z, diff.Y, diff.X),
            _ => new Vector3(diff.X, diff.Y, diff.Z)
        };

        diff = (Rot / 6 % 4) switch
        {
            0 => new Vector3(diff.X, diff.Y, diff.Z),
            1 => new Vector3(diff.X, -diff.Z, diff.Y),
            2 => new Vector3(diff.X, -diff.Y, -diff.Z),
            3 => new Vector3(diff.X, diff.Z, -diff.Y),
            _ => new Vector3(diff.X, diff.Y, diff.Z)
        };

        return Pos + diff;
    }

    public IEnumerable<Vector3> GetBeaconsWorld()
    {
        return Beacons.Select(Transform);
    }

    public IEnumerable<int> AbsCoordsWorld()
    {
        return GetBeaconsWorld()
            .SelectMany(coord => new[] { coord.X, coord.Y, coord.Z }, (_, v) => (int)Math.Abs(v));
    }
}