using System;
using System.Collections.Generic;
using System.Diagnostics;
using CorpPass.Models;
using CorpPass.Services;
using CorpPass.Views;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        #region PRIVATEVARS

        public string created;
        public string lastModified;
        public int passwordScore;
        public string passwordStrenght;
        private string _description;
        private string _folder;
        private string _group;
        private string _icon;
        private Icon _iconModel;
        private bool _isLeaked;
        private string _itemId;
        private string _login;
        private string _name;
        private string _password;
        private RadialGaugeChart _radialChart;

        #endregion PRIVATEVARS

        #region PUBLICVARS

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
            get => _radialChart;
            set => SetProperty(ref _radialChart, value);
        }

        public string Created
        {
            get => created;
            set => SetProperty(ref created, value);
        }

        public Command DeleteItem { get; }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string Folder
        {
            get => _folder;
            set => SetProperty(ref _folder, value);
        }

        public string Group
        {
            get => _group;
            set => SetProperty(ref _group, value);
        }

        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        public Icon IconModel
        {
            get => _iconModel;
            set => SetProperty(ref _iconModel, value);
        }

        public string Id { get; set; }

        public bool IsLeaked
        {
            get => _isLeaked;
            set => SetProperty(ref _isLeaked, value);
        }

        public string ItemId
        {
            get
            {
                return _itemId;
            }
            set
            {
                _itemId = value;
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
            get => _login;
            set => SetProperty(ref _login, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public Command OnChangeFavoriteStatus { get; }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public int PasswordScore
        {
            get => passwordScore;
            set => SetProperty(ref passwordScore, value);
        }

        public string PasswordStrenght
        {
            get => passwordStrenght;
            set => SetProperty(ref passwordStrenght, value);
        }

        public Command UpdateItem { get; }

        #endregion PUBLICVARS

        #region METHODS

        public async void ChangeFavoriteStatus()
        {
            var selectedItem = await DataStore.GetItemAsync(ItemId);
            selectedItem.IsFavorite = selectedItem.IsFavorite == true ? false : true;
            await DataStore.UpdateItemAsync(selectedItem);
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

        #endregion METHODS
    }
}