using ConsoleApp.Models.Day11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Puzzles
{
    public static class Day11MonkeyInTheMiddle
    {
        private const string DataFile = @"data\day11.txt";
        private const string PracticeFile = @"data\day11Practice.txt";

        public static int Part1()
        {
            string data = File.ReadAllText(PracticeFile);

            MonkeyBusiness mb = new MonkeyBusiness(data);

            mb.StartMonkeyBusiness(numRounds: 10000);

            int answer = mb.GetMonkeyBusinessLevel();

            return answer;
        }
    }
}
