using CorpPass.Models;
using CorpPass.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    [QueryProperty(nameof(GroupName), nameof(GroupName))]
    public class GroupItemsViewModel : BaseViewModel
    {
        private string groupName;

        public GroupItemsViewModel()
        {
            GroupedItems = new ObservableCollection<ItemsGroup<Item>>();
            BottomSheetItems = new List<CollectionListItem>();
            LoadItemsCommand = new Command(async () => await LoadGroupItemsAsync(groupName));

            DeleteItem = new Command<string>(OnDeleteItem);
            UpdateItem = new Command<string>(OnEditItem);
            OnChangeFavoriteStatus = new Command<string>(ChangeFavoriteStatus);

            InitItemContextMenuItems();
        }

        public List<CollectionListItem> BottomSheetItems { get; set; }

        public Command<string> DeleteItem { get; }

        public ObservableCollection<ItemsGroup<Item>> GroupedItems { get; private set; }

        public string GroupName
        {
            get
            {
                return groupName;
            }
            set
            {
                groupName = value;
                Task.Run(() => LoadGroupItemsAsync(value));
            }
        }

        public Command LoadItemsCommand { get; }

        public Command<string> OnChangeFavoriteStatus { get; }

        public Command<string> UpdateItem { get; }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private async void ChangeFavoriteStatus(string itemId)
        {
            var selectedItem = await DataStore.GetItemAsync(itemId);
            selectedItem.IsFavorite = selectedItem.IsFavorite == true ? false : true;
            await DataStore.UpdateItemAsync(selectedItem);

            OnAppearing();
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

        private async Task LoadGroupItemsAsync(string name)
        {
            IsBusy = true;

            try
            {
                var items = (await DataStore.GetItemsAsync(true))
                    .Where(x => x.Group == name)
                    .OrderBy(i => i.Name)
                    .ToList();

                GroupedItems.Clear();

                var grouped = items.GroupBy(i => i.Name[0]).ToList();
                foreach (var item in grouped)
                {
                    GroupedItems.Add(new ItemsGroup<Item>(item.Key.ToString(), item.ToList()));
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

        private async void OnDeleteItem(string itemId)
        {
            await DataStore.DeleteItemAsync(itemId);
            OnAppearing();
        }

        private async void OnEditItem(string itemId)
        {
            await Shell.Current.GoToAsync($"{nameof(ItemUpdatePage)}?{nameof(ItemUpdateViewModel.ItemId)}={itemId}");
        }
    }
}