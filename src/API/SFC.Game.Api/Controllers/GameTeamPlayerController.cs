using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SFC.Game.Api.Infrastructure.Extensions;
using SFC.Game.Api.Infrastructure.Models.Base;
using SFC.Game.Api.Infrastructure.Models.Game.Team.General.Find;
using SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Create;
using SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Creates;
using SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Find;
using SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Get;
using SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Update;
using SFC.Game.Api.Infrastructure.Models.Game.Team.Player.Updates;
using SFC.Game.Api.Infrastructure.Models.Pagination;
using SFC.Game.Application.Features.Common.Base;
using SFC.Game.Application.Features.Common.Extensions;
using SFC.Game.Application.Features.Game.Team.Player.Commands.Create;
using SFC.Game.Application.Features.Game.Team.Player.Commands.Creates;
using SFC.Game.Application.Features.Game.Team.Player.Commands.Update;
using SFC.Game.Application.Features.Game.Team.Player.Commands.Updates;
using SFC.Game.Application.Features.Game.Team.Player.Queries.Find;
using SFC.Game.Application.Features.Game.Team.Player.Queries.Find.Dto.Filters;
using SFC.Game.Application.Features.Game.Team.Player.Queries.Get;
using SFC.Game.Infrastructure.Constants;

namespace SFC.Game.Api.Controllers;

/// <summary>
/// Game teams controller
/// </summary>
[Tags("Game Team Players")]
[Route("api/Games/{id}/Teams/{teamId}")]
[ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
public class GameTeamPlayerController : ApiControllerBase
{
    /// <summary>
    /// Create new game team player.
    /// </summary>
    /// <param name="id">Game Id.</param>
    /// <param name="teamId">Team Id.</param>
    /// <param name="playerId">Player Id.</param>
    /// <param name="request">Create game team player request.</param>
    /// <returns>An ActionResult of type CreateGamTeamPlayerResponse</returns>
    /// <response code="201">Returns **new** created game team player.</response>
    /// <response code="400">Returns **validation** errors.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    [HttpPost("Players/{playerId}")]
    [Authorize(Policy.OwnGame)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CreateGameTeamPlayerResponse>> CreateGameTeamAsync(
        [FromRoute] long id, [FromRoute] long teamId, [FromRoute] long playerId, [FromBody] CreateGameTeamPlayerRequest request)
    {
        CreateGameTeamPlayerCommand command = Mapper.Map<CreateGameTeamPlayerCommand>(request)
                                                    .SetGameId(id)
                                                    .SetTeamId(teamId)
                                                    .SetPlayerId(playerId);

        CreateGameTeamPlayerViewModel model = await Mediator.Send(command).ConfigureAwait(false);

        return CreatedAtRoute("GetGameTeamPlayer", new
        {
            id = model.GameTeamPlayer.GameId,
            teamId = model.GameTeamPlayer.TeamId,
            playerId = model.GameTeamPlayer.PlayerId
        }, Mapper.Map<CreateGameTeamPlayerResponse>(model));
    }

    /// <summary>
    /// Creates new game team players.
    /// </summary>
    /// <param name="id">Game Id.</param>
    /// <param name="teamId">Team Id.</param>
    /// <param name="request">Create game team player request.</param>
    /// <returns>An ActionResult of type CreatesGamTeamPlayerResponse</returns>
    /// <response code="201">Returns **new** created game team player.</response>
    /// <response code="400">Returns **validation** errors.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    [HttpPost("Players")]
    [Authorize(Policy.OwnGame)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CreatesGameTeamPlayerResponse>> CreatesGameTeamAsync(
        [FromRoute] long id, [FromRoute] long teamId, [FromBody] CreatesGameTeamPlayerRequest request)
    {
        CreatesGameTeamPlayerCommand command = Mapper.Map<CreatesGameTeamPlayerCommand>(request)
                                                     .SetGameId(id)
                                                     .SetTeamId(teamId);

        CreatesGameTeamPlayerViewModel model = await Mediator.Send(command).ConfigureAwait(false);

#pragma warning disable CA2234 // Pass system uri objects instead of strings
        return Created(string.Empty, Mapper.Map<CreatesGameTeamPlayerResponse>(model));
#pragma warning restore CA2234 // Pass system uri objects instead of strings
    }

    /// <summary>
    /// Update game team player.
    /// </summary>
    /// <param name="id">Game Id.</param>
    /// <param name="teamId">Team Id.</param>
    /// <param name="playerId">Player Id.</param>
    /// <param name="request">Update game team player request.</param>
    /// <returns>No content</returns>
    /// <response code="204">Returns no content if game team player **successfully** updated.</response>
    /// <response code="400">Returns **validation** errors.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    /// <response code="403">Returns when **failed** authorization.</response>
    /// <response code="404">Returns when invite **not found**.</response>
    /// <response code="409">Returns when **flow validation** errors.</response>
    [HttpPut("Players/{playerId}")]
    [Authorize(Policy.OwnGame)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
    public async Task<ActionResult> UpdateGameTeamPlayerAsync(
        [FromRoute] long id, [FromRoute] long teamId, [FromRoute] long playerId, [FromBody] UpdateGameTeamPlayerRequest request)
    {
        UpdateGameTeamPlayerCommand command = Mapper.Map<UpdateGameTeamPlayerCommand>(request)
                                                    .SetGameId(id)
                                                    .SetTeamId(teamId)
                                                    .SetPlayerId(playerId);

        await Mediator.Send(command)
                      .ConfigureAwait(false);

        return NoContent();
    }

    /// <summary>
    /// Updates game team player.
    /// </summary>
    /// <param name="id">Game Id.</param>
    /// <param name="teamId">Team Id.</param>
    /// <param name="request">Update game team players request.</param>
    /// <returns>No content</returns>
    /// <response code="204">Returns no content if game team players **successfully** updated.</response>
    /// <response code="400">Returns **validation** errors.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    /// <response code="403">Returns when **failed** authorization.</response>
    /// <response code="404">Returns when invite **not found**.</response>
    /// <response code="409">Returns when **flow validation** errors.</response>
    [HttpPut("Players")]
    [Authorize(Policy.OwnGame)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
    public async Task<ActionResult> UpdatesGameTeamPlayerAsync(
        [FromRoute] long id, [FromRoute] long teamId, [FromBody] UpdatesGameTeamPlayerRequest request)
    {
        UpdatesGameTeamPlayerCommand command = Mapper.Map<UpdatesGameTeamPlayerCommand>(request)
                                                     .SetGameId(id)
                                                     .SetTeamId(teamId);

        await Mediator.Send(command)
                      .ConfigureAwait(false);

        return NoContent();
    }

    /// <summary>
    /// Return game team player model by unique identifier.
    /// </summary>
    /// <param name="id">Game Id.</param>
    /// <param name="teamId">Team Id.</param>
    /// <param name="playerId">Player Id.</param>
    /// <returns>An ActionResult of type GetGameTeamResponse</returns>
    /// <response code="200">Returns game team model.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    /// <response code="404">Returns when game team **not found** by unique identifier.</response>
    [HttpGet("Players/{playerId}", Name = "GetGameTeamPlayer")]
    [Authorize(Policy.General)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetGameTeamPlayerResponse>> GetGameTeamPlayerAsync(
        [FromRoute] long id, [FromRoute] long teamId, [FromRoute] long playerId)
    {
        GetGameTeamPlayerQuery query = new GetGameTeamPlayerQuery() { GameId = id, TeamId = teamId, PlayerId = playerId }
            .SetIncludes(Request.GetIncludes());

        GetGameTeamPlayerViewModel gameTeam = await Mediator.Send(query).ConfigureAwait(false);

        return Ok(Mapper.Map<GetGameTeamPlayerResponse>(gameTeam));
    }

    /// <summary>
    /// Return list of game team players.
    /// </summary>
    /// <param name="id">Game Id.</param>
    /// <param name="teamId">Team Id.</param>
    /// <param name="request">Get game team players request.</param>
    /// <returns>An ActionResult of type GetGameTeamsResponse</returns>
    /// <response code="200">Returns list of game team players with pagination header.</response>
    /// <response code="400">Returns **validation** errors.</response>
    /// <response code="401">Returns when **failed** authentication.</response>
    [HttpGet("Players/Find")]
    [Authorize(Policy.General)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetGameTeamPlayersResponse>> GetGameTeamPlayersAsync(
        [FromRoute] long id, [FromRoute] long teamId, [FromQuery] GetGameTeamPlayersRequest request)
    {
        BasePaginationRequest<GetGameTeamPlayersViewModel, GetGameTeamPlayersFilterDto> query = Mapper.Map<GetGameTeamPlayersQuery>(request)
                                                                                                      .SetGameId(id)
                                                                                                      .SetTeamId(teamId)
                                                                                                      .SetIncludes(Request.GetIncludes());

        GetGameTeamPlayersViewModel result = await Mediator.Send(query)
                                                     .ConfigureAwait(false);

        PageMetadataModel metadata = Mapper.Map<PageMetadataModel>(result.Metadata)
                                           .SetLinks(UriService, Request.QueryString.Value!, Request.Path.Value!);

        Response.AddPaginationHeader(metadata);

        return Ok(Mapper.Map<GetGameTeamPlayersResponse>(result));
    }
}