using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Day10
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

            List<int> cycleChecks = new()
            { 20, 60, 100, 140, 180, 220};

            int programCounter = 0;
            int regX = 1;
            int signalStrength = 0;

            foreach (string line in inputData)
            {
                //Start execution

                int tempRegMod = 0;
                if (line == "noop")
                {
                    programCounter += 1;
                }
                else
                {
                    string[] instruction = line.Split(' ');
                    
                    if (instruction[0] != "addx")
                        throw new Exception("Invalid instruction " + instruction[0]);

                    programCounter += 2;
                    tempRegMod += int.Parse(instruction[1]);
                }

                // Check for signal mod

                if (cycleChecks.Count > 0 && programCounter >= cycleChecks[0])
                {
                    signalStrength += cycleChecks[0] * regX;
                    cycleChecks.RemoveAt(0);
                }

                // Finish execution
                
                regX += tempRegMod;
            }

            return signalStrength;
        }
    }
}