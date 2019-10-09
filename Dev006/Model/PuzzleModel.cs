using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev006.Model
{
    public class PuzzleModel
    {
        private readonly string puzzleString = null;
        private readonly string answerString = null;

        private readonly Square[] _squares = null;
        public Square[] Squares { get { return _squares; } }
        public int TotalSolved
        {
            get
            {
                int count = 0;
                foreach (Square square in _squares)
                    if (square.Solved == true)
                        count++;
                return count;
            }
        }
        public PuzzleModel()
        {
            _squares = new Square[81];
            puzzleString = LoadPuzzleString();
            answerString = LoadAnswerString();

            MapPuzzleStringToSquares();
            MapAnswerStringToSquares();

            PrintSquareToDebugOutput();
        }
        private void MapPuzzleStringToSquares()
        {
            char c;
            for (int i = 0; i < 81; i++)
            {
                c = puzzleString[i];
                if (Char.IsDigit(c))
                {
                    _squares[i] = new Square(i, true);
                }
                else
                {
                    _squares[i] = new Square(i, false);
                }
            }
        }

        private void MapAnswerStringToSquares()
        {
            // loads answers from answerString to 
            char c;
            for (int i = 0; i < 81; i++)
            {
                c = answerString[i];
                if (Char.IsDigit(c))
                {
                    _squares[i].SetAnswer((int)Char.GetNumericValue(c), false);
                }
            }
        }

        private string LoadPuzzleString()
        {
            return ("53..7...." +
                    "6..195..." +
                    ".98....6." +
                    "8...6...3" +
                    "4..8.3..1" +
                    "7...2...6" +
                    ".6....28." +
                    "...419..5" +
                    "....8..79");
        }

        private string LoadAnswerString()
        {
            return ("534678912" +
                    "672195348" +
                    "198342567" +
                    "859761423" +
                    "426853791" +
                    "713924856" +
                    "961537284" +
                    "287419635" +
                    "345286179");
        }

        private void PrintSquareToDebugOutput()
        {
            for (int i = 0; i < 81; i++)
            {
                System.Diagnostics.Trace.WriteLine("***************************");
                System.Diagnostics.Trace.WriteLine("\tarrayMember: " + _squares[i].ArrayMember);
                if (_squares[i].AnswerValue == null)
                    System.Diagnostics.Trace.WriteLine("\tanswerValue: is null");
                else
                    System.Diagnostics.Trace.WriteLine("\tanswerValue: " + _squares[i].AnswerValue);
                if (_squares[i].Possibles == null)
                    System.Diagnostics.Trace.WriteLine("\tpossibles: is null");
                else
                    System.Diagnostics.Trace.WriteLine("\tpossibles Count: " + _squares[i].Possibles.Count());
                System.Diagnostics.Trace.WriteLine("\trowMember: " + _squares[i].RowMember);
                System.Diagnostics.Trace.WriteLine("\tcolumnMember: " + _squares[i].ColumnMember);
                System.Diagnostics.Trace.WriteLine("\tgroupMember: " + _squares[i].GroupMember);
            }


        }
    }
}
