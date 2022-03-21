using CorpPass.Models;
using CorpPass.Views;
using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CorpPass.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        #region VARS

        private bool isFavoriteBusy = false;
        private Item _selectedItem;

        public bool IsFavoriteBusy
        {
            get { return isFavoriteBusy; }
            set { SetProperty(ref isFavoriteBusy, value); }
        }
        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }
        public ObservableCollection<ItemsGroup<Item>> GroupedItems { get; private set; }
        public ObservableCollection<ItemsGroup<Item>> GroupedFavoriteItems { get; private set; }
        public Command LoadItemsCommand { get; }
        public Command LoadFavoriteItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command SearchPageRedirect { get; }
        public Command MenuPageRedirect { get; }
        public Command<string> DeleteItem { get; }
        public Command<string> OnChangeFavoriteStatus { get; }
        public Command<string> UpdateItem { get; }
        public Command<Item> ItemTapped { get; }
        public Command<Item> AdditionalTappedCommand { get; }
        public List<Item> Items { get; set; }
        public List<CollectionListItem> BottomSheetItems { get; set; }

        #endregion

        public ItemsViewModel()
        {
            GroupedItems = new ObservableCollection<ItemsGroup<Item>>();
            GroupedFavoriteItems = new ObservableCollection<ItemsGroup<Item>>();
            BottomSheetItems = new List<CollectionListItem>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadFavoriteItemsCommand = new Command(async () => await ExecuteLoadFavoriteItemsCommand());
            ItemTapped = new Command<Item>(OnItemSelected);
            DeleteItem = new Command<string>(OnDeleteItem);
            UpdateItem = new Command<string>(OnEditItem);
            OnChangeFavoriteStatus = new Command<string>(ChangeFavoriteStatus);
            AddItemCommand = new Command(OnAddItem);

            MenuPageRedirect = new Command(OnOpenMenuPage);
            SearchPageRedirect = new Command(OnOpenSearchPage);

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
                Name = "Add to favorite",
                ItemCommand = OnChangeFavoriteStatus
            });
        }

        #region COMMANDS
        async Task ExecuteLoadFavoriteItemsCommand()
        {
            IsFavoriteBusy = true;

            try
            {
                var items = (await DataStore.GetItemsAsync(true)).ToList().OrderBy(i => i.Group).ToList();

                GroupedFavoriteItems.Clear();
                var tempList = new ObservableCollection<Item>();

                foreach (var item in items)
                {
                    if (item.IsFavorite)
                    {
                        tempList.Add(item);
                    }
                }

                var groupedFavoriteList = tempList.GroupBy(i => i.Group).ToList();

                foreach (var item in groupedFavoriteList)
                {
                    GroupedFavoriteItems.Add(new ItemsGroup<Item>(item.Key, item.ToList()));
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
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                var items = (await DataStore.GetItemsAsync(true)).ToList().OrderBy(i => i.Group).ToList();

                GroupedItems.Clear();

                var grouped = items.GroupBy(i => i.Group).ToList();
                foreach (var item in grouped)
                {
                    GroupedItems.Add(new ItemsGroup<Item>(item.Key, item.ToList()));
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
        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        private async void OnEditItem(string itemId)
        {
            await Shell.Current.GoToAsync($"{nameof(ItemUpdatePage)}?{nameof(ItemUpdateViewModel.ItemId)}={itemId}");
        }
        public async void OnDeleteItem(string itemId)
        {
            await DataStore.DeleteItemAsync(itemId);
            OnAppearing();
        }
        public async void ChangeFavoriteStatus(string itemId)
        {
            var selectedItem = await DataStore.GetItemAsync(itemId);
            selectedItem.IsFavorite = selectedItem.IsFavorite == true ? false : true;
            await DataStore.UpdateItemAsync(selectedItem);

            OnAppearing();
        }
        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }
        private async void OnOpenMenuPage(object obj)
        {
            await Shell.Current.GoToAsync(nameof(MenuPage));
        }
        private async void OnOpenSearchPage(object obj)
        {
            await Shell.Current.GoToAsync(nameof(SearchPage));
        }
        #endregion
    }
}