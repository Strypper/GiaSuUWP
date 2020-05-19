using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Gia_Sư.Models.AppTools
{
    public class PaginationRouteEvent : RoutedEventArgs
    {
        public int PageNumber { get; set; }
    }
}
