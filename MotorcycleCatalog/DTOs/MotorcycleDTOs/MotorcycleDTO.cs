using MotorcycleCatalog.DTOs.ReviewDTOs;

namespace MotorcycleCatalog.DTOs.BookDTOs
{
    public class MotorcycleDTO
    {
        public int MotorcycleId { get; set; }

        public string Model { get; set; }

        public string Brand { get; set; }

        public DateTime ProductionDate { get; set; } 

        public int Weight { get; set; } 

        public int Torque { get; set; } 

        public decimal HorsePower { get; set; }
        public int Rating { get; set; }

        public List<ReviewDTO> Reviews { get; set; }
    }
}
