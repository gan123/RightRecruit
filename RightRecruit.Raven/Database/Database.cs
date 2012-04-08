using Raven.Client;
using Raven.Client.Extensions;

namespace RightRecruit.Raven.Database
{
    public class Database : IDatabase
    {
        public IDocumentStore DocumentStore { get; set; }

        public void Create(string name)
        {
            string dbName = null;
            if (name.Split(' ').Length > 0)
                dbName = name.Split(' ')[0].ToLower();
            if (DocumentStore != null && !string.IsNullOrEmpty(dbName))
                DocumentStore.DatabaseCommands.EnsureDatabaseExists(dbName);
        }
    }
}