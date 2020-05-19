using Gia_Sư.Components;
using Gia_Sư.Components.PopUps;
using Gia_Sư.Components.SubjectUI;
using Gia_Sư.Helpers.ResizeHelper;
using Gia_Sư.Models.AppTools;
using Gia_Sư.Models.SubjectData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


namespace Gia_Sư.Pages.Subject
{
    public sealed partial class RootSubject : Page
    {
        public ObservableCollection<OverviewRequest> SubjectRequestList;
        OverviewRequest selectedItem;
        private int PageNumber = 0;
        private int RequestNumber;
        private SubjectRequest sr;
        public string GetRequestUrl(int PageNumber) => $"https://localhost:44316/api/SubjectControllers/RequestPage/{PageNumber}";
        public string GetRequestDetailUrl(int RequestNumber) => $"https://localhost:44316/api/SubjectControllers/RequestDetail/{RequestNumber}";

        private static readonly HttpClientHandler handler = new HttpClientHandler() { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator };
        private readonly HttpClient httpClient = new HttpClient(handler);

        private bool HomeWorkVisible { get; set; }
        private bool PresentationVisible { get; set; }
        private bool LaboratoryVisible { get; set; }


        public RootSubject()
        {
            this.InitializeComponent();

            sr = new SubjectRequest();

            //SubjectRequestList = SubjectData.getData();

            PaginationControl.pageClick += PageClickEvent;
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


                //Start the Connected Animation
                ConnectedAnimation ConnectedAnimation = SubjectGridView.PrepareConnectedAnimation("forwardAnimation", selectedItem, "ProfilePicture");
                ConnectedAnimation.Configuration = new GravityConnectedAnimationConfiguration();
                ConnectedAnimation.TryStart(destinationElement);

                //Animate the layout
                OverlayPopup.Visibility = Visibility.Visible;
                LeftSide.Translation = new System.Numerics.Vector3(0, 0, 0);
                RightSide.Translation = new System.Numerics.Vector3(0, 0, 0);
                DaysOfWeekField.Translation = new System.Numerics.Vector3(0, 0, 0);
                CloseBtn.Translation = new System.Numerics.Vector3(0, 0, 0);
                CloseBtn.Rotation = 90;

                //Get the detail info
                var response = await httpClient.GetAsync(GetRequestDetailUrl(selectedItem.RequestID));
                var result = await response.Content.ReadAsStringAsync();
                sr = JsonConvert.DeserializeObject<SubjectRequest>(result);

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
            LeftSide.Translation = new System.Numerics.Vector3(-500, 0, 0);
            RightSide.Translation = new System.Numerics.Vector3(500, 0, 0);
            DaysOfWeekField.Translation = new System.Numerics.Vector3(0, 300, 0);
            CloseBtn.Translation = new System.Numerics.Vector3(0, -300, 0);
            CloseBtn.Rotation = 0;

            SubjectGridView.ScrollIntoView(selectedItem, ScrollIntoViewAlignment.Default);
            SubjectGridView.UpdateLayout();

            ConnectedAnimation ConnectedAnimation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("backwardsAnimation", destinationElement);
            ConnectedAnimation.Completed += ConnectedAnimation_Completed;
            ConnectedAnimation.Configuration = new DirectConnectedAnimationConfiguration();
            await SubjectGridView.TryStartConnectedAnimationAsync(ConnectedAnimation, selectedItem, "ProfilePicture");


            await Task.Delay(500);
            Price.Text = "Đang Tải";

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
            Debug.WriteLine(e.PageNumber);
            await GetOverViewRequestsAsync(e.PageNumber);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
           //httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.Token);
           await GetOverViewRequestsAsync(0);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) 
        {

        }

        public async Task GetOverViewRequestsAsync(int page)
        {
            PageNumber = page;
            var response = await httpClient.GetAsync(GetRequestUrl(PageNumber));
            var result = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine(result);
            SubjectRequestList = JsonConvert.DeserializeObject<ObservableCollection<OverviewRequest>>(result);
            System.Diagnostics.Debug.WriteLine(SubjectRequestList);
            SubjectGridView.ItemsSource = SubjectRequestList;
        }
        

        private async void CreateRequest_Click(object sender, RoutedEventArgs e)
        {
            RequestSubject rs = new RequestSubject();
            await rs.ShowAsync();
        }
    }
}
