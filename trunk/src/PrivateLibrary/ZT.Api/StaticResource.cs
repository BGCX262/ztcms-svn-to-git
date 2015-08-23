namespace ZT.Api
{
    public class StaticResource
    {
        /// <summary>
        /// 默认未通过验证时的跳转路径
        /// </summary>
        public static string WebsFolder { get; set; }

        /// <summary>
        /// 默认未通过验证时的跳转路径
        /// </summary>
        public static string DefaultRedRedirect { get; set; }

        /// <summary>
        /// 默认允许访问的Controller
        /// </summary>
        public static string DefaultLoginUserAccessController { get; set; }

        /// <summary>
        /// 默认允许访问的Action
        /// </summary>
        public static string DefaultLoginUserAccessAction { get; set; }

        /// <summary>
        /// 用户登录路径
        /// </summary>
        public static string UserLoginUrl { get; set; }
    }
}
