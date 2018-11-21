using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MopApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticsPage : ContentPage
	{
        public readonly HttpClient _client = new HttpClient();
        public ObservableCollection<Post> _posts;
        public StatisticsPage ()
		{
			InitializeComponent ();
            getStatButton.Clicked += receiveFirstRecord;
            minTempValueLabel.Text = "26.12.2017: -18";
            maxTempValueLabel.Text = "15.08.2017: 37";
            avgTempValueLabel.Text = "21";
        }

        private async void receiveFirstRecord(object sender, EventArgs e)
        {
            string content = await _client.GetStringAsync(MainCarouselPage.Url);
            List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(content);
            _posts = new ObservableCollection<Post>(posts);
            minTempValueLabel.Text = _posts[0].Week;
        }

    }
}