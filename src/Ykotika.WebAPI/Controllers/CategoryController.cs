using Microsoft.AspNetCore.Mvc;

namespace Ykotika.WebAPI.Controllers
{
    [Route("categories")]
    public class CategoryController : BaseController
    {
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
        public async Task<IActionResult> Create()
        {
            return Ok();
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
