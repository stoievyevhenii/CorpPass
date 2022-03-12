using CorpPass.Models;
using Xamarin.Forms;

namespace CorpPass.ViewModels
{
    internal class DBConnectionViewModel : BaseViewModel
    {
        private string serverAdress;
        private string userName;
        private string password;

        public DBConnectionViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);

            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(serverAdress)
                && !string.IsNullOrWhiteSpace(userName)
                && !string.IsNullOrWhiteSpace(password);
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

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
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
    }
}
