using System;
using System.Collections.Generic;

namespace TestApp.Common
{
    public static class DateTimeHelper
    {
        public const string FORMAT_YEAR = "yyyy";
        public const string FORMAT_MONTH = "yyyyMM";
        public const string FORMAT_DAY = "yyyyMMdd";
        public const string FORMAT_HOUR = "yyyyMMddHH";
        public const string FORMAT_MINUTE = "yyyyMMddHHmm";
        public const string FORMAT_SECOND = "yyyyMMddHHmmss";

        /// <summary>
        /// 时间字符串转化为时间
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime ConvertStrToDatetime(string s)
        {
            switch (s.Length)
            {
                case 4:
                    return ConvertStrToDatetime(s, FORMAT_YEAR);

                case 6:
                    return ConvertStrToDatetime(s, FORMAT_MONTH);

                case 8:
                    return ConvertStrToDatetime(s, FORMAT_DAY);

                case 10:
                    return ConvertStrToDatetime(s, FORMAT_HOUR);

                case 12:
                    return ConvertStrToDatetime(s, FORMAT_MINUTE);

                case 14:
                    return ConvertStrToDatetime(s, FORMAT_SECOND);

                default:
                    return ConvertStrToDatetime("2000", FORMAT_YEAR);
            }
        }

        /// <summary>
        /// 时间字符串转化为时间,时间字符串格式
        /// </summary>
        /// <param name="s"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime ConvertStrToDatetime(string s, string format)
        {
            return DateTime.ParseExact(s, format, System.Globalization.CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 时间字符串转化为时间戳,时间字符串格式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long ConvertStrToDatetimeLong(string s)
        {
            return ConvertDateTimeToLong(ConvertStrToDatetime(s));
        }

        /// <summary>
        ///  DateTime --> long 13位 时间戳
        ///
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>13位 时间戳</returns>
        public static long ConvertDateTimeToLong(DateTime dt)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long timeStamp = (dt.Ticks - dtStart.Ticks) / 10000;
            return timeStamp;
        }

        /// <summary>
        /// 时间戳加天数，可为负
        /// </summary>
        /// <param name="time"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static long AddDays(this long time, int i)
        {
            time += 86400000 * i;
            return time;
        }

        /// <summary>
        /// 初始化时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="dateType"></param>
        /// <returns></returns>
        public static DateTime InitDateTime(DateTime dateTime, EnumDateType dateType)
        {
            var format = GetDateFormatByDateType(dateType);
            return ConvertStrToDatetime(dateTime.ToString(format), format);
        }

        public static EnumDateType GetDateType(string dateType)
        {
            dateType = dateType.ToLower().Trim();
            switch (dateType)
            {
                case "y":
                case "year":
                    return EnumDateType.Year;

                case "M":
                case "month":
                    return EnumDateType.Month;

                case "d":
                case "day":
                    return EnumDateType.Day;

                case "H":
                case "hour":
                    return EnumDateType.Hour;

                case "m":
                case "minute":
                    return EnumDateType.Minute;

                case "s":
                case "second":
                    return EnumDateType.Second;

                default:
                    return EnumDateType.Minute;
            }
        }

        /// <summary>
        /// 获取时间编码
        /// </summary>
        /// <param name="dateType"></param>
        /// <returns></returns>
        public static string GetDateFormatByDateType(EnumDateType dateType)
        {
            switch (dateType)
            {
                case EnumDateType.Year:
                    return FORMAT_YEAR;

                case EnumDateType.Month:
                    return FORMAT_MONTH;

                case EnumDateType.Day:
                    return FORMAT_DAY;

                case EnumDateType.Hour:
                    return FORMAT_HOUR;

                case EnumDateType.Minute:
                    return FORMAT_MINUTE;

                case EnumDateType.Second:
                    return FORMAT_SECOND;

                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// 根据类型获取日期
        /// </summary>
        /// <param name="dateType"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeByType(EnumDateType dateType, int number)
        {
            switch (dateType)
            {
                case EnumDateType.Year:
                    return DateTime.Now.AddYears(-number);

                case EnumDateType.Month:
                    return DateTime.Now.AddMonths(-number);

                case EnumDateType.Day:
                    return DateTime.Now.AddDays(-number);

                case EnumDateType.Hour:
                    return DateTime.Now.AddHours(-number);

                case EnumDateType.Minute:
                    return DateTime.Now.AddMinutes(-number);

                case EnumDateType.Second:
                    return DateTime.Now.AddSeconds(-number);

                default:
                    return DateTime.Now;
            }
        }

        /// <summary>
        /// DateTime --> long
        /// 时间戳 通用类型
        /// </summary>
        /// <returns></returns>
        public static long GetCurTime()
        {
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return unixTimestamp;
        }

        /// <summary>
        /// 获取当前时间时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetDatetimeNow()
        {
            return ConvertDateTimeToLong(DateTime.Now);
        }

        /// <summary>
        ///  long --> DateTime
        ///  13位 时间戳
        /// </summary>
        /// <param name="d">13位 时间戳</param>
        /// <returns></returns>
        public static DateTime ConvertLongToDateTime(long d)
        {
            var time = DateTime.MinValue;
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            time = startTime.AddMilliseconds(d);
            return time;
        }

        /// <summary>
        /// 时间转换根据间隔
        /// </summary>
        /// <param name="dateTimeStr"></param>
        /// <param name="spanMinute"></param>
        /// <returns></returns>
        public static DateTime ConvertToDatatime(string dateTimeStr, int spanMinute)
        {
            var dateTime = ConvertStrToDatetime(dateTimeStr);
            return ConvertToDatatime(dateTime, spanMinute);
        }

        /// <summary>
        /// 时间转换根据间隔
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="spanMinute"></param>
        /// <returns></returns>
        public static DateTime ConvertToDatatime(DateTime dateTime, int spanMinute)
        {
            var result = DateTime.Now;
            if (spanMinute < 60)
            {
                //分级
                var minute = dateTime.Minute / spanMinute * spanMinute;
                result = ConvertStrToDatetime(dateTime.ToString(FORMAT_HOUR));
            }
            else if (spanMinute >= 60 && spanMinute < 60 * 24)
            {
                //时级
                var hourSpan = spanMinute / 60;
                var hour = dateTime.Day / hourSpan * hourSpan;
                result = ConvertStrToDatetime(dateTime.ToString(FORMAT_DAY));
            }
            else if (spanMinute == 60 * 24)
            {
                //日
                result = ConvertStrToDatetime(dateTime.ToString(FORMAT_DAY));
            }
            else if (spanMinute == 60 * 24 * 30)
            {
                result = ConvertStrToDatetime(dateTime.ToString(FORMAT_MONTH));
            }
            else if (spanMinute == 60 * 24 * 365)
            {
                result = ConvertStrToDatetime(dateTime.ToString(FORMAT_YEAR));
            }
            return result;
        }

        /// <summary>
        /// 检查时间间隔是否有效
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static bool CheckUseful(int interval)
        {
            if (interval > 0 && interval < 1440)
            {
                return 1440 % interval == 0;
            }

            return false;
        }
    }

    public enum EnumDateType
    {
        /// <summary>
        /// 年
        /// </summary>
        Year,

        /// <summary>
        /// 月
        /// </summary>
        Month,

        /// <summary>
        /// 日
        /// </summary>
        Day,

        /// <summary>
        /// 时
        /// </summary>
        Hour,

        /// <summary>
        /// 分
        /// </summary>
        Minute,

        /// <summary>
        /// 秒
        /// </summary>
        Second,
    }
}