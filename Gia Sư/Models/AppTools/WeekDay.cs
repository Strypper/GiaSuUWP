using System;

namespace Gia_Sư.Models.AppTools
{
    public class WeekDay
    {
        public WeekDaysEnum weekDay { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }

        public WeekDay(WeekDaysEnum day, TimeSpan timeStart, TimeSpan timeEnd)
        {
            this.weekDay = day;
            this.TimeStart = timeStart;
            this.TimeEnd = timeEnd;
        }
    }
}
