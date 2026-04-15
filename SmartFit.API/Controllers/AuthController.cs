


using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartFit.Application.Common.Interfaces;
using SmartFit.Application.Features.Authentication.Commands.Login;
using SmartFit.Application.Features.Authentication.Commands.register;
using SmartFit.Domain.Entities;
using SmartFit.Application.DTOs;
using System.Net;


namespace SmartFit.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
       

        public AuthController(
     UserManager<ApplicationUser> userManager,
     SignInManager<ApplicationUser> signInManager,
     IJwtService jwtService,
     IMediator mediator,
     IConfiguration configuration
   )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _mediator = mediator;
            _configuration = configuration;
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            try
            {
                var userId = await _mediator.Send(command);

                return Ok(new
                {
                    Message = "User registered successfully",
                    UserId = userId
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return Unauthorized("Invalid email or password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);
            if (result.IsLockedOut)
            {
                return Unauthorized("Account is locked. Try again later.");
            }

            if (!result.Succeeded)
            {
                return Unauthorized("Invalid email or password");
            }

            // 🔐 Generate JWT
            var token = _jwtService.GenerateToken(user.Id, user.Email);

            return Ok(new
            {
                token = token,
                email = user.Email
            });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
        {
            if (string.IsNullOrEmpty(model.Email))
                return BadRequest("Email is required");

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return Ok("If the email exists, a reset link will be sent.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var baseUrl = _configuration["Frontend:BaseUrl"];

            var resetLink = $"{baseUrl}/reset-password?email={model.Email}&token={Uri.EscapeDataString(token)}";

            return Ok(new
            {
                Message = "Reset link generated successfully",
                ResetLink = resetLink
            });
        }



        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return BadRequest("Invalid request");

            var decodedToken = WebUtility.UrlDecode(model.Token);

            var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.NewPassword);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Password has been reset successfully");
        }
    }
}

