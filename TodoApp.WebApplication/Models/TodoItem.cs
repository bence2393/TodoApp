using System.ComponentModel.DataAnnotations;

namespace TodoApp.WebApplication.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        [StringLength(120, MinimumLength = 3)]
        [Required]
        public string Message { get; set; }

        [Display(Name = "Done")]
        public bool IsDone { get; set; }
    }
}