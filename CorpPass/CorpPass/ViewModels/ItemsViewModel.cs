using CorpPass.Models;
using CorpPass.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace CorpPass.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }
        public Command<Item> AdditionalTappedCommand { get; }
        public Command SearchPageRedirect { get; }
        public Command MenuPageRedirect { get; }

        public ItemsViewModel()
        {
            Items = new ObservableCollection<Item>();
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
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
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