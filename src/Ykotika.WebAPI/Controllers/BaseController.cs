using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Models;

namespace Ykotika.WebAPI.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        internal Guid UserId => !User.Identity.IsAuthenticated
            ? Guid.Empty
            : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        internal string UserEmail => !User.Identity.IsAuthenticated
            ? ""
            : User.FindFirst(ClaimTypes.Email).Value;
    }
    public class SortingQueryParams : IMapWith<SortingDto>
    {
        public string? SortBy { get; set; }
        public string? IsDescending { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SortingQueryParams, SortingDto>()
                .ForMember(to => to.SortBy,
                opt => opt.MapFrom(from => from.SortBy))
                .ForMember(to => to.IsDescending,
                opt => opt.MapFrom(from => from.IsDescending));
        }
    }
    public class PaginationQueryParams : IMapWith<PaginationDto>
    {
        public string? Page { get; set; }
        public string? PageSize { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PaginationQueryParams, PaginationDto>()
                .ForMember(to => to.Page,
                opt => opt.MapFrom(from => from.Page))
                .ForMember(to => to.PageSize,
                opt => opt.MapFrom(from => from.PageSize));
        }
    }
}
