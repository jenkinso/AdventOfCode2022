using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models.Day12
{
    public class Node
    {
        public char ElevationLetter { get; private set; }
        public int ElevationNum { get; private set; }
        public bool IsSource { get; set; }
        public bool IsDestination { get; set; }
        public int Row { get; private set; }
        public int Column { get; private set; }
        public List<Node> Edges { get; private set; } = new();

        private const int AsciiOffset = -96;
        private const char StartElevationLetter = 'a';
        private const char EndElevationLetter = 'z';
        private const int MaxHeightDifferenceConnectedNodes = 1;

        public Node(char elevationLetter, int row, int column)
        {
            ElevationLetter = elevationLetter;
            Row = row;
            Column = column;

            if (ElevationLetter == 'S')
            {
                ElevationLetter = StartElevationLetter;
                IsSource = true;
            }
            else if (ElevationLetter == 'E')
            {
                ElevationLetter = EndElevationLetter;
                IsDestination = true;
            }

            ElevationNum = (int)ElevationLetter + AsciiOffset;
        }

        public void CheckForAddEdge(Node otherNode)
        {
            if (otherNode.ElevationNum - this.ElevationNum <= MaxHeightDifferenceConnectedNodes)
            {
                Edges.Add(otherNode);
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;

            Node other = (Node)obj;

            if (other.Row == this.Row && other.Column == this.Column)
            {
                return true;
            }

            return false;
        }
    }
}
