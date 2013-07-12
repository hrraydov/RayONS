using RayONS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RayONS.Models
{
    public class LessonsViewModel
    {
        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public string LessonCode { get; set; }
        public LessonsViewModel(Lesson lesson)
        {
            this.LessonId = lesson.LessonId;
            this.LessonName = lesson.LessonName;
            this.LessonCode = lesson.Code;
        }
    }
}