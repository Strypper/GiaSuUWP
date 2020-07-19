using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Gia_Sư.Components.College
{
    public sealed partial class CollegeSubjectRequestOverViewCell : UserControl
    {


        public string SchoolLogoUrl
        {
            get { return (string)GetValue(SchoolLogoUrlProperty); }
            set { SetValue(SchoolLogoUrlProperty, value); }
        }

        public static readonly DependencyProperty SchoolLogoUrlProperty =
            DependencyProperty.Register("SchoolLogoUrl", typeof(string), typeof(CollegeSubjectRequestOverViewCell), null);

        public string SchoolName
        {
            get { return (string)GetValue(SchoolNameProperty); }
            set { SetValue(SchoolNameProperty, value); }
        }

        public static readonly DependencyProperty SchoolNameProperty =
            DependencyProperty.Register("SchoolName", typeof(string), typeof(CollegeSubjectRequestOverViewCell), null);

        public string SubjectName
        {
            get { return (string)GetValue(SubjectNameProperty); }
            set { SetValue(SubjectNameProperty, value); }
        }

        public static readonly DependencyProperty SubjectNameProperty =
            DependencyProperty.Register("SubjectName", typeof(string), typeof(CollegeSubjectRequestOverViewCell), null);

        public string Price
        {
            get { return (string)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }


        // Using a DependencyProperty as the backing store for Price.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PriceProperty =
            DependencyProperty.Register("Price", typeof(string), typeof(CollegeSubjectRequestOverViewCell), null);



        public int PaymentTimeType
        {
            get { return (int)GetValue(PaymentTimeTypeProperty); }
            set { SetValue(PaymentTimeTypeProperty, value); }
        }

        public static readonly DependencyProperty PaymentTimeTypeProperty =
            DependencyProperty.Register("PaymentTimeType", typeof(int), typeof(CollegeSubjectRequestOverViewCell), null);



        public string PaymentType { get; set; }




        public CollegeSubjectRequestOverViewCell()
        {
            this.InitializeComponent();
            switch (PaymentTimeType)
            {
                case 0:
                    PaymentType = "Theo giờ";
                    break;
                case 1:
                    PaymentType = "Theo tuần";
                    break;
                case 2:
                    PaymentType = "Theo tháng";
                    break;
            }
        }

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            MainLayout.Background = new SolidColorBrush(Color.FromArgb(150, 37, 170, 225));
            SubjectRequest.Scale = new Vector3(1.2f, 1.2f, 1);
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            MainLayout.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            SubjectRequest.Scale = new Vector3(1f, 1f, 1);
        }
    }
}
