namespace AdventOfCode.Day16
{

    /// <summary>
    /// One point in tree of nodes
    /// </summary>
    public class TreeNode
    {
        string NodeName { get; set; }
        public int Price { get; set; }

        public readonly List<TreeNode> ContainingNodes = new();

        private TreeNode? _parent;
        private int _depth;

        public TreeNode(TreeNode? parent, string name, int depth, int price)
        {
            NodeName = name;
            _parent = parent;
            _depth = depth;
            Price = price;
        }
        public TreeNode(string name)
        {
            NodeName = name;
            _parent = null;
            _depth = 0;
            Price = 0;
        }

        /// <summary>
        /// Builds list of contained nodes recursively to make a tree
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="valveDefinitions"></param>
        public void Build(List<string> inputs, Dictionary<string, ValveDefinition> valveDefinitions)
        {
            foreach (string nextValve in inputs)
            {
                //If depth would be too long continue with next attempt
                int depth = _depth + 1 + valveDefinitions[nextValve].PathCosts[NodeName];
                if (depth > 29)
                    continue;

                //Creates new list of valve targets
                List<string> newInputs = new(inputs);
                newInputs.Remove(nextValve);

                //Calculates price for next node and creates it
                int price = Price + (30 - depth) * valveDefinitions[nextValve].FlowRate;
                TreeNode node = new(this, nextValve, depth, price);
                
                //Adds new node to tree and builds it if possible
                if (newInputs.Count > 0)
                    node.Build(newInputs, valveDefinitions);
                
                ContainingNodes.Add(node);
            }
        }

        /// <summary>
        /// Calculates best price of this and children in tree
        /// </summary>
        /// <returns></returns>
        public int GetBestPrice()
        {
            int bestPrice = Price;
            foreach (TreeNode node in ContainingNodes)
            {
                int price = node.GetBestPrice();
                if (price > bestPrice)
                    bestPrice = price;
            }
            return bestPrice;
        }

        /// <summary>
        /// Gets full path of this node from tree
        /// </summary>
        /// <returns></returns>
        public List<string> GetFullPath()
        {
            List<string> path = new();
            TreeNode? current = this;
            while (current != null)
            {
                path.Add(current.NodeName);
                current = current._parent;
            } 
            path.Reverse();
            return path;
        }

        public override string ToString()
        {
            return $"({NodeName}) [{string.Join(", ",GetFullPath())}]";
        }
    }
}