using SuncereDataCenter.Core.Helper;
using SuncereDataCenter.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SuncereDataCenter.Core.Attributes
{
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "Unsealed so that subclassed types can set properties in the default constructor or override our behavior.")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class SuncereAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        public string Controller { get; set; }
        public string Action { get; set; }

        // This method must be thread-safe since it is called by the thread-safe OnCacheAuthorization() method.
        protected virtual bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool result;
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            SuncereUser user = SessionHelper.GetCurrentUser();
            if (user == null)
            {
                result = false;
            }
            else if (string.IsNullOrEmpty(Controller) && string.IsNullOrEmpty(Action))
            {
                result = true;
            }
            else
            {
                List<SuncerePermission> userPermissions = SessionHelper.GetUserPermissions();
                if (userPermissions == null)
                {
                    result = false;
                }
                else
                {
                    result = userPermissions.Any(o => o.Controller == Controller && o.Action == Action);
                }
                string localPath = httpContext.Request.Url.LocalPath;
                //SuncereUserActionLog log = new SuncereUserActionLog()
                //{
                //    Controller = localPath.Substr(localPath.IndexOf("/") + 1, localPath.LastIndexOf("/")),
                //    Action = localPath.Substring(localPath.LastIndexOf("/") + 1),
                //    Url = httpContext.Request.Url.PathAndQuery,
                //    Referrer = httpContext.Request.UrlReferrer.PathAndQuery,
                //    UserName = user.UserName,
                //    IPAddress = httpContext.Request.UserHostAddress,
                //    CreationTime = DateTime.Now
                //};
                //if (log.Url.Length > 256)
                //{
                //    log.Url = log.Url.Remove(256);
                //}
                //using (SuncereContext db = new SuncereContext())
                //{
                //    CreationAuditedRepository<SuncereUserActionLog> repository = new CreationAuditedRepository<SuncereUserActionLog>(db);
                //    repository.Add(log);
                //    db.SaveChanges();
                //}
            }
            return result;
        }

        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (OutputCacheAttribute.IsChildActionCacheActive(filterContext))
            {
                // If a child action cache block is active, we need to fail immediately, even if authorization
                // would have succeeded. The reason is that there's no way to hook a callback to rerun
                // authorization before the fragment is served from the cache, so we can't guarantee that this
                // filter will be re-run on subsequent requests.
                throw new InvalidOperationException("AuthorizeAttribute CannotUseWithinChildActionCache");
            }

            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
                                     || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);

            if (skipAuthorization)
            {
                return;
            }

            if (AuthorizeCore(filterContext.HttpContext))
            {
                // ** IMPORTANT **
                // Since we're performing authorization at the action level, the authorization code runs
                // after the output caching module. In the worst case this could allow an authorized user
                // to cause the page to be cached, then an unauthorized user would later be served the
                // cached page. We work around this by telling proxies not to cache the sensitive page,
                // then we hook our custom authorization code into the caching mechanism so that we have
                // the final say on whether a page should be served from the cache.

                HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                cachePolicy.AddValidationCallback(CacheValidateHandler, null /* data */);
            }
            else
            {
                HandleUnauthorizedRequest(filterContext);
            }
        }

        protected virtual void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                ReflectedActionDescriptor descriptor = filterContext.ActionDescriptor as ReflectedActionDescriptor;
                if (descriptor == null || descriptor.MethodInfo.ReturnType.Name == "ActionResult")
                {
                    SessionHelper.SetReturnUrl(filterContext.HttpContext.Request.Url.PathAndQuery);
                    filterContext.Result = new RedirectResult("/Home/Login");
                    return;
                }
            }
            filterContext.Result = new HttpUnauthorizedResult();
        }

        // This method must be thread-safe since it is called by the caching module.
        protected virtual HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            bool isAuthorized = AuthorizeCore(httpContext);
            return (isAuthorized) ? HttpValidationStatus.Valid : HttpValidationStatus.IgnoreThisRequest;
        }
    }
}
