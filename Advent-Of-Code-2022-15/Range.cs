using System.Data.Common;

namespace AdventOfCode.Day15
{
    /// <summary>
    /// Helper for keeping and mering ranges
    /// </summary>
    public class Range : IEquatable<Range>
    {
        private long _point1;
        private long _point2;
        public long Point1 { get { return _point1; } set { _point1 = value; Normalize(); } }
        public long Point2 { get { return _point2; } set { _point2 = value; Normalize(); } }
        public long Length { get { return _point2 - _point1 + 1; } }

        public Range(long Point1, long Point2) 
        {
            this._point1 = Point1;
            this._point2 = Point2;
            Normalize();
        }
        
        public void Normalize()
        {
            if (this._point1 <= this._point2)
                return;

            long tempPoint = this._point1;
            this._point1 = this._point2;
            this._point2 = tempPoint;
        }

        public static bool ContainsPoint(Range range, int point)
        {
            return range.ContainsPoint(point);
        }

        public bool ContainsPoint(int point)
        {
            return (point >= _point1 && point <= _point2);
        }

        public static bool Intersects(Range range1, Range range2)
        {
            return range1.Intersects(range2);
        }

        public bool Intersects(Range range)
        {
            return (Point1 <= range.Point2 && Point2 >= range.Point1);
        }

        public static Range Merge(Range range1, Range range2)
        {
            return range1.Merge(range2);
        }

        public Range Merge(Range range)
        {
            if (!this.Intersects(range))
            {
                throw new Exception("Ranges do not intersect.");
            }

            long newPoint1 = Point1;
            long newPoint2 = Point2;
            if (range.Point1 < newPoint1) newPoint1 = range.Point1;
            if (range.Point2 > newPoint2) newPoint2 = range.Point2;
            return new Range(newPoint1, newPoint2);
        }

        public override string ToString()
        {
            return $"{{{Point1},{Point2}}}";
        }

        public static bool operator ==(Range left, Range right)
        {
            return (left._point1 == right._point1 && left._point2 == right._point2);
        }

        public static bool operator !=(Range left, Range right)
        {
            return (left._point1 != right._point1 || left._point2 != right._point2);
        }

        public bool Equals(Range? range) => Equals(range);

        public override bool Equals(object? obj)
        {
            return obj is Range range &&
                   _point1 == range._point1 &&
                   _point2 == range._point2;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_point1, _point2);
        }
    }
}