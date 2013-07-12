using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RayONS.DAL;

namespace RayONS.Areas.Trainer.Controllers
{
    public class CategoryQueriesController : Controller
    {
        private RayONSEntities db = new RayONSEntities();

        //
        // GET: /Trainer/CategoryQueries/

        public ActionResult Index()
        {
            if (User.IsInRole("Trainer"))
            {
                Guid UserId = UsersDAL.getUserId(User.Identity.Name);
                var categoryqueries = db.CategoryQueries.Include(c => c.User).Where(c => c.UserId == UserId);
                return View(categoryqueries.ToList());
            }
            else if (User.IsInRole("Administrator"))
            {
                var categoryqueries = db.CategoryQueries.Include(c => c.User);
                return View(categoryqueries.ToList());
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // GET: /Trainer/CategoryQueries/Details/5

        public ViewResult Details(int id)
        {
            CategoryQuery categoryquery = db.CategoryQueries.Find(id);
            return View(categoryquery);
        }

        //
        // GET: /Trainer/CategoryQueries/Create

        public ActionResult Create()
        {
            if (User.IsInRole("Administrator") || User.IsInRole("Trainer"))
            {
                ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // POST: /Trainer/CategoryQueries/Create

        [HttpPost]
        public ActionResult Create(CategoryQuery categoryquery)
        {
            if (ModelState.IsValid)
            {
                db.CategoryQueries.Add(categoryquery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", categoryquery.UserId);
            return View(categoryquery);
        }

        //
        // GET: /Trainer/CategoryQueries/Edit/5

        public ActionResult Edit(int id)
        {
            if (User.IsInRole("Administrator") || User.IsInRole("Trainer"))
            {
                CategoryQuery categoryquery = db.CategoryQueries.Find(id);
                ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", categoryquery.UserId);
                return View(categoryquery);
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // POST: /Trainer/CategoryQueries/Edit/5

        [HttpPost]
        public ActionResult Edit(CategoryQuery categoryquery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryquery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", categoryquery.UserId);
            return View(categoryquery);
        }

        //
        // GET: /Trainer/CategoryQueries/Delete/5

        public ActionResult Delete(int id)
        {
            if (User.IsInRole("Administrator") || User.IsInRole("Trainer"))
            {
                CategoryQuery categoryquery = db.CategoryQueries.Find(id);
                return View(categoryquery);
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // POST: /Trainer/CategoryQueries/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryQuery categoryquery = db.CategoryQueries.Find(id);
            db.CategoryQueries.Remove(categoryquery);
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