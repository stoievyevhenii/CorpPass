using CorpPass.Models;
using CorpPass.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    public class SearchPageViewModel : BaseViewModel
    {
        public Command LoadItemsCommand { get; }
        public Command PerformSearch { get; }
        public Command<Item> ItemTapped { get; }
        public ObservableCollection<Item> Items { get; set; }

        public SearchPageViewModel()
        {
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            PerformSearch = new Command<string>(async (string query) => await StartSearch(query));
            ItemTapped = new Command<Item>(OnItemSelected);
            Items = new ObservableCollection<Item>();
        }

        public async Task StartSearch(string query)
        {
            try
            {
                IsBusy = true;
                Items.Clear();

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
        public async void OnAppearing()
        {
            IsBusy = true;
            await ExecuteLoadItemsCommand();
        }
        public async Task ExecuteLoadItemsCommand()
        {
            Items.Clear();
            IsBusy = true;

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
        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}
