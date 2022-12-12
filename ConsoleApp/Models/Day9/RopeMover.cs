using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models.Day9
{
    public class RopeMover
    {
        public RopeKnot Head { get; private set; } = new RopeKnot("Head");
        public RopeKnot Tail { get; private set; } = new RopeKnot("Tail");
        public int TailPositionCount
        {
            get
            {
                return _tailPositions.Count;
            }
        }

        private HashSet<(int x, int y)> _tailPositions = new();
        
        private string[] _headInstructions;

        public RopeMover(string[] headInstructions)
        {
            _headInstructions = headInstructions;

            ProcessHeadInstructions();
        }

        private void ProcessHeadInstructions()
        {
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
                            Head.UpdateX(1);
                            break;
                        case 'L':
                            Head.UpdateX(-1);
                            break;
                        case 'U':
                            Head.UpdateY(1);
                            break;
                        case 'D':
                            Head.UpdateY(-1);
                            break;
                        default:
                            break;
                    }

                    UpdateTailPosition();
                    _tailPositions.Add((Tail.x, Tail.y));
                }

                // For debugging:
                //PrintPositions();
            }
        }

        private void UpdateTailPosition()
        {
            int dx = Head.x - Tail.x;
            int dy = Head.y - Tail.y;

            int moveX = 0;
            int moveY = 0;

            if (Math.Abs(dx) > 1 && dy == 0)
            {
                // Move horizontally
                moveX = dx > 0 ? 1 : -1;
                Tail.UpdateX(moveX);
            }
            else if (dx == 0 && Math.Abs(dy) > 1)
            {
                // Move vertically
                moveY = dy > 0 ? 1 : -1;
                Tail.UpdateY(moveY);
            }
            else if ((Math.Abs(dx) > 1 && Math.Abs(dy) == 1) || (Math.Abs(dx) == 1 && Math.Abs(dy) > 1))
            {
                // Move diagonally
                moveX = dx > 0 ? 1 : -1;
                Tail.UpdateX(moveX);
                moveY = dy > 0 ? 1 : -1;
                Tail.UpdateY(moveY);
            }
            else
            {
                // No move required
            }
        }

        private void PrintPositions()
        {
            Console.WriteLine($"Head = {Head.ToString()}. Tail = {Tail.ToString()}.");
        }
    }
}
