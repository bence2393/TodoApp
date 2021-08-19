using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.WebApplication.Models;
using Xunit;

namespace TodoApp.Unit.Tests.UnitTests
{
    public class TodoItemUnitTest
    {
        [Fact]
        public void OnCreateTodoItem_ModelIsExcepted()
        {
            // Arrange
            // Act
            var todoItem = new TodoItem()
            {
                Id = 0,
                IsDone = false,
                Message = "Create more unit tests"
            };

            // Assert
            Assert.Equal(0, todoItem.Id);
            Assert.False(todoItem.IsDone);
            Assert.Equal("Create more unit tests", todoItem.Message);
        }
    }
}
