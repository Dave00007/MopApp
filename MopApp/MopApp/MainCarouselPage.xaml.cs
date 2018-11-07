using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MopApp
{
    public partial class MainCarouselPage : CarouselPage
    {
        //private const string Url = "http://jsonplaceholder.typicode.com/posts";
        public const string Url = "http://ec2-18-184-187-189.eu-central-1.compute.amazonaws.com/api/routes/";
        public readonly HttpClient _client = new HttpClient();
        public ObservableCollection<Post> _posts;

        public MainCarouselPage()
        {
            InitializeComponent();
            postButton.Clicked += postSomeValue;
        }

        private async void postSomeValue(object sender, EventArgs e)
        {
            Post post = new Post { Name = $"Name: Dave" }; //Creating a new instane of Post with a Title Property and its value in a Timestamp format
            string content = JsonConvert.SerializeObject(post); //Serializes or convert the created Post into a JSON String
            await _client.PostAsync(Url, new StringContent(content, Encoding.UTF8, "application/json")); //Send a POST request to the specified Uri as an asynchronous operation and with correct character encoding (utf9) and contenct type (application/json).
        }
    }
}