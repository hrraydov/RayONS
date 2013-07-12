using RayONS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RayONS.Models
{
    public class CategoriesViewModel
    {
        
        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

        public List<Subcat> Subcat { get; set; }

        public CategoriesViewModel(Category category, List<Subcategory> subcategory)
        {
            Subcat = new List<Subcat>();
            this.CategoryName = category.CategoryName;
            this.CategoryId = category.CategoryId;
            foreach (Subcategory item in subcategory)
            {
                this.Subcat.Add(new Subcat
                {
                    SubcategoryName = item.SubcategoryName,
                    SubcategoryId = item.SubcategoryId
                });
            }
        }
    }
    public class Subcat
    {
        public string SubcategoryName { get; set; }

        public int SubcategoryId { get; set; }

    }
}