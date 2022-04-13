using System.Collections.Generic;
using System.Threading.Tasks;
using CorpPass.Models;
using SQLite;
using Xamarin.Essentials;

namespace CorpPass.Services
{
    public class DatabaseSQLite
    {
        private readonly SQLiteAsyncConnection _database;
        private readonly PreferencesSecurity _preferencesModel;

        public DatabaseSQLite(string dbPath)
        {
            _preferencesModel = new PreferencesSecurity();
            var dbKey = _preferencesModel.GetSecureData(PreferencesKeys.PassPhrase).Result;

            var options = new SQLiteConnectionString(dbPath, true, dbKey);

            _database = new SQLiteAsyncConnection(options);
            _database.CreateTableAsync<Item>().Wait();
            _database.CreateTableAsync<HistoryItem>().Wait();
        }

        #region Items

        public async Task<int> DeleteAllItemsAsync()
        {
            return await _database.DeleteAllAsync<Item>();
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

        #endregion Items

        #region HISTORYITEM

        public List<HistoryItem> GetItemHistory()
        {
            return _database.Table<HistoryItem>().ToListAsync().Result;
        }

        public Task<List<HistoryItem>> GetItemHistoryById(string id)
        {
            return _database.Table<HistoryItem>().Where(i => i.ItemId == id).ToListAsync();
        }

        #endregion HISTORYITEM
    }
}