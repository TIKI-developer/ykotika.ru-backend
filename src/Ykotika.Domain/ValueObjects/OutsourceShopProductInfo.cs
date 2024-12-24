using Ykotika.Domain.Entities;

namespace Ykotika.Domain.ValueObjects
{
    public class OutsourceShopProductInfo
    {
        public required OutsourceShop OutsourceShop { get; set; }
        public required string Link { get; set; }
    }
}