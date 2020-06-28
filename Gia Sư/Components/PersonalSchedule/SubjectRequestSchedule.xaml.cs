using Gia_Sư.Models.SubjectData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Gia_Sư.Components.PopUps
{
    public sealed partial class SubjectRequestSchedule : UserControl
    {
        public ICollection<RequestSchedules> Schedules { get; set; }
        public SubjectRequestSchedule()
        {
            this.InitializeComponent();
            Schedules = new List<RequestSchedules>();
        }
        public void AnalyzeData()
        {
           foreach (RequestSchedules rs in Schedules)
           {
                //Time Duration
                TimeSpan duration = rs.timeEnd - rs.timeStart;

                Rectangle rec = new Rectangle();
                rec.Margin = new Thickness(5,5,5,5);
                switch (Int32.Parse(rs.weekDay))
                {
                    case 0:
                        rec.SetValue(Grid.ColumnProperty, 1);
                        break;
                    case 1:
                        rec.SetValue(Grid.ColumnProperty, 2);
                        break;
                    case 2:
                        rec.SetValue(Grid.ColumnProperty, 3);
                        break;
                    case 3:
                        rec.SetValue(Grid.ColumnProperty, 4);
                        break;
                    case 4:
                        rec.SetValue(Grid.ColumnProperty, 5);
                        break;
                    case 5:
                        rec.SetValue(Grid.ColumnProperty, 6);
                        break;
                    case 6:
                        rec.SetValue(Grid.ColumnProperty, 7);
                        break;
                }
                if(rs.timeEnd <= new TimeSpan(12, 0, 0))
                {
                    rec.SetValue(Grid.RowProperty, 1);
                    rec.Fill = new SolidColorBrush(Colors.Green);
                    if (rs.timeStart < new TimeSpan(08, 0, 0))
                    {
                        rec.VerticalAlignment = VerticalAlignment.Top;
                    }
                    else if (rs.timeStart < new TimeSpan(10, 0, 0)) 
                    {
                        rec.VerticalAlignment = VerticalAlignment.Center;
                    }
                    else if (rs.timeStart < new TimeSpan(12, 0, 0))
                    {
                        rec.VerticalAlignment = VerticalAlignment.Bottom;
                    }
                }
                else if (rs.timeEnd <= new TimeSpan(17, 0, 0))
                {
                    rec.SetValue(Grid.RowProperty, 2);
                    rec.Fill = new SolidColorBrush(Colors.Yellow);
                    if (rs.timeStart < new TimeSpan(14, 0, 0))
                    {
                        rec.VerticalAlignment = VerticalAlignment.Top;
                    }
                    else if (rs.timeStart < new TimeSpan(16, 0, 0))
                    {
                        rec.VerticalAlignment = VerticalAlignment.Center;
                    }
                    else if (rs.timeStart < new TimeSpan(18, 0, 0))
                    {
                        rec.VerticalAlignment = VerticalAlignment.Bottom;
                    }
                }
                else if(rs.timeEnd <= new TimeSpan(23, 0, 0))
                {
                    rec.SetValue(Grid.RowProperty, 3);
                    rec.Fill = new SolidColorBrush(Colors.Blue);
                    if (rs.timeStart < new TimeSpan(20, 0, 0))
                    {
                        rec.VerticalAlignment = VerticalAlignment.Top;
                    }
                    else if (rs.timeStart < new TimeSpan(22, 0, 0))
                    {
                        rec.VerticalAlignment = VerticalAlignment.Center;
                    }
                    else if (rs.timeStart < new TimeSpan(24, 0, 0))
                    {
                        rec.VerticalAlignment = VerticalAlignment.Bottom;
                    }
                }
                //Set the Rectangle Height
                if(duration <= new TimeSpan(02, 0, 0))
                {
                    rec.Height = 30;
                }
                else if(duration <= new TimeSpan(04, 0, 0))
                {
                    rec.Height = 60;
                }
                else if(duration <= new TimeSpan(06, 0, 0))
                {
                    rec.VerticalAlignment = VerticalAlignment.Stretch;
                }
                
                ToolTip toolTip = new ToolTip();
                toolTip.Content = rs.timeStart + " - " + rs.timeEnd;
                ToolTipService.SetToolTip(rec, toolTip);
                BoardUI.Children.Add(rec);
            }
        }
    }
}
