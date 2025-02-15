using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using StudioTG.Application.DTO.Requests;
using StudioTG.Application.DTO.Responses;
using StudioTG.Application.Interfaces;
using StudioTG.Domain.Entities.Fields;
using System.Diagnostics;

namespace StudioTG.Web.Web.Controllers
{
    [Route("api")]
    public class Minesweeper(IFieldService fieldService,
        IFieldSerializationService serializationService,
        IValidator<NewGameRequest> gameValidator,
        IValidator<MakeTurnRequest> turnValidator) : Controller
    {
        [Route("new")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FieldStateResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [DebuggerStepThrough]
        public async Task<ActionResult> NewGame([FromBody] NewGameRequest gameRequest, CancellationToken cancellationToken)
        {
            Log.Information("[{controller} Controller] Creating game with params {request}", nameof(Minesweeper), gameRequest);
            Log.Information("[{controller} Controller] Validation start", nameof(Minesweeper));
            gameValidator.ValidateAndThrow(gameRequest);
            Log.Information("[{controller} Controller] Request valid, creating game", nameof(Minesweeper));
            Field field = await fieldService.CreateFieldAsync(gameRequest.Width, gameRequest.Height, gameRequest.MinesCount, cancellationToken);
            Log.Information("[{controller} Controller] Game Id {Id}", nameof(Minesweeper), field.Id);
            return Ok(serializationService.Serialize(field));
        }

        [Route("turn")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FieldStateResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [DebuggerStepThrough]
        public async Task<ActionResult> GameTurn([FromBody] MakeTurnRequest turnRequest, CancellationToken cancellationToken)
        {
            Log.Information("[{controller} Controller] Make turn with params {request}", nameof(Minesweeper), turnRequest);
            Log.Information("[{controller} Controller] Validation start", nameof(Minesweeper));
            turnValidator.ValidateAndThrow(turnRequest);
            Log.Information("[{controller} Controller] Request valid, making turn", nameof(Minesweeper));
            Field field = await fieldService.MakeTurnAsync(turnRequest.Id, turnRequest.Row, turnRequest.Collumn, cancellationToken);
            Log.Information("[{controller} Controller] Made turn for game {Id}", nameof(Minesweeper), field.Id);
            return Ok(serializationService.Serialize(field));
        }
    }
}
