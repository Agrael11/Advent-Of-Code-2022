using System;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Unicode;

namespace AdventOfCode.Day15
{
    /// <summary>1
    /// Main Class for Challange 2
    /// </summary>
    public static class Challange2
    {
        private static int frequencyMultiplier = 4000000;
        private static readonly HashSet<Sensor> _sensors = new();
        private static int _maxRange = -1;

        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static long DoChallange(string input, int maxRange = 4000000)
        {
            _sensors.Clear();

            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            _maxRange = maxRange;

            //Parses input into list of sensors
            foreach (string line in inputData)
            {
                string[] splitLine = line.Split(' ');
                int x1 = int.Parse(splitLine[2].Substring(2, splitLine[2].Length - 3));
                int y1 = int.Parse(splitLine[3].Substring(2, splitLine[3].Length - 3));
                int x2 = int.Parse(splitLine[8].Substring(2, splitLine[8].Length - 3));
                int y2 = int.Parse(splitLine[9].Substring(2));
                Point beacon = new Point(x2, y2);
                _sensors.Add(new Sensor(new Point(x1, y1), beacon));
            }

            //For each line in possible range, search for beacon
            for (int y = 0; y <= maxRange; y++)
            {
                long? result = SearchForPointAtLine(y);
                if (result is null)
                    continue;

                return result.Value;
            }

            return -1;
        }

        /// <summary>
        /// Searches for point without signal on y
        /// </summary>
        /// <param name="searchY">Line to search</param>
        /// <returns>null if no empty space on y, otherwise returns frequency of beacon</returns>
        /// <exception cref="Exception"></exception>
        public static long? SearchForPointAtLine(int searchY)
        {
            List<Range> ranges = new();

            //Get range at position y of all sensors and at it to list
            foreach (Sensor sensor in _sensors)
            {
                if (!sensor.IsOnLine(searchY))
                    continue;

                ranges.Add(sensor.GetRangeOnLine(searchY));
            }

            //Combine all possible ranges at that line
            while (TryCombine(ranges)) ;

            //Should not have more than two ranges - would mean two empty spaces
            if (ranges.Count > 2)
                throw new Exception("That's not right");

            long result = 0;

            //Finds empty space between ranges
            if (ranges.Count == 1)
            {
                if (ranges[0].Point1 <= 0 && ranges[0].Point2 >= _maxRange)
                    return null;

                if (ranges[0].Point1 > 0)
                {
                    result = ranges[0].Point1 - 1;
                }
                else
                {
                    result = ranges[0].Point2 + 1;
                }
            }
            else if (ranges[0].Point2 < ranges[0].Point1)
            {
                result = ranges[0].Point2 + 1;
            }
            else
            {
                result = ranges[1].Point2 + 1;
            }

            ranges.Clear();

            //Calculates frequency of beacon at empty space.
            return result * _maxRange + searchY;
        }

        /// <summary>
        /// Tries to combine two ranges in list
        /// </summary>
        /// <param name="ranges">False if no ranges possible to combine</param>
        /// <returns></returns>
        public static bool TryCombine(List<Range> ranges)
        {
            for (int i = 0; i < ranges.Count; i++)
            {
                for (int j = i + 1; j < ranges.Count; j++)
                {
                    if (ranges[i].Intersects(ranges[j]))
                    {
                        Range newRange = ranges[i].Merge(ranges[j]);
                        ranges.RemoveAt(j);
                        ranges.RemoveAt(i);
                        ranges.Add(newRange);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}