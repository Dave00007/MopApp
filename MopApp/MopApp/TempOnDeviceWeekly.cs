using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;

namespace MopApp
{
    class TempOnDeviceWeekly : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string _week;
        private string _avgResult;

        [JsonProperty("week")] 
        public string Week
        {
            get => _week;
            set
            {
                _week = value;
                OnPropertyChanged();
            }
        }

        [JsonProperty("avgResult")]
        public string AvgResult
        {
            get => _avgResult;
            set
            {
                _avgResult = value;
                OnPropertyChanged(); 
            }
        }

        //This is how you create your OnPropertyChanged() method
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
   
}
