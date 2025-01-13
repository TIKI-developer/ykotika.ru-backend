using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;
using Ykotika.Application.Queries;
using Ykotika.Application.ViewModels;
using Ykotika.WebAPI.ModelBinders;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Controllers
{
    [Route("users")]
    public class UserController
        (IMapper mapper)
        : BaseController
    {
        private readonly IMapper _mapper = mapper;

        [HttpGet("me")]
        public async Task<ActionResult<UserDetails>>
            GetMe()
        {
            var query = new GetUserByIdQuery { Id = UserId };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpPut("me")]
        public async Task<IActionResult>
            UpdateMe([FromBody] UpdateProfileDto dto)
        {
            var command = _mapper.Map<UpdateProfileCommand>(dto);
            command.Id = UserId;
            await Mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<UserItem>>>
            Get([FromQuery] UserListQueryParams queryParams)
        {
            var query = _mapper.Map<GetUserListQuery>(queryParams);
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetails>>
            Get(Guid id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult>
            ChangeRoles(Guid id, [FromBody] UpdateUserRolesDto dto)
        {
            var command = _mapper.Map<UpdateUserRolesCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }
    }
    public class UserListQueryParams : IMapWith<GetUserListQuery>
    {
        [ModelBinder(BinderType = typeof(SortingBinder))]
        public SortingQueryParams Sorting { get; set; } = new();

        [ModelBinder(BinderType = typeof(PaginationBinder))]
        public PaginationQueryParams Pagination { get; set; } = new();

        [ModelBinder(BinderType = typeof(UserFilterBinder))]
        public UserFilterQueryParams Filter { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserListQueryParams, GetUserListQuery>();
        }
    }
    public class UserFilterQueryParams : IMapWith<UserFilterDto>
    {
        public string? IsPublished { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserFilterQueryParams, UserFilterDto>()
                .ForMember(to => to.IsPublished,
                opt => opt.MapFrom(from => bool.Parse(from.IsPublished)));
        }
    }
}
