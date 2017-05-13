using System;
using Newtonsoft.Json;
using WhereTo.Helpers;

namespace WhereTo.Models
{
    public class BaseDataObject : ObservableObject
    {
        public BaseDataObject()
        {
        }

        /// <summary>
        /// Id for item
        /// </summary>

        [JsonIgnore]
        public int Id { get; set; }
    }
}
