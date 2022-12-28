using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Day13
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
            List<(ListNode node1, ListNode node2)> nodePairs = new();

            //Parses node pairs
            for (int i = 0; i < inputData.Length; i+=3)
            {
                ListNode node1 = parseListNode(inputData[i].Substring(1,inputData[i].Length-2));
                ListNode node2 = parseListNode(inputData[i+1].Substring(1, inputData[i+1].Length - 2));
                nodePairs.Add((node1, node2));
            }

            //Checks each pair - if left is correct, add it's index (i+1) into sum
            int sum = 0;
            for (int i = 0; i < nodePairs.Count; i++)
            {
                var nodePair = nodePairs[i];
                int result = CompareTwoNodes(nodePair.node1, nodePair.node2);
                if (result == -1) sum += i + 1;
            }

            return sum;
        }

        /// <summary>
        /// FInds ListNode Input - string between corrresponding [ and ] braces
        /// </summary>
        /// <param name="listNodeInput"></param>
        /// <returns></returns>
        public static string GetListNodeInput(string listNodeInput)
        {
            string result = listNodeInput;
            int braces = 0;
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] == '[') braces++;
                if (result[i] == ']') braces--;
                if (braces == 0)
                {
                    result = result.Substring(1, i - 1);
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Compares two nodes
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <returns></returns>
        public static int CompareTwoNodes(Node node1, Node node2)
        {
            //If both nodes are integers, compare them
            //Equaal = 0
            //Left lower = -1
            //Left higher = 1
            if (node1.GetType() == typeof(IntNode) && node2.GetType() == typeof(IntNode))
            {
                int node1Value = ((IntNode)node1).Value;
                int node2Value = ((IntNode)node2).Value;
                if (node1Value == node2Value)
                    return 0;
                if (node1Value < node2Value)
                {
                    return -1;
                }
                return 1;
            }

            //If one of nodes is integer and other list, convert integer one into list and compare them using this function
            if (node1.GetType() == typeof(IntNode) && node2.GetType() == typeof(ListNode))
            {
                ListNode realNode1 = ((IntNode)node1).ToListNode();
                return CompareTwoNodes(realNode1, node2);
            }
            if (node1.GetType() == typeof(ListNode) && node2.GetType() == typeof(IntNode))
            {
                ListNode realNode2 = ((IntNode)node2).ToListNode();
                return CompareTwoNodes(node1, realNode2);
            }

            //IF both are list, compare each item in list.
            //Equal = 0
            //Left out of items first or left recursively higher = 1
            //Right out of items first or left recursively lower = -1
            ListNode listNode1 = (ListNode)node1;
            ListNode listNode2 = (ListNode)node2;

            for (int i = 0; i < listNode1.Nodes.Count; i++)
            {
                if (listNode2.Nodes.Count - 1 < i) return 1;
                int result = CompareTwoNodes(listNode1.Nodes[i], listNode2.Nodes[i]);
                if (result == 0) continue;
                return result;
            }
            if (listNode1.Nodes.Count == listNode2.Nodes.Count)
                return 0;

            return -1;
        }

        /// <summary>
        /// Parses string into ListNode
        /// </summary>
        /// <param name="listNodeInput"></param>
        /// <returns></returns>
        public static ListNode parseListNode(string listNodeInput)
        {
            ListNode listNode =  new ListNode();

            string data = "";

            for (int charPos = 0; charPos < listNodeInput.Length; charPos++)
            {
                if (listNodeInput[charPos] == '[')
                {
                    data = GetListNodeInput(listNodeInput.Substring(charPos));
                    charPos += data.Length + 2;
                    listNode.Nodes.Add(parseListNode(data));
                    data = "";
                }
                else if (listNodeInput[charPos] == ',')
                {
                    listNode.Nodes.Add(new IntNode() { Value = int.Parse(data) });
                    data = "";
                }
                else
                {
                    data += listNodeInput[charPos];
                }
            }
            if (data != "")
            {
                listNode.Nodes.Add(new IntNode() { Value = int.Parse(data) });
            }

            return listNode;
        }   
    }
}