using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace NHibernate.Vertica.TestConsole
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory();
                
                return _sessionFactory;
            }
        }

        private static void InitializeSessionFactory()
        {
            // InitializeSessionFactoryMySql();
            InitializeSessionFactoryVertica7();
        }
        
        /// <summary>
        /// test this MySQL as well
        /// </summary>
        private static void InitializeSessionFactoryMySql()
        {
            _sessionFactory = Fluently.Configure()
                .Database(FluentNHibernate.Cfg.Db.MySQLConfiguration.Standard
                              .ConnectionString(@"Server=localhost;Database=test;Uid=root;Pwd=;")
                              .ShowSql()
                )
                .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<Car>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                                                .Create(true, true))
                .BuildSessionFactory();
        }

        private static void InitializeSessionFactoryVertica7()
        {
            // TODO: fill in your credentials here!
            _sessionFactory = Fluently.Configure()
                .Database(NHibernate.Vertica.Vertica7Configuration.Standard
                              .ConnectionString(@"Servername=<myserver>;Port=5433;Database=<mydb>;Username=dbadmin;Password=<mypassword>")
                              .ShowSql()
                )
                .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<Car>())
                
                // have not yet needed the following in Vertica, though some say it may be worth modifying
                // .ExposeConfiguration(cfg => cfg.SetProperty("hbm2ddl.keywords", "auto-quote"))

                // enabling the following line will generate the schema
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true)) 
                .BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}