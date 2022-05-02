using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BusinessLayer.Dto;
using BusinessLayer.Entities;

namespace BusinessLayer
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItemDto>> GetAllItemsAsync(CancellationToken cancellationToken);
        Task<TodoItemDto> GetTodoItemDtoAsync(long id, CancellationToken cancellationToken);
        Task<TodoItem> FindTodoItemAsync(long id, CancellationToken cancellationToken);
        Task CreateAsync(TodoItem todoItem, CancellationToken cancellationToken);
        Task UpdateAsync(TodoItem todoItem, CancellationToken cancellationToken);
        Task DeleteAsync(TodoItem todoItem, CancellationToken cancellationToken);
    }
}