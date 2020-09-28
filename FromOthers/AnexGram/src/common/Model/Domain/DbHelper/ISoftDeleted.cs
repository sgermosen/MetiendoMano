namespace Model.Domain.DbHelper
{
    public interface ISoftDeleted
    {
        bool Deleted { get; set; }
    }
}
