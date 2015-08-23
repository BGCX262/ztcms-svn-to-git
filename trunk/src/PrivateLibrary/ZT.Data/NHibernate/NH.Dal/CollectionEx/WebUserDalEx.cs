using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHEntity.Entities;
using NHibernate;

namespace NH.Dal.Collection
{
    public partial class WebUserDal
    {
        /// <summary>
        /// 根据登录名和密码获取用户
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public WebUser GetUserByLoginIdAndPassword(string loginId, string password)
        {
            loginId = loginId.Replace("'", "");
            password = password.Replace("'", "");
            //hql
            var hqlQuery = Session.CreateQuery("from WebUser u where u.LoginID = :LoginID and u.Password=:Password ")
                .SetString("LoginID", loginId)
                .SetString("Password", password);

            var list = hqlQuery.List<WebUser>();

            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// 返回分页列表页
        /// </summary>
        /// <param name="currentPage">当前页码（注意从零开始）</param>
        /// <param name="pagesize">每页条目数</param>
        /// <returns></returns>
        public IList<WebUser> GetUsers(int currentPage, int pagesize)
        {
            //hql
            var hqlQuery = Session.CreateQuery("from WebUser")
                 .SetFirstResult(currentPage * pagesize).SetMaxResults(pagesize);
            return hqlQuery.List<WebUser>();
        }
    }
}
