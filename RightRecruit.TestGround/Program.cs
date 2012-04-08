using System;
using System.Linq;
using Raven.Abstractions.Data;
using Raven.Client.Document;
using Raven.Client.Linq;
using Raven.Json.Linq;
using Raven.Client.Extensions;
using RightRecruit.Domain.Agency;

namespace RightRecruit.TestGround
{
    class Program
    {
        static void Main(string[] args)
        {
            var store = new DocumentStore {Url = "http://localhost:8082"};
            store.Initialize();

            //store.DatabaseCommands.EnsureDatabaseExists("TestDb");

            //using(var session = store.OpenSession("TestDb"))
            //{
            //    var agency = new Agency();
            //    agency.Name = "TestAgency";

            //    session.Store(agency);
            //    session.SaveChanges();
            //}

            using (var session = store.OpenSession("TestDb"))
            {
                var agency = session.Query<Agency>()
                    .Where(a => a.Name == "TestAgency")
                    .SingleOrDefault();

                if (agency != null)
                    Console.WriteLine(agency.Name);

            }

            Console.ReadLine();
        }
    }
}
