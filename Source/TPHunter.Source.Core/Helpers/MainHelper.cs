using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static int[] ParseClasses(this string classesText)
        {
            classesText = classesText.Replace(" ", "");
            if (string.IsNullOrEmpty(classesText) || classesText == "-")
                return null;

            int[] classesArray = Array.ConvertAll(classesText.Split('/').Where(x => !string.IsNullOrEmpty(x)).ToArray(), int.Parse);
            return classesArray;
        }
        public static int? ParseClass(this string classText)
        {
            classText = classText.Replace(" ", "");
            if (string.IsNullOrEmpty(classText) || classText == "-")
                return null;
            return Convert.ToInt32(classText);
        }
    }
}
