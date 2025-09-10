namespace ErrorReportingSystem.Application.DTOs
{
    public class CreateCommentRequest
    {
        public Guid ErrorReportId { get; set; }   // Den rapport kommentaren tillhör
        public string Content { get; set; } = string.Empty;
        public string CreatedByUsername { get; set; } = string.Empty;
    }
}

