﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFClient.Pages
{
    /// <summary>
    /// Interaction logic for DPOptionsPage.xaml
    /// </summary>
    public partial class DPOptionsPage : Page
    {
        public DPOptionsPage()
        {
            InitializeComponent();
        }

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            //TestPage2 testPage2 = new TestPage2();
            //this.NavigationService.Navigate(testPage2);
            Console.WriteLine("Button Create Clicked");

        }

        private void Button_Click_View(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Button View Clicked");

        }

        private void Button_Click_FillIn(object sender, RoutedEventArgs e)
        {

            Console.WriteLine("Button Fill in Clicked");

        }
    }
}
