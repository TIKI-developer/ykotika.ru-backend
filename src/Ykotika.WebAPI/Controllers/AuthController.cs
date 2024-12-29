using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
        IJwtProvider jwtProvider,
        IOptions<ClientsOptions> clients) : BaseController
    {
        private readonly IMapper _mapper = mapper;
        private readonly IEmailVerifier _emailVerifier = emailVerifier;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly ClientsOptions _clients = clients.Value;

        [Route("signup")]
        [HttpPost]
        public async Task<ActionResult<Signup>> Signup([FromBody] SignupDto signupDto)
        {
            var command = _mapper.Map<SignupCommand>(signupDto);

            var vm = await Mediator.Send(command);

            HttpContext.Response.Cookies.Append(Cookies.ACCESS_TOKEN_NAME, vm.AccessToken);

            return Ok(vm);
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginDto signupDto)
        {
            var command = _mapper.Map<LoginCommand>(signupDto);

            var vm = await Mediator.Send(command);

            HttpContext.Response.Cookies.Append(Cookies.ACCESS_TOKEN_NAME, vm.AccessToken);

            return Ok(vm);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await Task.Run(() =>
            {
                HttpContext.Response.Cookies.Delete(Cookies.ACCESS_TOKEN_NAME);
            });

            return Ok();
        }

        [Authorize(Roles = $"{Roles.GUEST_ROLE}")]
        [HttpPost("verifications/email")]
        public async Task<IActionResult> SendVerifyEmailMessage()
        {
            await Task.Run(() =>
            {
                var token = _jwtProvider.GenerateEmailVerificationToken(UserId, UserEmail);
                var confirmationLink = "";
                var encodeToken = Uri.EscapeDataString(token);

                if (_clients.WebURLs.IsNullOrEmpty() || _clients.GeneralClientUrl.IsNullOrEmpty())
                {
                    confirmationLink = Url.Action(
                       "VerifyEmail",
                       "Auth",
                       new { token = encodeToken! },
                       protocol: Request.Scheme
                   );
                }
                else
                {
                    confirmationLink = $"{_clients.GeneralClientUrl}/auth/new-verification?token={encodeToken}";
                }
                _emailVerifier.SendVerificationLink(UserEmail, confirmationLink!);
            });

            return Ok();
        }

        [Authorize(Roles = $"{Roles.GUEST_ROLE}")]
        [HttpGet("verifications/email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            var decodedToken = Uri.UnescapeDataString(token);
            if (!_jwtProvider.VerifyEmailToken(decodedToken, UserId, UserEmail))
            {
                return BadRequest("Invalid token!");
            }

            var command = new VerifyEmailCommand
            { UserId = UserId };

            var vm = await Mediator.Send(command);

            HttpContext.Response.Cookies.Append(Cookies.ACCESS_TOKEN_NAME, vm.AccessToken);

            return Ok();
        }

        [Authorize(Roles = $"{Roles.CUSTOMER_ROLE}")]
        [HttpPatch("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var command = _mapper.Map<ChangePasswordCommand>(dto);
            command.UserId = UserId;
            await Mediator.Send(command);

            return Ok();
        }
    }
}
