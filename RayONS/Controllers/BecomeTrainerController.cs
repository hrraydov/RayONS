using RayONS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RayONS.Controllers
{
    public class BecomeTrainerController : Controller
    {
        //
        // GET: /BecomeTrainer/
        RayONSEntities db = new RayONSEntities();
        public ActionResult Index()
        {
            if (!(User.IsInRole("Administrator") || User.IsInRole("Trainer")))
            {
                TempData["UserName"] = User.Identity.Name;
                return View();
            }
            else
            {
                return Redirect("/");
            }
        }
        [HttpPost]
        public ActionResult Index(int id,string why)
        {
            if (!TrainerQueryDAL.queryExist(User.Identity.Name))
            {
                ModelState.AddModelError(String.Empty, "Вече си подал заявка");
            }
            if (ModelState.IsValid)
            {
                TrainerQuery data = new TrainerQuery();
                data.UserId = UsersDAL.getUserId(User.Identity.Name);
                data.Why = why;
                db.TrainerQueries.Add(data);
                db.SaveChanges();
                return Redirect("/");
            }
            else
            {
                return View();
            }

        }
    }
}
