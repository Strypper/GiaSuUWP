using Gia_Sư.Models.SmartZone;
using Microsoft.Toolkit.Uwp.UI.Controls.TextToolbarSymbols;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gia_Sư.Pages.SmartZone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SmartZone : Page
    {
        private readonly List<Models.SmartZone.SmartZone> smartZones = new List<Models.SmartZone.SmartZone>();
        public SmartZone()
        {
            this.InitializeComponent();

            smartZones = FakeLocationData.GetData();
            LocationList.ItemsSource = smartZones;
        }
    }
}
