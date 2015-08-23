using NHibernate.Cfg;
using NHibernate;

namespace NH.Dal {
    public class NHibernateHelper {
        private readonly ISessionFactory _sessionFactory;

        public NHibernateHelper() {
            _sessionFactory = GetSessionFactory();
        }

        private ISessionFactory GetSessionFactory() {
            return (new Configuration()).Configure().BuildSessionFactory();
        }

        public ISession GetSession() {
            return _sessionFactory.OpenSession();
        }
    }
}