using CorpPass.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            ImportCommand = new Command(LoadDataFromFile);

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

        private async void LoadDataFromFile()
        {
            try
            {
                PickOptions options = new PickOptions();
                FilePickerFileType customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "public.text" } },
                    { DevicePlatform.Android, new[] { "text/plain" } },
                });

                options.FileTypes = customFileType;

                var file = await FilePicker.PickAsync(options);
                if (file != null)
                {
                    var stream = await file.OpenReadAsync();
                    var streamReader = new StreamReader(stream);
                    string text = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"FILE PICKER ERROR = {ex.Message}");
            }
        }
    }
}