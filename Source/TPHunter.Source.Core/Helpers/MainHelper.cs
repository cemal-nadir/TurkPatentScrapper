using System;
using System.Globalization;
using System.Linq;

namespace TPHunter.Source.Core.Helpers
{
    public static class MainHelper
    {
        public static DateTime? CustomConvertToDatetime(this string dateText)
        {
            if (string.IsNullOrEmpty(dateText) || dateText == "-")
                return null;

            dateText = dateText.Replace(".", "/");
            DateTime? date = Convert.ToDateTime(dateText);
            return date;
        }

        public static string NormalizeText(this string text)
        {

            if (text.Replace(" ","").Length < 3)
                text = text.Replace("-", "");
            text = text.Replace("*", "");
            text = text.ToLower(CultureInfo.CreateSpecificCulture("tr"));
            return text.Replace(" ","").Length == 0 ? null : text;
        }
        public static int[] ParseClasses(this string classesText)
        {
            classesText = classesText.Replace(" ", "");
            if (string.IsNullOrEmpty(classesText) || classesText == "-")
                return null;

            var classesArray = Array.ConvertAll(classesText.Split('/').Where(x => !string.IsNullOrEmpty(x)).ToArray(), int.Parse);
            return classesArray;
        }
        public static int? ParseClass(this string classText)
        {
            if (classText == null) return null;
            classText = classText.Replace(" ", "");
            if (string.IsNullOrEmpty(classText) || classText == "-")
                return null;
            return Convert.ToInt32(classText);
        }

        public static string FixImageBase64(this string base64Text)
        {
            return base64Text.Replace('-', '+').Replace("data:image/jpeg;base64,", "").Replace('_', '/');
        }
    }
}
