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
    [Route("offers")]
    public class OfferController
        (IMapper mapper)
        : BaseController
    {
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        public async Task<ActionResult<BaseList<OfferItem>>>
            Get([FromQuery] OfferListQueryParams queryParams)
        {
            var query = _mapper.Map<GetOfferListQuery>(queryParams);
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OfferDetails>>
            GetById(Guid id)
        {
            var query = new GetOfferByIdQuery { Id = id };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }
        [HttpGet("current")]
        public async Task<ActionResult<CurrentOfferDetails>>
            GetCurrent([FromQuery]
                       bool acceptMe = false)
        {
            var query = new GetCurrentOfferQuery
            {
                UserId = acceptMe ? UserId : null,
            };
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpPost]
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        public async Task<IActionResult>
            Create([FromBody] CreateOfferDto dto)
        {
            var command = _mapper.Map<CreateOfferCommand>(dto);
            command.AuthorId = UserId;
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        public async Task<ActionResult<Guid>>
            Update(Guid id, [FromBody] UpdateOfferDto dto)
        {
            var command = _mapper.Map<UpdateOfferCommand>(dto);
            command.Id = id;
            command.AuthorId = UserId;
            var updatedId = await Mediator.Send(command);

            return Ok(updatedId);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{Roles.DIRECTOR_ROLE}")]
        public async Task<IActionResult>
            Delete(Guid id)
        {
            var command = new DeleteOfferCommand { Id = id };
            await Mediator.Send(command);

            return Ok();
        }
    }
    public class OfferListQueryParams : IMapWith<GetOfferListQuery>
    {
        [ModelBinder(BinderType = typeof(SortingBinder))]
        public SortingQueryParams Sorting { get; set; } = new();

        [ModelBinder(BinderType = typeof(OfferFilterBinder))]
        public OfferFilterQueryParams Filter { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OfferListQueryParams, GetOfferListQuery>();
        }
    }
    public class OfferFilterQueryParams : IMapWith<OfferFilterDto>
    {
        public string? IsPublished { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OfferFilterQueryParams, OfferFilterDto>()
                .ForMember(to => to.IsPublished,
                    opt => opt.MapFrom(from =>
                        string.IsNullOrEmpty(from.IsPublished) ? (bool?)null :
                        (from.IsPublished.Equals("true", StringComparison.OrdinalIgnoreCase) ? (bool?)true : (bool?)false)));
        }
    }
}
