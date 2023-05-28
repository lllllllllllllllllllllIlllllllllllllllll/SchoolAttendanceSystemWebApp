using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prototype.Models
{
    public class Attendances
    {
        [Key]
        public int AttendanceId { get; set; }
        [Required]
        public string Date { get; set; }

        [Required]
        public string Status { get; set; }

        // foreign keys
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Students Students { get; set; }
    }
}
