using System;
using System.Globalization;
using System.Web;
using System.IO;
using NH.Dal.Collection;
using UtilityCollection.Check;
using UtilityCollection.Encrypt;
using UtilityCollection.Handler;
using ZT.Api.User.Authorize;

namespace ZT.Api.User.Identity
{
    public class IdentityHelper
    {
        #region 登录账户
        /// <summary>
        ///目前已登录账户
        /// </summary>
        public static Models.User User
        {
            get
            {
                Models.User user;
                if (HttpContext.Current.Session["CurrentUser"] != null)
                {
                    user = HttpContext.Current.Session["CurrentUser"] as Models.User;
                }
                else
                {
                    //读取cookie自动登录
                    LoginUserByCookie();
                    user = HttpContext.Current.Session["CurrentUser"] as Models.User;
                }
                return user;
            }
            set
            {
                HttpContext.Current.Session["CurrentUser"] = value;
            }
        }
        #endregion

        #region 判断访问权限
        /// <summary>
        /// 判断访问权限
        /// </summary>
        /// <param name="controller">controller</param>
        /// <param name="action">action</param>
        /// <returns></returns>
        public static bool IsAllow(string controller, string action)
        {
            var objUserAuthorize = new UserAuthorizeAttribute(User);
            return objUserAuthorize.IsAllowed(User, controller, action);
        }
        #endregion

        #region 用户登录
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginId">登录ID</param>
        /// <param name="password">登录密码</param>
        public static void LoginUser(string loginId, string password)
        {
            var userTemp = new Models.User { LoginResult = false };
            var webUserDal = new WebUserDal();
            var webUser = webUserDal.GetUserByLoginIdAndPassword(loginId, password);
            if (webUser != null)
            {
                System.Web.Security.FormsAuthentication.SetAuthCookie("Login", false);

                userTemp.Id = webUser.ID;
                userTemp.LoginId = webUser.LoginID;
                userTemp.Password = webUser.Password;
                userTemp.LoginResult = true;

                User = userTemp;

                //记录登录Cookie
                var objCookie = new HttpCookie("mInfo");
                objCookie.Values.Add("i", RsaCryption.RsaEncrypt(userTemp.LoginId));
                objCookie.Values.Add("p", RsaCryption.RsaEncrypt(userTemp.Password));
                objCookie.Values.Add("tm", RsaCryption.RsaEncrypt(DateTime.Now.ToString(CultureInfo.InvariantCulture)));
                objCookie.Expires = DateTime.Now.AddMinutes(60);
                objCookie.Path = "/";
                ////设置作用域 常用语跨域
                //if (HttpContext.Current.Request.Url.Host.ToLower() != "localhost")
                //{
                //    objCookie.Domain = ValueConfig.DefaultHost;
                //}
                objCookie.HttpOnly = true;
                objCookie.Secure = false;
                HttpContext.Current.Response.Cookies.Set(objCookie);

                //检测用户主文件夹
                var path = AppDomain.CurrentDomain.BaseDirectory + StaticResource.WebsFolder;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //检测用户文件夹
                path += "\\" + User.LoginId;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            else
            {
                User = new Models.User { LoginResult = false };
            }
        }
        #endregion

        #region 读取cookie登录
        /// <summary>
        ///读取cookie登录
        /// </summary>
        public static void LoginUserByCookie()
        {
            var uCookie = HttpContext.Current.Request.Cookies["mInfo"];
            if (uCookie != null)
            {

                var strUid = (uCookie.Values.Get("i") ?? "").Trim().Replace("'", "");
                var strUpsd = (uCookie.Values.Get("p") ?? "").Trim().Replace("'", "");
                var strUTime = (uCookie.Values.Get("tm") ?? "").Trim().Replace("'", "");

                strUid = RsaCryption.RsaDecrypt(strUid);
                strUpsd = RsaCryption.RsaDecrypt(strUpsd);
                strUTime = RsaCryption.RsaDecrypt(strUTime);

                if (!CheckParams.IsDateTime(strUTime))
                {
                    User.LoginResult = false;
                    return;
                }

                //防止伪造cookies
                var ts = DateTime.Now.Subtract(Convert.ToDateTime(strUTime));
                //36000000000   ticks 60分钟
                if (ts.Ticks > 36000000000)
                {
                    User.LoginResult = false;
                    return;
                }
                LoginUser(strUid, strUpsd);
            }
        }
        #endregion

        #region 注销登录
        /// <summary>
        /// 注销登录
        /// </summary>
        public static void LoginOut()
        {
            User = null;
            var objCookie = new HttpCookie("mInfo");

            var ts = new TimeSpan(-1, 0, 0, 0);
            objCookie.Expires = DateTime.Now.Add(ts);
            HttpContext.Current.Response.AppendCookie(objCookie);
            JsHelper.ParentRedirect(StaticResource.UserLoginUrl);
        }
        #endregion
    }
}