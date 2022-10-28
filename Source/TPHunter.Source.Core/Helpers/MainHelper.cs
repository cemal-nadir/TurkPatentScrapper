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
            if (string.IsNullOrEmpty(classesText) || classesText == "-")
                return null;
            classesText = classesText.Replace(" ", "");
            string[] classesArrayCache = classesText.Split('/');
            int[] classesArray = Array.ConvertAll(classesArrayCache, int.Parse);
            return classesArray;
        }
    }
}
