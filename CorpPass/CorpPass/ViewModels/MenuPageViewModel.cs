using CorpPass.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        public Command RedirectToDBConnectPage { get; }

        public MenuPageViewModel()
        {
            RedirectToDBConnectPage = new Command(OpenDBSettings);
        }       
        
        private async void OpenDBSettings()
        {
            await Shell.Current.GoToAsync(nameof(DBConnectionPage));
        }
    }
}
