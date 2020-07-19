using Gia_Sư.Models.CollegeSubjectData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

namespace Gia_Sư.Pages.Tutor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TutorPage : Page
    {
        private int PageNumber = 0;
        public string GetRequestUrl(int PageNumber) => $"https://giasuapi2.azurewebsites.net/api/CollegeSubjectControllers/RequestCollegeSubjectsPage/{PageNumber}";
        public string GetRequestDetailUrl(int RequestNumber) => $"https://giasuapi2.azurewebsites.net/api/CollegeSubjectControllers/RequestCollegeSubjectDetail/{RequestNumber}";
        public string SearchRequestUrl(string SubName) => $"https://giasuapi2.azurewebsites.net/api/CollegeSubjectControllers/SearchSubject/{SubName}";
        public string FeedBackSubmitUrl = "https://giasuapi2.azurewebsites.net/api/FeedbackControllers/CreateFeedBack";
        private readonly HttpClient httpClient = new HttpClient();
        public TutorPage()
        {
            this.InitializeComponent();
        }
        private async Task<ObservableCollection<OverviewCollegeRequest>> GetOverViewCollegeSubjectsRequestsAsync(int page)
        {
            PageNumber = page;
            var response = await httpClient.GetAsync(GetRequestUrl(PageNumber));
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ObservableCollection<OverviewCollegeRequest>>(result);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TutorsGridView.ItemsSource = await GetOverViewCollegeSubjectsRequestsAsync(0);
        }
    }
}
