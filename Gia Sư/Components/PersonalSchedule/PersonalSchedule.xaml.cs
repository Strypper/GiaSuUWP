﻿using System;
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

namespace Gia_Sư.Components
{
    public sealed partial class PersonalSchedule : UserControl
    {
        public PersonalSchedule()
        {
            this.InitializeComponent();
        }

        private void AddButtonHoverEnter(object sender, PointerRoutedEventArgs e)
        {
        }

        private void AddButtonHoverExit(object sender, PointerRoutedEventArgs e)
        {
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            int row = Grid.GetRow(b);
            int column = Grid.GetColumn(b);
            System.Diagnostics.Debug.WriteLine(row);
            System.Diagnostics.Debug.WriteLine(column);
        }
    }
}
