using CorpPass.Models;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    internal class DBConnectionViewModel : BaseViewModel
    {
        private string _password;
        private string _serverAddress;
        private string _userName;

        public DBConnectionViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);

            PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public Command SaveCommand { get; }

        public string ServerAddress
        {
            get => _serverAddress;
            set => SetProperty(ref _serverAddress, value);
        }

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private async void OnSave()
        {
            var dbConnection = new DB
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
            return !string.IsNullOrWhiteSpace(_serverAddress)
                   && !string.IsNullOrWhiteSpace(_userName)
                   && !string.IsNullOrWhiteSpace(_password);
        }
    }
}