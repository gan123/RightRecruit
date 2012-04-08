using System.Linq;
using Raven.Client.Indexes;
using RightRecruit.Domain.Client;

namespace RightRecruit.Raven.Indexes
{
    public class ClientsIndex : AbstractIndexCreationTask<Client>
    {
        public ClientsIndex()
        {
            Map = clients => from c in clients
                             select new
                                        {
                                            Name = c.Name + " " + c.AlternateName + " " + c.Contact.Email + " " + c.Address.ToString()
                                        };
        }
    }
}