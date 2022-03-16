﻿using CorpPass.Views;
using Xamarin.Forms;

namespace CorpPass
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
            Routing.RegisterRoute(nameof(MenuPage), typeof(MenuPage));
            Routing.RegisterRoute(nameof(DBConnectionPage), typeof(DBConnectionPage));
            Routing.RegisterRoute(nameof(FavoritesPage), typeof(FavoritesPage));

        }

    }
}
