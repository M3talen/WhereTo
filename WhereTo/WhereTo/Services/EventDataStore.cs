using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WhereTo.Models;

namespace WhereTo.Services
{
    class EventDataStore : IDataStore<Event>
    {
        private HttpClient _httpClient = new HttpClient();

        public async Task<bool> AddItemAsync(Event item)
        {
            var data = JsonConvert.SerializeObject(item);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost:5000/api/todo/item", content);
            var result = JsonConvert.DeserializeObject<int>(response.Content.ReadAsStringAsync().Result);
            return result == 1 ? true:false;
        }

        public Task<bool> DeleteItemAsync(Event item)
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Event>> GetItemsAsync(bool forceRefresh = false)
        {
            var json = await _httpClient.GetStringAsync("http://wheretoservice.azurewebsites.net/api/values");
            var events = JsonConvert.DeserializeObject<List<Event>>(json);
            return events;
        }

        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> PullLatestAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SyncAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(Event item)
        {
            throw new NotImplementedException();
        }
    }
}
