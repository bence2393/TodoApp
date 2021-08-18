using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TodoApp.WebApplication.Data;
using TodoApp.WebApplication.Models;

namespace TodoApp.WebApplication.Pages.TodoItems
{
    public class IndexModel : PageModel
    {
        private readonly TodoAppContext _context;

        public IndexModel(TodoAppContext context)
        {
            _context = context;
        }

        public IList<TodoItem> TodoItems { get;set; }

        public async Task OnGetAsync()
        {
            TodoItems = await _context.TodoItems.ToListAsync();
        }
    }
}
