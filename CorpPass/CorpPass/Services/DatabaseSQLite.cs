using CorpPass.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public Task<List<Item>> GetItemsAsync()
        {
            return _database.Table<Item>().ToListAsync();
        }
        public Task<int> SaveItemAsync<T>(T item)
        {
            return _database.InsertAsync(item);
        }
        public Task<int> UpdateItem<T>(T item)
        {
            return _database.UpdateAsync(item);
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
    }
}
