using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models.Day2
{
    /// <summary>
    /// Part 2 version of RockPaperScissors.
    /// </summary>
    public class RockPaperScissorsAlt
    {
        private Dictionary<char, Shape> opponentsCodeToShapeMap = new();
        private Dictionary<char, Outcome> myCodeToOutcomeMap = new();
        private Dictionary<Shape, int> shapeToScoreMap = new();
        private Dictionary<Outcome, int> outcomeToScoreMap = new();

        public int MyTotalScore { get; private set; }

        public RockPaperScissorsAlt(ShapeScores shapeScores, OutcomeScores outcomeScores, OutcomeCodes myOutcomeCodes, ShapeCodes opponentsShapeCodes, string filePath)
        {
            ConfigureScores(shapeScores, outcomeScores);

            ConfigureCodes(myOutcomeCodes, opponentsShapeCodes);

            Play(filePath);
        }

        private void Play(string filePath)
        {
            string[] roundsData = File.ReadAllLines(filePath);

            foreach (var line in roundsData)
            {
                Shape opponentsShape = opponentsCodeToShapeMap[line[0]];
                Outcome myOutcome = myCodeToOutcomeMap[line[2]];

                Shape myShape = GetMyShape(opponentsShape, myOutcome);

                UpdateMyTotalScore(myShape, myOutcome);
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

        private void ConfigureCodes(OutcomeCodes myOutcomeCodes, ShapeCodes opponentsCodes)
        {
            myCodeToOutcomeMap.Add(myOutcomeCodes.LoseCode, Outcome.Lose);
            myCodeToOutcomeMap.Add(myOutcomeCodes.DrawCode, Outcome.Draw);
            myCodeToOutcomeMap.Add(myOutcomeCodes.WinCode, Outcome.Win);

            opponentsCodeToShapeMap.Add(opponentsCodes.RockCode, Shape.Rock);
            opponentsCodeToShapeMap.Add(opponentsCodes.PaperCode, Shape.Paper);
            opponentsCodeToShapeMap.Add(opponentsCodes.ScissorsCode, Shape.Scissors);
        }

        private Shape GetMyShape(Shape opponentsShape, Outcome myOutcome)
        {
            if (opponentsShape == Shape.Rock)
            {
                if (myOutcome == Outcome.Lose)
                {
                    return Shape.Scissors;
                }
                else if (myOutcome == Outcome.Draw)
                {
                    return Shape.Rock;
                }
                else if (myOutcome == Outcome.Win)
                {
                    return Shape.Paper;
                }
            }
            else if (opponentsShape == Shape.Paper)
            {
                if (myOutcome == Outcome.Lose)
                {
                    return Shape.Rock;
                }
                else if (myOutcome == Outcome.Draw)
                {
                    return Shape.Paper;
                }
                else if (myOutcome == Outcome.Win)
                {
                    return Shape.Scissors;
                }
            }
            else if (opponentsShape == Shape.Scissors)
            {
                if (myOutcome == Outcome.Lose)
                {
                    return Shape.Paper;
                }
                else if (myOutcome == Outcome.Draw)
                {
                    return Shape.Scissors;
                }
                else if (myOutcome == Outcome.Win)
                {
                    return Shape.Rock;
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
