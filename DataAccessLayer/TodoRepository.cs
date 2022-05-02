using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLayer;
using BusinessLayer.Dto;
using BusinessLayer.Entities;
using BusinessLayer.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    internal class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _todoContext;
        private readonly IMapper _mapper;

        public TodoRepository(TodoContext todoContext, IMapper mapper)
        {
            _todoContext = todoContext ?? throw new ArgumentNullException(nameof(todoContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<TodoItemDto>> GetAllItemsAsync(CancellationToken cancellationToken)
        {
            return await _todoContext.Set<TodoItem>().AsNoTracking().ProjectTo<TodoItemDto>(_mapper.ConfigurationProvider).ToArrayAsync(cancellationToken);
        }

        public async Task<TodoItemDto> GetTodoItemDtoAsync(long id, CancellationToken cancellationToken)
        {
            return await _todoContext.Set<TodoItem>().AsNoTracking().ProjectTo<TodoItemDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<TodoItem> FindTodoItemAsync(long id, CancellationToken cancellationToken)
        {
            return await _todoContext.FindAsync<TodoItem>(new object[] {id}, cancellationToken);
        }

        public async Task CreateAsync(TodoItem todoItem, CancellationToken cancellationToken)
        {
            _todoContext.Add(todoItem);
            await _todoContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(TodoItem todoItem, CancellationToken cancellationToken)
        {
            _todoContext.Update(todoItem);
            try
            {
                await _todoContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _todoContext.Set<TodoItem>().AnyAsync(e => e.Id == todoItem.Id, cancellationToken) == false)
                {
                    throw new TodoItemNotFoundException();
                }

                throw;
            }
        }

        public async Task DeleteAsync(TodoItem todoItem, CancellationToken cancellationToken)
        {
            _todoContext.Remove(todoItem);
            await _todoContext.SaveChangesAsync(cancellationToken);
        }
    }
}