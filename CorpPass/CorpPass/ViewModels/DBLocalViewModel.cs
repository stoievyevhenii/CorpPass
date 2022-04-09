using System.Collections.Generic;
using CorpPass.Models;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    internal class DBLocalViewModel : BaseViewModel
    {
        public DBLocalViewModel()
        {
            ActionsList = new List<ItemsGroup<CollectionListItem>>();
            RemoveAllCommand = new Command(RemoveAllItems);
            InitActions();
        }

        public List<ItemsGroup<CollectionListItem>> ActionsList { get; }

        public Command ImportCommand { get; }
        public Command RemoveAllCommand { get; }
        public Command ExportCommand { get; }

        private void InitActions()
        {
            ActionsList.Add(new ItemsGroup<CollectionListItem>("Backup", new List<CollectionListItem>()
            {
                new CollectionListItem()
                {
                    Name = "Import",
                    Icon = "icon_import",
                    ItemCommand = ImportCommand
                },
                new CollectionListItem()
                {
                    Name = "Export",
                    Icon = "icon_export",
                    ItemCommand = ExportCommand
                }
            }));
            ActionsList.Add(new ItemsGroup<CollectionListItem>("Managed", new List<CollectionListItem>()
            {
                new CollectionListItem()
                {
                    Name = "Remove all data",
                    Icon = "icon_delete",
                    ItemCommand = RemoveAllCommand
                }
            }));
        }

        private async void RemoveAllItems()
        {
            var deleteAllActionIsSuccess = await DataStore.DeleteAllAsync();

            if (deleteAllActionIsSuccess)
            {
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}