using ConsoleApp.Puzzles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models.Day3
{
    public class ElfGroup
    {
        public char BadgeType { get; private set; }
        public int PriorityNumber
        {
            get
            {
                return BadgeType.ToPriorityNumber();
            }
        }

        private List<HashSet<char>> _rucksacksItemsSet = new();
        private string[] _ruckSacks;
        private int _groupSize;

        public ElfGroup(string[] rucksacks, int groupSize)
        {
            if (groupSize != rucksacks.Length) throw new ArgumentException("Group size and number of rucksacks don't match.");

            _ruckSacks = rucksacks;
            _groupSize = groupSize;            

            CountItems();
            FindBadgeType();
        }

        private void CountItems()
        {
            foreach (string rucksack in _ruckSacks)
            {
                var itemsSet = new HashSet<char>();
                
                _rucksacksItemsSet.Add(itemsSet);

                foreach (char item in rucksack)
                {
                    itemsSet.Add(item);
                }
            }
        }

        private void FindBadgeType()
        {
            // Create a copy of the first rucksack, just so that its original data is unmodified and available for debugging
            HashSet<char> firstRucksack = new HashSet<char>(_rucksacksItemsSet[0]);

            foreach (var rucksack in _rucksacksItemsSet)
            {
                firstRucksack.IntersectWith(rucksack);
            }

            if (firstRucksack.Count() != 1) throw new Exception("There should be a single common item between the rucksacks!");

            foreach (var item in firstRucksack)
            {
                BadgeType = item;
            }
        }
    }
}
