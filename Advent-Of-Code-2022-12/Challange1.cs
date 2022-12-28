using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Day12
{
    /// <summary>1
    /// Main Class for Challange 1
    /// </summary>
    public static class Challange1
    {
        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static int DoChallange(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            int width = inputData[0].Length;
            int height = inputData.Length;

            //Build the heightmap and get start and end positions
            int[,] heightMap = new int[width, height];
            (int value, double prize, bool visited)[,] valueMap = new (int value, double prize, bool visited)[width, height];
            (int x, int y) startPosition = (-1, -1);
            (int x, int y) endPosition = (-1, -1);

            for (int y= 0; y < height; y++)
            {
                for (int x =0; x < width; x++)
                {
                    char c = inputData[y][x];
                    if (c == 'S')
                    {
                        c = 'a';
                        startPosition = (x, y);
                    }
                    if (c == 'E')
                    {
                        c = 'z';
                        endPosition = (x, y);
                    }
                    heightMap[x, y] = c - 'a';
                    valueMap[x, y] = (int.MaxValue, double.MaxValue, false);
                }
            }

            //Set default values
            valueMap[startPosition.x, startPosition.y] = (0, GetPrize(startPosition, endPosition), true);
            (int x, int y) currentPosition = startPosition;
            List<(int x, int y)> toExplore = new() { currentPosition };
            List<(int x, int y)> easyLoop = new() { (-1, 0), (1, 0), (0, -1), (0, 1) };

            //Just A* it
            while (currentPosition != endPosition)
            {
                double minPrize = double.MaxValue;
                foreach (var explore in toExplore)
                {
                    if (valueMap[explore.x, explore.y].prize < minPrize)
                    {
                        minPrize = valueMap[explore.x, explore.y].prize;
                        currentPosition = explore;
                    }
                }

                toExplore.Remove(currentPosition);

                int currentValue = valueMap[currentPosition.x, currentPosition.y].value;
                int currentHeight = heightMap[currentPosition.x, currentPosition.y];

                foreach (var easyLoopVar in easyLoop)
                {
                    int newX = currentPosition.x + easyLoopVar.x;
                    int newY = currentPosition.y + easyLoopVar.y;
                    if (newX < 0 || newX >= width || newY < 0 || newY >= height)
                        continue;

                    if (heightMap[newX, newY] - currentHeight > 1)
                        continue;

                    int newScore = currentValue+1;
                    double newPrize = GetPrize((newX, newY), endPosition) + newScore;
                    if (newPrize >= valueMap[newX, newY].prize)
                        continue;

                    valueMap[newX, newY].value = newScore;
                    valueMap[newX, newY].prize = newPrize;
                    toExplore.Add((newX, newY));
                }
            }

            return valueMap[endPosition.x, endPosition.y].value;
        }

        //Gets guessed prize for two points, which is distance * 2 here
        public static double GetPrize((int x, int y) node1, (int x, int y) node2)
        {
            int distX = Math.Abs(node2.x - node1.x);
            int distY = Math.Abs(node2.y - node1.y);
            return Math.Sqrt(distY * distY + distX * distX)*2;
        }
    }
}