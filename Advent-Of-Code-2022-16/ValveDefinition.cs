namespace AdventOfCode.Day16
{

    /// <summary>
    /// Contains information about one valve, including it's flow rate, targets and best costs 
    /// between itself and other valves with flow
    /// </summary>
    public class ValveDefinition
    {
        public string ValveName { get; set; }
        public int FlowRate { get; set; }
        public readonly List<string> Targets;
        public readonly Dictionary<string, int> PathCosts = new();

        public ValveDefinition(string valveName, int flowRate, List<string> targets)
        {
            ValveName = valveName;
            FlowRate = flowRate;
            Targets = targets;
        }

        public override string ToString()
        {
            string ret = $"Valve {ValveName} has flow rate = {FlowRate}; tunnels lead to valves ";
            foreach (string target in Targets)
            {
                ret += target + ", ";
            }
            ret = ret.TrimEnd(' ').TrimEnd(',');
            return ret;
        }
    }
}