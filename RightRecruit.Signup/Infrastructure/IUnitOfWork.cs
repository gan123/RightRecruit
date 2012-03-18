using Raven.Client;

namespace RightRecruit.Signup.Infrastructure
{
    public interface IUnitOfWork
    {
        IDocumentSession DocumentSession { get; }
    }
}