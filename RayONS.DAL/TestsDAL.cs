using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayONS.DAL
{
    public static class TestsDAL
    {
        /// <summary>
        /// Checks if test exists.
        /// </summary>
        /// <param name="testName"></param>
        /// <returns></returns>
        public static bool existTest(string testName)
        {
            RayONSEntities db = new RayONSEntities();
            if (db.Tests.Where(t => t.TestName == testName).Count() > 0) return true;
            else return false;
        }
        /// <summary>
        /// Gets all tests for a given subcategory.
        /// </summary>
        /// <param name="subcategoryId"></param>
        /// <returns></returns>
        public static List<Test> getAllTestsForSubcategory(int subcategoryId)
        {
            RayONSEntities db = new RayONSEntities();
            return db.Tests.Where(l => l.SubcategoryId == subcategoryId).ToList();
        }
        /// <summary>
        /// Gets all questions for a given test.
        /// </summary>
        /// <param name="TestId"></param>
        /// <returns></returns>
        public static List<Question> getAllQuestionsForTest(int TestId)
        {
            RayONSEntities db = new RayONSEntities();
            return db.Questions.Where(q => q.TestId == TestId).ToList();
        }
        /// <summary>
        /// Gets the true answer for a give question.
        /// </summary>
        /// <param name="QuestionId"></param>
        /// <returns></returns>
        public static byte getTrueAnswer(int QuestionId)
        {
            RayONSEntities db = new RayONSEntities();
            return db.Questions.Where(q => q.QuestionId == QuestionId).Select(q => q.TrueAnswer).First();
        }
        /// <summary>
        /// Gets all test results for a given user.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static List<TestsResult> getResultsForUser(Guid UserId)
        {
            RayONSEntities db = new RayONSEntities();
            return db.TestsResults.Include("Test").Where(t => t.UserId == UserId).ToList();
        }
        /// <summary>
        /// Gets all test results.
        /// </summary>
        /// <returns></returns>
        public static List<TestsResult> getAllResults()
        {
            RayONSEntities db = new RayONSEntities();
            return db.TestsResults.Include("Test").Include("User").ToList();
        }
        /// <summary>
        /// Gets all results for a given test.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static List<TestsResult> getResultsForTests(Guid UserId)
        {
            RayONSEntities db = new RayONSEntities();
            return db.TestsResults.Include("Test").Include("User").Where(t => t.Test.UserId == UserId).ToList();
        }
        /// <summary>
        /// Gets the name of a test.
        /// </summary>
        /// <param name="TestId"></param>
        /// <returns></returns>
        public static string getTestName(int TestId)
        {
            RayONSEntities db = new RayONSEntities();
            return db.Tests.Where(t => t.TestId == TestId).Select(t => t.TestName).First(); ;
        }
    }
}
