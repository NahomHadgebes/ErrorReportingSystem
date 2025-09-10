using System.ComponentModel.DataAnnotations;

namespace ErrorReportingSystem.Application.DTOs
{
    public class UpdateErrorReportRequest
    {
        [Required, StringLength(100, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required, RegularExpression("^(Low|Medium|High|Critical)$")]
        public string Priority { get; set; } = "Medium";

        [Required, RegularExpression("^(New|Open|InProgress|Resolved)$")]
        public string Status { get; set; } = "New";
        // Obs: vi uppdaterar INTE CreatedByUsername/CreatedAt här.
    }
}

