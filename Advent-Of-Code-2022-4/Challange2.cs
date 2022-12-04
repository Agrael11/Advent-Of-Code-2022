namespace AdventOfCode.Day04
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

            int contained = 0;

            foreach (string line in inputData)
            {
                //Split the input data into integers
                string[] pairs = line.Split(',');
                string[] sections1 = pairs[0].Split('-');
                string[] sections2 = pairs[1].Split('-');
                int section1start = int.Parse(sections1[0]);
                int section1end = int.Parse(sections1[1]);
                int section2start = int.Parse(sections2[0]);
                int section2end = int.Parse(sections2[1]);

                //Check if 2 is overlapped with 1 or 1 with 2
                if (section2start <= section1end && section2end >= section1start)
                {
                    contained++;
                }
                else if (section1start <= section2end && section1end >= section2start)
                {
                    contained++;
                }
            }

            return contained;
        }

    }
}