using AutoMapper;
using IdentityApi.Requests;
using IdentityBusinessLogic.DTO;
using IdentityBusinessLogic.Services.Implementations;
using IdentityBusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        private readonly IUserAuthenticate _authService;
        private readonly ICredentialsService _credentialsService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AuthentificationController(IUserAuthenticate authService, IUserService userService, ICredentialsService credentialsService, IMapper mapper)
        {
            _authService = authService;
            _userService = userService;
            _credentialsService = credentialsService;
            _mapper = mapper;
        }

        [HttpPost("registration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(UserRegisterRequest user)
        {
            var userToRegister = _mapper.Map<UserDto>(user);

            var tokens = await _authService.RegisterUserAsync(userToRegister, user.Password);

            SetRefreshToken(tokens.refreshToken);

            return Ok(tokens.accessToken);
        }

        


        [HttpPost("connection")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody, Required] UserLoginRequest user)
        {
            var tokens = await _authService.AuthenticateUserAsync(user.Email, user.Password);

            SetRefreshToken(tokens.refreshToken);

            return Ok(tokens.accessToken);
        }

        [HttpGet("refreshToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["RefreshToken"];

            if (refreshToken is null)
                return RedirectToRoute("login");

            var tokens = await _authService.CreateTokensAsync(refreshToken);

            SetRefreshToken(tokens.refreshToken);

            return Ok(tokens.accessToken);
        }
        private void SetRefreshToken(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(3)
            };

            Response.Cookies.Append("RefreshToken", refreshToken, cookieOptions);
        }


    }
}
