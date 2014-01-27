NHibernate.Vertica
==================

Vertica 7 (Unofficial) Support for NHibernate

NHibernate does not support Vertica at present. Some people seem to have been make use of the PostgreSQL.Standard configuration, though mostly on the Java front. It didn't work for me. In fact, there are numerous limitations and incompatibilities that crop up when using this NHibernate configuration.

This package implements an Vertica7 Driver and Dialect for NHibernate. It uses the Vertica 7 ADO.NET provider by subclassing the PostgreSQL Dialect:

            _sessionFactory = Fluently.Configure()
                .Database(FluentNHibernate.Cfg.Db.PostgreSQLConfiguration.Standard
                              .ConnectionString(@"Servername=<myserver>;Port=5433;Database=<mydb>;Username=dbadmin;Password=<mypassword>")
                              .Driver<NHibernate.Vertica.Vertica7Driver>()
                              .Dialect<NHibernate.Vertica.Vertica7Dialect>()
                              .ShowSql()

It is then possible to use NHibernate with Vertica 7 to:
  a) select data out of Vertica using Linq to NHibernate:
  
            var makeQuery = (from make in session.Query<Make>()
                                 where make.Name == makeName
                                 select make).SingleOrDefault();
                                 
  a) insert and update data
  b) generate schema into Vertica from POCOs
  c) Use Vertica-style prepared statements (these differ from PostGreSQL)
  
The TestConsole project demonstrates how to integrate NHibernate with Vertica 7.



  



