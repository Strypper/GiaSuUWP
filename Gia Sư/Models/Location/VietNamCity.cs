using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gia_Sư.Models.Location
{
    public class VietNamCity 
    {
        public int cityID { get; set; }
        public string cityName { get; set; }
    }
    public class VietNamDistrict 
    {
        public int districtID { get; set; }
        public int cityID { get; set; }
        public string districtName { get; set; }
    }
}
