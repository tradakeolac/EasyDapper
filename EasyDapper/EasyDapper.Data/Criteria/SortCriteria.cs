namespace EasyDapper.Data.Criteria
{
    public class SortCriteria : ISortCriteria
    {
        public string PropertyName { get; set; }
        public bool Desc { get; set; }
    }
}
