using RayONS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RayONS.Models
{
    public class TestsViewModel
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public TestsViewModel(Test test)
        {
            this.TestId = test.TestId;
            this.TestName = test.TestName;
        }
    }
}