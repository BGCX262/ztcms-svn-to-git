using System.Web;
using System.Web.Mvc;

namespace ZT.Api.User.Authorize
{
    public class OriginCheckAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Request.UrlReferrer == null)
            {
                filterContext.Result = new EmptyResult();
            }
        }
    }
}