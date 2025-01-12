using Ykotika.Domain.Entities;

namespace Ykotika.Domain.Interfaces
{
    public interface IContent
    {
        public bool IsPublished { get; set; }
        public User User { get; set; }
    }
}
