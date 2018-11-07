using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using Entry = Microcharts.Entry;
using Microcharts;
using SkiaSharp;

namespace MopApp
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChartPage : ContentPage
    {
        public readonly HttpClient _client = new HttpClient();
        public ObservableCollection<Post> _posts;
        private List<Entry> entries = new List<Entry> {};
     
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        
        public ChartPage ()
		{
			InitializeComponent ();
            createEntries();
            Average.Chart = new LineChart {Entries = entries}; 
            getChartButton.Clicked += receiveSecondRecord;
        }

        private async void receiveSecondRecord(object sender, EventArgs e)
        {
            string content = await _client.GetStringAsync(MainCarouselPage.Url);
            List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(content);
            _posts = new ObservableCollection<Post>(posts);
        }


        public void createEntries()
        {
            Random rnd = new Random();

            for (int i = 0; i <= 52; i++)
            {
                float value = rnd.Next(-10, 40);
                Entry tmp = new Entry(value)
                {
                    ValueLabel = value.ToString(),
                    Color = getColorFromValue(value),
                };
                if (i % 4 == 0)
                {
                   // tmp.Label = i.ToString();
                }
                entries.Add(tmp);
            }

        }

        public SKColor getColorFromValue(float value)
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
    }
}