using Gia_Sư.Components.PopUps;
using Gia_Sư.Helpers;
using Gia_Sư.Helpers.ResizeHelper;
using Gia_Sư.Models;
using Gia_Sư.Models.AppTools;
using Gia_Sư.Models.Location;
using Gia_Sư.Models.Person;
using Gia_Sư.Models.SubjectData;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gia_Sư
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPageV2 : Page
    {
        private string AzureProfileImageUrl;
        private string UserRole = "";
        private double PredictAge;
        private int CityId, DistrictId, GroupId;

        private readonly string RegisterUrl = "https://giasuapi2.azurewebsites.net/api/LoginRegister/Register";
        private readonly string LoginUrl = "https://giasuapi2.azurewebsites.net/api/LoginRegister/Login";
        private readonly string GetUserInfoUrl = "https://giasuapi2.azurewebsites.net/api/LoginRegister/GetUserInfo";
        private readonly string CitiesUrl = "https://giasuapi2.azurewebsites.net/api/VietNamLocation/CitiesList";
        private readonly string StudyGroupUrl = "https://giasuapi2.azurewebsites.net/api/SubjectControllers/StudyGroupList";

        private string DistrictUrl(int cityid) => $"https://giasuapi2.azurewebsites.net/api/VietNamLocation/DistrictsList/{CityId}";
        private string StudyFieldUrl(int groupid) => $"https://giasuapi2.azurewebsites.net/api/SubjectControllers/StudyFieldList/{GroupId}";
        private string SchoolUrl(int districtid) => $"https://giasuapi2.azurewebsites.net/api/SubjectControllers/SchoolList/{DistrictId}";

        private StorageFile userPhoto;
        private VietNamCity ObjectCity;
        private VietNamDistrict ObjectDistrict;
        private StudyGroup ObjectSG;

        private List<Validation> ValidForm = new List<Validation>();

        private List<VietNamCity> VNCity = new List<VietNamCity>();
        private List<VietNamDistrict> VNDistrict = new List<VietNamDistrict>();

        private List<StudyGroup> SG = new List<StudyGroup>();
        private List<StudyField> SF = new List<StudyField>();
        private List<School> _Sc = new List<School>();

        //private static readonly HttpClientHandler handler = new HttpClientHandler() { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator };
        private readonly HttpClient httpClient = new HttpClient();

        SignUpSuccess contentSuccess = new SignUpSuccess();

        //Color Bloom
        bool carefulPlz = false;
        bool stopDisposing = false;
        PropertySet _colorsByPivotItem;
        ColorBloomTransitionHelper transition, surroundButtonTransition;
        Queue<PivotItem> pendingTransitions = new Queue<PivotItem>();
        public LoginPageV2()
        {
            this.InitializeComponent();

            if (carefulPlz == false)
            {


                this.InitializeColors();
                this.InitializeTransitionHelper();
                this.Unloaded += ColorBloomTransition_Unloaded;
            }
            else
            {
                this.InitializeColors();
            }
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (carefulPlz == true)
            {
                this.InitializeTransitionHelper();
                this.Unloaded += ColorBloomTransition_Unloaded;
            }
            ExecuteAnimation();
            await Task.Delay(150);
            //City and District ComboBox
            await GetCitiesAsync();
            await GetStudyGroupAsync();
        }
        private void ExecuteAnimation()
        {
            LogoLayout.Translation = new Vector3(0, 0, 0);
            LogoImage.Scale = new Vector3(1, 1, 0);
            Mountain.Translation = new Vector3(0, 0, 0);


            Form.EnableImplicitAnimation(VisualPropertyType.Offset, 1400);
            Form.EnableFluidVisibilityAnimation(showFromScale: 0.6f, hideToScale: 0.8f, showDuration: 400, hideDuration: 250);
            GiaSuBrand.EnableImplicitAnimation(VisualPropertyType.Offset, 1400);
            GiaSuBrand.EnableFluidVisibilityAnimation(showFromScale: 0.6f, hideToScale: 0.8f, showDuration: 400, hideDuration: 250);
            GiaSuBrand.EnableImplicitAnimation(VisualPropertyType.Offset, 400);
            Right.EnableImplicitAnimation(VisualPropertyType.Offset, 1400);
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
            ExistingSchool.ItemsSource = _Sc;
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
        private async void BrowsePhoto(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            userPhoto = await picker.PickSingleFileAsync();
            if (userPhoto != null)
            {
                var filestream = await userPhoto.OpenAsync(FileAccessMode.Read);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.SetSource(filestream);
                ProfilePic.ProfilePicture = bitmapImage;
            }
        }
        private CloudStorageAccount createStorageAccountFromConnectionString()
        {
            var localSettings = ApplicationData.Current.LocalSettings;

            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=giasuprofileimagecloud;AccountKey=YPVAclmUAv1jfDRSVBoSumWlyFDNNdrcUOKN9cqSvcPbsG/YS85m5Jtr+1pvaLUCB8i6Cxb7bgoXrLn+p6Anrg==;EndpointSuffix=core.windows.net");
            }
            catch (FormatException)
            {
                WaitingRegisterBar.ShowError = true;
                throw new FormatException("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
            }
            catch (ArgumentException)
            {
                WaitingRegisterBar.ShowError = true;
                throw new ArgumentException("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
            }

            return storageAccount;
        }
        private async Task saveUserPhoto(StorageFile photo)
        {
            if (photo == null)
            {
                Debug.WriteLine("No Photo Attach");
                SignUp.Content = "\uE114";
                SignUp.FontFamily = new FontFamily("Segoe MDL2 Assets");
                SignUp.FontSize = 30;
                SignUp.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
            }
            else
            {
                CloudStorageAccount storageAccount = createStorageAccountFromConnectionString();
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("profileimagecontainer");
                await container.CreateIfNotExistsAsync();
                CloudBlockBlob blob = container.GetBlockBlobReference(photo.Name);
                await blob.UploadFromFileAsync(photo);
                AzureProfileImageUrl = blob.Uri.AbsoluteUri;
            }
        }
        private async void TurnOnCamera_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            userPhoto = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (userPhoto == null)
            {
                // User cancelled photo capture
                return;
            }
            else
            {
                var filestream = await userPhoto.OpenAsync(FileAccessMode.Read);
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.SetSource(filestream);
                ProfilePic.ProfilePicture = bitmapImage;
            }
        }
        private void LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            bool validation = TextBoxRegex.GetIsValid(tb);
            if (validation == false)
            {
                tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86)); 
                tb.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                SignUp.Content = "\uE814";
                SignUp.FontFamily = new FontFamily("Segoe MDL2 Assets");
                SignUp.FontSize = 30;
                SignUp.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
            }
            else { tb.Foreground = new SolidColorBrush(Colors.Green); tb.BorderBrush = new SolidColorBrush(Colors.Green); }
        }
        private void Password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Password.Password == "")
            {
                Password.Header = "Xin hãy cung cấp mật khẩu";
                Password.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                SignUp.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
            }
            else if (Password.Password.Length < 6)
            {
                Password.Header = "Mật khẩu phải có 6 ký tự trở lên";
                Password.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
            }
            else if (Password.Password.Any(char.IsUpper) == false)
            {
                Password.Header = "Mật khẩu phải có ít nhất 1 chữ HOA";
                Password.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
            }
            else if(Password.Password.Any(char.IsLower) == false) 
            {
                Password.Header = "Mật khẩu phải có ít nhất 1 chữ THƯỜNG";
                Password.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
            }
            else if (Password.Password.Any(char.IsNumber) == false)
            {
                Password.Header = "Mật khẩu phải có số";
                Password.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
            }
            else
            {
                Password.Header = "Mật Khẩu ✅";
                Password.BorderBrush = new SolidColorBrush(Colors.Green);
            }
        }
        private void ConfirmPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Password.Password.Length < 6 || ConfirmPassword.Password == "" || Password.Password.Any(char.IsUpper) == false || Password.Password.Any(char.IsNumber) == false)
            {
                ConfirmPassword.Header = "Có gì đó sai";
                ConfirmPassword.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                if (Password.Password != ConfirmPassword.Password)
                {
                    ConfirmPassword.Header = "Xác thực mật khẩu phải chính xác";
                    Password.Header = "Xác thực mật khẩu phải chính xác";
                    ConfirmPassword.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                    Password.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                    SignUp.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                }
            }
            else
            {
                ConfirmPassword.Header = "Xác Thực Mật Khẩu ✅";
                ConfirmPassword.BorderBrush = new SolidColorBrush(Colors.Green);
                Password.Header = "Mật Khẩu ✅";
                Password.BorderBrush = new SolidColorBrush(Colors.Green);
            }
        }
        private async void City_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObjectCity = (VietNamCity)City.SelectedItem;
            CityId = ObjectCity.cityID;
            await GetDistrictsAsync(CityId, 0);
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
            if (ObjectDistrict == null)
            {
                SchoolDistrict.SelectedItem = null;
            }
            else
            {
                DistrictId = ObjectDistrict.districtID;
                await GetSchoolsAsync(DistrictId);
            }
        }
        private async void SchoolCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObjectCity = (VietNamCity)SchoolCity.SelectedItem;
            CityId = ObjectCity.cityID;
            await GetDistrictsAsync(CityId, 1);
        }
        private void DateOfBirth_Closed(object sender, object e)
        {
            double CalculateAge;
            if (DateOfBirth.Date == null)
            {
                DateOfBirth.Date = null;
            }
            else
            {
                DateTimeOffset UserYear = (DateTimeOffset)DateOfBirth.Date;
                DateTimeOffset now = DateTimeOffset.Now;
                TimeSpan PredictAgeTimeSpan = now - UserYear;
                CalculateAge = Math.Floor((now - UserYear).TotalDays) / ((DateTime.IsLeapYear(year: now.Year) ? 366 : 365));
                PredictAge = (CalculateAge % 1) >= 0.951 ? Math.Round(CalculateAge) : Math.Floor(CalculateAge);
            }
        }
        private void ProfileImageLayout_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            PhotoAction.Opacity = 1;
        }
        private void ProfileImageLayout_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            PhotoAction.Opacity = 0;
        }
        private async void SignUp_Click(object sender, RoutedEventArgs e)
        {
            WaitingRegisterBar.Visibility = Visibility.Visible;
            if (CheckForm() == true)
            {
                WaitingRegisterBar.Visibility = Visibility.Visible;
                await saveUserPhoto(userPhoto);
                await RegisterAsync();
            }
            else
            {
                SignUpError content = new SignUpError(ValidForm);
                await content.ShowAsync();
                ValidForm.Clear();
            }
        }
        private bool CheckForm()
        {

            if (Address.Text == "")
            {
                Address.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                Address.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
            }
            else
            {
                Address.Foreground = new SolidColorBrush(Colors.Green);
                Address.BorderBrush = new SolidColorBrush(Colors.Green);
            }
            
            foreach(TextBox tb in UserRealName.Children.OfType<TextBox>())
            {
                bool ValidationTextBox = TextBoxRegex.GetIsValid(tb);
                if (ValidationTextBox == false)
                {
                    Validation valtb = new Validation();
                    valtb.TextBoxValid = TextBoxRegex.GetIsValid(tb);
                    valtb.TextBoxName = tb.Name;
                    tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                    tb.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                    SignUp.Content = "\uE814";
                    SignUp.FontFamily = new FontFamily("Segoe MDL2 Assets");
                    SignUp.FontSize = 30;
                    SignUp.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                    ValidForm.Add(valtb);
                }
                else { tb.Foreground = new SolidColorBrush(Colors.Green); tb.BorderBrush = new SolidColorBrush(Colors.Green); }
            }

            //TextBox Validation
            foreach (StackPanel container in RegisterForm.Children.OfType<StackPanel>())
            {
                foreach (TextBox tb in container.Children.OfType<TextBox>())
                {
                    if (tb != Address)
                    {
                        bool ValidationTextBox = TextBoxRegex.GetIsValid(tb);
                        if (ValidationTextBox == false)
                        {
                            Validation valtb = new Validation();
                            valtb.TextBoxValid = TextBoxRegex.GetIsValid(tb);
                            valtb.TextBoxName = tb.Name;
                            tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                            tb.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                            SignUp.Content = "\uE814";
                            SignUp.FontFamily = new FontFamily("Segoe MDL2 Assets");
                            SignUp.FontSize = 30;
                            SignUp.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                            ValidForm.Add(valtb);
                        }
                        else { tb.Foreground = new SolidColorBrush(Colors.Green); tb.BorderBrush = new SolidColorBrush(Colors.Green); }
                    }
                }
                foreach (ComboBox cb in container.Children.OfType<ComboBox>())
                {
                    if (cb != ExistingSchool)
                    {
                        Validation valcb = new Validation();
                        if (cb.SelectedItem == null)
                        {
                            valcb.TextBoxValid = false;
                            valcb.TextBoxName = cb.Name;
                            cb.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                            cb.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                            WaitingRegisterBar.Visibility = Visibility.Collapsed;
                            SignUp.Content = "\uE814";
                            SignUp.FontFamily = new FontFamily("Segoe MDL2 Assets");
                            SignUp.FontSize = 30;
                            SignUp.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                            ValidForm.Add(valcb);
                        }
                        else
                        {
                            cb.Foreground = new SolidColorBrush(Colors.Green);
                            cb.BorderBrush = new SolidColorBrush(Colors.Green);
                        }
                    }
                }
            }
            //Password Validation
            if (Password.Password == "")
            {
                Validation valpass = new Validation();
                Password.Header = "Xin hãy cung cấp mật khẩu";
                Password.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                WaitingRegisterBar.Visibility = Visibility.Collapsed;
                SignUp.Content = "\uE814";
                SignUp.FontFamily = new FontFamily("Segoe MDL2 Assets");
                SignUp.FontSize = 30;
                SignUp.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                valpass.TextBoxName = "Không có mật khẩu";
                valpass.TextBoxValid = false;
                ValidForm.Add(valpass);
            }
            else if (Password.Password.Length < 6)
            {
                Validation valpasslength = new Validation();
                Password.Header = "Mật khẩu phải có 6 ký tự trở lên";
                Password.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                WaitingRegisterBar.Visibility = Visibility.Collapsed; SignUp.Content = "\uE814";
                SignUp.FontFamily = new FontFamily("Segoe MDL2 Assets");
                SignUp.FontSize = 30;
                SignUp.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                valpasslength.TextBoxName = "Mật khẩu phải có 6 ký tự trở lên";
                valpasslength.TextBoxValid = false;
                ValidForm.Add(valpasslength);
            }
            else if (Password.Password.Any(char.IsUpper) == false)
            {
                Validation valpassUpperCase = new Validation();
                Password.Header = "Mật khẩu phải có ít nhất 1 chữ HOA";
                Password.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                WaitingRegisterBar.Visibility = Visibility.Collapsed; SignUp.Content = "\uE814";
                SignUp.FontFamily = new FontFamily("Segoe MDL2 Assets");
                SignUp.FontSize = 30;
                SignUp.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                valpassUpperCase.TextBoxName = "Mật khẩu phải có ít nhất 1 chữ HOA";
                valpassUpperCase.TextBoxValid = false;
                ValidForm.Add(valpassUpperCase);
            }
            else if (Password.Password != ConfirmPassword.Password)
            {
                Validation valpassConfirm = new Validation();
                ConfirmPassword.Header = "Xác thực mật khẩu phải chính xác";
                Password.Header = "Xác thực mật khẩu phải chính xác";
                ConfirmPassword.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                WaitingRegisterBar.Visibility = Visibility.Collapsed; SignUp.Content = "\uE814";
                SignUp.FontFamily = new FontFamily("Segoe MDL2 Assets");
                SignUp.FontSize = 30;
                SignUp.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                valpassConfirm.TextBoxName = "Xác thực mật khẩu phải chính xác";
                valpassConfirm.TextBoxValid = false;
                ValidForm.Add(valpassConfirm);
            }
            else
            {
                ConfirmPassword.Header = "Xác Thực Mật Khẩu ✅";
                ConfirmPassword.BorderBrush = new SolidColorBrush(Colors.Green);
                Password.Header = "Mật Khẩu ✅";
                Password.BorderBrush = new SolidColorBrush(Colors.Green);
            }

            //Birthday Validation
            if (DateOfBirth.Date == null)
            {
                //Check if the person select the box ?
                Validation valDate = new Validation();
                valDate.TextBoxName = "Không Có Ngày Sinh Nhật";
                valDate.TextBoxValid = false;
                DateOfBirth.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                WaitingRegisterBar.Visibility = Visibility.Collapsed;
                SignUp.Content = "\uE814";
                SignUp.FontFamily = new FontFamily("Segoe MDL2 Assets");
                SignUp.FontSize = 30;
                SignUp.Foreground = new SolidColorBrush(Color.FromArgb(255, 251, 44, 86));
                ValidForm.Add(valDate);
            }
            else
            {
                //Calculate the age base on their birthday
                DateOfBirth.BorderBrush = new SolidColorBrush(Colors.Green);
            }

            //Age Validation
            if (PredictAge < 10)
            {
                Validation valAge = new Validation();
                valAge.TextBoxName = "Bạn phải trên 10 tuổi mới được sử dụng hệ thống";
                valAge.TextBoxValid = false;
                ValidForm.Add(valAge);
            }
            WaitingRegisterBar.Visibility = Visibility.Collapsed;
            if (ValidForm.Count != 0)
            {
                foreach (Validation i in ValidForm)
                {
                    System.Diagnostics.Debug.WriteLine(i.TextBoxName);
                }
                return false;
            }
            return true;
        }
        private async Task RegisterAsync()
        {
            var birthday = DateOfBirth.Date;
            VietNamCity Uvnc = (VietNamCity)City.SelectedItem;
            VietNamDistrict Uvnd = (VietNamDistrict)District.SelectedItem;
            VietNamCity Svnc = (VietNamCity)SchoolCity.SelectedItem;
            VietNamDistrict Svnd = (VietNamDistrict)SchoolDistrict.SelectedItem;
            StudyGroup sg = (StudyGroup)StudyGroup.SelectedItem;
            StudyField sf = (StudyField)StudyField.SelectedItem;
            School sc = (School)ExistingSchool.SelectedItem;
            int GenderConvert = Gender.SelectedIndex;
            switch (Role.SelectedIndex)
            {
                case 0:
                    UserRole = "Admin";
                    break;
                case 1:
                    UserRole = "Student";
                    break;
                case 2:
                    UserRole = "Tutor";
                    break;
            }

            var values = new Dictionary<string, object>
            {
               {"UserName", UserName.Text},
               {"Email" , Email.Text},
               {"Pass" , Password.Password},
               {"FirstName", FirstName.Text },
               {"LastName", LastName.Text },
               {"Gender",  GenderConvert},
               {"Age", Convert.ToInt32(PredictAge)},
               {"DayOfBirth", birthday.ToString()},
               {"SchoolID", sc.schoolID},
               {"SchoolDistrict", Svnd.districtID},
               {"SchoolCity", Svnc.cityID},
               {"StudyGroup", sg.StudyGroupID},
               {"StudyField", sf.StudyFieldID},
               {"Address", Address.Text},
               {"District", Uvnd.districtID},
               {"City", Uvnc.cityID},
               {"PhoneNumber", Mobile.Text},
               {"ProfileImageUrl", AzureProfileImageUrl},
               {"Role", UserRole},
            };
            var content = new StringContent(JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json");
            try
            {
                var response = await httpClient.PostAsync(RegisterUrl, content);
                string Status = response.StatusCode.ToString();
                var responseString = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine(Status);
                if (Status == "OK")
                {
                    MainPivot.SelectedIndex = 1;
                    WaitingRegisterBar.Visibility = Visibility.Collapsed;
                    await contentSuccess.ShowAsync();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(responseString);
                    WaitingRegisterBar.ShowError = true;
                }
            }
            catch (Exception)
            {
                WaitingRegisterBar.ShowError = true;
                throw new Exception("Some thing wrong");
            }
        }
        private async Task LoginAsync(string email, string password)
        {
            var values = new Dictionary<string, string>
            {
               {"Email" , email},
               {"Pass" , password}
            };
            var content = new StringContent(JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(LoginUrl, content);
            string Status = response.StatusCode.ToString();
            var responseString = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(Status);
            if (Status == "OK")
            {
                App.Token = JsonConvert.DeserializeObject<Token>(responseString);
                await GetUserInfo(App.Token.token);
                System.Diagnostics.Debug.WriteLine(App.Token);
                //splash.localData.Values["UserToken"] = App.Token.token;
                WaitingLoginBar.Visibility = Visibility.Collapsed;
                Frame.Navigate(typeof(MainPage));
            }
            else
            {
                EmailLogin.BorderBrush = new SolidColorBrush(Colors.Orange);
                PasswordLogin.BorderBrush = new SolidColorBrush(Colors.Orange);
                ErrorMessage.Visibility = Visibility.Visible;
                SignIn.Foreground = new SolidColorBrush(Colors.Orange);
                WaitingLoginBar.ShowError = true;
            }
        }
        private async void LogIn_Click(object sender, RoutedEventArgs e)
        {
            WaitingLoginBar.Visibility = Visibility.Visible;
            string Email = EmailLogin.Text;
            string Pass = PasswordLogin.Password;
            await LoginAsync(Email, Pass);
        }
        private async Task GetUserInfo(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync(GetUserInfoUrl);
            var result = await response.Content.ReadAsStringAsync();
            App.User = JsonConvert.DeserializeObject<UserModel>(result);
        }
        //Trigger When Pivot Selected Change
        private void MainPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var beforeheader = sender as Pivot;
            var rightBeforeHeader = beforeheader.SelectedItem as PivotItem;

            var header = rightBeforeHeader.Header as StackPanel;

            var headerPosition = header.TransformToVisual(Form).TransformPoint(new Windows.Foundation.Point(0d, 0d));


            var initialBounds = new Windows.Foundation.Rect()
            {
                Width = header.RenderSize.Width,
                Height = header.RenderSize.Height,
                X = headerPosition.X,
                Y = headerPosition.Y
            };

            var finalBounds = Window.Current.Bounds;  // maps to the bounds of the current window

            //The code is super easy to understand if you set a break point here and 
            //check to see what happens step by step ;)
            transition.Start((Color)_colorsByPivotItem[header.Name],  // the color for the circlular bloom
                                 initialBounds,                                  // the initial size and position
                                 finalBounds);                             // the area to fill over the animation duration

            // Add item to queue of transitions

            var pivotItem = (PivotItem)MainPivot.Items.Single(i => ((StackPanel)((PivotItem)i).Header).Name.Equals(header.Name));

            pendingTransitions.Enqueue(pivotItem);

            //This code deals with a bug that occurs when you go navigate to a new page then come back to this one.
            if (carefulPlz == true)
            {
                var item = pendingTransitions.Dequeue();
                var headerFinish = item;
                Form.Background = new SolidColorBrush((Color)_colorsByPivotItem[headerFinish.Name]);
                carefulPlz = false;
            }
            // Make the content visible immediately, when first clicked. Subsequent clicks will be handled by Pivot Control
            var content = (FrameworkElement)pivotItem.Content;
            if (content.Visibility == Visibility.Collapsed)
            {
                content.Visibility = Visibility.Visible;
            }


            //var bounds = Window.Current.Bounds;
            //double width = bounds.Width;
            switch (MainPivot.SelectedIndex)
            {
                case 0:
                    //Debug.WriteLine(width);
                    //if (width < 700)
                    //{
                    //    Layout.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Star);
                    //}
                    //else Layout.ColumnDefinitions[0].Width = new GridLength(0.4, GridUnitType.Star);
                    break;
                case 1:
                    //Debug.WriteLine(width);
                    //if (width < 700)
                    //{
                    //    Layout.ColumnDefinitions[0].Width = new GridLength(1.4, GridUnitType.Star);
                    //}
                    //else Layout.ColumnDefinitions[0].Width = new GridLength(1.4, GridUnitType.Star);
                    break;
            }
        }
        private void ColorBloomTransition_Unloaded(object sender, RoutedEventArgs e)
        {
            if (stopDisposing == false)
            {
                transition.Dispose();
                surroundButtonTransition.Dispose();
                stopDisposing = true;
            }

        }
        private void ColorBloomTransitionCompleted(object sender, EventArgs e)
        {
            var item = pendingTransitions.Dequeue();
            var header = (StackPanel)item.Header;
            Form.Background = new SolidColorBrush((Color)_colorsByPivotItem[header.Name]);
        }
        private void InitializeTransitionHelper()
        {
            transition = new ColorBloomTransitionHelper(hostForVisual);
            surroundButtonTransition = new ColorBloomTransitionHelper(anotherHost);
            transition = new ColorBloomTransitionHelper(hostForVisual);
            surroundButtonTransition = new ColorBloomTransitionHelper(anotherHost);
            transition.ColorBloomTransitionCompleted += ColorBloomTransitionCompleted;
        }
        private void GuessLogin_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
        private void Form_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var uiCanvasLocation = Form.TransformToVisual(Form).TransformPoint(new Windows.Foundation.Point(0d, 0d));
            var clip = new RectangleGeometry()
            {
                Rect = new Windows.Foundation.Rect(uiCanvasLocation, e.NewSize)
            };
            Form.Clip = clip;
            InitializeTransitionHelper();
        }
        private void InitializeColors()
        {
            _colorsByPivotItem = new PropertySet();
            _colorsByPivotItem.Add("firstPivot", Color.FromArgb(200, 37, 170, 225));
            _colorsByPivotItem.Add("secondPivot", Color.FromArgb(200, 46, 49, 145));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!(e.Parameter == null) && e.Parameter.ToString() != "")
            {
                carefulPlz = true;
            }
        }
    }
}
