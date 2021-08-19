using System;
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
        public IList<TodoItem> TodoItems { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TodoItemIsDone { get; set; }

        public SelectList IsDoneSelectList { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public SelectList PageSizeSelectList { get; set; }

        [BindProperty(SupportsGet = true)] 
        public int PageSize { get; set; } = 10;

        public int Count { get; set; }

        public bool ShowPrevious => CurrentPage > 1;

        public bool ShowNext => CurrentPage < TotalPages;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

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
            var todoItemsQueryable = from t in _context.TodoItems select t;

            IsDoneSelectList = new SelectList(await isDoneQueryable.Distinct().ToListAsync());
            PageSizeSelectList = new SelectList(new[] { 1, 5, 10, 15, 20, 25 });

            todoItemsQueryable = FilterTodoItemsQueryable(todoItemsQueryable);
            var todoItemsList = await todoItemsQueryable.ToListAsync();
            Count = todoItemsList.Count;

            TodoItems = GetPaginatedResult(todoItemsList, CurrentPage, PageSize);
        }

        private IQueryable<TodoItem> FilterTodoItemsQueryable(IQueryable<TodoItem> todoItemsQueryable)
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                todoItemsQueryable = todoItemsQueryable.Where(t => t.Message.Contains(SearchString));
            }

            if (TodoItemIsDone != null)
            {
                todoItemsQueryable = todoItemsQueryable.Where(t => t.IsDone == TodoItemIsDone.Equals("True"));
            }

            return todoItemsQueryable;
        }

        private static List<TodoItem> GetPaginatedResult(IEnumerable<TodoItem> todoItems, int currentPage, int pageSize = 10)
        {
            return todoItems.OrderBy(t => t.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}