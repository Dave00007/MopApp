using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MopApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class MainPage : MasterDetailPage
    {
        public List<MasterPageItem> menuList { get; set; }
        public MainPage()
        {
            InitializeComponent();
            menuList = new List<MasterPageItem>();
  
            menuList.Add(new MasterPageItem() { Title = "Home", Icon = "home.png", TargetType = typeof(AverageTempDevicesChart) });
            menuList.Add(new MasterPageItem() { Title = "Details of Devices", Icon = "setting.png", TargetType = typeof(ChartPage) });
            // menuList.Add(new MasterPageItem() { Title = "Help", Icon = "help.png", TargetType = typeof(HelpPage) });

            navigationDrawerList.ItemsSource = menuList;
    
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(StatisticsPage)));
        }
  
        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;
            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }
    }
}