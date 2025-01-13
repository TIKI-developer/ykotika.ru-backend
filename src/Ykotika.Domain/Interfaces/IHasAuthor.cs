using Ykotika.Domain.Entities;

namespace Ykotika.Domain.Interfaces
{
    public interface IHasAuthor
    {
        User User { get; set; }
    }
}
