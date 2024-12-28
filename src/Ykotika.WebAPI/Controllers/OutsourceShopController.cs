using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Commands;
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
        public async Task<ActionResult<Guid>> Create([FromBody] CreateOutsourceShopDto dto)
        {
            var command = _mapper.Map<CreateOutsourceShopCommand>(dto);
            var id = await Mediator.Send(command);

            return Ok(id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update()
        {
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete()
        {
            return Ok();
        }
    }
}
