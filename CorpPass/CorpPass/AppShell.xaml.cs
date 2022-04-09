using CorpPass.Views;
using Xamarin.Forms;

namespace CorpPass
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
            Routing.RegisterRoute(nameof(GroupItemsPage), typeof(GroupItemsPage));
            Routing.RegisterRoute(nameof(GroupsPage), typeof(GroupsPage));
            Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(ItemUpdatePage), typeof(ItemUpdatePage));
            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
            Routing.RegisterRoute(nameof(MenuPage), typeof(MenuPage));
            Routing.RegisterRoute(nameof(DBConnectionPage), typeof(DBConnectionPage));
            Routing.RegisterRoute(nameof(FavoritesPage), typeof(FavoritesPage));
            Routing.RegisterRoute(nameof(PINSettigsPage), typeof(PINSettigsPage));
            Routing.RegisterRoute(nameof(PassPhrasePage), typeof(PassPhrasePage));
        }

        public void GoToGroupsPage()
        {
            Shell.Current.GoToAsync(nameof(GroupsPage));
        }
    }
}