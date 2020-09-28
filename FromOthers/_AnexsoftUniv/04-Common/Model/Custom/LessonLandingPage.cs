using Common;
using System.Collections;
using System.Collections.Generic;

namespace Model.Custom
{
    public class LessonLandingPage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Video { get; set; }
        public bool HasVideo
        {
            get
            {
                return !string.IsNullOrEmpty(Video);
            }
        }
        public string Content { get; set; }
        public bool Learned { get; set; }

        public List<LessonListLandingPage> Lessons { get;set;}

        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public LessonLandingPage()
        {
            Lessons = new List<LessonListLandingPage>();
        }
    }

    public class LessonListLandingPage
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public bool Learned { get; set; }
    }
}
