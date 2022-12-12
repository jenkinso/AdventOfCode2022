using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Puzzles
{
    public static class Day08TreetopTreeHouse
    {
        private const string DataFile = @"data\day08.txt";
        private const string PracticeFile = @"data\day08Practice.txt";

        public static int Part1()
        {
            return SolveTheProblem().visibleCount;
        }

        public static int Part2()
        {
            return SolveTheProblem().highestScenicScore;
        }

        private static (int visibleCount, int highestScenicScore) SolveTheProblem()
        {
            (int visibleCount, int highestScenicScore) answers = (0, 0);

            string[] data = File.ReadAllLines(DataFile);

            int[,] grid = GetGridOfInts(data);
            int numRows = grid.GetLength(0);
            int numCols = grid.GetLength(1);

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    if (row == 0 || row == numRows - 1 || col == 0 || col == numCols - 1)
                    {
                        answers.visibleCount++;
                        continue;
                    }

                    int height = grid[row, col];
                    bool visibleLeft = true;
                    bool visibleRight = true;
                    bool visibleUp = true;
                    bool visibleDown = true;
                    int leftScenic = 0;
                    int rightScenic = 0;
                    int upScenic = 0;
                    int downScenic = 0;

                    // Look left
                    for (int c = col - 1; c >= 0; c--)
                    {
                        leftScenic++;

                        if (grid[row, c] >= height)
                        {
                            visibleLeft = false;
                            break;
                        }
                    }

                    // Look right
                    for (int c = col + 1; c < numCols; c++)
                    {
                        rightScenic++;

                        if (grid[row, c] >= height)
                        {
                            visibleRight = false;
                            break;
                        }
                    }

                    // Look up
                    for (int r = row - 1; r >= 0; r--)
                    {
                        upScenic++;

                        if (grid[r, col] >= height)
                        {
                            visibleUp = false;
                            break;
                        }
                    }

                    // Look down
                    for (int r = row + 1; r < numRows; r++)
                    {
                        downScenic++;

                        if (grid[r, col] >= height)
                        {
                            visibleDown = false;
                            break;
                        }
                    }

                    if (visibleLeft || visibleRight || visibleUp || visibleDown)
                    {
                        answers.visibleCount++;
                    }

                    int scenicScore = leftScenic * rightScenic * upScenic * downScenic;

                    if (scenicScore > answers.highestScenicScore)
                    {
                        answers.highestScenicScore = scenicScore;
                    }
                }
            }

            return answers;
        }

        private static int[,] GetGridOfInts(string[] data)
        {
            int numCols = data[0].Length;
            int numRows = data.Length;

            int[,] grid = new int[numRows, numCols];

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    grid[row, col] = int.Parse((data[row][col]).ToString());
                }
            }

            return grid;
        }
    }
}
