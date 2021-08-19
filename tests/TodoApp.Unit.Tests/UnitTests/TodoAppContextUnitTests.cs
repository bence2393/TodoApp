using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApp.WebApplication.Data;
using TodoApp.WebApplication.Models;
using Xunit;

namespace TodoApp.Unit.Tests.UnitTests
{
    public class TodoAppContextUnitTest
    {
        [Fact]
        public async Task ToListAsync_ItemsAreReturned()
        {
            using (var db = new TodoAppContext(Utilities.Utilities.TestDbContextOptions()))
            {
                // Arrange
                var expectedMessages = Utilities.Utilities.GetSeedingItems();
                await db.AddRangeAsync(expectedMessages);
                await db.SaveChangesAsync();

                // Act
                var result = await db.TodoItems.ToListAsync();

                // Assert
                var actualMessages = Assert.IsAssignableFrom<List<TodoItem>>(result);
                Assert.Equal(
                    expectedMessages.OrderBy(m => m.Id).Select(m => m.Message), 
                    actualMessages.OrderBy(m => m.Id).Select(m => m.Message));
            }
        }

        [Fact]
        public async Task AddAsync_TodoItemIsAdded()
        {
            using (var db = new TodoAppContext(Utilities.Utilities.TestDbContextOptions()))
            {
                // Arrange
                var recId = 10;
                var expectedMessage = new TodoItem() { Id = recId, Message = "Improve app", IsDone = false};

                // Act
                await db.AddAsync(expectedMessage);

                // Assert
                var actualMessage = await db.FindAsync<TodoItem>(recId);
                Assert.Equal(expectedMessage, actualMessage);
            }
        }

        [Fact]
        public async Task RemoveRange_TodoItemsAreDeleted()
        {
            using (var db = new TodoAppContext(Utilities.Utilities.TestDbContextOptions()))
            {
                // Arrange
                var seedItems = Utilities.Utilities.GetSeedingItems();
                await db.AddRangeAsync(seedItems);
                await db.SaveChangesAsync();

                // Act
                db.RemoveRange(seedItems);
                await db.SaveChangesAsync();

                // Assert
                Assert.Empty(await db.TodoItems.AsNoTracking().ToListAsync());
            }
        }

    }
}
