using CorpPass.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string text;
        private string description;
        private string name;

        private List<string> groupList;
        private List<string> folderList;


        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            groupList = new List<string>
            {
                "Group 1",
                "Group 2",
                "Group 3"
            };

            folderList = new List<string>
            {
                "Folder 1",
                "Folder 2",
                "Folder 3"
            };

            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(text)
                && !string.IsNullOrWhiteSpace(description);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
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

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Login = Text,
                Description = Description,
                Name = Name,
            };

            await DataStore.AddItemAsync(newItem);

            await Shell.Current.GoToAsync("..");
        }
    }
}
