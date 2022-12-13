﻿namespace Day13
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
            List<PacketList> pairs = new List<PacketList>();
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
                                pairs.Add(packetList);
                            }
                        }
                        else
                        {
                            numberString += c;
                        }
                    }
                    continue;
                }

                // we have a pair to consider
                Console.WriteLine("Index " + count);
                int compareResult = pairs[0].Compare(pairs[1]);
                if(compareResult < 0)
                {
                    correctIndices.Add(count);
                    Console.WriteLine(" correct.");
                }
                Console.WriteLine();
                pairs.Clear();  
                count++;
            }
            int result = 0;
            foreach(int index in correctIndices)
            {
                result += index;
            }
            Console.WriteLine(result);
        }
    }
    abstract class Packet
    {
        public abstract int Compare(PacketList other);
        public abstract int Compare(PacketLeaf other);
    }
    class PacketList : Packet
    {
        public List<Packet> packets;
        public PacketList()
        {
            packets = new List<Packet>();
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