using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorpPass.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);

        Task<bool> DeleteItemAsync(string id);

        Task<bool> DeleteAllAsync();

        Task<T> GetItemAsync(string id);

        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

        Task<bool> UpdateItemAsync(T item);
    }
}