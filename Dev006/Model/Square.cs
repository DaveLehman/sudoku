using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev006.Model
{
    public class Square
    {
        // A Sudoku Square is solved if you know what value to put in the square with your pencil or
        // with this program. Set initially in the constructor, could be changed later.
        // If _solved is TRUE, AnswerValue is exposed to the user and is known.
        // If _solved is FALSE, we must have an array of ints to represent possible AnswerValues.
        private bool _solved { get; set; }
        public bool Solved { get { return _solved; } }

        // int represents the number that should be in this Square. Tied to _solved boolean.
        // Null if not known. Set initially in the constructor, should never change.
        private int? _answerValue { get; set; }
        public int? AnswerValue { get { return _answerValue; } }      //  1 - 9 OR null


        // if _solved is false, and _answerValue is null, this array represents the possible remaining values for this square.
        // The normal puzzle solving process involves removing possibles from squares until you know what the value of this square must be.
        // Set initially in the constructor, will be changed later.
        private List<int> _possibles { get; set; }        // members are {1,2,3,4,5,6,7,8,9}


        // obviously watch out when you use index 0-8 ~~ to get values 1-9 ... 
        public List<int> Possibles { get { if (_possibles != null) return _possibles; else return null; } }



        public int ArrayMember { get; set; }        // ArrayMember is an int in the range 0 to 80 set in constructor. Never changes. 
        public int RowMember { get; set; }         // number representing membership in rows 0 to 8. Never changes. 
        public int ColumnMember { get; set; }      // number representing membership in column 0 to 8. Never changes. 
        public int GroupMember { get; set; }       // number representing membership in group 0 to 8. Never changes. 

        public Square(int arrayMember, bool solved)
        {
            // _answerValue will be set later. Everything else initialized
            ArrayMember = arrayMember;
            _solved = solved;
            SetRowColumnGroup(arrayMember);
            if (solved)
            {
                _possibles = null;
            }
            else
            {
                _possibles = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            }
        }

        private void SetRowColumnGroup(int arrayMember)
        {
            // rows and columns are simple integer division math
            int modulo = arrayMember % 9;
            int intDiv = arrayMember / 9;
            ColumnMember = modulo;
            RowMember = intDiv;

            /* 0    1   2   |   3   4    5  |   6    7  8 
            * 9    10  11  |   12  13  14  |   15  16  17
            * 18   19  20  |   21  22  23  |   24  25  26
            * _________________________________________
            * 27   28  29  |   30  31  32  |   33  34  35
            * 36   37  38  |   39  40  41  |   42  43  44
            * 45   46  47  |   48  49  50  |   51  52  53
            * _____________________________________________
            * 54   55  56  |   57  58  59  |   60  61  62
            * 63   64  65  |   66  67  68  |   69  70  71
            * 72   73  74  |   75  76  77  |   78  79  80
            */

            // easiest way to do groups is just create arrays of members and match 'em
            int[] group0 = new int[] { 0, 1, 2, 9, 10, 11, 18, 19, 20 };
            int[] group1 = new int[] { 3, 4, 5, 12, 13, 14, 21, 22, 23 };
            int[] group2 = new int[] { 6, 7, 8, 15, 16, 17, 24, 25, 26 };
            int[] group3 = new int[] { 27, 28, 29, 36, 37, 38, 45, 46, 47 };
            int[] group4 = new int[] { 30, 31, 32, 39, 40, 41, 48, 49, 50 };
            int[] group5 = new int[] { 33, 34, 35, 42, 43, 44, 51, 52, 53 };
            int[] group6 = new int[] { 54, 55, 56, 63, 64, 65, 72, 73, 74 };
            int[] group7 = new int[] { 57, 58, 59, 66, 67, 68, 75, 76, 77 };
            int[] group8 = new int[] { 60, 61, 62, 69, 70, 71, 78, 79, 80 };

            if (group0.Contains(arrayMember))
                GroupMember = 0;
            if (group1.Contains(arrayMember))
                GroupMember = 1;
            if (group2.Contains(arrayMember))
                GroupMember = 2;
            if (group3.Contains(arrayMember))
                GroupMember = 3;
            if (group4.Contains(arrayMember))
                GroupMember = 4;
            if (group5.Contains(arrayMember))
                GroupMember = 5;
            if (group6.Contains(arrayMember))
                GroupMember = 6;
            if (group7.Contains(arrayMember))
                GroupMember = 7;
            if (group8.Contains(arrayMember))
                GroupMember = 8;
        }


        public void SetAnswer(int answerValue, bool clearPossibles)
        {
            // The answer to this Square is now known. Set its value and clear the Possibles list if asked
            _answerValue = answerValue;
            if (clearPossibles)
                _possibles = null;
        }

        public bool RequestPossibilityRemove(int valueToRemove)
        {
            if (_possibles == null)
                return false;
            int possibleCount = _possibles.Count();
            if(valueToRemove != AnswerValue)
            {
                _possibles.Remove(valueToRemove);
            }

            // check if we actually removed a possible
            if (possibleCount == _possibles.Count)
            {
                System.Diagnostics.Trace.WriteLine("Can't remove possible value " + valueToRemove + " from square " + ArrayMember);
                return false;
            }
            else
            {
                System.Diagnostics.Trace.WriteLine("Removed possible value " + valueToRemove + " from square " + ArrayMember);
                return true;
            }

        }

        public void SetSolved()
        {
            _solved = true;
        }

    }
}
