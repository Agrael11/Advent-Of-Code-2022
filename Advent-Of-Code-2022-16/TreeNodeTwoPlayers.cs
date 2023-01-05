using System.Collections;
using System.Linq.Expressions;

namespace AdventOfCode.Day16
{
    /// <summary>
    /// Contains information about one node in a tree - contains information for character and elephant
    /// </summary>
    public class TreeNodeTwoPlayers
    {
        string MyNodeName { get; set; }
        string ElephantNodeName { get; set; }
        public int ElephantPrice { get; set; }
        public int MyPrice { get; set; }
        public int Price { get { return ElephantPrice + MyPrice; } }

        private TreeNodeTwoPlayers? _parent;
        private int _mySteps;
        private int _elephantSteps;
        private static HashSet<(int p1, int p2)> _visitedNodePaths = new();
        public static int maxPrice = 0;

        public TreeNodeTwoPlayers(TreeNodeTwoPlayers? parent, string myName, string elephantName, int mySteps, int elephantSteps, int myPrice, int elephantPrice)
        {
            MyNodeName = myName;
            ElephantNodeName = elephantName;
            _parent = parent;
            _mySteps = mySteps;
            _elephantSteps = elephantSteps;
            MyPrice = myPrice;
            ElephantPrice = elephantPrice;
        }

        public TreeNodeTwoPlayers(string startPoint)
        {
            MyNodeName = startPoint;
            ElephantNodeName = startPoint;
            _parent = null;
            _mySteps = 0;
            _elephantSteps = 0;
            MyPrice = 0;
            ElephantPrice = 0;
        }


        /// <summary>
        /// Builds next point in tree
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="valveDefinitions"></param>
        public int Build(List<int> toVisitList, List<string> inputs, Dictionary<string, ValveDefinition> valveDefinitions)
        {
            int highestPrice = 0;
            foreach (int nextValveMineID in toVisitList)
            {
                string nextValveMine = inputs[nextValveMineID];
                foreach (int nextValveElephantsID in toVisitList)
                {
                    string nextValveElephants = inputs[nextValveElephantsID];
                    //Ignore if same valve
                    if (nextValveElephants == nextValveMine)
                        continue;

                    int nextDepthMe = _mySteps + 1 + valveDefinitions[nextValveMine].PathCosts[MyNodeName];
                    int nextDepthElephant = _elephantSteps + 1 + valveDefinitions[nextValveElephants].PathCosts[ElephantNodeName];
                    
                    //If both points would be too deep
                    if (nextDepthElephant > 25 && nextDepthMe > 25)
                        continue;

                    int flags1 = GetPathFlags(GetFullPath(0, nextValveMine), inputs);
                    int flags2 = GetPathFlags(GetFullPath(1, nextValveElephants), inputs);
                    bool containsPathFlags = _visitedNodePaths.Contains((flags1, flags2)) || _visitedNodePaths.Contains((flags2, flags1));

                    if (containsPathFlags)
                        continue;

                    string nextValveNameMine = nextValveMine;
                    string nextValveNameElephant = nextValveElephants;
                    int nextPriceMe = MyPrice + (26 - nextDepthMe) * valveDefinitions[nextValveMine].FlowRate;
                    int nextPriceElephant = ElephantPrice + (26 - nextDepthElephant) * valveDefinitions[nextValveElephants].FlowRate;
                    
                    List<int> newToVisitList = new(toVisitList);

                    //Check if one of points would be too deep
                    if (nextDepthElephant > 25)
                    {
                        nextValveNameElephant = ElephantNodeName;
                        nextPriceElephant = ElephantPrice;
                        nextDepthElephant = _elephantSteps;
                        newToVisitList.Remove(inputs.IndexOf(nextValveNameMine));

                    }
                    else if (nextDepthMe > 25)
                    {
                        nextValveNameMine = MyNodeName;
                        nextPriceElephant = MyPrice;
                        nextDepthElephant = _mySteps;
                        newToVisitList.Remove(inputs.IndexOf(nextValveElephants));
                    }
                    else
                    {
                        newToVisitList.Remove(inputs.IndexOf(nextValveNameMine));
                        newToVisitList.Remove(inputs.IndexOf(nextValveElephants));
                    }

                    if (nextPriceElephant + nextPriceMe > highestPrice)
                    {
                        highestPrice = nextPriceMe + nextPriceElephant;
                        if (highestPrice > maxPrice)
                        {
                            maxPrice = highestPrice;
                            int left = Console.CursorLeft;
                            Console.Write(" " + maxPrice);
                            Console.CursorLeft = left;
                        }
                    }

                    //Creates new node and builds it
                    TreeNodeTwoPlayers node = new(this, nextValveNameMine, nextValveNameElephant, nextDepthMe, nextDepthElephant, nextPriceMe, nextPriceElephant);

                    flags1 = GetPathFlags(GetFullPath(0, nextValveNameMine), inputs);
                    flags2 = GetPathFlags(GetFullPath(1, nextValveNameElephant), inputs);


                    _visitedNodePaths.Add((flags1, flags2));

                    int buildPrize = node.Build(newToVisitList, inputs, valveDefinitions);

                    if (buildPrize > highestPrice)
                    {
                        highestPrice = buildPrize;
                        if (highestPrice > maxPrice)
                        {
                            maxPrice = highestPrice;
                            int left = Console.CursorLeft;
                            Console.Write(" " + maxPrice);
                            Console.CursorLeft = left;
                        }
                    }   
                }
            }
            return highestPrice;
        }

        public int GetPathFlags(List<string> path, List<string> inputs)
        {
            string fpath = "";
            for (int i = 0; i < path.Count; i++)
            {
                string pathPart = path[i];
                if (pathPart != "AA")
                {
                    fpath += pathPart + ",";
                }
            }
            return fpath.GetHashCode();
        }

        /// <summary>
        /// Gets full path of tree in this point.
        /// </summary>
        /// <returns></returns>
        public List<string> GetFullPath()
        {
            List<string> path = new();
            TreeNodeTwoPlayers? current = this;
            while (current != null)
            {
                path.Add($"{{{current.MyNodeName},{current.ElephantNodeName}}}");
                current = current._parent;
            }
            path.Reverse();
            return path;
        }

        public List<string> GetFullPath(int player)
        {
            List<string> path = new();
            TreeNodeTwoPlayers? current = this;
            while (current != null)
            {
                if (player == 0)
                    path.Add(current.MyNodeName);
                else
                    path.Add(current.ElephantNodeName);
                current = current._parent;
            }
            path.Reverse();
            return path;
        }

        public List<string> GetFullPath(int player, string next)
        {
            List<string> path = new();
            TreeNodeTwoPlayers? current = this;
            while (current != null)
            {
                if (player == 0)
                    path.Add(current.MyNodeName);
                else
                    path.Add(current.ElephantNodeName);
                current = current._parent;
            }
            path.Reverse();
            path.Add(next);
            return path;
        }

        public override string ToString()
        {
            return $"({{{MyNodeName},{ElephantNodeName})}} [{string.Join(", ",GetFullPath())}]";
        }
    }
}