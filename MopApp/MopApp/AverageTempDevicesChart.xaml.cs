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
using System.Globalization;
using System.Web;

namespace MopApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AverageTempDevicesChart : ContentPage
	{
        private List<Entry> entries = new List<Entry> { };
        private const string Url = "http://ec2-18-184-187-189.eu-central-1.compute.amazonaws.com/api/temperature/plot/1/";
        public readonly HttpClient _client = new HttpClient();
        public ObservableCollection<Post> _posts;

        public AverageTempDevicesChart ()
		{
			InitializeComponent ();
           // createEntries();
            

            AverageTempDevices.Chart = new BarChart { Entries = entries };
        }
        protected override async void OnAppearing()
        {
            string content = await _client.GetStringAsync(Url);
            List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(content);
            _posts = new ObservableCollection<Post>(posts);
             maxTempLabel.Text = _posts[0].AvgResult;
             Random rnd = new Random();

            // for (int i = 0; i < _posts.Count; i++)
            //{
            float value = rnd.Next(-10, 40);
           // float value = float.Parse(_posts[0].Average, CultureInfo.InvariantCulture.NumberFormat);
                Entry tmp = new Entry(value)
                {
                    ValueLabel = value.ToString(),
                    Color = MopApp.ChartPage.getColorFromValue(value),
                };

               // tmp.Label = _posts[0].Name;

                entries.Add(tmp);
           // }


        }
        public async void createEntries(object sender, EventArgs e)
        {

            string content = await _client.GetStringAsync(MainCarouselPage.Url);
            


            List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(content);
            _posts = new ObservableCollection<Post>(posts);
            maxTempLabel.Text = _posts[0].Week;
           Random rnd = new Random();

            for (int i = 0; i < _posts.Count; i++)
            {
               float value = rnd.Next(-10, 40);
              //  float value = float.Parse(_posts[i].Average, CultureInfo.InvariantCulture.NumberFormat); 
                Entry tmp = new Entry(value)
                {
                    ValueLabel = value.ToString(),
                    Color = MopApp.ChartPage.getColorFromValue(value),
                    
                };
                
                    // tmp.Label = _posts[i].Name;
                
                entries.Add(tmp);
            }

        }
    }
}