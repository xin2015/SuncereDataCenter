using Newtonsoft.Json;
using SuncereDataCenter.Basic.CryptoTransverters;
using SuncereDataCenter.Core.Attributes;
using SuncereDataCenter.Core.Helper;
using SuncereDataCenter.Core.SystemManagement;
using SuncereDataCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuncereDataCenter.API.Controllers
{
    [SuncereAuthorize]
    public class HomeController : Controller
    {
        private SuncereDataCenterModel model;

        public HomeController()
        {
            model = new SuncereDataCenterModel();
        }

        public ActionResult Index()
        {
            return View();
        }

        public string GetToken()
        {
            TokenModel tm = new TokenModel("test", "Test@2020");
            return AsymmetricEncryption.Default.EncryptToString(JsonConvert.SerializeObject(tm));
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(TokenModel tm)
        {
            if (ModelState.IsValid)
            {
                if (tm.Time.AddMinutes(30) < DateTime.Now)
                {
                    ModelState.AddModelError("", "登录超时。");
                }
                else
                {
                    SuncereUser user = model.SuncereUser.FirstOrDefault(o => o.Status && o.UserName == tm.UserName);
                    if (user == null)
                    {
                        ModelState.AddModelError("", "账号不存在或者用户已关闭。");
                    }
                    else
                    {
                        if (SHA1Encryption.Default.EncryptPassword(tm.Password) == user.Password)
                        {
                            user.LastLoginIP = Request.UserHostAddress;
                            user.LastLoginTime = DateTime.Now;
                            List<SuncerePermission> userPermissions = new List<SuncerePermission>();
                            foreach (SuncereRole role in user.SuncereRole)
                            {
                                if (role.Status)
                                {
                                    foreach (SuncerePermission permission in role.SuncerePermission)
                                    {
                                        if (permission.Status)
                                        {
                                            if (!userPermissions.Contains(permission))
                                            {
                                                userPermissions.Add(permission);
                                            }
                                        }
                                    }
                                }
                            }
                            model.SaveChanges();
                            SessionHelper.SetCurrentUser(user);
                            SessionHelper.SetUserPermissions(userPermissions);
                            string returnUrl = SessionHelper.GetReturnUrl();
                            return RedirectToLocal(returnUrl);
                        }
                        else
                        {
                            ModelState.AddModelError("Password", "密码错误。");
                        }
                    }
                }
            }
            return View(tm);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            SessionHelper.SetCurrentUser(null);
            SessionHelper.SetUserPermissions(null);
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                model.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}