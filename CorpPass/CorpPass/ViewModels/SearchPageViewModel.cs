using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CorpPass.Models;
using CorpPass.Views;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    public class SearchPageViewModel : BaseViewModel
    {
        public SearchPageViewModel()
        {
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            PerformSearch = new Command<string>(async (string query) => await StartSearch(query));
            ItemTapped = new Command<Item>(OnItemSelected);
            Items = new ObservableCollection<Item>();
        }

        public ObservableCollection<Item> Items { get; set; }
        public Command<Item> ItemTapped { get; }
        public Command LoadItemsCommand { get; }
        public Command PerformSearch { get; }

        public async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            Items.Clear();
            try
            {
                var items = (await DataStore.GetItemsAsync(true)).ToList();

                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch { }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        public async Task StartSearch(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                await ExecuteLoadItemsCommand();
            }

            IsBusy = true;
            Items.Clear();
            try
            {
                var items = await DataStore.GetItemsAsync();
                var filteredList = items.Where(i => i.Name.Contains(query)).ToList();

                foreach (var item in filteredList)
                {
                    Items.Add(item);
                }
            }
            catch { }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}