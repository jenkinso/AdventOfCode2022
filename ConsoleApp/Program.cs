using ConsoleApp.Puzzles;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"The answer to Day 1 Part 1 is: {Day01CalorieCounting.Part1()}.");
            Console.WriteLine($"The answer to Day 1 Part 2 is: {Day01CalorieCounting.Part2()}.");

            Console.WriteLine($"The answer to Day 2 Part 1 is: {Day02RockPaperScissors.Part1()}.");
            Console.WriteLine($"The answer to Day 2 Part 2 is: {Day02RockPaperScissors.Part2()}.");

            Console.WriteLine($"The answer to Day 3 Part 1 is: {Day03RucksackReorganization.Part1()}.");
            Console.WriteLine($"The answer to Day 3 Part 2 is: {Day03RucksackReorganization.Part2()}.");

            Console.WriteLine($"The answer to Day 4 Part 1 is: {Day04CampCleanup.Part1()}.");
            Console.WriteLine($"The answer to Day 4 Part 2 is: {Day04CampCleanup.Part2()}.");
        }
    }
}