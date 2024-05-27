using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MotorcycleCatalog.Data;
using MotorcycleCatalog.DTOs.BookDTOs;
using MotorcycleCatalog.Helpers;
using MotorcycleCatalog.Interfaces;
using MotorcycleCatalog.Models;

namespace MotorcycleCatalog.Repository
{
    /// <summary>
    /// Repository class for managing Motorcycle entities.
    /// </summary>
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="MotorcycleRepository"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public MotorcycleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all motorcycles based on the specified query object.
        /// </summary>
        /// <param name="query">The query object containing pagination information.</param>
        /// <returns>A list of motorcycles matching the query.</returns>
        public async Task<List<Motorcycle>> GetAllAsync(QueryObject query)
        {
            var motorcycles = _context.Motorcycles.Include(c => c.Reviews).AsQueryable();

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await motorcycles.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        /// <summary>
        /// Retrieves a motorcycle by its ID.
        /// </summary>
        /// <param name="id">The ID of the motorcycle.</param>
        /// <returns>The motorcycle with the specified ID.</returns>
        public async Task<Motorcycle?> GetByIdAsync(int id)
        {
            return await _context.Motorcycles.Include(c => c.Reviews).FirstOrDefaultAsync(i => i.MotorcycleId == id);
        }

        /// <summary>
        /// Creates a new motorcycle.
        /// </summary>
        /// <param name="motorcycleModel"></param>
        /// <returns>The created motorcycle model.</returns>
        public async Task<Motorcycle?> CreateAsync(Motorcycle motorcycleModel)
        {
            await _context.Motorcycles.AddAsync(motorcycleModel);
            await _context.SaveChangesAsync();
            return motorcycleModel;
        }
        /// <summary>
        /// Deletes a motorcycle by its ID.
        /// </summary>
        /// <param name="id">The ID of the motorcycle to delete.</param>
        /// <returns>The deleted motorcycle model.</returns>
        public async Task<Motorcycle?> DeleteAsync(int id)
        {
            var motorcycleModel = await _context.Motorcycles.FirstOrDefaultAsync(x => x.MotorcycleId == id);

            if(motorcycleModel == null)
            {
                return null;
            }

            _context.Motorcycles.Remove(motorcycleModel);
            await _context.SaveChangesAsync();
            return motorcycleModel;
        }

        /// <summary>
        /// Updates an existing motorcycle.
        /// </summary>
        /// <param name="id">The ID of the motorcycle to update.</param>
        /// <param name="motorcycleUpdateDTO">The motorcycle update DTO containing updated data.</param>
        /// <returns>The updated motorcycle model.</returns>
        public async Task<Motorcycle?> UpdateAsync(int id, UpdateMotorcycleRequestDTO motorcycleUpdateDTO)
        {
            var existingMotorcycle = await _context.Motorcycles.FirstOrDefaultAsync(x => x.MotorcycleId == id);

            if(existingMotorcycle == null)
            {
                return null;
            }

            existingMotorcycle.Model = motorcycleUpdateDTO.Model;
            existingMotorcycle.Brand = motorcycleUpdateDTO.Brand;
            existingMotorcycle.ProductionDate = motorcycleUpdateDTO.ProductionDate;
            existingMotorcycle.Weight = motorcycleUpdateDTO.Weight;
            existingMotorcycle.Torque = motorcycleUpdateDTO.Torque;
            existingMotorcycle.HorsePower = motorcycleUpdateDTO.HorsePower;

            await _context.SaveChangesAsync();

            return existingMotorcycle;
        }

        /// <summary>
        /// Checks if a motorcycle exists by its ID.
        /// </summary>
        /// <param name="id">The ID of the motorcycle to check.</param>
        /// <returns>A boolean indicating whether the motorcycle exists.</returns>
        public Task<bool> MotorcycleExists(int id)
        {
            return _context.Motorcycles.AnyAsync(s => s.MotorcycleId == id);
        }

        /// <summary>
        /// Retrieves a motorcycle by its model.
        /// </summary>
        /// <param name="model">The model of the motorcycle.</param>
        /// <returns>The motorcycle with the specified model.</returns>
        public async Task<Motorcycle?> GetByModelAsync(string model)
        {
            return await _context.Motorcycles.Include(c => c.Reviews).FirstOrDefaultAsync(i => i.Model == model);
        }
    }
}
