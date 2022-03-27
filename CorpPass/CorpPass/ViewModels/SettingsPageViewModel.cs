using CorpPass.Models;
using CorpPass.Views;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        public SettingsPageViewModel()
        {
            RedirectToDBConnectPage = new Command(OpenDBSettings);
            RedirectToPINPage = new Command(OpenPINSettings);
            RedirectToAboutPage = new Command(OpenAboutPage);
            SettingsPagesList = new List<ItemsGroup<CollectionListItem>>();

            InitSettingsPages();
        }

        public Command RedirectToAboutPage { get; }
        public Command RedirectToDBConnectPage { get; }
        public Command RedirectToPINPage { get; }
        public List<ItemsGroup<CollectionListItem>> SettingsPagesList { get; }

        private void InitSettingsPages()
        {
            SettingsPagesList.Add(new ItemsGroup<CollectionListItem>("Data", new List<CollectionListItem>()
            {
                new CollectionListItem()
                {
                    Name = "Save to DB",
                    Icon = "icon_dbconnection",
                    ItemCommand = RedirectToDBConnectPage
                }
            }));

            SettingsPagesList.Add(new ItemsGroup<CollectionListItem>("Security", new List<CollectionListItem>()
            {
                new CollectionListItem()
                {
                    Name = "PIN",
                    Icon = "icon_lock",
                    ItemCommand = RedirectToPINPage
                }
            }));
            SettingsPagesList.Add(new ItemsGroup<CollectionListItem>("About", new List<CollectionListItem>()
            {
                new CollectionListItem()
                {
                    Name = "Info",
                    Icon = "icon_details",
                    ItemCommand = RedirectToAboutPage
                }
            }));
        }

        private async void OpenAboutPage()
        {
            await Shell.Current.GoToAsync(nameof(AboutPage));
        }

        private async void OpenDBSettings()
        {
            await Shell.Current.GoToAsync(nameof(DBConnectionPage));
        }

        private async void OpenPINSettings()
        {
            await Shell.Current.GoToAsync(nameof(PINSettigsPage));
        }
    }
}