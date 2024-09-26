using Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Series : BaseEntity
    {
        [Required] public string CoverImage { get; set; }
        [Required] public string LinkVideo { get; set; }

        // Relations
        [Required] public int ProducerId { get; set; }
        public Producer Producer { get; set; }
        [Required] public int GenderPrimaryId { get; set; }
        public Gender GenderPrimary { get; set; }
        public int? GenderSecondaryId { get; set; }
        public Gender GenderSecondary { get; set; }
    }
}