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
    public class ItemsGroup : List<Item>
    {
        public string Name { get; private set; }

        public ItemsGroup(string name, List<Item> items) : base(items)
        {
            Name = name;
        }
    }

    public class ItemsViewModel : BaseViewModel
    {
        bool isFavoriteBusy = false;
        public bool IsFavoriteBusy
        {
            get { return isFavoriteBusy; }
            set { SetProperty(ref isFavoriteBusy, value); }
        }
        public ObservableCollection<ItemsGroup> GroupedItems { get; private set; }
        public ObservableCollection<Item> GroupedFavoriteItems { get; private set; }
        public List<Item> Items { get; set; }
        public Command LoadItemsCommand { get; }
        public Command LoadFavoriteItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }
        public Command<Item> AdditionalTappedCommand { get; }
        public Command SearchPageRedirect { get; }
        public Command MenuPageRedirect { get; }
        private Item _selectedItem;
        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        public ItemsViewModel()
        {
            GroupedItems = new ObservableCollection<ItemsGroup>();
            GroupedFavoriteItems = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadFavoriteItemsCommand = new Command(async () => await ExecuteLoadFavoriteItemsCommand());
            ItemTapped = new Command<Item>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
            MenuPageRedirect = new Command(OnOpenMenuPage);
            SearchPageRedirect = new Command(OnOpenSearchPage);
        }
        public void OnAppearing()
        {
            IsBusy = true;
            IsFavoriteBusy = true;
            SelectedItem = null;
        }

        async Task ExecuteLoadFavoriteItemsCommand()
        {
            IsFavoriteBusy = true;

            try
            {
                var items = (await DataStore.GetItemsAsync(true)).ToList().OrderBy(i => i.Group).ToList();

                GroupedFavoriteItems.Clear();

                foreach (var item in items)
                {
                    if (item.IsFavorite)
                    {
                        GroupedFavoriteItems.Add(item);
                    }
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
                var items = (await DataStore.GetItemsAsync(true)).ToList().OrderBy(i=>i.Group).ToList();
                
                GroupedItems.Clear();
                                
                var grouped = items.GroupBy(i => i.Group).ToList();
                foreach (var item in grouped)
                {
                    GroupedItems.Add(new ItemsGroup(item.Key, item.ToList()));
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

        private async void OnOpenMenuPage(object obj)
        {
            await Shell.Current.GoToAsync(nameof(MenuPage));
        }

        private async void OnOpenSearchPage(object obj)
        {
            await Shell.Current.GoToAsync(nameof(SearchPage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

    }
}