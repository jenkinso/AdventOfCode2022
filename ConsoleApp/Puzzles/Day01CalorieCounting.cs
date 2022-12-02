using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Puzzles
{
    public static class Day01CalorieCounting
    {
        private const string DataFile = @"data\day01.txt";
        private const string PracticeFile = @"data\day01Practice.txt";

        public static int Part1()
        {
            int[] calorieCounts = GetCalorieCountPerElf(DataFile);

            return GetMaxValue(calorieCounts);
        }

        public static int Part2()
        {
            int[] calorieCounts = GetCalorieCountPerElf(DataFile);

            SortArray(calorieCounts);

            int answer = calorieCounts[0] + calorieCounts[1] + calorieCounts[2];

            return answer;
        }

        private static int[] GetCalorieCountPerElf(string filepath)
        {
            string[] lines = File.ReadAllLines(filepath);

            List<int> calorieCounts = new();

            int calorieCount = 0;

            foreach (var item in lines)
            {
                if (item == string.Empty)
                {
                    calorieCounts.Add(calorieCount);
                    calorieCount = 0;

                    continue;
                }

                calorieCount += int.Parse(item);
            }

            // add the last elf's count
            calorieCounts.Add(calorieCount);

            return calorieCounts.ToArray();
        }

        private static int GetMaxValue(int[] numbers)
        {
            int maxValue = int.MinValue;

            foreach (var item in numbers)
            {
                if (item > maxValue)
                {
                    maxValue = item;
                }
            }

            return maxValue;
        }

        private static void SortArray(int[] numbers)
        {
            int tempValue = 0;
            int swapIndex = 0;

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                int maxValue = numbers[i];

                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (numbers[j] > maxValue)
                    {
                        maxValue = numbers[j];
                        swapIndex = j;
                    }
                }

                if (maxValue > numbers[i])
                {
                    tempValue = numbers[i];
                    numbers[i] = maxValue;
                    numbers[swapIndex] = tempValue;
                }
            }
        }
    }
}
