using Gia_Sư.Models.SubjectData;
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
    public sealed partial class SubjectOverViewUI : UserControl
    {

        //public Person data { get { return this.DataContext as Person; } }


        public SubjectOverViewUI()
        {
            this.InitializeComponent();
            //this.DataContextChanged += (s, e) => Bindings.Update();
        }
    }
}
