using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;
using Ykotika.Application.Queries;
using Ykotika.Application.ViewModels;
using Ykotika.WebAPI.Constants;
using Ykotika.WebAPI.ModelBinders;
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
            var query = _mapper.Map<GetFormListQuery>(queryParams);
            var authorizationResult = await
                _authorizationService
                .AuthorizeAsync
                (User,
                new PublishableResourceDto { IsPublished = query.Filter.IsPublished },
                Policies.FORM_LIST_POLICY);

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
    }
    public class FormListQueryParams : IMapWith<GetFormListQuery>
    {
        [ModelBinder(BinderType = typeof(SortingBinder))]
        public SortingQueryParams Sorting { get; set; } = new();

        [ModelBinder(BinderType = typeof(PaginationBinder))]
        public PaginationQueryParams Pagination { get; set; } = new();

        [ModelBinder(BinderType = typeof(FormFilterBinder))]
        public required FormFilterQueryParams Filter { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FormListQueryParams, GetFormListQuery>();
        }
    }
    public class FormFilterQueryParams : IMapWith<FormFilterDto>
    {
        public string? IsPublished { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FormFilterQueryParams, FormFilterDto>()
                .ForMember(to => to.IsPublished,
                    opt => opt.MapFrom(from =>
                        string.IsNullOrEmpty(from.IsPublished) ? (bool?)null :
                        (from.IsPublished.Equals("true", StringComparison.OrdinalIgnoreCase) ? (bool?)true : (bool?)false)));
        }
    }
}
