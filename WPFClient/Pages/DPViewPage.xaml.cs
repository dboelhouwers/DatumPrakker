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
        public DPViewPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Show who can join");
        }
        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Back");
        }
    }
}
