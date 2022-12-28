namespace AdventOfCode.Day13
{
    public class Node
    {

    }

    public class IntNode: Node
    {
        public int Value;

        public static ListNode ToListNode(IntNode node)
        {
            ListNode newNode = new();
            newNode.Nodes.Add(node);
            return newNode;
        }

        public ListNode ToListNode()
        {
            return ToListNode(this);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class ListNode: Node
    {
        public List<Node> Nodes = new();
        public override string ToString()
        {
            string str = "[";
            for (int i = 0; i < Nodes.Count; i++)
            {
                str += Nodes[i].ToString();
                if (i < Nodes.Count - 1) str += ",";
            }
            str += "]";
            return str;
        }
    }
}