using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Models.Day7;
using Directory = ConsoleApp.Models.Day7.Directory;
using File = ConsoleApp.Models.Day7.File;

namespace ConsoleApp.Puzzles
{
    public static class Day07NoSpaceLeftOnDevice
    {
        private const string DataFile = @"data\day07.txt";
        private const string PracticeFile = @"data\day07Practice.txt";

        public static int Part1()
        {
            string[] data = System.IO.File.ReadAllLines(DataFile);

            var graphBuilder = new DirectoryGraphBuilder(data);
            
            Directory rootNode = graphBuilder.Build();

            List<Directory> directories = FindDirectoriesWithMaxSize(100000, rootNode);

            int sumOfSizes = 0;

            directories.ForEach(dir => sumOfSizes += dir.Size);
            
            return sumOfSizes;
        }

        private static List<Directory> FindDirectoriesWithMaxSize(int maxSize, Directory rootNode)
        {
            List<Directory> result = new List<Directory>();

            // Collections of visited and queued nodes
            List<Directory> visited = new();
            Queue<Directory> queue = new();

            // Initial step for a Breadth First Search of the graph of directories: add the starting node
            visited.Add(rootNode);
            queue.Enqueue(rootNode);


            // Carry out BFS
            while (queue.Count > 0)
            {
                Directory currentDirectory = queue.Dequeue();

                foreach (Directory directory in currentDirectory.Subdirectories)
                {
                    if (!visited.Contains(directory))
                    {
                        visited.Add(directory);
                        queue.Enqueue(directory);

                        if (directory.Size <= maxSize)
                        {
                            result.Add(directory);
                        }
                    }
                }
            }

            return result;
        }

        private static void TestingCode()
        {
            Directory root = new Directory(null, "\\");
            File b = new File("b.txt", 14848514);
            root.Files.Add(b);
            File c = new File("c.dat", 8504156);
            root.Files.Add(c);

            Directory a = new Directory(root, "a");
            root.Subdirectories.Add(a);
            File f = new File("f", 29116);
            a.Files.Add(f);
            File g = new File("g", 2557);
            a.Files.Add(g);
            File h = new File("h.lst", 62596);
            a.Files.Add(h);

            Directory e = new Directory(a, "e");
            a.Subdirectories.Add(e);
            File i = new File("i", 584);
            e.Files.Add(i);

            Directory d = new Directory(root, "d");
            root.Subdirectories.Add(d);
            File j = new File("j", 4060174);
            d.Files.Add(j);
            File dlog = new File("d.log", 8033020);
            d.Files.Add(dlog);
            File dext = new File("d.ext", 5626152);
            d.Files.Add(dext);
            File k = new File("k", 7214296);
            d.Files.Add(k);

            int sizeOfE = e.Size;
            int sizeOfA = a.Size;
            int sizeOfD = d.Size;
            int sizeOfRoot = root.Size;
        }
    }
}
