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
using WebApi.Controllers;
using Microsoft.Extensions.Logging;

namespace WebApi.Tests
{
    public class TodoItemsControllerTests
    {
        private static readonly Fixture Fixture = new Fixture();

        [Fact]
        public void controllershouldbetestable()
        {
            var items = GenerateTodoItems();
            var expected = items.Where(i => i.IsComplete == true);
            var contextMock = new Mock<TodoContext>();
            contextMock.Setup(x => x.TodoItems).ReturnsDbSet(items);
            var controller = new TodoItemsController(
                new Mock<ILogger<TodoItemsController>>().Object,
                contextMock.Object);


            // Act
            var result = controller.GetCompleted();

            // Assert
            Assert.Equal(expected.ToArray(), result);
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
