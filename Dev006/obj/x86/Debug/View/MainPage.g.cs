﻿#pragma checksum "E:\dev\TrashDev\Dev006\View\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "12789A6227FF00D0E83AC8CABCB7955C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dev006.View
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // View\MainPage.xaml line 12
                {
                    this.pm = (global::Dev006.ViewModel.PuzzleManager)(target);
                }
                break;
            case 3: // View\MainPage.xaml line 25
                {
                    this.EntireGrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 4: // View\MainPage.xaml line 242
                {
                    this.solvedText = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 5: // View\MainPage.xaml line 239
                {
                    this.statusText = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6: // View\MainPage.xaml line 232
                {
                    this.PeekAllButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 7: // View\MainPage.xaml line 235
                {
                    this.AutoRemoveToggle = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                    ((global::Windows.UI.Xaml.Controls.CheckBox)this.AutoRemoveToggle).Click += this.AutoRemoveToggle_Click;
                }
                break;
            case 8: // View\MainPage.xaml line 36
                {
                    this.group0 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 9: // View\MainPage.xaml line 58
                {
                    this.group1 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 10: // View\MainPage.xaml line 79
                {
                    this.group2 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 11: // View\MainPage.xaml line 100
                {
                    this.group3 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 12: // View\MainPage.xaml line 121
                {
                    this.group4 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 13: // View\MainPage.xaml line 142
                {
                    this.group5 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 14: // View\MainPage.xaml line 163
                {
                    this.group6 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 15: // View\MainPage.xaml line 184
                {
                    this.group7 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 16: // View\MainPage.xaml line 205
                {
                    this.group8 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
