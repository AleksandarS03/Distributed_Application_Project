﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcycleCatalog.Data;
using MotorcycleCatalog.DTOs.UserDTOs;
using MotorcycleCatalog.Helpers;
using MotorcycleCatalog.Interfaces;
using MotorcycleCatalog.Mappers;

namespace DigitalLibrary.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;

        public UserController(ApplicationDbContext context, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _context = context;
        }

        /// <summary>
        /// Get a paginated list of users.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var users = await _userRepository.GetAllAsync(query);

            var userDTO = users.Select(s => s.ToUserDTO());

            return Ok(users);
        }

        /// <summary>
        /// Get a user by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userRepository.GetByIdAsync(id);

            if(user == null)
            {
                return NotFound();
            }

            return Ok(user.ToUserDTO());
        }

        /// <summary>
        /// Get a user by Username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("{username}")]
        public async Task<IActionResult> GetByUsername([FromRoute] string username)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userRepository.GetByUsernameAsync(username);

            if(user == null )
            {
                return NotFound();
            }

            return Ok(user.ToUserDTO());
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestDTO userDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userModel = userDTO.ToUserFromCreateDTO();
            await _userRepository.CreateAsync(userModel);
            //it creates the user, then gives it the new user's id so it can use the getbyid and show you the
            //newly created user in the 201 success message
            return CreatedAtAction(nameof(GetById), new { id = userModel.UserId }, userModel.ToUserDTO());
        }

        /// <summary>
        /// Update an existing user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserRequestDTO userUpdateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userModel = await _userRepository.UpdateAsync(id, userUpdateDTO);

            if(userModel == null)
            {
                return NotFound();
            }

            return Ok(userModel.ToUserDTO());
        }

        /// <summary>
        /// Delete an existing user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userModel = await _userRepository.DeleteAsync(id);

            if(userModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
