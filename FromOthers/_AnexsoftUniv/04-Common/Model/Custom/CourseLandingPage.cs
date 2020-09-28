using Common;
using Common.MyExtensions;
using System;
using System.Collections.Generic;

namespace Model.Custom
{
    public class CourseLandingPage
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryIcon { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Instructor { get; set; }
        public int Students { get; set; }
        public decimal Price { get; set; }
        public decimal Vote { get; set; }

        public Enums.Status Status { get; set; }

        public IEnumerable<CourseLessonsLandingPage> Lessons { get; set; }
        public IEnumerable<CourseCommentsLandingPage> Comments { get; set; }

        public int TotalComments { get; set; }

        public CourseLandingPage()
        {
            Lessons = new List<CourseLessonsLandingPage>();
            Comments = new List<CourseCommentsLandingPage>();
        }
    }

    public class CourseLessonsLandingPage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Video { get; set; }
    }

    public class CourseCommentsLandingPage
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Comment { get; set; }
        public decimal Vote { get; set; }
        public DateTime? Date { get; set; }
        public string DateForView {
            get
            {
                return Date.UseMyCustomFormat();
            }
        }
    }
}
