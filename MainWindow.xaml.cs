using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Wpf;
using System.ComponentModel;
using LiveCharts.Defaults;
using System.Xml.Linq;
using SQLite;
using System.Diagnostics;
using System.Net.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using JBM_OGIHARA;



namespace JBM_OGIHARA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ChartViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new ChartViewModel();
            DataContext = _viewModel;

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Report>();
            }

            StartTimer();

            //ReadDatabase();


        }

        private void StartTimer()
        {
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(10)  // Adjust the interval as needed (e.g., 10 seconds)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        // Event handler for timer tick (refresh data)
        private void Timer_Tick(object sender, EventArgs e)
        {
            _viewModel.UpdateChartData();

        }




        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public SeriesCollection SeriesCollection { get; set; }
        public Func<double, string> XFormatter { get; set; }
        private Func<double, string> _yFormatter;
        public Func<double, string> YFormatter
        {
            get { return _yFormatter; }
            set
            {
                _yFormatter = value;
                //OnPropertyChanged();
            }
        }

        private void Gauge_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //QueryValuations();

        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string url = "http://192.168.0.192";


            using (HttpClient client = new HttpClient())
            {

                try
                {

                    HttpResponseMessage response = await client.GetAsync(url);
                    //response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Trace.WriteLine(responseBody);
                }
                catch (HttpRequestException ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }

        }

        private void REPORTS_Click(object sender, RoutedEventArgs e)
        {
            Readings readings = new Readings();
            readings.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)

        {
            int success = Validate(Date1.ToString(), Date2.ToString());

            if (success >= 0)
            {
                    
                    DateTime[] queryDates = GetDatesBetween(DateTime.Parse(Date1.ToString()), DateTime.Parse(Date2.ToString())).ToArray();
                    List<SQLite.TableQuery<JBM_OGIHARA.Report>> values = new List<SQLite.TableQuery<JBM_OGIHARA.Report>>();

                Trace.WriteLine(queryDates[0]);

                    foreach (DateTime date in queryDates)
                    {
                    using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
                    {
                      values.Add(connection.Table<Report>().Where(v => v.DateTimeNow.StartsWith(date.ToString())));
                    }

                    }
                    Trace.WriteLine(values.Count);
            }

            
            
        }

        private static List<DateTime> GetDatesBetween(DateTime date1, DateTime date2)
        {
            List<DateTime> allDates = new List<DateTime>();

            for (DateTime date = date1; date <= date2; date = date.AddDays(1))
            {
                Trace.WriteLine(date);
                allDates.Add(date.Date);
            }
            Trace.WriteLine("list");
            Trace.WriteLine(allDates);
            return allDates;
        }

        private static int Validate(string date1, string date2)

        {
            if (!string.IsNullOrEmpty(date1) && !string.IsNullOrEmpty(date2))
            {
                DateTime Date1 = DateTime.Parse(date1);
                DateTime Date2 = DateTime.Parse(date2);
                int value = Date2.CompareTo(Date1);


                string V;
                // checking 
                if (value > 0)
                {
                    V = "is later than";

                }

                else if (value < 0)
                {
                    V = "is earlier than";
                    MessageBox.Show("From Date should be earlier than To Date. Please select again", "Invalid Date Range", MessageBoxButton.OK, MessageBoxImage.Error);
                }


                else
                {
                    V = "is the same as";

                }

                Trace.WriteLine(V);


                return value;
            }
            else
            {
                MessageBox.Show("Date should not be empty. Please select again", "Empty Date", MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }

        }

    }
}