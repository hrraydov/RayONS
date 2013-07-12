using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RayONS.DAL;
using System.IO;
using RayONS.Models;

namespace RayONS.Areas.Trainer.Controllers
{
    public class TLessonsController : Controller
    {
        private RayONSEntities db = new RayONSEntities();

        //
        // GET: /Trainer/TLessons/
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated && (User.IsInRole("Administrator") || User.IsInRole("Trainer")))
            {
                if (User.IsInRole("Trainer"))
                {
                    Guid userId = UsersDAL.getUserId(User.Identity.Name);
                    var lessons = db.Lessons.Include(l => l.Subcategory).Include(l => l.User).Where(s => s.UserId == userId);

                    return View(lessons.ToList());
                }
                else
                {
                    Guid userId = UsersDAL.getUserId(User.Identity.Name);
                    var lessons = db.Lessons.Include(l => l.Subcategory).Include(l => l.User);

                    return View(lessons.ToList());
                }
            }
            else
            {
                return Redirect("/");
            }
        }

        public ActionResult SeeNotApproved()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Administrator"))
            {
                Guid userId = UsersDAL.getUserId(User.Identity.Name);
                var lessons = db.Lessons.Include(l => l.Subcategory).Include(l => l.User).Where(l => l.IsApproved == 0);

                return View(lessons.ToList());

            }
            else
            {
                return Redirect("/");
            }
        }
        //
        // GET: /Trainer/TLessons/Details/5

        public ActionResult Details(int id)
        {
            if (User.Identity.IsAuthenticated && (User.IsInRole("Administrator") || User.IsInRole("Trainer")))
            {

                Lesson lesson = db.Lessons.Find(id);
                return View(lesson);
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // GET: /Trainer/TLessons/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated && (User.IsInRole("Administrator") || User.IsInRole("Trainer")))
            {
                ViewBag.SubcategoryId = new SelectList(db.Subcategories, "SubcategoryId", "SubcategoryName");
                ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // POST: /Trainer/TLessons/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Lesson lesson, string FCKeditor)
        {
            if (LessonsDAL.existLesson(lesson.LessonName))
                ModelState.AddModelError("LessonName", "Съществува урок с това име");
            if (String.IsNullOrEmpty(lesson.LessonName))
                ModelState.AddModelError("LessonName", "Въведете име на урока");
            if (ModelState.IsValid)
            {
                if (lesson.LessonName.Length > 50)
                    ModelState.AddModelError("LessonName", "Името на урока трябва да е под 50 символа");
                if (ModelState.IsValid)
                {
                    lesson.UserId = UsersDAL.getUserId(User.Identity.Name);
                    DateTime datetime = DateTime.Now;
                    var lessonName = datetime.ToString("yyyyMMddHHmmssfff");
                    StreamWriter sr = System.IO.File.CreateText(HttpContext.Server.MapPath(@"~\Content\Lessons\Lesson" + lessonName + ".txt"));
                    using (sr)
                    {
                        sr.Write(FCKeditor);
                    }
                    sr.Dispose();
                    lesson.Code = lessonName;
                    db.Lessons.Add(lesson);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.SubcategoryId = new SelectList(db.Subcategories, "SubcategoryId", "SubcategoryName", lesson.SubcategoryId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", lesson.UserId);
            return View(lesson);
        }

        //
        // GET: /Trainer/TLessons/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (User.Identity.IsAuthenticated && (User.IsInRole("Administrator") || User.IsInRole("Trainer")))
            {
                var url = LessonsDAL.getLessonCode(id);
                Lesson lesson = db.Lessons.Find(id);
                var reader = new StreamReader(HttpContext.Server.MapPath(@"~\Content\Lessons\Lesson" + url + ".txt"));
                var contents = reader.ReadToEnd();
                reader.Dispose();
                ViewBag.Content = contents;
                ViewBag.SubcategoryId = new SelectList(db.Subcategories, "SubcategoryId", "SubcategoryName", lesson.SubcategoryId);
                ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", lesson.UserId);
                TempData["LessonId"] = lesson.LessonId;
                TempData["UserId"] = lesson.UserId;
                return View(lesson);
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // POST: /Trainer/TLessons/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Lesson lesson, string FCKeditor)
        {
            string lessonName = LessonsDAL.getLessonName(lesson.LessonId);
            if (lesson.LessonId != (int)TempData["LessonId"])
            {
                ModelState.AddModelError(String.Empty, "Грешно ID");
            }
            if (lesson.UserId != (Guid)TempData["UserId"])
            {
                ModelState.AddModelError(String.Empty, "Грешно ID");
            }
            if (String.IsNullOrEmpty(lessonName))
                ModelState.AddModelError("LessonName", "Въведете име на урока");
            if (ModelState.IsValid)
            {

                var lessonNameC = LessonsDAL.getLessonCode(lesson.LessonId);
                StreamWriter sr = System.IO.File.CreateText(HttpContext.Server.MapPath(@"~\Content\Lessons\Lesson" + lessonNameC + ".txt"));
                sr.Write(FCKeditor);
                sr.Dispose();
                lesson.LessonName = lessonName;

                lesson.Code = lessonNameC;
                db.Entry(lesson).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.SubcategoryId = new SelectList(db.Subcategories, "SubcategoryId", "SubcategoryName", lesson.SubcategoryId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", lesson.UserId);

            return View(lesson);
        }

        //
        // GET: /Trainer/TLessons/Delete/5

        public ActionResult Delete(int id)
        {
            if (User.Identity.IsAuthenticated && (User.IsInRole("Administrator") || User.IsInRole("Trainer")))
            {

                Lesson lesson = db.Lessons.Find(id);
                return View(lesson);
            }
            else
            {
                return Redirect("/");
            }
        }

        //
        // POST: /Trainer/TLessons/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DateTime datetime = DateTime.Now;
            var lessonNameC = datetime.ToString("yyyyMMddHHmmssfff");
            Lesson lesson = db.Lessons.Find(id);
            System.IO.File.Delete(HttpContext.Server.MapPath(@"~\Content\Lessons\Lesson" + lessonNameC + ".txt"));
            db.Lessons.Remove(lesson);
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