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
    [Route("forms")]
    public class FormController
        (IMapper mapper,
        IAuthorizationService authorizationService)
        : BaseController
    {
        private readonly IMapper _mapper = mapper;
        private readonly IAuthorizationService _authorizationService = authorizationService;

        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpPost]
        public async Task<ActionResult<Guid>>
            Create([FromBody] CreateFormDto dto)
        {
            var command = _mapper.Map<CreateFormCommand>(dto);
            command.AuthorId = UserId;
            var id = await Mediator.Send(command);

            return Ok(id);
        }
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpPut("{id}")]
        public async Task<IActionResult>
            Update(Guid id, [FromBody] UpdateFormDto dto)
        {
            var command = _mapper.Map<UpdateFormCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult>
            Delete(Guid id)
        {
            var command = new DeleteFormCommand { Id = id };
            await Mediator.Send(command);

            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<FormDetails>>
            Get(Guid id)
        {
            var query = new GetFormByIdQuery { Id = id };
            var vm = await Mediator.Send(query);

            var authorizationResult = await
                _authorizationService
                .AuthorizeAsync(User, vm, Policies.CONTENT_POLICY);

            if (authorizationResult.Succeeded)
            {
                return Ok(vm);
            }

            return Forbid();
        }
        [HttpGet]
        public async Task<ActionResult<PagedList<FormItem>>>
            Get([FromQuery] FormListQueryParams queryParams)
        {
            var authorizationResult = await
                _authorizationService
                .AuthorizeAsync
                (User,
                new ContentResourceDto { IsPublished = queryParams.Filter.IsPublished },
                Policies.FORM_LIST_POLICY);

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            var query = _mapper.Map<GetFormListQuery>(queryParams);
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
    }
}
