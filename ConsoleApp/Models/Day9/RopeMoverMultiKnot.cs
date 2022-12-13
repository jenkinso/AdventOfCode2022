using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models.Day9
{
    public class RopeMoverMultiKnot
    {
        public List<RopeKnot> RopeKnots { get; private set; } = new();
        
        public int TailPositionCount
        {
            get
            {
                return _tailPositions.Count;
            }
        }

        private HashSet<(int x, int y)> _tailPositions = new();
        private string[] _headInstructions;
        private readonly RopeKnot _head;
        private readonly RopeKnot _tail;
        private readonly int _numKnots;

        public RopeMoverMultiKnot(string[] headInstructions, int numKnots)
        {
            _headInstructions = headInstructions;

            for (int i = 0; i < numKnots; i++)
            {
                RopeKnots.Add(new RopeKnot(i.ToString()));
                //RopeKnots[i].UpdateX(11);
                //RopeKnots[i].UpdateY(5);
            }

            _numKnots = numKnots;
            _head = RopeKnots[0];
            _tail = RopeKnots[numKnots - 1];

            ProcessHeadInstructions();
        }

        private void ProcessHeadInstructions()
        {
            //Console.WriteLine("Initial: ");
            //PrintGrid(26);

            foreach (var line in _headInstructions)
            {
                string[] parts = line.Split(' ');
                char direction = parts[0][0];
                int amount = int.Parse(parts[1]);

                for (int a = 0; a < amount; a++)
                {
                    switch (direction)
                    {
                        case 'R':
                            _head.UpdateX(1);
                            break;
                        case 'L':
                            _head.UpdateX(-1);
                            break;
                        case 'U':
                            _head.UpdateY(1);
                            break;
                        case 'D':
                            _head.UpdateY(-1);
                            break;
                        default:
                            break;
                    }

                    if (_numKnots > 1)
                    {
                        for (int knot = 1; knot < _numKnots; knot++)
                        {
                            UpdateKnotPosition(RopeKnots[knot], RopeKnots[knot - 1]);
                        }
                    }

                    _tailPositions.Add((_tail.x, _tail.y));
                    //PrintGrid(6);
                    //Console.WriteLine();
                }

                // For debugging:
                //PrintPositions();
                //Console.WriteLine("\n Step = {0} \n", line);
                //PrintGrid(26);
                //Console.WriteLine();
            }
        }

        private void UpdateKnotPosition(RopeKnot followingKnot, RopeKnot leadingKnot)
        {
            int dx = leadingKnot.x - followingKnot.x;
            int dy = leadingKnot.y - followingKnot.y;

            int moveX = 0;
            int moveY = 0;

            if (Math.Abs(dx) > 1 && dy == 0)
            {
                // Move horizontally
                moveX = dx > 0 ? 1 : -1;
                followingKnot.UpdateX(moveX);
            }
            else if (dx == 0 && Math.Abs(dy) > 1)
            {
                // Move vertically
                moveY = dy > 0 ? 1 : -1;
                followingKnot.UpdateY(moveY);
            }
            else if ((Math.Abs(dx) > 1 && Math.Abs(dy) > 0) || (Math.Abs(dx) > 0 && Math.Abs(dy) > 1))
            {
                // Move diagonally
                moveX = dx > 0 ? 1 : -1;
                followingKnot.UpdateX(moveX);
                moveY = dy > 0 ? 1 : -1;
                followingKnot.UpdateY(moveY);
            }
            else
            {
                // No move required
            }
        }

        private void PrintPositions()
        {
            Console.WriteLine($"Head = {_head.ToString()}. Tail = {_tail.ToString()}.");
        }

        private void PrintGrid(int sideLength)
        {
            for (int y = 0; y < sideLength; y++)
            {
                for (int x = 0; x < sideLength; x++)
                {
                    var printSymbol = ".";

                    for (int knot = 0; knot < _numKnots; knot++)
                    {
                        if (RopeKnots[knot].x == x && RopeKnots[knot].y == (sideLength - 1 - y))
                        {
                            printSymbol = RopeKnots[knot].Name;
                            break;
                        }
                    }

                    Console.Write(printSymbol);
                }

                Console.Write("\n");
            }
        }
    }
}
