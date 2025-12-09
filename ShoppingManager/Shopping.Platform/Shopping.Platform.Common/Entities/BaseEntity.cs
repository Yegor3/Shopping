namespace Shopping.Platform.Common.Entities
{
    public class BaseEntity
    {
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime? DeletionDate { get; set; }
    }
}