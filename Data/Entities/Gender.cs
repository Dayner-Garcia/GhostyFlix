using Data.Common;

namespace Data.Entities
{
    public class Gender : BaseEntity
    {
        // Relationships with Series
        public ICollection<Series> SeriesAsPrimary { get; set; }
        public ICollection<Series> SeriesAsSecondary { get; set; }
    }
}
