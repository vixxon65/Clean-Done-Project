using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanDone.Api.Models;

[Table("task_history")]
public class TaskHistory
{
    [Key]
    [Column("history_id")]
    public int HistoryId { get; set; }

    [Column("task_id")]
    public int TaskId { get; set; }

    [Column("completed_at")]
    public DateTime CompletedAt { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }
}
