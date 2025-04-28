namespace Ykotika.WebAPI.Models
{
	public record ChatConnectionDto(Guid UserId, Guid ChatId); 
	public record JoinChatDto(Guid ChatId);
}