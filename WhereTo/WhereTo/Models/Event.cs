using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace WhereTo.Models
{
    public class Event : BaseDataObject
    {
        public Event()
        {
        }
        

        public override bool Equals(object obj)
        {
            return EventName.Equals((obj as Event)?.EventName) && EventLocation.Equals((obj as Event)?.EventLocation);
        }

        public override int GetHashCode()
        {
            return EventName.GetHashCode() + EventLocation.GetHashCode();
        }

        public override string ToString()
        {
            return $"Event : {EventName} at {EventLocation.ToString()} - {Description}";
        }

        private string _eventName = string.Empty;
        private string _cathegory = string.Empty;
        private Position _eventLocation = new Position();
        private DateTime _startDateTime = new DateTime();
        private DateTime _endtDateTime = new DateTime();
        private string _description = string.Empty;

        public string Cathegory
        {
            get => _cathegory;
            set => SetProperty(ref _cathegory, value);
        }
        public string EventName
        {
            get => _eventName;
            set => SetProperty(ref _eventName, value);
        }

        public Position EventLocation
        {
            get => _eventLocation;
            set => SetProperty(ref _eventLocation, value);
        }

  

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public TimeSpan StartTime
        {
            get => _startDateTime.TimeOfDay;
            set => _startDateTime = new DateTime(_startDateTime.Year, _startDateTime.Month, _startDateTime.Day, value.Hours, value.Minutes, value.Seconds);
        }
        public DateTime StartDate
        {
            get => _startDateTime.Date;
            set => _startDateTime = new DateTime(value.Year, value.Month, value.Day, _startDateTime.Hour, _startDateTime.Minute, _startDateTime.Second);
        }
        public TimeSpan EndTime
        {
            get => _endtDateTime.TimeOfDay;
            set => _endtDateTime = new DateTime(_startDateTime.Year, _startDateTime.Month, _startDateTime.Day, value.Hours, value.Minutes, value.Seconds);
        }
        public DateTime EndDate
        {
            get => _endtDateTime.Date;
            set => _endtDateTime = new DateTime(value.Year, value.Month, value.Day, _startDateTime.Hour, _startDateTime.Minute, _startDateTime.Second);
        }
    }
}
