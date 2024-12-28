using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks.Dataflow;
using Ykotika.Application.Commands;
using Ykotika.Application.Queries;
using Ykotika.Application.ViewModels;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Controllers
{
    [Route("products")]
    public class ProductController
        (IMapper mapper)
        : BaseController
    {
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<ProductList>> Get()
        {
            var query = new GetProductListQuery();
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetails>> GetById(Guid id)
        {
            var query = new GetProductByIdQuery() { Id = id };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateProductDto dto)
        {
            var command = _mapper.Map<CreateProductCommand>(dto);
            var id = await Mediator.Send(command);

            return Ok(id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductDto dto)
        {
            var command = _mapper.Map<UpdateProductCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteProductCommand { Id = id };
            await Mediator.Send(command);

            return Ok();
        }
        [HttpPost("generate-spreadsheet")]
        public async Task<ActionResult<Guid>> GenerateSpreadSheet([FromBody] GenerateProductSpreadsheetDto dto)
        {
            var command = _mapper.Map<GenerateProductSpreadsheetCommand>(dto);
            var id = await Mediator.Send(command);

            return Ok(id);
        }
    }
}
