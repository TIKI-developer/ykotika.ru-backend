using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ykotika.WebAPI.Models;
using Ykotika.Application.Entities.User.Commands.Login;
using Ykotika.Application.Entities.User.Commands.Signup;

namespace Ykotika.WebAPI.Controllers
{
    [Route("users")]
    public class UserController(IMapper mapper) : BaseController
    {
        private readonly IMapper _mapper = mapper;

        [Route("signup")]
        [HttpPost]
        public async Task<ActionResult<string>> Signup([FromBody] SignupDto signupDto)
        {
            var command = _mapper.Map<SignupCommand>(signupDto);

            var token = await Mediator.Send(command);

            return Ok(token);
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto signupDto)
        {
            var command = _mapper.Map<LoginCommand>(signupDto);

            var token = await Mediator.Send(command);

            return Ok(token);
        }
    }
}
