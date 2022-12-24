using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Day09
{
    /// <summary>1
    /// Main Class for Challange 2
    /// </summary>
    public static class Challange2
    {
        //"Table" of moves, so I don't have to do it manually
        private static readonly Dictionary<Point, Point> _moveTable = new()
        {
            { new (-2, -2), new (1, 1)}, { new (-1, -2), new (1, 1)}, { new (0, -2), new (0, 1)}, {new (1, -2), new (-1, 1)}, { new (2, -2), new (-1, 1)},
            { new (-2, -1), new (1, 1)}, {new (2, -1), new (-1, 1)},
            { new (-2, 0), new (1, 0) }, {new (2, 0), new (-1, 0)},
            { new (-2, 1), new (1, -1)}, {new (2, 1), new (-1, -1)},
            { new (-2, 2), new (1, -1)}, { new (-1, 2), new (1, -1)}, { new (0, 2), new (0, -1)}, {new (1, 2), new (-1, -1)}, { new (2, 2), new (-1, -1)}
        };

        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static int DoChallange(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');
            //List of starting knots + head
            Point[] knots = { new(0, 0) , new(0, 0) , new(0, 0) , new(0, 0) , new(0, 0) , new(0, 0) , new(0, 0) , new(0, 0) , new(0, 0) };
            Point head = new(0, 0);
            //List of visited positions by last knot
            List<Point> visited = new()
            {
                knots[8]
            };

            //For each step of instruction move head, and knots accordingly
            foreach (string line in inputData)
            {
                string[] instructions = line.Split(' ');
                for (int i = 0; i < int.Parse(instructions[1]); i++)
                {
                    switch (instructions[0])
                    {
                        case "R": head.X += 1; break;
                        case "L": head.X -= 1; break;
                        case "U": head.Y -= 1; break;
                        case "D": head.Y += 1; break;
                    }
                    //This moves all the knots towards previous one
                    MoveTailByTable(head, ref knots[0]);
                    for (int knot = 1; knot < 8; knot++)
                    {
                        MoveTailByTable(knots[knot - 1], ref knots[knot]);
                    }
                    if (MoveTailByTable(knots[7], ref knots[8]) && !visited.Contains(knots[8]))
                    {
                        visited.Add(knots[8]);
                    }
                }
            }

            return visited.Count;
        }

        /// <summary>
        /// Moves "tail" towards "head" if needed.
        /// </summary>
        /// <param name="head"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public static bool MoveTailByTable(Point head, ref Point tail)
        {
            if (head == tail) return false;

            Point difference = tail - head;
            if (!_moveTable.ContainsKey(difference))
            {
                return false;
            }

            tail += _moveTable[difference];
            return true;
        }
    }
}