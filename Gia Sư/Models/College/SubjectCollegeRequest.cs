using Gia_Sư.Models.Person;
using Gia_Sư.Models.College;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Gia_Sư.Models.College
{
    public class SubjectCollegeRequest
    {
        public string name { get; set; }
        public string studyGroupName { get; set; }
        public string studyFieldName { get; set; }
        public string description { get; set; }
        public decimal Price { get; set; }
        public string teacher { get; set; }
        public string learningAddress { get; set; }
        public string districtName { get; set; }
        public string cityName { get; set; }
        public ICollection<UserModel> students { get; set; }
        public bool homework { get; set; }
        public bool presentation { get; set; }
        public bool laboratory { get; set; }
        public DateTime RequestDate { get; set; }
        public string schoolName { get; set; }
        public string schoolLogo { get; set; }
        public string schoolAddress { get; set; }
        public string schooldistrict { get; set; }
        public string schoolcity { get; set; }
        public int payMentTime { get; set; }
        public ICollection<RequestSchedules> requestSchedules { get; set; }
        public string VNDPrice { get => Price.ToString("####.000") + "VNĐ"; }
    }
    public class OverviewCollegeRequest
    {
        public int RequestID { get; set; }
        public decimal Price { get; set; }
        public string Sub { get; set; }
        public string SchoolName { get; set; }
        public string SchoolLogoUrl { get; set; }
        public DateTime Date { get; set; }
        public int payMentTimeType { get; set; }
        public string Day { get => Date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture); }
        public string VNDPrice { get => Price.ToString("####.000"); }
    }
}
