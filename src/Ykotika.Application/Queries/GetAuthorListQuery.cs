using MediatR;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Queries
{
    public class GetAuthorListQuery : IRequest<AuthorList>
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public AuthorRequest.ContactSocial? ContactSocial { get; set; }
        public AuthorStatus? Status { get; set; }
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; }
    }
}
