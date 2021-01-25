using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Snipper.Web.Entities
{
    public class SnippetFile
    {
        public Guid Id { get; set; }

        public Guid SnippetId { get; set; }
        [ForeignKey(nameof(SnippetId))]
        public virtual Snippet Snippet { get; set; }

        public int Order { get; set; }

        [Required, MaxLength(50)]
        public string Language { get; set; }

        [Required, MaxLength(50)]
        public string FileName { get; set; }

        public string Content { get; set; }
    }
}
