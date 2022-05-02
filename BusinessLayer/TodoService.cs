using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BusinessLayer.Dto;
using BusinessLayer.Entities;
using BusinessLayer.Exceptions;

namespace BusinessLayer
{
    internal class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        }

        public async Task<IEnumerable<TodoItemDto>> GetTodoDtoItemsAsync(CancellationToken cancellationToken)
        {
            return await _todoRepository.GetAllItemsAsync(cancellationToken);
        }

        public async Task<TodoItemDto> GetTodoItemDtoAsync(long id, CancellationToken cancellationToken)
        {
            return await _todoRepository.GetTodoItemDtoAsync(id, cancellationToken);
        }

        public async Task<TodoItemDto> CreateTodoItemAsync(CreateTodoItemDto todoItemDto, CancellationToken cancellationToken)
        {
            var todoItem = new TodoItem(todoItemDto.Name, todoItemDto.IsComplete);

            await _todoRepository.CreateAsync(todoItem, cancellationToken);

            return await _todoRepository.GetTodoItemDtoAsync(todoItem.Id, cancellationToken);
        }

        public async Task UpdateTodoItemAsync(long id, UpdateTodoItemDto todoItemDto, CancellationToken cancellationToken)
        {
            var todoItem = await _todoRepository.FindTodoItemAsync(id, cancellationToken);

            if (todoItem == null)
                throw new TodoItemNotFoundException();

            todoItem.SetName(todoItemDto.Name);
            todoItem.SetStatusComplete(todoItemDto.IsComplete);

            await _todoRepository.UpdateAsync(todoItem, cancellationToken);
        }

        public async Task DeleteTodoItemAsync(long id, CancellationToken cancellationToken)
        {
            var todoItem = await _todoRepository.FindTodoItemAsync(id, cancellationToken);

            if (todoItem == null)
                throw new TodoItemNotFoundException();

            await _todoRepository.DeleteAsync(todoItem, cancellationToken);
        }
    }
}