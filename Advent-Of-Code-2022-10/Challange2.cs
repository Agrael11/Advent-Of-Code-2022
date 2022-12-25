using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Day10
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
        public static string DoChallange(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            int programCounter = 0;
            int regX = 1;
            string result = "";

            foreach (string line in inputData)
            {
                //Start execution

                int tempRegMod = 0;
                int progCounterInc = 0;
                if (line == "noop")
                {
                    progCounterInc = 1;
                }
                else
                {
                    string[] instruction = line.Split(' ');

                    if (instruction[0] != "addx")
                        throw new Exception("Invalid instruction " + instruction[0]);
                  
                    progCounterInc = 2;
                    tempRegMod += int.Parse(instruction[1]);
                }

                // Draw pixels I guess

                for (int i = 0; i <progCounterInc; i++)
                {
                    int pcoffset = programCounter % 40;

                    if (pcoffset == 0 && result != "")
                    {
                        result += "\n";
                    }

                    if (pcoffset + 1 >= regX && pcoffset + 1 <= regX + 2)
                    {
                        result += "█";
                    }
                    else
                    {
                        result += "░";
                    }

                    programCounter++;
                }

                // Finish execution
                regX += tempRegMod;
            }

            return result;
        }
    }
}