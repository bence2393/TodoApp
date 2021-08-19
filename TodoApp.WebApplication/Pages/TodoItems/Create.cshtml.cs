using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoApp.WebApplication.Data;
using TodoApp.WebApplication.Models;

namespace TodoApp.WebApplication.Pages.TodoItems
{
    public class CreateModel : PageModel
    {
        private readonly TodoAppContext _context;

        public CreateModel(TodoAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TodoItem TodoItem { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyTodoItem = new TodoItem();

            if (await TryUpdateModelAsync(
                emptyTodoItem,
                "todoItem",
                t => t.Message, t => t.IsDone))
            {
                _context.TodoItems.Add(emptyTodoItem);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
