using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Severity is required.")]
        public string Severity { get; set; }
        public bool IsClosed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }

        public string CreatedById { get; set; }
        public string? CreatedByName { get; set; }

        public string? ResolvedById { get; set; }
        public string? ResolvedByName { get; set; }

        public Ticket()
        {

        }
    }
}
