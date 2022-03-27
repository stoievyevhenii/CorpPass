using Xamarin.Essentials;

namespace CorpPass.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private string appName;
        private string build;
        private string packageName;
        private string version;

        public AboutViewModel()
        {
            AppName = AppInfo.Name;
            PackageName = AppInfo.PackageName;
            Version = AppInfo.VersionString;
            Build = AppInfo.BuildString;
        }

        public string AppName
        {
            get { return appName; }
            set { SetProperty(ref appName, value); }
        }

        public string Build
        {
            get { return build; }
            set { SetProperty(ref build, value); }
        }

        public string PackageName
        {
            get { return packageName; }
            set { SetProperty(ref packageName, value); }
        }

        public string Version
        {
            get { return version; }
            set { SetProperty(ref version, value); }
        }
    }
}