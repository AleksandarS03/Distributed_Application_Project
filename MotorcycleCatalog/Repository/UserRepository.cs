using Microsoft.EntityFrameworkCore;
using MotorcycleCatalog.Data;
using MotorcycleCatalog.DTOs.UserDTOs;
using MotorcycleCatalog.Helpers;
using MotorcycleCatalog.Interfaces;
using MotorcycleCatalog.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace MotorcycleCatalog.Repository
{
    /// <summary>
    /// Repo for users
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
       /// <summary>
       ///  User Repository Constructor
       /// </summary>
       /// <param name="context"></param>
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns>User created</returns>
        public async Task<User> CreateAsync(User userModel)
        {
            await _context.Users.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }
        /// <summary>
        /// Deletes a user by "id"
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User deleted</returns>
        public async Task<User?> DeleteAsync(int id)
        {
            var userModel = await _context.Users.FirstOrDefaultAsync(i => i.UserId == id);

            if(userModel == null)
            {
                return null;
            }

            _context.Remove(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }
        /// <summary>
        /// Displays all Users
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Returns all Users</returns>
        public async Task<List<User>> GetAllAsync(QueryObject query)
        {
            var users = _context.Users.Include(c => c.Reviews).AsQueryable();

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await users.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }
        /// <summary>
        /// Displays a user by input(id)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.Include(c => c.Reviews).FirstOrDefaultAsync(i => i.UserId == id);
        }
        /// <summary>
        /// Search and display by Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.Include(c => c.Reviews).FirstOrDefaultAsync(i => i.Username == username);
        }
        /// <summary>
        /// Updates a User 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userUpdateDTO"></param>
        /// <returns>Updated user</returns>
        public async Task<User?> UpdateAsync(int id, UpdateUserRequestDTO userUpdateDTO)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);

            if(existingUser == null)
            {
                return null;
            }

            existingUser.Username = userUpdateDTO.Username;
            existingUser.Email = userUpdateDTO.Email;
            existingUser.Password = userUpdateDTO.Password;
            existingUser.DateOfBirth = userUpdateDTO.DateOfBirth;
            existingUser.IsActive = userUpdateDTO.IsActive;

            await _context.SaveChangesAsync();
            return existingUser;
        }
        /// <summary>
        /// Returns a true/false if user exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> UserExists(int id)
        {
            return _context.Users.AnyAsync(s => s.UserId == id);
        }
    }
}
