using AutoMapper;
using IdentityApi.Requests;
using IdentityApi.Responses;
using IdentityBusinessLogic.DTO;
using IdentityBusinessLogic.Services.Implementations;
using IdentityBusinessLogic.Services.Interfaces;
using IdentityDataAccess.Models;
using IdentityDataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IdentityBusinessLogic.Exceptions;


namespace IdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAuthenticate _authService;
        private readonly ICredentialsService _credentialsService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserAuthenticate authService, IUserService userService, ICredentialsService credentialsService, IMapper mapper)
        {
            _authService = authService;
            _userService = userService;
            _credentialsService = credentialsService;
            _mapper = mapper;
        }
        [HttpGet("email/{email}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getByEmail(string email)
        {
            var userByEmail = await _userService.GetUserByEmailAsync(email);
            var userResponse = _mapper.Map<UserResponse>(userByEmail);
            return Ok(userResponse);
        }




        [HttpDelete("delById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUser([FromBody] Guid Id)
        {
            var userDeleted = await _userService.DeleteAsync(Id);
            return Ok(userDeleted);
        }

        [HttpGet("selectedId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetbyId(Guid Id)
        {
            var userSelected = await _userService.GetByIdAsync(Id);
            var userResponse = _mapper.Map<UserResponse>(userSelected);

            return Ok(userResponse);
        }

        [HttpGet("allUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            var userResponse = _mapper.Map<List<UserResponse>>(users);
            return Ok(userResponse);
        }

        [HttpPut("updateUser/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatedUserAsync([FromBody] UserUpdateRequest user)
        {
            
            var userUp = _mapper.Map<UserDto>(user);
            return Ok(await _userService.UpdateAsync(userUp));

        }


    }

}
