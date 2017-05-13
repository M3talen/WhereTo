using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhereTo.Models;

namespace WhereTo.Services
{
    public interface IEventDataStore
    {
        Task<bool> AddItemAsync(Event _event);
        Task<bool> UpdateItemAsync(Event _event);
        Task<bool> DeleteItemAsync(Event _event);
        Task<Event> GetItemAsync(int id);
        Task<IEnumerable<Event>> GetItemsAsync(double longitude, double latitude, double radius);
    }
}
