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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gia_Sư
{
    using Gia_Sư.Models.Person;
    using Microsoft.Toolkit.Uwp.UI.Animations;
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    using Windows.ApplicationModel.Activation;
    using Windows.Storage;
    using Windows.UI.Core;
    partial class splash : Page
    {
        internal Rect splashImageRect; // Rect to store splash screen image coordinates.
        private SplashScreen mySplash; // Variable to hold the splash screen object.
        internal bool dismissed = false; // Variable to track splash screen dismissal status.
        internal Frame rootFrame;
        private readonly HttpClient httpClient = new HttpClient();

        public static ApplicationDataContainer localData = ApplicationData.Current.RoamingSettings;

        public splash(SplashScreen splashscreen, bool loadState)
        {
            this.InitializeComponent();
            // Listen for window resize events to reposition the extended splash screen image accordingly.
            // This ensures that the extended splash screen formats properly in response to window resizing.
            Window.Current.SizeChanged += new WindowSizeChangedEventHandler(ExtendedSplash_OnResize);

            mySplash = splashscreen;
            if (mySplash != null)
            {
                // Register an event handler to be executed when the splash screen has been dismissed.
                mySplash.Dismissed += new TypedEventHandler<SplashScreen, Object>(DismissedEventHandler);
                // Retrieve the window coordinates of the splash screen image.
                splashImageRect = mySplash.ImageLocation;
                PositionImage();
            }
            // Create a Frame to act as the navigation context
            rootFrame = new Frame();

        }




        private async void DismissedEventHandler(SplashScreen sender, object args)
        {
            dismissed = true;
            DismissExtendedSplash();
            // Complete app setup operations here...

        }

        async void DismissExtendedSplash()
        {
            //await Task.Delay(5000);
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => {
                await theImage.Scale(0.5f, 0.5f, (float)theImage.ActualWidth / 2, (float)theImage.ActualHeight / 2, 200, 0, EasingType.Linear).StartAsync();
                var bounds = Window.Current.Bounds;
                double width = bounds.Width;
                double height = bounds.Height;
                float scaleX = (float)(width / 16);
                float scaleY = (float)(height / 9);
                var anim = theImage.Scale(scaleX, scaleY, (float)theImage.ActualWidth / 2, (float)theImage.ActualHeight / 2, 300, 0, EasingType.Linear);
                anim.Completed += Anim_Completed;
                await anim.StartAsync();
            });
        }

        private void Anim_Completed(object sender, AnimationSetCompletedEventArgs e)
        {
            rootFrame = new Frame();
            //Clear the Value
            //localData.Values.Remove("UserToken");
            if (localData.Values["UserToken"] != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", localData.Values["UserToken"].ToString());
                var response = httpClient.GetAsync("https://giasuapi2.azurewebsites.net/api/LoginRegister/GetUserInfo").GetAwaiter().GetResult();
                var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                App.User = JsonConvert.DeserializeObject<UserModel>(result);
                rootFrame.Content = new MainPage();
                Window.Current.Content = rootFrame;
                rootFrame.Navigate(typeof(MainPage));
            }
            else
            {
                rootFrame.Content = new LoginPageV2();
                Window.Current.Content = rootFrame;
                rootFrame.Navigate(typeof(LoginPageV2));
            }
        }

        private void ExtendedSplash_OnResize(object sender, WindowSizeChangedEventArgs e)
        {
            // Safely update the extended splash screen image coordinates. This function will be executed when a user resizes the window.
            if (mySplash != null)
            {
                // Update the coordinates of the splash screen image.
                splashImageRect = mySplash.ImageLocation;
                PositionImage();

                // If applicable, include a method for positioning a progress control.
                // PositionRing();
            }


        }

        void PositionImage()
        {
            theImage.SetValue(Canvas.LeftProperty, splashImageRect.X);
            theImage.SetValue(Canvas.TopProperty, splashImageRect.Y);
            theImage.Height = splashImageRect.Height;
            theImage.Width = splashImageRect.Width;
        }

        async void RestoreStateAsync(bool loadState)
        {
            if (loadState)
            {
                // code to load your app's state here
            }
        }

    }
}
