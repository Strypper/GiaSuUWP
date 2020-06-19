using Gia_Sư.Models.Person;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Gia_Sư
{
    sealed partial class App : Application
    {
        public static Token Token = new Token();
        public static UserModel User = new UserModel();


        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState != ApplicationExecutionState.Terminated)
                {
                    bool loadState = (e.PreviousExecutionState == ApplicationExecutionState.Terminated);
                    splash extendedSplash = new splash(e.SplashScreen, loadState);
                    rootFrame.Content = extendedSplash;
                    Window.Current.Content = rootFrame;
                }
            }

            if (e.PrelaunchActivated == false)
            {
                Window.Current.Activate();
                ElementSoundPlayer.State = ElementSoundPlayerState.On;

                ElementSoundPlayer.Play(ElementSoundKind.Focus);
                ElementSoundPlayer.Play(ElementSoundKind.Invoke);
                ElementSoundPlayer.Play(ElementSoundKind.Show);
                ElementSoundPlayer.Play(ElementSoundKind.Hide);
                ElementSoundPlayer.Play(ElementSoundKind.MoveNext);
                ElementSoundPlayer.Play(ElementSoundKind.MovePrevious);
                ElementSoundPlayer.Play(ElementSoundKind.GoBack);

                var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
                coreTitleBar.ExtendViewIntoTitleBar = true;
            }
        }
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}
