using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Day11
{
    /// <summary>1
    /// Main Class for Challange 2
    /// </summary>
    public static class Challange2
    {
        /// <summary>
        /// Finds greatest common divisor
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        static int GreatestCommonDivisor(int number1, int number2)
        {
            while (number2 != 0)
            {
                int temp = number2;
                number2 = number1 % number2;
                number1 = temp;
            }
            return number1;
        }

        /// <summary>
        /// Finds lowest common multiple for two numbers
        /// </summary>
        /// <param name="number1"></param>
        /// <param name="number2"></param>
        /// <returns></returns>
        static int LeastCommonMultiple(int number1, int number2)
        {
            return (number1 / GreatestCommonDivisor(number1, number2)) * number2;
        }

        /// <summary>
        /// Finds lowest common multiple for array of numbers
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        static int LeastCommonMultiple(int[] inputs)
        {
            int lcm = inputs[0];
            for (int i = 1; i < inputs.Length; i++)
            {
                lcm = LeastCommonMultiple(lcm, inputs[i]);
            }
            return lcm;
        }

        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static ulong DoChallange(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            List<long> itemStressLevels = new();
            List<Monkey> monkeyList = new();
            long lowestCommonMultiple = 0;

            //Input parsing
            for (int i = 0; i < inputData.Length; i += 7)
            {
                string[] lineData = inputData[i + 1].TrimStart().Split(' ');
                List<int> ids = new();
                for (int item = 2; item < lineData.Length; item++)
                {
                    long itemStress = long.Parse(lineData[item].TrimEnd(','));
                    ids.Add(itemStressLevels.Count);
                    itemStressLevels.Add(itemStress);
                }
                lineData = inputData[i + 2].TrimStart().Split(' ');
                Monkey.Operators operation = lineData[4] switch
                {
                    "+" => Monkey.Operators.Add,
                    "-" => Monkey.Operators.Subtract,
                    "*" => Monkey.Operators.Multiply,
                    "/" => Monkey.Operators.Divide,
                    _ => Monkey.Operators.Add
                };
                int? operationMod = null;
                if (lineData[5] != "old")
                {
                    operationMod = int.Parse(lineData[5]);
                }
                lineData = inputData[i + 3].TrimStart().Split(' ');
                int test = int.Parse(lineData[3]);
                lineData = inputData[i + 4].TrimStart().Split(' ');
                int trueTarget = int.Parse(lineData[5]);
                lineData = inputData[i + 5].TrimStart().Split(' ');
                int falseTarget = int.Parse(lineData[5]);
                Monkey m = new(operation, operationMod, test, trueTarget, falseTarget, ids);
                monkeyList.Add(m);
            }

            //Finds lowest common multiple
            int[] divisionTargets = new int[monkeyList.Count];
            for (int i = 0; i < monkeyList.Count; i++)
            {
                divisionTargets[i] = monkeyList[i].DivisibleTest;
            }
            lowestCommonMultiple = (long)LeastCommonMultiple(divisionTargets);

            //10000 rounds of monkey game
            for (int round = 0; round < 10000; round++)
            {
                foreach (Monkey monkey in monkeyList)
                {
                    //while monkey is holding an item modify it's stress level according to monkey's "programming"
                    //and send it to target monkey, depending on monkeys decisionmaking 
                    while (monkey.ItemIDs.Count > 0)
                    {
                        int itemID = monkey.ItemIDs[0];
                        long itemStress = itemStressLevels[itemID];
                        long operationMod = itemStress;
                        if (monkey.Operation != null)
                            operationMod = (long)monkey.Operation.Value;
                        switch (monkey.Operator)
                        {
                            case Monkey.Operators.Add: itemStress += operationMod; break;
                            case Monkey.Operators.Subtract: itemStress -= operationMod; break;
                            case Monkey.Operators.Multiply: itemStress *= operationMod; break;
                            case Monkey.Operators.Divide: itemStress /= operationMod; break;
                        }

                        //Keeps stress level managable
                        itemStress %= lowestCommonMultiple;

                        if (itemStress % monkey.DivisibleTest == 0)
                        {
                            monkeyList[monkey.TrueTarget].ItemIDs.Add(itemID);
                        }
                        else
                        {
                            monkeyList[monkey.FalseTarget].ItemIDs.Add(itemID);
                        }

                        monkey.ItemIDs.RemoveAt(0);
                        itemStressLevels[itemID] = itemStress;
                        
                        //Keeps track on how many items monkey has processed
                        monkey.Inspections++;
                    }
                }
            }
            monkeyList = monkeyList.OrderByDescending(m => m.Inspections).ToList();
            return monkeyList[0].Inspections * monkeyList[1].Inspections;
        }
    }
}