namespace Model.Shared
{
    public class UserListFilter
    {
        public string Name { get; set; }
    }

    public class UserGetFilter
    {
        public string UserId { get; set; }
        public string SeoUrl { get; set; }
    }

    public class UserDto
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public string AboutUs { get; set; }
        public string Image { get; set; }

        public string SeoUrl { get; set; }
    }

    public class UserPartialDto
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string AboutUs { get; set; }

        public string Image { get; set; }
    }
}
