using Common;

namespace Model.Custom
{
    public class CourseForGridView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Instructor { get; set; }
        public int Students { get; set; }
        public int Lessons { get; set; }
        public Enums.Status Status { get; set; }
    }
}
