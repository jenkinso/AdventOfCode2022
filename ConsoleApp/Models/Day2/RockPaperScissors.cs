using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models.Day2
{
    /// <summary>
    /// Part 1 version of RockPaperScissors.
    /// </summary>
    public class RockPaperScissors
    {
        private Dictionary<char, Shape> opponentsCodeToShapeMap = new();
        private Dictionary<char, Shape> myCodeToShapeMap = new();
        private Dictionary<Shape, int> shapeToScoreMap = new();
        private Dictionary<Outcome, int> outcomeToScoreMap = new();

        public int MyTotalScore { get; private set; }

        public RockPaperScissors(ShapeScores shapeScores, OutcomeScores outcomeScores, ShapeCodes myShapeCodes, ShapeCodes opponentsShapeCodes, string filePath)
        {
            ConfigureScores(shapeScores, outcomeScores);

            ConfigureCodes(myShapeCodes, opponentsShapeCodes);

            Play(filePath);
        }

        private void Play(string filePath)
        {
            string[] roundsData = File.ReadAllLines(filePath);

            foreach (var line in roundsData)
            {
                Shape opponentsShape = opponentsCodeToShapeMap[line[0]];
                Shape myShape = myCodeToShapeMap[line[2]];

                var outcome = GetOutcome(myShape, opponentsShape);

                UpdateMyTotalScore(myShape, outcome);
            }
        }

        private void ConfigureScores(ShapeScores shapeScores, OutcomeScores outcomeScores)
        {
            shapeToScoreMap.Add(Shape.Rock, shapeScores.RockScore);
            shapeToScoreMap.Add(Shape.Paper, shapeScores.PaperScore);
            shapeToScoreMap.Add(Shape.Scissors, shapeScores.ScissorsScore);

            outcomeToScoreMap.Add(Outcome.Lose, outcomeScores.LoseScore);
            outcomeToScoreMap.Add(Outcome.Draw, outcomeScores.DrawScore);
            outcomeToScoreMap.Add(Outcome.Win, outcomeScores.WinScore);
        }

        private void ConfigureCodes(ShapeCodes myCodes, ShapeCodes opponentsCodes)
        {
            myCodeToShapeMap.Add(myCodes.RockCode, Shape.Rock);
            myCodeToShapeMap.Add(myCodes.PaperCode, Shape.Paper);
            myCodeToShapeMap.Add(myCodes.ScissorsCode, Shape.Scissors);

            opponentsCodeToShapeMap.Add(opponentsCodes.RockCode, Shape.Rock);
            opponentsCodeToShapeMap.Add(opponentsCodes.PaperCode, Shape.Paper);
            opponentsCodeToShapeMap.Add(opponentsCodes.ScissorsCode, Shape.Scissors);
        }

        private Outcome GetOutcome(Shape myShape, Shape opponentsShape)
        {
            if (myShape == Shape.Rock)
            {
                if (opponentsShape == Shape.Rock)
                {
                    return Outcome.Draw;
                }
                else if (opponentsShape == Shape.Paper)
                {
                    return Outcome.Lose;
                }
                else if (opponentsShape == Shape.Scissors)
                {
                    return Outcome.Win;
                }
            }
            else if (myShape == Shape.Paper)
            {
                if (opponentsShape == Shape.Rock)
                {
                    return Outcome.Win;
                }
                else if (opponentsShape == Shape.Paper)
                {
                    return Outcome.Draw;
                }
                else if (opponentsShape == Shape.Scissors)
                {
                    return Outcome.Lose;
                }
            }
            else if (myShape == Shape.Scissors)
            {
                if (opponentsShape == Shape.Rock)
                {
                    return Outcome.Lose;
                }
                else if (opponentsShape == Shape.Paper)
                {
                    return Outcome.Win;
                }
                else if (opponentsShape == Shape.Scissors)
                {
                    return Outcome.Draw;
                }
            }

            throw new ArgumentException("Invalid shape or outcome provided.");
        }

        private void UpdateMyTotalScore(Shape myShape, Outcome myOutcome)
        {
            MyTotalScore += shapeToScoreMap[myShape] + outcomeToScoreMap[myOutcome];
        }

    }
}
