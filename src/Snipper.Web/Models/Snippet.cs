using System;
using System.Collections.Generic;

namespace Snipper.Web.Models
{
    public class Snippet
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Category { get; set; }
        public string Language { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string UserName { get; set; }

        public List<SnippetFile> Files { get; set; } = new List<SnippetFile>();

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }

    public class SnippetFile
    {
        public Guid Id { get; set; }

        public int Order { get; set; }

        public string Language { get; set; }

        public string FileName { get; set; }

        public string Content { get; set; }
    }

    public class SnippetFileRecord
    {
        public Guid SnippetId { get; set; } = Guid.NewGuid();

        public string Category { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public Guid Id { get; set; } = Guid.NewGuid();

        public int Order { get; set; }

        public string Language { get; set; }

        public string FileName { get; set; }

        public string Content { get; set; }
    }
}
