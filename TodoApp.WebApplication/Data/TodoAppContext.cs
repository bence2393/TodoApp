using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public DbSet<TodoApp.WebApplication.Models.TodoItem> TodoItem { get; set; }
    }
}
