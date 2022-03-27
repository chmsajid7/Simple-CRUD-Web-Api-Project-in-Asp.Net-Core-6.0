using Microsoft.EntityFrameworkCore;
using TodoWebApi.Models;

namespace TodoWebApi.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> dbContext)
            : base(dbContext)
        { }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
