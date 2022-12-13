using ConsoleApp.Models.Day9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Puzzles
{
    public static class Day09RopeBridge
    {
        private const string DataFile = @"data\day09.txt";
        private const string PracticeFile = @"data\day09Practice.txt";
        private const string PracticeFile2 = @"data\day09Practice2.txt";

        public static int Part1()
        {
            string[] data = File.ReadAllLines(DataFile);

            RopeMover ropeMover = new RopeMover(data);

            int tailPositionCount = ropeMover.TailPositionCount;

            return tailPositionCount;
        }

        public static int Part2()
        {
            string[] data = File.ReadAllLines(DataFile);

            RopeMoverMultiKnot ropeMover = new RopeMoverMultiKnot(data, numKnots: 10);

            int tailPositionCount = ropeMover.TailPositionCount;

            return tailPositionCount;
        }
    }
}
