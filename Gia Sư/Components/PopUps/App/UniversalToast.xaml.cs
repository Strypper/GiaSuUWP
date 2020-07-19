using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

namespace Gia_Sư.Components.PopUps
{
    public sealed partial class UniversalToast : UserControl
    {
        public UniversalToast()
        {
            this.InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Toast.Translation = new Vector3(0, -100, 0);
            while (BackgroundTask.Value < 100)
            {
                BackgroundTask.Value++;
                await Task.Delay(5);
            }
            IconGrid.Translation = new Vector3(0, -40, 0);
            await Task.Delay(1500);
            Toast.Translation = new Vector3(0, 0, 0);
        }
    }
}
