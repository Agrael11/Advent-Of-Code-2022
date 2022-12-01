namespace AdventOfCode.Day01
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

            //Prepare variables - list of elves and amount of calories for current elf
            List<int> elves = new();
            int elfCalories = 0;

            //For each line in input
            for (int i = 0; i < inputData.Length; i++)
            {
                //If line is empty, add calories carried by current elf to list of elves and reset value of current calories
                if (string.IsNullOrWhiteSpace(inputData[i]))
                {
                    elves.Add(elfCalories);
                    elfCalories = 0;
                    continue;
                }

                //Add calories of item to current elfs calories
                elfCalories += int.Parse(inputData[i]);
            }

            //Add calories carried by last elf to list of elves
            elves.Add(elfCalories);

            //Sort the list
            elves.Sort();

            //And return the last item (biggest)
            return elves[^1];
        }
    }
}