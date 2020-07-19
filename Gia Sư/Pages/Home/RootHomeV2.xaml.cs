using Gia_Sư.Models.Stuhub;
using System.Collections.ObjectModel;
using System.Numerics;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gia_Sư.Pages.Home
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RootHomeV2 : Page
    {
        private ObservableCollection<UserStory> userStories;
        public RootHomeV2()
        {
            this.InitializeComponent();
            userStories = FakeUserStoryData.GetData();
            TopUserStories.ItemsSource = userStories;
            UserStoryPosts.ItemsSource = userStories;
        }

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
        }
    }
}
