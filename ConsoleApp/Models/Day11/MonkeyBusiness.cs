using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models.Day11
{
    public class MonkeyBusiness
    {
        public List<Monkey> Monkeys { get; private set; } = new();

        public MonkeyBusiness(string data)
        {
            string[] monkeyDefinitions = data.Split("\r\n\r\n");

            for (int monkey = 0; monkey < monkeyDefinitions.Length; monkey++)
            {
                Monkeys.Add(new Monkey(monkey, monkeyDefinitions[monkey], Monkeys));
            }
        }

        public void StartMonkeyBusiness(int numRounds)
        {
            for (int round = 1; round <= numRounds; round++)
            {
                Console.WriteLine($"Round {round}\n\n");
                Monkeys.ForEach(monkey => monkey.TakeATurn());
                //Console.WriteLine($"End of Round {round} summary: ");
                //Monkeys.ForEach(monkey => Console.WriteLine(monkey.ToString()));
            }
        }

        public int GetMonkeyBusinessLevel()
        {
            Monkeys.Sort();

            return Monkeys[0].InspectionCount * Monkeys[1].InspectionCount;
        }
    }
}
