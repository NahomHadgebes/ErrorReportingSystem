// ErrorReportingSystem.Api/Seed/DbSeeder.cs
using ErrorReportingSystem.Domain.Entities;
using ErrorReportingSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ErrorReportingSystem.Api.Seed
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            var now = DateTime.UtcNow;

            // Seeda ErrorReports om de inte finns
            if (!db.ErrorReports.Any())
            {
                var seed = new List<ErrorReport>
        {
            new() { Id = Guid.NewGuid(), Title = "Printer not responding", Description = "...", Priority = "High", Status = "Open", CreatedByUsername = "Alice Johnson", CreatedAt = now.AddDays(-12) },
            new() { Id = Guid.NewGuid(), Title = "Login page error", Description = "...", Priority = "Critical", Status = "New", CreatedByUsername = "Bob Smith", CreatedAt = now.AddDays(-9) }
        };

                db.ErrorReports.AddRange(seed);
                db.SaveChanges();
            }

            // Seeda Comments om de inte finns
            if (!db.Comments.Any())
            {
                var reports = db.ErrorReports.ToList(); // Nuvarande data i DB
                if (reports.Count >= 2)
                {
                    var commentSeed = new List<Comment>
            {
                new() { Id = Guid.NewGuid(), ErrorReportId = reports[0].Id, Content = "We're looking into it.", CreatedByUsername = "Admin", CreatedAt = now.AddHours(-5) },
                new() { Id = Guid.NewGuid(), ErrorReportId = reports[1].Id, Content = "Still getting 500 error.", CreatedByUsername = "Bob Smith", CreatedAt = now.AddHours(-2) }
            };

                    db.Comments.AddRange(commentSeed);
                    db.SaveChanges();
                }
            }
        }
    }
}


