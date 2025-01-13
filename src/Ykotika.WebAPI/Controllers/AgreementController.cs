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
    [Route("agreements")]
    public class AgreementController
        (IMapper mapper,
        IAuthorizationService authorizationService)
        : BaseController
    {
        private readonly IMapper _mapper = mapper;
        private readonly IAuthorizationService _authorizationService = authorizationService;

        [HttpGet]
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        public async Task<ActionResult<PagedList<AgreementItem>>>
            Get([FromQuery] AgreementListQueryParams queryParams)
        {
            var query = _mapper.Map<GetAgreementListQuery>(queryParams);
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{Roles.VERIFIED_ROLE}, {Roles.DIRECTOR_ROLE}")]
        public async Task<ActionResult<AgreementDetails>>
            GetById(Guid id)
        {
            var query = new GetAgreementByIdQuery { Id = id };
            var vm = await Mediator.Send(query);

            var authorizationResult = await
                _authorizationService
                .AuthorizeAsync(User, vm, Policies.READ_AGREEMENT_POLICY);

            if (authorizationResult.Succeeded)
            {
                return Ok(vm);
            }

            return Forbid();
        }

        [HttpPost]
        [Authorize(Roles = $"{Roles.VERIFIED_ROLE}")]
        public async Task<IActionResult>
            Create([FromBody] CreateAgreementDto dto)
        {
            var command = _mapper.Map<CreateAgreementCommand>(dto);
            command.UserId = UserId;
            var id = await Mediator.Send(command);

            return Ok(id);
        }
    }
    public class AgreementListQueryParams : IMapWith<GetAgreementListQuery>
    {
        [ModelBinder(BinderType = typeof(SortingBinder))]
        public SortingQueryParams Sorting { get; set; } = new();

        [ModelBinder(BinderType = typeof(PaginationBinder))]
        public PaginationQueryParams Pagination { get; set; } = new();

        [ModelBinder(BinderType = typeof(AgreementFilterBinder))]
        public AgreementFilterQueryParams Filter { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AgreementListQueryParams, GetAgreementListQuery>();
        }
    }
    public class AgreementFilterQueryParams : IMapWith<AgreementFilterDto>
    {
        public string? UserId { get; set; }
        public string? OfferId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AgreementFilterQueryParams, AgreementFilterDto>()
                .ForMember(to => to.OfferId,
                    opt => opt.MapFrom(from =>
                        string.IsNullOrEmpty(from.OfferId) ? (Guid?)null : Guid.Parse(from.OfferId)))
                .ForMember(to => to.UserId,
                    opt => opt.MapFrom(from =>
                        string.IsNullOrEmpty(from.UserId) ? (Guid?)null : Guid.Parse(from.UserId)));
        }
    }
}
