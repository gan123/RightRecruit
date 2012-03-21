﻿using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Raven.Abstractions.Json;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.MvcIntegration;
using RightRecruit.Domain.User;
using RightRecruit.Mvc.Infrastructure.Controllers;
using RightRecruit.Mvc.Infrastructure.Infrastructure;

namespace RightRecruit.Mvc.Infrastructure.Filters
{
    public class RavenActionFilter : ActionFilterAttribute
    {
        private static readonly IDocumentStore DocumentStore;

        static RavenActionFilter()
        {
            DocumentStore = new DocumentStore
                                {
                                    Url = ConfigurationManager.AppSettings["RavenServerUrl"],
                                    Conventions =
                                        {
                                            FindTypeTagName =  type =>
                                                                   {
                                                                       if (typeof(AgencyAdmin).IsAssignableFrom(type) || typeof(Recruiter).IsAssignableFrom(type)
                                                                           || typeof(ClientUser).IsAssignableFrom(type) || typeof(Candidate).IsAssignableFrom(type))
                                                                           return "Users";

                                                                       return DocumentConvention.DefaultTypeTagName(type);
                                                                   }
                                        }
                                };

            DocumentStore.Initialize();

            DocumentStore.Conventions.SaveEnumsAsIntegers = true;
            RavenProfiler.InitializeFor(DocumentStore);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sessionController = filterContext.Controller as AbstractController;
            if (sessionController == null) return;
            
            sessionController.UnitOfWork = new UnitOfWork();
            sessionController.HttpSessionStateBase[Globals.UnitOfWork] = DocumentStore.OpenSession();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var sessionController = filterContext.Controller as AbstractController;
            if (sessionController == null || filterContext.Exception != null) return;

            using(sessionController.UnitOfWork.DocumentSession)
            {
                if (sessionController.UnitOfWork.DocumentSession == null) return;
                sessionController.UnitOfWork.DocumentSession.SaveChanges();
            }
        }
    }
}
