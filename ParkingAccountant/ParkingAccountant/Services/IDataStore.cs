using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingAccountant.Services
{
    public interface IDataStore<T>
    {
        Task<int> AddDataAsync(T item);
        Task<int> UpdateDataAsync(T item);
        Task<int> DeleteDataAsync(string id);
        Task<T> GetDataAsync(string id);
        Task<IEnumerable<T>> GetDatasAsync(bool forceRefresh = false);
  
    }
}
