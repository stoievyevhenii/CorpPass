using CorpPass.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorpPass.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        public async Task<bool> AddItemAsync(Item item)
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

        public async Task<bool> DeleteAllAsync()
        {
            try
            {
                await App.Database.DeleteAllItemsAsync();
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            try
            {
                await App.Database.DeleteItem(id);
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<Item> GetItemAsync(string id)
        {
            try
            {
                var iconModel = new IconsSet();
                var table = App.Database.GetItemsAsync();
                var item = table.Where(i => i.Id == id).FirstOrDefault();

                item.IconModel = iconModel.GetIconModel(item.Icon);

                return item;
            }
            catch
            {
                var itemModel = new Item();
                var defaultItem = itemModel.GetEmptyItem();
                return await Task.FromResult(defaultItem);
            }
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            try
            {
                var iconModel = new IconsSet();
                var items = App.Database.GetItemsAsync();

                items = iconModel.SetIconForItem(items);

                return items;
            }
            catch
            {
                return await Task.FromResult(new List<Item>());
            }
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            try
            {
                await App.Database.UpdateItem(item);
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }
    }
}