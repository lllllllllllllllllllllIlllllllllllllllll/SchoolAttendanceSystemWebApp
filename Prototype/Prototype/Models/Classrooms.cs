using System.ComponentModel.DataAnnotations;

namespace Prototype.Models
{
    public class Classrooms
    {
        [Key]
        public int RoomId { get; set; }
        [Required]
        public string Code { get; set; }
        public string QrCode { get; set; }

        // relationship
        public List<Sessions> Sessions { get; set; }
    }
}
