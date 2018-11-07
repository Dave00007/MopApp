using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
namespace MopApp
{
    public partial class MainPage : ContentPage
    {
        //private const string Url = "http://jsonplaceholder.typicode.com/posts";
       private const string Url = "http://ec2-18-184-187-189.eu-central-1.compute.amazonaws.com/api/routes/";
        private readonly HttpClient _client = new HttpClient();
        private ObservableCollection<Post> _posts;

        public MainPage()
        {
            InitializeComponent();
            idButton.Clicked += receiveFirstRecord;
            titleButton.Clicked += receiveSecondRecord;
            postButton.Clicked += postSomeValue;
        }

        private async void receiveFirstRecord(object sender, EventArgs e)
        {
            string content = await _client.GetStringAsync(Url);
            List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(content);
            _posts = new ObservableCollection<Post>(posts);
            firstLabel.Text = _posts[0].Name;
        }

        private async void receiveSecondRecord(object sender, EventArgs e)
        {
            string content = await _client.GetStringAsync(Url);
            List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(content);
            _posts = new ObservableCollection<Post>(posts);
            secondLabel.Text = _posts[1].Name;
        }

        private async void postSomeValue(object sender, EventArgs e)
        {
            Post post = new Post { Name = $"Name: Dave" }; //Creating a new instane of Post with a Title Property and its value in a Timestamp format
            string content = JsonConvert.SerializeObject(post); //Serializes or convert the created Post into a JSON String
            await _client.PostAsync(Url, new StringContent(content, Encoding.UTF8, "application/json")); //Send a POST request to the specified Uri as an asynchronous operation and with correct character encoding (utf9) and contenct type (application/json).
           // _posts.Insert(0, post); //Updating the UI by inserting an element into the first index of the collection 
        }
    }
}