using MotorcycleCatalog.Models;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleCatalogMVC.Models
{
    public class ReviewViewModel
    {
        [Key]
        public int ReviewId { get; set; }

        public int MotorcycleId { get; set; }
        public Motorcycle Motorcycle { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int Rating { get; set; }

        public string ReviewText { get; set; } = string.Empty;

        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
    }
}
