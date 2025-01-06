using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2021.Helpers;

namespace AdventOfCode2021.Days
{
    public static class Day16
    {
        private static readonly string _inputPath = Path.Combine("input",
            $"{System.Reflection.MethodBase.GetCurrentMethod().DeclaringType?.Name}.txt");

        private static int _versionSums;
        
        public static object Part1()
        {
            var lines = Helper.GetInput(_inputPath);

            string binary = string.Join(string.Empty,
                lines[0].Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

            ParsePacket(binary, 0);
            return _versionSums;
        }
        
        public static object Part2()
        {
            var lines = Helper.GetInput(_inputPath);

            string binary = string.Join(string.Empty,
                lines[0].Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

            var (_, value) = ParsePacket(binary, 0);
            return value;
        }

        private static (int index, long value) ParsePacket(string packet, int curInd)
        {
            var versionNumber = Convert.ToInt32(packet.Substring(curInd, 3), 2);
            _versionSums += versionNumber;
            
            var type = Convert.ToInt32(packet.Substring(curInd + 3, 3), 2);
            if (type == 4)
            {
                curInd += 6;
                long value = 0;
                while (true)
                {
                    value = value * 16 + Convert.ToInt64(packet.Substring(curInd + 1, 4), 2);
                    curInd += 5;
                    if (packet[curInd - 5] == '0')
                    {
                        return (curInd, value);
                    }
                }
            }
            
            var lengthType = Convert.ToInt32(packet[curInd + 6].ToString(), 2);
            var values = new List<long>();
            if (lengthType == 0)
            {
                var length = Convert.ToInt32(packet.Substring(curInd + 7, 15), 2);
                curInd += 7 + 15;
                var startInd = curInd;
                
                while (true)
                {
                    long value;
                    (curInd, value) = ParsePacket(packet, curInd);
                    values.Add(value);
                    if (curInd - startInd == length)
                    {
                        break;
                    }
                }
            }
            else
            {
                var nPackets = Convert.ToInt32(packet.Substring(curInd + 7, 11), 2);
                curInd += 7 + 11;
                for (int i = 0; i < nPackets; i++)
                {
                    long value;
                    (curInd, value) = ParsePacket(packet, curInd);
                    values.Add(value);
                }
            }

            switch (type)
            {
                case 0:
                    return (curInd, values.Sum());
                case 1:
                    return (curInd, values.Aggregate((cur, total) => cur * total));
                case 2:
                    return (curInd, values.Min());
                case 3:
                    return (curInd, values.Max());
                case 5:
                    return (curInd, values[0] > values[1] ? 1 : 0);
                case 6:
                    return (curInd, values[0] < values[1] ? 1 : 0);
                case 7:
                    return (curInd, values[0] == values[1] ? 1 : 0);
                default:
                    throw new ArgumentException($"Wrong type {type}");
            }
        }
    }
}

