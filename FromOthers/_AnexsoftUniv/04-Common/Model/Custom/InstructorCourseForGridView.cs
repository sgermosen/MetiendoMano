namespace Model.Custom
{
    public class InstructorCourseForGridView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPerMonth { get; set; }
        public int Students { get; set; }
    }
}
