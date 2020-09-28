using Common;

namespace Model.Custom
{
    public class CourseCard
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
    }
}
