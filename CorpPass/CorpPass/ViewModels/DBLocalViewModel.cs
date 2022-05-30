using CorpPass.Models;
using CorpPass.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    internal class DBLocalViewModel : BaseViewModel
    {
        public DBLocalViewModel()
        {
            ActionsList = new List<ItemsGroup<CollectionListItem>>();
            RemoveAllCommand = new Command(RemoveAllItems);
            ImportCommand = new Command(LoadDataToDb);
            ExportCommand = new Command(ExportDataToFile);

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

        private async void ExportDataToFile()
        {
            try
            {
#pragma warning disable CS0618
                string downloadsFolderPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);
#pragma warning restore CS0618
                var items = (await DataStore.GetItemsAsync(true)).ToList().OrderBy(i => i.Name).ToList();
                var formatedLines = new List<string>();

                foreach (var item in items)
                {
                    var formatedItem = ItemConverter.ConvertItemToCSV(item);
                    formatedLines.Add(formatedItem);
                }

                var fileName = $"corppass_{DateTime.UtcNow.ToString("d-M-yyyy")}.csv";
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(downloadsFolderPath, fileName)))
                {
                    foreach (var formatedLine in formatedLines)
                    {
                        outputFile.WriteLine(formatedLine);
                    }
                }

                await Application.Current.MainPage.DisplayToastAsync("Data was export successfully ✅");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayToastAsync($"{ex.Message} 💔");
            }
        }

        private async Task<List<Item>> LoadDataFromFile()
        {
            try
            {
                var file = await FilePicker.PickAsync();
                if (file != null)
                {
                    var stream = await file.OpenReadAsync();
                    var streamReader = new StreamReader(stream);
                    string text = streamReader.ReadToEnd();

                    await Application.Current.MainPage.DisplayToastAsync("Data was import successfully! ✅");
                    return ItemConverter.ConvertCSVToItemModel(text);
                }

                await Application.Current.MainPage.DisplayToastAsync("File not found 💔");
                return new List<Item>();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayToastAsync($"{ex.Message} 💔");
                return new List<Item>();
            }
        }

        private async void LoadDataToDb()
        {
            var itemsFromFile = await LoadDataFromFile();
            var existingItems = (await DataStore.GetItemsAsync(true)).ToList().OrderBy(i => i.Name).ToList();

            foreach (var item in itemsFromFile)
            {
                var isExist = existingItems.Contains(item);

                if (!isExist)
                {
                    await DataStore.AddItemAsync(item);
                }
            }
        }
    }
}