using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Puzzles
{
    public static class Day06TuningTrouble
    {
        private const string DataFile = @"data\day06.txt";
        private const string PracticeFile = @"data\day06Practice.txt";

        public static int Part1()
        {
            string[] data = File.ReadAllLines(DataFile);
            string message = data[0];

            var markerLength = 4;

            return FindTheMarkerPosition(message, markerLength);            
        }

        public static int Part2()
        {
            string[] data = File.ReadAllLines(DataFile);
            string message = data[0];

            var markerLength = 14;

            return FindTheMarkerPosition(message, markerLength);
        }

        private static int FindTheMarkerPosition(string message, int markerLength)
        {
            int markerPosition = 0;

            var queue = InitialiseAQueue(message, markerLength);            

            // For each additional character:
            // add it to the queue, test for the marker, if not found: dequeue ready for the next iteration
            for (int c = markerLength - 1; c < message.Length; c++)
            {
                queue.Enqueue(message[c]);

                HashSet<char> set = new HashSet<char>(queue);

                if (set.Count == markerLength)
                {
                    markerPosition = c + 1;
                    break;
                }

                queue.Dequeue();
            }

            return markerPosition;
        }

        private static Queue<char> InitialiseAQueue(string message, int markerLength)
        {
            Queue<char> queue = new Queue<char>();

            // Initialise the queue: fill it with the first (markerLength - 1) characters
            for (int c = 0; c < markerLength - 1; c++)
            {
                queue.Enqueue(message[c]);
            }

            return queue;
        }

        /// <summary>
        /// Wanted to see if I could do it with loops and no queues.
        /// Not pretty.
        /// </summary>
        /// <returns>Surprisingly, the correct answer.</returns>
        public static int Part1WithLoops()
        {
            string[] data = File.ReadAllLines(DataFile);

            string message = data[0];

            const int markerLength = 4;

            int numCharactersProcessed = 0;

            for (int c = markerLength - 1; c < message.Length; c++)
            {
                bool duplicateFound = false;

                for (int d = c - markerLength + 1; d < c; d++)
                {
                    for (int e = d + 1; e <= c; e++)
                    {
                        if (message[e] == message[d])
                        {
                            duplicateFound = true;
                            break;
                        }
                    }

                    if (duplicateFound) break;
                }

                if (!duplicateFound)
                {
                    numCharactersProcessed = c + 1;
                    break;
                }
            }

            return numCharactersProcessed;
        }
    }
}
