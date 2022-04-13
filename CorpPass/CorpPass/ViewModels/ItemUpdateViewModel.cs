using System;
using System.Collections.Generic;
using System.Diagnostics;
using CorpPass.Models;
using CorpPass.Services;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    internal class ItemUpdateViewModel : BaseViewModel
    {
        #region VARIABLES

        private readonly List<string> _folderList;
        private readonly List<string> _groupList;
        private readonly FolderItems _itemsFolderModel;
        private readonly GroupItems _itemsGroupModel;
        private string _created;
        private string _description;
        private string _folder;
        private string _group;
        private string _icon;
        private string _itemId;
        private string _login;
        private string _name;
        private string _password;
        private int _selectedFolder;
        private int _selectedGroup;

        #endregion VARIABLES

        #region CONSTRUCTORS

        public ItemUpdateViewModel()
        {
            _itemsFolderModel = new FolderItems();
            _itemsGroupModel = new GroupItems();

            _folderList = _itemsFolderModel.GetFoldersList();
            _groupList = _itemsGroupModel.GetGroupsNameList();

            this.PropertyChanged += (_, __) => UpdateCommand.ChangeCanExecute();
            UpdateCommand = new Command(UpdateItemAndFixHistory, ValidateSave);
        }

        #endregion CONSTRUCTORS

        #region PROPERTIES

        public string Created
        {
            get => _created;
            set => SetProperty(ref _created, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string Folder
        {
            get => _folder;
            set => SetProperty(ref _folder, value);
        }

        public List<string> FolderList
        {
            get => _folderList;
        }

        public string Group
        {
            get => _group;
            set => SetProperty(ref _group, value);
        }

        public List<string> GroupList
        {
            get => _groupList;
        }

        public string Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        public string Id { get; set; }

        public string ItemId
        {
            get
            {
                return _itemId;
            }
            set
            {
                _itemId = value;
                LoadItemById(value);
            }
        }

        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public int SelectedFolder
        {
            get => _selectedFolder;
            set => SetProperty(ref _selectedFolder, value);
        }

        public int SelectedGroup
        {
            get => _selectedGroup;
            set => SetProperty(ref _selectedGroup, value);
        }

        public Command UpdateCommand { get; }
        private Item OldValue { get; set; }

        #endregion PROPERTIES

        #region METHODS

        public async void LoadItemById(string itemId)
        {
            var preferencesSecurityModel = new PreferencesSecurity();
            var passPhrase = await preferencesSecurityModel.GetSecureData(PreferencesKeys.PassPhrase);

            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                OldValue = item;

                Created = item.Created;
                Description = item.Description;
                Folder = item.Folder;
                Group = item.Group;
                Icon = item.Icon;
                Id = item.Id;
                Login = item.Login;
                Name = item.Name;
                Password = StringCipher.Decrypt(item.Password, passPhrase);

                SelectedGroup = _groupList.IndexOf(item.Group);
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public async void UpdateItemAndFixHistory()
        {
            try
            {
                var preferencesSecurityModel = new PreferencesSecurity();
                var passPhrase = await preferencesSecurityModel.GetSecureData(PreferencesKeys.PassPhrase);

                var updatedItem = new Item()
                {
                    Created = Created,
                    Description = Description,
                    Folder = Folder,
                    Group = Group,
                    Icon = Icon,
                    Id = Id,
                    LastModified = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                    Login = Login,
                    Name = Name,
                    Password = StringCipher.Encrypt(Password, passPhrase),
                    PasswordScore = PasswordSecurity.CheckStrength(Password)
                };

                var historyItem = new HistoryItem()
                {
                    Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                    ItemId = Id,
                    WasChanged = AnalyzeChanges(OldValue, updatedItem)
                };

                await HistoryItemDataStore.AddHistoryItemAsync(historyItem);
                await DataStore.UpdateItemAsync(updatedItem);

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private string AnalyzeChanges(Item oldObject, Item updatedItem)
        {
            var changes = new List<string>();

            if (oldObject.Name != updatedItem.Name)
            {
                changes.Add("Name");
            }

            if (oldObject.Login != updatedItem.Login)
            {
                changes.Add("Login");
            }

            if (oldObject.Password != updatedItem.Password)
            {
                changes.Add("Password");
            }

            if (oldObject.Description != updatedItem.Description)
            {
                changes.Add("Description");
            }

            if (oldObject.Icon != updatedItem.Icon)
            {
                changes.Add("Icon");
            }

            if (oldObject.Folder != updatedItem.Folder)
            {
                changes.Add("Folder");
            }

            if (oldObject.Group != updatedItem.Group)
            {
                changes.Add("Group");
            }

            return string.Join(",", changes);
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(_login)
                && !string.IsNullOrWhiteSpace(_password)
                && !string.IsNullOrWhiteSpace(_name)
                && !string.IsNullOrWhiteSpace(_group)
                && !string.IsNullOrWhiteSpace(_folder);
        }

        #endregion METHODS
    }
}