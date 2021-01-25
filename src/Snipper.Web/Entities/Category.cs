using System.ComponentModel.DataAnnotations;

namespace Snipper.Web.Entities
{
    public class Category
    {
        [Key]
        [Required, MaxLength(50)]
        public string Slug { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
    }
}
