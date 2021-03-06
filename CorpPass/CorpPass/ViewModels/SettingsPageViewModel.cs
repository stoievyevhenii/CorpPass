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
            RedirectToAboutPage = new Command(OpenAboutPage);
            RedirectToDBConnectPage = new Command(OpenDBSettings);
            RedirectToDBLocalPage = new Command(OpenDBLocalSettings);
            RedirectToPINPage = new Command(OpenPINSettings);
            SettingsPagesList = new List<ItemsGroup<CollectionListItem>>();
            RedirectToPassPharsePage = new Command(OpenPassPhraseSettings);

            InitSettingsPages();
        }

        public Command RedirectToAboutPage { get; }
        public Command RedirectToDBConnectPage { get; }
        public Command RedirectToDBLocalPage { get; }
        public Command RedirectToPassPharsePage { get; }
        public Command RedirectToPINPage { get; }
        public List<ItemsGroup<CollectionListItem>> SettingsPagesList { get; }

        private void InitSettingsPages()
        {
            SettingsPagesList.Add(new ItemsGroup<CollectionListItem>("Data", new List<CollectionListItem>()
            {
                new CollectionListItem()
                {
                    Name = "Cloud DB",
                    Icon = "icon_dbconnection",
                    ItemCommand = RedirectToDBConnectPage
                },
                new CollectionListItem()
                {
                    Name = "Local DB",
                    Icon = "icon_dblocal",
                    ItemCommand = RedirectToDBLocalPage
                }
            }));

            SettingsPagesList.Add(new ItemsGroup<CollectionListItem>("Security", new List<CollectionListItem>()
            {
                new CollectionListItem()
                {
                    Name = "PIN",
                    Icon = "icon_lock",
                    ItemCommand = RedirectToPINPage
                },
                new CollectionListItem()
                {
                    Name = "Password phrase",
                    Icon = "icon_generate_password",
                    ItemCommand = RedirectToPassPharsePage
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

        private async void OpenDBLocalSettings()
        {
            await Shell.Current.GoToAsync(nameof(DBLocalPage));
        }

        private async void OpenDBSettings()
        {
            await Shell.Current.GoToAsync(nameof(DBConnectionPage));
        }

        private async void OpenPassPhraseSettings()
        {
            await Shell.Current.GoToAsync(nameof(PassPhrasePage));
        }

        private async void OpenPINSettings()
        {
            await Shell.Current.GoToAsync(nameof(PINSettigsPage));
        }
    }
}