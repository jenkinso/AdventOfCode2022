using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models.Day10
{
    public class CPU
    {
        public int X { get; private set; } = 1;
        public int CycleNumber { get; private set; } = 0;
        public int SignalStrength
        {
            get
            {
                return CycleNumber * X;
            }
        }
        public int SumInterestingSignalStrengths
        {
            get
            {
                return _interestingSignalStrengths.Sum();
            }
        }
        
        private Queue<int> _queue = new();
        private Queue<string> _instructionQueue = new();
        private int _programCounter = 0;
        private List<int> _interestingSignalStrengths = new();
        private const int CRTWidthPixels = 40;

        public CPU(string[] program)
        {
            foreach (var line in program)
            {
                _instructionQueue.Enqueue(line);
            }
        }

        public void RunProgram()
        {
            while (_programCounter >= 0)
            {
                CycleNumber++;

                RecordInterestingSignalStrength();

                DrawPixel();

                UpdateX();

                ReadInstruction();                

                _programCounter--;
            }
        }

        private void DrawPixel()
        {
            int horizontalPosition = CycleNumber % CRTWidthPixels - 1;

            // Assume pixel is dark
            char pixel = '.';

            if (horizontalPosition >= X - 1 && horizontalPosition <= X + 1)
            {
                // The horizontal position of the CRT is over one of the three sprite positions (X-1, X or X+1). Pixel is lit.
                pixel = '#';
            }

            Console.Write(pixel);

            if (IsEndOfRow())
            {
                Console.Write("\n");
            }
        }

        private bool IsEndOfRow() => CycleNumber % CRTWidthPixels == 0;

        private void RecordInterestingSignalStrength()
        {
            const int cyclePeriod = 40;
            const int startCycle = 20;

            if ((CycleNumber - startCycle) % cyclePeriod == 0)
            {
                _interestingSignalStrengths.Add(SignalStrength);
            }
        }

        private void UpdateX()
        {
            if (_queue.Count > 0)
            {
                X += _queue.Dequeue();
            }
        }

        private void ReadInstruction()
        {
            // Only attempt to read the next instruction when the "programCounter" is zero.
            // pc > 0 ==> previous instruction still in progress.
            if (_programCounter == 0)
            {
                if (_instructionQueue.Count > 0)
                {
                    string instruction = _instructionQueue.Dequeue();
                    ProcessInstruction(instruction);
                }
            }
        }

        private void ProcessInstruction(string instruction)
        {
            string[] parts = instruction.Split(' ');

            if (parts[0] == "noop")
            {
                // noop ==> we want one more cycle to occur, so increment pc, but do nothing else
                _programCounter++;
            }
            else if(parts[0] == "addx")
            {
                int argV = int.Parse(parts[1]);
                
                _queue.Enqueue(argV);

                // addx ==> we want two more cycles to occur. The argV is queued for updating X in the next cycle.
                // In the cycle following that, the next instruction is read.
                _programCounter += 2;
            }
        }

        public override string ToString()
        {
            return $"Cycle {CycleNumber} X = {X} _queue.Count = {_queue.Count} Sum int signal str = {SumInterestingSignalStrengths}";
        }
    }
}
