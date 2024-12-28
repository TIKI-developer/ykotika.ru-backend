using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Commands;
using Ykotika.Application.Queries;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Controllers
{
    [Route("categories")]
    public class CategoryController
        (IMapper mapper)
        : BaseController
    {
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetCategoriesQuery();
            //var vm = await Mediator.Send(query);
            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById()
        {
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateCategoryDto dto)
        {
            var command = _mapper.Map<CreateCategoryCommand>(dto);
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
