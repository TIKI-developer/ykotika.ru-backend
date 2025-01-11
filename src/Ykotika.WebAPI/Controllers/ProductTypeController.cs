using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Commands;
using Ykotika.Application.Queries;
using Ykotika.Application.ViewModels;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Controllers
{
    [Route("products/types")]
    public class ProductTypeController
        (IMapper mapper)
        : BaseController
    {
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<ProductTypeList>>
            Get([FromQuery] bool? isPublished)
        {
            var query = new GetProductTypeListQuery()
            {
                IsPublished = isPublished
            };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductTypeDetails>>
            GetById(Guid id)
        {
            var query = new GetProductTypeByIdQuery { Id = id };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>>
            Create([FromBody] CreateProductTypeDto dto)
        {
            var command = _mapper.Map<CreateProductTypeCommand>(dto);
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult>
            Update(Guid id, [FromBody] UpdateProductTypeDto dto)
        {
            var command = _mapper.Map<UpdateProductTypeCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>
            Delete(Guid id)
        {
            var command = new DeleteProductTypeCommand { Id = id };
            await Mediator.Send(command);

            return Ok();
        }
    }
}