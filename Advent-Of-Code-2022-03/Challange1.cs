namespace AdventOfCode.Day03
{
    /// <summary>
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

            int score = 0;

            foreach (string line in inputData)
            {
                //Split line (rucksack) data to two identical halves - compartments
                string compartment1 = line[..(line.Length / 2)];
                string compartment2 = line[(line.Length / 2)..];

                //Find shared item
                char shared = FindCommon(compartment1, compartment2);

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
        /// Finds shared item in compartments
        /// </summary>
        /// <param name="compartment1"></param>
        /// <param name="compartment2"></param>
        /// <returns></returns>
        public static char FindCommon(string compartment1, string compartment2)
        {
            foreach (char item1 in compartment1)
            {
                foreach (char item2 in compartment2)
                {
                    if (item1 == item2) return item1;
                }
            }

            return '\0';
        }
    }
}