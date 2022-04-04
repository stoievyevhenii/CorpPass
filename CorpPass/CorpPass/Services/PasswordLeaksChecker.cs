using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CorpPass.Services
{
    public class PasswordLeaksChecker
    {
        private readonly HttpClient client;

        public PasswordLeaksChecker()
        {
            client = new HttpClient();
        }

        public string GetSHA1(string word)
        {
            using (SHA1 sha1Hash = SHA1.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(word);
                byte[] hashBytes = sha1Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                return hash;
            }
        }

        public async Task<bool> PasswdIsLeaked(string fullHash)
        {
            var findedPasswdList = await GetListOfHashes(fullHash);
            bool isFinded = false;

            foreach (var item in findedPasswdList)
            {
                if (item.Contains(fullHash))
                {
                    isFinded = true;
                }
            }

            return isFinded;
        }

        private async Task<string[]> GetListOfHashes(string fullHash)
        {
            string hashPrefix = fullHash.Substring(0, 5);
            string url = $"https://api.pwnedpasswords.com/range/{hashPrefix}";

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var responseBodyArray = responseBody.Split(Environment.NewLine.ToCharArray());
                var completedHash = responseBodyArray.Select(x => hashPrefix + x).ToArray();

                return completedHash;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);

                return Array.Empty<string>();
            }
        }
    }
}