using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Commands;

using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Controllers
{
    [Route("offers")]
    public class OfferController
        (IMapper mapper)
        : BaseController
    {
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> Get()
        {


            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById()
        {
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOfferDto dto)
        {
            var command = _mapper.Map<CreateOfferCommand>(dto);
            var id = await Mediator.Send(command);

            return Ok(id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOfferDto dto)
        {
            var command = _mapper.Map<UpdateOfferCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteOfferCommand { Id = id };
            await Mediator.Send(command);

            return Ok();
        }
    }
}
