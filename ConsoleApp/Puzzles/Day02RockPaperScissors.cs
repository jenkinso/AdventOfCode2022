using ConsoleApp.Models.Day2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Puzzles
{
    public static class Day02RockPaperScissors
    {
        private const string DataFile = @"data\day02.txt";
        private const string PracticeFile = @"data\day02Practice.txt";

        public static int Part1()
        {
            ShapeScores shapeScores = new(RockScore: 1, PaperScore: 2, ScissorsScore: 3);
            OutcomeScores outcomeScores = new(LoseScore: 0, DrawScore: 3, WinScore: 6);
            ShapeCodes myShapeCodes = new(RockCode: 'X', PaperCode: 'Y', ScissorsCode: 'Z');
            ShapeCodes opponentsShapeCodes = new(RockCode: 'A', PaperCode: 'B', ScissorsCode: 'C');

            RockPaperScissors game = new RockPaperScissors(shapeScores, outcomeScores, myShapeCodes, opponentsShapeCodes, DataFile);

            return game.MyTotalScore;
        }

        public static int Part2()
        {
            ShapeScores shapeScores = new(RockScore: 1, PaperScore: 2, ScissorsScore: 3);
            OutcomeScores outcomeScores = new(LoseScore: 0, DrawScore: 3, WinScore: 6);
            OutcomeCodes myOutcomeCodes = new(LoseCode: 'X', DrawCode: 'Y', WinCode: 'Z');
            ShapeCodes opponentsShapeCodes = new(RockCode: 'A', PaperCode: 'B', ScissorsCode: 'C');

            RockPaperScissorsAlt game = new RockPaperScissorsAlt(shapeScores, outcomeScores, myOutcomeCodes, opponentsShapeCodes, DataFile);

            return game.MyTotalScore;
        }
    }
}
