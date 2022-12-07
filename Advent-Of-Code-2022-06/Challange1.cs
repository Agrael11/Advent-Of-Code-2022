namespace AdventOfCode.Day06
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
            string inputData = input.Replace("\r", "").TrimEnd('\n');

            //Declare variables and read first four characters
            string lastFour = inputData[..4];
            List<char> repeats = new();

            //Read through all characters in input and add them to the lastFour, while removing the first
            for (int charIndex = 4; charIndex < inputData.Length; charIndex++)
            {
                lastFour = lastFour.Remove(0, 1) + inputData[charIndex];
                for (int i = 0; i < 4; i++)
                {
                    if (!repeats.Contains(lastFour[i]))
                    {
                        repeats.Add(lastFour[i]);
                    }
                }

                //Check if character repeats
                if (repeats.Count == 4)
                {
                    return charIndex + 1;
                }
                else
                {
                    repeats.Clear();
                }
            }

            return -1;
        }
    }
}