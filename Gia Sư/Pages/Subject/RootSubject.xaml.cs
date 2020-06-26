using Gia_Sư.Components.PopUps;
using Gia_Sư.Helpers.ResizeHelper;
using Gia_Sư.Models.AppTools;
using Gia_Sư.Models.SubjectData;
using Microsoft.Graphics.Canvas.Effects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


namespace Gia_Sư.Pages.Subject
{
    public sealed partial class RootSubject : Page
    {
        OverviewRequest selectedItem = new OverviewRequest();
        private int PageNumber = 0;
        private string BugDetail;
        private SubjectRequest sr = new SubjectRequest();
        public string GetRequestUrl(int PageNumber) => $"https://giasuapi.azurewebsites.net/api/SubjectControllers/RequestPage/{PageNumber}";
        public string GetRequestDetailUrl(int RequestNumber) => $"https://giasuapi.azurewebsites.net/api/SubjectControllers/RequestDetail/{RequestNumber}";
        public string SearchRequestUrl(string SubName) => $"https://giasuapi.azurewebsites.net/api/SubjectControllers/SearchSubject/{SubName}";
        public string FeedBackSubmitUrl = "https://giasuapi.azurewebsites.net/api/FeedbackControllers/CreateFeedBack";
        private static readonly HttpClientHandler handler = new HttpClientHandler() { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator };
        private readonly HttpClient httpClient = new HttpClient(handler);

        private bool HomeWorkVisible { get; set; }
        private bool PresentationVisible { get; set; }
        private bool LaboratoryVisible { get; set; }
        private bool FirstTime = false;

        //Blur Animation
        private Compositor compositor = Window.Current.Compositor;
        private SpriteVisual effectVisual;
        private CompositionEffectBrush effectBrush;
        private CompositionEffectFactory effectFactory;
        private SpringScalarNaturalMotionAnimation _springAnimation;
        public RootSubject()
        {
            this.InitializeComponent();
            PaginationControl.pageClick += PageClickEvent;
            //Verify User
            if (App.User.UserName == null)
            {
                CreateRequest.Visibility = Visibility.Collapsed;
                PushRequest.Visibility = Visibility.Collapsed;
                RequestList.Visibility = Visibility.Collapsed;
                RequestPrice.Visibility = Visibility.Collapsed;
                PersonalSchedule.Visibility = Visibility.Collapsed;
            }
            this.NavigationCacheMode = NavigationCacheMode.Required;
            //Animate Size Changed
            HotRequest.EnableImplicitAnimation(VisualPropertyType.Offset, 1400);
        }
        private async void Subject_ItemClick(object sender, ItemClickEventArgs e)
        {
            GridViewItem ClickedItem = SubjectGridView.ContainerFromItem(e.ClickedItem) as GridViewItem;

            if (ClickedItem != null)
            {
                selectedItem = ClickedItem.Content as OverviewRequest;

                //System.Diagnostics.Debug.WriteLine(selectedItem);
                //We only need to pass the image
                PersonThumbnail.ProfilePicture = new BitmapImage(new Uri(selectedItem.ProfileUrlImage));
                SubjectNameInDetail.Text = selectedItem.Sub;

                //Start the Connected Animation
                ConnectedAnimation ConnectedAnimation = SubjectGridView.PrepareConnectedAnimation("forwardAnimation", selectedItem, "ProfilePicture");
                ConnectedAnimation.Configuration = new GravityConnectedAnimationConfiguration();
                ConnectedAnimation.TryStart(destinationElement);

                //Animate the layout
                OverlayPopup.Visibility = Visibility.Visible;
                LeftSide.Translation = new Vector3(0, 0, 0);
                RightSide.Translation = new Vector3(0, 0, 0);
                DaysOfWeekField.Translation = new Vector3(0, 0, 0);
                CloseBtn.Translation = new Vector3(0, 0, 0);
                CloseBtn.Rotation = 90;

                //Get the detail info
                var response = await httpClient.GetAsync(GetRequestDetailUrl(selectedItem.RequestID));
                var result = await response.Content.ReadAsStringAsync();
                sr = JsonConvert.DeserializeObject<SubjectRequest>(result);

                //Get the Schedules
                //SchedulesData = sr.requestSchedules;
                ScheduleBoard.Schedules = sr.requestSchedules;
                ScheduleBoard.AnalyzeData();

                //Get the PayMentTime
                switch (sr.payMentTime)
                {
                    case 0:
                        XamlPaymentTime.Text = "Trả theo giờ/Tiền mặt";
                        break;
                    case 1:
                        XamlPaymentTime.Text = "Trả theo tuần/Tiền mặt";
                        break;
                    case 2:
                        XamlPaymentTime.Text = "Trả theo tháng/Tiền mặt";
                        break;
                }
                Price.Text = sr.VNDPrice;

                SubjectNameValue.Text = sr.name;
                StudyGroupValue.Text = sr.studyGroupName;
                StudyFieldValue.Text = sr.studyFieldName;
                TeacherValue.Text = sr.teacher;

                SchoolNameValue.Text = sr.schoolName;
                SchoolLocationValue.Text = sr.schoolAddress;
                CityValue.Text = sr.cityName;
                DistrictValue.Text = sr.districtName;

                DescriptionLoader.Visibility = Visibility.Collapsed;
                Description.Text = sr.description;
                Description.Visibility = Visibility.Visible;

                ImageLoader.Visibility = Visibility.Collapsed;
                SchoolLogo.Source = new BitmapImage(new Uri(sr.schoolLogo));
                SchoolLogo.Visibility = Visibility.Visible;

                HomeWorkVisible = sr.homework;
                PresentationVisible = sr.presentation;
                LaboratoryVisible = sr.laboratory;



                HomeWork.Visibility = HomeWorkVisible ? Visibility.Visible : Visibility.Collapsed;
                Presentation.Visibility = PresentationVisible ? Visibility.Visible : Visibility.Collapsed;
                Laboratory.Visibility = LaboratoryVisible ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        private async void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            ScheduleBoard.ClearData();
            LeftSide.Translation = new Vector3(-500, 0, 0);
            RightSide.Translation = new Vector3(500, 0, 0);
            DaysOfWeekField.Translation = new Vector3(0, 300, 0);
            CloseBtn.Translation = new Vector3(0, -300, 0);
            CloseBtn.Rotation = 0;

            SubjectGridView.ScrollIntoView(selectedItem, ScrollIntoViewAlignment.Default);
            SubjectGridView.UpdateLayout();

            ConnectedAnimation ConnectedAnimation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("backwardsAnimation", destinationElement);
            ConnectedAnimation.Completed += ConnectedAnimation_Completed;
            ConnectedAnimation.Configuration = new DirectConnectedAnimationConfiguration();
            await SubjectGridView.TryStartConnectedAnimationAsync(ConnectedAnimation, selectedItem, "ProfilePicture");


            await Task.Delay(400);

            Price.Text = "Đang Tải";
            XamlPaymentTime.Text = "Đang Tải";

            SubjectNameValue.Text = "Đang Tải";
            StudyGroupValue.Text = "Đang Tải";
            StudyFieldValue.Text = "Đang Tải";
            TeacherValue.Text = "Đang Tải";

            SchoolNameValue.Text = "Đang Tải";
            SchoolLocationValue.Text = "Đang Tải";
            CityValue.Text = "Đang Tải";
            DistrictValue.Text = "Đang Tải";

            DescriptionLoader.Visibility = Visibility.Visible;
            Description.Visibility = Visibility.Collapsed;

            ImageLoader.Visibility = Visibility.Visible;
            SchoolLogo.Visibility = Visibility.Collapsed;

            HomeWork.Visibility = Visibility.Collapsed;
            Presentation.Visibility = Visibility.Collapsed;
            Laboratory.Visibility = Visibility.Collapsed;
        }
        private void ConnectedAnimation_Completed(ConnectedAnimation sender, object args)
        {
            OverlayPopup.Visibility = Visibility.Collapsed;
        }
        private async void PersonalSchedule_Click(object sender, RoutedEventArgs e)
        {
            PersonalSchedulePopUp p = new PersonalSchedulePopUp() { };
            await p.ShowAsync();
        }
        private async void PageClickEvent(object sender, PaginationRouteEvent e)
        {
            GetOverViewSubjectRequest.IsActive = true;
            SubjectGridView.ItemsSource = await GetOverViewRequestsAsync(e.PageNumber);
            GetOverViewSubjectRequest.IsActive = false;
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (FirstTime == false)
            {
                //Create Blur Animation
                effectVisual = compositor.CreateSpriteVisual();
                var destinationBrush = compositor.CreateBackdropBrush();
                var graphicsEffect = new GaussianBlurEffect
                {
                    Name = "Blur",
                    BlurAmount = 0f,
                    BorderMode = EffectBorderMode.Hard,
                    Source = new CompositionEffectSourceParameter("Background")
                };
                effectFactory = compositor.CreateEffectFactory(graphicsEffect, new[] { "Blur.BlurAmount" });
                effectBrush = effectFactory.CreateBrush();
                effectBrush.SetSourceParameter("Background", destinationBrush);
                effectVisual.Brush = effectBrush;
                ResizeVisual();
                ElementCompositionPreview.SetElementChildVisual(BlurLayout, effectVisual);
                _springAnimation = compositor.CreateSpringScalarAnimation();
                _springAnimation.Period = TimeSpan.FromSeconds(0.5);
                _springAnimation.DampingRatio = 0.75f;



                _springAnimation.FinalValue = 30f;
                effectBrush.StartAnimation("Blur.BlurAmount", _springAnimation);
                await Task.Delay(150);
                GetOverViewSubjectRequest.IsActive = true;
                SubjectGridView.ItemsSource = await GetOverViewRequestsAsync(0);
                await Task.Delay(3000);
                GetOverViewSubjectRequest.IsActive = false;
                FirstTime = true;
                _springAnimation.FinalValue = 0f;
                effectBrush.StartAnimation("Blur.BlurAmount", _springAnimation);
                await Task.Delay(2000);
                BlurLayout.Visibility = Visibility.Collapsed;
            }
        }
        private void ResizeVisual()
        {
            if (effectVisual == null) return;
            effectVisual.Size = new Vector2((float)BlurLayout.ActualWidth, (float)BlurLayout.ActualHeight);
        }
        private async Task<ObservableCollection<OverviewRequest>> GetOverViewRequestsAsync(int page)
        {
            PageNumber = page;
            var response = await httpClient.GetAsync(GetRequestUrl(PageNumber));
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ObservableCollection<OverviewRequest>>(result);
        }
        private async Task<ObservableCollection<OverviewRequest>> SearchSubjectRequest(string subname)
        {
            var response = await httpClient.GetAsync(SearchRequestUrl(subname));
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ObservableCollection<OverviewRequest>>(result);
        }
        private async void RefreshContainer_RefreshRequested(RefreshContainer sender, RefreshRequestedEventArgs args)
        {
            GetOverViewSubjectRequest.IsActive = true;
            await GetOverViewRequestsAsync(0);
            SubjectFinder.Text = "";
            GetOverViewSubjectRequest.IsActive = false;
        }
        private async void CreateRequest_Click(object sender, RoutedEventArgs e)
        {
            RequestSubject rs = new RequestSubject();
            rs.PrimaryButtonClick += SubmitEvent;
            await rs.ShowAsync();
        }
        private async void SubmitEvent(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (PageNumber == 0)
            {
                await GetOverViewRequestsAsync(0);
            }
            //Start Animation
            await StartAnimation();
        }
        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            GetOverViewSubjectRequest.IsActive = true;
            SubjectGridView.ItemsSource = await SearchSubjectRequest(SubjectFinder.Text);
            GetOverViewSubjectRequest.IsActive = false;
        }
        private async void RefreshPage_Click(object sender, RoutedEventArgs e)
        {
            GetOverViewSubjectRequest.IsActive = true;
            await GetOverViewRequestsAsync(0);
            SubjectFinder.Text = "";
            GetOverViewSubjectRequest.IsActive = false;
        }
        private async void SubmitFeedBack_Click(object sender, RoutedEventArgs e)
        {
            if(PlatformCombobox.SelectedItem == null)
            {
                PlatformCombobox.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                PlatformCombobox.Header = "Xin hãy chọn nền tảng của bạn";
            }
            else
            {
                BugLoadingRing.IsActive = true;
                BugDetailBox.Document.GetText(TextGetOptions.None, out BugDetail);
                var values = new Dictionary<string, string>
                    {
                       {"title" , BugTitle.Text},
                       {"detail", BugDetail},
                       {"platform", PlatformCombobox.SelectedItem.ToString()},
                       {"timeUpload", DateTime.Now.ToString()}
                    };
                var content = new StringContent(JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Token.token);
                var response = await httpClient.PostAsync(FeedBackSubmitUrl, content);
                string Status = response.StatusCode.ToString();
                Debug.WriteLine(Status);
                BugLoadingRing.IsActive = false;
                BugWindow.Hide();
                BugTitle.Text = "";
                BugDetailBox.Document.SetText(Windows.UI.Text.TextSetOptions.None, "");
                //Show Success Status
                await StartAnimation();
            }
        }
        private void PlatformCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlatformCombobox.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }
        private async Task StartAnimation()
        {
            BlurLayout.Visibility = Visibility.Visible;
            _springAnimation.FinalValue = 30f;
            effectBrush.StartAnimation("Blur.BlurAmount", _springAnimation);
            UniversalToast ut = new UniversalToast();
            ut.VerticalAlignment = VerticalAlignment.Bottom;
            ut.HorizontalAlignment = HorizontalAlignment.Center;
            OuterLayer.Children.Add(ut);
            await Task.Delay(3700);
            OuterLayer.Children.Remove(ut);
            _springAnimation.FinalValue = 0f;
            effectBrush.StartAnimation("Blur.BlurAmount", _springAnimation);
            await Task.Delay(1700);
            BlurLayout.Visibility = Visibility.Collapsed;
        }
    }
}
