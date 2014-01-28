NHibernate.Vertica
==================

Vertica 7 (Unofficial) Support for NHibernate (and Linq to NHibernate)

NHibernate does not offer support for Vertica 7, not surprising as it is new, not widely adopted and not a particularly strong player in Microsoft environments. Some people appear to have been able to get Java Hibernate working with the PostgreSQL.Standard configuration, but that is with Java and JDBC. When I tried to use PostgresSQL it didn't work even after installing the Npgsql ADO.NET driver. There are some limitations and incompatibilities that crop up when attempting to use the NHibernate Postgresql configuration.

This package implements an Vertica7 Driver and Dialect for NHibernate. It uses the Vertica 7 ADO.NET provider by subclassing the PostgreSQL Dialect:

            _sessionFactory = Fluently.Configure()
                .Database(NHibernate.Vertica.Vertica7Configuration.Standard
                              .ConnectionString(@"Servername=<vertica7server>;Port=5433;Database=<dbname>;Username=dbadmin;Password=<mypassword>")

It is then possible to use NHibernate with Vertica 7 to:
  a) select data out of Vertica using Linq to NHibernate:
  
            var makeQuery = (from make in session.Query<Make>()
                                 where make.Name == makeName
                                 select make).SingleOrDefault();
                                 
  b) insert and update data
  c) generate schema into Vertica from POCOs
  d) Use Vertica-style prepared statements (these differ from PostGreSQL)
  
The TestConsole project demonstrates how to integrate NHibernate with Vertica 7.

Pre-requisites:
Dependent DLLs are referenced from nuget where possible. 
Also requires the installation of the Vertica 7 ADO.NET Driver (Vertica.Data) available at http://www.vertica.com/.

NUGET
This package is available at Nuget, https://www.nuget.org/packages/NHibernate.Vertica/



  



