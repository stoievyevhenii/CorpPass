using System;
using System.Collections.Generic;
using CorpPass.Models;
using CorpPass.Services;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private readonly FolderItems _folderItemsModel;
        private readonly List<string> _folderList;
        private readonly GroupItems _groupItemsModel;
        private readonly List<string> _groupList;
        private string _description;
        private string _folder;
        private string _group;
        private string _icon;
        private string _login;
        private string _name;
        private string _password;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);

            _groupItemsModel = new GroupItems();
            _groupList = _groupItemsModel.GetGroupsNameList();

            _folderItemsModel = new FolderItems();
            _folderList = _folderItemsModel.GetFoldersList();

            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public Command CancelCommand { get; }

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

        public Command SaveCommand { get; }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var selectedGroupIndex = int.Parse(Group);
            var selectedIcon = string.IsNullOrEmpty(Icon) ? "icon_add" : Icon.Replace("File:", "").Trim();

            var iconSetModel = new IconsSet();
            var icon = iconSetModel.GetIconsSet().Find(i => i.Name == selectedIcon);

            var preferencesSecurityModel = new PreferencesSecurity();
            var passPhrase = await preferencesSecurityModel.GetSecureData(PreferencesKeys.SavePassPhrase);

            Item newItem = new Item()
            {
                Created = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                Description = Description,
                Folder = Folder,
                Group = _groupList[selectedGroupIndex],
                Icon = icon.Name,
                Id = Guid.NewGuid().ToString(),
                IsLeaked = false,
                LastModified = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                Login = Login,
                Name = Name,
                Password = StringCipher.Encrypt(Password, passPhrase),
                PasswordScore = PasswordSecurity.CheckStrength(Password)
            };

            await DataStore.AddItemAsync(newItem);

            await Shell.Current.GoToAsync("..");
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(_login)
                && !string.IsNullOrWhiteSpace(_password)
                && !string.IsNullOrWhiteSpace(_name)
                && !string.IsNullOrWhiteSpace(_folder)
                && !string.IsNullOrWhiteSpace(_group);
        }
    }
}