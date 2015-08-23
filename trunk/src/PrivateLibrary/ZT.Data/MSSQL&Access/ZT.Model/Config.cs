using System.Threading;
using System.Xml;
using System;
using System.Collections;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;


namespace ZT.Model.MSSQL_ACCESS
{
    /// <summary>
    /// ZTECMSystemConfig 的摘要说明。
    /// </summary>
    public class Config : IConfigurationSectionHandler
    {
        private const string DATAACCESS_CONNECTIONSTRING_DEFAULT = "Provider=SQLOLEDB;server=127.0.0.1; User ID=sa;Password=;database=CMS2.2;Connection Reset=FALSE";	// 缺省数据库连接串
        private const string DATAACCESS_CONNECTIONSTRING = "CMSConfig.DataAccess.ConnectionString";	// WebConfig中数据库连接字符串的节点名

        private static string _ConnectionString;	// 数据库连接字符串
        private static string _AppRoot;				// 应用程序根目录


        /// <summary>
        /// 按照数据库类型返回差异值  暂时无用
        /// </summary>
        /// <param name="intparam">输入值</param>
        /// <returns>差异输出值</returns>
        public static int JudgeDatabase(int intparam)
        {
            if (System.Configuration.ConfigurationSettings.AppSettings != null)
            {
                string strDBType = System.Configuration.ConfigurationSettings.AppSettings["DBType"];
                if (strDBType.Trim() == "0")
                {
                    return intparam + 1;
                }
                else if (strDBType.Trim() == "1")
                {
                    return intparam;
                }
                else
                {
                    throw new Exception("配置文件中数据库该类型配置不符合");
                }
            }
            else
            {
                throw new Exception("配置文件中没有配置数据库类型");
            }
        }

        /// <summary>
        /// 在调用ASP.NET应用程序之前从Web.Config中获取系统配置参数
        /// </summary>
        /// <param name="parent">
        ///  对应父配置节中的配置设置。 
        /// </param>
        /// <param name="configContext">
        /// 在从 ASP.NET 配置系统中调用 Create 时为 HttpConfigurationContext。否则，该参数是保留参数，并且为空引用（Visual Basic 中为 Nothing）。 
        /// </param>
        /// <param name="section">
        /// 一个 XmlNode，它包含配置文件中的配置信息。提供对配置节 XML 内容的直接访问。 
        /// </param>
        /// <returns>	配置对象。</returns>
        public Object Create(Object parent, object configContext, XmlNode section)
        {

            NameValueCollection settings;

            try
            {
                NameValueSectionHandler baseHandler = new NameValueSectionHandler();
                settings = (NameValueCollection)baseHandler.Create(parent, configContext, section);
            }
            catch
            {
                settings = null;
            }

            if (settings == null)
            {
                _ConnectionString = DATAACCESS_CONNECTIONSTRING_DEFAULT;
            }
            else
            {
                _ConnectionString = ReadSetting(settings, DATAACCESS_CONNECTIONSTRING, DATAACCESS_CONNECTIONSTRING_DEFAULT);

            }

            return settings;
        }

        /// <summary>
        /// 根据获得的Key，从NameValueCollection中读取字符串值.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static string ReadSetting(NameValueCollection settings, string key, string defaultValue)
        {
            try
            {
                Object setting = settings[key];

                return (setting == null) ? defaultValue : (string)setting;
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 这个函数在Application_OnStart时被调用.
        /// </summary>
        /// <param name="myAppPath">应用程序路径</param>
        public static void OnApplicationStart(string AppPath)
        {
            _AppRoot = AppPath;

            //GetConfig方法会引发配置节处理程序的Create方法
            System.Configuration.ConfigurationSettings.GetConfig("CMSConfig");

        }

        /// <summary>
        /// 用于获取MMS的数据库联接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return _ConnectionString;
            }
            set
            {
                _ConnectionString = value;
            }
        }


        /// <value>
        /// 获取应用程序路径
        /// </value>
        public static string AppRoot
        {
            get
            {
                return _AppRoot;
            }
        }
    }
}
