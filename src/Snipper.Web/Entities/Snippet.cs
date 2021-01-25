using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Snipper.Web.Entities
{
    public class Snippet
    {
        public Guid Id { get; set; }

        public string CategorySlug { get; set; }
        [ForeignKey(nameof(CategorySlug))]
        public virtual Category Category { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }

        public virtual List<SnippetFile> Files { get; set; } = new List<SnippetFile>();

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
