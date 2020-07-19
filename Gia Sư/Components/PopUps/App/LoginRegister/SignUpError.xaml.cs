using Gia_Sư.Models.AppTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gia_Sư.Components.PopUps
{
    public sealed partial class SignUpError : ContentDialog
    {

        public SignUpError()
        {
            this.InitializeComponent(); 
        }
        public SignUpError(List<Validation> errorList)
        {
            this.InitializeComponent();
            ErrorList.ItemsSource = errorList;
        }

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
