using System.ComponentModel.DataAnnotations;

namespace MotorcycleCatalog.DTOs.BookDTOs
{
    public class CreateMotorcycleRequestDTO
    {
        [Required]
        [MaxLength(255, ErrorMessage = "Model is too long")]
        public string Model { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Brand is too long")]
        public string Brand { get; set; }

        [Required]
        [Range(0,1000, ErrorMessage = "Weight must be Number")]
        public int Weight { get; set; }

        [Required]
        public DateTime ProductionDate { get; set; }

        [Required]
        [Range(0,1000, ErrorMessage = "HorsePower must be Number")]
        public int HorsePower { get; set; }

        [Required]
        [Range(0, 1000, ErrorMessage = "Torque must be Number")]
        public int Torque { get; set; }
    }
}
