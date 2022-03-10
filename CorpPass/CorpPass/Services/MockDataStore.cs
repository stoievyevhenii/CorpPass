using CorpPass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorpPass.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Login = "First item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Login = "Second item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Login = "Third item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Login = "Fourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Login = "Fifth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Login = "Sixth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Login = "Seventh item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Login = "Fifth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Login = "Nineth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Login = "10", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Login = "11", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Login = "12", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Login = "13", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Login = "14", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Login = "15", Description="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}