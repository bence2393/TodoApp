using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TodoApp.WebApplication.Data;
using TodoApp.WebApplication.Models;

namespace TodoApp.WebApplication.Pages.TodoItems
{
    public class EditModel : PageModel
    {
        private readonly TodoAppContext _context;

        public EditModel(TodoAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TodoItem TodoItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TodoItem = await _context.TodoItems.FindAsync(id);

            if (TodoItem == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var todoItemToUpdate = await _context.TodoItems.FindAsync(id);

            if (todoItemToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(
                todoItemToUpdate,
                "todoItem",
                t => t.Message, t => t.IsDone))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }

        private bool TodoItemExists(int id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
