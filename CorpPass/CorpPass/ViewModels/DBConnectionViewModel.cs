using CorpPass.Models;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    internal class DBConnectionViewModel : BaseViewModel
    {
        private string password;
        private string serverAdress;
        private string userName;

        public DBConnectionViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);

            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public Command SaveCommand { get; }

        public string ServerAddress
        {
            get => serverAdress;
            set => SetProperty(ref serverAdress, value);
        }

        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        private async void OnSave()
        {
            DB dbConnection = new DB()
            {
                ServerAddress = ServerAddress,
                Login = UserName,
                Password = Password
            };

            await Shell.Current.GoToAsync("..");
            //TODO: Save data in SQLlite
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(serverAdress)
                && !string.IsNullOrWhiteSpace(userName)
                && !string.IsNullOrWhiteSpace(password);
        }
    }
}