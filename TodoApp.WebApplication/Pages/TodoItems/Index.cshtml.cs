using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoApp.WebApplication.Data;
using TodoApp.WebApplication.Models;

namespace TodoApp.WebApplication.Pages.TodoItems
{
    public class IndexModel : PageModel
    {
        public IList<TodoItem> TodoItems { get;set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TodoItemIsDone { get; set; }

        public SelectList IsDone { get; set; }

        private readonly TodoAppContext _context;

        public IndexModel(TodoAppContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            var isDoneQueryable = from t in _context.TodoItems
                orderby t.IsDone
                select t.IsDone;

            var todoItems = from t in _context.TodoItems select t;

            if (!string.IsNullOrEmpty(SearchString))
            {
                todoItems = todoItems.Where(t => t.Message.Contains(SearchString));
            }

            if (TodoItemIsDone != null)
            {
                todoItems = todoItems.Where(t => t.IsDone == TodoItemIsDone.Equals("True"));
            }
            IsDone = new SelectList(await isDoneQueryable.Distinct().ToListAsync());

            TodoItems = await todoItems.ToListAsync();
        }
    }
}