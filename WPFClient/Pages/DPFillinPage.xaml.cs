using System;
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
    /// Interaction logic for DPFillinPage.xaml
    /// </summary>
    public partial class DPFillinPage : Page
    {
        private DPOptionsPage dpOptionPage;
        public DPFillinPage(DPOptionsPage dpOptionPage_)
        {
            InitializeComponent();
            dpOptionPage = dpOptionPage_;
        }

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("[DPFillinPage] Button Confirm Clicked");
        }

        private void Date_Check_Handlr(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("[DPFillinPage] Check Changed");
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Back");
            NavigationService.Navigate(dpOptionPage);
        }

    }
}
