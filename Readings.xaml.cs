using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using JBM_OGIHARA;
using System.ComponentModel;

namespace JBM_OGIHARA
{
    /// <summary>
    /// Interaction logic for Readings.xaml
    /// </summary>
    public partial class Readings : Window
    {
        public Readings()
        {
            InitializeComponent();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Save report
            int success = Validate(Date0.ToString(), Date2.ToString());

            if (success >= 0)
            {

                Report report = new Report()
                {
                    MeterName = Machinebox.Text,
                    DateTimeNow = Date0.ToString(),
                    VAReadings = Date2.ToString(),


                };
                
                using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
                {
                   connection.Insert(report);
                }
            }


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
