using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApi.Models;
using WebApi.Reposotories;
using Xunit;
using System.Collections.Generic;
using System.Collections;
namespace WebApi.Tests
{
    public class TodoRepositoryTests : IClassFixture<TodoDatabaseFixture>
    {
        TodoDatabaseFixture fixture;

        public TodoRepositoryTests(TodoDatabaseFixture fixture)
        {
            this.fixture = fixture;
        }
        [Fact]
        public void CanAddTodoItem()
        {
            this.fixture.CleanUp();
            var todoService = new TodoRepository(fixture.DbContext);
            var initialTodos = todoService.AllTodoItems().ToList();
            Assert.Empty(initialTodos);

            todoService.AddTodoItem(new TodoItem { Name = "Item 1", IsComplete = false });
            var todos = todoService.AllTodoItems().ToList();
            Assert.Single(todos);
        }
        [Fact]
        public void CanDeleteFinishedTodos()
        {
            this.fixture.CleanUp();
            var todoService = new TodoRepository(fixture.DbContext);

            todoService.AddTodoItem(new TodoItem { Name = "Item 1", IsComplete = false });
            todoService.AddTodoItem(new TodoItem { Name = "Item 2", IsComplete = true });
            todoService.AddTodoItem(new TodoItem { Name = "Item 3", IsComplete = true });
            todoService.AddTodoItem(new TodoItem { Name = "Item 4", IsComplete = false });

            var todos = todoService.AllTodoItems().ToList();
            Assert.Equal(4, todos.Count);

            todoService.DeleteCompletedTodoItems();

            todos = todoService.AllTodoItems().ToList();
            Assert.Equal(2, todos.Count);
        }
    }
}
