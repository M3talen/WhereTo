using System;
using System.Collections.Generic;
using System.Text;
using FFImageLoading.Work;
using Xamarin.Forms.GoogleMaps;
using ImageSource = Xamarin.Forms.ImageSource;

namespace WhereTo.Models
{
    public enum EventCathegory
    {
        Drink ,
        Food ,
        Sport
    }

    public class Event : BaseDataObject
    {
        public Event()
        {
        }

        private string _eventName = string.Empty;
        private EventCathegory _cathegory;
        private Position _eventLocation = new Position();
        private DateTime _startDateTime = DateTime.Now;
        private DateTime _endtDateTime = DateTime.Now;
        private string _description = string.Empty;
        private ImageSource _icon = string.Empty;

        public EventCathegory Cathegory
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

        public ImageSource Icon
        {
            get
            {
                switch (Cathegory)
                {
                    case EventCathegory.Drink:
                        return ImageSource.FromFile("ico4.png");
                    case EventCathegory.Food:
                        return ImageSource.FromFile("ico3.png");
                    case EventCathegory.Sport:
                        return ImageSource.FromFile("ico2.png");
                }
                return _icon;
            }
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
    }
}
