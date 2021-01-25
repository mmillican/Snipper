using System;
using System.Collections.Generic;

namespace Snipper.Web.Models
{
    public class SnippetModel
    {
        public Guid Id { get; set; }

        public string Category { get; set; }
        public string Language { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string UserName { get; set; }

        public List<SnippetFileModel> Files { get; set; } = new List<SnippetFileModel>();

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }

    public class SnippetFileModel
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
