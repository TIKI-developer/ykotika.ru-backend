using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Commands;
using Ykotika.Application.Queries;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;
using Ykotika.WebAPI.Constants;
using Ykotika.WebAPI.Models;
using Ykotika.WebAPI.QueryParams;

namespace Ykotika.WebAPI.Controllers
{
    [Route("authors")]
    public class AuthorController
        (IMapper mapper) : BaseController
    {
        private readonly IMapper _mapper = mapper;

        [HttpGet("me")]
        [Authorize(Roles = $"{Roles.AUTHOR_ROLE}")]
        public async Task<ActionResult<AuthorDetails>>
            GetMe()
        {
            var query = new GetAuthorByUserQuery { Id = UserId };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet]
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        public async Task<ActionResult<PagedList<AuthorItem>>>
            Get([FromQuery] AuthorListQueryParams queryParams)
        {
            var query = _mapper.Map<GetAuthorListQuery>(queryParams);
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDetails>>
            GetById(Guid id)
        {
            var query = new GetAuthorByUserQuery { Id = id };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
        [HttpPost]
        [Authorize(Roles = $"{Roles.VERIFIED_ROLE}")]
        public async Task<ActionResult<Guid>>
            SendRequest([FromBody] SendRequestToBeAuthorDto dto)
        {
            var command = _mapper.Map<SendRequestToBeAuthorCommand>(dto);
            command.UserId = UserId;
            var id = await Mediator.Send(command);

            return Ok(id);
        }
        [HttpPatch("{id}")]
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        public async Task<IActionResult>
            ChangeStatus(Guid id, [FromBody] UpdateAuthorStatusDto dto)
        {
            var command = _mapper.Map<UpdateAuthorStatusCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }
    }
}
