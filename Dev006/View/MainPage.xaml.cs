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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Dev006.View
{
    using Dev006.ViewModel;
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        BoxControl[] bc = new BoxControl[81];
        public MainPage()
        {
            this.InitializeComponent();
            //PeekAllButton needs special handling -- Button object expects Click as a standard event, and PointerPressed/Released only fire with the right mouse button.
            // We want left press to fire PointerPressed and left released to fire PointerReleased so need to add handlers here
            PeekAllButton.AddHandler(PointerPressedEvent, new PointerEventHandler(PeekAllButton_PointerPressed), true);
            PeekAllButton.AddHandler(PointerReleasedEvent, new PointerEventHandler(PeekAllButton_PointerReleased), true);

            CreateDisplayBoxControls();

            // check stuus of autoRemove toggle before drawing display
            if (AutoRemoveToggle.IsChecked == true)
            {
                System.Diagnostics.Trace.WriteLine("AutoRemove Click, current value Checked");
                // if we have not been removing possibilities before, we need to remove
                // possibilities for every square that has been solved...
                for (int i = 0; i < 81; i++)
                {
                    int? solvedValue;
                    if (pm.Puzzle.Squares[i].Solved)
                    {
                        solvedValue = pm.Puzzle.Squares[i].AnswerValue;
                        pm.NewSolvePossiblesRemove(i, solvedValue);
                    }
                }


            }
            UpdatePuzzleDisplay();
        }

        public void UpdatePuzzleDisplay()
        {
            for (int i = 0; i < 81; i++)
            {
                // get references to the items in the UserControl we need
                Grid pGrid = bc[i].FindName("PossGrid") as Grid;
                Grid aGrid = bc[i].FindName("AnswerGrid") as Grid;
                Grid wGrid = bc[i].FindName("WrongAnswerGrid") as Grid;
                TextBlock aTextBlock = bc[i].FindName("AText") as TextBlock;
                TextBlock OneTextBlock = bc[i].FindName("Block1Text") as TextBlock;
                TextBlock TwoTextBlock = bc[i].FindName("Block2Text") as TextBlock;
                TextBlock ThreeTextBlock = bc[i].FindName("Block3Text") as TextBlock;
                TextBlock FourTextBlock = bc[i].FindName("Block4Text") as TextBlock;
                TextBlock FiveTextBlock = bc[i].FindName("Block5Text") as TextBlock;
                TextBlock SixTextBlock = bc[i].FindName("Block6Text") as TextBlock;
                TextBlock SevenTextBlock = bc[i].FindName("Block7Text") as TextBlock;
                TextBlock EightTextBlock = bc[i].FindName("Block8Text") as TextBlock;
                TextBlock NineTextBlock = bc[i].FindName("Block9Text") as TextBlock;
                wGrid.Visibility = Visibility.Collapsed;
                if (pm.Puzzle.Squares[i].Solved)
                {
                    aTextBlock.Text = pm.Puzzle.Squares[i].AnswerValue.ToString();
                    aGrid.Visibility = Visibility.Visible;
                    pGrid.Visibility = Visibility.Collapsed;
                }
                else
                {
                    aGrid.Visibility = Visibility.Collapsed;

                    if (pm.Puzzle.Squares[i].Possibles.Contains(1))
                        OneTextBlock.Visibility = Visibility.Visible;
                    else
                        OneTextBlock.Visibility = Visibility.Collapsed;
                    if (pm.Puzzle.Squares[i].Possibles.Contains(2))
                        TwoTextBlock.Visibility = Visibility.Visible;
                    else
                        TwoTextBlock.Visibility = Visibility.Collapsed;
                    if (pm.Puzzle.Squares[i].Possibles.Contains(3))
                        ThreeTextBlock.Visibility = Visibility.Visible;
                    else
                        ThreeTextBlock.Visibility = Visibility.Collapsed;
                    if (pm.Puzzle.Squares[i].Possibles.Contains(4))
                        FourTextBlock.Visibility = Visibility.Visible;
                    else
                        FourTextBlock.Visibility = Visibility.Collapsed;
                    if (pm.Puzzle.Squares[i].Possibles.Contains(5))
                        FiveTextBlock.Visibility = Visibility.Visible;
                    else
                        FiveTextBlock.Visibility = Visibility.Collapsed;
                    if (pm.Puzzle.Squares[i].Possibles.Contains(6))
                        SixTextBlock.Visibility = Visibility.Visible;
                    else
                        SixTextBlock.Visibility = Visibility.Collapsed;
                    if (pm.Puzzle.Squares[i].Possibles.Contains(7))
                        SevenTextBlock.Visibility = Visibility.Visible;
                    else
                        SevenTextBlock.Visibility = Visibility.Collapsed;
                    if (pm.Puzzle.Squares[i].Possibles.Contains(8))
                        EightTextBlock.Visibility = Visibility.Visible;
                    else
                        EightTextBlock.Visibility = Visibility.Collapsed;
                    if (pm.Puzzle.Squares[i].Possibles.Contains(9))
                        NineTextBlock.Visibility = Visibility.Visible;
                    else
                        NineTextBlock.Visibility = Visibility.Collapsed;

                    pGrid.Visibility = Visibility.Visible;
                }
            }
        }

        private void ShowAllAnswers()
        {
            for(int i = 0; i < 81; i++)
            {
                Grid aGrid = bc[i].FindName("AnswerGrid") as Grid;
                TextBlock aTextBlock = bc[i].FindName("AText") as TextBlock;

                aTextBlock.Text = pm.Puzzle.Squares[i].AnswerValue.ToString();
                System.Diagnostics.Trace.WriteLine("Set answer block " + i + " to " + aTextBlock.Text);
                aGrid.Visibility = Visibility.Visible;
            }
        }

        private void PeekAllButton_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("PeekAll button pressed");
            ShowAllAnswers();
        }

        private void PeekAllButton_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("PeekAll button released");
            UpdatePuzzleDisplay();
        }

        private void AutoRemoveToggle_Click(object sender, RoutedEventArgs e)
        {
            if(AutoRemoveToggle.IsChecked == true)
            {
                System.Diagnostics.Trace.WriteLine("AutoRemove Click, current value Checked");
                // if we have not been removing possibilities before, we need to remove
                // possibilities for every square that has been solved...
                for(int i = 0; i < 81; i++)
                {
                    int? solvedValue;
                    if(pm.Puzzle.Squares[i].Solved)
                    {
                        solvedValue = pm.Puzzle.Squares[i].AnswerValue;
                        pm.NewSolvePossiblesRemove(i, solvedValue);
                    }
                }
                UpdatePuzzleDisplay();
            }
            else
            {
                System.Diagnostics.Trace.WriteLine("AutoRemove Click, current value Unchecked");
            }
      }
        private void CreateDisplayBoxControls()
        {
            /* Need to initialize all the user controls. We have references to Grids named group0 through group9. */
            // need to make 81 of these and add them to the proper group Grid
            bc[0] = new BoxControl
            {
                ArrayMember = 0,
                RowMember = 0,
                ColumnMember = 0,
                GroupMember = 0,
                pm = pm
            };
            bc[0].SetValue(Grid.RowProperty, 0);
            bc[0].SetValue(Grid.ColumnProperty, 0);
            bc[0].SetValue(FrameworkElement.NameProperty, "bc0");
            group0.Children.Add(bc[0]);


            bc[1] = new BoxControl
            {
                ArrayMember = 1,
                RowMember = 0,
                ColumnMember = 1,
                GroupMember = 0,
                pm = pm
            };
            bc[1].SetValue(Grid.RowProperty, 0);
            bc[1].SetValue(Grid.ColumnProperty, 1);
            bc[1].SetValue(FrameworkElement.NameProperty, "bc1");
            group0.Children.Add(bc[1]);

            bc[2] = new BoxControl
            {
                ArrayMember = 2,
                RowMember = 0,
                ColumnMember = 2,
                GroupMember = 0,
                pm = pm
            };
            bc[2].SetValue(Grid.RowProperty, 0);
            bc[2].SetValue(Grid.ColumnProperty, 2);
            bc[2].SetValue(FrameworkElement.NameProperty, "bc2");
            group0.Children.Add(bc[2]);

            bc[3] = new BoxControl
            {
                ArrayMember = 3,
                RowMember = 0,
                ColumnMember = 3,
                GroupMember = 1,
                pm = pm
            };
            bc[3].SetValue(Grid.RowProperty, 0);
            bc[3].SetValue(Grid.ColumnProperty, 0);
            bc[3].SetValue(FrameworkElement.NameProperty, "bc3");
            group1.Children.Add(bc[3]);

            bc[4] = new BoxControl
            {
                ArrayMember = 4,
                RowMember = 0,
                ColumnMember = 4,
                GroupMember = 1,
                pm = pm
            };
            bc[4].SetValue(Grid.RowProperty, 0);
            bc[4].SetValue(Grid.ColumnProperty, 1);
            bc[4].SetValue(FrameworkElement.NameProperty, "bc4");
            group1.Children.Add(bc[4]);

            bc[5] = new BoxControl
            {
                ArrayMember = 5,
                RowMember = 0,
                ColumnMember = 5,
                GroupMember = 1,
                pm = pm
            };
            bc[5].SetValue(Grid.RowProperty, 0);
            bc[5].SetValue(Grid.ColumnProperty, 2);
            bc[5].SetValue(FrameworkElement.NameProperty, "bc5");
            group1.Children.Add(bc[5]);

            bc[6] = new BoxControl
            {
                ArrayMember = 6,
                RowMember = 0,
                ColumnMember = 6,
                GroupMember = 2,
                pm = pm
            };
            bc[6].SetValue(Grid.RowProperty, 0);
            bc[6].SetValue(Grid.ColumnProperty, 0);
            bc[6].SetValue(FrameworkElement.NameProperty, "bc6");
            group2.Children.Add(bc[6]);

            bc[7] = new BoxControl
            {
                ArrayMember = 7,
                RowMember = 0,
                ColumnMember = 7,
                GroupMember = 2,
                pm = pm
            };
            bc[7].SetValue(Grid.RowProperty, 0);
            bc[7].SetValue(Grid.ColumnProperty, 1);
            bc[7].SetValue(FrameworkElement.NameProperty, "bc7");
            group2.Children.Add(bc[7]);

            bc[8] = new BoxControl
            {
                ArrayMember = 8,
                RowMember = 0,
                ColumnMember = 8,
                GroupMember = 2,
                pm = pm
            };
            bc[8].SetValue(Grid.RowProperty, 0);
            bc[8].SetValue(Grid.ColumnProperty, 2);
            bc[8].SetValue(FrameworkElement.NameProperty, "bc8");
            group2.Children.Add(bc[8]);

            bc[9] = new BoxControl
            {
                ArrayMember = 9,
                RowMember = 1,
                ColumnMember = 0,
                GroupMember = 0,
                pm = pm
            };
            bc[9].SetValue(Grid.RowProperty, 1);
            bc[9].SetValue(Grid.ColumnProperty, 0);
            bc[9].SetValue(FrameworkElement.NameProperty, "bc9");
            group0.Children.Add(bc[9]);

            bc[10] = new BoxControl
            {
                ArrayMember = 10,
                RowMember = 1,
                ColumnMember = 1,
                GroupMember = 0,
                pm = pm
            };
            bc[10].SetValue(Grid.RowProperty, 1);
            bc[10].SetValue(Grid.ColumnProperty, 1);
            bc[10].SetValue(FrameworkElement.NameProperty, "bc10");
            group0.Children.Add(bc[10]);

            bc[11] = new BoxControl
            {
                ArrayMember = 11,
                RowMember = 1,
                ColumnMember = 2,
                GroupMember = 0,
                pm = pm
            };
            bc[11].SetValue(Grid.RowProperty, 1);
            bc[11].SetValue(Grid.ColumnProperty, 2);
            bc[11].SetValue(FrameworkElement.NameProperty, "bc11");
            group0.Children.Add(bc[11]);

            bc[12] = new BoxControl
            {
                ArrayMember = 12,
                RowMember = 1,
                ColumnMember = 3,
                GroupMember = 1,
                pm = pm
            };
            bc[12].SetValue(Grid.RowProperty, 1);
            bc[12].SetValue(Grid.ColumnProperty, 0);
            bc[12].SetValue(FrameworkElement.NameProperty, "bc12");
            group1.Children.Add(bc[12]);

            bc[13] = new BoxControl
            {
                ArrayMember = 13,
                RowMember = 1,
                ColumnMember = 4,
                GroupMember = 1,
                pm = pm
            };
            bc[13].SetValue(Grid.RowProperty, 1);
            bc[13].SetValue(Grid.ColumnProperty, 1);
            bc[13].SetValue(FrameworkElement.NameProperty, "bc13");
            group1.Children.Add(bc[13]);

            bc[14] = new BoxControl
            {
                ArrayMember = 14,
                RowMember = 1,
                ColumnMember = 5,
                GroupMember = 1,
                pm = pm
            };
            bc[14].SetValue(Grid.RowProperty, 1);
            bc[14].SetValue(Grid.ColumnProperty, 2);
            bc[14].SetValue(FrameworkElement.NameProperty, "bc14");
            group1.Children.Add(bc[14]);

            bc[15] = new BoxControl
            {
                ArrayMember = 15,
                RowMember = 1,
                ColumnMember = 6,
                GroupMember = 2,
                pm = pm
            };
            bc[15].SetValue(Grid.RowProperty, 1);
            bc[15].SetValue(Grid.ColumnProperty, 0);
            bc[15].SetValue(FrameworkElement.NameProperty, "bc15");
            group2.Children.Add(bc[15]);

            bc[16] = new BoxControl
            {
                ArrayMember = 16,
                RowMember = 1,
                ColumnMember = 7,
                GroupMember = 2,
                pm = pm
            };
            bc[16].SetValue(Grid.RowProperty, 1);
            bc[16].SetValue(Grid.ColumnProperty, 1);
            bc[16].SetValue(FrameworkElement.NameProperty, "bc16");
            group2.Children.Add(bc[16]);

            bc[17] = new BoxControl
            {
                ArrayMember = 17,
                RowMember = 1,
                ColumnMember = 8,
                GroupMember = 2,
                pm = pm
            };
            bc[17].SetValue(Grid.RowProperty, 1);
            bc[17].SetValue(Grid.ColumnProperty, 2);
            bc[17].SetValue(FrameworkElement.NameProperty, "bc17");
            group2.Children.Add(bc[17]);

            bc[18] = new BoxControl
            {
                ArrayMember = 18,
                RowMember = 2,
                ColumnMember = 0,
                GroupMember = 0,
                pm = pm
            };
            bc[18].SetValue(Grid.RowProperty, 2);
            bc[18].SetValue(Grid.ColumnProperty, 0);
            bc[18].SetValue(FrameworkElement.NameProperty, "bc18");
            group0.Children.Add(bc[18]);

            bc[19] = new BoxControl
            {
                ArrayMember = 19,
                RowMember = 2,
                ColumnMember = 1,
                GroupMember = 0,
                pm = pm
            };
            bc[19].SetValue(Grid.RowProperty, 2);
            bc[19].SetValue(Grid.ColumnProperty, 1);
            bc[19].SetValue(FrameworkElement.NameProperty, "bc19");
            group0.Children.Add(bc[19]);

            bc[20] = new BoxControl
            {
                ArrayMember = 20,
                RowMember = 2,
                ColumnMember = 2,
                GroupMember = 0,
                pm = pm
            };
            bc[20].SetValue(Grid.RowProperty, 2);
            bc[20].SetValue(Grid.ColumnProperty, 2);
            bc[20].SetValue(FrameworkElement.NameProperty, "bc20");
            group0.Children.Add(bc[20]);

            bc[21] = new BoxControl
            {
                ArrayMember = 21,
                RowMember = 2,
                ColumnMember = 3,
                pm = pm,
                GroupMember = 1
            };
            bc[21].SetValue(Grid.RowProperty, 2);
            bc[21].SetValue(Grid.ColumnProperty, 0);
            bc[21].SetValue(FrameworkElement.NameProperty, "bc21");
            group1.Children.Add(bc[21]);

            bc[22] = new BoxControl
            {
                ArrayMember = 22,
                RowMember = 2,
                ColumnMember = 4,
                GroupMember = 1,
                pm = pm
            };
            bc[22].SetValue(Grid.RowProperty, 2);
            bc[22].SetValue(Grid.ColumnProperty, 1);
            bc[22].SetValue(FrameworkElement.NameProperty, "bc22");
            group1.Children.Add(bc[22]);

            bc[23] = new BoxControl
            {
                ArrayMember = 23,
                RowMember = 2,
                ColumnMember = 5,
                GroupMember = 1,
                pm = pm
            };
            bc[23].SetValue(Grid.RowProperty, 2);
            bc[23].SetValue(Grid.ColumnProperty, 2);
            bc[23].SetValue(FrameworkElement.NameProperty, "bc23");
            group1.Children.Add(bc[23]);

            bc[24] = new BoxControl
            {
                ArrayMember = 24,
                RowMember = 2,
                ColumnMember = 6,
                GroupMember = 2,
                pm = pm
            };
            bc[24].SetValue(Grid.RowProperty, 2);
            bc[24].SetValue(Grid.ColumnProperty, 0);
            bc[24].SetValue(FrameworkElement.NameProperty, "bc24");
            group2.Children.Add(bc[24]);

            bc[25] = new BoxControl
            {
                ArrayMember = 25,
                RowMember = 2,
                ColumnMember = 7,
                GroupMember = 2,
                pm = pm
            };
            bc[25].SetValue(Grid.RowProperty, 2);
            bc[25].SetValue(Grid.ColumnProperty, 1);
            bc[25].SetValue(FrameworkElement.NameProperty, "bc25");
            group2.Children.Add(bc[25]);

            bc[26] = new BoxControl
            {
                ArrayMember = 26,
                RowMember = 2,
                ColumnMember = 8,
                GroupMember = 2,
                pm = pm
            };
            bc[26].SetValue(Grid.RowProperty, 2);
            bc[26].SetValue(Grid.ColumnProperty, 2);
            bc[26].SetValue(FrameworkElement.NameProperty, "bc26");
            group2.Children.Add(bc[26]);

            bc[27] = new BoxControl
            {
                ArrayMember = 27,
                RowMember = 3,
                ColumnMember = 0,
                GroupMember = 3,
                pm = pm
            };
            bc[27].SetValue(Grid.RowProperty, 0);
            bc[27].SetValue(Grid.ColumnProperty, 0);
            bc[27].SetValue(FrameworkElement.NameProperty, "bc27");
            group3.Children.Add(bc[27]);

            bc[28] = new BoxControl
            {
                ArrayMember = 28,
                RowMember = 3,
                ColumnMember = 1,
                GroupMember = 3,
                pm = pm
            };
            bc[28].SetValue(Grid.RowProperty, 0);
            bc[28].SetValue(Grid.ColumnProperty, 1);
            bc[28].SetValue(FrameworkElement.NameProperty, "bc28");
            group3.Children.Add(bc[28]);

            bc[29] = new BoxControl
            {
                ArrayMember = 29,
                RowMember = 3,
                ColumnMember = 2,
                GroupMember = 3,
                pm = pm
            };
            bc[29].SetValue(Grid.RowProperty, 0);
            bc[29].SetValue(Grid.ColumnProperty, 2);
            bc[29].SetValue(FrameworkElement.NameProperty, "bc29");
            group3.Children.Add(bc[29]);

            bc[30] = new BoxControl
            {
                ArrayMember = 30,
                RowMember = 3,
                ColumnMember = 3,
                GroupMember = 4,
                pm = pm
            };
            bc[30].SetValue(Grid.RowProperty, 0);
            bc[30].SetValue(Grid.ColumnProperty, 0);
            bc[30].SetValue(FrameworkElement.NameProperty, "bc30");
            group4.Children.Add(bc[30]);

            bc[31] = new BoxControl
            {
                ArrayMember = 31,
                RowMember = 3,
                ColumnMember = 4,
                GroupMember = 4,
                pm = pm
            };
            bc[31].SetValue(Grid.RowProperty, 0);
            bc[31].SetValue(Grid.ColumnProperty, 1);
            bc[31].SetValue(FrameworkElement.NameProperty, "bc31");
            group4.Children.Add(bc[31]);

            bc[32] = new BoxControl
            {
                ArrayMember = 32,
                RowMember = 3,
                ColumnMember = 5,
                GroupMember = 4,
                pm = pm
            };
            bc[32].SetValue(Grid.RowProperty, 0);
            bc[32].SetValue(Grid.ColumnProperty, 2);
            bc[32].SetValue(FrameworkElement.NameProperty, "bc32");
            group4.Children.Add(bc[32]);

            bc[33] = new BoxControl
            {
                ArrayMember = 33,
                RowMember = 3,
                ColumnMember = 6,
                GroupMember = 5,
                pm = pm
            };
            bc[33].SetValue(Grid.RowProperty, 0);
            bc[33].SetValue(Grid.ColumnProperty, 0);
            bc[33].SetValue(FrameworkElement.NameProperty, "bc33");
            group5.Children.Add(bc[33]);

            bc[34] = new BoxControl
            {
                ArrayMember = 34,
                RowMember = 3,
                ColumnMember = 7,
                GroupMember = 5,
                pm = pm
            };
            bc[34].SetValue(Grid.RowProperty, 0);
            bc[34].SetValue(Grid.ColumnProperty, 1);
            bc[34].SetValue(FrameworkElement.NameProperty, "bc34");
            group5.Children.Add(bc[34]);

            bc[35] = new BoxControl
            {
                ArrayMember = 35,
                RowMember = 3,
                ColumnMember = 8,
                GroupMember = 5,
                pm = pm
            };
            bc[35].SetValue(Grid.RowProperty, 0);
            bc[35].SetValue(Grid.ColumnProperty, 2);
            bc[35].SetValue(FrameworkElement.NameProperty, "bc35");
            group5.Children.Add(bc[35]);

            bc[36] = new BoxControl
            {
                ArrayMember = 36,
                RowMember = 4,
                ColumnMember = 0,
                GroupMember = 3,
                pm = pm
            };
            bc[36].SetValue(Grid.RowProperty, 1);
            bc[36].SetValue(Grid.ColumnProperty, 0);
            bc[36].SetValue(FrameworkElement.NameProperty, "bc36");
            group3.Children.Add(bc[36]);

            bc[37] = new BoxControl
            {
                ArrayMember = 37,
                RowMember = 4,
                ColumnMember = 1,
                GroupMember = 3,
                pm = pm
            };
            bc[37].SetValue(Grid.RowProperty, 1);
            bc[37].SetValue(Grid.ColumnProperty, 1);
            bc[37].SetValue(FrameworkElement.NameProperty, "bc37");
            group3.Children.Add(bc[37]);

            bc[38] = new BoxControl
            {
                ArrayMember = 38,
                RowMember = 4,
                ColumnMember = 2,
                GroupMember = 3,
                pm = pm
            };
            bc[38].SetValue(Grid.RowProperty, 1);
            bc[38].SetValue(Grid.ColumnProperty, 2);
            bc[38].SetValue(FrameworkElement.NameProperty, "bc38");
            group3.Children.Add(bc[38]);

            bc[39] = new BoxControl
            {
                ArrayMember = 39,
                RowMember = 4,
                ColumnMember = 3,
                GroupMember = 4,
                pm = pm
            };
            bc[39].SetValue(Grid.RowProperty, 1);
            bc[39].SetValue(Grid.ColumnProperty, 0);
            bc[39].SetValue(FrameworkElement.NameProperty, "bc39");
            group4.Children.Add(bc[39]);

            bc[40] = new BoxControl
            {
                ArrayMember = 40,
                RowMember = 4,
                ColumnMember = 4,
                GroupMember = 4,
                pm = pm
            };
            bc[40].SetValue(Grid.RowProperty, 1);
            bc[40].SetValue(Grid.ColumnProperty, 1);
            bc[40].SetValue(FrameworkElement.NameProperty, "bc40");
            group4.Children.Add(bc[40]);

            bc[41] = new BoxControl
            {
                ArrayMember = 41,
                RowMember = 4,
                ColumnMember = 5,
                GroupMember = 4,
                pm = pm
            };
            bc[41].SetValue(Grid.RowProperty, 1);
            bc[41].SetValue(Grid.ColumnProperty, 2);
            bc[41].SetValue(FrameworkElement.NameProperty, "bc41");
            group4.Children.Add(bc[41]);

            bc[42] = new BoxControl
            {
                ArrayMember = 42,
                RowMember = 4,
                ColumnMember = 6,
                GroupMember = 5,
                pm = pm
            };
            bc[42].SetValue(Grid.RowProperty, 1);
            bc[42].SetValue(Grid.ColumnProperty, 0);
            bc[42].SetValue(FrameworkElement.NameProperty, "bc42");
            group5.Children.Add(bc[42]);

            bc[43] = new BoxControl
            {
                ArrayMember = 43,
                RowMember = 4,
                ColumnMember = 7,
                GroupMember = 5,
                pm = pm
            };
            bc[43].SetValue(Grid.RowProperty, 1);
            bc[43].SetValue(Grid.ColumnProperty, 1);
            bc[43].SetValue(FrameworkElement.NameProperty, "bc43");
            group5.Children.Add(bc[43]);

            bc[44] = new BoxControl
            {
                ArrayMember = 44,
                RowMember = 4,
                ColumnMember = 8,
                GroupMember = 5,
                pm = pm
            };
            bc[44].SetValue(Grid.RowProperty, 1);
            bc[44].SetValue(Grid.ColumnProperty, 2);
            bc[44].SetValue(FrameworkElement.NameProperty, "bc44");
            group5.Children.Add(bc[44]);

            bc[45] = new BoxControl
            {
                ArrayMember = 45,
                RowMember = 5,
                ColumnMember = 0,
                GroupMember = 3,
                pm = pm
            };
            bc[45].SetValue(Grid.RowProperty, bc[45].RowMember);
            bc[45].SetValue(Grid.ColumnProperty, bc[45].ColumnMember);
            bc[45].SetValue(FrameworkElement.NameProperty, "bc45");
            group3.Children.Add(bc[45]);

            bc[46] = new BoxControl
            {
                ArrayMember = 46,
                RowMember = 5,
                ColumnMember = 1,
                GroupMember = 3,
                pm = pm
            };
            bc[46].SetValue(Grid.RowProperty, bc[46].RowMember);
            bc[46].SetValue(Grid.ColumnProperty, bc[46].ColumnMember);
            bc[46].SetValue(FrameworkElement.NameProperty, "bc46");
            group3.Children.Add(bc[46]);

            bc[47] = new BoxControl
            {
                ArrayMember = 47,
                RowMember = 5,
                ColumnMember = 2,
                GroupMember = 3,
                pm = pm
            };
            bc[47].SetValue(Grid.RowProperty, bc[47].RowMember);
            bc[47].SetValue(Grid.ColumnProperty, bc[47].ColumnMember);
            bc[47].SetValue(FrameworkElement.NameProperty, "bc47");
            group3.Children.Add(bc[47]);

            bc[48] = new BoxControl
            {
                ArrayMember = 48,
                RowMember = 5,
                ColumnMember = 3,
                GroupMember = 4,
                pm = pm
            };
            bc[48].SetValue(Grid.RowProperty, 2);
            bc[48].SetValue(Grid.ColumnProperty, 0);
            bc[48].SetValue(FrameworkElement.NameProperty, "bc48");
            group4.Children.Add(bc[48]);

            bc[49] = new BoxControl
            {
                ArrayMember = 49,
                RowMember = 5,
                ColumnMember = 4,
                GroupMember = 4,
                pm = pm
            };
            bc[49].SetValue(Grid.RowProperty, 2);
            bc[49].SetValue(Grid.ColumnProperty, 1);
            bc[49].SetValue(FrameworkElement.NameProperty, "bc49");
            group4.Children.Add(bc[49]);

            bc[50] = new BoxControl
            {
                ArrayMember = 50,
                RowMember = 5,
                ColumnMember = 5,
                GroupMember = 4,
                pm = pm
            };
            bc[50].SetValue(Grid.RowProperty, 2);
            bc[50].SetValue(Grid.ColumnProperty, 2);
            bc[50].SetValue(FrameworkElement.NameProperty, "bc50");
            group4.Children.Add(bc[50]);

            bc[51] = new BoxControl
            {
                ArrayMember = 51,
                RowMember = 5,
                ColumnMember = 6,
                GroupMember = 5,
                pm = pm
            };
            bc[51].SetValue(Grid.RowProperty, 2);
            bc[51].SetValue(Grid.ColumnProperty, 0);
            bc[51].SetValue(FrameworkElement.NameProperty, "bc51");
            group5.Children.Add(bc[51]);

            bc[52] = new BoxControl
            {
                ArrayMember = 52,
                RowMember = 5,
                ColumnMember = 7,
                GroupMember = 5,
                pm = pm
            };
            bc[52].SetValue(Grid.RowProperty, 2);
            bc[52].SetValue(Grid.ColumnProperty, 1);
            bc[52].SetValue(FrameworkElement.NameProperty, "bc52");
            group5.Children.Add(bc[52]);

            bc[53] = new BoxControl
            {
                ArrayMember = 53,
                RowMember = 5,
                ColumnMember = 8,
                GroupMember = 5,
                pm = pm
            };
            bc[53].SetValue(Grid.RowProperty, 2);
            bc[53].SetValue(Grid.ColumnProperty, 2);
            bc[53].SetValue(FrameworkElement.NameProperty, "bc53");
            group5.Children.Add(bc[53]);

            bc[54] = new BoxControl
            {
                ArrayMember = 54,
                RowMember = 6,
                ColumnMember = 0,
                GroupMember = 6,
                pm = pm
            };
            bc[54].SetValue(Grid.RowProperty, 0);
            bc[54].SetValue(Grid.ColumnProperty, 0);
            bc[54].SetValue(FrameworkElement.NameProperty, "bc54");
            group6.Children.Add(bc[54]);

            bc[55] = new BoxControl
            {
                ArrayMember = 55,
                RowMember = 6,
                ColumnMember = 1,
                GroupMember = 6,
                pm = pm
            };
            bc[55].SetValue(Grid.RowProperty, 0);
            bc[55].SetValue(Grid.ColumnProperty, 1);
            bc[55].SetValue(FrameworkElement.NameProperty, "bc55");
            group6.Children.Add(bc[55]);

            bc[56] = new BoxControl
            {
                ArrayMember = 56,
                RowMember = 6,
                ColumnMember = 2,
                GroupMember = 6,
                pm = pm
            };
            bc[56].SetValue(Grid.RowProperty, 0);
            bc[56].SetValue(Grid.ColumnProperty, 2);
            bc[56].SetValue(FrameworkElement.NameProperty, "bc56");
            group6.Children.Add(bc[56]);

            bc[57] = new BoxControl
            {
                ArrayMember = 57,
                RowMember = 6,
                ColumnMember = 3,
                GroupMember = 7,
                pm = pm
            };
            bc[57].SetValue(Grid.RowProperty, 0);
            bc[57].SetValue(Grid.ColumnProperty, 0);
            bc[57].SetValue(FrameworkElement.NameProperty, "bc57");
            group7.Children.Add(bc[57]);

            bc[58] = new BoxControl
            {
                ArrayMember = 58,
                RowMember = 6,
                ColumnMember = 4,
                GroupMember = 7,
                pm = pm
            };
            bc[58].SetValue(Grid.RowProperty, 0);
            bc[58].SetValue(Grid.ColumnProperty, 1);
            bc[58].SetValue(FrameworkElement.NameProperty, "bc58");
            group7.Children.Add(bc[58]);

            bc[59] = new BoxControl
            {
                ArrayMember = 59,
                RowMember = 6,
                ColumnMember = 5,
                GroupMember = 7,
                pm = pm
            };
            bc[59].SetValue(Grid.RowProperty, 0);
            bc[59].SetValue(Grid.ColumnProperty, 2);
            bc[59].SetValue(FrameworkElement.NameProperty, "bc59");
            group7.Children.Add(bc[59]);

            bc[60] = new BoxControl
            {
                ArrayMember = 60,
                RowMember = 6,
                ColumnMember = 6,
                GroupMember = 8,
                pm = pm
            };
            bc[60].SetValue(Grid.RowProperty, 0);
            bc[60].SetValue(Grid.ColumnProperty, 0);
            bc[60].SetValue(FrameworkElement.NameProperty, "bc60");
            group8.Children.Add(bc[60]);

            bc[61] = new BoxControl
            {
                ArrayMember = 61,
                RowMember = 6,
                ColumnMember = 7,
                GroupMember = 8,
                pm = pm
            };
            bc[61].SetValue(Grid.RowProperty, 0);
            bc[61].SetValue(Grid.ColumnProperty, 1);
            bc[61].SetValue(FrameworkElement.NameProperty, "bc61");
            group8.Children.Add(bc[61]);

            bc[62] = new BoxControl
            {
                ArrayMember = 62,
                RowMember = 6,
                ColumnMember = 8,
                GroupMember = 8,
                pm = pm
            };
            bc[62].SetValue(Grid.RowProperty, 0);
            bc[62].SetValue(Grid.ColumnProperty, 2);
            bc[62].SetValue(FrameworkElement.NameProperty, "bc62");
            group8.Children.Add(bc[62]);

            bc[63] = new BoxControl
            {
                ArrayMember = 63,
                RowMember = 7,
                ColumnMember = 0,
                GroupMember = 6,
                pm = pm
            };
            bc[63].SetValue(Grid.RowProperty, 1);
            bc[63].SetValue(Grid.ColumnProperty, 0);
            bc[63].SetValue(FrameworkElement.NameProperty, "bc63");
            group6.Children.Add(bc[63]);


            bc[64] = new BoxControl
            {
                ArrayMember = 64,
                RowMember = 7,
                ColumnMember = 1,
                GroupMember = 6,
                pm = pm
            };
            bc[64].SetValue(Grid.RowProperty, 1);
            bc[64].SetValue(Grid.ColumnProperty, 1);
            bc[64].SetValue(FrameworkElement.NameProperty, "bc64");
            group6.Children.Add(bc[64]);


            bc[65] = new BoxControl
            {
                ArrayMember = 65,
                RowMember = 7,
                ColumnMember = 2,
                GroupMember = 6,
                pm = pm
            };
            bc[65].SetValue(Grid.RowProperty, 1);
            bc[65].SetValue(Grid.ColumnProperty, 2);
            bc[65].SetValue(FrameworkElement.NameProperty, "bc65");
            group6.Children.Add(bc[65]);


            bc[66] = new BoxControl
            {
                ArrayMember = 66,
                RowMember = 7,
                ColumnMember = 3,
                GroupMember = 7,
                pm = pm
            };
            bc[66].SetValue(Grid.RowProperty, 1);
            bc[66].SetValue(Grid.ColumnProperty, 0);
            bc[66].SetValue(FrameworkElement.NameProperty, "bc66");
            group7.Children.Add(bc[66]);


            bc[67] = new BoxControl
            {
                ArrayMember = 67,
                RowMember = 7,
                ColumnMember = 4,
                GroupMember = 7,
                pm = pm
            };
            bc[67].SetValue(Grid.RowProperty, 1);
            bc[67].SetValue(Grid.ColumnProperty, 1);
            bc[67].SetValue(FrameworkElement.NameProperty, "bc67");
            group7.Children.Add(bc[67]);


            bc[68] = new BoxControl
            {
                ArrayMember = 68,
                RowMember = 7,
                ColumnMember = 5,
                GroupMember = 7,
                pm = pm
            };
            bc[68].SetValue(Grid.RowProperty, 1);
            bc[68].SetValue(Grid.ColumnProperty, 2);
            bc[68].SetValue(FrameworkElement.NameProperty, "bc68");
            group7.Children.Add(bc[68]);

            bc[69] = new BoxControl
            {
                ArrayMember = 69,
                RowMember = 7,
                ColumnMember = 6,
                GroupMember = 8,
                pm = pm
            };
            bc[69].SetValue(Grid.RowProperty, 1);
            bc[69].SetValue(Grid.ColumnProperty, 0);
            bc[69].SetValue(FrameworkElement.NameProperty, "bc69");
            group8.Children.Add(bc[69]);

            bc[70] = new BoxControl
            {
                ArrayMember = 70,
                RowMember = 7,
                ColumnMember = 7,
                GroupMember = 8,
                pm = pm
            };
            bc[70].SetValue(Grid.RowProperty, 1);
            bc[70].SetValue(Grid.ColumnProperty, 1);
            bc[70].SetValue(FrameworkElement.NameProperty, "bc70");
            group8.Children.Add(bc[70]);

            bc[71] = new BoxControl
            {
                ArrayMember = 71,
                RowMember = 7,
                ColumnMember = 8,
                GroupMember = 8,
                pm = pm
            };
            bc[71].SetValue(Grid.RowProperty, 1);
            bc[71].SetValue(Grid.ColumnProperty, 2);
            bc[71].SetValue(FrameworkElement.NameProperty, "bc71");
            group8.Children.Add(bc[71]);

            bc[72] = new BoxControl
            {
                ArrayMember = 72,
                RowMember = 8,
                ColumnMember = 0,
                GroupMember = 6,
                pm = pm
            };
            bc[72].SetValue(Grid.RowProperty, 2);
            bc[72].SetValue(Grid.ColumnProperty, 0);
            bc[72].SetValue(FrameworkElement.NameProperty, "bc72");
            group6.Children.Add(bc[72]);


            bc[73] = new BoxControl
            {
                ArrayMember = 73,
                RowMember = 8,
                ColumnMember = 1,
                GroupMember = 6,
                pm = pm
            };
            bc[73].SetValue(Grid.RowProperty, 2);
            bc[73].SetValue(Grid.ColumnProperty, 1);
            bc[73].SetValue(FrameworkElement.NameProperty, "bc73");
            group6.Children.Add(bc[73]);

            bc[74] = new BoxControl
            {
                ArrayMember = 74,
                RowMember = 8,
                ColumnMember = 2,
                GroupMember = 6,
                pm = pm
            };
            bc[74].SetValue(Grid.RowProperty, 2);
            bc[74].SetValue(Grid.ColumnProperty, 2);
            bc[74].SetValue(FrameworkElement.NameProperty, "bc74");
            group6.Children.Add(bc[74]);

            bc[75] = new BoxControl
            {
                ArrayMember = 75,
                RowMember = 8,
                ColumnMember = 3,
                GroupMember = 7,
                pm = pm
            };
            bc[75].SetValue(Grid.RowProperty, 2);
            bc[75].SetValue(Grid.ColumnProperty, 0);
            bc[75].SetValue(FrameworkElement.NameProperty, "bc75");
            group7.Children.Add(bc[75]);


            bc[76] = new BoxControl
            {
                ArrayMember = 76,
                RowMember = 8,
                ColumnMember = 4,
                GroupMember = 7,
                pm = pm
            };
            bc[76].SetValue(Grid.RowProperty, 2);
            bc[76].SetValue(Grid.ColumnProperty, 1);
            bc[76].SetValue(FrameworkElement.NameProperty, "bc76");
            group7.Children.Add(bc[76]);


            bc[77] = new BoxControl
            {
                ArrayMember = 77,
                RowMember = 8,
                ColumnMember = 5,
                GroupMember = 7,
                pm = pm
            };
            bc[77].SetValue(Grid.RowProperty, 2);
            bc[77].SetValue(Grid.ColumnProperty, 2);
            bc[77].SetValue(FrameworkElement.NameProperty, "bc77");
            group7.Children.Add(bc[77]);


            bc[78] = new BoxControl
            {
                ArrayMember = 78,
                RowMember = 8,
                ColumnMember = 6,
                GroupMember = 8,
                pm = pm
            };
            bc[78].SetValue(Grid.RowProperty, 2);
            bc[78].SetValue(Grid.ColumnProperty, 0);
            bc[78].SetValue(FrameworkElement.NameProperty, "bc78");
            group8.Children.Add(bc[78]);

            bc[79] = new BoxControl
            {
                ArrayMember = 79,
                RowMember = 8,
                ColumnMember = 7,
                GroupMember = 8,
                pm = pm
            };
            bc[79].SetValue(Grid.RowProperty, 2);
            bc[79].SetValue(Grid.ColumnProperty, 1);
            bc[79].SetValue(FrameworkElement.NameProperty, "bc79");
            group8.Children.Add(bc[79]);


            bc[80] = new BoxControl
            {
                ArrayMember = 80,
                RowMember = 8,
                ColumnMember = 8,
                GroupMember = 8,
                pm = pm
            };
            bc[80].SetValue(Grid.RowProperty, 2);
            bc[80].SetValue(Grid.ColumnProperty, 2);
            bc[80].SetValue(FrameworkElement.NameProperty, "bc80");
            group8.Children.Add(bc[80]);
        }


    }
}
