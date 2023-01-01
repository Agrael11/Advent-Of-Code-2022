namespace AdventOfCode.Day15
{
    /// <summary>
    /// Sensor object
    /// </summary>
    public class Sensor
    {
        public Point Position { get; private set; }
        public Point Beacon { get; private set; }
        public long ManhattanDistance { get; private set; }
        private Range _rangeX;
        private Range _rangeY;
        public Sensor(Point position, Point beacon)
        {
            Position = position;
            Beacon = beacon;
            ManhattanDistance = Manhattan(position, beacon);
            _rangeX = new Range(position.X - ManhattanDistance, position.X + ManhattanDistance);
            _rangeY = new Range(position.Y - ManhattanDistance, position.Y + ManhattanDistance);
        }

        public bool IsOnLine(int line)
        {
            return _rangeY.ContainsPoint(line);
        }

        public Range GetRangeOnLine(int line)
        {
            if (!IsOnLine(line))
                throw new Exception("Target not on line");

            int diff = Math.Abs(Position.Y - line);
            return new Range(_rangeX.Point1 + diff, _rangeX.Point2 - diff);
        }

        private long Manhattan(Point point1, Point point2)
        {
            return Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
        }
    }
}