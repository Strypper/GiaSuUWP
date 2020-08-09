using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gia_Sư.Models.Stuhub.Club
{
    public class ClubInfo
    {
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public string ClubLogoURL { get; set; }
        public string ClubCoverImageURL { get; set; }
        public string Color { get; set; }
        public DateTime DateStarted { get; set; }
        public string Location { get; set; }
        public string PhoneNumbers { get; set; }
        public string Email { get; set; }
        public string ClubInfoURL { get; set; }
        public string Intro { get; set; }
        public string Detail { get; set; }
        public int Rating { get; set; }
        public int Members { get; set; }
        public int Projects { get; set; }
        public string Requirement { get; set; }
        public int ClubPrice { get; set; }
    }
    public class TestData
    {
        public static List<ClubInfo> GetData()
        {
            List<ClubInfo> data = new List<ClubInfo>();
            data.Add(new ClubInfo
            {
                ClubId = 1,
                ClubName = "Vision",
                ClubLogoURL = "ms-appx:///Assets/TestPurpose/SchoolLogo/IU.png",
                ClubCoverImageURL = "ms-appx:///Assets/TestPurpose/Wallpapers/.NET.png",
                Color = "",
                DateStarted = new DateTime(2008, 5, 1, 8, 30, 52),
                Location = "Online",
                PhoneNumbers = "0348164682",
                Email = "FutureWingsStrypper@outlook.com",
                ClubInfoURL = "",
                Intro = "Câu lạc bộ này để tụ tập các bạn đam mê lập trình công nghệ .NET, chúng mình sẽ cùng nhau góp vào tạo lên những đồ chơi có giá trị trong cuộc sống với công nghệ của Microsoft ở cả 2 mảng phần cứng + phần mềm.",
                Detail = "",
                Rating = 5,
                Members = 20,
                Projects = 5,
                Requirement = "Courage, Will and Fearless",
                ClubPrice = 21
            });
            data.Add(new ClubInfo
            {
                ClubId = 2,
                ClubName = "Vision",
                ClubLogoURL = "ms-appx:///Assets/TestPurpose/SchoolLogo/IU.png",
                ClubCoverImageURL = "ms-appx:///Assets/TestPurpose/Wallpapers/.NET.png",
                Color = "",
                DateStarted = new DateTime(2008, 5, 1, 8, 30, 52),
                Location = "Online",
                PhoneNumbers = "0348164682",
                Email = "FutureWingsStrypper@outlook.com",
                ClubInfoURL = "",
                Intro = "You wanna change the world ? join us.",
                Detail = "",
                Rating = 5,
                Members = 20,
                Projects = 5,
                Requirement = "Courage, Will and Fearless",
                ClubPrice = 21
            });
            data.Add(new ClubInfo
            {
                ClubId = 3,
                ClubName = "Vision",
                ClubLogoURL = "ms-appx:///Assets/TestPurpose/SchoolLogo/IU.png",
                ClubCoverImageURL = "ms-appx:///Assets/TestPurpose/Wallpapers/.NET.png",
                Color = "",
                DateStarted = new DateTime(2008, 5, 1, 8, 30, 52),
                Location = "Online",
                PhoneNumbers = "0348164682",
                Email = "FutureWingsStrypper@outlook.com",
                ClubInfoURL = "",
                Intro = "You wanna change the world ? join us.",
                Detail = "",
                Rating = 5,
                Members = 20,
                Projects = 5,
                Requirement = "Courage, Will and Fearless",
                ClubPrice = 21
            });
            return data;
        }
    }
}
