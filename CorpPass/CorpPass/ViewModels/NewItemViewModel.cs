using CorpPass.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string icon;
        private string login;
        private string password;
        private string group;
        private string folder;
        private string description;
        private string name;

        private List<string> groupList;
        private List<string> folderList;

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
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public List<string> GroupList
        {
            get => groupList;
        }
        public List<string> FolderList
        {
            get => folderList;
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);

            groupList = new List<string>();
            folderList = new List<string>();

            string[] alphabet = new[] { "A", "B", "C", "D", "E", "F" };

            for (var i = 0; i < alphabet.Length; i++)
            {
                groupList.Add($"{alphabet[i]}");
                folderList.Add($"{alphabet[i]}");
            }

            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(login)
                && !string.IsNullOrWhiteSpace(password)
                && !string.IsNullOrWhiteSpace(name);
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            var selectedGroupIndex = int.Parse(Group);
            var selectedFolderIndex = int.Parse(Folder);

            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Icon = Icon,
                Login = Login,
                Password = Password,
                Description = Description,
                Name = Name,
                Group = groupList[selectedGroupIndex],
                Folder = folderList[selectedFolderIndex]
            };

            await DataStore.AddItemAsync(newItem);

            await Shell.Current.GoToAsync("..");
        }
    }
}
