using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityCollection.ToolsCollection
{
    public class XMLStringHelper
    {
        /// <summary>
        /// 提示错误，错误信息如下： 十六进制值 0x0B 是无效的字符错误产生原因是xml文件中包含低位非打印字符造成的处理方法：在产生xml文件的时候，过滤低位非打印字符
        /// </summary>
        /// <param name="tmp">需要过滤的字符串</param>
        /// <returns></returns>
        public static string ReplaceLowOrderASCIICharacters(string tmp)
        {
            StringBuilder info = new StringBuilder();
            foreach (char cc in tmp)
            {
                int ss = (int)cc;
                if (((ss >= 0) && (ss <= 8)) || ((ss >= 11) && (ss <= 12)) || ((ss >= 14) && (ss <= 32)))
                    info.AppendFormat(" ", ss);//&#x{0:X};
                else info.Append(cc);
            }
            return info.ToString();
        }
    }
}
