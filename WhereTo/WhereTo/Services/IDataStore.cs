﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace WhereTo.Services
{
	public interface IDataStore<T>
	{
		Task<bool> AddItemAsync(T item);
		Task<bool> UpdateItemAsync(T item);
		Task<bool> DeleteItemAsync(T item);
		Task<T> GetItemAsync(string id);
		Task<IEnumerable<T>> GetItemsAsync();
        Task<IEnumerable<T>> GetItemsAsync(double longitude, double latitude,double radius);

        Task InitializeAsync();
		Task<bool> PullLatestAsync();
		Task<bool> SyncAsync();
	}
}
