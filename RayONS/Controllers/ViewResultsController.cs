using RayONS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RayONS.Controllers
{
    public class ViewResultsController : Controller
    {
        
        // GET: /ViewResults/

        public ActionResult Index()
        {
            List<TestsResult> results = TestsDAL.getResultsForUser(UsersDAL.getUserId(User.Identity.Name));
            return View(results);
        }

        public ActionResult ViewAllResults()
        {
            List<TestsResult> results = TestsDAL.getAllResults();
            return View(results);
        }
        public ActionResult ViewResultsForTest()
        {
            List<TestsResult> results = TestsDAL.getResultsForTests(UsersDAL.getUserId(User.Identity.Name));
            return View(results);
        }
    }
}
