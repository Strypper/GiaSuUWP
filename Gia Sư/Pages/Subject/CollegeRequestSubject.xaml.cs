using Gia_Sư.Components.PopUps;
using Gia_Sư.Models.AppTools;
using Gia_Sư.Models.CollegeSubjectData;
using Gia_Sư.Models.Services.CollegeStudyGroup;
using Microsoft.Toolkit.Uwp.UI.Animations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gia_Sư.Pages.Subject
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
            if(ToggleFilterButton.IsChecked == true)
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
    }
}
