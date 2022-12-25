namespace AdventOfCode.Day11
{
    /// <summary>
    /// Just a simple class that holds information about monkey
    /// </summary>
    public class Monkey
    {
        public enum Operators { Add, Subtract, Multiply, Divide}

        public readonly List<int> ItemIDs = new();
        public Operators Operator;
        public int? Operation;
        public int DivisibleTest;
        public int TrueTarget;
        public int FalseTarget;
        public ulong Inspections = 0;
        

        public Monkey(Operators operation, int? operationMod, int divisionTest, int trueTarget, int falseTarget, List<int> itemIDs)
        {
            Operator = operation;
            Operation = operationMod;
            DivisibleTest = divisionTest;
            TrueTarget = trueTarget;
            FalseTarget = falseTarget;
            foreach (int id in itemIDs)
            {
                ItemIDs.Add(id);
            }
        }
    }
}