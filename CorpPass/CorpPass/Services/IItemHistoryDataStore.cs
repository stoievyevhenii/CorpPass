using System.Collections.Generic;
using System.Threading.Tasks;
using CorpPass.Models;

namespace CorpPass.Services
{
    public interface IItemHistoryDataStore
    {
        Task<bool> AddHistoryItemAsync(HistoryItem item);

        Task<bool> DeleteAllHistoryItemAsync();

        Task<List<HistoryItem>> GetHistoryItemsByIdAsync(string id);
    }
}