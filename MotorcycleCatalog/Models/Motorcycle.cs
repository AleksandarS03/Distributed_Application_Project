using System.ComponentModel.DataAnnotations;

namespace MotorcycleCatalog.Models
{
    public class Motorcycle
    {

        [Key]
        public int MotorcycleId { get; set; }

        public string Model { get; set; } = string.Empty;

        public string Brand { get; set; } =string.Empty;

        public DateTime ProductionDate { get; set; } = DateTime.UtcNow;

        public int Weight { get; set; }

        public int Torque { get; set; }

        public decimal HorsePower { get; set; }

        public int Rating { get; set; }

        // Navigation property for the related reviews
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
