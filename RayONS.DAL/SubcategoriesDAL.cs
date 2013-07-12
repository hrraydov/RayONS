using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayONS.DAL
{
    public class SubcategoriesDAL
    {
        /// <summary>
        /// Gets all subcategories for a given category.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public static List<Subcategory> getAllSubcategoriesForCategory(int categoryId)
        {
            RayONSEntities db = new RayONSEntities();
            return db.Subcategories.Where(s => s.CategoryId == categoryId).ToList();
        }
        /// <summary>
        /// Gets the name of a subcategory.
        /// </summary>
        /// <param name="subcategoryId"></param>
        /// <returns></returns>
        public static string getSubcategoryName(int subcategoryId)
        {
            RayONSEntities db = new RayONSEntities();
            return db.Subcategories.Where(s => s.SubcategoryId == subcategoryId).Select(s => s.SubcategoryName).First();
        }
        /// <summary>
        /// Checks if subcategory exists.
        /// </summary>
        /// <param name="subcategoryName"></param>
        /// <returns></returns>
        public static bool existSubcategory(string subcategoryName)
        {
            RayONSEntities db = new RayONSEntities();
            int count = db.Subcategories.Where(c => c.SubcategoryName == subcategoryName).Count();
            if (count == 0) return false;
            else return true;
        }
    }
}
