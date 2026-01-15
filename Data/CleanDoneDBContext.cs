using CleanDone.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanDone.Api.Data;

public class CleanDoneDbContext : DbContext
{
    public CleanDoneDbContext(DbContextOptions<CleanDoneDbContext> options) : base(options) { }

    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<TaskHistory> TaskHistory => Set<TaskHistory>();
}
