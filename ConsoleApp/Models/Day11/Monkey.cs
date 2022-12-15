using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models.Day11
{
    public class Monkey : IComparable<Monkey>
    {
        public int Number { get; private set; }
        public Queue<long> Items { get; private set; } = new();
        public int InspectionCount { get; private set; } = 0;

        private long _coefficientA;
        private long _coefficientB;
        private long _constantC;
        private int _testDivisor;
        private const int WorryLevelDivisor = 3;
        private readonly List<Monkey> _monkeyList;
        private int _monkeyNumberWhenTestTrue;
        private int _monkeyNumberWhenTestFalse;

        public Monkey(int monkeyNumber, string definition, List<Monkey> monkeyList)
        {
            Number = monkeyNumber;

            _monkeyList = monkeyList;

            ParseDefinition(definition);
        }

        public void TakeATurn()
        {
            //Console.WriteLine($"Monkey {Number}\n\n");

            while (Items.Count > 0)
            {
                InspectAnItem();
            }
        }

        private void InspectAnItem()
        {
            InspectionCount++;

            long itemWorryLevel = Items.Dequeue();
            //Console.WriteLine($"Monkey inspects an item with a worry level of {itemWorryLevel}.");

            itemWorryLevel = OperationCalcWorryLevel(itemWorryLevel);
            //Console.WriteLine($"Worry level becomes {itemWorryLevel}");

            itemWorryLevel /= WorryLevelDivisor;
            //Console.WriteLine($"Worry level is divided by {WorryLevelDivisor} to {itemWorryLevel}.");

            bool testResult = GetTestResult(itemWorryLevel);
            //Console.WriteLine($"Is worry level divisible by {_testDivisor}? {testResult}.");

            int throwToMonkeyNum = testResult ? _monkeyNumberWhenTestTrue : _monkeyNumberWhenTestFalse;

            _monkeyList[throwToMonkeyNum].AddAnItem(itemWorryLevel);
            //Console.WriteLine($"Item with worry level {itemWorryLevel} is thrown to monkey {throwToMonkeyNum}.");
        }

        public void AddAnItem(long worryLevel)
        {
            Items.Enqueue(worryLevel);
        }

        private void ParseDefinition(string definition)
        {
            string[] lines = definition.Split("\n");

            ParseStartingItems(lines[1]);

            ParseOperation(lines[2]);

            ParseTestDivisor(lines[3]);

            ParseTestOutcomes(lines[4], lines[5]);
        }

        private void ParseTestOutcomes(string ifTrueLine, string ifFalseLine)
        {
            _monkeyNumberWhenTestTrue = int.Parse(ifTrueLine.Substring(29));

            _monkeyNumberWhenTestFalse = int.Parse(ifFalseLine.Substring(30));
        }

        private void ParseTestDivisor(string testDivisorLine)
        {
            _testDivisor = int.Parse(testDivisorLine.Substring(21));
        }

        private void ParseOperation(string operationLine)
        {
            string[] parts = operationLine.Split("= ");

            string[] expressionParts = parts[1].Split(" ");

            string operand1 = expressionParts[0];
            string mathsOperator = expressionParts[1];
            string operand2 = expressionParts[2];

            int operandTemp;

            if (operand1 == "old" && mathsOperator == "*" && int.TryParse(operand2, out operandTemp))
            {
                // old * A
                _coefficientA = operandTemp;
                _coefficientB = 0;
                _constantC = 0;
            }
            else if ((mathsOperator == "+" || mathsOperator == "-") && int.TryParse(operand2, out operandTemp))
            {
                // old +/- C
                _constantC = operandTemp;
                _coefficientA = 1;
                _coefficientB = 0;
            }
            else if (operand1 == "old" && mathsOperator == "*" && operand2.Trim() == "old")
            {
                // old * old
                _coefficientB = 1;
                _coefficientA = 0;
                _constantC = 0;
            }
            else
            {
                throw new ArgumentException("Maths expression for Operation in unexpected format.");
            }
        }

        private void ParseStartingItems(string itemsLine)
        {
            string[] items = itemsLine.Substring(17).Split(",");
            
            foreach (var item in items)
            {
                Items.Enqueue(int.Parse(item));
            }
        }

        private long OperationCalcWorryLevel(long currentValue)
        {
            return _coefficientA * currentValue
                + _coefficientB * currentValue * currentValue
                + _constantC;
        }

        private bool GetTestResult(long currentWorryLevel)
        {
            double current = Convert.ToDouble(currentWorryLevel);
            double divisor = Convert.ToDouble(_testDivisor);

            double result = current / divisor;
            double resultFloored = Math.Floor(result);

            bool testResult = (result - resultFloored) < 1e-6;

            return testResult;
        }

        public override string ToString()
        {
            string itemList = String.Empty;

            foreach (var item in Items)
            {
                itemList += item.ToString() + ", ";
            }

            return $"Monkey {Number}: {itemList}";
        }

        public int CompareTo(Monkey? other)
        {
            if (other == null) return 1;

            if (InspectionCount == other.InspectionCount) return 0;

            // want sort descending

            if (InspectionCount > other.InspectionCount) return -1;

            return 1;
        }
    }
}
