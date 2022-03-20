using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CorpPass.Services
{
    internal class PreferencesSecurity
    {
        internal async void SetSecureData(string key, string data)
        {
            await SecureStorage.SetAsync(key, data);
        }

        internal async Task<string> GetSecureData(string key)
        {
            string password = "";
            
            try
            {
                password = await SecureStorage.GetAsync(key);
            }
            catch {
                password = "";
            }

            return password;
        }
    }
}
