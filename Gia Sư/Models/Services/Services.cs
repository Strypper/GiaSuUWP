using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gia_Sư.Models.Services
{
    public class Services
    {
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortCutIcon1 { get; set; }
        public string ShortCutIcon2 { get; set; }
        public string ShortCut { get; set; }
        public string ShortCut2 { get; set; }
    }
    public class ServiceList
    {
        public static List<Services> GetServices()
        {
            var data = new List<Services>();
            data.Add(new Services { Title = "GIA SƯ", Icon = "\uE77B", ShortCutIcon1 = "\uED54", ShortCut = "LỢI ÍCH", ShortCutIcon2 = "\uE713", ShortCut2 = "VỀ HỆ THỐNG" });
            data.Add(new Services { Title = "VIỆC LÀM", Icon = "\uE821", ShortCutIcon1 = "\uE8F3", ShortCut = "TIN MỚI", ShortCutIcon2 = "\uEA80", ShortCut2 = "CHỌN VIỆC LÀM HIỆU QUẢ" });
            data.Add(new Services { Title = "TỤ TẬP", Icon = "\uEC32", ShortCutIcon1 = "\uE71E", ShortCut = "TÌM KIẾM QUÁN ĂN", ShortCutIcon2 = "\uF164", ShortCut2 = "ĐĂNG KÝ DỊCH VỤ" });
            data.Add(new Services { Title = "MUA ĐỒ", Icon = "\uE719", ShortCutIcon1 = "\uE8EC", ShortCut = "DEAL TỐT", ShortCutIcon2 = "\uE781", ShortCut2 = "LƯU Ý KHI BUÔN BÁN" });
            return data;
        }
    }
}
