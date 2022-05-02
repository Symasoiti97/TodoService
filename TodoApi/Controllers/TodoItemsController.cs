using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using BusinessLayer;
using BusinessLayer.Dto;
using BusinessLayer.Exceptions;
using Microsoft.AspNetCore.Http;

namespace TodoApi.Controllers
{
    /// <summary>
    /// Api for managing TodoItems
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoItemsController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        /// <summary>
        /// Get all TodoItems
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <response code="200">TodoItems</response>
        [ProducesResponseType(typeof(IEnumerable<TodoItemDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems(CancellationToken cancellationToken)
        {
            return Ok(await _todoService.GetTodoDtoItemsAsync(cancellationToken));
        }

        /// <summary>
        /// Gets a specific TodoItem
        /// </summary>
        /// <param name="id" example="1"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">TodoItem</response>
        /// <response code="404">Not found</response>
        [ProducesResponseType(typeof(TodoItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem([FromRoute] long id, CancellationToken cancellationToken)
        {
            try
            {
                return await _todoService.GetTodoItemDtoAsync(id, cancellationToken);
            }
            catch (TodoItemNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Updates a specific TodoItem
        /// </summary>
        /// <param name="id" example="1">A specific TodoItemId</param>
        /// <param name="updateTodoItemDto">A specific TodoItem for updating</param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">TodoItem updated</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem([FromRoute] long id, [FromBody] [Required] UpdateTodoItemDto updateTodoItemDto,
            CancellationToken cancellationToken)
        {
            try
            {
                await _todoService.UpdateTodoItemAsync(id, updateTodoItemDto, cancellationToken);
                return NoContent();
            }
            catch (TodoItemNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Creates a TodoItem
        /// </summary>
        /// <param name="todoItemDto">A specific TodoItem for creating</param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">TodoItem created</response>
        [ProducesResponseType(typeof(TodoItemDto), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> CreateTodoItem([FromBody] [Required] CreateTodoItemDto todoItemDto, CancellationToken cancellationToken)
        {
            return await _todoService.CreateTodoItemAsync(todoItemDto, cancellationToken);
        }

        /// <summary>
        /// Deletes a specific TodoItem
        /// </summary>
        /// <param name="id" example="1">A specific TodoItemId</param>
        /// <param name="cancellationToken"></param>
        /// <response code="204">TodoItem deleted</response>
        /// <response code="404">Not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem([FromRoute] long id, CancellationToken cancellationToken)
        {
            try
            {
                await _todoService.DeleteTodoItemAsync(id, cancellationToken);
                return NoContent();
            }
            catch (TodoItemNotFoundException)
            {
                return NotFound();
            }
        }
    }
}