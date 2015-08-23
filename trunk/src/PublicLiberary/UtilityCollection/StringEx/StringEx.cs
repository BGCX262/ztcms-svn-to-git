using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityCollection.StringEx
{
    public static class StringEx
    {
        public static string ToUpperFirstChar(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return source;
            }
            else
            {
                if (string.IsNullOrEmpty(source.Trim()))
                {
                    return source;
                }
                else
                {
                    var firstchar = source.GetByIndex(0).ToUpper();
                    return source.ReplaceByIndex(0, firstchar);
                }
            }
        }

        /// <summary>
        /// 替换指定的某位字符
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="index">指定位置</param>
        /// <param name="newstring">要替换成的字符串</param>
        /// <returns></returns>
        public static string ReplaceByIndex(this string source, int index, string newstring)
        {
            if (index > source.Length - 1)
            {
                return source;
            }
            else
            {
                if (index == 0)
                {
                    string strTemp = source.Remove(0, 1);
                    return newstring + strTemp;
                }
                else if (index == source.Length - 1)
                {
                    string strTemp = source.Remove(index);
                    return strTemp + newstring;
                }
                else
                {
                    string strTempFirst = source.Remove(index);
                    string strTempLast = source.Remove(0, index + 1);
                    return strTempFirst + newstring + strTempLast;
                }
            }
        }

        /// <summary>
        /// 获取字符串中指定位
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="index">指定位置</param>
        /// <returns></returns>
        public static string GetByIndex(this string source, int index)
        {
            if (index > source.Length - 1)
            {
                return "";
            }
            else
            {
                if (index == 0)
                {
                    string strTemp = source.Remove(1);
                    return strTemp;
                }
                else if (index == source.Length - 1)
                {
                    string strTemp = source.Remove(0, index);
                    return strTemp;
                }
                else
                {
                    string strTemp = source.Remove(index + 1);
                    strTemp = strTemp.Remove(0, index);
                    return strTemp;
                }
            }
        }

        public static string FilterSql(this string source)
        {
            return source.Replace("'", "");
        }
    }
}
