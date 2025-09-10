using System;

namespace ErrorReportingSystem.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid ErrorReportId { get; set; }

        public string Content { get; set; } = string.Empty;
        public string CreatedByUsername { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        // Relation (valfritt)
        public ErrorReport? ErrorReport { get; set; }
    }
}

