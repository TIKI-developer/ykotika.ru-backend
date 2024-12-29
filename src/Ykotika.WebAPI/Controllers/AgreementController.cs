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
    [Route("agreements")]
    public class AgreementController
        (IMapper mapper)
        : BaseController
    {
        private readonly IMapper _mapper = mapper;

        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpGet]
        public async Task<ActionResult<AgreementList>> GetAll()
        {
            var query = new GetAgreementListQuery();
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<AgreementDetails>> GetById(Guid id)
        {
            var query = new GetAgreementByIdQuery { Id = id };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize(Roles = $"{Roles.AUTHOR_ROLE}")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAgreementDto dto)
        {
            var command = _mapper.Map<CreateAgreementCommand>(dto);
            command.UserId = UserId;
            await Mediator.Send(command);

            return Ok();
        }
    }
}
