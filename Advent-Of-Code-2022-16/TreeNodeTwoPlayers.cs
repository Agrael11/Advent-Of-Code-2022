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

        public readonly List<TreeNodeTwoPlayers> ContainingNodes = new();

        private TreeNodeTwoPlayers? _parent;
        private int _mySteps;
        private int _elephantSteps;

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
        public void Build(List<string> inputs, Dictionary<string, ValveDefinition> valveDefinitions)
        {
            foreach (string nextValveMine in inputs)
            {
                foreach (string nextValveElephants in inputs)
                {
                    //Ignore if same valve
                    if (nextValveElephants == nextValveMine)
                        continue;

                    int nextDepthMe = _mySteps + 1 + valveDefinitions[nextValveMine].PathCosts[MyNodeName];
                    int nextDepthElephant = _elephantSteps + 1 + valveDefinitions[nextValveElephants].PathCosts[ElephantNodeName];
                    
                    //If both points would be too deep
                    if (nextDepthElephant > 25 && nextDepthMe > 25)
                        continue;

                    string nextValveNameMine = nextValveMine;
                    string nextValveNameElephant = nextValveElephants;
                    int nextPriceMe = MyPrice + (26 - nextDepthMe) * valveDefinitions[nextValveMine].FlowRate;
                    int nextPriceElephant = ElephantPrice + (26 - nextDepthElephant) * valveDefinitions[nextValveElephants].FlowRate;
                    
                    List<string> newInputs = new(inputs);

                    //Check if one of points would be too deep
                    if (nextDepthElephant > 25)
                    {
                        nextValveNameElephant = ElephantNodeName;
                        nextPriceElephant = ElephantPrice;
                        nextDepthElephant = _elephantSteps;
                        newInputs.Remove(nextValveMine);
                    }
                    else if (nextDepthMe > 25)
                    {
                        nextValveNameMine = MyNodeName;
                        nextPriceElephant = MyPrice;
                        nextDepthElephant = _mySteps;
                        newInputs.Remove(nextValveElephants);
                    }
                    else
                    {
                        newInputs.Remove(nextValveMine);
                        newInputs.Remove(nextValveElephants);
                    }

                    //Creates new node and builds it
                    TreeNodeTwoPlayers node = new(this, nextValveNameMine, nextValveNameElephant, nextDepthMe, nextDepthElephant, nextPriceMe, nextPriceElephant);

                    node.Build(newInputs, valveDefinitions);

                    ContainingNodes.Add(node);
                }
            }
        }

        /// <summary>
        /// Gets best price in a tree
        /// </summary>
        /// <returns></returns>
        public int GetBestPrice()
        {
            int bestPrice = Price;
            foreach (var node in ContainingNodes)
            {
                int price = node.GetBestPrice();
                if (price > bestPrice)
                    bestPrice = price;
            }
            return bestPrice;
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

        public override string ToString()
        {
            return $"({{{MyNodeName},{ElephantNodeName})}} [{string.Join(", ",GetFullPath())}]";
        }
    }
}