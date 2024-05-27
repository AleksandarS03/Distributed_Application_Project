using MotorcycleCatalog.DTOs.ReviewDTOs;
using MotorcycleCatalog.Models;
using System.Runtime.CompilerServices;

namespace MotorcycleCatalog.Mappers
{
    public static class ReviewMappers
    {
        public static ReviewDTO ToReviewDTO(this Review reviewModel)
        {
            return new ReviewDTO
            {
                ReviewId = reviewModel.ReviewId,
                MotorcycleId = reviewModel.MotorcycleId,
                UserId = reviewModel.UserId,
                Rating = reviewModel.Rating,
                ReviewText = reviewModel.ReviewText,
                ReviewDate = reviewModel.ReviewDate,
            };
        }

        public static Review ToReviewFromCreateDTO(this CreateReviewRequestDTO reviewDTO, 
            int userId, int motorcycleId)
        {
            return new Review
            {
                Rating = reviewDTO.Rating,
                ReviewText = reviewDTO.ReviewText,
                ReviewDate = reviewDTO.ReviewDate,
                UserId = userId,
                MotorcycleId = motorcycleId,
            };
        }

        public static Review ToReviewFromUpdateDTO(this UpdateReviewRequestDTO reviewDTO)
        {
            return new Review
            {
                Rating = reviewDTO.Rating,
                ReviewText = reviewDTO.ReviewText,
                ReviewDate = reviewDTO.ReviewDate,
            };
        }
    }
}
