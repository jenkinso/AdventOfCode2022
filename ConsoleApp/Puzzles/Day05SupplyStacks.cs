using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Puzzles
{
    public static class Day05SupplyStacks
    {
        private const string DataFile = @"data\day05.txt";
        private const string PracticeFile = @"data\day05Practice.txt";

        public static string Part1()
        {
            string[] data = File.ReadAllLines(DataFile);

            int indexStartInstructions = GetLineIndexAtStartInstructions(data);
            int indexStackLabels = indexStartInstructions - 2;
            int maxInitialHeightStacks = indexStackLabels;

            string[] labelsArray = data[indexStackLabels].Trim().Split("   ");

            int numStacks = labelsArray.Length;

            var stacksList = GetListOfEmptyStacks(numStacks);

            InitialiseStacks(maxInitialHeightStacks, numStacks, data, stacksList);

            ProcessInstructions(indexStartInstructions, data, stacksList);

            var message = GetMessageForElves(stacksList);

            return message;
        }

        public static string Part2()
        {
            string[] data = File.ReadAllLines(DataFile);

            int indexStartInstructions = GetLineIndexAtStartInstructions(data);
            int indexStackLabels = indexStartInstructions - 2;
            int maxInitialHeightStacks = indexStackLabels;

            string[] labelsArray = data[indexStackLabels].Trim().Split("   ");

            int numStacks = labelsArray.Length;

            var stacksList = GetListOfEmptyStacks(numStacks);

            InitialiseStacks(maxInitialHeightStacks, numStacks, data, stacksList);

            ProcessInstructionsPart2(indexStartInstructions, data, stacksList);

            var message = GetMessageForElves(stacksList);

            return message;
        }

        private static int GetLineIndexAtStartInstructions(string[] data)
        {
            int lineIndex = 0;

            for (int line = 0; line < data.Length; line++)
            {
                if (data[line] == string.Empty)
                {
                    // instructions start on the line after the blank
                    lineIndex = line + 1;
                }
            }

            return lineIndex;
        }

        private static List<Stack<char>> GetListOfEmptyStacks(int numStacks)
        {
            List<Stack<char>> stacksList = new List<Stack<char>>();

            for (int i = 0; i < numStacks; i++)
            {
                stacksList.Add(new Stack<char>());
            }

            return stacksList;
        }

        private static void InitialiseStacks(int maxInitialHeightStacks, int numStacks, string[] data, List<Stack<char>> stacksList)
        {
            const int StackSpacing = 4;

            // Iterate through the stacks bottom to top
            for (int lineIndex = maxInitialHeightStacks - 1; lineIndex >= 0; lineIndex--)
            {
                for (int stack = 0; stack < numStacks; stack++)
                {
                    int stackColumnIndex = stack * StackSpacing + 1;

                    char crate = data[lineIndex][stackColumnIndex];

                    if (crate != ' ')
                    {
                        stacksList[stack].Push(crate);
                    }
                }
            }
        }

        private static void ProcessInstructions(int indexStartInstructions, string[] data, List<Stack<char>> stacksList)
        {
            for (int lineIndex = indexStartInstructions; lineIndex < data.Length; lineIndex++)
            {
                string[] instruction = data[lineIndex]
                    .Replace("move ", string.Empty)
                    .Replace("from ", string.Empty)
                    .Replace("to ", string.Empty)
                    .Split(' ');

                int quantity = int.Parse(instruction[0]);
                int fromStack = int.Parse(instruction[1]);
                int toStack = int.Parse(instruction[2]);

                for (int q = 0; q < quantity; q++)
                {
                    var crateRemoved = stacksList[fromStack - 1].Pop();

                    stacksList[toStack - 1].Push(crateRemoved);
                }
            }
        }

        private static void ProcessInstructionsPart2(int indexStartInstructions, string[] data, List<Stack<char>> stacksList)
        {
            for (int lineIndex = indexStartInstructions; lineIndex < data.Length; lineIndex++)
            {
                string[] instruction = data[lineIndex]
                    .Replace("move ", string.Empty)
                    .Replace("from ", string.Empty)
                    .Replace("to ", string.Empty)
                    .Split(' ');

                int quantity = int.Parse(instruction[0]);
                int fromStack = int.Parse(instruction[1]);
                int toStack = int.Parse(instruction[2]);

                Stack<char> intermediateStack = new();

                for (int q = 0; q < quantity; q++)
                {
                    intermediateStack.Push(stacksList[fromStack - 1].Pop());
                }

                for (int q = 0; q < quantity; q++)
                {
                    stacksList[toStack - 1].Push(intermediateStack.Pop());
                }
            }
        }

        private static string GetMessageForElves(List<Stack<char>> stacksList)
        {
            string message = string.Empty;

            for (int stack = 0; stack < stacksList.Count; stack++)
            {
                message += stacksList[stack].Peek().ToString();
            }

            return message;
        }
    }
}
