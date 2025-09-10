using System;
using System.ComponentModel.DataAnnotations;

namespace ErrorReportingSystem.Application.DTOs
{
    public class ErrorReportDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^(Low|Medium|High|Critical)$",
            ErrorMessage = "Priority must be one of: Low, Medium, High, Critical.")]
        public string Priority { get; set; } = "Medium";

        [Required]
        [RegularExpression("^(New|Open|InProgress|Resolved)$",
            ErrorMessage = "Status must be one of: New, Open, InProgress, Resolved.")]
        public string Status { get; set; } = "New";

        [Required]
        [StringLength(50)]
        public string CreatedByUsername { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }

    public class CreateErrorReportRequest
    {
        [Required, StringLength(100, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required, RegularExpression("^(Low|Medium|High|Critical)$")]
        public string Priority { get; set; } = "Medium";

        [Required, RegularExpression("^(New|Open|InProgress|Resolved)$")]
        public string Status { get; set; } = "New";

        [Required, StringLength(50)]
        public string CreatedByUsername { get; set; } = string.Empty;
    }
}



