using Gia_Sư.Helpers.ResizeHelper;
using System;
using System.ComponentModel;
using System.Numerics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Gia_Sư.Components.Stuhub
{
    public sealed partial class UserStoryComponent : UserControl
    {


        public string PersonName
        {
            get { return (string)GetValue(PersonNameProperty); }
            set { SetValue(PersonNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PersonName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PersonNameProperty =
            DependencyProperty.Register("PersonName", typeof(string), typeof(UserStoryComponent), null);




        public string ProfileUrl
        {
            get { return (string)GetValue(ProfileUrlProperty); }
            set { SetValue(ProfileUrlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProfileUrl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProfileUrlProperty =
            DependencyProperty.Register("ProfileUrl", typeof(string), typeof(UserStoryComponent), null);



        public string StoryImage
        {
            get { return (string)GetValue(StoryImageProperty); }
            set { SetValue(StoryImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PersonName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StoryImageProperty =
            DependencyProperty.Register("StoryImage", typeof(string), typeof(UserStoryComponent), null);



        public string Story
        {
            get { return (string)GetValue(StoryProperty); }
            set { SetValue(StoryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Story.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StoryProperty =
            DependencyProperty.Register("Story", typeof(string), typeof(UserStoryComponent), null);


        public UserStoryComponent()
        {
            this.InitializeComponent();
        }

        private void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton tB = sender as ToggleButton;
            if (tB != null)
            {
                if (tB.IsChecked == true)
                {
                    tB.IsChecked = true;
                    CommentSection.Visibility = Visibility.Visible;
                }
                else
                {
                    tB.IsChecked = false;
                    CommentSection.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
