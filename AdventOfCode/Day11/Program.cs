namespace Day11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                StreamReader sr = new StreamReader(args[0]);
                Console.SetIn(sr);
            }
            bool complete = false;
            List<Monkey> monkeys = new List<Monkey>();
            Monkey currentMonkey = null;
            while (!complete)
            {
                string input = Console.ReadLine();
                if (input == null)
                {
                    complete = true;
                    continue;
                }
                input = input.Trim();
                if(input == "")
                {
                    // next monkey
                    continue;
                }
                if (input[0] == 'M')
                {
                    // monkey row
                    string[] monkeyInput = input.Split(' ');
                    string idString = monkeyInput[1].Replace(":", "");
                    int id = int.Parse(idString);
                    currentMonkey = new Monkey(id);
                    monkeys.Add(currentMonkey);
                }
                else if (input[0] == 'S')
                {
                    // starting items
                    string[] parts = input.Split(':');
                    string[] items = parts[1].Split(',');
                    foreach(string itemString in items)
                    {
                        int worryLevel = int.Parse(itemString.Trim());
                        Item item = new Item(worryLevel);
                        currentMonkey.AddItem(item);
                    }

                }
                else if (input[0] == 'O')
                {
                    // operation
                    string[] parts = input.Split('=');
                    string operationString = parts[1].Trim();
                    string[] operationParts = operationString.Split(' ');
                    string operatorString = operationParts[1];
                    string operandString = operationParts[2];
                    int operand = 0;
                    if (operandString != "old")
                    {
                        operand = int.Parse(operandString);
                    }
                    else
                    {
                        operatorString = "square";
                    }
                    currentMonkey.Operand = operand;
                    currentMonkey.SetOperator(operatorString);
                }
                else if (input[0] == 'T')
                {
                    // test
                    string[] parts = input.Split(' ');
                    string lastPartString = parts[parts.Length - 1];
                    int lastPart = int.Parse(lastPartString);
                    currentMonkey.Test = lastPart;
                }
                else if (input[0] == 'I')
                {
                    // true or false
                    string[] parts = input.Split(' ');
                    string trueOrFalse = parts[1].Replace(":", "");
                    string lastPartString = parts[parts.Length - 1];
                    int lastPart = int.Parse(lastPartString);
                    if (bool.Parse(trueOrFalse))
                    {
                        // true
                        currentMonkey.TrueMonkeyId = lastPart;
                    }
                    else
                    {
                        //false
                        currentMonkey.FalseMonkeyId = lastPart;
                    }
                }
            }
            foreach(Monkey monkey in monkeys)
            {
                monkey.TrueMonkey = monkeys[monkey.TrueMonkeyId];
                monkey.FalseMonkey = monkeys[monkey.FalseMonkeyId];
            }
            int numberOfRounds = 20;
            for (int i = 0; i < numberOfRounds; i++)
            {
                foreach (Monkey monkey in monkeys)
                {
                    monkey.InspectItems();
                }
            }
            monkeys.Sort();
            int monkeyBusiness = monkeys[0].InspectionCount * monkeys[1].InspectionCount;
            Console.WriteLine(monkeyBusiness);
        }
    }
}
class Monkey : IComparable<Monkey>
{
    public int InspectionCount { get; set; }
    public int Id { get; set; }
    public int Test { get; set; }
    public int Operand { get; set; }
    public int TrueMonkeyId { get; set; }
    public int FalseMonkeyId { get; set; }
    public Monkey? TrueMonkey { get; set; }
    public Monkey? FalseMonkey { get; set; }
    public Queue<Item> Items { get; set; }
    public delegate int OperationDelegate(int worryLevel);
    public OperationDelegate? Operation;
    public Monkey(int id)
    {
        Id = id;
        Items = new Queue<Item>();
        InspectionCount = 0;
    }
    public void InspectItems()
    {
        while(Items.Count > 0)
        {
            InspectItem();
        }
    }
    public void InspectItem()
    {
        Item currentItem = Items.Dequeue();
        int worryLevel = Operation(currentItem.WorryLevel);
        worryLevel = worryLevel / 3;
        currentItem.WorryLevel = worryLevel;
        if(worryLevel % Test == 0)
        {
            TrueMonkey.AddItem(currentItem);
        }
        else
        {
            FalseMonkey.AddItem(currentItem);
        }
        InspectionCount++;
    }
    public void AddItem(Item item)
    {
        Items.Enqueue(item);
    }
    public void SetOperator(string operation)
    {
        if(operation == "square")
        {
            Operation = Square;
        }
        else if(operation == "*")
        {
            Operation = Multiply;
        }
        else if(operation == "+")
        {
            Operation = Add;
        }
    }
    public int Multiply(int worryLevel)
    {
        worryLevel = worryLevel * Operand;
        return worryLevel;
    }
    public int Add(int worryLevel)
    {
        worryLevel = worryLevel + Operand;
        return worryLevel;
    }
    public int Square(int worryLevel)
    {
        worryLevel = (worryLevel * worryLevel);
        return worryLevel;
    }

    public int CompareTo(Monkey? other)
    {
        return other.InspectionCount - InspectionCount;
    }
}
class Item
{
    public int WorryLevel { get; set; }
    public Item(int worryLevel)
    {
        WorryLevel = worryLevel;
    }
}