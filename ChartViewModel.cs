using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace JBM_OGIHARA
{
    public class ChartViewModel
    {
        private SeriesCollection _seriesCollection;

    public SeriesCollection SeriesCollection
    {
        get { return _seriesCollection; }
        set
        {
            _seriesCollection = value;
            OnPropertyChanged(nameof(SeriesCollection));
        }
    }

    public ChartViewModel()
    {
        // Initialize your series collection here, e.g., add initial series.
        SeriesCollection = new SeriesCollection
        {
            new LineSeries
            {
                Title = "VA",
                Values = new ChartValues<ObservablePoint>
                {
                    new ObservablePoint(0, 4),
                    new ObservablePoint(1, 6),
                    new ObservablePoint(2, 5),
                    new ObservablePoint(3, 2),
                }
            },
            new LineSeries
            {
                    Title = "Power Factor",
                    Values = new ChartValues<ObservablePoint> 
                    {
                        new ObservablePoint(0, 8),
                        new ObservablePoint(2, 10),
                        new ObservablePoint(6, 9),
                        new ObservablePoint(8, 7),
                    }
            },
                new LineSeries
                {
                    Title = "Current Avg",
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(0, 8),
                        new ObservablePoint(2, 6),
                        new ObservablePoint(8, 4),
                        new ObservablePoint(8, 20),
                    }
                }

        };
    }

    // INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
        public void UpdateChartData()
        {
            // Simulate updating data (replace with your logic to update the SeriesCollection)
            Random random = new Random();
            foreach (var series in SeriesCollection)
            {
                var lineSeries = (LineSeries)series;
                foreach (var point in lineSeries.Values.Cast<ObservablePoint>())
                {
                    point.Y = random.Next(0, 10);  // Example: Update Y-values with random data
                }
            }

            // Notify the UI that the data has changed
            OnPropertyChanged(nameof(SeriesCollection));
        }

    }
}
