using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Day15
{
    /// <summary>1
    /// Main Class for Challange 1
    /// </summary>
    public static class Challange1
    {
        private static readonly HashSet<Sensor> _sensors = new();
        private static readonly HashSet<Point> _beacons = new();
        private static readonly List<Range> _ranges = new();


        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static long DoChallange(string input, int searchY = 2000000)
        {
            _sensors.Clear();
            _beacons.Clear();
            _ranges.Clear();

            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            //Parse input data into list of sensors and beacons
            foreach (string line in inputData)
            {
                string[] splitLine = line.Split(' ');
                int x1 = int.Parse(splitLine[2].Substring(2, splitLine[2].Length - 3));
                int y1 = int.Parse(splitLine[3].Substring(2, splitLine[3].Length - 3));
                int x2 = int.Parse(splitLine[8].Substring(2, splitLine[8].Length - 3));
                int y2 = int.Parse(splitLine[9].Substring(2));
                Point beacon = new Point(x2, y2);
                _sensors.Add(new Sensor(new Point(x1, y1), beacon));
                if (_beacons.Contains(beacon))
                    continue;

                _beacons.Add(beacon);
            }

            //for each sensor that is on y position, add range of it's signal range on y to list of rnges
            foreach (Sensor sensor in _sensors)
            {
                if (!sensor.IsOnLine(searchY))
                    continue;

                _ranges.Add(sensor.GetRangeOnLine(searchY));
            }

            //Combine all possible ranges
            while (TryCombine()) ;

            long length = 0;

            //Remove beacons from ranges and add their lengths
            foreach (Range range in _ranges)
            {
                length += range.Length;
                foreach (Point beacon in _beacons)
                {
                    if (beacon.Y == searchY && range.ContainsPoint(beacon.X)) length--;
                }
            }

            return length;
        }

        /// <summary>
        /// Tries to combine two ranges in list.
        /// </summary>
        /// <returns>false if no ranges possible to combine</returns>
        public static bool TryCombine()
        {
            for (int i = 0; i < _ranges.Count; i++)
            {
                for (int j = i+1; j < _ranges.Count; j++)
                {
                    if (_ranges[i].Intersects(_ranges[j]))
                    {
                        Range newRange = _ranges[i].Merge(_ranges[j]);
                        _ranges.RemoveAt(j);
                        _ranges.RemoveAt(i);
                        _ranges.Add(newRange);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}