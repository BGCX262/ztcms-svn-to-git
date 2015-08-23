namespace ZT.Api.User.Models
{
    public class User
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int Id { get; set; }
       
        /// <summary>
        /// 登录ID
        /// </summary>
        public string LoginId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码（密文形式）
        /// </summary>
        public string Password { get; set; }

       /// <summary>
        /// 所属角色值列表
       /// </summary>
        public string RoleListValue { get; set; }

        /// <summary>
        /// 所属角色名称列表
        /// </summary>
        public string RoleListName { get; set; }

        /// <summary>
        /// 允许的Action列表 形式如 action1,cortallor1|action2,controllor2
        /// </summary>
        public string ActionList { get; set; }

        /// <summary>
        /// 登陆结果
        /// </summary>
        public bool LoginResult { get; set; }
    }
}