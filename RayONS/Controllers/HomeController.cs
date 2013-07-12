using RayONS.DAL;
using RayONS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RayONS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Category> list = CategoriesDAL.GetAllCategories();
            List<CategoriesViewModel> viewModel = new List<CategoriesViewModel>();
            foreach (Category category in list)
            {
                viewModel.Add(new CategoriesViewModel(category, SubcategoriesDAL.getAllSubcategoriesForCategory(category.CategoryId)));
            }
            return View(viewModel);
        }



        public ActionResult Tests()
        {
            List<Category> list = CategoriesDAL.GetAllCategories();
            List<CategoriesViewModel> viewModel = new List<CategoriesViewModel>();
            foreach (Category category in list)
            {
                viewModel.Add(new CategoriesViewModel(category, SubcategoriesDAL.getAllSubcategoriesForCategory(category.CategoryId)));
            }
            return View(viewModel);
        }
    }
}
