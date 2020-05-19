using Gia_Sư.Helpers.ResizeHelper;
using Gia_Sư.Models.Services;
using Gia_Sư.Pages.Subject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gia_Sư.Pages.Home
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RootHome : Page
    {
        private List<Services> services;
        public RootHome()
        {
            this.InitializeComponent();
            Below.Opacity = 1;
            Welcome.Translation = new Vector3(0, 0, 0);
            services = ServiceList.GetServices();
            Items.ItemsSource = services;


            Benefit.EnableImplicitAnimation(VisualPropertyType.Offset, 1400);
            WelcomeText.EnableImplicitAnimation(VisualPropertyType.Offset, 1400);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {

        }

        //rootFrame.GoBack(new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Jump(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RootSubject));
        }
    }
}
