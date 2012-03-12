using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Raven.Abstractions.Json;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.MvcIntegration;
using RightRecruit.Web.Controllers;
using RightRecruit.Web.Infrastructure;

namespace RightRecruit.Web.Filters
{
    public class RavenActionFilter : ActionFilterAttribute
    {
        private static readonly IDocumentStore DocumentStore;

        static RavenActionFilter()
        {
            DocumentStore = new DocumentStore
                                {
                                    Url = ConfigurationManager.AppSettings["RavenServerUrl"]
                                };

            DocumentStore.Initialize();
            DocumentStore.Conventions.CustomizeJsonSerializer = jsonSerializer =>
            {
                jsonSerializer.Converters.Remove(jsonSerializer.Converters.Where(c =>
                c.GetType() == typeof(JsonEnumConverter)).First());
            };

            DocumentStore.Conventions.SaveEnumsAsIntegers = true;
            RavenProfiler.InitializeFor(DocumentStore);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sessionController = filterContext.Controller as AbstractController;
            if (sessionController == null) return;
            
            sessionController.UnitOfWork = new UnitOfWork();
            sessionController.HttpSessionStateBase["UnitOrWork"] = DocumentStore.OpenSession();
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
