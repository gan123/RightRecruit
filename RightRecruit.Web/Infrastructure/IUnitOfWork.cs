using Raven.Client;

namespace RightRecruit.Web.Infrastructure
{
    public interface IUnitOfWork
    {
        IDocumentSession DocumentSession { get; }
    }
}