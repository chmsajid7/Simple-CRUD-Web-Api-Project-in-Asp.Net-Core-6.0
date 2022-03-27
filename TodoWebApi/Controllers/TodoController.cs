using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoWebApi.Data;
using TodoWebApi.Models;
using TodoWebApi.Models.ViewModels;

namespace TodoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext dbContext;
        public TodoController(TodoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetTodoItems()
        {
            var todoItems = this.dbContext.TodoItems.ToList();
            return Ok(todoItems);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetTodoItem(Guid id)
        {
            var todoItem = this.dbContext.TodoItems
                .Where(x => x.Id.Equals(id))
                .FirstOrDefault();
            return Ok(todoItem);
        }

        [HttpPost]
        public IActionResult CreateTodoItem(CreateTodoItem createTodoItem)
        {
            this.dbContext.TodoItems
                .Add(new TodoItem
                {
                    Title = createTodoItem.Title,
                    Description = createTodoItem.Description
                });
            var result = this.dbContext.SaveChanges();
            return Created("", result);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateTodoItem(Guid id, CreateTodoItem createTodoItem)
        {
            var todoItem = this.dbContext.TodoItems.FirstOrDefault(x => x.Id.Equals(id));
            if (todoItem == null)
            {
                return NoContent();
            }

            todoItem.Title = createTodoItem.Title;
            todoItem.Description = createTodoItem.Description;

            this.dbContext.TodoItems.Update(todoItem);
            var result = this.dbContext.SaveChanges();
            return Ok(result);
        }
        
        [HttpPatch]
        [Route("{id}")]
        public IActionResult PatchTodoItem(Guid id, string description)
        {
            var todoItem = this.dbContext.TodoItems.FirstOrDefault(x => x.Id.Equals(id));
            if (todoItem == null)
            {
                return NoContent();
            }

            todoItem.Description = description;

            this.dbContext.TodoItems.Update(todoItem);
            var result = this.dbContext.SaveChanges();
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteTodoItem(Guid id)
        {
            var todoItem = this.dbContext.TodoItems.FirstOrDefault(x => x.Id.Equals(id));
            if (todoItem == null)
            {
                return NoContent();
            }

            this.dbContext.TodoItems.Remove(todoItem);
            var result = this.dbContext.SaveChanges();
            return Ok(result);
        }
    }
}
