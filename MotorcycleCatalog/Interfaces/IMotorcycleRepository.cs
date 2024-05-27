using MotorcycleCatalog.DTOs.BookDTOs;
using MotorcycleCatalog.Helpers;
using MotorcycleCatalog.Models;

namespace MotorcycleCatalog.Interfaces
{
    public interface IMotorcycleRepository
    {
        Task<List<Motorcycle>> GetAllAsync(QueryObject query);
        Task<Motorcycle?> GetByIdAsync(int id);
        Task<Motorcycle?> GetByModelAsync(string model);
        Task<Motorcycle> CreateAsync(Motorcycle motorcycleModel);
        Task<Motorcycle?> UpdateAsync(int id, UpdateMotorcycleRequestDTO motorcycleModel);
        Task<Motorcycle?> DeleteAsync(int id);
        Task<bool> MotorcycleExists(int id);
    }
}
