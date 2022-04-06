using System.Collections.Generic;
using CorpPass.Models;
using CorpPass.Services;
using CorpPass.Views;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    public class GroupsViewModel : BaseViewModel
    {
        public GroupsViewModel()
        {
            RedirectToGroupPageCommand = new Command<string>(RedirectToGroupPage);
            GroupsList = new List<ItemsGroup<CollectionListItem>>();
            InitSettingsPages();
        }

        public List<ItemsGroup<CollectionListItem>> GroupsList { get; }
        public Command RedirectToGroupPageCommand { get; }

        private void InitSettingsPages()
        {
            var groupItemsModel = new GroupItems();
            var groupsItemsList = groupItemsModel.GetGroupsItems();

            foreach (var groupItem in groupsItemsList)
            {
                groupItem.ItemCommand = RedirectToGroupPageCommand;
            }

            GroupsList.Add(new ItemsGroup<CollectionListItem>("Available groups", groupsItemsList));
        }

        private async void RedirectToGroupPage(string group)
        {
            if (group == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(GroupItemsPage)}?{nameof(GroupItemsViewModel.GroupName)}={group}");
        }
    }
}