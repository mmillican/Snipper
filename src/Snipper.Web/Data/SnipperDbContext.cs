using Microsoft.EntityFrameworkCore;
using Snipper.Web.Entities;

namespace Snipper.Data
{
    public class SnipperDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Snippet> Snippets { get; set; }
        public DbSet<SnippetFile> SnippetFiles { get; set; }

        public SnipperDbContext(DbContextOptions<SnipperDbContext> options)
            : base(options)
        {

        }
    }
}
