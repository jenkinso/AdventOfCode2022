using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models.Day12
{
    public class Dijkstra
    {
        public Stack<Node> ShortestPath = new();
        public int NumSteps
        {
            get
            {
                return ShortestPath.Count;
            }
        }

        private Node[] _graph;
        private int _numNodes;
        private Node _source;
        private Dictionary<Node, int> _distanceFromSource = new();
        private Dictionary<Node, Node?> _previous = new();

        public Dijkstra(Node[] graph, Node source)
        {
            _graph = graph;
            _numNodes = _graph.Length;
            _source = source;
        }

        public void RunDijkstra()
        {
            List<Node> unvisitedQueue = new();

            // Initialise values in distance and previous arrays and populate queue of unvisited nodes
            foreach (var node1 in _graph)
            {
                _distanceFromSource[node1] = node1 == _source ? 0 : int.MaxValue;
                _previous[node1] = null;
                unvisitedQueue.Add(node1);
            }

            while (unvisitedQueue.Count > 0)
            {
                Node? currentNode = FindNodeWithMinDistance(unvisitedQueue);

                if (currentNode == null)
                {
                    // all remaining nodes cannot be reached
                    break;
                }

                unvisitedQueue.Remove(currentNode);

                foreach (var neighbour in currentNode.Edges)
                {
                    int altDistance = _distanceFromSource[currentNode] + 1;

                    if (altDistance < _distanceFromSource[neighbour])
                    {
                        _distanceFromSource[neighbour] = altDistance;
                        _previous[neighbour] = currentNode;
                    }
                }
            }            
        }

        public void RunShortestPath(Node destinationNode)
        {
            while (_previous[destinationNode] != null)
            {
                destinationNode = _previous[destinationNode];
                
                ShortestPath.Push(destinationNode);
            }
        }

        private Node? FindNodeWithMinDistance(List<Node> unvisitedNodes)
        {
            Node? result = null;

            int minDistance = int.MaxValue;

            foreach (var node in unvisitedNodes)
            {
                if (_distanceFromSource[node] < minDistance)
                {
                    minDistance = _distanceFromSource[node];
                    result = node;
                }
            }

            return result;
        }
    }
}
