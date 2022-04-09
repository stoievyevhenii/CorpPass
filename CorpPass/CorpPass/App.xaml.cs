using System;
using System.IO;
using CorpPass.Services;
using Xamarin.Forms;

namespace CorpPass
{
    public partial class App : Application
    {
        private static DatabaseSQLite database;

        public static DatabaseSQLite Database
        {
            get
            {
                if (database == null)
                {
                    database = new DatabaseSQLite(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Items.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}