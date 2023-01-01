using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Day14
{
    /// <summary>1
    /// Main Class for Challange 2
    /// </summary>
    public static class Challange2
    {
        private enum StepResult { Success, Failure, Neutral }

        private readonly static Point _sandSource = new(500, 0);
        private static int _floorPosition = int.MinValue;
        private readonly static HashSet<Point> _blockMap = new();
        private readonly static HashSet<Point> _sandMap = new();
        private static Point? _currentSand = null;
        private static Point _previousSafeSand = new(_sandSource.X, _sandSource.Y);

        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static int DoChallange(string input)
        {
            _blockMap.Clear();
            _sandMap.Clear();
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            //Parse and create map
            foreach (string line in inputData)
            {
                string[] splitLine = line.Split(" -> ");

                for (int pointIndex = 0; pointIndex < splitLine.Length - 1; pointIndex++)
                {
                    string[] point = splitLine[pointIndex].Split(',');
                    Point point1 = new Point(int.Parse(point[0]), int.Parse(point[1]));
                    point = splitLine[pointIndex + 1].Split(',');
                    Point point2 = new Point(int.Parse(point[0]), int.Parse(point[1]));
                    //Set lowest point
                    if (point1.Y > _floorPosition)
                        _floorPosition = point1.Y;
                    if (point2.Y > _floorPosition)
                        _floorPosition = point2.Y;
                    GeneratePoints(point1, point2);
                }
            }

            _floorPosition += 2;

            //While result is not failure does one step
            StepResult result;
            do
            {
                //If current sand is null, creates one at source
                if (_currentSand is null)
                {
                    _currentSand = new Point(_previousSafeSand.X, _previousSafeSand.Y);
                }

                //Does one step for current sand
                result = SandsStep();

                //if sand was placed, set current sand to null
                if (result == StepResult.Success)
                {
                    if (_currentSand == _previousSafeSand)
                        _previousSafeSand = new Point(_sandSource.X, _sandSource.Y);
                    _currentSand = null;
                }
            } while (result != StepResult.Failure);

            //Return number of steps taken = number of sands placed
            return _sandMap.Count;
        }

        /// <summary>
        /// One step in 
        /// </summary>
        /// <returns>Success, when block is placed on safe place,
        /// Failure if block cannot be placed or is placed in start location,
        /// Neutral if block is moved</returns>
        /// <exception cref="NullReferenceException"></exception>
        private static StepResult SandsStep()
        {
            if (_currentSand is null)
                throw new NullReferenceException();

            //There is never void

            //Try down
            if (!IsBlockOrSandOn(_currentSand.X, _currentSand.Y + 1))
            {
                _previousSafeSand = new Point(_currentSand.X, _currentSand.Y);
                _currentSand.Y = FindHighestSafePoint(_currentSand.X, _currentSand.Y);
                return StepResult.Neutral;
            }
            //Try left diagonal
            if (!IsBlockOrSandOn(_currentSand.X - 1, _currentSand.Y + 1))
            {
                _previousSafeSand = new Point(_currentSand.X, _currentSand.Y);
                _currentSand.Y += 1;
                _currentSand.X -= 1;
                return StepResult.Neutral;
            }
            //Try right diagonal
            if (!IsBlockOrSandOn(_currentSand.X + 1, _currentSand.Y + 1))
            {
                _previousSafeSand = new Point(_currentSand.X, _currentSand.Y);
                _currentSand.Y += 1;
                _currentSand.X += 1;
                return StepResult.Neutral;
            }
            //Place
            _sandMap.Add(_currentSand);
            if (_currentSand == _sandSource) return StepResult.Failure;
            return StepResult.Success;
        }

        /// <summary>
        /// Finds highest safe point from position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static int FindHighestSafePoint(int x, int y)
        {
            while (y < _floorPosition)
            {
                if (IsBlockOrSandOn(x, y))
                {
                    return y-1;
                }
                y++;
            }
            return _floorPosition-1;
        }

        /// <summary>
        /// Checks if block or sand is on position x or y. also returns true if y is floor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>True if block is on position, false if not</returns>
        private static bool IsBlockOrSandOn(int x, int y)
        {
            return y == _floorPosition || _blockMap.Contains(new Point(x, y)) || _sandMap.Contains(new Point(x, y));
        }

        /// <summary>
        /// Adds block to specific map, if not already in map
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="map"></param>
        private static void AddToMap(int x, int y, HashSet<Point> map)
        {
            if (!map.Contains(new Point(x, y)))
            {
                map.Add(new Point(x, y));
            }
        }

        /// <summary>
        /// Generates points between two points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="map"></param>
        private static void GeneratePoints(Point point1, Point point2)
        {
            if (point1.X == point2.X)
            {
                GeneratePointsVertical(point1, point2);
                return;
            }
            if (point1.Y == point2.Y)
            {
                GeneratePointsHorizontal(point1, point2);
                return;
            }
            throw new Exception("Diagonal line");
        }
        /// <summary>
        /// Generates points between two horizontal points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="map"></param>
        private static void GeneratePointsHorizontal(Point point1, Point point2)
        {
            if (point1.X > point2.X)
            {
                Point tempPoint = point1;
                point1 = point2;
                point2 = tempPoint;
            }

            for (int x = point1.X; x <= point2.X; x++)
            {
                AddToMap(x, point1.Y, _blockMap);
            }
        }
        /// <summary>
        /// Generates points between two vertical points
        /// </summary>
        /// <param name="point"></param>
        /// <param name="map"></param>
        private static void GeneratePointsVertical(Point point1, Point point2)
        {
            if (point1.Y > point2.Y)
            {
                Point tempPoint = point1;
                point1 = point2;
                point2 = tempPoint;
            }

            for (int y = point1.Y; y <= point2.Y; y++)
            {
                AddToMap(point1.X, y, _blockMap);
            }
        }
    }
}