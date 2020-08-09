using System;
namespace Gia_Sư.Models.College
{
    public class RequestSchedules
    {
        public int scheduleID { get; set; }
        public string weekDay { get; set; }
        public TimeSpan timeStart { get; set; }
        public TimeSpan timeEnd { get; set; }
    }
}
