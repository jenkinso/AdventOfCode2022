using ConsoleApp.Models.Day12;
using System;
using System.Collections.Generic;
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

            int numSteps = Dijkstra(graph);

            return numSteps;
        }

        private static int Dijkstra(Node[] graph)
        {
            int numNodes = graph.Length;

            Dictionary<Node, int> distanceFromSource = new();
            Dictionary<Node, Node?> previous = new();
            List<Node> unvisitedQueue = new();

            // Initialise values in distance and previous arrays and populate queue of unvisited nodes
            foreach (var node1 in graph)
            {
                distanceFromSource[node1] = node1.IsSource ? 0 : int.MaxValue;
                previous[node1] = null;
                unvisitedQueue.Add(node1);
            }

            while (unvisitedQueue.Count > 0)
            {
                Node? currentNode = FindNodeWithMinDistance(unvisitedQueue, distanceFromSource);

                if (currentNode == null)
                {
                    break;
                }

                bool success = unvisitedQueue.Remove(currentNode);

                foreach (var neighbour in currentNode.Edges)
                {
                    int altDistance = distanceFromSource[currentNode] + 1;

                    if (altDistance < distanceFromSource[neighbour])
                    {
                        distanceFromSource[neighbour] = altDistance;
                        previous[neighbour] = currentNode;
                    }

                }
            }

            // Run shortest path algorithm
            int numSteps = 0;

            Node node = graph.First(node => node.IsDestination);

            while (previous[node] != null)
            {
                node = previous[node];
                numSteps++;
            }

            return numSteps;
        }

        private static Node? FindNodeWithMinDistance(List<Node> unvisitedNodes, Dictionary<Node, int> distanceFromSource)
        {
            Node? result = null;

            int minDistance = int.MaxValue;

            foreach (var node in unvisitedNodes)
            {
                if (distanceFromSource[node] < minDistance)
                {
                    minDistance = distanceFromSource[node];
                    result = node;
                }
            }

            return result;
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
    }
}
