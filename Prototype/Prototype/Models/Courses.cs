using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prototype.Models
{
    public class Courses
    {
        [Key]
        public int CourseId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }

        // relationship
        public List<Sessions> Sessions { get; set; }
        // foreign keys
        public int TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public Teachers Teachers { get; set; }

    }
}
