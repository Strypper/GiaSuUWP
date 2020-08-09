using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gia_Sư.Models.College
{
    public class TutorCollegeRequest
    {
        public int tutorID { get; set; }
        public string tutorStudyGroup { get; set; }
        public string tutorStudyField { get; set; }
        public string profileUrlImage { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string schoolName { get; set; }
        public string schoolLogoUrl { get; set; }

        public string fullName { get => lastName + " " + firstName; }
    }
}
