using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models.Day9
{
    public class RopeKnot
    {
        public string Name { get; private set; }
        public int x { get; private set; }
        public int y { get; private set; }

        public RopeKnot(string name)
        {
            Name = name;
        }

        public void UpdateX(int dx)
        {
            x += dx;
        }
        
        public void UpdateY(int dy)
        {
            y += dy;
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }

    }
}
