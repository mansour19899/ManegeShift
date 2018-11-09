using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManegeShift
{
    public static class PersianDate
    {
        public static DateTime ToGeorgianDateTime(this string persianDate)
        {
            int year = Convert.ToInt32(persianDate.Substring(0, 4));
            int month = Convert.ToInt32(persianDate.Substring(5, 2));
            int day = Convert.ToInt32(persianDate.Substring(8, 2));
            DateTime georgianDateTime = new DateTime(year, month, day, new System.Globalization.PersianCalendar());
            return georgianDateTime;
        }

        public static string ToPersianDateString(this DateTime georgianDate)
        {
            System.Globalization.PersianCalendar persianCalendar = new System.Globalization.PersianCalendar();

            string year = persianCalendar.GetYear(georgianDate).ToString();
            string month = persianCalendar.GetMonth(georgianDate).ToString().PadLeft(2, '0');
            string day = persianCalendar.GetDayOfMonth(georgianDate).ToString().PadLeft(2, '0');
            string persianDateString = string.Format("{0}/{1}/{2}", year, month, day);
            return persianDateString;
        }
        public static string AddDaysToShamsiDate(this string persianDate, int days)
        {
            DateTime dt = persianDate.ToGeorgianDateTime();
            dt = dt.AddDays(days);
            return dt.ToPersianDateString();
        }
        public static string ToPersianday(this string persianDate)
        {
            System.Globalization.PersianCalendar persianCalendar = new System.Globalization.PersianCalendar();

            DateTime dt = persianDate.ToGeorgianDateTime();
            var day = dt.DayOfWeek;
            string dayy="";
            switch (day)
            {
                case DayOfWeek.Friday:
                    dayy = "جمعه"; 
                     break;
                case DayOfWeek.Saturday:
                    dayy = "شنبه";
                    break;
                case DayOfWeek.Sunday:
                    dayy = "یک شنبه";
                    break;
                case DayOfWeek.Monday:
                    dayy = "دوشنبه";
                    break;
                case DayOfWeek.Tuesday:
                    dayy = "سه شنبه";
                    break;
                case DayOfWeek.Wednesday:
                    dayy = "چهارشنبه";
                    break;
                case DayOfWeek.Thursday:
                    dayy = "پنج شنبه";
                    break;
            }
            return dayy;
        }

        public static int ToPersianDayOfWeek(this string persianDate)
        {
            System.Globalization.PersianCalendar persianCalendar = new System.Globalization.PersianCalendar();

            DateTime dt = persianDate.ToGeorgianDateTime();
            var day = dt.DayOfWeek;
            int dayy = 0;
            switch (day)
            {
                case DayOfWeek.Friday:
                    dayy = 7;
                    break;
                case DayOfWeek.Saturday:
                    dayy = 1;
                    break;
                case DayOfWeek.Sunday:
                    dayy = 2;
                    break;
                case DayOfWeek.Monday:
                    dayy = 3;
                    break;
                case DayOfWeek.Tuesday:
                    dayy =4;
                    break;
                case DayOfWeek.Wednesday:
                    dayy = 5;
                    break;
                case DayOfWeek.Thursday:
                    dayy = 6;
                    break;
            }
            return dayy;
        }

        public static int DiffDaysShamsi(this string FirstDate, string SecondtDate)
        {
            DateTime dt = FirstDate.ToGeorgianDateTime();
            DateTime dtt = SecondtDate.ToGeorgianDateTime();
            TimeSpan dif = dt - dtt;
            
            return dif.Days;
        }

        public static bool IsKabiseh(this string persianDate)
        {
            var Year = Convert.ToInt16(persianDate.Substring(0, 4));
            if ((Year - 1395) % 4 == 0)
                return true;
            else
            return false;
        }

        public static bool IsKabiseh(this DateTime Date)
        {
            string persianDate = Date.ToPersianDateString();
            var Year = Convert.ToInt16(persianDate.Substring(0, 4));
            if ((Year - 1395) % 4 == 0)
                return true;
            else
                return false;
        }

        public static List<string> ReturnDaysOfMonth(this string persianDate)
        {
            List<string> listDays = new List<string>() {"01","02","03","04","05","06","07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19"
            ,"20","21", "22", "23", "24", "25", "26", "27", "28", "29"};
            var month = persianDate.Substring(5,2);
            if (month.CompareTo("07") < 0)
            {
                listDays.Add("30");
                listDays.Add("31");
            }
            else
            {
                listDays.Add("30");
            }
            if (!persianDate.IsKabiseh() & month.CompareTo("12") == 0)
            {
                listDays.RemoveAt(29);
            }

            return listDays;
        }

    }
}
