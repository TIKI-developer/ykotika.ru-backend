using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
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
        IEmailService emailService,
        IJwtProvider jwtProvider)
        : BaseController
    {
        private readonly IMapper _mapper = mapper;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly IEmailService _emailService = emailService;

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
        [EnableRateLimiting("RefreshTokenLimiter")]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<LoginResponse>>
            NewRefreshToken([FromBody] UpdateRefreshTokenDto dto)
        {
            if (dto.RefreshToken != null &&
                dto.AccessToken != null)
            {
                var command = new GenerateRefreshTokenCommand
                {
                    UserId = Guid.Parse(_jwtProvider
                    .GetPrincipalFromExpiredToken(dto.AccessToken)
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
                return BadRequest();
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
            var message = _emailService.GetStringTemplateByName("Verification", new() { { "link", confirmationLink } });
            await _emailService.Send(UserEmail, "Подтверждение почты ykotika.ru", message);

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

        [HttpPatch("password/recover")]
        public async Task<ActionResult<LoginResponse>>
            RecoverPassword([FromQuery] string token, [FromBody] UpdateForgottenPasswordDto dto)
        {
            var decodedToken = Uri.UnescapeDataString(token);
            var command = _mapper.Map<ResetPasswordCommand>(dto);
            command.Token = decodedToken;
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }
        [HttpPost("password/recover/email")]
        public async Task<IActionResult>
            SendRecoverPasswordEmail([FromBody] SendRecoverPasswordMessageDto dto)
        {
            var token = _jwtProvider.GeneratePasswordRecoverToken(dto.Email);
            var encodeToken = Uri.EscapeDataString(token);

            var recoverLink = $"{Request.Headers.Origin}/auth/password/recover?token={encodeToken}";
            var message = _emailService.GetStringTemplateByName("RecoverPassword", new(){ { "link", recoverLink } });
            await _emailService.Send(dto.Email, "Восстановление пароля", message);

            return Ok();
        }
    }
}
