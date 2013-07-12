using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayONS.DAL
{
    public class LessonsDAL
    {
        /// <summary>
        /// Gets all lessons for a given subcategory.
        /// </summary>
        /// <param name="subcategoryId"></param>
        /// <returns></returns>
        public static List<Lesson> getAllLessonsForSubcategory(int subcategoryId)
        {
            RayONSEntities db = new RayONSEntities();
            return db.Lessons.Where(l => l.SubcategoryId == subcategoryId).ToList();
        }
        /// <summary>
        /// Gets the author of a lesson.
        /// </summary>
        /// <param name="LessonCode"></param>
        /// <returns></returns>
        public static string getLessonOwner(string LessonCode)
        {
            RayONSEntities db = new RayONSEntities();
            return UsersDAL.getUserName((Guid)db.Lessons.Where(s => s.Code == LessonCode).Select(s => s.UserId).First());
        }
        /// <summary>
        /// Checks if lesson exists.
        /// </summary>
        /// <param name="LessonName"></param>
        /// <returns></returns>
        public static bool existLesson(string LessonName)
        {
            RayONSEntities db = new RayONSEntities();
            var count = db.Lessons.Where(s => s.LessonName == LessonName).Count();
            if (count > 0) return true;
            else return false;
        }
        /// <summary>
        /// Gets the name of a lesson by id of a lesson.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string getLessonName(int id)
        {
            RayONSEntities db = new RayONSEntities();
            return db.Lessons.Where(s => s.LessonId == id).Select(s => s.LessonName).First();
        }
        /// <summary>
        /// Gets the name of a lesson by a code of lesson.
        /// </summary>
        /// <param name="LessonCode"></param>
        /// <returns></returns>
        public static string getLessonName(string LessonCode)
        {
            RayONSEntities db = new RayONSEntities();
            return db.Lessons.Where(s => s.Code == LessonCode).Select(s => s.LessonName).First();
        }
        /// <summary>
        /// Gets the unique code for a lesson.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string getLessonCode(int id)
        {
            RayONSEntities db = new RayONSEntities();
            return db.Lessons.Where(s => s.LessonId == id).Select(s => s.Code).First();
        }
    }
}
