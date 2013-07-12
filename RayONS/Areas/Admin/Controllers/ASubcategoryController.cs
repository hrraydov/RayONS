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
    public class ASubcategoryController : Controller
    {
        private RayONSEntities db = new RayONSEntities();

        //
        // GET: /Admin/ASubcategory/

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Administrator"))
            {
                var subcategories = db.Subcategories.Include(s => s.Category).OrderBy(s => s.Category.CategoryName);
                var categories = db.Categories.Select(c => c.CategoryName).ToList();
                ViewData["categoriesCount"] = categories.Count;
                ViewData["categories"] = categories;
                return View(subcategories.ToList());
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // GET: /Admin/ASubcategory/Details/5

        public ActionResult Details(int id)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Administrator"))
            {
                Subcategory subcategory = db.Subcategories.Find(id);
                return View(subcategory);
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // GET: /Admin/ASubcategory/Create

        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Administrator"))
            {
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // POST: /Admin/ASubcategory/Create

        [HttpPost]
        public ActionResult Create(Subcategory subcategory)
        {
            if (String.IsNullOrEmpty(subcategory.SubcategoryName))
                ModelState.AddModelError("SubcategoryName", "Въведете име на подкатегорията");
            if (SubcategoriesDAL.existSubcategory(subcategory.SubcategoryName) == true)
                ModelState.AddModelError("SubcategoryName", "Съществува подкатегория с това име");
            if (ModelState.IsValid)
            {

                db.Subcategories.Add(subcategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", subcategory.CategoryId);
            return View(subcategory);
        }

        //
        // GET: /Admin/ASubcategory/Edit/5

        public ActionResult Edit(int id)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Administrator"))
            {
                Subcategory subcategory = db.Subcategories.Find(id);
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", subcategory.CategoryId);
                return View(subcategory);
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // POST: /Admin/ASubcategory/Edit/5

        [HttpPost]
        public ActionResult Edit(Subcategory subcategory)
        {
            if (String.IsNullOrEmpty(subcategory.SubcategoryName))
                ModelState.AddModelError("SubcategoryName", "Въведете име на подкатегорията");
            if (SubcategoriesDAL.existSubcategory(subcategory.SubcategoryName) == true)
                ModelState.AddModelError("SubcategoryName", "Съществува подкатегория с това име");
            if (ModelState.IsValid)
            {
                db.Entry(subcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", subcategory.CategoryId);
            return View(subcategory);
        }

        //
        // GET: /Admin/ASubcategory/Delete/5

        public ActionResult Delete(int id)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Administrator"))
            {
                Subcategory subcategory = db.Subcategories.Find(id);
                return View(subcategory);
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // POST: /Admin/ASubcategory/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Subcategory subcategory = db.Subcategories.Find(id);
            db.Subcategories.Remove(subcategory);
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