using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SuncereDataCenter.Model;

namespace SuncereDataCenter.API.Controllers
{
    public class SuncereUsersController : Controller
    {
        private SuncereDataCenterEntities db = new SuncereDataCenterEntities();

        // GET: SuncereUsers
        public ActionResult Index()
        {
            return View(db.SuncereUser.ToList());
        }

        // GET: SuncereUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuncereUser suncereUser = db.SuncereUser.Find(id);
            if (suncereUser == null)
            {
                return HttpNotFound();
            }
            return View(suncereUser);
        }

        // GET: SuncereUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SuncereUsers/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,DisplayName,Password,EmailAddress,PhoneNumber,Status,Static,LastLoginTime,LastLoginIP,IP,EnableIPBinding,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,Remark")] SuncereUser suncereUser)
        {
            if (ModelState.IsValid)
            {
                db.SuncereUser.Add(suncereUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(suncereUser);
        }

        // GET: SuncereUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuncereUser suncereUser = db.SuncereUser.Find(id);
            if (suncereUser == null)
            {
                return HttpNotFound();
            }
            return View(suncereUser);
        }

        // POST: SuncereUsers/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,DisplayName,Password,EmailAddress,PhoneNumber,Status,Static,LastLoginTime,LastLoginIP,IP,EnableIPBinding,CreationTime,CreatorUserId,LastModificationTime,LastModifierUserId,Remark")] SuncereUser suncereUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suncereUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(suncereUser);
        }

        // GET: SuncereUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuncereUser suncereUser = db.SuncereUser.Find(id);
            if (suncereUser == null)
            {
                return HttpNotFound();
            }
            return View(suncereUser);
        }

        // POST: SuncereUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SuncereUser suncereUser = db.SuncereUser.Find(id);
            db.SuncereUser.Remove(suncereUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
