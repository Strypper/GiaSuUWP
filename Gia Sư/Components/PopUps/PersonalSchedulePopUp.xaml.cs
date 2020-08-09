using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gia_Sư.Components.PopUps
{
    public sealed partial class PersonalSchedulePopUp : ContentDialog
    {
        public PersonalSchedulePopUp()
        {
            this.InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Dialog.Hide();
        }

        private async void Update_ClickAsync(object sender, RoutedEventArgs e)
        {

        }
    }
}
