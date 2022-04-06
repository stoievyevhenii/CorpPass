using System.Collections.Generic;
using System.Linq;

namespace CorpPass.Services
{
    public class FolderItems
    {
        private List<string> ItemsFolders;

        public FolderItems()
        {
            ExtractFoldersList();
        }

        public List<string> GetFoldersList()
        {
            return ItemsFolders.Distinct().ToList();
        }

        private void ExtractFoldersList()
        {
            var items = App.Database.GetItemsAsync();

            ItemsFolders = new List<string>();

            foreach (var item in items)
            {
                var itemFolder = item.Folder;
                ItemsFolders.Add(itemFolder);
            }
        }
    }
}