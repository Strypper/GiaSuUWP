using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gia_Sư.Models
{
    public class School
    {
        public int schoolID { get; set; }
        public string schoolName { get; set; }
        public int city { get; set; }
        public int district { get; set; }
        public string schoolAddress { get; set; }
        public string schoolLogo { get; set; }
    }
}
