using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MotorcycleCatalog.DTOs.BookDTOs;
using MotorcycleCatalog.Helpers;
using MotorcycleCatalog.Data;
using MotorcycleCatalog.Interfaces;
using MotorcycleCatalog.Mappers;

namespace DigitalLibrary.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MotorcycleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMotorcycleRepository _motorcycleRepository;
        /// <summary>
        /// Constructor for Motorycle Controller
        /// </summary>
        /// <param name="context"></param>
        /// <param name="motorcycleRepository"></param>
        public MotorcycleController(ApplicationDbContext context, IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
            _context = context;
        }

        /// <summary>
        /// Get a paginated list of motorcycles.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var motorcycle = await _motorcycleRepository.GetAllAsync(query);

            var motorcycleDTO = motorcycle.Select(s => s.ToMotorcycleDTO());

            return Ok(motorcycle);
        }

        /// <summary>
        /// Get a motorcycle by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var motorcycle = await _motorcycleRepository.GetByIdAsync(id);

            if(motorcycle == null)
            {
                return NotFound();
            }

            return Ok(motorcycle.ToMotorcycleDTO());
        }

        /// <summary>
        /// Get a motorcycle by Model.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("{model}")]
        public async Task<IActionResult> GetByModel([FromRoute] string model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var motorcycle = await _motorcycleRepository.GetByModelAsync(model);

            if(motorcycle == null)
            {
                return NotFound();
            }

            return Ok(motorcycle.ToMotorcycleDTO());
        }

        /// <summary>
        /// Create a new motorcycle.
        /// </summary>
        /// <param name="motorcycleDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMotorcycleRequestDTO motorcycleDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var motorcycleModel = motorcycleDTO.ToMotorcycleFromCreateDTO();
            await _motorcycleRepository.CreateAsync(motorcycleModel);
            return CreatedAtAction(nameof(GetById), new {id = motorcycleModel.MotorcycleId}, motorcycleModel.ToMotorcycleDTO());
        }

        /// <summary>
        /// Update an existing motorcycle.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="motorcycleUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateMotorcycleRequestDTO motorcycleUpdateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var motorcycleModel = await _motorcycleRepository.UpdateAsync(id, motorcycleUpdateDTO);

            if(motorcycleModel == null)
            {
                return NotFound();
            }

            return Ok(motorcycleModel.ToMotorcycleDTO());
        }

        /// <summary>
        /// Delete an existing motorcycle
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var motorcycleModel = await _motorcycleRepository.DeleteAsync(id);

            if(motorcycleModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
