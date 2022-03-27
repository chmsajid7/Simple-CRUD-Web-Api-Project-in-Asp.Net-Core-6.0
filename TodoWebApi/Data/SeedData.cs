using Microsoft.EntityFrameworkCore;
using TodoWebApi.Models;

namespace TodoWebApi.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TodoDbContext(serviceProvider.GetRequiredService<DbContextOptions<TodoDbContext>>()))
            {
                if (context.TodoItems.Any())
                {
                    return;
                }

                var todoItemsList = new List<TodoItem>()
                {
                    new TodoItem()
                    {
                        Id=Guid.NewGuid(),
                        Title="Task 1",
                        Description="Description 1"
                    },
                    new TodoItem()
                    {
                        Id=Guid.NewGuid(),
                        Title="Task 2",
                        Description="Description 2"
                    },
                };

                context.TodoItems.AddRange(todoItemsList);
                context.SaveChanges();
            }
        }
    }
}
