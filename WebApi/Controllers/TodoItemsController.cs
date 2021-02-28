using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoItemsController : ControllerBase
    {

        private readonly ILogger<TodoItemsController> _logger;
        private readonly TodoContext _todoContext;
        public TodoItemsController(ILogger<TodoItemsController> logger, TodoContext todoContext)
        {
            _todoContext = todoContext;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetCompleted()
        {
            var items = _todoContext.TodoItems;
            foreach(var group in items.GroupBy(i => i.IsComplete))
            {
                if (group.Key)
                {

                }
                else
                {

                }
            }
            /**
             * do some complicated stuff
             * **/
            return items.Where(i => i.IsComplete == true).ToArray();
        }
    }
}
