using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Day08
{
    /// <summary>1
    /// Main Class for Challange 2  
    /// </summary>
    public static class Challange2
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

            int forestWidth = inputData[0].Length;
            int forestHeight = inputData.Length;

            //Build the Forest Height Map
            int[,] forestHeightMap = new int[forestWidth, forestHeight];
            int record = 0;

            for (int y = 0; y < forestHeight; y++)
            {
                for (int x = 0; x < forestWidth; x++)
                {
                    forestHeightMap[x, y] = (int)(inputData[y][x] - '0');
                }
            }

            //Check Every Tree Inside the Forest Height Map For Number of Visible Trees

            for (int y = 1; y < forestHeight - 1; y++)
            {
                for (int x = 1; x < forestWidth - 1; x++)
                {
                    int visible = 1;
                    int treeSize = forestHeightMap[x, y];
                    //To Left
                    visible *= HowManyShorterBetweenLine(x - 1, 0, y, treeSize, ref forestHeightMap);
                    //To Right
                    visible *= HowManyShorterBetweenLine(x + 1, forestWidth-1, y, treeSize, ref forestHeightMap);
                    //To Top
                    visible *= HowManyShorterBetweenRow(y - 1, 0, x, treeSize, ref forestHeightMap);
                    //To Bottom
                    visible *= HowManyShorterBetweenRow(y + 1, forestHeight-1, x, treeSize, ref forestHeightMap);
                    if (visible > record) record = visible;
                }
            }

            return record;
        }

        /// <summary>
        /// Finds shorter trees starting at x1 (inclusive) to x2 (inclusive), at line y
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        /// <param name="heightMap"></param>
        /// <returns></returns>
        static int HowManyShorterBetweenLine(int x1, int x2, int y, int value, ref int[,] heightMap)
        {
            int visible = 0;
            if (x1 < x2)
            {
                for (int x = x1; x <= x2; x++)
                {
                    visible++;
                    if (heightMap[x, y] >= value) break;
                }
            }
            else
            {
                for (int x = x1; x >= x2; x--)
                {
                    visible++;
                    if (heightMap[x, y] >= value) break;
                }
            }
            return visible;
        }

        /// <summary>
        /// Finds shorter trees starting at y1 (inclusive) to y2 (inclusive), at row x
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        /// <param name="heightMap"></param>
        /// <returns></returns>
        static int HowManyShorterBetweenRow(int y1, int y2, int x, int value, ref int[,] heightMap)
        {
            int visible = 0;
            if (y1 < y2)
            {
                for (int y = y1; y <= y2; y++)
                {
                    visible++;
                    if (heightMap[x, y] >= value) break;
                }
            }
            else
            {
                for (int y = y1; y >= y2; y--)
                {
                    visible++;
                    if (heightMap[x, y] >= value) break;
                }
            }
            return visible;
        }
    }
}