using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models.Day2
{
    public record struct ShapeScores(int RockScore, int PaperScore, int ScissorsScore);

    public record struct OutcomeScores(int LoseScore, int DrawScore, int WinScore);

    public record struct ShapeCodes(char RockCode, char PaperCode, char ScissorsCode);

    public record struct OutcomeCodes(char LoseCode, char DrawCode, char WinCode);
}
