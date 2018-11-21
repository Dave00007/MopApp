using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using Entry = Microcharts.Entry;
using Microcharts;
using SkiaSharp;
using System.Globalization;

namespace MopApp
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChartPage : ContentPage
    {
        public readonly HttpClient _client = new HttpClient();
        private ObservableCollection<TempOnDeviceWeekly> tempOnDeviceWeekly;
        private List<Entry> entries = new List<Entry> {};
        private string url = "http://ec2-18-184-187-189.eu-central-1.compute.amazonaws.com/api/temperature/plot/";

        internal ObservableCollection<TempOnDeviceWeekly> TempOnDeviceWeekly { get => tempOnDeviceWeekly; set => tempOnDeviceWeekly = value; }

        public ChartPage ()
		{
			InitializeComponent ();
            loadPickerData();

        }

        public void loadPickerData()
        { 
            var listData = new List<string>();
            listData.Add("Xiaomi");
            listData.Add("Samsung");
            listData.Add("Apple");
            devicesPicker.ItemsSource = listData;
        }

         private async void receiveSecondRecord(object sender, EventArgs e)
          {
            var selectedDeviceIndex = devicesPicker.SelectedIndex;
            var selectedDeviceName = devicesPicker.SelectedItem;

            chartTitle.Text = "Average Temperature(weekly) on " + selectedDeviceName + " device";
            entries.Clear();

            string content = await _client.GetStringAsync(createUrl(selectedDeviceIndex + 1));
            List<TempOnDeviceWeekly> averageTempOnDeviceWeekly = JsonConvert.DeserializeObject<List<TempOnDeviceWeekly>>(content);
            TempOnDeviceWeekly = new ObservableCollection<TempOnDeviceWeekly>(averageTempOnDeviceWeekly);

            for (int i = 0; i <= 51; i++)
             {
                float value = float.Parse(TempOnDeviceWeekly[i].AvgResult, CultureInfo.InvariantCulture.NumberFormat);
                Entry tmp = new Entry(value)
                {
                    ValueLabel = TempOnDeviceWeekly[i].AvgResult.Substring(0,5),
                    Color = getColorFromValue(value),
                };

                entries.Add(tmp);
             } 
            
            Average.Chart = new LineChart { Entries = entries };

          }

        public string createUrl(int deviceIndex)
        {
            return url + deviceIndex.ToString() + "/";
        }

        public static SKColor getColorFromValue(float value)
        {
            if (value > 35) return SKColor.Parse("ff5533");
            if (value > 30) return SKColor.Parse("ff8a33");
            if (value > 25) return SKColor.Parse("ffb833");
            if (value > 20) return SKColor.Parse("ffd133");
            if (value > 15) return SKColor.Parse("ffe933");
            if (value > 10) return SKColor.Parse("4df7d0");
            if (value > 5) return SKColor.Parse("4de0f7");
            if (value >= 0) return SKColor.Parse("4deaf7");
            if (value < -10) return SKColor.Parse("4db9f7");
            if (value < 0) return SKColor.Parse("4d7ef7");
            return SKColor.Parse("1e2d2e");
        }
    
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

    }
}