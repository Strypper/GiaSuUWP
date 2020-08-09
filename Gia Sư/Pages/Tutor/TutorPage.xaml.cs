using Gia_Sư.Models.College;
using Gia_Sư.Models.Tutor.CollegeStudyGroup;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Numerics;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gia_Sư.Pages.Tutor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TutorPage : Page
    {
        private int PageNumber = 0;
        public string GetTutorUrl(int PageNumber) => $"https://giasuapi2.azurewebsites.net/api/CollegeTutorControllers/RequestCollegeTutorsPage/{PageNumber}";
        public string GetTutorDetailUrl(int RequestNumber) => $"https://giasuapi2.azurewebsites.net/api/CollegeSubjectControllers/RequestCollegeSubjectDetail/{RequestNumber}";
        public string SearchRequestUrl(string SubName) => $"https://giasuapi2.azurewebsites.net/api/CollegeSubjectControllers/SearchSubject/{SubName}";
        public string FeedBackSubmitUrl = "https://giasuapi2.azurewebsites.net/api/FeedbackControllers/CreateFeedBack";
        private readonly HttpClient httpClient = new HttpClient();

        TutorCollegeRequest selectedItem = new TutorCollegeRequest();
        private List<CollegeStudyGroupModel> csg = new List<CollegeStudyGroupModel>();
        public TutorPage()
        {
            this.InitializeComponent();
            csg = CollegeStudyGroupData.GetData();
            StudyGroupFlipView.ItemsSource = csg;
            StudyGroupFlipView.Opacity = 1;
        }
        private async Task<ObservableCollection<TutorCollegeRequest>> GetOverViewCollegeTutorsRequestsAsync(int page)
        {
            PageNumber = page;
            var response = await httpClient.GetAsync(GetTutorUrl(PageNumber));
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ObservableCollection<TutorCollegeRequest>>(result);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TutorsGridView.ItemsSource = await GetOverViewCollegeTutorsRequestsAsync(0);
            TopTutorGridView.ItemsSource = await GetOverViewCollegeTutorsRequestsAsync(0);
            //await LoadMap();
        }
        //private async Task LoadMap()
        //{
        //    Geolocator geolocator = new Geolocator();
        //    Geoposition pos = await geolocator.GetGeopositionAsync();
        //    Geopoint myLocation = pos.Coordinate.Point;
        //    MapIcon mapIcon = new MapIcon { Location = myLocation, Title = "Vị trí của tôi", NormalizedAnchorPoint = new Point(0.5, 1), ZIndex = 0 };
        //    mapIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/AppImage/pin.png"));
            
        //    GiaSuMapControl.MapElements.Add(mapIcon);
        //    GiaSuMapControl.Center = myLocation;
        //    GiaSuMapControl.ZoomLevel = 12;
        //    GiaSuMapControl.LandmarksVisible = true;
        //}
        private async void TutorsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            GridViewItem ClickedItem = TutorsGridView.ContainerFromItem(e.ClickedItem) as GridViewItem;

            if (ClickedItem != null)
            {
                selectedItem = ClickedItem.Content as TutorCollegeRequest;

                //System.Diagnostics.Debug.WriteLine(selectedItem);
                //We only need to pass the image
                PersonThumbnail.ProfilePicture = new BitmapImage(new Uri(selectedItem.profileUrlImage));
                SubjectNameInDetail.Text = selectedItem.firstName;

                //Start the Connected Animation
                ConnectedAnimation ConnectedAnimation = TutorsGridView.PrepareConnectedAnimation("forwardAnimation", selectedItem, "ProfilePicture");
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
                //var response = await httpClient.GetAsync(GetRequestDetailUrl(selectedItem.RequestID));
                //var result = await response.Content.ReadAsStringAsync();
                //sr = JsonConvert.DeserializeObject<SubjectRequest>(result);

                ////Get the Schedules
                ////SchedulesData = sr.requestSchedules;
                //srs = new SubjectRequestSchedule();
                //srs.Schedules = sr.requestSchedules;
                //srs.AnalyzeData();
                //DaysOfWeek.Children.Add(srs);
                //DayOfWeekRing.IsActive = false;

                ////Get the PayMentTime
                //switch (sr.payMentTime)
                //{
                //    case 0:
                //        XamlPaymentTime.Text = "Trả theo giờ/Tiền mặt";
                //        break;
                //    case 1:
                //        XamlPaymentTime.Text = "Trả theo tuần/Tiền mặt";
                //        break;
                //    case 2:
                //        XamlPaymentTime.Text = "Trả theo tháng/Tiền mặt";
                //        break;
                //}
                //Price.Text = sr.VNDPrice;

                //SubjectNameValue.Text = sr.name;
                //StudyGroupValue.Text = sr.studyGroupName;
                //StudyFieldValue.Text = sr.studyFieldName;
                //TeacherValue.Text = sr.teacher;

                //SchoolNameValue.Text = sr.schoolName;
                //SchoolLocationValue.Text = sr.schoolAddress;
                //CityValue.Text = sr.cityName;
                //DistrictValue.Text = sr.districtName;

                //DescriptionLoader.Visibility = Visibility.Collapsed;
                //Description.Text = sr.description;
                //Description.Visibility = Visibility.Visible;

                //ImageLoader.Visibility = Visibility.Collapsed;
                //SchoolLogo.Source = new BitmapImage(new Uri(sr.schoolLogo));
                //SchoolLogo.Visibility = Visibility.Visible;

                //HomeWorkVisible = sr.homework;
                //PresentationVisible = sr.presentation;
                //LaboratoryVisible = sr.laboratory;



                //HomeWork.Visibility = HomeWorkVisible ? Visibility.Visible : Visibility.Collapsed;
                //Presentation.Visibility = PresentationVisible ? Visibility.Visible : Visibility.Collapsed;
                //Laboratory.Visibility = LaboratoryVisible ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        private async void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            LeftSide.Translation = new Vector3(-500, 0, 0);
            RightSide.Translation = new Vector3(500, 0, 0);
            DaysOfWeekField.Translation = new Vector3(0, 300, 0);
            CloseBtn.Translation = new Vector3(0, -300, 0);
            CloseBtn.Rotation = 0;

            TutorsGridView.ScrollIntoView(selectedItem, ScrollIntoViewAlignment.Default);
            TutorsGridView.UpdateLayout();

            ConnectedAnimation ConnectedAnimation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("backwardsAnimation", destinationElement);
            ConnectedAnimation.Completed += ConnectedAnimation_Completed;
            ConnectedAnimation.Configuration = new DirectConnectedAnimationConfiguration();
            await TutorsGridView.TryStartConnectedAnimationAsync(ConnectedAnimation, selectedItem, "ProfilePicture");


            await Task.Delay(400);
            //DaysOfWeek.Children.Remove(srs);
            DayOfWeekRing.IsActive = true;

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
    }
}
