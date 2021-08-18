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

            //if (id == null)
            //{
            //    return NotFound();
            //}

            //TodoItem = await _context.TodoItem.FirstOrDefaultAsync(m => m.Id == id);

            //if (TodoItem == null)
            //{
            //    return NotFound();
            //}
            //return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var studentToUpdate = await _context.TodoItems.FindAsync(id);

            if (studentToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<TodoItem>(
                studentToUpdate,
                "todoItem",
                t => t.Message, t => t.IsDone))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            //_context.Attach(TodoItem).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!TodoItemExists(TodoItem.Id))
            //    {
            //        return NotFound();
            //    }

            //    throw;
            //}

            //return RedirectToPage("./Index");
        }

        private bool TodoItemExists(int id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
