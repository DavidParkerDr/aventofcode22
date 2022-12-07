using System.IO;
using static System.Net.WebRequestMethods;

namespace Day7
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
            Directory rootDirectory = new Directory("Root");
            Stack<Directory> stack = new Stack<Directory>();
            List<Directory> justRight = new List<Directory>();
            stack.Push(rootDirectory);
            int totalFileStorage = 70000000;
            int unusedSpaceThreshold = 30000000;
            while (!complete)
            {
                string input = Console.ReadLine();
                if (input == null)
                {
                    complete = true;
                    continue;
                }
                string[] inputs = input.Split(' ');
                if (inputs[0] == "$")
                {
                    // command
                    string command = inputs[1];
                    if(command == "cd")
                    {
                        string parameter = inputs[2];
                        if(parameter == "..")
                        {
                            // do a count of children
                            Directory currentDirectory = stack.Peek();
                            int size = currentDirectory.CalculateSize();
                            stack.Pop();
                            if(size < 100000)
                            {
                                justRight.Add(currentDirectory);
                            }
                        }
                        else if(parameter == "/")
                        {
                            // back to root
                            while(stack.Count > 1)
                            {
                                stack.Pop();
                            }
                        }
                        else
                        {
                            // go down a directory
                            Directory childDirectory = (Directory)stack.Peek().GetChild(parameter);
                            if(childDirectory != null)
                            {
                                stack.Push(childDirectory);
                            }
                            else
                            {
                                // something went wrong here
                                Console.WriteLine("ERROR - Directory not found");
                            }                            
                        }
                    }
                    else if (command == "ls")
                    {
                        // do nothing
                    }
                    else
                    {
                        //something went wrong here
                        Console.WriteLine("ERROR - Unknown Command");
                    }
                }
                else
                {
                    Directory currentDirectory = stack.Peek();
                    // result
                    File newFile;
                    if(inputs[0] == "dir")
                    {
                        //directory
                        newFile = new Directory(inputs[1]);
                    }
                    else
                    {
                        // file
                        newFile = new File(inputs[1]);
                        newFile.Size = int.Parse(inputs[0]);
                    }
                    currentDirectory.AddFile(newFile);
                }
            }
            while (stack.Count > 0)
            {
                Directory currentDirectory = stack.Peek();
                int size = currentDirectory.CalculateSize();
                stack.Pop();
            }
            rootDirectory.OutputToConsole();
            Console.WriteLine("Root Size: " + rootDirectory.Size);
            int unusedSpace = totalFileStorage - rootDirectory.Size;
            Console.WriteLine("Unused Space: " + unusedSpace);
            int spaceNeeded = unusedSpaceThreshold - unusedSpace;
            Console.WriteLine("Space Needed: " + spaceNeeded);
            Directory smallestDirectory = rootDirectory;
            rootDirectory.FindSmallestDirectoryAboveThreshold(spaceNeeded, ref smallestDirectory);
            Console.WriteLine(smallestDirectory.Name + " " + smallestDirectory.Size);
        }
    }
    class Directory : File
    {
        List<File> files;
        public Directory(string name) : base(name)
        {
            files = new List<File>();
        }
        public override void OutputToConsole(int indent = 0)
        {
            for(int i = 0; i < indent; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine(ToString());
            foreach(File child in files)
            {
                child.OutputToConsole(indent + 3);
            }
        }
        public override string ToString()
        {
            return Name + " (dir, size = " + Size + ")";
        }
        public void AddFile(File file)
        {
            files.Add(file);
        }
        public int CalculateSize()
        {
            Size = 0;
            foreach(File child in files)
            {
                Size += child.Size;
            }
            return Size;
        }
        public File GetChild(string name)
        {
            foreach(File child in files)
            {
                if(child.Name == name)
                {
                    return child;
                }
            }
            return null;
        }
        public void FindSmallestDirectoryAboveThreshold(int threshold, ref Directory smallestDirectory)
        {
            if (Size > threshold)
            {
                if(Size < smallestDirectory.Size)
                {
                    smallestDirectory = this;
                }
            }
            foreach(File child in files)
            {
                if (child is Directory)
                {
                    Directory directory = (Directory)child;
                    directory.FindSmallestDirectoryAboveThreshold(threshold, ref smallestDirectory);
                }
            }
        }
    }
    class File
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public File(string name)
        {
            Size = 0;
            Name = name;
        }
        public virtual void OutputToConsole(int indent = 0)
        {
            for (int i = 0; i < indent; i++)
            {
                Console.Write(" ");
            }
            Console.WriteLine(ToString());
            
        }
        public override string ToString()
        {
            return Name + " (file, size = " + Size + ")";
        }
    }
}