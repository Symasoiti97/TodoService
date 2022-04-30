using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    /// <summary>
    /// Api for managing TodoItems
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all TodoItems
        /// </summary>
        /// <response code="200">TodoItems</response>
        [ProducesResponseType(typeof(IEnumerable<TodoItemDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems()
        {
            return await _context.TodoItems
                .Select(x => ItemToDto(x))
                .ToListAsync();
        }

        /// <summary>
        /// Gets a specific TodoItem
        /// </summary>
        /// <param name="id" example="1"></param>
        /// <response code="200">TodoItem</response>
        /// <response code="404">Not found</response>
        [ProducesResponseType(typeof(TodoItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem([FromRoute] long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDto(todoItem);
        }

        /// <summary>
        /// Updates a specific TodoItem
        /// </summary>
        /// <param name="id" example="1">A specific TodoItemId</param>
        /// <param name="todoItemDto">A specific TodoItem for updating</param>
        /// <response code="204">TodoItem updated</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem([FromRoute] long id, [FromBody, Required] TodoItemDto todoItemDto)
        {
            if (id != todoItemDto.Id)
            {
                return BadRequest();
            }

            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = todoItemDto.Name;
            todoItem.IsComplete = todoItemDto.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a TodoItem
        /// </summary>
        /// <param name="todoItemDto">A specific TodoItem for creating</param>
        /// <response code="200">TodoItem created</response>
        [ProducesResponseType(typeof(TodoItemDto), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> CreateTodoItem([FromBody, Required] TodoItemDto todoItemDto)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDto.IsComplete,
                Name = todoItemDto.Name
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new {id = todoItem.Id}, ItemToDto(todoItem));
        }

        /// <summary>
        /// Deletes a specific TodoItem
        /// </summary>
        /// <param name="id" example="1">A specific TodoItemId</param>
        /// <response code="204">TodoItem deleted</response>
        /// <response code="404">Not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem([FromRoute] long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id) => _context.TodoItems.Any(e => e.Id == id);

        private static TodoItemDto ItemToDto(TodoItem todoItem) =>
            new TodoItemDto
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}