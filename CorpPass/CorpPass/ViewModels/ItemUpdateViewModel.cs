using CorpPass.Models;
using CorpPass.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    internal class ItemUpdateViewModel : BaseViewModel
    {
        private string itemId;
        private string name;
        private string login;
        private string icon;
        private string password;
        private string group;
        private string folder;
        private string description;
        private int selectedFolder;
        private int selectedGroup;
        private readonly GroupItems _itemsGroupModel;
        private readonly FolderItems _itemsFolderModel;

        public string Id { get; set; }
        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string Login
        {
            get => login;
            set => SetProperty(ref login, value);
        }
        public string Icon
        {
            get => icon;
            set => SetProperty(ref icon, value);
        }
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        public string Group
        {
            get => group;
            set => SetProperty(ref group, value);
        }
        public string Folder
        {
            get => folder;
            set => SetProperty(ref folder, value);
        }
        public List<string> GroupList
        {
            get => _groupList;
        }
        public List<string> FolderList
        {
            get => _folderList;
        }
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public int SelectedFolder
        {
            get => selectedFolder;
            set => SetProperty(ref selectedFolder, value);
        }
        public int SelectedGroup
        {
            get => selectedGroup;
            set => SetProperty(ref selectedGroup, value);
        }

        public Command UpdateCommand { get; }

        private List<string> _groupList;
        private List<string> _folderList;

        public ItemUpdateViewModel()
        {
            _itemsGroupModel = new GroupItems();
            _groupList = _itemsGroupModel.GetGroupsNameList();

            _itemsFolderModel = new FolderItems();
            _folderList = _itemsFolderModel.GetFoldersList();
            
            UpdateCommand = new Command(UpdateItem, ValidateSave);

            this.PropertyChanged +=
                (_, __) => UpdateCommand.ChangeCanExecute();
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);

                Id = item.Id;
                Name = item.Name;
                Icon = item.Icon;
                Login = item.Login;
                Password = item.Password;
                Group = item.Group;
                Folder = item.Folder;
                Description = item.Description;

                SelectedGroup = _groupList.IndexOf(item.Group);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public async void UpdateItem()
        {
            var updatedItem = new Item()
            {
                Id = Id,
                Name = Name,
                Icon = Icon,
                Login = Login,
                Password = Password,
                Group = _groupList[int.Parse(Group)],
                Folder = Folder,
                Description = Description
            };

            await DataStore.UpdateItemAsync(updatedItem);
            await Shell.Current.GoToAsync("..");
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(login)
                && !string.IsNullOrWhiteSpace(password)
                && !string.IsNullOrWhiteSpace(name)
                && !string.IsNullOrWhiteSpace(group)
                && !string.IsNullOrWhiteSpace(folder);
        }
    }
}
