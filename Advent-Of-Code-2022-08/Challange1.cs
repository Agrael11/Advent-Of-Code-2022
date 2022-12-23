using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Day08
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

            int forestWidth = inputData[0].Length;
            int forestHeight = inputData.Length;

            //Build the Forest Height Map
            int[,] forestHeightMap = new int[forestWidth, forestHeight];
            int visible = 2 * forestWidth + 2 * forestHeight - 4;

            for (int y = 0; y < forestHeight; y++)
            {
                for (int x = 0; x < forestWidth; x++)
                {
                    forestHeightMap[x, y] = (int)(inputData[y][x] - '0');
                }
            }

            //Check Every Tree Inside the Forest Height Map For Visibility

            for (int y = 1; y < forestHeight - 1; y++)
            {
                for (int x = 1; x < forestWidth - 1; x++)
                {
                    int treeSize = forestHeightMap[x, y];
                    //From Left
                    if (!IsTallerBetweenLine(0, x - 1, y, treeSize, ref forestHeightMap))
                    {
                        visible++;
                        continue;
                    }
                    //From Right
                    if (!IsTallerBetweenLine(x + 1, forestWidth - 1, y, treeSize, ref forestHeightMap))
                    {
                        visible++;
                        continue;
                    }
                    //From Top
                    if (!IsTallerBetweenRow(0, y - 1, x, treeSize, ref forestHeightMap))
                    {
                        visible++;
                        continue;
                    }
                    //From Bottom
                    if (!IsTallerBetweenRow(y + 1, forestHeight - 1, x, treeSize, ref forestHeightMap))
                    {
                        visible++;
                        continue;
                    }
                }
            }

            return visible;
        }

        /// Check if finds taller tree between x1 (inclusive) to x2 (inclusive), at line y
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        /// <param name="heightMap"></param>
        /// <returns></returns>
        static bool IsTallerBetweenLine(int x1, int x2, int y, int value, ref int[,] heightMap)
        {
            for (int x = x1; x <= x2; x++)
            {
                if (heightMap[x, y] >= value) return true;
            }
            return false;
        }

        /// Check if finds taller tree between y1 (inclusive) to y2 (inclusive), at row x
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        /// <param name="heightMap"></param>
        /// <returns></returns>
        static bool IsTallerBetweenRow(int y1, int y2, int x, int value, ref int[,] heightMap)
        {
            for (int y = y1; y <= y2; y++)
            {
                if (heightMap[x, y] >= value) return true;
            }
            return false;
        }
    }
}