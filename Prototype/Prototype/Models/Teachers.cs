using System.ComponentModel.DataAnnotations;

namespace Prototype.Models
{
    public class Teachers
    {
        [Key]
        public int TeacherId { get; set; }
        [Required]
        public string Name { get; set; }

        // relationship
        public List<Courses> Courses { get; set; }
    }
}
