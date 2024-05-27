using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcycleCatalog.DTOs.ReviewDTOs;
using MotorcycleCatalog.Interfaces;
using MotorcycleCatalog.Mappers;

namespace DigitalLibrary.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IUserRepository _userRepository;
        /// <summary>
        /// Constructur for Review controller
        /// </summary>
        /// <param name="reviewRepository"></param>
        /// <param name="motorcycleRepository"></param>
        /// <param name="userRepository"></param>
        public ReviewController(IReviewRepository reviewRepository, IMotorcycleRepository motorcycleRepository, 
            IUserRepository userRepository)
        {
            _reviewRepository = reviewRepository;
            _motorcycleRepository = motorcycleRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get a paginated list of motorcycle.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviews = await _reviewRepository.GetAllAsync();

            var reviewDTO = reviews.Select(s => s.ToReviewDTO());

            return Ok(reviewDTO);
        }

        /// <summary>
        /// Get a review by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = await _reviewRepository.GetByIdAsync(id);

            if(review == null)
            {
                return NotFound();
            }

            return Ok(review.ToReviewDTO());
        }

        /// <summary>
        /// Create a new review by userId and motorcycleId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="motorcycleId"></param>
        /// <param name="reviewDTO"></param>
        /// <returns></returns>
        [HttpPost("{userId:int},{motorcycleId:int}")]
        public async Task<IActionResult> Create([FromRoute] int userId, [FromRoute] int motorcycleId, 
           [FromBody] CreateReviewRequestDTO reviewDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _motorcycleRepository.MotorcycleExists(motorcycleId))
            {
                return BadRequest("The specified motorcycle does not exist.");
            }

            if (!await _userRepository.UserExists(userId))
            {
                return BadRequest("The specified user does not exist.");
            }

            var reviewModel = reviewDTO.ToReviewFromCreateDTO(userId, motorcycleId);
            await _reviewRepository.CreateAsync(reviewModel);
            return CreatedAtAction(nameof(GetById), new { id = reviewModel.ReviewId }, reviewModel.ToReviewDTO());
        }

        /// <summary>
        /// Update an existing review.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] UpdateReviewRequestDTO updateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = await _reviewRepository.UpdateAsync(id, updateDTO.ToReviewFromUpdateDTO());

            if(review == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(review.ToReviewDTO());
        }

        /// <summary>
        /// Delete an existing review.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewModel = await _reviewRepository.DeleteAsync(id);

            if(reviewModel == null)
            {
                return NotFound("Review does not exist");
            }

            return Ok(reviewModel);
        }
    }
}
