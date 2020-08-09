using System.Numerics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Gia_Sư.Components
{
    public sealed partial class ExploreButton : UserControl
    {
        public ExploreButton()
        {
            this.InitializeComponent();
        }

        private void Hover(object sender, PointerRoutedEventArgs e)
        {
            MovingIcon.Translation = new Vector3(20, 0, 0);
        }

        private void Exitst(object sender, PointerRoutedEventArgs e)
        {
            MovingIcon.Translation = new Vector3(0, 0, 0);
        }
    }
}
