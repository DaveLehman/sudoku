using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Dev006.View
{
    using Dev006.ViewModel;
    using Windows.UI;

    public sealed partial class BoxControl : UserControl
    {
        public int ArrayMember { get; set; }        // ArrayMember is an int in the range 0 to 80 set in constructor. Never changes. 
        public int RowMember { get; set; }         // number representing membership in rows 0 to 8. Never changes. 
        public int ColumnMember { get; set; }      // number representing membership in column 0 to 8. Never changes. 
        public int GroupMember { get; set; }       // number representing membership in group 0 to 8. Never changes. 

        public PuzzleManager pm { get; set; }       // reference to PuzzleManager
        public BoxControl()
        {
            this.InitializeComponent();
        }

        private void Possibilities_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {

                var properties = e.GetCurrentPoint(this).Properties;
                var sourceObject = e.OriginalSource;
                TextBlock possBlock = sourceObject as TextBlock; // this block contains '9' for example
                Grid possGrid = this.FindName("PossGrid") as Grid;
                Grid wrongGrid = this.FindName("WrongAnswerGrid") as Grid;
                TextBlock answerBlock = this.FindName("XText") as TextBlock;


                if (properties.IsLeftButtonPressed)
                {
                    // Left button pressed
                    // User is guessing at the correct answer for this square
                    System.Diagnostics.Trace.WriteLine("Left button pressed " + ((TextBlock)possBlock).Text);
                    System.Diagnostics.Trace.WriteLine("Box Control Name is "+ this.GetValue(FrameworkElement.NameProperty));
                    // if user input is not correct in terms of the puzzle, display a red X
                    // (PointerReleased caught by WrongAnswerGrid will reset the value)
                    string text = ((TextBlock)possBlock).Text;
                    int value = Int32.Parse(text);
                    if (pm.UserLeftMouseDown(this, value) == true )
                    {
                        // puzzle has been updated, now update the display
                        MainPage parentPage = FindParent(this, typeof(MainPage)) as MainPage;
                        parentPage.UpdatePuzzleDisplay();
                    }
                    else
                    {
                        System.Diagnostics.Trace.WriteLine(value + " is not the answer!");
                        wrongGrid.Visibility = Visibility.Visible;
                    }

                }
                else if (properties.IsRightButtonPressed)
                {
                    // Right button pressed
                    // User is trying to remove a possibility from this square
                    System.Diagnostics.Trace.WriteLine("Right button pressed " + ((TextBlock)possBlock).Text);
                    string text = ((TextBlock)possBlock).Text;
                    int value = Int32.Parse(text);
                    if (pm.UserRightMouseDown(this, value) == true)
                    {
                        // puzzle has been updated, now update the display
                        MainPage parentPage = FindParent(this, typeof(MainPage)) as MainPage;
                        parentPage.UpdatePuzzleDisplay();
                    }
                    else
                    {
                        System.Diagnostics.Trace.WriteLine(value + " is answer! Can't remove it as a possibility");
                        wrongGrid.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void Possibilities_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            /* This is the corresponding mouse up from Possibilities_Pointer Pressed
             * If the user makes an incorrect choice, PointerPressed (handled by the
             * nine text boxes in the possibilities grid) makes the possibilities grid
             * invisible and make the wrong answer grid with a red X Visible.
             * Therefore, the PointerReleased event doesn't go to the nine text boxes but 
             * instead goes to the WrongAnswer grid. This is actually a good thing since
             * we only need to respond to this mouse up event if we've displayed a red x
             * and this function only executes if we have one...*/
            System.Diagnostics.Trace.WriteLine("Pointer Released Event");
            // if user made a mistake and got the red X, we need to restore how it was
            // before the error display
            var properties = e.GetCurrentPoint(this).Properties;
            var sourceObject = e.OriginalSource;
            Grid wrongGrid = this.FindName("WrongAnswerGrid") as Grid;
            wrongGrid.Visibility = Visibility.Collapsed;

            // otherwise we don't care about this event
        }

        public UIElement FindParent(UIElement uieSearchStart, Type t)
        {
            //Traverse the VisualTree until finding the UIElement that matches the given type.
            DependencyObject parent = VisualTreeHelper.GetParent(uieSearchStart);
            //Stop if we hit the top of the hierarchy or the type of the current parent
            //(or its base type) matches the given type.
            while (parent != null && (parent.GetType() != t && parent.GetType().BaseType != t))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            if (parent != null)
                return (UIElement)parent;
            else
                return null;
        }
    }


}
