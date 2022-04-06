using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CorpPass.Models;
using CorpPass.Services;
using CorpPass.Views;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        #region VARS

        private IconsSet _iconsSetModel = new IconsSet();
        private Item _selectedItem;
        private bool isFavoriteBusy = false;
        private string totalItemsCount;
        private string totalLeakedItemsCount;

        public Command AddItemCommand { get; }

        public Command<Item> AdditionalTappedCommand { get; }

        public List<CollectionListItem> BottomSheetItems { get; set; }

        public Command<string> DeleteItem { get; }

        public ObservableCollection<ItemsGroup<Item>> GroupedFavoriteItems { get; private set; }

        public ObservableCollection<ItemsGroup<Item>> GroupedItems { get; private set; }

        public Command GroupsPageRedirect { get; }

        public bool IsFavoriteBusy
        {
            get { return isFavoriteBusy; }
            set { SetProperty(ref isFavoriteBusy, value); }
        }

        public List<Item> Items { get; set; }

        public Command<Item> ItemTapped { get; }

        public Command LoadFavoriteItemsCommand { get; }

        public Command LoadItemsCommand { get; }

        public Command<string> OnChangeFavoriteStatus { get; }

        public Command SearchPageRedirect { get; }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        public Command SettingsPageRedirect { get; }

        public string TotalItemsCount
        {
            get { return totalItemsCount; }
            set { SetProperty(ref totalItemsCount, value); }
        }

        public string TotalLeakedItemsCount
        {
            get { return totalLeakedItemsCount; }
            set { SetProperty(ref totalLeakedItemsCount, value); }
        }

        public Command<string> UpdateItem { get; }

        #endregion VARS

        public ItemsViewModel()
        {
            BottomSheetItems = new List<CollectionListItem>();
            GroupedFavoriteItems = new ObservableCollection<ItemsGroup<Item>>();
            GroupedItems = new ObservableCollection<ItemsGroup<Item>>();

            AddItemCommand = new Command(OnAddItem);
            DeleteItem = new Command<string>(OnDeleteItem);
            ItemTapped = new Command<Item>(OnItemSelected);
            LoadFavoriteItemsCommand = new Command(async () => await ExecuteLoadFavoriteItemsCommand());
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            OnChangeFavoriteStatus = new Command<string>(ChangeFavoriteStatus);
            UpdateItem = new Command<string>(OnEditItem);

            GroupsPageRedirect = new Command(OnOpenGroupsPage);
            SearchPageRedirect = new Command(OnOpenSearchPage);
            SettingsPageRedirect = new Command(OnOpenSettingsPage);

            InitItemContextMenuItems();
        }

        public void OnAppearing()
        {
            IsBusy = true;
            IsFavoriteBusy = true;
            SelectedItem = null;
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

        #region COMMANDS

        public async void ChangeFavoriteStatus(string itemId)
        {
            var selectedItem = await DataStore.GetItemAsync(itemId);
            selectedItem.IsFavorite = selectedItem.IsFavorite != true;
            await DataStore.UpdateItemAsync(selectedItem);

            OnAppearing();
        }

        public async void OnDeleteItem(string itemId)
        {
            await DataStore.DeleteItemAsync(itemId);
            OnAppearing();
        }

        private async Task ExecuteLoadFavoriteItemsCommand()
        {
            IsFavoriteBusy = true;

            try
            {
                var items = (await DataStore.GetItemsAsync(true)).ToList().OrderBy(i => i.Name).ToList();

                GroupedFavoriteItems.Clear();
                var tempList = new ObservableCollection<Item>();

                foreach (var item in items)
                {
                    if (item.IsFavorite)
                    {
                        tempList.Add(item);
                    }
                }

                var groupedFavoriteList = tempList.GroupBy(i => i.Name[0].ToString().ToUpper()).ToList();

                foreach (var item in groupedFavoriteList)
                {
                    GroupedFavoriteItems.Add(new ItemsGroup<Item>(item.Key.ToString().ToUpper(), item.ToList()));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsFavoriteBusy = false;
            }
        }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                var items = (await DataStore.GetItemsAsync(true)).ToList().OrderBy(i => i.Name).ToList();

                TotalItemsCount = items.Count.ToString();
                TotalLeakedItemsCount = items.Count(i => i.IsLeaked == true).ToString();

                GroupedItems.Clear();

                var grouped = items.GroupBy(i => i.Name[0].ToString().ToUpper()).ToList();
                foreach (var item in grouped)
                {
                    GroupedItems.Add(new ItemsGroup<Item>(item.Key.ToString().ToUpper(), item.ToList()));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        private async void OnEditItem(string itemId)
        {
            await Shell.Current.GoToAsync($"{nameof(ItemUpdatePage)}?{nameof(ItemUpdateViewModel.ItemId)}={itemId}");
        }

        private async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        private async void OnOpenGroupsPage(object obj)
        {
            await Shell.Current.GoToAsync(nameof(GroupsPage));
        }

        private async void OnOpenSearchPage(object obj)
        {
            await Shell.Current.GoToAsync(nameof(SearchPage));
        }

        private async void OnOpenSettingsPage(object obj)
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }

        #endregion COMMANDS
    }
}