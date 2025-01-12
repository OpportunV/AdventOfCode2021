using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using AdventOfCode2021.Models.Day19;

namespace AdventOfCode2021.Days
{
    public static class Day19
    {
        private static readonly HashSet<Scanner> _scanners;

        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType?.Name}.txt");


        static Day19()
        {
            _scanners = File.ReadAllText(_inputPath)
                .Replace("\n\n", "|")
                .Split('|')
                .Select(scanner => scanner.Split('\n')
                    .Skip(1)
                    .Select(beacon => beacon.Split(',')
                        .Select(int.Parse)
                        .ToList())
                    .Select(vals => new Vector3(vals[0], vals[1], vals[2]))
                    .ToList())
                .Select(beacons => new Scanner(Vector3.Zero, 0, beacons))
                .ToHashSet();
        }

        public static object Part1()
        {
            return LocateScanners(_scanners.ToHashSet())
                .SelectMany(scanner => scanner.GetBeaconsWorld())
                .ToHashSet()
                .Count;
        }

        public static object Part2()
        {
            var scanners = LocateScanners(_scanners.ToHashSet()).ToList();
            var res = 0;
            for (var i = 0; i < scanners.Count; i++)
            {
                for (var j = i + 1; j < scanners.Count; j++)
                {
                    var cur = scanners[i].Pos - scanners[j].Pos;
                    res = Math.Max(res, (int)(Math.Abs(cur.X) + Math.Abs(cur.Y) + Math.Abs(cur.Z)));
                }
            }

            return res;
        }

        private static HashSet<Scanner> LocateScanners(HashSet<Scanner> scanners)
        {
            var seen = new HashSet<Scanner>();
            var toVisit = new Queue<Scanner>();

            seen.Add(scanners.First());
            toVisit.Enqueue(scanners.First());

            scanners.Remove(scanners.First());

            while (toVisit.Count > 0)
            {
                var scanner1 = toVisit.Dequeue();
                foreach (var scanner2 in scanners.ToArray())
                {
                    var next = TryToLocate(scanner1, scanner2);
                    if (next != null)
                    {
                        seen.Add(next);
                        toVisit.Enqueue(next);

                        scanners.Remove(scanner2);
                    }
                }
            }

            return seen;
        }

        private static Scanner TryToLocate(Scanner scanner1, Scanner scanner2)
        {
            var beacons1 = scanner1.GetBeaconsWorld().ToArray();

            foreach (var (beacon1, beacon2) in PossibleBeacons(scanner1, scanner2))
            {
                var rotated2 = scanner2;
                for (var rotation = 0; rotation < 24; rotation++)
                {
                    rotated2 = rotated2.Rotate();
                    var beacon2Rotated = rotated2.Transform(beacon2);

                    var located2 = rotated2.Translate(beacon1 - beacon2Rotated);

                    if (located2.GetBeaconsWorld().Intersect(beacons1).Count() >= 12)
                    {
                        return located2;
                    }
                }
            }

            return null;
        }

        private static IEnumerable<(Vector3 beacon1, Vector3 beacon2)> PossibleBeacons(Scanner scanner1,
            Scanner scanner2)
        {
            foreach (var beacon1 in scanner1.GetBeaconsWorld())
            {
                var coords1 = scanner1.Translate(-beacon1).AbsCoordsWorld().ToHashSet();

                foreach (var beacon2 in scanner2.GetBeaconsWorld())
                {
                    var cords2 = scanner2.Translate(-beacon2).AbsCoordsWorld();

                    if (cords2.Count(d => coords1.Contains(d)) >= 3 * 12)
                    {
                        yield return (beacon1, beacon2);
                    }
                }
            }
        }
    }
}