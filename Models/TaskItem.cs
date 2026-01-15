using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanDone.Api.Models;

[Table("tasks")]
public class TaskItem
{
    [Key]
    [Column("task_id")]
    public int TaskId { get; set; }

    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Column("room")]
    public string Room { get; set; } = string.Empty;

    [Column("frequency")]
    public string Frequency { get; set; } = "OneTime"; // OneTime, Daily, Weekly, Monthly

    [Column("due_date")]
    public DateTime DueDate { get; set; }

    [Column("estimated_minutes")]
    public int EstimatedMinutes { get; set; } = 15;

    [Column("priority")]
    public int Priority { get; set; } = 2;

    [Column("is_active")]
    public bool IsActive { get; set; } = true;
}
