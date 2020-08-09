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
