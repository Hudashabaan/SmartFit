using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFit.Application.Features.Meals.Commands.AddFoodToMeal.SmartFit.Application.Features.Meals.Commands.AddFoodToMeal;
using SmartFit.Application.Features.Meals.Commands.CreateMeal;
using SmartFit.Application.Features.Meals.Commands.Delete_Meal;
using SmartFit.Application.Features.Meals.Commands.Update_Meal;
using SmartFit.Application.Features.Meals.DTOs;
using SmartFit.Application.Features.Meals.Queries.GetMealsByDate;
using SmartFit.Application.Features.Meals.Queries.Summary;

namespace SmartFit.API.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MealsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MealsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateMealDto dto)
        {
            var result = await _mediator.Send(new CreateMealCommand(dto));
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetByDate(DateTime date)
        {
            var result = await _mediator.Send(new GetMealsByDateQuery(date));
            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateMealCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteMealCommand { Id = id });
            return Ok(result);
        }

        [Authorize]
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary(DateTime date)
        {
            var result = await _mediator.Send(new GetDailySummaryQuery(date));
            return Ok(result);
        }


        [HttpPost("add-food")]
        public async Task<IActionResult> AddMeal(AddFoodToMealCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
