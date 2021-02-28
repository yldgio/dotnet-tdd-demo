using System;
//using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Relational;
namespace WebApi.Reposotories
{
    public class TodoRepository
    {
        private TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }

        public void AddTodoItem(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();
        }

        public IEnumerable<TodoItem> AllTodoItems()
        {
            return _context.TodoItems;
        }
        public IEnumerable<TodoItem> CompletedTodoItems()
        {
            return _context.TodoItems.Where(i => i.IsComplete).ToList();
        }
        public IEnumerable<TodoItem> InCompleteTodoItems()
        {
            return _context.TodoItems.Where(i=>!i.IsComplete).ToList();
        }

        public void DeleteTodoItem(TodoItem item)
        {
            _context.TodoItems.Remove(item);
            _context.SaveChanges();
        }

        public void DeleteAllTodoItems()
        {
            _context.RemoveRange(_context.TodoItems); //.Database.ExecuteSqlRaw("delete from TodoItems where IsComplete = 'true'");
            _context.SaveChanges();
        }

        public void DeleteCompletedTodoItems()
        {
            _context.RemoveRange(_context.TodoItems.Where(i => i.IsComplete == true)); //.Database.ExecuteSqlRaw("delete from TodoItems where IsComplete = 'true'");
            _context.SaveChanges();
        }
    }
}
