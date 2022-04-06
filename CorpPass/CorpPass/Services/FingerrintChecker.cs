using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

namespace CorpPass.Services
{
    public enum FingerprintScanKeys
    {
        Availability,
        Authed
    }

    public static class FingerrintChecker
    {
        public static async Task<bool> CheckFingerprintAvailibility()
        {
            return await CrossFingerprint.Current.IsAvailableAsync();
        }

        public static async Task<Dictionary<FingerprintScanKeys, bool>> UseFingerprint()
        {
            var fingerprintResults = new Dictionary<FingerprintScanKeys, bool>();
            var availibility = await CheckFingerprintAvailibility();

            if (!availibility)
            {
                fingerprintResults.Add(FingerprintScanKeys.Authed, false);
            }
            else
            {
                var authResult = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Let`s check your finger", "Touch the scanner"));

                if (authResult.Authenticated)
                {
                    fingerprintResults.Add(FingerprintScanKeys.Authed, true);
                }
                else
                {
                    fingerprintResults.Add(FingerprintScanKeys.Authed, false);
                }
            }

            return fingerprintResults;
        }
    }
}