using ConsoleApp.Models.Day10;
using ConsoleApp.Models.Day9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Puzzles
{
    public static class Day10CathodeRayTube
    {
        private const string DataFile = @"data\day10.txt";
        private const string PracticeFile = @"data\day10Practice.txt";
        private const string PracticeFile2 = @"data\day10Practice2.txt";

        public static int Part1()
        {
            string[] data = File.ReadAllLines(DataFile);

            CPU cpu = new(data);

            cpu.RunProgram();
            
            return cpu.SumInterestingSignalStrengths;
        }
    }
}
