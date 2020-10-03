using Gia_Sư.Models.Person;
using Gia_Sư.Pages.SmartZone;
using Gia_Sư.Pages.Stuhub;
using Gia_Sư.Pages.Tutor;
using Gia_Sư.Pages.Tutor.CollegeSubject;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
                }
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                    //rootFrame.Navigate(typeof(TutorPage), e.Arguments);
                    //rootFrame.Navigate(typeof(MainPage), e.Arguments);
                    //rootFrame.Navigate(typeof(CollegeRequestSubject), e.Arguments);
                }

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
