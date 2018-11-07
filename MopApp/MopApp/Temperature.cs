using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;

namespace MopApp
{
    class Temperature : INotifyPropertyChanged
    {
        private int Id { get; set; }
        private int number;
        private int timestamp;

        [JsonProperty("number")]
        
        public int Number
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged(); //This notifies the View or ViewModel that the value that a property in the Model has changed and the View needs to be updated.
            }
        }

        [JsonProperty("timestamp")]
        public int TimeStamp
        {
            get => timestamp;
            set
            {
                timestamp = value;
                OnPropertyChanged(); //This notifies the View or ViewModel that the value that a property in the Model has changed and the View needs to be updated.
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
