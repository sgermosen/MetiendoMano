namespace Model.Custom
{
    public class CoursePerStudentListView
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Progress { get; set; }
        public bool HaveVotes { get; set; }
    }
}
