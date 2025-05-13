using System.Text.Json;

namespace MainTest.Framework.Extensions;

public static class StringExtensions
{
    public static string ToPersianNumber(this string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;

        return input.Replace("0", "۰")
                    .Replace("1", "۱")
                    .Replace("2", "۲")
                    .Replace("3", "۳")
                    .Replace("4", "۴")
                    .Replace("5", "۵")
                    .Replace("6", "۶")
                    .Replace("7", "۷")
                    .Replace("8", "۸")
                    .Replace("9", "۹");
    }

    public static string ToEnglishNumber(this string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;

        return input.Replace("۰", "0")
                    .Replace("۱", "1")
                    .Replace("۲", "2")
                    .Replace("۳", "3")
                    .Replace("۴", "4")
                    .Replace("۵", "5")
                    .Replace("۶", "6")
                    .Replace("۷", "7")
                    .Replace("۸", "8")
                    .Replace("۹", "9");
    }

    public static bool IsValidEmail(this string s)
    {
        var trimmedEmail = s.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false;
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(s);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsValidDateTime(this string s)
    {
        try
        {
            Convert.ToDateTime(s);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static string GetJsonProperty(this string json, params string[] propertyPath)
    {
        using (JsonDocument document = JsonDocument.Parse(json))
        {
            JsonElement element = document.RootElement;
            foreach (var property in propertyPath)
            {
                if (element.TryGetProperty(property, out JsonElement childElement))
                    element = childElement;
                else
                    return null;
            }
            return element.ToString();
        }
    }
}
