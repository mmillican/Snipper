using System;

namespace Snipper.Web.Models
{
    public class SnippetSearchModel
    {
        public string Category { get; set; }
        public Guid Id { get; set; }
        public Guid SnippetId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }

        public string Language { get; set; }

        public string Content { get; set; }
    }
}
