using CorpPass.Models;
using CorpPass.Views;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using MvvmHelpers;
using Xamarin.Essentials;
using System.Collections.Generic;

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
        private Item _selectedItem;
        public List<ItemsGroup> GroupedItems { get; private set; }
        public List<Item> Items { get; set; }

        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }
        public Command<Item> AdditionalTappedCommand { get; }
        public Command SearchPageRedirect { get; }
        public Command MenuPageRedirect { get; }

        public ItemsViewModel()
        {
            Items = new List<Item>();
            GroupedItems = new List<ItemsGroup>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Item>(OnItemSelected);
            AdditionalTappedCommand = new Command<Item>(OnCopyToClipboard);
            AddItemCommand = new Command(OnAddItem);
            MenuPageRedirect = new Command(OnOpenMenuPage);
            SearchPageRedirect = new Command(OnOpenSearchPage);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                var items = (await DataStore.GetItemsAsync(true)).ToList().OrderBy(i=>i.Group).ToList();
                
                Items.Clear();

                foreach (var item in items)
                {
                    Items.Add(item);
                }

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

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
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

        async void OnCopyToClipboard(Item item)
        {
            if (item == null)
            {
                Debug.WriteLine("ITEMS IS EMPTY");
            }
            else
            {
                Debug.WriteLine($"ITEM LOGIN - {item.Login}");
                await Clipboard.SetTextAsync(item.Login);
            }
        }
    }
}