namespace CleanDone.Api.Dtos;

public class CreateTaskRequest
{
    public string Title { get; set; } = "";
    public string Room { get; set; } = "";
    public string Frequency { get; set; } = "Daily";
    public DateTime DueDate { get; set; }
    public int EstimatedMinutes { get; set; } = 10;
    public int Priority { get; set; } = 1;
}
