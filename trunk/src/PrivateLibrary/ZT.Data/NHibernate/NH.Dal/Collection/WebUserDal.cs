using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHEntity.Entities;
using NHibernate;

namespace NH.Dal.Collection {
        public partial class WebUserDal {
        private NHibernateHelper nhibernateHelper = new NHibernateHelper();

        protected ISession Session { get; set; }

        public WebUserDal() {
            this.Session = nhibernateHelper.GetSession();
        }

        public WebUserDal(ISession session) {
            this.Session = session;
        }

        public void CreateWebUser(WebUser WebUser) {
            Session.Save(WebUser);
            Session.Flush();
        }

        public WebUser GetWebUserById(int ID) {
            return Session.Get<WebUser>(ID);
        }

        public IList<WebUser> GetWebUsers() {
            IList<WebUser> list = null;
            list = Session.QueryOver<WebUser>().List();
            return list;
        }
    }
}