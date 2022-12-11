using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models.Day7
{
    public class Directory : IComparable<Directory>
    {
        public Directory Parent { get; private set; }
        public string Name { get; private set; }
        public List<File> Files { get; set; } = new();
        public List<Directory> Subdirectories { get; set; } = new();
        public int Size 
        {
            get
            {
                int size = 0;

                foreach (var file in Files)
                {
                    size += file.Size;
                }

                foreach (var directory in Subdirectories)
                {
                    size += directory.Size;
                }

                return size;
            }
        }

        public Directory(Directory parent, string name)
        {
            Parent = parent;
            Name = name;
        }

        public int CompareTo(Directory? other)
        {
            if (other == null) return 1;

            return Size.CompareTo(other.Size);
        }
    }
}
