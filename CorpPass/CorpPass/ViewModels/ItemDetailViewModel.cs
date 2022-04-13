using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
        #region VARIABLES

        private bool _isLeaked;
        private Icon _iconModel;
        private RadialGaugeChart _radialChart;
        private string _description;
        private string _folder;
        private string _group;
        private string _icon;
        private string _itemId;
        private string _login;
        private string _name;
        private string _password;

        public int passwordScore;
        public string created;
        public string lastModified;
        public string passwordStrenght;

        #endregion VARIABLES

        #region PROPERTIES

        public ItemDetailViewModel()
        {
            BottomSheetItems = new List<CollectionListItem>();
            DeleteItem = new Command(OnDeleteItem);
            HistoryItemsList = new ObservableCollection<HistoryItem>();
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

        public ObservableCollection<HistoryItem> HistoryItemsList { get; set; }

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

        #endregion PROPERTIES

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

                var preferencesSecurityModel = new PreferencesSecurity();
                var passPhrase = await preferencesSecurityModel.GetSecureData(PreferencesKeys.PassPhrase);

                Created = item.Created;
                Description = item.Description;
                Folder = item.Folder;
                Group = item.Group;
                Icon = item.Icon;
                IconModel = item.IconModel;
                Id = item.Id;
                IsLeaked = item.IsLeaked;
                LastModified = item.LastModified;
                Login = item.Login;
                Name = item.Name;
                Password = StringCipher.Decrypt(item.Password, passPhrase);
                PasswordScore = Convert.ToInt32(item.PasswordScore * 100);
                PasswordStrenght = PasswordSecurity.ConvertPointsToStrenghtStatus(item.PasswordScore);

                InitChart(item.PasswordScore, item.IconModel);
                LoadItemsHistory(item.Id);
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

        private async void LoadItemsHistory(string id)
        {
            HistoryItemsList.Clear();

            try
            {
                var items = (await HistoryItemDataStore.GetHistoryItemsByIdAsync(id)).OrderByDescending(x => x.Date);

                foreach (var item in items)
                {
                    HistoryItemsList.Add(item);
                }
            }
            catch { }
        }

        private async void OnEditItem()
        {
            await Shell.Current.GoToAsync($"{nameof(ItemUpdatePage)}?{nameof(ItemUpdateViewModel.ItemId)}={ItemId}");
        }

        #endregion METHODS
    }
}