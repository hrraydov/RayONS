using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayONS;
using RayONS.Controllers;
using RayONS.Models;

namespace RayONS.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            HomeController controller = new HomeController();
            Assert.IsInstanceOfType(controller, typeof(HomeController));
        }

        [TestMethod]
        public void Tests()
        {
            var controller = new HomeController();
            var result = controller.Tests() as ViewResult;
            var model = result.ViewData.Model as List<CategoriesViewModel>;
            Assert.IsNotNull(model);
        }
    }
}
