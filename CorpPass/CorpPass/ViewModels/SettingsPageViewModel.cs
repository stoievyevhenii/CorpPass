using CorpPass.Models;
using CorpPass.Views;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        public Command RedirectToDBConnectPage { get; }
        public Command RedirectToPINPage { get; }
        public List<ItemsGroup<CollectionListItem>> SettingsPagesList { get; }


        public SettingsPageViewModel()
        {
            RedirectToDBConnectPage = new Command(OpenDBSettings);
            RedirectToPINPage = new Command(OpenPINSettings);
            SettingsPagesList = new List<ItemsGroup<CollectionListItem>>();

            InitSettingsPages();
        }

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
