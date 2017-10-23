using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcUserManagement.Models;
using System.IO;

namespace MvcUserManagement.Controllers
{
    public class ProfileController : Controller
    {
        private userContext db = new userContext();

        // GET: Profile
        public ActionResult Index()
        {

            return View(db.userInformations.ToList());
        }

        // GET: Profile/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userInformation userInformation = db.userInformations.Find(id);
            if (userInformation == null)
            {
                return HttpNotFound();
            }
            return View(userInformation);
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            return View();
        }
       

        // POST: Profile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(userInformation userInformation,HttpPostedFileBase file)
        {
            
            if (ModelState.IsValid)
            {
                
                if (file != null && file.ContentLength > 0)
                {
                    var filename = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/"), filename);
                    var picpath = Path.Combine("~/Images", filename);
                    file.SaveAs(path);
                    userInformation.image = picpath.ToString();
                }
                db.userInformations.Add(userInformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userInformation);
        }

        // GET: Profile/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userInformation userInformation = db.userInformations.Find(id);
            if (userInformation == null)
            {
                return HttpNotFound();
            }
            return View(userInformation);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,fullName,age,email,image")] userInformation userInformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userInformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userInformation);
        }

        // GET: Profile/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userInformation userInformation = db.userInformations.Find(id);
            if (userInformation == null)
            {
                return HttpNotFound();
            }
            return View(userInformation);
        }

        // POST: Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            userInformation userInformation = db.userInformations.Find(id);
            db.userInformations.Remove(userInformation);
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
