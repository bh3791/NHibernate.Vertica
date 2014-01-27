NHibernate.Vertica
==================

Vertica 7 Driver for NHibernate

NHibernate does not support Vertica, and some people make use of the PostgreSQL.Standard configuration.
However, there are numerous limitations and incompatibilities that crop up when using this configuration.

This package uses Fluent NHibernate to configure a Driver and Dialect for Vertica7 that uses the Vertica 7
ADO.NET provider by subclassing the PostgreSQL Dialect:

            _sessionFactory = Fluently.Configure()
                .Database(FluentNHibernate.Cfg.Db.PostgreSQLConfiguration.Standard
                              .ConnectionString(@"Servername=<myserver>;Port=5433;Database=<mydb>;Username=dbadmin;Password=<mypassword>")
                              .Driver<NHibernate.Vertica.Vertica7Driver>()
                              .Dialect<NHibernate.Vertica.Vertica7Dialect>()
                              .ShowSql()

It is then possible to use NHibernate with Vertica 7 to:
  a) insert and update data, 
  b) generate schema into Vertica
  c) use Linq to NHibernate with prepared statements
  
This is an example of how to integrate NHibernate with Vertica 7 and NO SUPPORT IS IMPLIED.

  



