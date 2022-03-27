using CorpPass.Models;
using CorpPass.Views;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        public MenuPageViewModel()
        {
            RedirectToSettingsPage = new Command(OpenSettingsPage);
            RedirectToAboutPage = new Command(OpenAboutPage);
            MenuPagesList = new List<ItemsGroup<CollectionListItem>>();

            InitSettingsPages();
        }

        public List<ItemsGroup<CollectionListItem>> MenuPagesList { get; }
        public Command RedirectToAboutPage { get; }
        public Command RedirectToSettingsPage { get; }

        private void InitSettingsPages()
        {
            MenuPagesList.Add(new ItemsGroup<CollectionListItem>("Configs", new List<CollectionListItem>()
            {
                new CollectionListItem()
                {
                    Name = "Settings",
                    Icon = "icon_settings",
                    ItemCommand = RedirectToSettingsPage
                }
            }));

            MenuPagesList.Add(new ItemsGroup<CollectionListItem>("Additional", new List<CollectionListItem>()
            {
                new CollectionListItem()
                {
                    Name = "Details",
                    Icon = "icon_details",
                    ItemCommand = RedirectToAboutPage
                }
            }));
        }

        private async void OpenAboutPage()
        {
            await Shell.Current.GoToAsync(nameof(AboutPage));
        }

        private async void OpenSettingsPage()
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }
    }
}