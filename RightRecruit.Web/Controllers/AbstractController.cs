using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using RightRecruit.Domain;
using RightRecruit.Web.Infrastructure;

namespace RightRecruit.Web.Controllers
{
    public class AbstractController : Controller
    {
        public HttpSessionStateBase HttpSessionStateBase
        {
            get { return Session; }
        }

        public IUnitOfWork UnitOfWork { get; set; }
        public ICurrentUserProvider CurrentUserProvider { get; set; }

        protected string GetAttachmentString(AttachmentReference attachmentReference)
        {
            var attachment = UnitOfWork.DocumentSession.Advanced.DatabaseCommands.GetAttachment(attachmentReference.AttachmentId);
            if (attachment != null && attachment.Data != null)
            {
                using(var streamReader = new StreamReader(attachment.Data(), Encoding.UTF8))
                {
                    return streamReader.ReadToEnd();
                }
            }

            return string.Empty;
        }
    }
}
