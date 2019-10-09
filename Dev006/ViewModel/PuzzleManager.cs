using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev006.ViewModel
{
    using Dev006.View;
    using Dev006.Model;
    using System.ComponentModel;

    public class PuzzleManager:INotifyPropertyChanged
    {
        private readonly PuzzleModel _puzzle = new PuzzleModel();
        public PuzzleModel Puzzle { get { return _puzzle; } }

        private string _statusString = "No events have occurred yet";
        public string StatusString { get { return _statusString; } }
        public string SolvedString { get { return Puzzle.TotalSolved.ToString(); } }

        private bool _autoRemove = false;
        public bool AutoRemovePossibilities { get { return _autoRemove; } }

        internal bool UserLeftMouseDown(BoxControl boxControl, int value)
        {
            // Left Mouse Button on a possibility
            // means user wants to set this value as the answer to this square
            System.Diagnostics.Trace.WriteLine("PuzzleManager: received LMouse Down from " + boxControl.Name + ", value " + value);
            // strip the bc and convert to an integer
            string name = boxControl.Name;
            name = name.Replace("bc", "");
            int index = Int32.Parse(name);
            // check the puzzle answers at index
            if(_puzzle.Squares[index].AnswerValue == value)
            {
                // set Solved
                _puzzle.Squares[index].SetSolved();
                _statusString = ("SOLVED square " + index + " with a value of " + value);
                OnPropertyChanged("StatusString");
                OnPropertyChanged("SolvedString");
                NewSolvePossiblesRemove(index, value);
                return true;
            }
            else
            {
                _statusString = ("WRONG -- The answer to square " + index + " is not " + value + "... Try Again");
                OnPropertyChanged("StatusString");
                return false;
            }
        }

        public void NewSolvePossiblesRemove(int index, int? value)
        {
            // Square at 'index' has just been solved -- it has a value of 'value'
            // Now need to remove that value as a possibility for all Squares in the
            // same Row, Column and Group as index
            System.Diagnostics.Trace.WriteLine("NewSolvePossiblesRemove()");

            var rowColGrpMates = 
                from sqr in _puzzle.Squares
                where (sqr.RowMember == _puzzle.Squares[index].RowMember) ||
                    (sqr.ColumnMember == _puzzle.Squares[index].ColumnMember) ||
                    (sqr.GroupMember == _puzzle.Squares[index].GroupMember)
                select sqr;
            System.Diagnostics.Trace.WriteLine("Selected count " + rowColGrpMates.Count());
            int removed = 0;
            foreach (Square sq in rowColGrpMates)
            {
                if (sq.RequestPossibilityRemove((int)value))
                    removed++;
            }
            System.Diagnostics.Trace.WriteLine("Removed " + removed);

        }

        internal bool UserRightMouseDown(BoxControl boxControl, int value)
        {
            // Right Mouse Button on a possibility
            // means user wants remove this possibility
            System.Diagnostics.Trace.WriteLine("PuzzleManager: received RMouse Down from " + boxControl.Name + ", value " + value);
            // strip the bc and convert to an integer
            string name = boxControl.Name;
            name = name.Replace("bc", "");
            int index = Int32.Parse(name);
            // check the puzzle possibilites at index
            if (_puzzle.Squares[index].RequestPossibilityRemove(value) == true)
            {
                _statusString = "You're right -- " + value.ToString() + " isn't possible in square " + name;
                OnPropertyChanged("StatusString");
                return true;
            }
            else
            {
                _statusString = "WRONG -- you can't remove "+ value.ToString() + " as a possibility from square "+ name + ". Try again.";
                OnPropertyChanged("StatusString");
                return false;
            }
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
