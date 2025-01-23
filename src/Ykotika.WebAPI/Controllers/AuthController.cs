using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ykotika.Application.Commands;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;
using Ykotika.WebAPI.Constants;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Controllers
{
    [Route("auth")]
    public class AuthController
        (IMapper mapper,
        IEmailVerifier emailVerifier,
        IJwtProvider jwtProvider)
        : BaseController
    {
        private readonly IMapper _mapper = mapper;
        private readonly IEmailVerifier _emailVerifier = emailVerifier;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        [Route("signup")]
        [HttpPost]
        public async Task<ActionResult<SignupResponse>>
            Signup([FromBody] SignupDto signupDto)
        {
            var command = _mapper.Map<SignupCommand>(signupDto);
            command.Issuer = Request.Headers.Host.ToString();
            command.Audience = Request.Headers.Origin.ToString();
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [Route("login")]
        [HttpPost]

        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginDto signupDto)
        {
            var command = _mapper.Map<LoginCommand>(signupDto);
            command.Issuer = Request.Headers.Host.ToString();
            command.Audience = Request.Headers.Origin.ToString();
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [HttpPost("logout")]
        public async Task<IActionResult>
            Logout()
        {
            await Task.Run(() =>
            {
                HttpContext.Response.Cookies.Delete(Cookies.ACCESS_TOKEN_NAME);
                HttpContext.Response.Cookies.Delete(Cookies.REFRESH_TOKEN_NAME);
            });

            return Ok();
        }
        [HttpPost("refresh-token")]
        public async Task<ActionResult<LoginResponse>>
            NewRefreshToken([FromBody] UpdateRefreshTokenDto dto)
        {
            if (dto.RefreshToken != null &&
                Request.Cookies.TryGetValue(Cookies.ACCESS_TOKEN_NAME, out var accessToken))
            {
                var command = new GenerateRefreshTokenCommand
                {
                    UserId = Guid.Parse(_jwtProvider
                    .GetPrincipalFromExpiredToken(accessToken)
                    .FindFirstValue(ClaimTypes.NameIdentifier)),
                    RefreshToken = dto.RefreshToken,
                    Issuer = Request.Headers.Host.ToString(),
                    Audience = Request.Headers.Origin.ToString(),
                };

                var vm = await Mediator.Send(command);

                return Ok(vm);
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost("verifications/email")]
        [Authorize(Roles = $"{Roles.UNVERIFIED_ROLE}")]
        public async Task<IActionResult>
            SendVerifyEmailMessage()
        {
            var token = _jwtProvider.GenerateEmailVerificationToken(UserId, UserEmail);
            var encodeToken = Uri.EscapeDataString(token);

            var confirmationLink = $"{Request.Headers.Origin}/auth/verifications/email?token={encodeToken}";
            await _emailVerifier.SendVerificationLinkAsync(UserEmail, confirmationLink!);

            return Ok();
        }

        [HttpGet("verifications/email")]
        [Authorize(Roles = $"{Roles.UNVERIFIED_ROLE}")]
        public async Task<IActionResult>
            VerifyEmail([FromQuery] string token)
        {
            var decodedToken = Uri.UnescapeDataString(token);
            if (!_jwtProvider.VerifyEmailToken(decodedToken, UserId, UserEmail))
            {
                return BadRequest("Invalid token!");
            }

            var command = new VerifyEmailCommand
            { 
                UserId = UserId,
                Issuer = Request.Headers.Host.ToString(),
                Audience = Request.Headers.Origin.ToString(),
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [HttpPatch("password")]
        [Authorize(Roles = $"{Roles.VERIFIED_ROLE}")]
        public async Task<IActionResult>
            ChangePassword([FromBody] UpdatePasswordDto dto)
        {
            var command = _mapper.Map<UpdatePasswordCommand>(dto);
            command.UserId = UserId;
            await Mediator.Send(command);

            return Ok();
        }
    }
}
