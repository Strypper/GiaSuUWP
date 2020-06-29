using Gia_Sư.Models.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gia_Sư.Models.Person
{
    public class UserModel
    {
        public string Role { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImageUrl { get; set; }
        public string CoverImageUrl { get; set; }
        public DateTime DayOfBirth { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string SchoolName { get; set; }
        public string SchoolAddress { get; set; }
        public string SchoolLogo { get; set; }
        public string UserAddress { get; set; }
    }
    public class Token 
    {
        public string token { get; set; }
    }
}
