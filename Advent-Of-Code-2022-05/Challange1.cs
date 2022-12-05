using System.ComponentModel.DataAnnotations;

namespace AdventOfCode.Day05
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
        public static string DoChallange(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            List<string> startPositions = new();
            List<string> instructions = new();

            //Split the input data into startPositions and instructions, for easier use
            bool readingPositions = true;
            foreach (string line in inputData)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    readingPositions = false;
                    startPositions.RemoveAt(startPositions.Count - 1);
                    continue;
                }

                if (readingPositions)
                {
                    startPositions.Add(line);
                }
                else
                {
                    instructions.Add(line);
                }
            }

            startPositions.Reverse();

            List<List<char>> stacks = new();

            //Parse start positions into actual stacks
            foreach (string line in startPositions)
            {
                for (int i = 0; i < line.Length; i+=4)
                {
                    if (stacks.Count <= i/4)
                    {
                        stacks.Add(new());
                    }

                    string crate = line.Substring(i, 3);
                    if (string.IsNullOrWhiteSpace(crate))
                        continue;
                    else
                    {
                        stacks[i / 4].Add(crate[1]);
                    }
                }
            }

            //Move crates into stacks according to instructions
            foreach (string line in instructions)
            {
                string[] instruction = line.Split(' ');
                int amount = int.Parse(instruction[1]);
                int source = int.Parse(instruction[3]);
                int destination = int.Parse(instruction[5]);
                for (int i = 0; i < amount; i++)
                {
                    char crate = stacks[source - 1][stacks[source - 1].Count - 1];
                    stacks[destination - 1].Add(crate);
                    stacks[source - 1].RemoveAt(stacks[source - 1].Count - 1);
                }
            }

            //And read the top crate of each stack
            string result = "";
            foreach (List<char> stack in stacks)
            {
                result += stack[stack.Count - 1];
            }

            return result;
        }
    }
}