using System.ComponentModel.DataAnnotations;

namespace Prototype.Models
{
    public class Students
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        public string Name { get; set; }

        // relationship
        public List<Attendances> Attendances { get; set; }
    }
}
