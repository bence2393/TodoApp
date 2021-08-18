using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TodoApp.WebApplication.Data;
using TodoApp.WebApplication.Models;

namespace TodoApp.WebApplication.Pages.TodoItems
{
    public class IndexModel : PageModel
    {
        private readonly TodoApp.WebApplication.Data.TodoAppContext _context;

        public IndexModel(TodoApp.WebApplication.Data.TodoAppContext context)
        {
            _context = context;
        }

        public IList<TodoItem> TodoItem { get;set; }

        public async Task OnGetAsync()
        {
            TodoItem = await _context.TodoItem.ToListAsync();
        }
    }
}
