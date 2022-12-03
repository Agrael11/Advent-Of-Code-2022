namespace AdventOfCode.Day03
{
    /// <summary>
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

            int score = 0;

            for (int i = 0; i < inputData.Length; i+=3)
            {
                //Find shared item on three lines - rucksacks
                char shared = FindCommon(inputData, i);

                //And add to score - a - z = 1 - 26, A - Z = 27-52
                if (shared >= 'a' && shared <= 'z')
                {
                    score += (shared - 'a') + 1;
                }
                if (shared >= 'A' && shared <= 'Z')
                {
                    score += (shared - 'A') + 27;
                }
            }

            return score;
        }

        /// <summary>
        /// find shared item in three rucksacks, starting at index.
        /// </summary>
        /// <param name="inputData"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static char FindCommon(string[] inputData, int index)
        {
            foreach (char item1 in inputData[index])
            {
                foreach (char item2 in inputData[index + 1])
                {
                    if (item1 != item2) continue;
                    foreach (char item3 in inputData[index + 2])
                    {
                        if (item1 == item3) return item1;
                    }
                }
            }

            return '\0';
        }
    }
}