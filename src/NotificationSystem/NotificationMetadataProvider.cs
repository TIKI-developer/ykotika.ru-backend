using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;

namespace Ykotika.NotificationSystem
{
    public class NotificationMetadataProvider(IYkotikaDbContext dbContext) : INotificationMetadataProvider
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task<Dictionary<string, string>> EnrichMetadataAsync(string type, Dictionary<string, string> initial)
        {
            var metadata = new Dictionary<string, string>(initial);

            if (type == "ChatMessage" && initial.TryGetValue("chatId", out var chatId))
            {
                var product = await
                    _dbContext
                    .Products
                    .FirstOrDefaultAsync(e => e.Discussion.Id == Guid.Parse(chatId));

                if (product != null)
                {
                    var discussion = product.Discussion;

                    if (discussion != null)
                    {
                        metadata["chatType"] = discussion.Type;
                        if (discussion.Type == "productDiscussion" && discussion != null)
                        {
                            metadata["productId"] = product.Id.ToString();
                        }
                    }
                }
            }

            return metadata;
        }
    }

}
