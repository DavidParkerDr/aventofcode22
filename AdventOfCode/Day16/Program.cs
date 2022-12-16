namespace Day16
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
            Dictionary<string, Valve> valves = new Dictionary<string, Valve>();
            while (!complete)
            {
                string input = Console.ReadLine();
                if (input == null)
                {
                    complete = true;
                    continue;
                }

                input = input.Replace("Valve ", "");
                string valveName = input.Substring(0, 2);
                string[] parts = input.Split('=');
                string secondPart = parts[1];
                string[] afterTheEqualsParts = secondPart.Split(';');
                string rateString = afterTheEqualsParts[0];
                int rate = int.Parse(rateString);
                string afterTheSemiColon = afterTheEqualsParts[1];
                afterTheSemiColon = afterTheSemiColon.Replace(" tunnels lead to valves ", "");
                afterTheSemiColon = afterTheSemiColon.Replace(" tunnel leads to valve ", "");
                string[] valveParts = afterTheSemiColon.Split(", ");
                Valve valve = new Valve(valveName, rate);
                valves.Add(valve.Name, valve);
                foreach(string valvePart in valveParts)
                {
                    valve.AddOtherValveString(valvePart);
                }    
            }
            foreach(KeyValuePair<string, Valve> pair in valves)
            {
                Valve valve = pair.Value;
                foreach(string otherValveString in valve.OtherValveStrings)
                {
                    Valve otherValve = valves[otherValveString];
                    valve.AddOtherValve(otherValve);
                }
            }
            Valve startValve = valves["AA"];
        }
    }
    class Valve
    {
        public string Name { get; set; }
        public int Rate { get; set; }
        public List<string> OtherValveStrings { get; set; }
        public List<Valve> Valves { get; set; }
        public Valve(string name, int rate)
        {
            Name = name;
            Rate = rate;
            OtherValveStrings = new List<string>();
            Valves = new List<Valve>();
        }
        public void AddOtherValveString(string otherValveString)
        {
            OtherValveStrings.Add(otherValveString);
        }
        public void AddOtherValve(Valve otherValve)
        {
            Valves.Add(otherValve);
        }
        public override string ToString()
        {
            return Name + " rate: " + Rate;
        }
    }
}