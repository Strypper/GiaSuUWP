using Gia_Sư.Components.PopUps;
using Gia_Sư.Models.AppTools;
using Gia_Sư.Models.College;
using Gia_Sư.Models.Tutor.CollegeStudyGroup;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gia_Sư.Pages.Tutor.CollegeSubject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CollegeRequestSubject : Page
    {
        OverviewCollegeRequest selectedItem = new OverviewCollegeRequest();
        private int PageNumber = 0;
        private string BugDetail;
        private SubjectCollegeRequest sr = new SubjectCollegeRequest();
        private SubjectRequestSchedule srs = new SubjectRequestSchedule();
        private List<CollegeStudyGroupModel> csg = new List<CollegeStudyGroupModel>();
        private ObservableCollection<OverviewCollegeRequest> teachingList = new ObservableCollection<OverviewCollegeRequest>();
        public string GetRequestUrl(int PageNumber) => $"https://giasuapi2.azurewebsites.net/api/CollegeSubjectControllers/RequestCollegeSubjectsPage/{PageNumber}";
        public string GetRequestDetailUrl(int RequestNumber) => $"https://giasuapi2.azurewebsites.net/api/CollegeSubjectControllers/RequestCollegeSubjectDetail/{RequestNumber}";
        public string SearchRequestUrl(string SubName) => $"https://giasuapi2.azurewebsites.net/api/CollegeSubjectControllers/SearchSubject/{SubName}";
        public string FeedBackSubmitUrl = "https://giasuapi2.azurewebsites.net/api/FeedbackControllers/CreateFeedBack";
        private readonly HttpClient httpClient = new HttpClient();

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

        public CollegeRequestSubject()
        {
            this.InitializeComponent();
            PaginationControl.pageClick += PageClickEvent;
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ExecuteAnimation();
            csg = CollegeStudyGroupData.GetData();
            StudyGroupFilter.ItemsSource = csg;
            TeachingList.ItemsSource = teachingList;
            CollegeSubjectRequestGridView.ItemsSource = await GetData(0);
        }

        private async Task<ObservableCollection<OverviewCollegeRequest>> GetData(int page)
        {
            PageNumber = page;
            var response = await httpClient.GetAsync(GetRequestUrl(PageNumber));
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ObservableCollection<OverviewCollegeRequest>>(result);
        }

        private void ExecuteAnimation()
        {
            MainLayout.Translation = new Vector3(0, 60, 0);
        }
        private async void PageClickEvent(object sender, PaginationRouteEvent e)
        {
            //GetOverViewSubjectRequest.IsActive = true;
            CollegeSubjectRequestGridView.ItemsSource = await GetData(e.PageNumber);
            //GetOverViewSubjectRequest.IsActive = false;
        }
        private void ToggleFilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (ToggleFilterButton.IsChecked == true)
            {
                MainLayout.Translation = new Vector3(0, 120, 0);
                ToggleFilterButton.Rotation = 180;
            }
            else
            {
                MainLayout.Translation = new Vector3(0, 60, 0);
                ToggleFilterButton.Rotation = 0;
            }
        }

        private void RefreshContainer_RefreshRequested(RefreshContainer sender, RefreshRequestedEventArgs args)
        {

        }

        private void CollegeSubjectRequestGridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void CollegeSubjectRequestGridView_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            OverviewCollegeRequest collegerequest = e.Items[0] as OverviewCollegeRequest;
            e.Data.Properties.Add("CollegeRequest", collegerequest);
            e.Data.RequestedOperation = DataPackageOperation.Copy;
        }

        private void CollegeSubjectRequestGridView_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }
        private void TeachingList_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }
        private void TeachingList_DragEnter(object sender, DragEventArgs e)
        {
            e.DragUIOverride.IsGlyphVisible = false;
        }
        private async void TeachingList_Drop(object sender, DragEventArgs e)
        {
            var element = e.DataView.Properties["CollegeRequest"] as OverviewCollegeRequest;
            teachingList.Add(element);
        }
    }
}
