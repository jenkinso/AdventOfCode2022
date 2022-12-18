using ConsoleApp.Models.Day12;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Puzzles
{
    public static class Day12HillClimbingAlgorithm
    {

        private const string DataFile = @"data\day12.txt";
        private const string PracticeFile = @"data\day12Practice.txt";

        public static int Part1()
        {
            string[] data = File.ReadAllLines(DataFile);

            Node[] graph = GenerateGraph(data);

            Node destination = graph.First(node => node.IsDestination);
            Node source = graph.First(node => node.IsSource);

            Dijkstra dijkstra = new(graph, source);            

            dijkstra.RunDijkstra();
            dijkstra.RunShortestPath(destination);

            int numSteps = dijkstra.NumSteps;

            return numSteps;
        }

        public static int Part2()
        {
            string[] data = File.ReadAllLines(DataFile);

            Node[] graph = GenerateGraph(data);

            Node destination = graph.First(node => node.IsDestination);

            List<Node> possibleStartingNodes = graph.Where(node => node.ElevationLetter == 'a').ToList();

            int lowestNumSteps = int.MaxValue;
            Node bestStartingNode;
            int nodeNum = 0;

            foreach (var source in possibleStartingNodes)
            {
                nodeNum++;
                Dijkstra dijkstra = new(graph, source);
                dijkstra.RunDijkstra();
                dijkstra.RunShortestPath(destination);

                if (dijkstra.PathFound && dijkstra.NumSteps < lowestNumSteps)
                {
                    lowestNumSteps = dijkstra.NumSteps;
                    bestStartingNode = source;
                }

                Console.WriteLine("Tested node " + nodeNum + ". Lowest num steps = " + lowestNumSteps + ".");
            }

            return lowestNumSteps;
        }

        private static Node[] GenerateGraph(string[] data)
        {
            int numRows = data.Length;
            int numCols = data[0].Length;

            Node[,] graph = new Node[numRows, numCols];

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    graph[row, col] = new Node(data[row][col], row, col);
                }
            }

            AddEdgesToAllNodes(graph);

            return GetOneDimensionalGraph(graph);
        }

        private static void AddEdgesToAllNodes(Node[,] graph)
        {
            int numRows = graph.GetLength(0);
            int numCols = graph.GetLength(1);

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    // Check neighbouring nodes: Up, Down, Left and Right
                    if (row - 1 >= 0) graph[row, col].CheckForAddEdge(graph[row - 1, col]);
                    if (row + 1 < numRows) graph[row, col].CheckForAddEdge(graph[row + 1, col]);
                    if (col - 1 >= 0) graph[row, col].CheckForAddEdge(graph[row, col - 1]);
                    if (col + 1 < numCols) graph[row, col].CheckForAddEdge(graph[row, col + 1]);
                }
            }
        }

        private static Node[] GetOneDimensionalGraph(Node[,] graph)
        {
            Node[] oneDimGraph = new Node[graph.Length];

            int index = 0;

            for (int row = 0; row < graph.GetLength(0); row++)
            {
                for (int col = 0; col < graph.GetLength(1); col++)
                {
                    oneDimGraph[index] = graph[row, col];
                    index++;
                }
            }

            return oneDimGraph;
        }
    }
}
