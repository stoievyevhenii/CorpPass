using Xamarin.Essentials;

namespace CorpPass.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private string _appName;
        private string _build;
        private string _packageName;
        private string _version;

        public AboutViewModel()
        {
            AppName = AppInfo.Name;
            PackageName = AppInfo.PackageName;
            Version = AppInfo.VersionString;
            Build = AppInfo.BuildString;
        }

        public string AppName
        {
            get => _appName;
            set => SetProperty(ref _appName, value);
        }

        public string Build
        {
            get => _build;
            set => SetProperty(ref _build, value);
        }

        public string PackageName
        {
            get => _packageName;
            set => SetProperty(ref _packageName, value);
        }

        public string Version
        {
            get => _version;
            set => SetProperty(ref _version, value);
        }
    }
}