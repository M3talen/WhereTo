using System;
using Newtonsoft.Json;
using WhereTo.Helpers;

namespace WhereTo.Models
{
    public class BaseDataObject : ObservableObject
    {
        public BaseDataObject()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Id for item
        /// </summary>

        [JsonIgnore]
        public string Id { get; set; }
    }
}
