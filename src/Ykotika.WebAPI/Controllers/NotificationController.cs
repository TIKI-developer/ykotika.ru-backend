using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Commands;
using Ykotika.WebAPI.Constants;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Controllers
{
    [Route("notifications")]
    public class NotificationController : BaseController
    {
        [Authorize(Roles = $"{Roles.VERIFIED_ROLE}")]
        [HttpPatch("{id}")]
        public async Task<IActionResult>
            MarkAsRead(Guid id, [FromBody] UpdateNotificationReadStatusDto dto)
        {
            var command = new UpdateNotificationReadStatusCommand
            { Id = id, UserId = UserId, IsRead = dto.IsRead };
            await Mediator.Send(command);

            return Ok();
        }
        [Authorize(Roles = $"{Roles.VERIFIED_ROLE}")]
        [HttpPatch]
        public async Task<IActionResult>
        MarkAsReadAll([FromBody] UpdateNotificationReadStatusDto dto)
        {
            var command = new UpdateAllNotificationsReadStatusCommand
            { UserId = UserId, IsRead = dto.IsRead };
            await Mediator.Send(command);

            return Ok();
        }
    }
}
