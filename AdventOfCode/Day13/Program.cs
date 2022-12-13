namespace Day13
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
            List<Packet> packets = new List<Packet>();
            List<int> correctIndices = new List<int>();
            int count = 1;
            while (!complete)
            {
                string input = Console.ReadLine();
                if (input == null)
                {
                    complete = true;                    
                }
                else if(input.Length != 0)
                {
                    Stack<PacketList> stack = new Stack<PacketList>();
                    string numberString = "";
                    foreach(char c in input)
                    {
                        if (c == '[')
                        {
                            PacketList packetList = new PacketList();
                            if(stack.Count > 0)
                            {
                                stack.Peek().AddPacket(packetList);
                            }
                            stack.Push(packetList);
                        }
                        else if(c == ',')
                        {
                            if(numberString.Length > 0)
                            {
                                int number = int.Parse(numberString);
                                numberString = "";
                                PacketLeaf leaf = new PacketLeaf(number);
                                stack.Peek().AddPacket(leaf);
                            }
                        }
                        else if(c == ']')
                        {
                            if (numberString.Length > 0)
                            {
                                int number = int.Parse(numberString);
                                numberString = "";
                                PacketLeaf leaf = new PacketLeaf(number);
                                stack.Peek().AddPacket(leaf);
                            }
                            PacketList packetList = stack.Pop();
                            if(stack.Count == 0)
                            {
                                packets.Add(packetList);
                            }
                        }
                        else
                        {
                            numberString += c;
                        }
                    }
                    continue;
                }                
            }
            PacketList dividerList1 = new PacketList();
            PacketLeaf dividerLeaf1 = new PacketLeaf(2);
            dividerList1.AddPacket(dividerLeaf1);
            PacketList divider1 = new PacketList();
            divider1.AddPacket(dividerList1);
            divider1.IsDivider = true;
            packets.Add(divider1);

            PacketList dividerList2 = new PacketList();
            PacketLeaf dividerLeaf2 = new PacketLeaf(6);
            dividerList2.AddPacket(dividerLeaf2);
            PacketList divider2 = new PacketList();
            divider2.AddPacket(dividerList2);
            divider2.IsDivider = true;
            packets.Add(divider2);

            packets.Sort();
            int packetCount = 1;
            int result = 1;
            foreach(Packet packet in packets)
            {
                Console.WriteLine(packet.ToString());
                if(packet.IsDivider)
                {
                    result *= packetCount;
                }
                packetCount++;
            }
            Console.WriteLine(result.ToString());
        }
    }
    abstract class Packet : IComparable<Packet>
    {
        public bool IsDivider { get; set; }
        public abstract int Compare(PacketList other);
        public abstract int Compare(PacketLeaf other);

        public int CompareTo(Packet? other)
        {
            int compareResult = 0;
            if (other is PacketList)
            {
                compareResult = Compare(other as PacketList);
            }
            else
            {
                compareResult = Compare(other as PacketLeaf);
            }
            return compareResult;
        }
    }
    class PacketList : Packet
    {
        public List<Packet> packets;
        public PacketList()
        {
            packets = new List<Packet>();
            IsDivider = false;
        }
        public void AddPacket(Packet packet)
        {
            packets.Add(packet);
        }

        public override int Compare(PacketList other)
        {
            Console.WriteLine("Compare " + ToString() + " vs " + other.ToString());
            int numPackets= packets.Count;
            int numOtherPackets = other.packets.Count;
            if (numPackets > 0)
            {
                if (numOtherPackets == 0)
                {
                    Console.WriteLine("Right side ran out of items, so inputs are not in the right order");
                    return 1;
                }
                else
                {
                    int i = 0;
                    for (; i < packets.Count; i++)
                    {
                        Packet packet = packets[i];
                        if(i < numOtherPackets)
                        {
                            Packet otherPacket = other.packets[i];
                            int compareResult = 0;
                            if (otherPacket is PacketLeaf)
                            {
                                compareResult = packet.Compare(otherPacket as PacketLeaf);
                            }
                            else
                            {
                                compareResult = packet.Compare(otherPacket as PacketList);
                            }                            
                            if(compareResult != 0)
                            {
                                return compareResult;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Right side ran out of items, so inputs are not in the right order");
                            return 1;
                        }
                    }
                    i--;
                    if(i < numOtherPackets-1)
                    {
                        Console.WriteLine("Left side ran out of items, so inputs are in the right order");
                        return -1;
                    }
                }                
            }
            else
            {
                // run out of left packets
                if(numOtherPackets > 0)
                {
                    // still have right packets
                    Console.WriteLine("Left side ran out of items, so inputs are in the right order");
                    return -1;
                }
                else
                {
                    // no right packets either (equal)
                    return 0;
                }
            }
            return 0;
        }

        public override int Compare(PacketLeaf other)
        {
            Console.WriteLine("Mixed types; convert right to [" + other.Value + "] and retry comparison");
            PacketList otherPacketList = new PacketList();
            otherPacketList.AddPacket(other);
            int compareResult = Compare(otherPacketList);
            return compareResult;
        }

        public override string ToString()
        {
            string returnString = "[";
            bool includeComma = false;
            foreach(Packet packet in packets)
            {
                if(!includeComma)
                {
                    includeComma = true;
                }
                else
                {
                    returnString += ",";
                }
                returnString += packet.ToString();
            }
            returnString += "]";
            return returnString;
        }
    }
    class PacketLeaf : Packet
    {
        public int Value { get; set; }
        public PacketLeaf(int value)
        {
            Value = value;
            IsDivider = false;
        }
        public override string ToString()
        {
            return Value.ToString();
        }
        public override int Compare(PacketList other)
        {
            Console.WriteLine("Mixed types; convert left to [" + Value + "] and retry comparison");
            PacketList thisPacketList = new PacketList();
            thisPacketList.AddPacket(this);
            int compareResult = thisPacketList.Compare(other);
            return compareResult;
        }

        public override int Compare(PacketLeaf other)
        {
            Console.WriteLine("Compare " + Value + " vs " + other.Value);
            return Value - other.Value;
        }
    }
}