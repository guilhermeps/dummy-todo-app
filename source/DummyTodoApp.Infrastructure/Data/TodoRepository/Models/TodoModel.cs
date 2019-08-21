using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DummyTodoApp.Infrastructure.Data.TodoRepository.Models
{
    [Table("TODO")]
    public sealed class TodoModel
    {
        [Required]
        public Guid Id { get; private set; }

        [Required]
        [MinLength(3, ErrorMessage = "Todo's description must be greater than three (3)")]
        [MaxLength(50, ErrorMessage = "Todo's description must be less than fifty (50)")]
        public string Description { get; private set; }

        [Required]
        [MinLength(2, ErrorMessage = "Todo's owner must be greater than two (2)")]
        public string Owner { get; private set; }

        [Required]
        public bool Done { get; set; }

        public TodoModel(string description, string owner)
        {
            Id = Guid.NewGuid();
            Description = description;
            Owner = owner;
        }

        public bool IsValid() => !string.IsNullOrWhiteSpace(Description) && !string.IsNullOrWhiteSpace(Owner) && !(Id == Guid.Empty);
    }
}
