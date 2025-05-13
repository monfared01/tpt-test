using System.Text.RegularExpressions;

namespace MainTest.Framework.Utility
{
    public enum ComplexityLevel
    {
        Level1, // Minimum 8 characters
        Level2, // Minimum 8 characters, 1 uppercase letter, 1 digit
        Level3  // Minimum 8 characters, 1 uppercase letter, 1 digit, 1 special character
    }

    public class PasswordValidator
    {
        public static bool ValidatePassword(string password, ComplexityLevel level= ComplexityLevel.Level2)
        {
            if (password.Length < 8)
            {
                return false;
            }

            switch (level)
            {
                case ComplexityLevel.Level1:
                    return true;

                case ComplexityLevel.Level2:
                    return Regex.IsMatch(password, @"[A-Z]") && Regex.IsMatch(password, @"\d");

                case ComplexityLevel.Level3:
                    return Regex.IsMatch(password, @"[A-Z]") && Regex.IsMatch(password, @"\d") && Regex.IsMatch(password, @"[\W_]");

                default:
                    return false;
            }
        }
    }
}
