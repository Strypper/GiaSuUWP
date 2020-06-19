﻿using Gia_Sư.Components.PopUps;
using Gia_Sư.Pages;
using Gia_Sư.Pages.Home;
using Gia_Sư.Pages.Subject;
using Microsoft.Toolkit.Uwp.UI.Animations;
using Microsoft.Toolkit.Uwp.UI.Animations.Expressions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Gia_Sư
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Compositor compositor = Window.Current.Compositor;
        private Visual backVisual;
        private ScalarKeyFrameAnimation rotate;
        private bool _isCtrlKeyPressed;
        public MainPage()
        {
            this.InitializeComponent();
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
        }
        private void NavigationViewPanel_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
                Microsoft.UI.Xaml.Controls.NavigationViewItem item = args.SelectedItem as Microsoft.UI.Xaml.Controls.NavigationViewItem;
                NavView_Navigate(item);
        }
        private void NavView_Navigate(Microsoft.UI.Xaml.Controls.NavigationViewItem item)
        {
            switch (item.Name)
            {
                case "Home":
                    MainFrame.Navigate(typeof(RootHome));
                    break;
                case "Subject":
                    MainFrame.Navigate(typeof(RootSubject));
                    break;
            }
        }
        private void NavigationViewPanel_BackRequested(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewBackRequestedEventArgs args)
        {
            On_BackRequested();
        }
        private bool On_BackRequested()
        {
            Type page = MainFrame.SourcePageType;
            System.Diagnostics.Debug.WriteLine(page);
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
                return true;
            }
            return false;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(App.User.ProfileImageUrl == null)
            {
                User.ProfilePicture = null;
            }
            else
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.UriSource = new Uri(App.User.ProfileImageUrl);
                User.ProfilePicture = bitmap;
            }
            //Start the page
            MainFrame.Navigate(typeof(RootHome));
            //Animate the gear
            backVisual = ElementCompositionPreview.GetElementVisual(GearIcon);
            backVisual.Size = new System.Numerics.Vector2(20, 20);
            backVisual.CenterPoint = new System.Numerics.Vector3(backVisual.Size / 2, 0);

            rotate = compositor.CreateScalarKeyFrameAnimation();

            //var linear = compositor.CreateLinearEasingFunction();

            var startingValue = ExpressionValues.StartingValue.CreateScalarStartingValue();

            rotate.InsertExpressionKeyFrame(0.0f, startingValue);
            rotate.InsertExpressionKeyFrame(1.0f, startingValue + 360f);
            rotate.Duration = TimeSpan.FromMilliseconds(1000);
        }
        //Dectect what page on the Frame when it navigated
        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            var pageName = MainFrame.Content.GetType().Name;            
            if(pageName == "RootHome" || pageName == "RootSubject")
            {            
                //find menu item that has the matching tag
                var menuItem = NavigationViewPanel.MenuItems
                                         .OfType<Microsoft.UI.Xaml.Controls.NavigationViewItem>()
                                         .Where(item => item.Tag.ToString() == pageName)
                                         .First();

                //select
                NavigationViewPanel.SelectedItem = menuItem;

                NavigationViewPanel.IsBackButtonVisible = Microsoft.UI.Xaml.Controls.NavigationViewBackButtonVisible.Collapsed;
            }
            else NavigationViewPanel.IsBackButtonVisible = Microsoft.UI.Xaml.Controls.NavigationViewBackButtonVisible.Visible;


        }
        private async void Setting(object sender, RoutedEventArgs e)
        {
            Settings s = new Settings();
            await s.ShowAsync();
        }
        private void Settings_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            backVisual.StartAnimation(nameof(Visual.RotationAngleInDegrees), rotate);
        }
        private void NavigationViewPanel_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Control) _isCtrlKeyPressed = true;
            else if (_isCtrlKeyPressed)
            {
                switch (e.Key)
                {
                    case VirtualKey.S: Search.Focus(FocusState.Programmatic); break;
                    case VirtualKey.Number1: MainFrame.Navigate(typeof(RootHome)); break;
                    case VirtualKey.Number2: MainFrame.Navigate(typeof(RootSubject)); break;
                }
            }
        }
        private void NavigationViewPanel_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Control) _isCtrlKeyPressed = false;
        }
        private void User_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }
    }
}
