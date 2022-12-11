using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models.Day7
{
    public class File
    {
        public string Name { get; private set; }
        public int Size { get; private set; }

        public File(string name, int size)
        {
            Name = name;
            Size = size;
        }
    }
}
