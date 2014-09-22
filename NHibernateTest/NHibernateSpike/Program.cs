using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernateSpike.Entities;


namespace NHibernateSpike
{
    class Program
    {
        static void Main(string[] args)
        {
            var sessionFactory = CreateSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var barginBasin = new Store {Name = "Bargin Basin"};
                    var superMart = new Store() {Name = "SuperMart"};

                    var potatoes = new Product() {Name = "Potatoes", Price = 3.60};
                    var fish = new Product { Name = "Fish", Price = 4.49 };
                    var milk = new Product { Name = "Milk", Price = 0.79 };
                    var bread = new Product { Name = "Bread", Price = 1.29 };
                    var cheese = new Product { Name = "Cheese", Price = 2.10 };
                    var waffles = new Product { Name = "Waffles", Price = 2.41 };

                    var daisy = new Employee() {FirstName = "Daisy", LastName = "Harrisons"};
                    var jack = new Employee { FirstName = "Jack", LastName = "Torrances" };
                    var sue = new Employee { FirstName = "Sue", LastName = "Walkters" };
                    var bill = new Employee { FirstName = "Bill", LastName = "Tafts" };
                    var joan = new Employee { FirstName = "Joan", LastName = "Popes" };
                    var dennis = new Employee { FirstName = "dennis", LastName = "liu" };

                    AddProductsToStore(barginBasin, potatoes, fish, milk, bread, cheese);
                    AddProductsToStore(superMart, bread, cheese, waffles);

                    AddEmployeesToStore(barginBasin, daisy, jack, sue);
                    AddEmployeesToStore(superMart, bill, joan,dennis);

                    //save
                    session.SaveOrUpdate(barginBasin);
                    session.SaveOrUpdate(superMart);

                    transaction.Commit();

                }

                using (session.BeginTransaction())
                {
                    var stores = session.CreateCriteria(typeof (Store))
                        .List<Store>();
                    foreach (var store in stores)
                    {
                        WriteStorePretty(store);
                    }
                }

                Console.ReadKey();
            }
        }

        private static void WriteStorePretty(Store store)
        {
            Console.WriteLine(store.Name);
        }

        private static void AddEmployeesToStore(Store store, params Employee[] employees)
        {
            foreach (var employee in employees)
            {
                store.AddEmployee(employee);
            }
        }

        private static void AddProductsToStore(Store store, params Product[] products)
        {
            foreach (var product in products)
            {
                store.AddProduct(product);
            }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            //return Fluently.Configure()
            //    .Database(
            //    SQLiteConfiguration.Standard
            //    .UsingFile("firstProject.db"))
            //    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
            //    .ExposeConfiguration(BuildSchema)
            //    .BuildSessionFactory();

            return Fluently.Configure()
                .Database(
                MsSqlConfiguration.MsSql2008
                .ConnectionString(c => c.FromConnectionStringWithKey("SQLDRSConnectionString")))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();

        }

        private static void BuildSchema(Configuration config)
        {
            //if (File.Exists("firstProject.db"))
            //    File.Delete("firstProject.db");
            new SchemaExport(config).Create(false, true);
        }
    }
}
