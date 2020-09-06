using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gia_Sư.Models.SmartZone
{
    public class SmartZone
    {
        public string LocationName { get; set; }
        public string Logo { get; set; }
        public string BackgroundImage { get; set; }
    }

    public class FakeLocationData
    {
        public static List<SmartZone> GetData() 
        {
            var data = new List<SmartZone>();
            data.Add(new SmartZone()
            {
                LocationName = "THE COFFEE HOUSE",
                Logo = "ms-appx:///Assets/TestPurpose/LocationLogo/TheCoffeeHouse.png",
                BackgroundImage = "ms-appx:///Assets/TestPurpose/LocationOverView/TheCoffeeHouse.jpg",
            });
            data.Add(new SmartZone()
            {
                LocationName = "STARBUCKS",
                Logo = "ms-appx:///Assets/TestPurpose/LocationLogo/StarBucksLogo.png",
                BackgroundImage = "ms-appx:///Assets/TestPurpose/LocationOverView/StartBuckPlace.jpg",
            });
            data.Add(new SmartZone()
            {
                LocationName = "PASSIO",
                Logo = "ms-appx:///Assets/TestPurpose/LocationLogo/Passio.jpg",
                BackgroundImage = "ms-appx:///Assets/TestPurpose/LocationOverView/PassioPlace.jpg",
            });
            data.Add(new SmartZone()
            {
                LocationName = "KRISPYKREME",
                Logo = "ms-appx:///Assets/TestPurpose/LocationLogo/KrispyKreme.png",
                BackgroundImage = "ms-appx:///Assets/TestPurpose/LocationOverView/KKPlace.jpg",
            });
            data.Add(new SmartZone()
            {
                LocationName = "DUNKIN DONUTS",
                Logo = "ms-appx:///Assets/TestPurpose/LocationLogo/DD.png",
                BackgroundImage = "ms-appx:///Assets/TestPurpose/LocationOverView/DDPlace.png",
            });
            return data;
        }
    }
}
