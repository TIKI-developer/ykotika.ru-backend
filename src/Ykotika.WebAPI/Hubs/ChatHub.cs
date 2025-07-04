using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;
using System.Text.Json;
using Ykotika.Application.Commands;
using Ykotika.Application.Queries;
using Ykotika.Application.ViewModels;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Hubs;

public interface IChatClient
{
    public Task ReceiveMessage(MessageItem message);
    public Task ReceiveChat(ChatDetails chat);
}

public class ChatHub(IMediator mediator, IMapper mapper, IDistributedCache cache) : Hub<IChatClient>
{
    private readonly IMediator _mediator = mediator;
    private readonly IMapper _mapper = mapper;
    private readonly IDistributedCache _cache = cache;

    internal Guid UserId => !Context.User.Identity.IsAuthenticated
        ? Guid.Empty
        : Guid.Parse(Context.User.FindFirst(ClaimTypes.NameIdentifier).Value);

    //[Authorize(Roles = $"{Roles.AUTHOR_ROLE}, {Roles.MODERATOR_ROLE}, {Roles.ADMIN_ROLE}")]
    public async Task Join(JoinChatDto dto)
    {
        Console.WriteLine($"Создаем connection {JsonSerializer.Serialize(dto)}");
        var connection = new ChatConnectionDto(UserId, dto.ChatId);
        Console.WriteLine("Подготавливаем запрос");
        var query = new GetChatQuery { Id = connection.ChatId };
        Console.WriteLine($"Выполняем запрос {JsonSerializer.Serialize(query)}");
        var chat = await _mediator.Send(query);
        Console.WriteLine($"Запрос выполнен. Результат: {JsonSerializer.Serialize(chat)}");
        Console.WriteLine($"Сериализуем подключение {JsonSerializer.Serialize(connection)}");
        var stringConnection = JsonSerializer.Serialize(connection);
        Console.WriteLine("Кешируем подключение");
        await _cache.SetStringAsync(Context.ConnectionId, stringConnection);

        Console.WriteLine("Успешно");
        Console.WriteLine("Добавляем в группу");
        await Groups
                .AddToGroupAsync(Context.ConnectionId, connection.ChatId.ToString());

        Console.WriteLine($"Отправляем клиенту чат {JsonSerializer.Serialize(chat)}");
        await Clients
                .Users(connection.UserId.ToString())
                .ReceiveChat(chat);
    }
    public async Task SendMessage(CreateMessageDto messageDto)
    {
        var stringConnection = await _cache.GetAsync(Context.ConnectionId);
        var connection = JsonSerializer.Deserialize<ChatConnectionDto>(stringConnection);

        if (connection is not null)
        {
            var command = _mapper.Map<CreateMessageCommand>(messageDto);
            command.SenderId = connection.UserId;
            command.ChatId = connection.ChatId;
            var message = await _mediator.Send(command);

            await Clients
                .Group(connection.ChatId.ToString())
                .ReceiveMessage(message);
        }
    }
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}
