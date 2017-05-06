using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WhereTo.Models;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using System.Text;
using Android.Util;

[assembly: Dependency(typeof(WhereTo.Services.MockDataStore))]
namespace WhereTo.Services
{
    public class MockDataStore : IDataStore<Event>
    {
        bool isInitialized;
        List<Event> items;

        public async Task<bool> AddItemAsync(Event item)
        {
            await InitializeAsync();
            var _httpClient = new HttpClient();
            //items.Add(item);
            var data = JsonConvert.SerializeObject(item);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://wheretoservice.azurewebsites.net/api/values", content);
            var result = JsonConvert.DeserializeObject<int>(response.Content.ReadAsStringAsync().Result);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Event item)
        {
            await InitializeAsync();

            var _item = items.Where((Event arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Event item)
        {
            await InitializeAsync();

            var _item = items.Where((Event arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Event> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<Event> GetItemAsync()
        {
            await InitializeAsync();

            return await Task.FromResult(items.FirstOrDefault());
        }

        public async Task<IEnumerable<Event>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(items);
        }

        public Task<bool> PullLatestAsync()
        {
            return Task.FromResult(true);
        }


        public Task<bool> SyncAsync()
        {
            return Task.FromResult(true);
        }

        public async Task  InitializeAsync()
        {
            if (isInitialized)
                return;

            items = new List<Event>();
            /*var _items = new List<Event>
            {
                new Event
                {
                    Id = Guid.NewGuid().ToString(),
                    EventName = "Evening beer", Description="Live music and beer", Cathegory = EventCathegory.Drink,
                    StartTime = new TimeSpan(19,30,0), EndTime = new TimeSpan(23,0,0), EventLocation = new Position(45.7909984,15.9568062)
                },new Event
                {
                    Id = Guid.NewGuid().ToString(),
                    EventName = "Party", Description="Student party", Cathegory = EventCathegory.Drink,
                    StartTime = new TimeSpan(21,00,0), EndTime = new TimeSpan(23,0,0), EventLocation = new Position(45.7910022,15.9616193)
                },new Event
                {
                    Id = Guid.NewGuid().ToString(),
                    EventName = "Football traning", Description="Casual game", Cathegory = EventCathegory.Sport,
                    StartTime = new TimeSpan(17,00,0), EndTime = new TimeSpan(19,0,0), EventLocation = new Position(45.7934509,15.9591462)
                },new Event
                {
                    Id = Guid.NewGuid().ToString(),
                    EventName = "Dinner", Description="/", Cathegory = EventCathegory.Food,
                    StartTime = new TimeSpan(18,00,0), EndTime = new TimeSpan(19,0,0), EventLocation = new Position(45.7971932,15.9664646)
                },new Event
                {
                    Id = Guid.NewGuid().ToString(),
                    EventName = "Utakmica HRV-UKR", Description="Kvalifikacijska utakmica za SP", Cathegory = EventCathegory.Sport,
                    StartTime = new TimeSpan(18,00,0), EndTime = new TimeSpan(20,0,0), EventLocation = new Position(45.8184957,16.018656)
                },new Event
                {
                    Id = Guid.NewGuid().ToString(),
                    EventName = "In Flames trbt/Frantic Amber",
                    Description ="Jedan pravi međunarodni metal summit održat će se u Vintage Industrial Baru u srijedu 12.4.2017. u sklopu Good Vibrations programa!",
                    Cathegory = EventCathegory.Sport,
                    StartTime = new TimeSpan(20,00,0),
                    EndTime = new TimeSpan(23,0,0),
                    EventLocation = new Position(45.7902735,15.9552463)
                },new Event
                {
                    Id = Guid.NewGuid().ToString(),
                    EventName = "In Flames trbt/Frantic Amber",
                    Description ="Jedan pravi međunarodni metal summit održat će se u Vintage Industrial Baru u srijedu 12.4.2017. u sklopu Good Vibrations programa!",
                    Cathegory = EventCathegory.Sport,
                    StartDate = new DateTime(2017,4,12),
                    EndDate = new DateTime(2017,4,12),
                    StartTime = new TimeSpan(20,00,0),
                    EndTime = new TimeSpan(23,0,0),
                    EventLocation = new Position(45.7902735,15.9552463)
                },new Event
                {
                    Id = Guid.NewGuid().ToString(),
                    EventName = "Kraftwerk tribute by DJ Xed",
                    Description ="#Kraftwerk tribute povodom 40 godina najutjecajnijeg albuma elektronske glazbe Trans-Europe Express (1977.)!",
                    Cathegory = EventCathegory.Sport,
                    StartDate = new DateTime(2017,4,15),
                    EndDate = new DateTime(2017,4,16),
                    StartTime = new TimeSpan(20,00,0),
                    EndTime = new TimeSpan(6,0,0),
                    EventLocation = new Position(45.798648249129,15.970902442932)
                },new Event
                {
                    Id = Guid.NewGuid().ToString(),
                    EventName = "“I hate Mondays” Open mic night – kvalifikacije",
                    Description ="Nastavljamo potragu za novim komičarskim nadama u travnju!",
                    Cathegory = EventCathegory.Sport,
                    StartDate = new DateTime(2017,4,10),
                    EndDate = new DateTime(2017,4,10),
                    StartTime = new TimeSpan(20,00,0),
                    EndTime = new TimeSpan(23,0,0),
                    EventLocation = new Position(45.809362249487,15.976207852364)
                },
            };*/

            var _items = await  GetAsync();
           
            foreach (Event item in _items)
            {
                items.Add(item);
            }

            isInitialized = true;
        }

        public async Task<List<Event>> GetAsync()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("http://wheretoservice.azurewebsites.net/api/values");
            Log.Error("json",json);
            var events = JsonConvert.DeserializeObject<IEnumerable<Event>>(json);
            return events.ToList();
        }
    }
}
