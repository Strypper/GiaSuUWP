﻿using Gia_Sư.Models.Person;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Gia_Sư.Pages.UserInfoPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OverViewProfile : Page
    {
        private UserModel MyProfile = new UserModel();
        public OverViewProfile()
        {
            this.InitializeComponent();
            MyProfile = App.User;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadChartContents();
        }
        private void LoadChartContents()
        {
            Random rand = new Random();
            List<FinancialStuff> financialStuffList = new List<FinancialStuff>();
            financialStuffList.Add(new FinancialStuff() { Name = "TOÁN", Amount = rand.Next(0, 200) });
            financialStuffList.Add(new FinancialStuff() { Name = "LÝ", Amount = rand.Next(0, 200) });
            financialStuffList.Add(new FinancialStuff() { Name = "HÓA", Amount = rand.Next(0, 200) });
            financialStuffList.Add(new FinancialStuff() { Name = "ANH VĂN", Amount = rand.Next(0, 200) });
            (PieChart.Series[0] as PieSeries).ItemsSource = financialStuffList;
        }
    }
    public class FinancialStuff
    {
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}
