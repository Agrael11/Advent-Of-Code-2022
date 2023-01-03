namespace AdventOfCode.Day16
{
    //Helper class for X,Y coordinates...
    public class Point : IEquatable<Point>
    {

        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object? @object) => Equals(@object as Point);

        public bool Equals(Point? point)
        {
            if (ReferenceEquals(this, point)) return true;

            if (point is null) return false;

            //I know what i'm doing lol, I always compare to other point, right?
            //if (point.GetType() != this.GetType()) return false;

            return X == point.X && Y == point.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        public static bool operator !=(Point left, Point right)
        {
            return left.X != right.X || left.Y != right.Y;
        }

        public static Point operator +(Point left, Point right)
        {
            return new Point(left.X + right.X, left.Y + right.Y);
        }

        public static Point operator -(Point left, Point right)
        {
            return new Point(left.X - right.X, left.Y - right.Y);
        }

        public override string ToString()
        {
            return $"{{{X}:{Y}}}";
        }
    }
}