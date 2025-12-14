using App.Application.Commands.Days.AddDay;
using App.Application.Commands.Days.DeleteDay;
using App.Application.Queries.Days.GetDay.ByUserId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdwykLab.Domain.Entities;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DaysController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize]
        [HttpGet("me")] 
        public async Task<IActionResult> GetDayByUserId()
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            var result = await _mediator.Send(new GetDayByUserIdQuery(userId));
            return Ok(result);
        }

        [Authorize]
        [HttpPost] 
        public async Task<IActionResult> AddDay(bool isGood, DateOnly date)
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            var result = await _mediator.Send(new AddDayCommand(userId, isGood, date));
            return Ok(result);
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteDay([FromBody] DateOnly date)
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            var result = await _mediator.Send(new DeleteDayCommand(date, userId));
            return NoContent();
        }
    }
}
