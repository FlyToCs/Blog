using System.Text.RegularExpressions;

namespace Blog.Framework
{
    public static class TextHelper
    {
        public static string ToSlug(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            value = value.Trim().ToLower();

            value = value
                .Replace("۰", "0")
                .Replace("۱", "1")
                .Replace("۲", "2")
                .Replace("۳", "3")
                .Replace("۴", "4")
                .Replace("۵", "5")
                .Replace("۶", "6")
                .Replace("۷", "7")
                .Replace("۸", "8")
                .Replace("۹", "9");

            value = Regex.Replace(value, @"[^a-z0-9\u0600-\u06FF\s-]", "");
            value = Regex.Replace(value, @"\s+", " ").Trim();
            value = value.Replace(" ", "-");
            value = Regex.Replace(value, "-{2,}", "-");

            return value;
        }
    }
}