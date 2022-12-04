using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Puzzles
{
    public static class Day04CampCleanup
    {
        private const string DataFile = @"data\day04.txt";
        private const string PracticeFile = @"data\day04Practice.txt";

        public static int Part1()
        {
            var answers = SolveTheProblem();

            return answers.containCount;
        }

        public static int Part2()
        {
            var answers = SolveTheProblem();

            return answers.overlapCount;
        }

        private static (int containCount, int overlapCount) SolveTheProblem()
        {
            string[] data = File.ReadAllLines(DataFile);

            (int containCount, int overlapCount) answers = (0, 0);

            foreach (var line in data)
            {
                string[] pairs = line.Split(',');
                string[] pair1 = pairs[0].Split('-');
                string[] pair2 = pairs[1].Split('-');
                int p1L = int.Parse(pair1[0]);
                int p1U = int.Parse(pair1[1]);
                int p2L = int.Parse(pair2[0]);
                int p2U = int.Parse(pair2[1]);

                if ((p1L >= p2L && p1U <= p2U) || (p2L >= p1L && p2U <= p1U))
                {
                    answers.containCount++;
                }

                bool isRange1Lower = p1U < p2L;
                bool isRange1Higher = p1L > p2U;

                if (!(isRange1Lower || isRange1Higher))
                {
                    answers.overlapCount++;
                }
            }

            return answers;
        }
    }
}
