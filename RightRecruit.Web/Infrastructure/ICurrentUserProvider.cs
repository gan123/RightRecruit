using System;
using RightRecruit.Domain;
using RightRecruit.Domain.User;

namespace RightRecruit.Web.Infrastructure
{
    public interface ICurrentUserProvider
    {
        User CurrentUser { get; }
    }
}