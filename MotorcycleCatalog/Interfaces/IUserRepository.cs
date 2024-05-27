using MotorcycleCatalog.DTOs.UserDTOs;
using MotorcycleCatalog.Helpers;
using MotorcycleCatalog.Models;

namespace MotorcycleCatalog.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync(QueryObject query);
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task<User> CreateAsync(User userModel);
        Task<User?> UpdateAsync(int id, UpdateUserRequestDTO userModel);
        Task<User?> DeleteAsync(int id);
        Task<bool> UserExists(int id);
    }
}
