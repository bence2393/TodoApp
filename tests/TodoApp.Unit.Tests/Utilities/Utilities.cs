using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.WebApplication.Data;
using TodoApp.WebApplication.Models;


namespace TodoApp.Unit.Tests.Utilities
{
    public static class Utilities
    {
        public static DbContextOptions<TodoAppContext> TestDbContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<TodoAppContext>()
                .UseInMemoryDatabase("InMemoryDb")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        public static void InitializeDbForTests(TodoAppContext db)
        {
            db.TodoItems.AddRange(GetSeedingItems());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(TodoAppContext db)
        {
            db.TodoItems.RemoveRange(db.TodoItems);
            InitializeDbForTests(db);
        }

        public static List<TodoItem> GetSeedingItems()
        {
            return new List<TodoItem>()
            {
                new()
                {
                    IsDone = true,
                    Message = "TodoApp"
                },
                new()
                {
                    IsDone = false,
                    Message = "Nice UI"
                },
                new()
                {
                    IsDone = false,
                    Message = "Unit tests"
                },
                new()
                {
                    IsDone = false,
                    Message = "Integration tests"
                }
            };
        }
    }
}
