using CorpPass.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorpPass.Services
{
    public class FolderItems
    {
        private List<string> ItemsFolders;

        public FolderItems()
        {
            ExtractFoldersList();
        }

        private void ExtractFoldersList()
        {
            var items = Task.Run(async() => await App.Database.GetItemsAsync()).Result;
            
            ItemsFolders = new List<string>();
            
            foreach (var item in items)
            {
                var itemFolder = item.Folder;
                ItemsFolders.Add(itemFolder);
            }
        }
        
        public List<string> GetFoldersList()
        {
            return ItemsFolders;
        }
    }
}
