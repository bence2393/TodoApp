using System.Collections.Generic;
using TodoApp.WebApplication.Data;
using TodoApp.WebApplication.Models;

namespace TodoApp.Integration.Tests.Helpers
{
    public static class Utilities
    {
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
                    IsDone = false,
                    Message = "Task 1"
                },
                new()
                {
                    IsDone = true,
                    Message = "Task 2"
                },
                new()
                {
                    IsDone = false,
                    Message = "Task 3"
                },
                new()
                {
                    IsDone = false,
                    Message = "Task 4"
                }
            };
        }
    }
}
