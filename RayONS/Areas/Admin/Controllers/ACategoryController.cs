using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RayONS.DAL;

namespace RayONS.Areas.Admin.Controllers
{
    public class ACategoryController : Controller
    {
        private RayONSEntities db = new RayONSEntities();

        //
        // GET: /Admin/Category/

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Administrator"))
            {
                return View(db.Categories.OrderBy(c => c.CategoryName).ToList());
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // GET: /Admin/Category/Details/5

        public ActionResult Details(int id)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Administrator"))
            {
                Category category = db.Categories.Find(id);
                return View(category);
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // GET: /Admin/Category/Create

        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Administrator"))
            {
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // POST: /Admin/Category/Create

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (String.IsNullOrEmpty(category.CategoryName))
                ModelState.AddModelError("CategoryName", "Въведете име на категорията");
            if (CategoriesDAL.existCategory(category.CategoryName) == true)
                ModelState.AddModelError("CategoryName", "Съществува категория с такова име");
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //
        // GET: /Admin/Category/Edit/5

        public ActionResult Edit(int id)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Administrator"))
            {
                Category category = db.Categories.Find(id);
                return View(category);
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // POST: /Admin/Category/Edit/5

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (String.IsNullOrEmpty(category.CategoryName))
                ModelState.AddModelError("CategoryName", "Въведете име на категорията");
            if (CategoriesDAL.existCategory(category.CategoryName) == false)
                ModelState.AddModelError("CategoryName", "Съществува категория с такова име");
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //
        // GET: /Admin/Category/Delete/5

        public ActionResult Delete(int id)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Administrator"))
            {
                Category category = db.Categories.Find(id);
                return View(category);
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // POST: /Admin/Category/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}