//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RayONS.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Subcategory
    {
        public Subcategory()
        {
            this.Lessons = new HashSet<Lesson>();
            this.Tests = new HashSet<Test>();
        }
    
        public int SubcategoryId { get; set; }
        public int CategoryId { get; set; }
        public string SubcategoryName { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
    }
}
