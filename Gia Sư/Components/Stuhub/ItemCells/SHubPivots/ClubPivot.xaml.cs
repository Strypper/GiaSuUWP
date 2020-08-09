using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Gia_Sư.Components.Stuhub.ItemCells.SHubPivots
{
    public sealed partial class ClubPivot : UserControl
    {
        public int ClubId
        {
            get { return (int)GetValue(ClubIdProperty); }
            set { SetValue(ClubIdProperty, value); }
        }
        public static readonly DependencyProperty ClubIdProperty =
            DependencyProperty.Register("ClubId", typeof(int), typeof(SHClubOverViewCell), null);



        public string ClubName
        {
            get { return (string)GetValue(ClubNameProperty); }
            set { SetValue(ClubNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClubName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClubNameProperty =
            DependencyProperty.Register("ClubName", typeof(string), typeof(SHClubOverViewCell), null);



        public string ClubLogoUrl
        {
            get { return (string)GetValue(ClubLogoUrlProperty); }
            set { SetValue(ClubLogoUrlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClubLogoUrl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClubLogoUrlProperty =
            DependencyProperty.Register("ClubLogoUrl", typeof(string), typeof(SHClubOverViewCell), null);

        public string ClubCoverImageUrl
        {
            get { return (string)GetValue(ClubCoverImageUrlProperty); }
            set { SetValue(ClubCoverImageUrlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClubCoverImageUrl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClubCoverImageUrlProperty =
            DependencyProperty.Register("ClubCoverImageUrl", typeof(string), typeof(SHClubOverViewCell), null);

        public string Color
        {
            get { return (string)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(string), typeof(SHClubOverViewCell), null);



        public DateTime DateStarted
        {
            get { return (DateTime)GetValue(DateStartedProperty); }
            set { SetValue(DateStartedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DateStarted.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateStartedProperty =
            DependencyProperty.Register("DateStarted", typeof(DateTime), typeof(SHClubOverViewCell), null);



        public string Location
        {
            get { return (string)GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Location.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LocationProperty =
            DependencyProperty.Register("Location", typeof(string), typeof(SHClubOverViewCell), null);

        public string ClubPhoneNumber
        {
            get { return (string)GetValue(ClubPhoneNumberProperty); }
            set { SetValue(ClubPhoneNumberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClubPhoneNumber.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClubPhoneNumberProperty =
            DependencyProperty.Register("ClubPhoneNumber", typeof(string), typeof(SHClubOverViewCell), null);

        public string Email
        {
            get { return (string)GetValue(EmailProperty); }
            set { SetValue(EmailProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Email.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EmailProperty =
            DependencyProperty.Register("Email", typeof(string), typeof(SHClubOverViewCell), null);

        public string WebsiteUrl
        {
            get { return (string)GetValue(WebsiteUrlProperty); }
            set { SetValue(WebsiteUrlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WebsiteUrl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WebsiteUrlProperty =
            DependencyProperty.Register("WebsiteUrl", typeof(string), typeof(SHClubOverViewCell), null);


        public string Intro
        {
            get { return (string)GetValue(IntroProperty); }
            set { SetValue(IntroProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Info.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntroProperty =
            DependencyProperty.Register("Intro", typeof(string), typeof(SHClubOverViewCell), null);

        public string Detail
        {
            get { return (string)GetValue(DetailProperty); }
            set { SetValue(DetailProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Detail.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DetailProperty =
            DependencyProperty.Register("Detail", typeof(string), typeof(SHClubOverViewCell), null);

        public int Rating
        {
            get { return (int)GetValue(RatingProperty); }
            set { SetValue(RatingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Rating.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RatingProperty =
            DependencyProperty.Register("Rating", typeof(int), typeof(SHClubOverViewCell), null);

        public int Members
        {
            get { return (int)GetValue(MembersProperty); }
            set { SetValue(MembersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Members.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MembersProperty =
            DependencyProperty.Register("Members", typeof(int), typeof(SHClubOverViewCell), null);

        public int Projects
        {
            get { return (int)GetValue(ProjectsProperty); }
            set { SetValue(ProjectsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Projects.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProjectsProperty =
            DependencyProperty.Register("Projects", typeof(int), typeof(SHClubOverViewCell), null);

        public string Requirement
        {
            get { return (string)GetValue(RequirementProperty); }
            set { SetValue(RequirementProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Requirement.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RequirementProperty =
            DependencyProperty.Register("Requirement", typeof(string), typeof(SHClubOverViewCell), null);

        public int ClubPrice
        {
            get { return (int)GetValue(ClubPriceProperty); }
            set { SetValue(ClubPriceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClubPrice.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClubPriceProperty =
            DependencyProperty.Register("ClubPrice", typeof(int), typeof(SHClubOverViewCell), null);
        public ClubPivot()
        {
            this.InitializeComponent();
            //BelowGrid.Translation = new System.Numerics.Vector3(0, 100, 0);
        }

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            BelowGrid.Translation = new System.Numerics.Vector3(0, 0, 0);
            ClubNameText.Translation = new System.Numerics.Vector3(120, 20, 0);
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            BelowGrid.Translation = new System.Numerics.Vector3(0, 95, 0);
            ClubNameText.Translation = new System.Numerics.Vector3(0, 0, 0);
        }
    }
}
