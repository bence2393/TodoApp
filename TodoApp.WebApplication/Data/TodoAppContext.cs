using Microsoft.EntityFrameworkCore;
using TodoApp.WebApplication.Models;

namespace TodoApp.WebApplication.Data
{
    public class TodoAppContext : DbContext
    {
        public TodoAppContext (DbContextOptions<TodoAppContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItem { get; set; }
    }
}
