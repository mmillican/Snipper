using System;
using System.Collections.Generic;

namespace Snipper.Web.Models
{
    public class Snippet
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Category { get; set; }
        public string Language { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string UserName { get; set; }

        public List<SnippetFile> Files { get; set; } = new List<SnippetFile>();

        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }

    public class SnippetFile
    {
        public int Order { get; set; }

        public string FileName { get; set; }

        public string Content { get; set; }
    }
}