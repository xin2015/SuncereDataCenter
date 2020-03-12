using SuncereDataCenter.Basic.CryptoTransverters;
using SuncereDataCenter.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        #region User
        public ActionResult UserList()
        {
            return View(entities.SuncereUser.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserList(string keyword)
        {
            IQueryable<SuncereUser> query = entities.SuncereUser;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(o => o.UserName.Contains(keyword) || o.DisplayName.Contains(keyword) || o.Remark.Contains(keyword));
            }
            ViewBag.Keyword = keyword;
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
                SuncereUser item = entities.SuncereUser.FirstOrDefault(o => o.UserName == user.UserName);
                if (item == null)
                {
                    user.Status = true;
                    user.CreationTime = DateTime.Now;
                    user.Password = SHA1Encryption.Default.EncryptPassword(user.Password);
                    entities.SuncereUser.Add(user);
                    entities.SaveChanges();
                    return RedirectToAction("UserList");
                }
                else
                {
                    ModelState.AddModelError("UserExist", "账号已存在！");
                }
            }

            return View(user);
        }

        public ActionResult UserDetails(int id)
        {
            SuncereUser user = entities.SuncereUser.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult UserEdit(int id)
        {
            SuncereUser user = entities.SuncereUser.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit([Bind(Include = "Id,DisplayName,EmailAddress,PhoneNumber,Status,IP,EnableIPBinding,Remark")] SuncereUser user)
        {
            if (ModelState.IsValid)
            {
                SuncereUser item = entities.SuncereUser.Find(user.Id);
                if (item == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    item.DisplayName = user.DisplayName;
                    item.EmailAddress = user.EmailAddress;
                    item.PhoneNumber = user.PhoneNumber;
                    item.Status = user.Status;
                    item.IP = user.IP;
                    item.EnableIPBinding = user.EnableIPBinding;
                    item.Remark = user.Remark;
                    item.LastModificationTime = DateTime.Now;
                    entities.SaveChanges();
                    return RedirectToAction("UserList");
                }
            }

            return View(user);
        }

        public ActionResult UserDelete(int id)
        {
            SuncereUser user = entities.SuncereUser.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("UserDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult UserDeleteConfirmed(int id)
        {
            SuncereUser user = entities.SuncereUser.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            entities.SuncereUser.Remove(user);
            entities.SaveChanges();
            return RedirectToAction("UserList");
        }

        public ActionResult UserReset(int id)
        {
            SuncereUser user = entities.SuncereUser.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("UserReset")]
        [ValidateAntiForgeryToken]
        public ActionResult UserResetConfirmed(int id)
        {
            SuncereUser user = entities.SuncereUser.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            user.Password = SHA1Encryption.Default.EncryptPassword("123456");
            entities.SaveChanges();
            return RedirectToAction("UserList");
        }

        public ActionResult UserRoleList(int userId)
        {
            SuncereUser user = entities.SuncereUser.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = userId;
            return View(user.SuncereRole.ToList());
        }

        public ActionResult UserRoleAdd(int userId)
        {
            SuncereUser user = entities.SuncereUser.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = userId;
            List<SuncereRole> roleList = entities.SuncereRole.ToList();
            SelectList selectList = new SelectList(roleList, "Id", "Name");
            ViewBag.RoleSelectList = selectList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserRoleAdd(int userId, int roleId)
        {
            SuncereUser user = entities.SuncereUser.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            SuncereRole role = entities.SuncereRole.Find(roleId);
            if (role == null)
            {
                return HttpNotFound();
            }
            if (!user.SuncereRole.Any(o => o.Id == roleId))
            {
                user.SuncereRole.Add(role);
                entities.SaveChanges();
            }
            return RedirectToAction("UserRoleList", new { userId = userId });
        }
        #endregion

        #region Role
        public ActionResult RoleList()
        {
            return View(entities.SuncereRole.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleList(string keyword)
        {
            IQueryable<SuncereRole> query = entities.SuncereRole;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(o => o.Name.Contains(keyword) || o.Remark.Contains(keyword));
            }
            ViewBag.Keyword = keyword;
            return View(query.ToList());
        }

        public ActionResult RoleAdd()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAdd([Bind(Include = "Name,Remark")] SuncereRole role)
        {
            if (ModelState.IsValid)
            {
                SuncereRole item = entities.SuncereRole.FirstOrDefault(o => o.Name == role.Name);
                if (item == null)
                {
                    role.Status = true;
                    role.CreationTime = DateTime.Now;
                    entities.SuncereRole.Add(role);
                    entities.SaveChanges();
                    return RedirectToAction("RoleList");
                }
                else
                {
                    ModelState.AddModelError("RoleExist", "角色已存在！");
                }
            }

            return View(role);
        }

        public ActionResult RoleDetails(int id)
        {
            SuncereRole role = entities.SuncereRole.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        public ActionResult RoleEdit(int id)
        {
            SuncereRole role = entities.SuncereRole.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleEdit([Bind(Include = "Id,Status,Remark")] SuncereRole role)
        {
            if (ModelState.IsValid)
            {
                SuncereRole item = entities.SuncereRole.Find(role.Id);
                if (item == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    item.Status = role.Status;
                    item.Remark = role.Remark;
                    item.LastModificationTime = DateTime.Now;
                    entities.SaveChanges();
                    return RedirectToAction("RoleList");
                }
            }

            return View(role);
        }
        #endregion

        #region Permission
        public ActionResult PermissionList()
        {
            List<SuncerePermission> parentPermissionList = entities.SuncerePermission.Where(o => o.Type == 1).ToList();
            SelectList selectList = new SelectList(parentPermissionList, "Id", "Name");
            ViewBag.ParentPermissionSelectList = selectList;
            return View(entities.SuncerePermission.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PermissionList(string keyword, int? parentId)
        {
            IQueryable<SuncerePermission> query = entities.SuncerePermission;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(o => o.Name.Contains(keyword) || o.Controller.Contains(keyword) || o.Action.Contains(keyword) || o.Remark.Contains(keyword));
            }
            if (parentId.HasValue)
            {
                query = query.Where(o => o.ParentId == parentId.Value);
            }
            ViewBag.Keyword = keyword;
            List<SuncerePermission> parentPermissionList = entities.SuncerePermission.Where(o => o.Type == 1).ToList();
            SelectList selectList = new SelectList(parentPermissionList, "Id", "Name", parentId);
            ViewBag.ParentPermissionSelectList = selectList;
            return View(query.ToList());
        }
        #endregion

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