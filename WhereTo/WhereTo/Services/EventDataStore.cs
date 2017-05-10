using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.Util;
using WhereTo.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(WhereTo.Services.EventDataStore))]
namespace WhereTo.Services
{
    class EventDataStore : IDataStore<Event>
    {
        private HttpClient _httpClient = new HttpClient();

        bool isInitialized;
        List<Event> items = new List<Event>();

        public async Task<bool> AddItemAsync(Event item)
        {
            var data = JsonConvert.SerializeObject(item);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://wheretoservice.azurewebsites.net/api/event");
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

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://wheretoservice.azurewebsites.net/api/event/" + item.Id);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "DELETE";
          
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Console.WriteLine(httpResponse.StatusCode);
            return await Task.FromResult(true);
        }

        public async Task<Event> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Event>> GetItemsAsync(bool forceRefresh = false)
        {

            var _items = await GetAsync();
            items.Clear();
            foreach (Event item in _items)
            {
                if (item != null)
                {
                    items.Add(item);
                }
            }

            return await Task.FromResult(items);
        }

        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }


        private async Task<List<Event>> GetAsync()
        {
            items.Clear();
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("http://wheretoservice.azurewebsites.net/api/event/");
            Log.Error("json", json);
            var events = JsonConvert.DeserializeObject<IEnumerable<Event>>(json);
            return events.ToList();
        }

        public Task<bool> PullLatestAsync()
        {
            return Task.FromResult(true);
        }


        public Task<bool> SyncAsync()
        {
            return Task.FromResult(true);
        }


        public async Task<bool> UpdateItemAsync(Event item)
        {
            var _item = items.FirstOrDefault(arg => arg.Id == item.Id);
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }
    }
}
