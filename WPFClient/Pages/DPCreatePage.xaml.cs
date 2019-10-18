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
    /// Interaction logic for DPCreatePage.xaml
    /// </summary>
    public partial class DPCreatePage : Page
    {
        private DPOptionsPage dpOptionPage;
        private List<DateTime> dateTimesList;
        private String roomGUID;
        public DPCreatePage(DPOptionsPage dpOptionPage_)// ref/out 
        {
            InitializeComponent();
            dpOptionPage = dpOptionPage_;
            dateTimesList = new List<DateTime>();
            roomID.Content = roomGUID = Guid.NewGuid().ToString().ToUpper().Replace("-", "").Substring(0, 6);
            datePicker.BlackoutDates.Add(
                new CalendarDateRange(new DateTime(1500, 1, 1), DateTime.Today.AddDays(-1)));

        }

        private void addDateToList(DateTime dt)
        {
            //String guid = Guid.NewGuid().ToString();
            dateTimesList.Add(dt);

            StackPanel sp = new StackPanel(); //sp = stackPanel 
            Label dl = new Label(); //dl = dateLabel 
            Button ddb = new Button(); //ddb = dateDeleteButton 

            //Label init 
            dl.Content = $"{dt.Month}/{dt.Day}/{dt.Year}";
            dl.HorizontalAlignment = HorizontalAlignment.Right;
            dl.Width = 75;

            //Button delete init 
            ddb.Tag = $"{dt.Month}/{dt.Day}/{dt.Year}";
            ddb.Click += Button_Click_DeleteDate;
            ddb.Background = Brushes.Red;
            ddb.Width = ddb.Height = 25;
            ddb.Content = "X";

            //StackPanel init 
            sp.Orientation = Orientation.Horizontal;
            sp.HorizontalAlignment = HorizontalAlignment.Right;
            sp.Children.Add(dl);
            sp.Children.Add(ddb);

            //Add stackPanel to view 
            datesPanel.Children.Add(sp);

        }

        private void Button_Click_AddDP(object sender, RoutedEventArgs e)
        {
            if (datePicker.SelectedDate == null)
            {
                MessageBox.Show($"No date is selected.");
            }
            else
            {
                DateTime dt = datePicker.SelectedDate.Value;
                if (dateTimesList.Contains(dt))
                    MessageBox.Show($"Date {dt.Month}/{dt.Day}/{dt.Year} is already added.");
                else
                {
                    addDateToList(dt);
                    Console.Out.WriteLine($"Added: {dt.Date}");
                }
                //dateTimesList.ForEach(i => (i?.Equals(dt) ? null : i));
            }


        }

        private void Button_Click_DeleteDate(object sender, RoutedEventArgs e)
        {
            Button clicked = (Button) sender;

            Console.Out.WriteLine($"[DPCreatePage] clicked delete date with id : {clicked.Tag}");
            String cTag = clicked.Tag.ToString();
            foreach (DateTime d in dateTimesList)
            {
                string s = $"{d.Month}/{d.Day}/{d.Year}";
                if (s.Equals(cTag))
                {
                    dateTimesList.Remove(d);
                    Console.Out.WriteLine($"{cTag} Deleted.");
                    break;
                }
            }

            datesPanel.Children.Remove((StackPanel)clicked.Parent);

        }

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            //TestPage2 testPage2 = new TestPage2();
            //this.NavigationService.Navigate(testPage2);
            Console.WriteLine("[DPCreatePage] Button Create Clicked");

            if (nameTXTBox.Text.Equals(null) || nameTXTBox.Text.Equals("") || nameTXTBox.Text.Contains(" "))
            {
                MessageBox.Show("Please fill in a name without spaces.");
            }
            else
            {
                Console.Out.WriteLine(dateTimesList.Count);
                foreach (DateTime d in dateTimesList)
                {
                    Console.Out.WriteLine(d.Date);
                }

                MessageBox.Show($"DatumPrakker '{nameTXTBox.Text}' created.");

                //TODO 
                //String id, string name, List<DateTime> chooseableDates
                MainWindow.addDP(roomGUID, nameTXTBox.Text, dateTimesList);// add DatumPrakker 
                NavigationService.Navigate(dpOptionPage);
            }
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Back");
            NavigationService.Navigate(dpOptionPage);
        }

        //private class CustomDateTime
        //{
        //    private String guid { get; }
        //    private DateTime dateTime { get; }
        //    public CustomDateTime(string guid, DateTime dateTime)
        //    {
        //        this.guid = guid;
        //        this.dateTime = dateTime;
        //    }
        //}
    }
}
