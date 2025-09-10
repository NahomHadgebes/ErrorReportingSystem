using Microsoft.EntityFrameworkCore;
using ErrorReportingSystem.Domain.Entities;

namespace ErrorReportingSystem.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<ErrorReport> ErrorReports => Set<ErrorReport>();
    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.ErrorReport)
            .WithMany(r => r.Comments)
            .HasForeignKey(c => c.ErrorReportId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}



