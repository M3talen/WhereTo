using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhereTo.Models;

namespace WhereTo.Services
{
    public interface IUserDataStore
    {
        Task<bool> AddItemAsync(User _user);
        Task<bool> UpdateItemAsync(User _user);
        Task<bool> DeleteItemAsync(User _user);
        Task<User> GetItemAsync(int id);
        Task<IEnumerable<User>> GetItemsAsync();
    }
}
