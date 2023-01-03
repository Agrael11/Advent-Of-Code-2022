using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Day16
{
    /// <summary>1
    /// Main Class for Challange 1
    /// </summary>
    public static class Challange1
    {
        private static readonly Dictionary<string, ValveDefinition> valveDefinitions = new();
        private static readonly List<string> valvesWithFlow = new();

        /// <summary>
        /// This is the Main function
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static int DoChallange(string input)
        {
            //Read input data
            string[] inputData = input.Replace("\r", "").TrimEnd('\n').Split('\n');

            //Parse Inputs
            foreach (string line in inputData)
            {
                string[] splitLine = line.Split(' ');
                string name = splitLine[1];
                int flowrate = int.Parse(splitLine[4].Split('=')[1][..^1]);
                List<string> targets = new();
                for (int i = 9; i < splitLine.Length; i++)
                {
                    targets.Add(splitLine[i].TrimEnd(','));
                }
                valveDefinitions.Add(name, new ValveDefinition(name, flowrate, targets));

                //If valve can increase flow, add it to list
                if (flowrate != 0) valvesWithFlow.Add(name);
            }

            //Calculates best paths between all valves that contain flow + start point
            valvesWithFlow.Add("AA");

            foreach (string valve1 in valvesWithFlow)
            {
                foreach (string valve2 in valvesWithFlow)
                {
                    if (valve1 == valve2 || valveDefinitions[valve1].PathCosts.ContainsKey(valve2))
                        continue;

                    int best = FindBestPathBetween(valve1, valve2);
                    valveDefinitions[valve1].PathCosts.Add(valve2, best);
                    valveDefinitions[valve2].PathCosts.Add(valve1, best);
                }
            }

            valvesWithFlow.Remove("AA");

            //Creates node and calculates it's best price.

            TreeNode node = new TreeNode("AA");
            node.Build(valvesWithFlow, valveDefinitions);

            return node.GetBestPrice();
        }


        /// <summary>
        /// Calculates best fastest path price between two points.
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static int FindBestPathBetween(string point1, string point2)
        {
            string current = point1;
            Dictionary<string, int> toVisit = new();
            List<string> visited = new();
            toVisit.Add(current, 0);
            int price = 0;

            while (current != point2)
            {
                price++;
                visited.Add(current);
                toVisit.Remove(current);
                foreach (string next in valveDefinitions[current].Targets)
                {
                    if (visited.Contains(next) || toVisit.ContainsKey(next))
                        continue;
                    toVisit.Add(next, price);
                }
                toVisit = toVisit.OrderBy(a => a.Value).ToDictionary(x => x.Key, x => x.Value);
                current = toVisit.First().Key;
                price = toVisit[current];
            }
            return price;
        }
    }
}