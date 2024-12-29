using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Commands;
using Ykotika.Application.Queries;
using Ykotika.Application.ViewModels;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Controllers
{
    [Route("outsource-shops")]
    public class OutsourceShopController
        (IMapper mapper)
        : BaseController
    {
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<Guid>> Get()
        {
            var query = new GetOutsourceShopListQuery();
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OutsourceShopDetails>> GetById(Guid id)
        {
            var query = new GetOutsourceShopQuery { Id = id };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateOutsourceShopDto dto)
        {
            var command = _mapper.Map<CreateOutsourceShopCommand>(dto);
            var id = await Mediator.Send(command);

            return Ok(id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOutsourceShopDto dto)
        {
            var command = _mapper.Map<UpdateOutsourceShopCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteOutsourceShopCommand { Id = id };
            await Mediator.Send(command);

            return Ok();
        }
    }
}
