using System;
using System.Collections.Generic;
using System.Text;

using System.Xml.XPath;
using System.Xml.Xsl;

namespace UtilityCollection.Handler
{
    public class ValidationXslt
    { 
        /// <summary>
        /// 效验插件的Xslt文档 错误的捕捉和定位
        /// </summary>
        /// <param name="strXslt">Xslt文档</param>
        /// <param name="strPluginName">栏目名称</param>
        /// <returns>错误信息</returns>
        public static string ValidationXsltFormat(string strXslt, string strPluginName)
        {
            string strReturn = string.Empty;
            try
            {
                XPathDocument objReader = new XPathDocument(new System.IO.StringReader(strXslt));
            }
            catch (Exception ex)
            {
                strReturn = "出错了！\\n----------------------------详细信息---------------------------\\n栏目名称：" + strPluginName + "\\n系统错误：" + ex.Message;
            }
            return strReturn;
        }
    }
}
