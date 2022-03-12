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
            items = new List<Item>();
            string[] alphabet = new[] { "A", "B", "C", "D", "E", "F" };


            for (int i = 0; i < 50; i++)
            {
                var rnd = new Random();
                var groupIndex = rnd.Next(alphabet.Length);

                items.Add(new Item
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = $"Name - {i}",
                    Login = $"Item - {i}",
                    Description = $"Description for item - {i}",
                    Group = $"{alphabet[groupIndex]}",
                    Folder = $"Folder - {i}"
                });
            }
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