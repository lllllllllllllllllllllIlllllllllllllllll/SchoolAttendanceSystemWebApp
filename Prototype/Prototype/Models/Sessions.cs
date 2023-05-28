using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prototype.Models
{
    public class Sessions
    {
        [Key]
        public int SessionId { get; set; }
        [Required]
        public int RoomId { get; set; }

        [Required]
        public string DayOfWeek { get; set; }

        [Required]
        public string StartTime { get; set; }
        [Required]
        public string EndTime { get; set; }

        // foreign keys
        
        [ForeignKey("RoomId")]
        public Classrooms ClassRooms { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Courses Courses { get; set; }

        // relationship
        public List<Attendances> Attendances { get; set; }
    }
}
