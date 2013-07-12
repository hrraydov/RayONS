using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RayONS.DAL;
using System.Web.Security;

namespace RayONS.Areas.Trainer.Controllers
{
    public class TrainerQueriesController : Controller
    {
        private RayONSEntities db = new RayONSEntities();

        //
        // GET: /Trainer/TrainerQueries/

        public ActionResult Index()
        {
            if (User.IsInRole("Administrator"))
            {
                var trainerqueries = db.TrainerQueries.Include(t => t.User);
                return View(trainerqueries.ToList());
            }
            else if (User.Identity.IsAuthenticated)
            {
                Guid UserId = UsersDAL.getUserId(User.Identity.Name);
                var trainerqueries = db.TrainerQueries.Include(t => t.User).Where(t => t.UserId == UserId);
                return View(trainerqueries.ToList());
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // GET: /Trainer/TrainerQueries/Details/5

        public ViewResult Details(Guid id)
        {
            TrainerQuery trainerquery = db.TrainerQueries.Find(id);
            return View(trainerquery);
        }

        //
        // GET: /Trainer/TrainerQueries/Create

        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
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
        // POST: /Trainer/TrainerQueries/Create

        [HttpPost]
        public ActionResult Create(TrainerQuery trainerquery)
        {
            if (ModelState.IsValid)
            {
                db.TrainerQueries.Add(trainerquery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", trainerquery.UserId);
            return View(trainerquery);
        }

        //
        // GET: /Trainer/TrainerQueries/Edit/5

        public ActionResult Edit(Guid id)
        {
            if (User.Identity.IsAuthenticated)
            {
                TrainerQuery trainerquery = db.TrainerQueries.Find(id);
                ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", trainerquery.UserId);
                return View(trainerquery);
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // POST: /Trainer/TrainerQueries/Edit/5

        [HttpPost]
        public ActionResult Edit(TrainerQuery trainerquery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainerquery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", trainerquery.UserId);
            return View(trainerquery);
        }

        //
        // GET: /Trainer/TrainerQueries/Delete/5

        public ActionResult Delete(Guid id)
        {
            if (User.IsInRole("Administrator"))
            {
                TrainerQuery trainerquery = db.TrainerQueries.Find(id);
                return View(trainerquery);
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // POST: /Trainer/TrainerQueries/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TrainerQuery trainerquery = db.TrainerQueries.Find(id);
            db.TrainerQueries.Remove(trainerquery);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MakeTrainer(Guid id)
        {
            if (User.IsInRole("Administrator"))
            {
                Roles.AddUserToRole(UsersDAL.getUserName(id), "Trainer");
                this.DeleteConfirmed(id);
                db.SaveChanges();
                return Redirect("/");
            }
            else
            {
                return Redirect("/");
            }
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}