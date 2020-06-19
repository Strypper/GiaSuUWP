using Gia_Sư.Models.Person;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Gia_Sư.Models.SubjectData
{
    public class SubjectRequest
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
    public class OverviewRequest 
    {
        public int RequestID { get; set; }
        public string ProfileUrlImage { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public decimal Price { get; set; }
        public string Sub { get; set; }
        public string SchoolName { get; set; }
        public DateTime Date { get; set; }
        public string Day { get => Date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture); }
    }
}
