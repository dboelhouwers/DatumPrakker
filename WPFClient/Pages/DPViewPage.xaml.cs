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
using DP;

namespace WPFClient.Pages
{
    /// <summary>
    /// Interaction logic for DPViewPage.xaml
    /// </summary>
    public partial class DPViewPage : Page
    {
        private DPOptionsPage dpOptionPage;
        private DatumPrakker datumPrakker;
        public DPViewPage(DPOptionsPage dpOptionPage_)
        {
            InitializeComponent();
            dpOptionPage = dpOptionPage_;
        }

        public void setDatumPrakker(DatumPrakker datumPrakker_)
        {
            datumPrakker = datumPrakker_;

            dpID.Content = datumPrakker.id;
            nameLabel.Content = datumPrakker.name;
            createdByLabel.Content = datumPrakker.createdByUsername;

            foreach (DateTime d in datumPrakker.chooseableDates)
            {
                dateComboBox.Items.Add(d.Date.ToShortDateString());
            }
        }

        private void CB_Date_Changed(object sender, RoutedEventArgs e)
        {
            if (AvailablePeopleStackPanel.Children.Count > 0)
            {
                AvailablePeopleStackPanel.Children.Clear();
            }

            DateTime d = datumPrakker.chooseableDates[dateComboBox.SelectedIndex];

            foreach (DatumPrakker.DPAnswer dpa in datumPrakker.answers)
            {
                foreach (DateTime d2 in dpa.choosenDates)
                {
                    if (d.Equals(d2))
                    {
                        addPersonToAvailablePeople(dpa.username);
                    }
                }
            }
        }

        private void addPersonToAvailablePeople(string name)
        {
            Label l = new Label();
            l.Height = 25;
            l.Width = 220;
            l.HorizontalContentAlignment = HorizontalAlignment.Center;
            l.VerticalContentAlignment = VerticalAlignment.Center;
            l.Content = name;

            AvailablePeopleStackPanel.Children.Add(l);
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Back");
            NavigationService.Navigate(dpOptionPage);
        }

    }
}
