using System;
using System.Web;
using System.Text.RegularExpressions;

namespace UtilityCollection.Check
{
    public class CheckParams
    {
        private static bool RequestIsNull(string param)
        {
            return HttpContext.Current.Request[param] == null;
        }

        /// <summary>
        /// 是否为数字
        /// </summary>
        /// <param name="numberString"></param>
        /// <returns></returns>
        public static bool IsNumber(string numberString)
        {
            if (string.IsNullOrEmpty(numberString))
            {
                return false;
            }
            double i;
            var result = double.TryParse(numberString, out i);
            return result;
        }

        /// <summary>
        /// 是否为日期
        /// </summary>
        /// <param name="datetimeString"></param>
        /// <returns></returns>
        public static bool IsDateTime(string datetimeString)
        {
            if (string.IsNullOrEmpty(datetimeString))
            {
                return false;
            }
            DateTime i;
            var result = DateTime.TryParse(datetimeString, out i);
            return result;
        }

        /// <summary>
        /// 检测是否包含数字
        /// </summary>
        /// <param name="param">检测字符串</param>
        /// <returns>输出true/false</returns>
        public static bool ContainNum(string param)
        {
            const string regEx = @"/\d/ ";
            return Regex.IsMatch(param, regEx);
        }

        /// <summary>
        /// 检测是否为中文
        /// </summary>
        /// <param name="param">检测字符串</param>
        /// <returns>输出true/false</returns>
        public static bool IsZhcn(string param)
        {
            var reg = new Regex(@"[\u4e00-\u9fa5]");
            return reg.IsMatch(param);
        }

        /// <summary>
        /// 检测是否为中文和字母
        /// </summary>
        /// <param name="param">检测字符串</param>
        /// <returns>输出true/false</returns>
        public static bool IsZhcnOrlitterOrNumber(string param)
        {
            var reg = new Regex(@"^[a-zA-Z0-9_\u4e00-\u9fa5]+$");
            return reg.IsMatch(param);
        }

        /// <summary>
        /// 检测Email格式
        /// </summary>
        /// <param name="param">检测字符串</param>
        /// <returns>输出true/false</returns>
        public static bool IsEmail(string param)
        {
            var reg = new Regex(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
            return reg.IsMatch(param);
        }

        /// <summary>
        /// 检测国内电话和手机
        /// </summary>
        /// <param name="param">检测字符串</param>
        /// <returns>输出true/false</returns>
        public static bool IsPhoneOrMobile(string param)
        {
            var regPhone = new Regex(@"^0?\\d{11}$");
            var regMobile = new Regex(@"^(\(\d{3,4}\)|\d{3,4}-)?\d{7,8}(-\d{1,4})?$");
            //Regex regPhone = new Regex(@"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)");
            return regPhone.IsMatch(param) || regMobile.IsMatch(param);
        }
        /// <summary>
        /// 单独检测手机
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool IsMobile(string param)
        {
            var regMobile = new Regex(@"^1(3[4-9]|4[7]|5[01256789]|8[0-9])\d{8}$");
            return regMobile.IsMatch(param);
        }

        /// <summary>
        /// 检测密码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool IsPwd(string param)
        {
            var regPwd = new Regex(@"^[a-zA-Z]\w{6,18}$");
            return regPwd.IsMatch(param);
        }

        /// <summary>
        /// 验证登录用户名
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool IsLoginName(string param)
        {
            var regName = new Regex(@"^\w+$");
            return regName.IsMatch(param);
        }

        /// <summary>
        /// 检测QQ号码
        /// </summary>
        /// <param name="param">检测字符串</param>
        /// <returns>输出true/false</returns>
        public static bool IsQq(string param)
        {
            var regQq = new Regex(@"[1-9][0-9]{4,}");
            //Regex regPhone = new Regex(@"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)");
            return regQq.IsMatch(param);
        }

        /// <summary>
        /// 限定字符个数
        /// </summary>
        /// <param name="param">检测的字符串</param>
        /// <param name="limitNum">最小字符数</param>
        /// <returns>输出true/false</returns>
        public static bool LimitNum(string param, int limitNum)
        {
            int stringLength = param.Length;
            return stringLength >= limitNum;
        }

        /// <summary>
        ///截取字符
        /// </summary>
        /// <param name="param">源字符串</param>
        /// <param name="limitNum">限制 的字符格式</param>
        /// <returns>输出true/false</returns>
        public static string LimitString(string param, int limitNum)
        {
            int stringLength = param.Length;
            if (stringLength <= limitNum)
            {
                return param;
            }
            param = param.Remove(limitNum) + "...";
            return param;
        }

        /// <summary>
        /// 判断包含是否包含特定字符
        /// </summary>
        /// <param name="param">需要检测的字符串</param>
        /// <param name="containParam">包含的字符串</param>
        /// <returns>返回true/flase</returns>
        public static bool ContainString(string param, string containParam)
        {
            int i = param.IndexOf(containParam, StringComparison.Ordinal);
            return i != -1;
        }

        ///   <summary> 
        ///   屏蔽字符串中的特殊字符 
        ///   by   minjiang   07-08-11 
        ///   解决大小写的问题 
        ///   </summary> 
        public static string SafeRequest2(string str)
        {
            //定义要返回的字符串 

            //定义特殊字符串 
            const string sqlKill = "'|%|;|=|--|<|>|/";
            char[] separator = { '|' };
            string[] sql = sqlKill.Split(separator);
            foreach (string t in sql)
            {
                //如果有特殊字符则将它替换成为空 
                if (str.IndexOf(t.ToLower(), StringComparison.Ordinal) > -1)
                {
                    str = str.Replace(t, " ");
                }
            }
            string sReturn = str;
            return sReturn;
        }
        /// <summary>
        /// 判断包含是否包含特定字符
        /// </summary>
        /// <param name="param">需要检测的字符串</param>
        /// <param name="containParam">包含的字符数组</param>
        /// <returns>返回true/flase</returns>
        public static bool ContainString(string param, char[] containParam)
        {
            if (containParam.Length == 0)
            {
                return true;
            }
            var result = false;
            foreach (char t in containParam)
            {
                var index = param.IndexOf(t);
                if (index == -1)
                {
                    result = false;
                }
                else
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
