using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WhereTo.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(WhereTo.Services.EventDataStore))]
namespace WhereTo.Services
{
    public class EventDataStore : IEventDataStore
    {
        private HttpClient _httpClient = new HttpClient();
        private string _url = "http://wheretoservice.azurewebsites.net/api/event";
        List<Event> items = new List<Event>();

        public async Task<bool> AddItemAsync(Event item)
        {
            var data = JsonConvert.SerializeObject(item);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_url);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
                streamWriter.Flush();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Console.WriteLine(httpResponse.StatusCode);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Event item)
        {
            var _item = items.FirstOrDefault(arg => arg.Id == item.Id);
            items.Remove(_item);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_url +"/"+ item.Id);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "DELETE";
          
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Console.WriteLine(httpResponse.StatusCode);
            return await Task.FromResult(true);
        }

        public async Task<Event> GetItemAsync(int id)
        {
            string url = _url + "/" + id;
            var json = await _httpClient.GetStringAsync(url);
            var events = JsonConvert.DeserializeObject<Event>(json);
            return await Task.FromResult(events);
        }

        public async Task<IEnumerable<Event>> GetItemsAsync(double longitude, double latitude,double radius)
        {
            items.Clear();
            string url = _url + "/long:"+longitude +"lat:"+latitude+"radius:"+radius;
            var json = await _httpClient.GetStringAsync(url);
            var events = JsonConvert.DeserializeObject<IEnumerable<Event>>(json);
            var _items = events.ToList();

            foreach (Event item in _items)
            {
                if (item != null)
                {
                    items.Add(item);
                }
            }

            return await Task.FromResult(items);
        }
        
        public async Task<bool> UpdateItemAsync(Event item)
        {
            var _item = items.FirstOrDefault(arg => arg.Id == item.Id);
            items.Remove(_item);
            items.Add(item);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_url + "/"+ item.Id);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "PUT";
            var data = JsonConvert.SerializeObject(item);
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Console.WriteLine(httpResponse.StatusCode);
            return await Task.FromResult(true);
        }
    }
}
