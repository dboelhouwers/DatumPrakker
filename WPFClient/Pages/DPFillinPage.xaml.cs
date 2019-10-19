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
    /// Interaction logic for DPFillinPage.xaml
    /// </summary>
    public partial class DPFillinPage : Page
    {
        private DPOptionsPage dpOptionPage;
        private DatumPrakker datumPrakker;
        private List<DateTime> checkedDates;
        public DPFillinPage(DPOptionsPage dpOptionPage_)
        {
            InitializeComponent();
            dpOptionPage = dpOptionPage_;
            //addCheckbox("Dit is een datum");
        }

        public void setDatumPrakker(DatumPrakker datumPrakker_)
        {
            datumPrakker = datumPrakker_;

            checkedDates = new List<DateTime>();

            dpID.Content = datumPrakker.id;
            nameLabel.Content = datumPrakker.name;
            createdByLabel.Content = datumPrakker.createdByUsername;

            foreach (DateTime dt in datumPrakker.chooseableDates)
            {
                addDateToList(dt);
            }
        }

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("[DPFillinPage] Button Confirm Clicked");

            foreach (DateTime d in checkedDates)
            {
                Console.Out.WriteLine(d.Date);
            }

            MessageBox.Show($"DatumPrakker '{nameLabel.Content}' filled in.");

            MainWindow.addDPAnswer(datumPrakker.id, MainWindow.getUsername(), checkedDates);

            NavigationService.Navigate(dpOptionPage);

        }

        private void addCheckedDateToList(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            Console.WriteLine($"[DPFillinPage] Check Changed {cb.Content} : {cb.IsChecked}");

            String cTag = cb.Tag.ToString();
            foreach (DateTime d in datumPrakker.chooseableDates)
            {
                string s = $"{d.Month}/{d.Day}/{d.Year}";
                if (s.Equals(cTag))
                {
                    checkedDates.Add(d);
                    Console.Out.WriteLine($"{cTag} Added.");
                    break;
                }
            }
        }        

        private void deleteCheckedDateToList(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            Console.WriteLine($"[DPFillinPage] unCheck Changed {cb.Content} : {cb.IsChecked}");

            String cTag = cb.Tag.ToString();
            foreach (DateTime d in checkedDates)
            {
                string s = $"{d.Month}/{d.Day}/{d.Year}";
                if (s.Equals(cTag))
                {
                    checkedDates.Remove(d);
                    Console.Out.WriteLine($"{cTag} Deleted.");
                    break;
                }
            }
        }

        private void addDateToList(DateTime dt)
        {
            StackPanel sp = new StackPanel(); //sp = stackPanel 
            Label dl = new Label(); //dl = dateLabel 
            CheckBox cb = new CheckBox(); //cb = checkbox 

            //Label init 
            dl.Content = $"{dt.Month}/{dt.Day}/{dt.Year}";
            dl.HorizontalAlignment = HorizontalAlignment.Right;
            dl.Width = 75;

            //Checkbox 
            cb.Tag = $"{dt.Month}/{dt.Day}/{dt.Year}";
            cb.Checked += addCheckedDateToList;
            cb.Unchecked += deleteCheckedDateToList;
            cb.HorizontalAlignment = HorizontalAlignment.Center;
            cb.VerticalAlignment = VerticalAlignment.Center;
            cb.Padding = new Thickness(5, 2, 5, 2);

            //StackPanel init 
            sp.Orientation = Orientation.Horizontal;
            sp.HorizontalAlignment = HorizontalAlignment.Right;
            sp.Children.Add(dl);
            sp.Children.Add(cb);

            //Add stackPanel to view 
            datesPanel.Children.Add(sp);

        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Back");
            NavigationService.Navigate(dpOptionPage);
        }

    }
}
