using NHibernate.Dialect;
using FluentNHibernate.Cfg.Db;

namespace NHibernate.Vertica
{
    public class Vertica7Configuration : PersistenceConfiguration<Vertica7Configuration, Vertica7ConnectionStringBuilder>
    {
        protected Vertica7Configuration()
        {
            Driver<Vertica7Driver>();
        }

        public static Vertica7Configuration Standard
        {
            get { return new Vertica7Configuration().Dialect<Vertica7Dialect>(); }
        }
    }
}
