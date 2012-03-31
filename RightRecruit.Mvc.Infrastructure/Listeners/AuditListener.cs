using System;
using System.Web;
using Raven.Client.Listeners;
using Raven.Json.Linq;
using RightRecruit.Domain;
using RightRecruit.Domain.User;

namespace RightRecruit.Mvc.Infrastructure.Listeners
{
    public class AuditListener : IDocumentStoreListener
    {
        public bool BeforeStore(string key, object entityInstance, RavenJObject metadata)
        {
            var entity = entityInstance as Entity;
            if (entity == null) return false;

            var currentUserId = HttpContext.Current.Session[Globals.CurrentUser] == null ? "system" : ((User)HttpContext.Current.Session[Globals.CurrentUser]).Id;
            if (string.IsNullOrEmpty(entity.CreatedUser))
            {
                entity.CreatedDate = DateTime.Now;
                entity.CreatedUser = currentUserId;
            }
            entity.LastUpdatedDate = DateTime.Now;
            entity.LastUpdatedUserId = currentUserId;

            return true;
        }

        public void AfterStore(string key, object entityInstance, RavenJObject metadata)
        {
        }
    }
}