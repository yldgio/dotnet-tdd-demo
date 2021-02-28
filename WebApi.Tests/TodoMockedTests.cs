using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using Moq.EntityFrameworkCore;
using Xunit;
using WebApi.Models;
using WebApi.Reposotories;
using Moq;

namespace WebApi.Tests
{
    public class TodoMockedTests
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public void OnlyCompletedItemsWith_CompletedTodoItems()
        {
            var items = GenerateTodoItems();
            var expected = items.Where(i => i.IsComplete == true);
            var contextMock = new Mock<TodoContext>();
            contextMock.Setup(x => x.TodoItems).ReturnsDbSet(items);

            var repo = new TodoRepository(contextMock.Object);

            // Act
            var result = repo.CompletedTodoItems();

            // Assert
            Assert.Equal(expected, result);
        }
        private static IList<TodoItem> GenerateTodoItems()
        {
            IList<TodoItem> items = new List<TodoItem>
            {
                Fixture.Build<TodoItem>().With(u => u.IsComplete, true).Create(),
                Fixture.Build<TodoItem>().With(u => u.IsComplete, false).Create(),
                Fixture.Build<TodoItem>().With(u => u.IsComplete, false).Create(),
                Fixture.Build<TodoItem>().With(u => u.IsComplete, true).Create()
            };

            return items;
        }
    }
}
