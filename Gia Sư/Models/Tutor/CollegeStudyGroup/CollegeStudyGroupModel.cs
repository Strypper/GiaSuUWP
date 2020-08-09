using System.Collections.Generic;

namespace Gia_Sư.Models.Tutor.CollegeStudyGroup
{
    public class CollegeStudyGroupModel
    {
        public int GroupID { get; set; }
        public string StudyGroupName { get; set; }
        public string StudyGroupImageUrl { get; set; }
    }
    public class CollegeStudyGroupData
    {
        public static List<CollegeStudyGroupModel> GetData()
        {
            var data = new List<CollegeStudyGroupModel>();
            data.Add(new CollegeStudyGroupModel { GroupID = 4, StudyGroupName = "Khoa học Máy tính và Công nghệ thông tin", StudyGroupImageUrl = "ms-appx:///Assets/AppImage/StudyGroup/CNTT.png" });
            data.Add(new CollegeStudyGroupModel { GroupID = 6, StudyGroupName = "Khoa học Ứng dụng và Khoa học Cơ bản", StudyGroupImageUrl = "ms-appx:///Assets/AppImage/StudyGroup/Science.jpg" });
            data.Add(new CollegeStudyGroupModel { GroupID = 7, StudyGroupName = "Kinh doanh & Quản lý", StudyGroupImageUrl = "ms-appx:///Assets/AppImage/StudyGroup/Business.jpg" });
            data.Add(new CollegeStudyGroupModel { GroupID = 9, StudyGroupName = "Kỹ thuật", StudyGroupImageUrl = "ms-appx:///Assets/AppImage/StudyGroup/KyThuat.png" });
            data.Add(new CollegeStudyGroupModel { GroupID = 10, StudyGroupName = "Luật", StudyGroupImageUrl = "ms-appx:///Assets/AppImage/StudyGroup/Law.png" });
            data.Add(new CollegeStudyGroupModel { GroupID = 1, StudyGroupName = "Thể Chất", StudyGroupImageUrl = "ms-appx:///Assets/AppImage/StudyGroup/Sport.png" });
            data.Add(new CollegeStudyGroupModel { GroupID = 2, StudyGroupName = "Du Lịch và Dịch vụ Nhà Hàng, Khách Sạn", StudyGroupImageUrl = "ms-appx:///Assets/AppImage/StudyGroup/HotelService.png" });
            data.Add(new CollegeStudyGroupModel { GroupID = 3, StudyGroupName = "Giáo dục và Đào Tạo", StudyGroupImageUrl = "ms-appx:///Assets/AppImage/StudyGroup/Teacher.png" });
            data.Add(new CollegeStudyGroupModel { GroupID = 5, StudyGroupName = "Khoa học Xã Hội và Truyền Thông", StudyGroupImageUrl = "ms-appx:///Assets/AppImage/StudyGroup/MediaEducation.png" });
            data.Add(new CollegeStudyGroupModel { GroupID = 8, StudyGroupName = "Kiến trúc và Xây dựng", StudyGroupImageUrl = "ms-appx:///Assets/AppImage/StudyGroup/KienTrucXayDung.jpg" });
            data.Add(new CollegeStudyGroupModel { GroupID = 11, StudyGroupName = "Nghê thuật Sáng tạo và Thiết kế", StudyGroupImageUrl = "ms-appx:///Assets/AppImage/StudyGroup/Design.jpg" });
            data.Add(new CollegeStudyGroupModel { GroupID = 12, StudyGroupName = "Nhân văn", StudyGroupImageUrl = "ms-appx:///Assets/AppImage/StudyGroup/Farm.png" });
            data.Add(new CollegeStudyGroupModel { GroupID = 13, StudyGroupName = "Nông nghiệp và Thú y", StudyGroupImageUrl = "ms-appx:///Assets/AppImage/StudyGroup/CNTT.png" });
            data.Add(new CollegeStudyGroupModel { GroupID = 14, StudyGroupName = "Y tế và Sức khỏe", StudyGroupImageUrl = "ms-appx:///Assets/AppImage/StudyGroup/Medic.png" });
            return data;
        }
    }
}
