using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;

namespace NHibernate.Vertica.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            CarTest();
            Console.ReadLine();
        }

        private static void CarTest()
        {
            //Create
            Create(1, "Ford");
            Create(2, "Mercedes");

            //Read
            var mercedes = Read("Mercedes");

            //Update
            mercedes.Name = "Mercedes Benz";
            Update(mercedes);

            //Delete
            Delete(mercedes);
        }

        private static void Delete(Make existingMake)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Delete(existingMake);
                    transaction.Commit();
                    Console.WriteLine("Deleted Make: " + existingMake.Name);
                }
            }            
        }

        private static void Update(Make newMakeName)
        {
            using(var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(newMakeName);
                    transaction.Commit();
                    Console.WriteLine("Updated Make: " + newMakeName.Name);
                }
            }
        }

        private static Make Read(string makeName)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var makeQuery = (from make in session.Query<Make>()
                                 where make.Name == makeName
                                 select make).SingleOrDefault();

                Console.WriteLine("Read Make: " + makeQuery.Name);
                return makeQuery;
            }
        }

        private static void Create(int id, string carMake)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var make = new Make
                                   {
                                       Id = id,
                                      Name = carMake
                                   };
                    session.Save(make);

                    transaction.Commit();
                    Console.WriteLine("Created Make: " + make.Name);

                }
            }
        }
    }


}
