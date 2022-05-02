using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BusinessLayer.Dto;

namespace BusinessLayer
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItemDto>> GetTodoDtoItemsAsync(CancellationToken cancellationToken);
        Task<TodoItemDto> GetTodoItemDtoAsync(long id, CancellationToken CancellationToken);
        Task UpdateTodoItemAsync(long id, UpdateTodoItemDto todoItemDto, CancellationToken cancellationToken);
        Task<TodoItemDto> CreateTodoItemAsync(CreateTodoItemDto todoItemDto, CancellationToken cancellationToken);
        Task DeleteTodoItemAsync(long id, CancellationToken cancellationToken);
    }
}