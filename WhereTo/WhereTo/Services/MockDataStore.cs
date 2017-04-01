using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WhereTo.Models;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

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

            items.Add(item);

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

        public async Task InitializeAsync()
        {
            if (isInitialized)
                return;

            items = new List<Event>();
            var _items = new List<Event>
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
                },
            };

            foreach (Event item in _items)
            {
                items.Add(item);
            }

            isInitialized = true;
        }
    }
}
