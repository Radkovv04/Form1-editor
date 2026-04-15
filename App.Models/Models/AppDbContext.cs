using Microsoft.EntityFrameworkCore;

namespace App.Models.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<NoteRevision> NoteRevisions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public AppDbContext()
        {
            this.Database.EnsureCreated();
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (DbConfig.UseSQLite)
                    optionsBuilder.UseSqlite(DbConfig.SQLitePath);
                else
                    optionsBuilder.UseSqlServer(DbConfig.SqlServerPath);
            }
        }

    
    }
}