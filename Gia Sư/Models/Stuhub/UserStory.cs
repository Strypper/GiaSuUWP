using System;
using System.Collections.ObjectModel;

namespace Gia_Sư.Models.Stuhub
{
    public class UserStory
    {
        public int UserStoryId { get; set; }
        public string PersonProfileUrl { get; set; }
        public string PersonName { get; set; }
        public DateTime UploadDate { get; set; }
        public string Story { get; set; }
        public string StoryImage { get; set; }
        public int Like { get; set; }
        //public ICollection<Comment> Comments { get; set; }

    }
    public class FakeUserStoryData
    {
        public static ObservableCollection<UserStory> GetData()
        {
            var data = new ObservableCollection<UserStory>();
            data.Add(new UserStory()
            {
                PersonProfileUrl = "ms-appx:///Assets/TestPurpose/ProfileImage/Me(ver 2019).jpg",
                PersonName = "Strypper Vandel Jason",
                Story = "Lên đại học thì học đại cho nó lẹ 👌",
                StoryImage = "ms-appx:///Assets/TestPurpose/Wallpapers/IUTest1.jpg"
            });
            data.Add(new UserStory()
            {
                PersonProfileUrl = "ms-appx:///Assets/TestPurpose/ProfileImage/Gia.jpg",
                PersonName = "Huỳnh Quốc Gia",
                Story = "Vướng chuyện tình trường, mai lên công an viết tường trình 🙂",
                StoryImage = "ms-appx:///Assets/TestPurpose/Wallpapers/IUTest2.jpg"
            });
            data.Add(new UserStory()
            {
                PersonProfileUrl = "ms-appx:///Assets/TestPurpose/ProfileImage/Hưng.jpg",
                PersonName = "Nguyễn Đỗ Hưng",
                Story = "Tình yêu không tương thích, Thì chỉ mang lại thương tích 💔",
                StoryImage = "ms-appx:///Assets/TestPurpose/Wallpapers/IUTest3.jpg"
            });
            data.Add(new UserStory()
            {
                PersonProfileUrl = "ms-appx:///Assets/TestPurpose/ProfileImage/Hoàng.jpg",
                PersonName = "Lê Minh Hoàng",
                Story = "Nhiiiiiiiiiiii",
                StoryImage = "ms-appx:///Assets/TestPurpose/LocationOverView/4.jpg"
            });
            data.Add(new UserStory()
            {
                PersonProfileUrl = "ms-appx:///Assets/TestPurpose/ProfileImage/Phát.jpg",
                PersonName = "Lê Lưu Phát",
                Story = "Design with care",
                StoryImage = "ms-appx:///Assets/TestPurpose/LocationOverView/5.jpg"
            });
            return data;
        }
    }
}
