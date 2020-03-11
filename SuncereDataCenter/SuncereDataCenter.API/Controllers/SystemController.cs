using SuncereDataCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuncereDataCenter.API.Controllers
{
    public class SystemController : Controller
    {
        private SuncereDataCenterEntities entities;

        public SystemController()
        {
            entities = new SuncereDataCenterEntities();
        }

        // GET: System
        public ActionResult UserList(string keyword)
        {
            IQueryable<SuncereUser> query = entities.SuncereUser;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(o => o.UserName.Contains(keyword) || o.DisplayName.Contains(keyword));
            }
            return View(query.ToList());
        }

        public ActionResult UserAdd()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserAdd([Bind(Include = "UserName,DisplayName,Password,EmailAddress,PhoneNumber,IP,EnableIPBinding,Remark")] SuncereUser user)
        {
            if (ModelState.IsValid)
            {
                user.Status = true;
                user.CreationTime = DateTime.Now;
                entities.SuncereUser.Add(user);
                entities.SaveChanges();
                return RedirectToAction("UserList");
            }

            return View(user);
        }

        public ActionResult PermissionList(string keyword, int? parentId)
        {
            IQueryable<SuncerePermission> query = entities.SuncerePermission;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(o => o.Name.Contains(keyword) || o.Controller.Contains(keyword) || o.Action.Contains(keyword));
            }
            if (parentId.HasValue)
            {
                query = query.Where(o => o.ParentId == parentId.Value);
            }
            List<SuncerePermission> parentPermissionList = entities.SuncerePermission.Where(o => o.Type == 1).ToList();
            SelectList selectList = new SelectList(parentPermissionList, "Id", "Name", parentId);
            ViewBag.ParentPermissionSelectList = selectList;
            return View(query.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                entities.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}