using SuncereDataCenter.Basic.CryptoTransverters;
using SuncereDataCenter.Core.Attributes;
using SuncereDataCenter.Model;
using SuncereDataCenter.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuncereDataCenter.Web.Controllers
{
    [SuncereAuthorize]
    public class SystemController : Controller
    {
        private SuncereDataCenterModel model;

        public SystemController()
        {
            model = new SuncereDataCenterModel();
        }

        #region User
        [SuncereAuthorize(Controller = "System", Action = "UserList")]
        public ActionResult UserList()
        {
            return View(model.SuncereUser.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserList(string keyword)
        {
            IQueryable<SuncereUser> query = model.SuncereUser;
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
                SuncereUser item = model.SuncereUser.FirstOrDefault(o => o.UserName == user.UserName);
                if (item == null)
                {
                    user.Status = true;
                    user.CreationTime = DateTime.Now;
                    user.Password = SHA1Encryption.Default.EncryptPassword(user.Password);
                    model.SuncereUser.Add(user);
                    model.SaveChanges();
                    return RedirectToAction("UserList");
                }
                else
                {
                    ModelState.AddModelError("UserName", "账号已存在！");
                }
            }
            return View(user);
        }

        public ActionResult UserDetails(int id)
        {
            SuncereUser user = model.SuncereUser.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult UserEdit(int id)
        {
            SuncereUser user = model.SuncereUser.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit([Bind(Include = "Id,UserName,DisplayName,Password,EmailAddress,PhoneNumber,Status,IP,EnableIPBinding,Remark")] SuncereUser user)
        {
            if (ModelState.IsValid)
            {
                SuncereUser item = model.SuncereUser.Find(user.Id);
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
                    model.SaveChanges();
                    return RedirectToAction("UserList");
                }
            }
            return View(user);
        }

        public ActionResult UserDelete(int id)
        {
            SuncereUser user = model.SuncereUser.Find(id);
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
            SuncereUser user = model.SuncereUser.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            model.SuncereUser.Remove(user);
            model.SaveChanges();
            return RedirectToAction("UserList");
        }

        public ActionResult UserReset(int id)
        {
            SuncereUser user = model.SuncereUser.Find(id);
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
            SuncereUser user = model.SuncereUser.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            user.Password = SHA1Encryption.Default.EncryptPassword("123456");
            model.SaveChanges();
            return RedirectToAction("UserList");
        }

        public ActionResult UserRoleList(int userId)
        {
            SuncereUser user = model.SuncereUser.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = userId;
            return View(user.SuncereRole.ToList());
        }

        public ActionResult UserRoleAdd(int userId)
        {
            SuncereUser user = model.SuncereUser.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = userId;
            List<SuncereRole> roleList = model.SuncereRole.ToList();
            SelectList selectList = new SelectList(roleList, "Id", "Name");
            ViewBag.RoleSelectList = selectList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserRoleAdd(int userId, int roleId)
        {
            SuncereUser user = model.SuncereUser.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            SuncereRole role = model.SuncereRole.Find(roleId);
            if (role == null)
            {
                return HttpNotFound();
            }
            if (!user.SuncereRole.Any(o => o.Id == roleId))
            {
                user.SuncereRole.Add(role);
                model.SaveChanges();
            }
            return RedirectToAction("UserRoleList", new { userId = userId });
        }

        public ActionResult UserRoleDelete(int userId, int roleId)
        {
            SuncereUser user = model.SuncereUser.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            SuncereRole role = model.SuncereRole.Find(roleId);
            if (role == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = userId;
            return View(role);
        }

        [HttpPost, ActionName("UserRoleDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult UserRoleDeleteConfirmed(int userId, int roleId)
        {
            SuncereUser user = model.SuncereUser.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            SuncereRole role = model.SuncereRole.Find(roleId);
            if (role == null)
            {
                return HttpNotFound();
            }
            user.SuncereRole.Remove(role);
            model.SaveChanges();
            return RedirectToAction("UserRoleList", new { userId = userId });
        }

        public ActionResult ChangePassword(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel item, int id)
        {
            if (ModelState.IsValid)
            {
                SuncereUser user = model.SuncereUser.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                if (SHA1Encryption.Default.EncryptPassword(item.OldPassword) == user.Password)
                {
                    user.Password = SHA1Encryption.Default.EncryptPassword(item.NewPassword);
                    user.LastModificationTime = DateTime.Now;
                    model.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "旧密码不正确。");
                }
            }
            return View(item);
        }
        #endregion

        #region Role
        [SuncereAuthorize(Controller = "System", Action = "RoleList")]
        public ActionResult RoleList()
        {
            return View(model.SuncereRole.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleList(string keyword)
        {
            IQueryable<SuncereRole> query = model.SuncereRole;
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
                SuncereRole item = model.SuncereRole.FirstOrDefault(o => o.Name == role.Name);
                if (item == null)
                {
                    role.Status = true;
                    role.CreationTime = DateTime.Now;
                    model.SuncereRole.Add(role);
                    model.SaveChanges();
                    return RedirectToAction("RoleList");
                }
                else
                {
                    ModelState.AddModelError("Name", "名称已存在！");
                }
            }

            return View(role);
        }

        public ActionResult RoleDetails(int id)
        {
            SuncereRole role = model.SuncereRole.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        public ActionResult RoleEdit(int id)
        {
            SuncereRole role = model.SuncereRole.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleEdit([Bind(Include = "Id,Name,Status,Remark")] SuncereRole role)
        {
            if (ModelState.IsValid)
            {
                SuncereRole item = model.SuncereRole.Find(role.Id);
                if (item == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    item.Status = role.Status;
                    item.Remark = role.Remark;
                    item.LastModificationTime = DateTime.Now;
                    model.SaveChanges();
                    return RedirectToAction("RoleList");
                }
            }
            return View(role);
        }

        public ActionResult RoleDelete(int id)
        {
            SuncereRole role = model.SuncereRole.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        [HttpPost, ActionName("RoleDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult RoleDeleteConfirmed(int id)
        {
            SuncereRole role = model.SuncereRole.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            model.SuncereRole.Remove(role);
            model.SaveChanges();
            return RedirectToAction("RoleList");
        }

        public ActionResult RolePermissionList(int roleId)
        {
            SuncereRole role = model.SuncereRole.Find(roleId);
            if (role == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = roleId;
            return View(role.SuncerePermission.ToList());
        }

        public ActionResult RolePermissionAdd(int roleId)
        {
            SuncereRole role = model.SuncereRole.Find(roleId);
            if (role == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = roleId;
            List<SuncerePermission> permissionList = model.SuncerePermission.ToList();
            SelectList selectList = new SelectList(permissionList, "Id", "Name");
            ViewBag.PermissionSelectList = selectList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RolePermissionAdd(int roleId, int permissionId)
        {
            SuncereRole role = model.SuncereRole.Find(roleId);
            if (role == null)
            {
                return HttpNotFound();
            }
            SuncerePermission permission = model.SuncerePermission.Find(permissionId);
            if (permission == null)
            {
                return HttpNotFound();
            }
            if (!role.SuncerePermission.Any(o => o.Id == permissionId))
            {
                role.SuncerePermission.Add(permission);
                model.SaveChanges();
            }
            return RedirectToAction("RolePermissionList", new { roleId = roleId });
        }

        public ActionResult RolePermissionDelete(int roleId, int permissionId)
        {
            SuncereRole role = model.SuncereRole.Find(roleId);
            if (role == null)
            {
                return HttpNotFound();
            }
            SuncerePermission permission = model.SuncerePermission.Find(permissionId);
            if (permission == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = roleId;
            return View(permission);
        }

        [HttpPost, ActionName("RolePermissionDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult RolePermissionDeleteConfirmed(int roleId, int permissionId)
        {
            SuncereRole role = model.SuncereRole.Find(roleId);
            if (role == null)
            {
                return HttpNotFound();
            }
            SuncerePermission permission = model.SuncerePermission.Find(permissionId);
            if (permission == null)
            {
                return HttpNotFound();
            }
            role.SuncerePermission.Remove(permission);
            model.SaveChanges();
            return RedirectToAction("RolePermissionList", new { roleId = roleId });
        }
        #endregion

        #region Permission
        [SuncereAuthorize(Controller = "System", Action = "PermissionList")]
        public ActionResult PermissionList()
        {
            SelectList selectList = GetParentPermissionSelectList(null);
            ViewBag.ParentPermissionSelectList = selectList;
            return View(model.SuncerePermission.ToList());
        }

        private SelectList GetParentPermissionSelectList(int? parentId)
        {
            List<SuncerePermission> parentPermissionList = GetParentPermissionList();
            SelectList selectList = new SelectList(parentPermissionList, "Id", "Name", parentId);
            return selectList;
        }

        private List<SuncerePermission> GetParentPermissionList()
        {
            List<SuncerePermission> parentPermissionList = model.SuncerePermission.Where(o => o.Type == 1).ToList();
            parentPermissionList.Insert(0, new SuncerePermission() { Id = 0, Name = "无" });
            return parentPermissionList;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PermissionList(string keyword, int? parentId)
        {
            IQueryable<SuncerePermission> query = model.SuncerePermission;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(o => o.Name.Contains(keyword) || o.Controller.Contains(keyword) || o.Action.Contains(keyword) || o.Remark.Contains(keyword));
            }
            if (parentId.HasValue)
            {
                query = query.Where(o => o.ParentId == parentId.Value);
            }
            ViewBag.Keyword = keyword;
            SelectList selectList = GetParentPermissionSelectList(null);
            ViewBag.ParentPermissionSelectList = selectList;
            return View(query.ToList());
        }

        public ActionResult PermissionAdd()
        {
            SelectList selectList = GetParentPermissionSelectList(null);
            ViewBag.ParentPermissionSelectList = selectList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PermissionAdd([Bind(Include = "Name,Type,Controller,Action,Order,ParentId,Icon,Remark")] SuncerePermission permission)
        {
            if (ModelState.IsValid)
            {
                SuncerePermission item = model.SuncerePermission.FirstOrDefault(o => o.Name == permission.Name);
                if (item == null)
                {
                    permission.Status = true;
                    permission.CreationTime = DateTime.Now;
                    model.SuncerePermission.Add(permission);
                    model.SaveChanges();
                    return RedirectToAction("PermissionList");
                }
                else
                {
                    ModelState.AddModelError("PermissionExist", "权限已存在！");
                }
            }
            SelectList selectList = GetParentPermissionSelectList(null);
            ViewBag.ParentPermissionSelectList = selectList;
            return View(permission);
        }

        public ActionResult PermissionDetails(int id)
        {
            SuncerePermission permission = model.SuncerePermission.Find(id);
            if (permission == null)
            {
                return HttpNotFound();
            }
            List<SuncerePermission> parentPermissionList = GetParentPermissionList();
            ViewBag.ParentPermissionList = parentPermissionList;
            return View(permission);
        }

        public ActionResult PermissionEdit(int id)
        {
            SuncerePermission permission = model.SuncerePermission.Find(id);
            if (permission == null)
            {
                return HttpNotFound();
            }
            SelectList selectList = GetParentPermissionSelectList(permission.ParentId);
            ViewBag.ParentPermissionSelectList = selectList;
            return View(permission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PermissionEdit([Bind(Include = "Id,Name,Type,Controller,Action,Order,ParentId,Icon,Status,Remark")] SuncerePermission permission)
        {
            if (ModelState.IsValid)
            {
                SuncerePermission item = model.SuncerePermission.Find(permission.Id);
                if (item == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    item.Type = permission.Type;
                    item.Controller = permission.Controller;
                    item.Action = permission.Action;
                    item.Order = permission.Order;
                    item.ParentId = permission.ParentId;
                    item.Icon = permission.Icon;
                    item.Status = permission.Status;
                    item.Remark = permission.Remark;
                    item.LastModificationTime = DateTime.Now;
                    model.SaveChanges();
                    return RedirectToAction("PermissionList");
                }
            }
            SelectList selectList = GetParentPermissionSelectList(permission.ParentId);
            ViewBag.ParentPermissionSelectList = selectList;
            return View(permission);
        }

        public ActionResult PermissionDelete(int id)
        {
            SuncerePermission permission = model.SuncerePermission.Find(id);
            if (permission == null)
            {
                return HttpNotFound();
            }
            List<SuncerePermission> parentPermissionList = model.SuncerePermission.Where(o => o.Type == 1).ToList();
            ViewBag.ParentPermissionList = parentPermissionList;
            return View(permission);
        }

        [HttpPost, ActionName("PermissionDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult PermissionDeleteConfirmed(int id)
        {
            SuncerePermission permission = model.SuncerePermission.Find(id);
            if (permission == null)
            {
                return HttpNotFound();
            }
            model.SuncerePermission.Remove(permission);
            model.SaveChanges();
            return RedirectToAction("PermissionList");
        }
        #endregion

        #region Area
        public ActionResult AreaList()
        {
            return View(model.Area.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AreaList(string keyword)
        {
            IQueryable<Area> query = model.Area;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(o => o.AreaCode.Contains(keyword) || o.AreaName.Contains(keyword));
            }
            ViewBag.Keyword = keyword;
            return View(query.ToList());
        }

        public ActionResult AreaAdd()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AreaAdd([Bind(Include = "AreaCode,AreaName")] Area area)
        {
            if (ModelState.IsValid)
            {
                Area item = model.Area.FirstOrDefault(o => o.AreaCode == area.AreaCode);
                if (item == null)
                {
                    model.Area.Add(area);
                    model.SaveChanges();
                    return RedirectToAction("AreaList");
                }
                else
                {
                    ModelState.AddModelError("AreaCode", "编码已存在！");
                }
            }

            return View(area);
        }

        public ActionResult AreaEdit(string areaCode)
        {
            Area area = model.Area.Find(areaCode);
            if (area == null)
            {
                return HttpNotFound();
            }
            return View(area);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AreaEdit([Bind(Include = "AreaCode,AreaName")] Area area)
        {
            if (ModelState.IsValid)
            {
                Area item = model.Area.Find(area.AreaCode);
                if (item == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    item.AreaName = area.AreaName;
                    model.SaveChanges();
                    return RedirectToAction("AreaList");
                }
            }
            return View(area);
        }

        //public ActionResult AreaDelete(int id)
        //{
        //    Area role = model.Area.Find(id);
        //    if (role == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(role);
        //}

        //[HttpPost, ActionName("AreaDelete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult AreaDeleteConfirmed(int id)
        //{
        //    Area role = model.Area.Find(id);
        //    if (role == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    model.Area.Remove(role);
        //    model.SaveChanges();
        //    return RedirectToAction("AreaList");
        //}

        //public ActionResult AreaPermissionList(int roleId)
        //{
        //    Area role = model.Area.Find(roleId);
        //    if (role == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.AreaId = roleId;
        //    return View(role.Permission.ToList());
        //}

        //public ActionResult AreaPermissionAdd(int roleId)
        //{
        //    Area role = model.Area.Find(roleId);
        //    if (role == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.AreaId = roleId;
        //    List<Permission> permissionList = model.Permission.ToList();
        //    SelectList selectList = new SelectList(permissionList, "Id", "Name");
        //    ViewBag.PermissionSelectList = selectList;
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AreaPermissionAdd(int roleId, int permissionId)
        //{
        //    Area role = model.Area.Find(roleId);
        //    if (role == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    Permission permission = model.Permission.Find(permissionId);
        //    if (permission == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    if (!role.Permission.Any(o => o.Id == permissionId))
        //    {
        //        role.Permission.Add(permission);
        //        model.SaveChanges();
        //    }
        //    return RedirectToAction("AreaPermissionList", new { roleId = roleId });
        //}

        //public ActionResult AreaPermissionDelete(int roleId, int permissionId)
        //{
        //    Area role = model.Area.Find(roleId);
        //    if (role == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    Permission permission = model.Permission.Find(permissionId);
        //    if (permission == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.AreaId = roleId;
        //    return View(permission);
        //}

        //[HttpPost, ActionName("AreaPermissionDelete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult AreaPermissionDeleteConfirmed(int roleId, int permissionId)
        //{
        //    Area role = model.Area.Find(roleId);
        //    if (role == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    Permission permission = model.Permission.Find(permissionId);
        //    if (permission == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    role.Permission.Remove(permission);
        //    model.SaveChanges();
        //    return RedirectToAction("AreaPermissionList", new { roleId = roleId });
        //}
        #endregion

        #region City
        [AllowAnonymous]
        public ActionResult CityList()
        {
            return View(model.City.ToList());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CityList(string keyword)
        {
            IQueryable<City> query = model.City;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(o => o.CityCode.Contains(keyword) || o.CityName.Contains(keyword));
            }
            return View(query.ToList());
        }
        #endregion

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