using CleanDone.Api.Data;
using CleanDone.Api.Dtos;
using CleanDone.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CleanDone.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly CleanDoneDbContext _db;

    public TasksController(CleanDoneDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _db.Tasks
            .Where(t => t.IsActive)
            .OrderBy(t => t.DueDate)
            .ThenBy(t => t.Priority)
            .ToListAsync();

        return Ok(tasks);
    }

    [HttpGet("today")]
    public async Task<IActionResult> GetToday()
    {
        var today = DateTime.Today;

        var tasks = await _db.Tasks
            .Where(t => t.IsActive && t.DueDate == today)
            .OrderBy(t => t.Priority)
            .ToListAsync();

        return Ok(tasks);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return BadRequest("Title is required.");

        var task = new TaskItem
        {
            Title = request.Title.Trim(),
            Room = request.Room.Trim(),
            Frequency = request.Frequency,
            DueDate = request.DueDate.Date,
            EstimatedMinutes = request.EstimatedMinutes,
            Priority = request.Priority,
            IsActive = true
        };

        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { id = task.TaskId }, task);
    }

    [HttpPost("{id}/complete")]
    public async Task<IActionResult> Complete(int id)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task == null) return NotFound();

        // save history
        _db.TaskHistory.Add(new TaskHistory
        {
            TaskId = task.TaskId,
            CompletedAt = DateTime.Now
        });

        // recurrence logic
        if (task.Frequency == "OneTime")
        {
            task.IsActive = false;
        }
        else
        {
            task.DueDate = task.Frequency switch
            {
                "Daily" => task.DueDate.AddDays(1),
                "Weekly" => task.DueDate.AddDays(7),
                "Monthly" => task.DueDate.AddMonths(1),
                _ => task.DueDate
            };
        }

        await _db.SaveChangesAsync();
        return Ok(task);
    }
}