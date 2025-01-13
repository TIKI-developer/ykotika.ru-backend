using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Models
{
    public class AuthorFilterDto
    {
        public AuthorStatus? Status { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public AuthorRequest.ContactSocial? ContactSocial { get; set; }
    }
}
