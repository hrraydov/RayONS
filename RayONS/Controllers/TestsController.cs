using RayONS.DAL;
using RayONS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RayONS.Controllers
{
    public class TestsController : Controller
    {
        //
        // GET: /Tests/

        public ActionResult Index()
        {
            return Redirect("/");
        }
        public ActionResult ViewTests(int id, int page)
        {

            List<TestsViewModel> viewModel = new List<TestsViewModel>();
            List<Test> list = TestsDAL.getAllTestsForSubcategory(id);
            int i = 1;
            foreach (var item in list)
            {
                if (item.isApproved == 1)
                    if (i > (page - 1) * 10 && i <= page * 10)
                    {
                        viewModel.Add(new TestsViewModel(item));

                    }
                i++;
            }
            int listCount = list.Capacity;

            int numberOfPages = listCount / 10;
            if (listCount % 10 != 0)
                numberOfPages++;
            ViewData["numberOfPages"] = numberOfPages;
            ViewBag.SubcategoryName = SubcategoriesDAL.getSubcategoryName(id);
            ViewBag.SubcategoryId = id;
            return View(viewModel);
        }
        public ActionResult ViewTest(int id)
        {
            List<Question> questions = TestsDAL.getAllQuestionsForTest(id);
            ViewBag.TestName = TestsDAL.getTestName(id);
            TempData["QuestionsCount"] = questions.Count;
            TempData["TestId"] = id;
            return View(questions);
        }
        [HttpPost]
        public ActionResult Result(int[] questions, int[] questionsIds, int QuestionsCount, int TestId)
        {
            if (QuestionsCount != (int)TempData["QuestionsCount"])
            {
                ModelState.AddModelError(String.Empty, "Грешно ID");
            }
            if (TestId != (int)TempData["TestId"])
            {
                ModelState.AddModelError(String.Empty, "Грешно ID");
            }
            if (ModelState.IsValid)
            {
                int br = 0;
                int i = 0;
                for (i = 0; i < QuestionsCount; i++)
                {
                    if (questions[i] == TestsDAL.getTrueAnswer(questionsIds[i])) br += 1;
                }
                ViewBag.CorrectAnswers = br;
                ViewBag.QuestionsCount = QuestionsCount;
                RayONSEntities db = new RayONSEntities();
                TestsResult testsResult = new TestsResult();
                testsResult.QuestionsCount = QuestionsCount;
                testsResult.Result = br;
                testsResult.TestId = TestId;
                testsResult.UserId = UsersDAL.getUserId(User.Identity.Name);
                db.TestsResults.Add(testsResult);
                db.SaveChanges();
                return View();
            }
            else
            {
                return View();
            }
        }
    }
}
