namespace ErrorReportingSystem.Application.DTOs
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public Guid ErrorReportId { get; set; }
        public string Content { get; set; } = string.Empty;
        public string CreatedByUsername { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}

