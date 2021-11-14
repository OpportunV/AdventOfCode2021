using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Helpers
{
    public static class Helper
    {
        public static readonly List<(int, int)> Directions = new List<(int, int)>
        {
            (-1, -1),
            (-1,  0),
            (-1,  1),
            ( 0, -1),
            ( 0,  1),
            ( 1, -1),
            ( 1,  0),
            ( 1,  1),
        };
        public static string[] GetInput(string path)
        {
            var tmp = File.ReadLines(Path.Combine(path));
            return tmp as string[] ?? tmp.ToArray();
        }
        
        public static IEnumerable<(int, int)> Adjacent((int, int) pos, int width, int height)
        {
            var (x, y) = pos;
            foreach (var (i, j) in Directions)
            {
                
                var curX = x + i;
                if (curX < 0 || height <= curX)
                {
                    continue;
                }
                
                var curY = y + j;
                if (curY < 0 || width <= curY)
                {
                    continue;
                }

                yield return (curX, curY);
            }
        }
        
        public static IEnumerable<(int, int)> Adjacent((int, int) pos)
        {
            var (x, y) = pos;
            foreach (var (i, j) in Directions)
            {
                var curX = x + i;
                var curY = y + j;
                yield return (curX, curY);
            }
        }
        
        public static IEnumerable<(int, int, int)> Adjacent3D((int, int, int) pos)
        {
            var (x, y, z) = pos;
            for (int dx = -1; dx <= 1; dx++)
            {
                var curX = x + dx;
                for (int dy = -1; dy <= 1; dy++)
                {
                    var curY = y + dy;
                    for (int dz = -1; dz <= 1; dz++)
                    {
                        var curZ = z + dz;
                        if (curX == x && curY == y && curZ == z)
                        {
                            continue;
                        }

                        yield return (curX, curY, curZ);
                    }
                }
            }
        }
    }
    
}