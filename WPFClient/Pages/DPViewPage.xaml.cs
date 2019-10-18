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
    /// Interaction logic for DPViewPage.xaml
    /// </summary>
    public partial class DPViewPage : Page
    {
        private DPOptionsPage dpOptionPage;
        public DPViewPage(DPOptionsPage dpOptionPage_)
        {
            InitializeComponent();
            dpOptionPage = dpOptionPage_;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("[DPViewPage] Button Show who can join Clicked");
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Back");
            NavigationService.Navigate(dpOptionPage);
        }

    }
}
