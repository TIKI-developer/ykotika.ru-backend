using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Commands;
using Ykotika.Application.Queries;
using Ykotika.Application.ViewModels;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Controllers
{
    [Route("agreements")]
    public class AgreementController
        (IMapper mapper)
        : BaseController
    {
        private readonly IMapper _mapper = mapper;

        [HttpGet("{authorId}")]
        public async Task<ActionResult<AgreementList>> GetByUser(Guid authorId)
        {
            var query = new GetAgreementByUserQuery { AuthorId = authorId };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
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
