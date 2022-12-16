using Microsoft.EntityFrameworkCore;

namespace ExamClient.Models
{
    public class DB_API_Context : DbContext
    {
        protected DB_API_Context()
        {
        }

        public DB_API_Context(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Case> Cases { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
    }
}
