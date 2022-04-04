using System.Linq;

namespace CorpPass.Services
{
    public static class PasswordSecurity
    {
        public enum PasswordScore
        {
            Blank = 0,
            VeryWeak = 1,
            Weak = 2,
            Medium = 3,
            Strong = 4,
            VeryStrong = 5
        }

        public static double CheckStrength(string password)
        {
            int score = 0;

            if (password.Length < 1)
                return ConvertPointsToFloat(PasswordScore.Blank);
            if (password.Length < 4)
                return ConvertPointsToFloat(PasswordScore.VeryWeak);

            if (password.Length >= 8)
                score++;
            if (password.Length >= 12)
                score++;
            if (password.Any(char.IsUpper))
                score++;
            if (password.Any(char.IsLower))
                score++;

            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (password.Contains(item))
                {
                    score++;
                    break;
                }
            }

            return ConvertPointsToFloat((PasswordScore)score);
        }

        public static string ConvertPointsToStrenghtStatus(double points)
        {
            if (points <= 0.40)
            {
                return "Weak";
            }
            if (points > 0.40 && points <= 0.66)
            {
                return "Medium";
            }
            if (points > 0.66 && points <= 0.80)
            {
                return "Strong";
            }
            if (points > 0.80 && points <= 1)
            {
                return "VeryStrong";
            }

            return "Unknow";
        }

        internal static double ConvertPointsToFloat(PasswordScore passwordScore)
        {
            switch (passwordScore)
            {
                case PasswordScore.Blank:
                    return 0.1;

                case PasswordScore.VeryWeak:
                    return 0.3;

                case PasswordScore.Weak:
                    return 0.44;

                case PasswordScore.Medium:
                    return 0.61;

                case PasswordScore.Strong:
                    return 0.8;

                case PasswordScore.VeryStrong:
                    return 1;

                default:
                    throw new System.Exception();
            }
        }
    }
}