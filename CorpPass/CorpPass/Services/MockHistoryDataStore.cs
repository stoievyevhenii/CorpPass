using System.Collections.Generic;
using System.Threading.Tasks;
using CorpPass.Models;

namespace CorpPass.Services
{
    public class MockHistoryDataStore : IItemHistoryDataStore
    {
        public async Task<bool> AddHistoryItemAsync(HistoryItem item)
        {
            try
            {
                await App.Database.SaveItemAsync(item);
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public Task<bool> DeleteAllHistoryItemAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<HistoryItem>> GetHistoryItemsByIdAsync(string id)
        {
            var item = await App.Database.GetItemHistoryById(id);
            return item;
        }
    }
}