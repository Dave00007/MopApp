﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;

namespace MopApp
{
    public class Post : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string _week;
        private string _avgResult;

        [JsonProperty("week")] //This maps the element title of your web service to your model
        public string Week
        {
            get => _week;
            set
            {
                _week = value;
                OnPropertyChanged(); //This notifies the View or ViewModel that the value that a property in the Model has changed and the View needs to be updated.
            }
        }

        [JsonProperty("avgResult")] //This maps the element title of your web service to your model
        public string AvgResult
        {
            get => _avgResult;
            set
            {
                _avgResult = value;
                OnPropertyChanged(); //This notifies the View or ViewModel that the value that a property in the Model has changed and the View needs to be updated.
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