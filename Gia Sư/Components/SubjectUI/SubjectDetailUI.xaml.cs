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

namespace Gia_Sư.Components.SubjectUI
{
    public sealed partial class SubjectDetailUI : UserControl
    {


        //public string PersonImageURL
        //{
        //    get { return (string)GetValue(PersonImageURLProperty); }
        //    set { SetValue(PersonImageURLProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PersonImageURL.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PersonImageURLProperty =
        //    DependencyProperty.Register("PersonImageURL", typeof(string), typeof(string), null);


        public SubjectDetailUI()
        {
            this.InitializeComponent();
        }
    }
}
