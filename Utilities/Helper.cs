using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Helper
    {

        #region Helpers

        public static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }


        public static int GetIntDB(Object obj)
        {
            if (obj == Convert.DBNull || obj == null || obj.ToString().Trim() == "" || IsNumeric(obj) == false)
            {
                return 0;
            }

            return Convert.ToInt32(obj);
        }

        public static bool GetBoolDB(Object obj)
        {
            if (obj == Convert.DBNull || obj == null)
            {
                return false;
            }

            return Convert.ToBoolean(obj);
        }

        public static bool ParseBoolDB(Object obj)
        {
            if (obj == Convert.DBNull || obj == null)
            {
                return false;
            }
            try
            {
                return bool.Parse(obj.ToString());

            }
            catch (Exception)
            {
                return false;
            }
        }

        public static float GetFloatDB(Object obj)
        {
            if (obj == Convert.DBNull || obj == null)
            {
                return 0;
            }

            return Convert.ToSingle(obj);
        }

        public static double GetDoubleDB(Object obj)
        {
            if (obj == Convert.DBNull || obj == null)
            {
                return 0;
            }

            return Convert.ToDouble(obj);
        }

        public static String GetStringDB(Object obj)
        {
            if (obj == Convert.DBNull || obj == null)
            {
                return "";
            }

            return Convert.ToString(obj);
        }


        public static String GetDateDB(Object obj)
        {
            if (obj == Convert.DBNull || obj == null)
            {
                return "";
            }

            return Convert.ToDateTime(obj).ToString("dd.MM.yyyy");
        }


        public static DateTime GetDateTimeDB(Object obj, string format)
        {
            if (obj == Convert.DBNull || obj == null)
            {
                return DateTime.Now;
            }

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
            return DateTime.ParseExact(obj.ToString(), format, culture, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        }

        public static DateTime GetDateTimeDB(Object obj)
        {
            if (obj == Convert.DBNull || obj == null)
            {
                return DateTime.Now;
            }

            return Convert.ToDateTime(obj);
        }

        public static DateTime GetDateTimeDBPlusOneYear(Object obj)
        {
            if (obj == Convert.DBNull || obj == null)
            {
                return DateTime.Now.AddYears(1);
            }

            return Convert.ToDateTime(obj);
        }

        public static DateTime GetDateTimeDBWithNull(Object obj)
        {
            if (obj == Convert.DBNull || obj == null)
            {
                return DateTime.Now.AddDays(30);
            }

            return Convert.ToDateTime(obj);
        }

        public static Guid GetGuidDB(Object obj)
        {
            if (obj == Convert.DBNull || obj == null)
            {
                return Guid.Empty;
            }

            return Guid.Parse(obj.ToString());
        }



        public static DateTime? GetDateTimeDBNull(Object obj)
        {
            if (obj == Convert.DBNull || obj == null)
            {
                return null;
            }

            return Convert.ToDateTime(obj);
        }
        #endregion


    }
}
