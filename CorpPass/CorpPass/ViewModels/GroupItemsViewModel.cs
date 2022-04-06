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
    [QueryProperty(nameof(GroupName), nameof(GroupName))]
    public class GroupItemsViewModel : BaseViewModel
    {
        private readonly GroupItems _groupItemsModel;
        private string _groupName;
        private GroupItem _groupNameTitle;

        public GroupItemsViewModel()
        {
            _groupItemsModel = new GroupItems();

            GroupedItems = new ObservableCollection<ItemsGroup<Item>>();
            ItemTapped = new Command<Item>(OnItemSelected);
            BottomSheetItems = new List<CollectionListItem>();
            LoadItemsCommand = new Command(async () => await LoadGroupItemsAsync(_groupName));

            DeleteItem = new Command<string>(OnDeleteItem);
            UpdateItem = new Command<string>(OnEditItem);
            OnChangeFavoriteStatus = new Command<string>(ChangeFavoriteStatus);

            InitItemContextMenuItems();
        }

        public List<CollectionListItem> BottomSheetItems { get; set; }
        public Command<string> DeleteItem { get; }
        public ObservableCollection<ItemsGroup<Item>> GroupedItems { get; }

        public GroupItem GroupItemModel
        {
            get => _groupNameTitle;
            set => SetProperty(ref _groupNameTitle, value);
        }

        public string GroupName
        {
            get => _groupName;
            set
            {
                _groupName = value;
                Task.Run(() => LoadGroupItemsAsync(value));
            }
        }

        public Command<Item> ItemTapped { get; }
        public Command LoadItemsCommand { get; }

        public Command<string> OnChangeFavoriteStatus { get; }

        public Command<string> UpdateItem { get; }

        private async void ChangeFavoriteStatus(string itemId)
        {
            var selectedItem = await DataStore.GetItemAsync(itemId);
            selectedItem.IsFavorite = selectedItem.IsFavorite ? false : true;
            await DataStore.UpdateItemAsync(selectedItem);

            IsBusy = true;
        }

        private void InitItemContextMenuItems()
        {
            BottomSheetItems.Add(new CollectionListItem
            {
                Icon = "icon_edit",
                Name = "Edit",
                ItemCommand = UpdateItem
            });
            BottomSheetItems.Add(new CollectionListItem
            {
                Icon = "icon_delete",
                Name = "Delete",
                ItemCommand = DeleteItem
            });
            BottomSheetItems.Add(new CollectionListItem
            {
                Icon = "icon_favorite",
                Name = "Change favorite status",
                ItemCommand = OnChangeFavoriteStatus
            });
        }

        private async Task LoadGroupItemsAsync(string name)
        {
            IsBusy = true;
            GroupItemModel = _groupItemsModel.GetItemByName(name);
            GroupedItems.Clear();

            try
            {
                var items = (await DataStore.GetItemsAsync(true))
                    .Where(x => x.Group == name)
                    .OrderBy(i => i.Name)
                    .ToList();

                var grouped = items.GroupBy(i => i.Name[0]).ToList();
                foreach (var item in grouped)
                    GroupedItems.Add(new ItemsGroup<Item>(item.Key.ToString().ToUpper(), item.ToList()));
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

        private async void OnDeleteItem(string itemId)
        {
            await DataStore.DeleteItemAsync(itemId);
            IsBusy = true;
        }

        private async void OnEditItem(string itemId)
        {
            await Shell.Current.GoToAsync($"{nameof(ItemUpdatePage)}?{nameof(ItemUpdateViewModel.ItemId)}={itemId}");
        }

        private async void OnItemSelected(Item item)
        {
            if (string.IsNullOrEmpty(item.Id))
                return;

            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}