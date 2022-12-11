using System.Numerics;

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
                        long worryLevel = long.Parse(itemString.Trim());
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
                    long operand = 0;
                    if (operandString != "old")
                    {
                        operand = long.Parse(operandString);
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
                    long lastPart = long.Parse(lastPartString);
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
            long[] divisors = new long[monkeys.Count];
            int index = 0;
            foreach(Monkey monkey in monkeys)
            {
                divisors[index] = monkey.Test;
                index++;
                monkey.TrueMonkey = monkeys[monkey.TrueMonkeyId];
                monkey.FalseMonkey = monkeys[monkey.FalseMonkeyId];
            }
            Monkey.LowestCommonMultiple = LowestCommonMultiple(divisors);
            foreach (Monkey monkey in monkeys)
            {
                Console.WriteLine(monkey);
            }
            int numberOfRounds = 10000;
            for (int i = 0; i < numberOfRounds; i++)
            {
                Console.WriteLine("Round " + i.ToString());
                foreach (Monkey monkey in monkeys)
                {
                    monkey.InspectItems();
                }
                //foreach (Monkey monkey in monkeys)
                //{
                //    Console.WriteLine(monkey);
                //}
            }
            foreach (Monkey monkey in monkeys)
            {
                Console.WriteLine(monkey);
            }
            monkeys.Sort();
            
            long monkeyBusiness = monkeys[0].InspectionCount * monkeys[1].InspectionCount;
            Console.WriteLine(monkeyBusiness);
        }
        public static long LowestCommonMultiple(long[] numbers)
        {
            long answer = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                long number = numbers[i];
                answer = number * answer / GreatestCommonDivisor(number, answer);
            }
            return answer;
        }
        public static long GreatestCommonDivisor(long a, long b)
        {
            return b == 0 ? a : GreatestCommonDivisor(b, a % b);
        }
    }
    
}
class Monkey : IComparable<Monkey>
{
    public static long LowestCommonMultiple = 0;
    public long InspectionCount { get; set; }
    public int Id { get; set; }
    public long Test { get; set; }
    public long Operand { get; set; }
    public int TrueMonkeyId { get; set; }
    public int FalseMonkeyId { get; set; }
    public Monkey? TrueMonkey { get; set; }
    public Monkey? FalseMonkey { get; set; }
    public Queue<Item> Items { get; set; }
    public delegate void OperationDelegate(Item item);
    public OperationDelegate? Operation;
    public Monkey(int id)
    {
        Id = id;
        Items = new Queue<Item>();
        InspectionCount = 0;
    }
    public void InspectItems()
    {
       // Console.WriteLine(this);
        while (Items.Count > 0)
        {
            InspectItem();
        }
    }
    public void InspectItem()
    {
        Item currentItem = Items.Dequeue();
        InspectionCount++;
        Operation(currentItem);
        //worryLevel = worryLevel / 3;
        long testResult = (long)currentItem.WorryLevel % (long)Test;
        //  Console.WriteLine("   Test: " + worryLevel + " % " + Test + " = " + testResult);
        if (testResult == 0)
        {
            TrueMonkey.AddItem(currentItem);
           // Console.WriteLine("   Throws: to Monkey " + TrueMonkeyId + " (True)");
        }
        else
        {
            FalseMonkey.AddItem(currentItem);
           // Console.WriteLine("   Throws: to Monkey " + FalseMonkeyId + " (False)");
        }
       // Console.WriteLine();


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
    public void Multiply(Item item)
    {
        
        item.WorryLevel = (item.WorryLevel * Operand) % Monkey.LowestCommonMultiple;
        
        
        //Console.WriteLine("   Inspects: " + worryLevel + " * " + Operand + " = " + newWorryLevel);
        
    }
    public void Add(Item item)
    {
        item.WorryLevel = (item.WorryLevel + Operand) % Monkey.LowestCommonMultiple;
    }
    public void Square(Item item)
    {
        item.WorryLevel = (item.WorryLevel * item.WorryLevel) % Monkey.LowestCommonMultiple;
    }

    public int CompareTo(Monkey? other)
    {
        return (int)(other.InspectionCount - InspectionCount);
    }
    public override string ToString()
    {
        string returnString = "Monkey " + Id + "(" + InspectionCount + ") : ";
        foreach(Item item in Items)
        {
            returnString += " " + item.ToString() + ",";
        }
        return returnString;
    }
}
class Item
{
    static int Count = 0;
    public int Id { get; set; }
    public long WorryLevel { get; set; }
    public Item(long worryLevel)
    {
        WorryLevel = worryLevel;
        Id = Count++;
    }
    public override string ToString()
    {
        string returnString = "Item " + Id + "(" + WorryLevel + ")";
        
        return returnString;
    }
}