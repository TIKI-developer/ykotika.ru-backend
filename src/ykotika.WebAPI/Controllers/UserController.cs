using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Interfaces;
using Ykotika.WebAPI.Models;
using Ykotika.Application.Entities.User.Commands.Login;
using Ykotika.Application.Entities.User.Commands.Signup;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Ykotika.Application.Entities.User.Commands.VerifyEmail;

namespace Ykotika.WebAPI.Controllers
{
    [Route("users")]
    public class UserController(
        IMapper mapper,
        IEmailVerifier emailVerifier,
        IJwtProvider jwtProvider) : BaseController
    {
        private readonly IMapper _mapper = mapper;
        private readonly IEmailVerifier _emailVerifier = emailVerifier;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        [Route("signup")]
        [HttpPost]
        public async Task<ActionResult<string>> Signup([FromBody] SignupDto signupDto)
        {
            var command = _mapper.Map<SignupCommand>(signupDto);

            var token = await Mediator.Send(command);

            HttpContext.Response.Cookies.Append("creeper", token);

            return Ok(token);
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto signupDto)
        {
            var command = _mapper.Map<LoginCommand>(signupDto);

            var token = await Mediator.Send(command);

            HttpContext.Response.Cookies.Append("creeper", token);

            return Ok(token);
        }
        [Authorize(Roles = "Guest")]
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
        [Authorize(Roles = "Guest")]
        [Route("verify")]
        [HttpGet]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            // TODO: Добавить проверку токена, плюс нужно чтобы пользователь вошел в аккаунт заново, либо возвращать ему новый токен
            await Task.Run(() =>
            {
                Console.WriteLine(token);
            });
            var command = new VerifyEmailCommand
            { UserId = UserId };

            await Mediator.Send(command);

            return Ok();
        }
    }
}
