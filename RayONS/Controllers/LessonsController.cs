using RayONS.DAL;
using RayONS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RayONS.Controllers
{
    public class LessonsController : Controller
    {

        public ActionResult Index()
        {
            return Redirect("/");
        }

        public ActionResult ViewLessons(int id, int page)
        {

            List<LessonsViewModel> viewModel = new List<LessonsViewModel>();
            List<Lesson> list = LessonsDAL.getAllLessonsForSubcategory(id);
            int i = 1;
            foreach (var item in list)
            {
                if (item.IsApproved == 1)
                    if (i > (page - 1) * 10 && i <= page * 10)
                    {
                        viewModel.Add(new LessonsViewModel(item));

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

        [HttpGet]
        public ActionResult ViewLesson(string url)
        {
            if (url != null)
            {
                var reader = new StreamReader(HttpContext.Server.MapPath(@"~\Content\Lessons\" + url + ".txt"));
                var contents = reader.ReadToEnd();
                ViewBag.Content = contents;
                ViewBag.LessonName = LessonsDAL.getLessonName(url.Substring(6));
                ViewBag.LessonOwner = LessonsDAL.getLessonOwner(url.Substring(6));
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }
    }
}
