using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System.Configuration;

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
			var cn = ConfigurationManager.ConnectionStrings["Vertica7"];
            
            _sessionFactory = Fluently.Configure()
                .Database(FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2012
                              .ConnectionString(cn.ConnectionString)
                              .ShowSql()
                )
                .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<Car>())
				// comment this out to avoid schema autogeneration
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                                                .Create(true, true))

				/* consider controlling batch size for inserts and updates
				.ExposeConfiguration(config =>
				{
					config.SetProperty("adonet.batch_size", "1");
				})
				*/
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