using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayONS.DAL
{
    public static class CategoriesDAL
    {
        /// <summary>
        /// Gets all categories from database.
        /// </summary>
        /// <returns>List<Category></returns>
        public static List<Category> GetAllCategories()
        {
            RayONSEntities db = new RayONSEntities();
            return db.Categories.ToList();
        }
        /// <summary>
        /// Checks if category exists.
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns>bool</returns>
        public static bool existCategory(string categoryName)
        {
            RayONSEntities db = new RayONSEntities();
            int count = db.Categories.Where(c => c.CategoryName == categoryName).Count();
            if (count == 0) return false;
            else return true;
        }
    }
}
