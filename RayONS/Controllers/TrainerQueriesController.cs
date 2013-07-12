using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RayONS.DAL;
using System.Web.Security;

namespace RayONS.Controllers
{
    public class TrainerQueriesController : Controller
    {
        //
        // GET: /TrainerQueries/
        RayONSEntities db = new RayONSEntities();
        public ActionResult Index()
        {
            if (User.IsInRole("Administrator"))
            {
                string[] viewModel = TrainerQueryDAL.getAllQueries();
                return View(viewModel);
            }
            else
            {
                return Redirect("/");
            }
        }
        [HttpPost]
        public ActionResult Index(string userName)
        {
            Roles.AddUserToRole(userName, "Trainer");
            TrainerQuery data = db.TrainerQueries.Find(UsersDAL.getUserId(userName));
            db.TrainerQueries.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
