using CorpPass.Models;
using CorpPass.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private readonly FolderItems _folderItemsModel;
        private readonly List<string> _folderList;
        private readonly GroupItems _groupItemsModel;
        private readonly List<string> _groupList;
        private string description;
        private string folder;
        private string group;
        private string icon;
        private string login;
        private string name;
        private string password;

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
            get => description;
            set => SetProperty(ref description, value);
        }

        public string Folder
        {
            get => folder;
            set => SetProperty(ref folder, value);
        }

        public List<string> FolderList
        {
            get => _folderList;
        }

        public string Group
        {
            get => group;
            set => SetProperty(ref group, value);
        }

        public List<string> GroupList
        {
            get => _groupList;
        }

        public string Icon
        {
            get => icon;
            set => SetProperty(ref icon, value);
        }

        public string Login
        {
            get => login;
            set => SetProperty(ref login, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
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
                Password = Password,
                PasswordScore = PasswordSecurity.CheckStrength(Password)
            };

            await DataStore.AddItemAsync(newItem);

            await Shell.Current.GoToAsync("..");
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(login)
                && !string.IsNullOrWhiteSpace(password)
                && !string.IsNullOrWhiteSpace(name);
        }
    }
}