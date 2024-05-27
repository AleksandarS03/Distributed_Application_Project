using Microsoft.EntityFrameworkCore;
using MotorcycleCatalog.Data;
using MotorcycleCatalog.Interfaces;
using MotorcycleCatalog.Models;

namespace MotorcycleCatalog.Repository
{
    /// <summary>
    /// Repository class for managing Review entities.
    /// </summary>
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReviewRepository"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new review.
        /// </summary>
        /// <param name="reviewModel">The review model to create.</param>
        /// <returns>The created review model.</returns>
        public async Task<Review> CreateAsync(Review reviewModel)
        {
            await _context.Reviews.AddAsync(reviewModel);
            await _context.SaveChangesAsync();
            return reviewModel;
        }

        /// <summary>
        /// Deletes a review by its ID.
        /// </summary>
        /// <param name="id">The ID of the review to delete.</param>
        /// <returns>The deleted review model, or null if not found.</returns>
        public async Task<Review?> DeleteAsync(int id)
        {
            var reviewModel = await _context.Reviews.FirstOrDefaultAsync(x => x.ReviewId == id);

            if (reviewModel == null)
            {
                return null;
            }

            _context.Reviews.Remove(reviewModel);
            await _context.SaveChangesAsync();
            return reviewModel;
        }

        /// <summary>
        /// Retrieves all reviews.
        /// </summary>
        /// <returns>A list of all reviews.</returns>
        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        /// <summary>
        /// Retrieves a review by its ID.
        /// </summary>
        /// <param name="id">The ID of the review.</param>
        /// <returns>The review with the specified ID, or null if not found.</returns>
        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        /// <summary>
        /// Updates an existing review.
        /// </summary>
        /// <param name="id">The ID of the review to update.</param>
        /// <param name="reviewModel">The review model containing updated data.</param>
        /// <returns>The updated review model, or null if not found.</returns>
        public async Task<Review?> UpdateAsync(int id, Review reviewModel)
        {
            var existingReview = await _context.Reviews.FindAsync(id);

            if(existingReview == null) 
            {
                return null;
            }

            existingReview.Rating = reviewModel.Rating;
            existingReview.ReviewText = reviewModel.ReviewText;
            existingReview.ReviewDate = reviewModel.ReviewDate;

            await _context.SaveChangesAsync();

            return existingReview;
        }
    }
}
