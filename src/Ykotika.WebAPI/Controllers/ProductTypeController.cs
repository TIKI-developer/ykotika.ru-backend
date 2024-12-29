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
    [Route("products/types")]
    public class ProductTypeController
        (IMapper mapper)
        : BaseController
    {
        private readonly IMapper _mapper = mapper;


        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpGet]
        public async Task<ActionResult<ProductTypeList>> Get([FromQuery] bool? isPublished)
        {
            var query = new GetProductTypeListQuery()
            {
                IsPublished = isPublished
            };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize(Roles = $"{Roles.AUTHOR_ROLE}")]
        [HttpGet("published")]
        public async Task<ActionResult<ProductTypeList>> GetPublished()
        {
            var query = new GetProductTypeListQuery()
            {
                IsPublished = true
            };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}, {Roles.AUTHOR_ROLE}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductTypeDetails>> GetById(Guid id)
        {
            var query = new GetProductTypeByIdQuery { Id = id };
            var vm = await Mediator.Send(query);

            if (!(User.IsInRole(Roles.DIRECTOR_ROLE)))
            {
                if (vm.IsPublished == false)
                {
                    return NotFound();
                }
            }

            return Ok(vm);
        }

        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateProductTypeDto dto)
        {
            var command = _mapper.Map<CreateProductTypeCommand>(dto);
            var id = await Mediator.Send(command);

            return Ok(id);
        }
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductTypeDto dto)
        {
            var command = _mapper.Map<UpdateProductTypeCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteProductTypeCommand { Id = id };
            await Mediator.Send(command);

            return Ok();
        }
    }
}
