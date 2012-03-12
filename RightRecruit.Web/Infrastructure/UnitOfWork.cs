﻿using System.Web;
using Raven.Client;
using Raven.Client.Document;

namespace RightRecruit.Web.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDocumentSession DocumentSession
        {
            get { return (DocumentSession) HttpContext.Current.Session["UnitOfWOrk"]; }
        }
    }
}