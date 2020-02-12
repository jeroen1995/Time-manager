using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Configuration;
using CsvHelper.Configuration;

namespace Time_manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
        }
        public class Record
        {
            //public int ActiviteitID { get; set; }
            public String RecordStartDate { get; set; }
            public String RecordStartTime { get; set; }
            public String RecordStopTime { get; set; }
            public String RecordActiviteit { get; set; }
            public String RecordCategorie { get; set; }
        }
        bool StartStopType = true;
        String StartDate = "";
        String StartTime = "";
        String StopTime = "";
        String Activity = "";
        String Category = "";
        
        private void StartStop_Click(object sender, RoutedEventArgs e)
        {
            //var OutputList = new List<Record>();
            
            if (StartStopType)
            {
                StartDate = DateTime.Now.ToString("dd/MM/yyyy");
                StartTime = DateTime.Now.ToString("HH:mm:ss");
                Activity = Activiteit.Text;
                Category = Categorie.Text;
                StartStop.Content = "Stop";
                StartStop.Background = Brushes.Red;
                
            } 
            else
            {
                StopTime = DateTime.Now.ToString("HH:mm:ss");
                var OutputRecord = new List<Record> { 
                    new Record {RecordStartDate = StartDate, RecordStartTime = StartTime, RecordStopTime = StopTime, RecordActiviteit = Activity, RecordCategorie = Category }
                };
                OutputBox.Text += $"{StartDate} | {StartTime} | {StopTime} | {Activity} | {Category} \n";
                String Path = $"C:\\Users\\{Environment.UserName}\\documents\\TimeManagement.csv";
                CsvConfiguration csvconfig = new CsvConfiguration(CultureInfo.CurrentCulture)
                {
                    HasHeaderRecord = !File.Exists(Path)
                };
                if (!(File.Exists(Path)))
                {
                    using (File.Create(Path)) { }
                }
                
                using (var writer = new StreamWriter(Path, append: true))
                using (var csv = new CsvWriter(writer, csvconfig))
                {
                    csv.WriteRecords(OutputRecord);
                    writer.Flush();
                }
                StartStop.Content = "Start";
                StartStop.Background = Brushes.LawnGreen;

            }
            
            
            StartStopType = !StartStopType;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OutputBox.Text = "";
        }
    }
}
