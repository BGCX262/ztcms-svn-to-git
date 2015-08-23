using System;
using System.Web;
using System.Web.Mvc;
using UtilityCollection.Xml;
using ZT.Api.User.Identity;

namespace ZT.Api.User.Authorize
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 默认配置文件路径
        /// </summary>
        private const string Path = "Setting\\Global.config";

        #region 初始化验证
        /// <summary>
        /// 初始化验证 使用内置路径
        /// </summary>
        public UserAuthorizeAttribute()
        {
            var xmlProcess = new XmlProcess(Path);
            SetAttribute(ref xmlProcess);
        }

        /// <summary>
        /// 初始化验证
        /// </summary>
        /// <param name="settingFilePath">全局设置文件路径，相对于程序集</param>
        public UserAuthorizeAttribute(string settingFilePath)
        {
            var xmlProcess = new XmlProcess(settingFilePath);
            SetAttribute(ref xmlProcess);
        }

        /// <summary>
        /// 初始化验证 使用内置路径
        /// </summary>
        public UserAuthorizeAttribute(Models.User user)
        {
            User = user;
            var xmlProcess = new XmlProcess(Path);
            SetAttribute(ref xmlProcess);
        }

        /// <summary>
        /// 初始化验证 使用内置路径
        /// </summary>
        public UserAuthorizeAttribute(Models.User user, string settingFilePath)
        {
            User = user;
            var xmlProcess = new XmlProcess(settingFilePath);
            SetAttribute(ref xmlProcess);
        }
        #endregion

        /// <summary>
        /// 根据XMl设置属性
        /// </summary>
        /// <param name="xmlProcess">XML源</param>
        private static void SetAttribute(ref XmlProcess xmlProcess)
        {
            if (xmlProcess.XmlFileExist)
            {
                StaticResource.WebsFolder = xmlProcess.ReadAttribute("/Global/User/WebsFolder", "Url").ToLower().Trim();
                StaticResource.UserLoginUrl = xmlProcess.ReadAttribute("/Global/User/UserLogin", "Url").ToLower().Trim();
                StaticResource.DefaultRedRedirect = xmlProcess.ReadAttribute("/Global/User/DefaultRedRedirect", "Url").ToLower().Trim();
                StaticResource.DefaultLoginUserAccessController = xmlProcess.ReadAttribute("/Global/User/DefaultLoginUserAccess", "Controller").ToLower().Trim();
                StaticResource.DefaultLoginUserAccessAction = xmlProcess.ReadAttribute("/Global/User/DefaultLoginUserAccess", "Action").ToLower().Trim();
            }
            else
            {
                throw new Exception(string.Format("文件{0}不存在", xmlProcess.XmlPath));
            }
        }

        #region 属性

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public Models.User User { get; private set; }

        #endregion

        /// <summary>
        /// 重载验证
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = IdentityHelper.User;

            //用户未登陆
            if (user == null || !user.LoginResult)
            {
                HttpContext.Current.Response.Redirect(StaticResource.UserLoginUrl, true);
                filterContext.Result = new EmptyResult();
            }

            var controller = filterContext.RouteData.Values["controller"].ToString().ToLower();
            var action = filterContext.RouteData.Values["action"].ToString().ToLower();

            var isHome = (controller == StaticResource.DefaultLoginUserAccessController && action == StaticResource.DefaultLoginUserAccessAction);
            if (!isHome)
            {
                var isAllowed = IsAllowed(user, controller, action);

                if (isAllowed) return;
                HttpContext.Current.Response.Redirect(StaticResource.DefaultRedRedirect, true);
                filterContext.Result = new EmptyResult();
            }
        }

        /// <summary>
        /// 判断是否允许访问
        /// </summary>
        /// <span name="user"> </span>用户
        /// <span name="controller"> </span>控制器
        /// <span name="action"> </span>action
        /// <returns>是否允许访问</returns>
        public bool IsAllowed(Models.User user, string controller, string action)
        {
            var findResult = false;
            //加载角色
            if (user != null)
            {
                findResult = FindAllowAction(user.ActionList, controller.ToLower(), action.ToLower());
            }

            return findResult;
        }

        private bool FindAllowAction(string actionCollection, string controllerName, string actionName)
        {
            var result = false;

            if (string.IsNullOrEmpty(actionCollection))
            {
                return false;
            }

            var temp = "|" + actionCollection + "|";
            if (temp.ToLower().Contains("|" + controllerName + "," + actionName + "|"))
            {
                result = true;
            }
            return result;
        }
    }
}