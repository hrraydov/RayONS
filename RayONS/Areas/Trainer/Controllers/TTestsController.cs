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
    public class TTestsController : Controller
    {
        private RayONSEntities db = new RayONSEntities();

        //
        // GET: /Trainer/TTests/

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated && (User.IsInRole("Administrator") || User.IsInRole("Trainer")))
            {
                if (User.IsInRole("Trainer"))
                {
                    Guid userId = UsersDAL.getUserId(User.Identity.Name);
                    var tests = db.Tests.Include(t => t.User).Include(t => t.Subcategory).Where(t => t.UserId == userId);
                    return View(tests.ToList());
                }
                else
                {
                    var tests = db.Tests.Include(t => t.User).Include(t => t.Subcategory);
                    return View(tests.ToList());
                }
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // GET: /Trainer/TTests/Details/5

        public ActionResult Details(int id)
        {
            if (User.Identity.IsAuthenticated && (User.IsInRole("Administrator") || User.IsInRole("Trainer")))
            {
                Test test = db.Tests.Find(id);
                return View(test);
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // GET: /Trainer/TTests/Create

        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated && (User.IsInRole("Administrator") || User.IsInRole("Trainer")))
            {
                ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
                ViewBag.SubcategoryId = new SelectList(db.Subcategories, "SubcategoryId", "SubcategoryName");
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // POST: /Trainer/TTests/Create

        [HttpPost]
        public ActionResult Create(Test test)
        {
            if (String.IsNullOrEmpty(test.TestName))
                ModelState.AddModelError("TestName", "Въведете име на теста");
            if (TestsDAL.existTest(test.TestName))
                ModelState.AddModelError("TestName", "Съществува тест с това име");
            if (ModelState.IsValid)
            {
                test.UserId = UsersDAL.getUserId(User.Identity.Name);
                db.Tests.Add(test);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", test.UserId);
            ViewBag.SubcategoryId = new SelectList(db.Subcategories, "SubcategoryId", "SubcategoryName", test.SubcategoryId);
            return View(test);
        }

        //
        // GET: /Trainer/TTests/Edit/5

        public ActionResult Edit(int id)
        {
            if (User.Identity.IsAuthenticated && (User.IsInRole("Administrator") || User.IsInRole("Trainer")))
            {
                Test test = db.Tests.Find(id);
                ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", test.UserId);
                ViewBag.SubcategoryId = new SelectList(db.Subcategories, "SubcategoryId", "SubcategoryName", test.SubcategoryId);
                TempData["TestId"] = test.TestId;
                TempData["UserId"] = test.UserId;
                return View(test);
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // POST: /Trainer/TTests/Edit/5

        [HttpPost]
        public ActionResult Edit(Test test)
        {
            if (String.IsNullOrEmpty(test.TestName))
                ModelState.AddModelError("TestName", "Въведете име на теста");
            if (test.TestId != (int)TempData["TestId"])
            {
                ModelState.AddModelError(String.Empty, "Грешно ID");
            }
            if (test.UserId != (Guid)TempData["UserId"])
            {
                ModelState.AddModelError(String.Empty, "Грешно ID");
            }
            if (ModelState.IsValid)
            {
                db.Entry(test).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", test.UserId);
            ViewBag.SubcategoryId = new SelectList(db.Subcategories, "SubcategoryId", "SubcategoryName", test.SubcategoryId);
            return View(test);
        }

        //
        // GET: /Trainer/TTests/Delete/5

        public ActionResult Delete(int id)
        {
            if (User.Identity.IsAuthenticated && (User.IsInRole("Administrator") || User.IsInRole("Trainer")))
            {
                Test test = db.Tests.Find(id);
                return View(test);
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // POST: /Trainer/TTests/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            this.Delete2(id,"del");
            return RedirectToAction("Index");
        }
        public void Delete2(int id, string del) {
            var questionsList = db.Questions.Where(q => q.TestId == id).ToList();
            foreach (var item in questionsList) {
                var delete= new RayONS.Areas.Trainer.Controllers.TestsQuestionsController();
                delete.Delete2(item.QuestionId, "del");
            }
            var testsResults = db.TestsResults.Where(t => t.TestId == id).ToList();
            foreach (var item in testsResults) {
                TestsResult testsResult = db.TestsResults.Find(item.ResultId);
                db.TestsResults.Remove(testsResult);
                db.SaveChanges();
            }
            db.SaveChanges();
            Test test = db.Tests.Find(id);
            db.Tests.Remove(test);
            db.SaveChanges();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}