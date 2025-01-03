using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Commands;
using Ykotika.Application.Queries;
using Ykotika.Application.ViewModels;
using Ykotika.WebAPI.Constants;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Controllers
{
    [Route("authors")]
    public class AuthorController
        (IMapper mapper) : BaseController
    {
        private readonly IMapper _mapper = mapper;

        [Authorize(Roles = $"{Roles.CUSTOMER_ROLE}")]
        [HttpPost]
        public async Task<ActionResult<Guid>>
            SendRequestToBeAuthor([FromBody] SendRequestToBeAuthorDto dto)
        {
            var command = _mapper.Map<SendRequestToBeCommand>(dto);
            command.UserId = UserId;
            var id = await Mediator.Send(command);

            return Ok(id);
        }
        [Authorize(Roles = $"{Roles.AUTHOR_ROLE}")]
        [HttpGet("me")]
        public async Task<ActionResult<AuthorDetails>>
            GetMyRequest()
        {
            var query = new GetAuthorByUserQuery { Id = UserId };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpGet]
        public async Task<ActionResult<AuthorList>>
            GetAll()
        {
            var query = new GetAuthorListQuery();
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpGet("{id}")]
        public async Task<IActionResult>
            GetById(Guid id)
        {
            var query = new GetAuthorByUserQuery { Id = id };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpPatch("{id}")]
        public async Task<IActionResult>
            ChangeAuthorStatus(Guid id, [FromBody] ChangeAuthorStatusDto dto)
        {
            var command = _mapper.Map<ChangeAuthorStatusCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }

        [HttpGet("{id}/agreements")]
        public async Task<ActionResult<AgreementList>>
            GetAgreements(Guid id)
        {
            var query = new GetAgreementListByAuthorQuery { Id = id };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
    }
}
