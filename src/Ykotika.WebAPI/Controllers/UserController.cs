using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Ykotika.Application.Commands.Author;
using Ykotika.Application.Commands.User;
using Ykotika.Application.Interfaces;
using Ykotika.Application.Queries.User;
using Ykotika.Application.ViewModels;
using Ykotika.WebAPI.Constants;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Controllers
{
    [Route("users")]
    public class UserController
        (IMapper mapper,
        IEmailVerifier emailVerifier,
        IJwtProvider jwtProvider,
        IOptions<Clients> clients) : BaseController
    {
        private readonly IMapper _mapper = mapper;
        private readonly IEmailVerifier _emailVerifier = emailVerifier;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly Clients _clients = clients.Value;

        [Route("signup")]
        [HttpPost]
        public async Task<ActionResult<SignupResponse>> Signup([FromBody] SignupDto signupDto)
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

        [Authorize(Roles = $"{Roles.GUEST_ROLE}")]
        [Route("send-verify")]
        [HttpPost]
        public async Task<IActionResult> SendVerifyEmailMessage()
        {
            await Task.Run(() =>
            {
                var token = _jwtProvider.GenerateEmailVerificationToken(UserId, UserEmail);
                var confirmationLink = "";
                var encodeToken = Uri.EscapeDataString(token);

                if (_clients.WebURLs.IsNullOrEmpty())
                {
                    confirmationLink = Url.Action(
                       "VerifyEmail",
                       "User",
                       new { token = encodeToken! },
                       protocol: Request.Scheme
                   );
                }
                else
                {
                    confirmationLink = $"{_clients.WebURLs}/auth/new-verification?token={encodeToken}";
                }
                _emailVerifier.SendVerificationLink(UserEmail, confirmationLink!);
            });

            return Ok();
        }

        [Authorize(Roles = $"{Roles.GUEST_ROLE}")]
        [Route("verify")]
        [HttpGet]
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

            return Ok(vm);
        }

        [Authorize(Roles = $"{Roles.CUSTOMER_ROLE}")]
        [Route("send-request-to-be-author")]
        [HttpPut]
        public async Task<IActionResult> SendRequestToBeAuthor([FromBody] SendRequestToBeAuthorDto dto)
        {
            var command = _mapper.Map<SendRequestToBeCommand>(dto);
            command.UserId = UserId;
            await Mediator.Send(command);

            return Ok();
        }

        [HttpGet("profile")]
        public async Task<ActionResult<UserDetails>> GetProfile()
        {
            var query = new GetProfileQuery { Id = UserId };
            var vm = await Mediator.Send(query);

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
    }
}
