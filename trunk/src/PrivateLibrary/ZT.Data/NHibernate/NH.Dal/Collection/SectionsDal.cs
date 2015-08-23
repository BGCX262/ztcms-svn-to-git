using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHEntity.Entities;
using NHibernate;

namespace NH.Dal.Collection {
        public partial class SectionsDal {
        private NHibernateHelper nhibernateHelper = new NHibernateHelper();

        protected ISession Session { get; set; }

        public SectionsDal() {
            this.Session = nhibernateHelper.GetSession();
        }

        public SectionsDal(ISession session) {
            this.Session = session;
        }

        public void CreateSections(Sections Sections) {
            Session.Save(Sections);
            Session.Flush();
        }

        public Sections GetSectionsById(int ID) {
            return Session.Get<Sections>(ID);
        }

        public IList<Sections> GetSectionss() {
            IList<Sections> list = null;
            list = Session.QueryOver<Sections>().List();
            return list;
        }
    }
}