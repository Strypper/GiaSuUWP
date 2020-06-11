using Gia_Sư.Models.AppTools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Printing;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Gia_Sư.Components
{
    public sealed partial class Pagination : UserControl
    {
        public event EventHandler<PaginationRouteEvent> pageClick;
        private int NumberOfRequest;
        //Get the request count
        private readonly string GetNumberOfPage = $"https://giasuapi.azurewebsites.net/api/SubjectControllers/NumberOfRecord";
        private static readonly HttpClientHandler handler = new HttpClientHandler() { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator };
        private readonly HttpClient httpClient = new HttpClient(handler);
        private bool FirstTriggerButton = true;

        public Pagination()
        {
            this.InitializeComponent();
            GetNOfPage();
        }

        private async Task GetNOfPage()
        { 
            var response = await httpClient.GetAsync(GetNumberOfPage);
            var result = await response.Content.ReadAsStringAsync();
            NumberOfRequest = JsonConvert.DeserializeObject<int>(result);
            double NumberOfPage = (double)NumberOfRequest / 18;
            for (int i = 0; i < NumberOfPage; i++)
            {
                ToggleButton tb = new ToggleButton();
                tb.Style = (Style)Application.Current.Resources["ToggleButtonRevealStyle"];
                tb.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
                tb.Margin = new Thickness(5);
                tb.Click += NavigatePage;
                tb.Content = i;
                Pages.Children.Add(tb);
                if (FirstTriggerButton == true) tb.IsChecked = true;
                FirstTriggerButton = false;
            }
        }

        protected async void NavigatePage(object sender, RoutedEventArgs e)
        {
            ToggleButton tb = sender as ToggleButton;
            int pagenum = System.Convert.ToInt32(tb.Content);
            foreach (ToggleButton toggle in Pages.Children)
            {
                if (toggle != tb)
                {
                    toggle.IsChecked = false;
                }
            }
            OnPageClick(pagenum);
        }
        protected void OnPageClick(int pn)
        {
            pageClick?.Invoke(this, new PaginationRouteEvent() { PageNumber = pn });
        }
    }
}
