

using MotorcycleCatalog.DTOs.BookDTOs;
using MotorcycleCatalog.Models;

namespace MotorcycleCatalog.Mappers
{
    public static class MotorcycleMappers
    {
        public static MotorcycleDTO ToMotorcycleDTO(this Motorcycle motorcycleModel)
        {
            return new MotorcycleDTO
            {
                MotorcycleId = motorcycleModel.MotorcycleId,
                Model = motorcycleModel.Model,
                Brand = motorcycleModel.Brand,
                ProductionDate = motorcycleModel.ProductionDate,
                Torque = motorcycleModel.Torque,
                Weight = motorcycleModel.Weight,
                Rating = motorcycleModel.Rating,
                Reviews = motorcycleModel.Reviews.Select(c => c.ToReviewDTO()).ToList()
            };
        }

        public static Motorcycle ToMotorcycleFromCreateDTO(this CreateMotorcycleRequestDTO motorcycleDto)
        {
            return new Motorcycle
            {
                Model = motorcycleDto.Model,
                Brand = motorcycleDto.Brand,
                Torque = motorcycleDto.Torque,
                ProductionDate = motorcycleDto.ProductionDate,
                Weight = motorcycleDto.Weight,
                HorsePower = motorcycleDto.HorsePower,
            };
        }

        
    }
}
