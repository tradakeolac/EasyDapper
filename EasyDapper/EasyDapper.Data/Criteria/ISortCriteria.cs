namespace EasyDapper.Data.Criteria
{
    public interface ISortCriteria
    {
        bool Desc { get; set; }
        string PropertyName { get; set; }
    }
}