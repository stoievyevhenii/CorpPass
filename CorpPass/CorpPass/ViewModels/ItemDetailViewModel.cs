using CorpPass.Models;
using CorpPass.Services;
using CorpPass.Views;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        public string created;
        public string lastModified;
        public int passwordScore;
        public string passwordStrenght;
        private string description;
        private string folder;
        private string group;
        private string icon;
        private Icon iconModel;
        private bool isLeaked;
        private string itemId;
        private string login;
        private string name;
        private string password;
        private RadialGaugeChart radialChart;

        public ItemDetailViewModel()
        {
            BottomSheetItems = new List<CollectionListItem>();
            DeleteItem = new Command(OnDeleteItem);
            OnChangeFavoriteStatus = new Command(ChangeFavoriteStatus);
            UpdateItem = new Command(OnEditItem);

            InitItemContextMenuItems();
        }

        public List<CollectionListItem> BottomSheetItems { get; set; }

        public RadialGaugeChart Chart
        {
            get => radialChart;
            set => SetProperty(ref radialChart, value);
        }

        public string Created
        {
            get => created;
            set => SetProperty(ref created, value);
        }

        public Command DeleteItem { get; }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string Folder
        {
            get => folder;
            set => SetProperty(ref folder, value);
        }

        public string PasswordStrenght
        {
            get => passwordStrenght;
            set => SetProperty(ref passwordStrenght, value);
        }

        public string Group
        {
            get => group;
            set => SetProperty(ref group, value);
        }

        public string Icon
        {
            get => icon;
            set => SetProperty(ref icon, value);
        }

        public Icon IconModel
        {
            get => iconModel;
            set => SetProperty(ref iconModel, value);
        }

        public string Id { get; set; }

        public bool IsLeaked
        {
            get => isLeaked;
            set => SetProperty(ref isLeaked, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public string LastModified
        {
            get => lastModified;
            set => SetProperty(ref lastModified, value);
        }

        public string Login
        {
            get => login;
            set => SetProperty(ref login, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public Command OnChangeFavoriteStatus { get; }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public int PasswordScore
        {
            get => passwordScore;
            set => SetProperty(ref passwordScore, value);
        }

        public Command UpdateItem { get; }

        public async void ChangeFavoriteStatus()
        {
            var selectedItem = await DataStore.GetItemAsync(ItemId);
            selectedItem.IsFavorite = selectedItem.IsFavorite == true ? false : true;
            await DataStore.UpdateItemAsync(selectedItem);
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);

                Id = item.Id;
                Icon = item.Icon;
                IconModel = item.IconModel;
                Name = item.Name;
                Login = item.Login;
                Password = item.Password;
                Group = item.Group;
                Folder = item.Folder;
                Description = item.Description;
                Created = item.Created;
                LastModified = item.LastModified;
                IsLeaked = item.IsLeaked;
                PasswordScore = Convert.ToInt32(item.PasswordScore * 100);
                PasswordStrenght = PasswordSecurity.ConvertPointsToStrenghtStatus(item.PasswordScore);

                InitChart(item.PasswordScore, item.IconModel);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public async void OnDeleteItem()
        {
            await DataStore.DeleteItemAsync(ItemId);
            await Shell.Current.GoToAsync("..");
        }

        public void InitChart(double passwdScore, Icon icon)
        {
            var passwordScores = Convert.ToInt32(passwdScore * 100);
            var chartColor = icon.Accent.ToHex();

            var entries = new[]
            {
                new ChartEntry(passwordScores)
                {
                    Color = SKColor.Parse(chartColor),
                },
            };

            var chart = new RadialGaugeChart
            {
                Entries = entries,
                Margin = 0,
                BackgroundColor = SKColor.Empty,
                MaxValue = 100
            };

            Chart = chart;
        }

        private void InitItemContextMenuItems()
        {
            BottomSheetItems.Add(new CollectionListItem()
            {
                Icon = "icon_edit",
                Name = "Edit",
                ItemCommand = UpdateItem
            });
            BottomSheetItems.Add(new CollectionListItem()
            {
                Icon = "icon_delete",
                Name = "Delete",
                ItemCommand = DeleteItem
            });
            BottomSheetItems.Add(new CollectionListItem()
            {
                Icon = "icon_favorite",
                Name = "Change favorite status",
                ItemCommand = OnChangeFavoriteStatus
            });
        }

        private async void OnEditItem()
        {
            await Shell.Current.GoToAsync($"{nameof(ItemUpdatePage)}?{nameof(ItemUpdateViewModel.ItemId)}={ItemId}");
        }
    }
}