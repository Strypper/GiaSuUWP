using Gia_Sư.Models.AppTools;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
