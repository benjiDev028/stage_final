using AutoMapper;
using IdentityBusinessLogic.DTO;
using IdentityBusinessLogic.Services.Implementations;
using IdentityBusinessLogic.Services.Interfaces;
using IdentityDataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("email")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> getByEmail(string email)
        {
            var userByEmail = await _userService.GetUserByEmailAsync(email);
            return Ok(userByEmail);
        }



        [HttpPost("createUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] UserDto user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User creation data is required.");
                }

                // Générez le hash et le sel du mot de passe
                _credentialsService.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

                // Construisez l'objet UserDto avec les informations fournies et le mot de passe hashé
                var userDto = new UserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Username = user.Username,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Id = Guid.NewGuid(),
                    RegistrationDate = DateTime.UtcNow

                };

                //créer l'utilisateur avec le UserDto
                var result = await _userService.CreateAsync(userDto);
                if (!result)
                {
                    return BadRequest("User could not be created.");
                }

                // Créez un token pour l'utilisateur
                var token = _credentialsService.CreateToken(userDto);
                var refreshToken = _credentialsService.CreateRefreshToken();

                // Retournez les données de l'utilisateur et les tokens
                return Ok(new
                {
                    UserId = userDto.Id,
                    Token = token,
                    RefreshToken = refreshToken
                });
            }
            catch (Exception e)
            {
                // Loguez l'exception et retournez un message d'erreur générique au client
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the user.");
            }
        }

        [HttpDelete("delById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromBody] Guid Id)
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
            return Ok(userSelected);
        }

        [HttpGet("allUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Users()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpPut("updateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(User user)
        {
            var userUp = _mapper.Map<UserDto>(user);
            return Ok(await _userService.UpdateAsync(userUp));
           
        }

    }

}
