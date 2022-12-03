using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Puzzles
{
    public static class Day03RucksackReorganization
    {
        private const string DataFile = @"data\day03.txt";
        private const string PracticeFile = @"data\day03Practice.txt";

        public static int Part1()
        {
            string[] data = File.ReadAllLines(DataFile);

            List<char> duplicateItems = new();

            int prioritiesSum = 0;

            foreach (var rucksack in data)
            {
                int numItems = rucksack.Length;

                var firstCompartment = PackACompartment(rucksack, 0, numItems / 2 - 1);
                
                FindDuplicatesInOtherCompartment(firstCompartment, rucksack, numItems / 2, numItems - 1, duplicateItems);

                prioritiesSum = CalculatePrioritiesSum(duplicateItems);
            }

            return prioritiesSum;
        }

        private static HashSet<char> PackACompartment(string items, int lowerIndex, int upperIndex)
        {
            HashSet<char> compartment = new();

            for (int i = lowerIndex; i <= upperIndex; i++)
            {
                compartment.Add(items[i]);
            }

            return compartment;
        }

        private static void FindDuplicatesInOtherCompartment(HashSet<char> firstCompartment, string items, int lowerIndex, int upperIndex, List<char> duplicates)
        {
            for (int i = lowerIndex; i <= upperIndex; i++)
            {
                if (firstCompartment.Contains(items[i]))
                {
                    duplicates.Add(items[i]);
                    break;
                }
            }
        }

        private static int CalculatePrioritiesSum(List<char> duplicates)
        {
            int sum = 0;

            foreach (char item in duplicates)
            {
                int priority = item.ToPriorityNumber();

                sum += priority;
            }

            return sum;
        }
    }

    public static class Extensions
    {
        public static int ToPriorityNumber(this char character)
        {
            int asciiNumber = (int)character;
            
            bool isLowerCase = asciiNumber > 96;

            int priorityNumber = isLowerCase ? asciiNumber - 96 : asciiNumber - 38;

            return priorityNumber;
        }
    }

    
}
