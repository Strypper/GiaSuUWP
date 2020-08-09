using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gia_Sư.Components.PopUps
{
    public sealed partial class SignUpSuccess : ContentDialog
    {
        public SignUpSuccess()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
