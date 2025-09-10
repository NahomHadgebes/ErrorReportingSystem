namespace ErrorReportingSystem.Domain.Entities;

public class ErrorReport
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Priority { get; set; } = "Medium"; // Low, Medium, High
    public string Status { get; set; } = "New"; // New, InProgress, Resolved
    public string CreatedByUsername { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

}

