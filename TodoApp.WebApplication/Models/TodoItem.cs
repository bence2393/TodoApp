using System.ComponentModel.DataAnnotations;

namespace TodoApp.WebApplication.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        public string Message { get; set; }

        [Display(Name = "Done")]
        public bool IsDone { get; set; }
    }
}