using CorpPass.Models;
using CorpPass.Services;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string name;
        private string icon;
        private string login;
        private string password;
        private string group;
        private string folder;
        private string description;
        private Icon iconModel;

        public string Id { get; set; }
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
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
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
        public Icon IconModel
        {
            get => iconModel;
            set => SetProperty(ref iconModel, value);
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);                
                
                Id = item.Id;
                Icon = item.Icon;
                IconModel = item.IconModel;
                Name = item.Name;
                Login = item.Login;
                Password = item.Password;
                Group = item.Group;
                Folder = item.Folder;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
