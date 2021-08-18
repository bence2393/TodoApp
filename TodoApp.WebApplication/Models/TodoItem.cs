using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.WebApplication.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsDone { get; set; }
    }
}