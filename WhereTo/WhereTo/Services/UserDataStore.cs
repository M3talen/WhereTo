using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WhereTo.Models;
using Newtonsoft.Json;
using System.Linq;

namespace WhereTo.Services
{
    public class UserDataStore : IUserDataStore
    {
        private HttpClient _httpClient = new HttpClient();
        private string _url = "http://wheretoservice.azurewebsites.net/api/user";
        List<User> items = new List<User>();

        public async Task<bool> AddItemAsync(User _user)
        {
            var data = JsonConvert.SerializeObject(_user);
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

        public async Task<bool> DeleteItemAsync(User _user)
        {
            var _item = items.FirstOrDefault(arg => arg.Id == _user.Id);
            items.Remove(_item);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_url + _user.Id);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "DELETE";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Console.WriteLine(httpResponse.StatusCode);
            return await Task.FromResult(true);
        }

        public async Task<User> GetItemAsync(int id)
        {
            string url = _url + "/" + id;
            var json = await _httpClient.GetStringAsync(url);
            var user = JsonConvert.DeserializeObject<User>(json);
            return await Task.FromResult(user);
        }

        public async Task<IEnumerable<User>> GetItemsAsync()
        {
            items.Clear();
            var json = await _httpClient.GetStringAsync(_url);
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(json);
            var _users = users.ToList();

            foreach (User u in _users)
            {
                if (u != null)
                    items.Add(u);
            }

            return await Task.FromResult(items);
        }

        public async Task<bool> UpdateItemAsync(User _user)
        {
            var _item = items.FirstOrDefault(arg => arg.Id == _user.Id);
            items.Remove(_item);
            items.Add(_user);

            return await Task.FromResult(true);
        }
    }
}
