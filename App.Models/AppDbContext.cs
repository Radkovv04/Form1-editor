using Microsoft.EntityFrameworkCore;

namespace App.Models
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>()
                .HasMany(n => n.Revisions)
                .WithOne()
                .HasForeignKey(r => r.NoteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}