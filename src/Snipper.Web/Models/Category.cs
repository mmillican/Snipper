using System.ComponentModel.DataAnnotations;

namespace Snipper.Web.Models
{
    public class CategoryModel
    {
        [MaxLength(50)]
        public string Slug { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
    }
}
