using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Tests
{
    public class TodoDatabaseFixture : IDisposable
    {
        public TodoDatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase("TEST_IN_MEMORY_DATABASE")
            .Options;
            DbContext = new TodoContext(options);
            DbContext.Database.EnsureCreatedAsync();
            PrepareTestDatabase();

            // ... initialize data in the test database ...
        }

        protected void PrepareTestDatabase()
        {
            // Seed my db DbContext;
        }
        public void CleanUp()
        {
            DbContext.RemoveRange(DbContext.TodoItems);
            DbContext.SaveChanges();
        }
        public void Dispose()
        {
            CleanupTestDatabase();
        }

        protected void CleanupTestDatabase()
        {
            DbContext.Database.EnsureDeleted(); 
        }

        public TodoContext DbContext { get; private set; }
    }
}
