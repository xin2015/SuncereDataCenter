using SuncereDataCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SuncereDataCenter.Core.Helper
{
    public static class SessionHelper
    {
        static string currentUserKey = "CurrentUser";
        static string returnUrlKey = "ReturnUrl";
        static string userPermissionsKey = "UserPermissions";

        public static void SetCurrentUser(SuncereUser user)
        {
            HttpContext.Current.Session[currentUserKey] = user;
        }

        public static SuncereUser GetCurrentUser()
        {
            return HttpContext.Current.Session[currentUserKey] as SuncereUser;
        }

        public static SuncereUser GetCurrentUserSafely()
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Session[currentUserKey] as SuncereUser;
            }
            else
            {
                return null;
            }
        }

        public static void SetReturnUrl(string url)
        {
            HttpContext.Current.Session[returnUrlKey] = url;
        }

        public static string GetReturnUrl()
        {
            return HttpContext.Current.Session[returnUrlKey] as string;
        }

        public static void SetUserPermissions(List<SuncerePermission> userPermissions)
        {
            HttpContext.Current.Session[userPermissionsKey] = userPermissions;
        }

        public static List<SuncerePermission> GetUserPermissions()
        {
            return HttpContext.Current.Session[userPermissionsKey] as List<SuncerePermission>;
        }
    }
}
