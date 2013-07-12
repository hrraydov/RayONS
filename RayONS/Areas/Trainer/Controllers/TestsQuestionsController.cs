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
    public class TestsQuestionsController : Controller
    {
        private RayONSEntities db = new RayONSEntities();

        //
        // GET: /Trainer/TestsQuestions/

        public ViewResult Index(int id)
        {
            var questions = db.Questions.Include(q => q.Test).Where(q => q.TestId == id);
            return View(questions.ToList());
        }

        //
        // GET: /Trainer/TestsQuestions/Details/5

        public ViewResult Details(int id)
        {
            Question question = db.Questions.Find(id);
            return View(question);
        }

        //
        // GET: /Trainer/TestsQuestions/Create

        public ActionResult Create(int id)
        {
            ViewBag.TestId = new SelectList(db.Tests, "TestId", "TestName");
            return View();
        }

        //
        // POST: /Trainer/TestsQuestions/Create

        [HttpPost]
        public ActionResult Create(Question question, int id)
        {
            if (String.IsNullOrEmpty(question.Answer1))
                ModelState.AddModelError("Answer1", "Въведете Отговор 1");
            if (String.IsNullOrEmpty(question.QuestionName))
                ModelState.AddModelError("QuestionName", "Въведете въпрос");
            if (String.IsNullOrEmpty(question.Answer2))
                ModelState.AddModelError("Answer2", "Въведете Отговор 2");
            if (String.IsNullOrEmpty(question.Answer3))
                ModelState.AddModelError("Answer3", "Въведете Отговор 3");
            if (String.IsNullOrEmpty(question.Answer4))
                ModelState.AddModelError("Answer4", "Въведете Отговор 4");
            if (ModelState.IsValid)
            {
                question.TestId = id;
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = id });
            }

            ViewBag.TestId = new SelectList(db.Tests, "TestId", "TestName", question.TestId);
            return View(question);
        }

        //
        // GET: /Trainer/TestsQuestions/Edit/5

        public ActionResult Edit(int id)
        {
            Question question = db.Questions.Find(id);
            TempData["QuestionId"] = question.QuestionId;
            TempData["TestId"] = question.TestId;
            return View(question);
        }

        //
        // POST: /Trainer/TestsQuestions/Edit/5

        [HttpPost]
        public ActionResult Edit(Question question)
        {
            if (question.QuestionId != (int)TempData["QuestionId"])
            {
                ModelState.AddModelError(String.Empty, "Грешно Id - въпрос");
            }
            if (question.TestId != (int)TempData["TestId"])
            {
                ModelState.AddModelError(String.Empty, "Грешно Id - тест");
            }
            if (String.IsNullOrEmpty(question.Answer1))
                ModelState.AddModelError("Answer1", "Въведете Отговор 1");
            if (String.IsNullOrEmpty(question.QuestionName))
                ModelState.AddModelError("QuestionName", "Въведете въпрос");
            if (String.IsNullOrEmpty(question.Answer2))
                ModelState.AddModelError("Answer2", "Въведете Отговор 2");
            if (String.IsNullOrEmpty(question.Answer3))
                ModelState.AddModelError("Answer3", "Въведете Отговор 3");
            if (String.IsNullOrEmpty(question.Answer4))
                ModelState.AddModelError("Answer4", "Въведете Отговор 4");
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = question.TestId });
            }
            ViewBag.TestId = new SelectList(db.Tests, "TestId", "TestName", question.TestId);
            return View(question);
        }

        //
        // GET: /Trainer/TestsQuestions/Delete/5

        public ActionResult Delete(int id)
        {
            Question question = db.Questions.Find(id);
            return View(question);
        }

        //
        // POST: /Trainer/TestsQuestions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = id });
        }
        public void Delete2(int id, string del)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}