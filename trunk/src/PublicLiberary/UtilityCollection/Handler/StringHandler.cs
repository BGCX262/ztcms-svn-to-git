using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace UtilityCollection.Handler
{
   public class StringHandler
    {
        public static string FilterSql(string strValue)
        {
            string strResult = (string.IsNullOrEmpty(strValue) ? "" : strValue.Replace("'", ""));
            return strResult;
        }

        #region 截取字符串
        public static string CutString(string inputString, int number)
        {
            if (inputString.Length != 0)
            {
                return (inputString.Length > number ? (inputString.Remove(number) + "...") : inputString);
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion
        #region 截取字符串的长度（分字节）
        public static string StringUtility(object content, int length)
        {
            string temp = content.ToString().Replace("<br/>", "").Replace("<br>", "");//先替换换行标签，保证不出现换行

            //参数说明：要处理的字符串，符合条件的表达式[汉字]，替换的字符[内容随意写但是要两个字符，因为一个中文对应两个字符，不区分大小写]
            if (Regex.Replace(temp, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= length)
            {
                return temp;
            }
            for (int i = temp.Length; i >= 0; i--)
            {
                temp = temp.Substring(0, i);
                if (Regex.Replace(temp, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length <= length - 3)
                {
                    return temp + "...";
                }
            }
            return "";
        }
        #endregion

        /// <summary>
        /// //替换HTML标记 
        /// </summary>
        /// <param name="Htmlstring">要过滤的字符</param>
        /// <returns></returns>
        public static string ClearHTML(string Htmlstring)
        {
            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<img[^>]*>;", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring.Replace("'", "");
            Htmlstring = HttpUtility.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;

        }

        /// <summary>
        /// 过滤字符中的sql关键字
        /// </summary>
        /// <param name="strString"></param>
        /// <returns></returns>
        public static string ClearSqlKeyWord(string strString)
        {
            return strString;
        }

        /// <summary>
        /// 过滤字符中的sql和html关键字
        /// </summary>
        /// <param name="strString"></param>
        /// <returns></returns>
        public static string ClearSqlAndHtmlKeyWord(string strString)
        {
            return strString;
        }
    }
}
