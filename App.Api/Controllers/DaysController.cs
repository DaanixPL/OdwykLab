using App.Application.Commands.Days.AddDay;
using App.Application.Commands.Days.DeleteDay;
using App.Application.Queries.Days.GetDay.ByUserId;
using App.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DaysController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{userId}")] 
        public async Task<IActionResult> GetDaysByUserId([FromQuery] int userId)
        {
            var query = new GetDayByUserIdQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost] 
        public async Task<IActionResult> CreateDay([FromBody] AddDayCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [Authorize]
        [HttpDelete("{date}")]
        public async Task<IActionResult> DeleteDay(DateOnly date)
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
            var command = new DeleteDayCommand(date, userId);
            var result = await _mediator.Send(command);
            return NoContent();
        }
    }
}
