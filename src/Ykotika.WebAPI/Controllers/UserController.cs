using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Entities.Author.Commands;
using Ykotika.Application.Entities.User.Commands.Login;
using Ykotika.Application.Entities.User.Commands.Signup;
using Ykotika.Application.Entities.User.Commands.VerifyEmail;
using Ykotika.Application.Interfaces;
using Ykotika.WebAPI.Constants;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Controllers
{
    [Route("users")]
    public class UserController
        (IMapper mapper,
        IEmailVerifier emailVerifier,
        IJwtProvider jwtProvider) : BaseController
    {
        private readonly IMapper _mapper = mapper;
        private readonly IEmailVerifier _emailVerifier = emailVerifier;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        [Route("signup")]
        [HttpPost]
        public async Task<ActionResult<SignupViewModel>> Signup([FromBody] SignupDto signupDto)
        {
            var command = _mapper.Map<SignupCommand>(signupDto);

            var vm = await Mediator.Send(command);

            HttpContext.Response.Cookies.Append("creeper", vm.AccessToken);

            return Ok(vm);
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<LoginViewModel>> Login([FromBody] LoginDto signupDto)
        {
            var command = _mapper.Map<LoginCommand>(signupDto);

            var vm = await Mediator.Send(command);

            HttpContext.Response.Cookies.Append("creeper", vm.AccessToken);

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
                Console.WriteLine(token);
                var confirmationLink = Url.Action(
                    "VerifyEmail",
                    "User",
                    new { token = token! },
                    protocol: Request.Scheme
                );
                _emailVerifier.SendVerificationLink(UserEmail, confirmationLink!);
            });

            return Ok();
        }

        [Authorize(Roles = $"{Roles.GUEST_ROLE}")]
        [Route("verify")]
        [HttpGet]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            if (!_jwtProvider.VerifyEmailToken(token, UserId, UserEmail))
            {
                return BadRequest("Invalid token!");
            }

            var command = new VerifyEmailCommand
            { UserId = UserId };

            await Mediator.Send(command);


            return Ok();
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
    }
}
