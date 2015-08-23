using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHEntity.Entities;
using NHibernate;

namespace NH.Dal.Collection {
        public partial class ArticleDal {
        private NHibernateHelper nhibernateHelper = new NHibernateHelper();

        protected ISession Session { get; set; }

        public ArticleDal() {
            this.Session = nhibernateHelper.GetSession();
        }

        public ArticleDal(ISession session) {
            this.Session = session;
        }

        public void CreateArticle(Article Article) {
            Session.Save(Article);
            Session.Flush();
        }

        public Article GetArticleById(int ID) {
            return Session.Get<Article>(ID);
        }

        public IList<Article> GetArticles() {
            IList<Article> list = null;
            list = Session.QueryOver<Article>().List();
            return list;
        }
    }
}