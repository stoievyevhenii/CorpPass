using CorpPass.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CorpPass.Services
{
    public class DatabaseSQLite
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseSQLite(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Item>().Wait();
        }

        public async Task<int> DeleteItem(string id)
        {
            var item = await GetItemAsync(id);
            return await _database.DeleteAsync(item);
        }

        public Task<Item> GetItemAsync(string id)
        {
            return _database.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public List<Item> GetItemsAsync()
        {
            var items = _database.Table<Item>().ToListAsync().Result;
            var itemsForCheckOnPasswordLeaked = items;

            var networkState = Connectivity.NetworkAccess.ToString();

            if (networkState != "None")
            {
                Task.Run(() =>
                {
                    var passwrodSecurityModel = new PasswordLeaksChecker();

                    foreach (var item in itemsForCheckOnPasswordLeaked)
                    {
                        var hash = passwrodSecurityModel.GetSHA1(item.Password);
                        item.IsLeaked = passwrodSecurityModel.PasswdIsLeaked(hash).Result;

                        UpdateItem(item);
                    }
                });
            }

            return items;
        }

        public Task<int> SaveItemAsync<T>(T item)
        {
            return _database.InsertAsync(item);
        }

        public Task<int> UpdateItem<T>(T item)
        {
            return _database.UpdateAsync(item);
        }
    }
}