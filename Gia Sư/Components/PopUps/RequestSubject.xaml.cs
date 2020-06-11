using Gia_Sư.Models;
using Gia_Sư.Models.AppTools;
using Gia_Sư.Models.Location;
using Gia_Sư.Models.SubjectData;
using Gia_Sư.Pages.Subject;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gia_Sư.Components.PopUps
{
    public sealed partial class RequestSubject : ContentDialog

    {
        private int CityId, DistrictId, GroupId;
        private string Descript;
        private readonly string CreateRequestUrl = "https://giasuapi.azurewebsites.net/api/SubjectControllers/CreateRequest";
        private static readonly HttpClientHandler handler = new HttpClientHandler() { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator };
        private readonly HttpClient httpClient = new HttpClient(handler);
        private readonly string CitiesUrl = "https://giasuapi.azurewebsites.net/api/VietNamLocation/CitiesList";
        private readonly string StudyGroupUrl = "https://giasuapi.azurewebsites.net/api/SubjectControllers/StudyGroupList";
        public string StudyFieldUrl(int groupid) => $"https://giasuapi.azurewebsites.net/api/SubjectControllers/StudyFieldList/{GroupId}";
        public string SchoolUrl(int districtid) => $"https://giasuapi.azurewebsites.net/api/SubjectControllers/SchoolList/{DistrictId}";
        public string DistrictUrl(int PageNumber) => $"https://giasuapi.azurewebsites.net/api/VietNamLocation/DistrictsList/{CityId}";
        private VietNamCity ObjectCity;
        private VietNamDistrict ObjectDistrict;
        private List<VietNamCity> VNCity;
        private List<VietNamDistrict> VNDistrict;
        private List<School> _Sc;

        private StudyGroup ObjectSG;
        private List<StudyGroup> SG;
        private List<StudyField> SF;
        private int ValidErr;

        //Delegate
        //public event EventHandler rootSubjectRefresh;

        private ObservableCollection<WeekDay> Choosentimes;

        public RequestSubject()
        {
            this.InitializeComponent();
        }
        private async void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.UriSource = new Uri(App.User.ProfileImageUrl);
            ProfileImage.ProfilePicture = bitmap;
            await GetCitiesAsync();
            await GetStudyGroupAsync();

            Choosentimes = new ObservableCollection<WeekDay>();
            ListOfDay.ItemsSource = Choosentimes;
            //WeekDay Combobox bind to the Enum
            var DaysOfWeek = Enum.GetValues(typeof(WeekDaysEnum)).Cast<WeekDaysEnum>();
            DayOfWeek.ItemsSource = DaysOfWeek.ToList();
        }
        private async Task GetCitiesAsync()
        {
            var response = await httpClient.GetAsync(CitiesUrl);
            var result = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine(result);
            VNCity = JsonConvert.DeserializeObject<List<VietNamCity>>(result);
            City.ItemsSource = VNCity;
            SchoolCity.ItemsSource = VNCity;
        }
        private async Task GetDistrictsAsync(int Cid, int who)
        {
            CityId = Cid;
            var response = await httpClient.GetAsync(DistrictUrl(CityId));
            var result = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine(result);
            VNDistrict = JsonConvert.DeserializeObject<List<VietNamDistrict>>(result);
            switch (who)
            {
                case 0:
                    District.ItemsSource = VNDistrict;
                    break;
                case 1:
                    SchoolDistrict.ItemsSource = VNDistrict;
                    break;
            }
        }
        private async Task GetSchoolsAsync(int District)
        {
            DistrictId = District;
            var response = await httpClient.GetAsync(SchoolUrl(DistrictId));
            var result = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine(result);
            _Sc = JsonConvert.DeserializeObject<List<School>>(result);
            School.ItemsSource = _Sc;
        }
        private async Task GetStudyGroupAsync()
        {
            var response = await httpClient.GetAsync(StudyGroupUrl);
            var result = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine(result);
            SG = JsonConvert.DeserializeObject<List<StudyGroup>>(result);
            StudyGroup.ItemsSource = SG;
        }
        private async Task GetStudyFieldAsync(int Gid)
        {
            GroupId = Gid;
            var response = await httpClient.GetAsync(StudyFieldUrl(GroupId));
            var result = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine(result);
            SF = JsonConvert.DeserializeObject<List<StudyField>>(result);
            StudyField.ItemsSource = SF;
        }
        private async void SchoolCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObjectCity = (VietNamCity)SchoolCity.SelectedItem;
            CityId = ObjectCity.cityID;
            await GetDistrictsAsync(CityId, 1);
        }

        private async void StudyGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObjectSG = (StudyGroup)StudyGroup.SelectedItem;
            GroupId = ObjectSG.StudyGroupID;
            await GetStudyFieldAsync(GroupId);
        }

        private async void SchoolDistrict_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObjectDistrict = (VietNamDistrict)SchoolDistrict.SelectedItem;
            DistrictId = ObjectDistrict.districtID;
            await GetSchoolsAsync(DistrictId);
        }

        private async void City_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObjectCity = (VietNamCity)City.SelectedItem;
            CityId = ObjectCity.cityID;
            await GetDistrictsAsync(CityId, 0);
        }
        private bool InfoValid()
        {
            foreach(ComboBox cb in Form.Children.OfType<ComboBox>())
            {
                if(cb.SelectedItem == null)
                {
                    cb.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                    cb.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                    ValidErr++;
                }
            }
            foreach (ComboBox cb in Form2.Children.OfType<ComboBox>())
            {
                if (cb.SelectedItem == null && cb != PartnerLocation)
                {
                    cb.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                    cb.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                    ValidErr++;
                }
            }
            if (ValidErr > 0)
            {
                ValidErr = 0;
                return false;
            } 
            return true;
        }

        private void TimeStart_SelectedTimeChanged(TimePicker sender, TimePickerSelectedValueChangedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine(TimeStart.Time);
            System.Diagnostics.Debug.WriteLine(TimeStart.Time.TotalMinutes);
        }

        private void TimeEnd_SelectedTimeChanged(TimePicker sender, TimePickerSelectedValueChangedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine(TimeEnd.Time);
            System.Diagnostics.Debug.WriteLine(TimeEnd.Time.TotalMinutes);
            TimeSpan TotalTime = TimeEnd.Time - TimeStart.Time;
            System.Diagnostics.Debug.WriteLine(TotalTime);
            System.Diagnostics.Debug.WriteLine(TotalTime.TotalMinutes);
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            WaitingBar.Visibility = Visibility.Visible;
            //Prepare popup component
            VietNamDistrict district = (VietNamDistrict)District.SelectedValue;
            VietNamCity city = (VietNamCity)City.SelectedValue;
            StudyGroup sg = (StudyGroup)StudyGroup.SelectedItem;
            StudyField sf = (StudyField)StudyField.SelectedItem;
            School sc = (School)School.SelectedItem;
            Description.Document.GetText(TextGetOptions.None, out Descript);
            //Validation
            if (InfoValid() == true)
            {
                try
                {
                    var value = new Dictionary<string, object>
                                {
                                    { "SubjectName", SubjectName.Text },
                                    { "StudyGroup", sg.StudyGroupID },
                                    { "StudyField", sf.StudyFieldID },
                                    { "SubjectTeacher", Teacher.Text},
                                    { "Price", Price.Value },
                                    { "LearningAddress", Address.Text },
                                    { "LearningDistrict", district.districtID },
                                    { "LearningCity", city.cityID },
                                    { "Description", Descript },
                                    { "SchoolID", sc.schoolID },
                                    { "HomeWork", HomeWork.IsChecked },
                                    { "Presentation", Presentation.IsChecked },
                                    { "Laboratory", Lab.IsChecked },
                                    { "WeekDays", Choosentimes }
                                };
                    var content = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.Token.token);
                    var response = await httpClient.PostAsync(CreateRequestUrl, content);
                    string Status = response.StatusCode.ToString();
                    var responseString = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine(Status);
                    System.Diagnostics.Debug.WriteLine(App.Token.token);
                    if (Status == "OK")
                    {
                        //Show Success Status
                        WaitingBar.Visibility = Visibility.Collapsed;
                        //Trigger the page refresh
                        //Delegate is not working demand a look
                        //OnRootSubjectPageRefresh();
                        //Close the popup
                        this.Hide();
                    }
                    else
                    {
                        WaitingBar.ShowError = true;
                    }
                }
                catch (Exception)
                {
                    WaitingBar.ShowError = true;
                    throw new Exception("Some thing wrong");
                }
            }
            else
            {
                WaitingBar.ShowError = true;
            }
        }

        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Hide();
        }

        private void WeekDayAdd_Click(object sender, RoutedEventArgs e)
        {
            var dow = (WeekDaysEnum)DayOfWeek.SelectedItem; 
            WeekDay wd = new WeekDay(dow, TimeStart.Time, TimeEnd.Time);
            if (addWeekDay(wd) == false) 
            {
                TimeStart.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                TimeEnd.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                Add.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
            };
        }

        public bool addWeekDay(WeekDay wd)
        {
            if (Choosentimes.Count > 0)
            {
                if (wd.TimeStart >= wd.TimeEnd) return false;
                foreach (WeekDay WD in Choosentimes)
                {
                    if (wd.weekDay == WD.weekDay)
                    {
                        if (wd.TimeStart <= WD.TimeStart)
                        {
                            if (wd.TimeEnd >= WD.TimeStart)
                            {
                                System.Diagnostics.Debug.WriteLine("Conflict Time request time end {0} is bigger than existing time start {1}", wd.TimeEnd, WD.TimeStart);
                                return false;
                            }
                        }
                        if (wd.TimeStart >= WD.TimeStart)
                        {
                            if (wd.TimeStart <= WD.TimeEnd)
                            {
                                System.Diagnostics.Debug.WriteLine("Conflict Time request time start {0} is smaller than existing time end {1}", wd.TimeStart, WD.TimeEnd);
                                return false;
                            }
                        }
                    }
                }
            }
            Choosentimes.Add(wd);
            System.Diagnostics.Debug.WriteLine("No Conflict");
            System.Diagnostics.Debug.WriteLine("Added " + wd.weekDay + wd.TimeStart + wd.TimeEnd);
            return true;
        }
        //Delegate is not working demand a look
        //protected void OnRootSubjectPageRefresh()
        //{
        //    rootSubjectRefresh?.Invoke(this, EventArgs.Empty);
        //}
    }
}
