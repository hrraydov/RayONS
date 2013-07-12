using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RayONS.Controllers;
using System.Web.Mvc;
using RayONS.Models;
using System.Collections.Generic;

namespace RayONS.Tests.Controllers
{
    [TestClass]
    public class LessonsControllerTest
    {
        [TestMethod]
        public void ViewLessons()
        {
            var controller = new LessonsController();
            var result = controller.ViewLessons(1, 1) as ViewResult;
            var model = controller.ViewData.Model as List<LessonsViewModel>;
            Assert.IsNotNull(model);
        }
        [TestMethod]
        public void ViewLessonWithNullUrl()
        {
            var controller = new LessonsController();
            var result = controller.ViewLesson(null) as RedirectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("/", result.Url);
        }
    }
}
