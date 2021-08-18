using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.WebApplication.Data;

namespace TodoApp.WebApplication.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new TodoAppContext(serviceProvider.GetRequiredService< DbContextOptions<TodoAppContext>>());

            if (context.TodoItems.Any())
            {
                return;
            }

            var todoItemList = new List<TodoItem>();
            var random = new Random();

            for (var i = 0; i < 100; i++)
            {
                todoItemList.Add(
                    new TodoItem
                    {
                        Message = $"Task {i+1}",
                        IsDone = random.Next(2)<1
                    }
                );
            }

            context.TodoItems.AddRange(todoItemList);
            context.SaveChanges();
        }
    }
}
