using Data.Common;

namespace Data.Entities
{
    public class Producer : BaseEntity
    {
        // Relationship with Series
        public ICollection<Series> Series { get; set; }
    }
}
