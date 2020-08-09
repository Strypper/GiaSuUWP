using Gia_Sư.Models.Stuhub;
using Gia_Sư.Models.Stuhub.Club;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gia_Sư.Pages.Stuhub
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StuhubHomePage : Page
    {
        private ObservableCollection<UserStory> userStories = new ObservableCollection<UserStory>();
        private List<ClubInfo> clubsList = new List<ClubInfo>();
        public StuhubHomePage()
        {
            this.InitializeComponent();
            userStories = FakeUserStoryData.GetData();
            clubsList = TestData.GetData();
            TopUserStories.ItemsSource = userStories;
            UserStoryPosts.ItemsSource = userStories;
            ClubList.ItemsSource = clubsList;
        }

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}
