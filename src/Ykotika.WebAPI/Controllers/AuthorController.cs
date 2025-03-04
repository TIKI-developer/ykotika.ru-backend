using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;
using Ykotika.Application.Queries;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;
using Ykotika.WebAPI.Constants;
using Ykotika.WebAPI.ModelBinders;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Controllers
{
    [Route("authors")]
    public class AuthorController
        (IMapper mapper) : BaseController
    {
        private readonly IMapper _mapper = mapper;

        [HttpGet("me")]
        [Authorize(Roles = $"{Roles.AUTHOR_ROLE}")]
        public async Task<ActionResult<AuthorDetails>>
            GetMe()
        {
            var query = new GetAuthorByUserQuery { Id = UserId };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }


        [HttpPut("me")]
        [Authorize(Roles = $"{Roles.AUTHOR_ROLE}")]
        public async Task<IActionResult>
            Update([FromBody] UpdateAuthorDto dto)
        {
            var command = _mapper.Map<UpdateAuthorCommand>(dto);
            command.Id = UserId;
            await Mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        public async Task<ActionResult<PagedList<AuthorItem>>>
            Get([FromQuery] AuthorListQueryParams queryParams)
        {
            var query = _mapper.Map<GetAuthorListQuery>(queryParams);
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDetails>>
            GetById(Guid id)
        {
            var query = new GetAuthorByUserQuery { Id = id };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
        [HttpPost]
        [Authorize(Roles = $"{Roles.VERIFIED_ROLE}")]
        public async Task<ActionResult<Guid>>
            SendRequest([FromBody] SendRequestToBeAuthorDto dto)
        {
            var command = _mapper.Map<SendRequestToBeAuthorCommand>(dto);
            command.UserId = UserId;
            var id = await Mediator.Send(command);

            return Ok(id);
        }
        [HttpPatch("{id}")]
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        public async Task<IActionResult>
            ChangeStatus(Guid id, [FromBody] UpdateAuthorStatusDto dto)
        {
            var command = _mapper.Map<UpdateAuthorStatusCommand>(dto);
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }
    }
    public class AuthorListQueryParams : IMapWith<GetAuthorListQuery>
    {
        [ModelBinder(BinderType = typeof(SortingBinder))]
        public SortingQueryParams Sorting { get; set; } = new();

        [ModelBinder(BinderType = typeof(PaginationBinder))]
        public PaginationQueryParams Pagination { get; set; } = new();

        [ModelBinder(BinderType = typeof(AuthorFilterBinder))]
        public required AuthorFilterQueryParams Filter { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AuthorListQueryParams, GetAuthorListQuery>();
        }
    }
    public class AuthorFilterQueryParams : IMapWith<AuthorFilterDto>
    {
        public string? Status { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? ContactSocial { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AuthorFilterQueryParams, AuthorFilterDto>()
                .ForMember(to => to.Status,
                    opt => opt.MapFrom(from =>
                        string.IsNullOrEmpty(from.Status) ?
                        (AuthorStatus?)null :
                        Enum.Parse<AuthorStatus>(from.Status)))
                .ForMember(to => to.Name,
                    opt => opt.MapFrom(from => from.Name))
                .ForMember(to => to.Surname,
                    opt => opt.MapFrom(from => from.Surname))
                .ForMember(to => to.ContactSocial,
                    opt => opt.MapFrom(from =>
                        string.IsNullOrEmpty(from.ContactSocial) ?
                        (AuthorRequest.ContactSocial?)null :
                        Enum.Parse<AuthorRequest.ContactSocial>(from.ContactSocial)));
        }
    }
}
